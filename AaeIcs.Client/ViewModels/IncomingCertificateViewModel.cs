using AAEICS.Client.Models;

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using AAEICS.Client.Messages;
using AAEICS.Core.Contracts.Services;
using AAEICS.Core.DTO.Certificates;
using AAEICS.Core.DTO.General;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AAEICS.Client.ViewModels;

public partial class IncomingCertificateViewModel: ObservableObject
{
    private bool _isSyncing;
    private readonly IIncomingCertificateService _incomingCertificateService;
    
    [ObservableProperty]
    private IncomingCertificate _incomingCertificate = new();
    
    [ObservableProperty]
    private ObservableCollection<IncomingCertificateLine> _incomingCertificateLines = [];

    [ObservableProperty]
    private ObservableCollection<MeasureUnitDTO> _measureUnitsList = [
        new()
        {
            UnitId = 1,
            Name = "штуки"
        },
        new()
        {
            UnitId = 2,
            Name = "комплект"
        },
        new()
        {
            UnitId = 3,
            Name = "одиниця"
        }
    ];
    
    
    [ObservableProperty]
    private bool _isAllConfirmed;

    public IncomingCertificateViewModel(IIncomingCertificateService incomingCertificateService)
    {
        _incomingCertificateService = incomingCertificateService;
        IncomingCertificateLines.CollectionChanged += OnIncomingCertificateLinesCollectionChanged;

        foreach (var actLine in IncomingCertificateLines)
        {
            actLine.PropertyChanged += OnCertificateLinePropertyChanged;
        }

        
        CheckHeaderState();
    }
    
    // Логіка: Заголовок -> Рядки
    // Викликається, коли користувач клікає на Checkbox у шапці
    partial void OnIsAllConfirmedChanged(bool value)
    {
        // Якщо ми зараз всередині процесу синхронізації (змінюємо заголовок через рядок),
        // то не треба спускати зміни назад у рядки.
        if (_isSyncing) return;

        try
        {
            _isSyncing = true; // Блокуємо зворотній зв'язок

            if (IncomingCertificateLines != null)
            {
                foreach (var actLine in IncomingCertificateLines)
                {
                    actLine.IsConfirmed = value;
                }
            }
        }
        finally
        {
            _isSyncing = false; // Знімаємо блокування
        }
    }

    // Логіка: Рядки -> Заголовок
    // Викликається, коли змінюється будь-яка властивість всередині рядка
    private void OnCertificateLinePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IncomingCertificateLine.IsConfirmed))
            CheckHeaderState();
    }

    // Допоміжний метод перевірки стану
    private void CheckHeaderState()
    {
        if (_isSyncing) return;

        try
        {
            _isSyncing = true; // Блокуємо спуск змін у рядки

            // Якщо всі рядки IsConfirmed == true, то і заголовок ставимо в true.
            // Інакше - false.
            IsAllConfirmed = IncomingCertificateLines.All(x => x.IsConfirmed);
        }
        finally
        {
            _isSyncing = false;
        }
    }

    // Обробка додавання/видалення нових рядків
    private void OnIncomingCertificateLinesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        // Якщо додали нові рядки - підписуємось на їх зміни
        if (e.NewItems != null)
        {
            foreach (IncomingCertificateLine item in e.NewItems)
            {
                item.PropertyChanged += OnCertificateLinePropertyChanged;
            }
        }

        // Якщо видалили рядки - відписуємось (щоб не було витоку пам'яті)
        if (e.OldItems != null)
        {
            foreach (IncomingCertificateLine item in e.OldItems)
            {
                item.PropertyChanged -= OnCertificateLinePropertyChanged;
            }
        }

        // Перевіряємо заголовок, бо кількість елементів змінилася
        CheckHeaderState();
    }
    
    
    [RelayCommand]
    private void AddRow(object obj)
    {
        IncomingCertificateLines.Add(new IncomingCertificateLine { IsConfirmed = false });
    }

    [RelayCommand]
    private void DeleteRow(object item)
    {
        if (item is IncomingCertificateLine actLine)
            IncomingCertificateLines.Remove(actLine); 
    }

    [RelayCommand]
    private async Task AddCertificateAsync() // 1. Змінили на async Task
    {
        // Завжди корисно перевірити, чи ініціалізована модель
        if (IncomingCertificate == null)
            return;

        // 2. Мапимо рядки сертифіката (ObservableCollection -> List<DTO>)
        // LINQ Select дозволяє перетворити кожен елемент однієї колекції на інший тип
        var linesDto = IncomingCertificateLines.Select((line, index) => new IncomingCertificateLineDTO
        {
            Name = line.Name,
            NomenclatureCode = line.NomenclatureCode,
            BatchNumber = line.BatchNumber,
            OrdinalNumber = index + 1,

            // Зверни увагу: у твоїй WPF моделі MeasureUnit - це int. 
            // DTO очікує об'єкт MeasureUnitDTO. Тому ми створюємо його і передаємо ID.
            MeasureUnit = new MeasureUnitDTO { UnitId = line.MeasureUnit },

            PricePerUnit = line.PricePerUnit,
            QuantitySent = line.QuantitySent,
            CategorySent = line.CategorySent,
            QuantityReceived = line.QuantityReceived,
            CategoryReceived = line.CategoryReceived,
            Notes = line.Notes
        }).ToList();

        // 3. Мапимо головний сертифікат (Model -> DTO)
        var incomingCertificateDTO = new IncomingCertificateDTO
        {
            Edrpou = IncomingCertificate.Edrpou,
            ApproveDate = IncomingCertificate.ApproveDate,
            RegistrationDate = IncomingCertificate.RegistrationDate,
            RegistrationPlace = IncomingCertificate.RegistrationPlace,
            TransferDateStart = IncomingCertificate.TransferDateStart,
            TransferDateEnd = IncomingCertificate.TransferDateEnd,

            // УВАГА: Тут я припускаю, що ти вже змінив string-поля на int-поля (ID), 
            // як ми обговорювали раніше. Якщо ні, доведеться це зробити!
            /*
            DonorId = IncomingCertificate.DonorId,
            RecipientId = IncomingCertificate.RecipientId,
            DeliveryCompany = IncomingCertificate.DeliveryCompanyId,
            Reason = IncomingCertificate.ReasonId,
            ApprovePerson = new PersonnelDTO { PersonId = IncomingCertificate.ApprovePersonId },
            */

            CertificateLines = linesDto
        };

        try
        {
            // 4. Передаємо DTO у сервіс. Використовуємо await, щоб дочекатися завершення!
            var result = await _incomingCertificateService.AddIncomingCertificateAsync(incomingCertificateDTO, linesDto);
            WeakReferenceMessenger.Default.Send(new NewCertificateMessage(result));
                

            // Тут можна додати код для очищення форми, наприклад:
            // IncomingCertificate = new IncomingCertificate();
            // IncomingCertificateLines.Clear();
        }
        catch (Exception ex)
        {
            var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            System.Windows.MessageBox.Show($"Сталася помилка при збереженні:\n{errorMessage}", "Помилка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            WeakReferenceMessenger.Default.Send(new NewCertificateMessage(false));
        }
    }
}