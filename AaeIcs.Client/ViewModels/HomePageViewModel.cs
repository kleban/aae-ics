using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.ObjectModel;

using System.Windows;
using AAEICS.Client.Models;
using AAEICS.Core.Contracts.Services;

namespace AAEICS.Client.ViewModels;

public partial class HomePageViewModel : ObservableObject
{
    private readonly IIncomingCertificateService _incomingCertificateService;

    [ObservableProperty]
    private ObservableCollection<DashboardCard> _dashboardItems = new();
    
    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private ObservableCollection<IncomingCertificate> _incomingCertificates = new();
    
    private bool _isSyncing;
    
    [ObservableProperty]
    private bool _isAllConfirmed;
    
    public HomePageViewModel(IIncomingCertificateService incomingCertificateService)
    {
        _incomingCertificateService = incomingCertificateService;

        DashboardItems =
        [
            new DashboardCard(
                titleKey: nameof(Resources.Languages.Resources.CreatedIncomingCertificatesTodayDashboardTitle),
                descriptionKey: nameof(Resources.Languages.Resources.CreatedIncomingCertificatesTodayDashboardDescription),
                icon: Application.Current.Resources["Icon.Warehouse"],
                initialValue: 0
            ),

            new DashboardCard(
                titleKey: nameof(Resources.Languages.Resources.CreatedIssuanceCertificatesTodayDashboardTitle),
                descriptionKey: nameof(Resources.Languages.Resources.CreatedIssuanceCertificatesTodayDashboardDescription),
                icon: Application.Current.Resources["Icon.Stocks"],
                initialValue: 0
            ),
        ];
        
        _ = LoadDataAsync();
    }
    
    public void UpdateDashboardData(DashboardCard dashboardCard, int newValue)
    {
        dashboardCard.CardValue = newValue;
    }
    
    private async Task LoadDataAsync()
    {
        IsBusy = true;
        try
        {
            // Отримуємо дані з сервісу
            var data = await _incomingCertificateService.GetIncomingCertificates();
            
            // Очищуємо та заповнюємо колекцію
            IncomingCertificates.Clear();
            IncomingCertificates = new ObservableCollection<IncomingCertificate>(
                data.Select(item => new IncomingCertificate
                {
                    Edrpou = item.Edrpou,
                    ApprovePerson = item.ApprovePerson.LastName,
                    ApproveDate = item.ApproveDate,
                    RegistrationDate = item.RegistrationDate,
                    RegistrationPlace = item.RegistrationPlace,
                    TransferDateStart = item.TransferDateStart,
                    TransferDateEnd = item.TransferDateEnd,
                    Donor = item.Donor.Name,
                    Recipient = item.Recipient.Name,
                    DeliveryCompany = item.DeliveryCompany.Name,
                })
            );
            UpdateDashboardData(DashboardItems[0], IncomingCertificates.Count);
        }
        catch (Exception ex)
        {
            // Тут можна додати логіку обробки помилок (наприклад, діалогове вікно)
            MessageBox.Show($"Помилка завантаження даних: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }
    
    
    // Логіка: Заголовок -> Рядки
    // Викликається, коли користувач клікає на Checkbox у шапці
    // partial void OnIsAllConfirmedChanged(bool value)
    // {
    //     // Якщо ми зараз всередині процесу синхронізації (змінюємо заголовок через рядок),
    //     // то не треба спускати зміни назад у рядки.
    //     if (_isSyncing) return;
    //
    //     try
    //     {
    //         _isSyncing = true; // Блокуємо зворотній зв'язок
    //
    //         if (TableItems != null)
    //         {
    //             foreach (var item in TableItems)
    //             {
    //                 item.IsConfirmed = value;
    //             }
    //         }
    //     }
    //     finally
    //     {
    //         _isSyncing = false; // Знімаємо блокування
    //     }
    // }

    // Логіка: Рядки -> Заголовок
    // Викликається, коли змінюється будь-яка властивість всередині рядка
    // private void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
    // {
    //     if (e.PropertyName == nameof(TableRowItem.IsConfirmed))
    //     {
    //         CheckHeaderState();
    //     }
    // }

    // // Допоміжний метод перевірки стану
    // private void CheckHeaderState()
    // {
    //     if (_isSyncing) return;
    //
    //     try
    //     {
    //         _isSyncing = true; // Блокуємо спуск змін у рядки
    //
    //         // Якщо всі рядки IsConfirmed == true, то і заголовок ставимо в true.
    //         // Інакше - false.
    //         IsAllConfirmed = TableItems.All(x => x.IsConfirmed);
    //     }
    //     finally
    //     {
    //         _isSyncing = false;
    //     }
    // }

    // Обробка додавання/видалення нових рядків
    // private void OnTableItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    // {
    //     // Якщо додали нові рядки - підписуємось на їх зміни
    //     if (e.NewItems != null)
    //     {
    //         foreach (TableRowItem item in e.NewItems)
    //         {
    //             item.PropertyChanged += OnItemPropertyChanged;
    //         }
    //     }
    //
    //     // Якщо видалили рядки - відписуємось (щоб не було витоку пам'яті)
    //     if (e.OldItems != null)
    //     {
    //         foreach (TableRowItem item in e.OldItems)
    //         {
    //             item.PropertyChanged -= OnItemPropertyChanged;
    //         }
    //     }
    //
    //     // Перевіряємо заголовок, бо кількість елементів змінилася
    //     CheckHeaderState();
    // }
    
    // [RelayCommand]
    // private void AddRow(object obj)
    // {
    //     TableItems.Add(new TableRowItem { Description = "Новий запис", Value = "0", IsConfirmed = false });
    // }
    //
    // [RelayCommand]
    // private void DeleteRow(object item)
    // {
    //     if (item is TableRowItem rowItem)
    //     { 
    //         TableItems.Remove(rowItem); 
    //     }
    //     
    // }
}