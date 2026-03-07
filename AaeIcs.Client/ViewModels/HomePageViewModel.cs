using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using AAEICS.Client.Models;
using AAEICS.Core.Contracts.Services;
using Brush = System.Drawing.Brush;

namespace AAEICS.Client.ViewModels;

public class Member
{
    public string Character { get; set; }
    public SolidColorBrush? BgColor { get; set; }
    public string Number { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}

public partial class HomePageViewModel : ObservableObject
{
    private readonly IIncomingCertificateService _incomingCertificateService;

    [ObservableProperty]
    private ObservableCollection<DashboardCard> _dashboardItems = new();
    
    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private ObservableCollection<IncomingCertificate> _incomingCertificates = new();
    
    [ObservableProperty]
    ObservableCollection<Member> _members = new ObservableCollection<Member>();
    
    private bool _isSyncing;
    
    [ObservableProperty]
    private bool _isAllConfirmed;
    
    public HomePageViewModel(IIncomingCertificateService incomingCertificateService)
    {
        _incomingCertificateService = incomingCertificateService;
        var converter = new BrushConverter();
        
        Members.Add(new Member { Number = "1", Character = "J", BgColor = (SolidColorBrush)converter.ConvertFromString("#1098AD"), Name = "John Doe", Position = "Coach", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
        Members.Add(new Member { Number = "2", Character = "R", BgColor = (SolidColorBrush)converter.ConvertFromString("#1E88E5"), Name = "Reza Alavi", Position = "Administrator", Email = "reza110@hotmail.com", Phone = "254-451-7893" });
        Members.Add(new Member { Number = "3", Character = "D", BgColor = (SolidColorBrush)converter.ConvertFromString("#FF8F00"), Name = "Dennis Castillo", Position = "Coach", Email = "deny.cast@gmail.com", Phone = "125-520-0141" });
        Members.Add(new Member { Number = "4", Character = "G", BgColor = (SolidColorBrush)converter.ConvertFromString("#FF5252"), Name = "Gabriel Cox", Position = "Coach", Email = "coxcox@gmail.com", Phone = "808-635-1221" });
        Members.Add(new Member { Number = "5", Character = "L", BgColor = (SolidColorBrush)converter.ConvertFromString("#0CA678"), Name = "Lena Jones", Position = "Manager", Email = "lena.offi@hotmail.com", Phone = "320-658-9174" });
        Members.Add(new Member { Number = "6", Character = "B", BgColor = (SolidColorBrush)converter.ConvertFromString("#6741D9"), Name = "Benjamin Caliword", Position = "Administrator", Email = "beni12@hotmail.com", Phone = "114-203-6258" });
        Members.Add(new Member { Number = "7", Character = "S", BgColor = (SolidColorBrush)converter.ConvertFromString("#FF6D00"), Name = "Sophia Muris", Position = "Coach", Email = "sophi.muri@gmail.com", Phone = "852-233-6854" });
        Members.Add(new Member { Number = "8", Character = "A", BgColor = (SolidColorBrush)converter.ConvertFromString("#FF5252"), Name = "Ali Pormand", Position = "Manager", Email = "alipor@yahoo.com", Phone = "968-378-4849" });
        Members.Add(new Member { Number = "9", Character = "F", BgColor = (SolidColorBrush)converter.ConvertFromString("#1E88E5"), Name = "Frank Underwood", Position = "Manager", Email = "frank@yahoo.com", Phone = "301-584-6966" });
        Members.Add(new Member { Number = "10", Character = "S", BgColor = (SolidColorBrush)converter.ConvertFromString("#0CA678"), Name = "Saeed Dasman", Position = "Coach", Email = "saeed.dasi@hotmail.com", Phone = "817-320-5052" });

        Members.Add(new Member { Number = "11", Character = "J", BgColor = (SolidColorBrush)converter.ConvertFromString("#1098AD"), Name = "John Doe", Position = "Coach", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
        Members.Add(new Member { Number = "12", Character = "R", BgColor = (SolidColorBrush)converter.ConvertFromString("#1E88E5"), Name = "Reza Alavi", Position = "Administrator", Email = "reza110@hotmail.com", Phone = "254-451-7893" });
        Members.Add(new Member { Number = "13", Character = "D", BgColor = (SolidColorBrush)converter.ConvertFromString("#FF8F00"), Name = "Dennis Castillo", Position = "Coach", Email = "deny.cast@gmail.com", Phone = "125-520-0141" });
        Members.Add(new Member { Number = "14", Character = "G", BgColor = (SolidColorBrush)converter.ConvertFromString("#FF5252"), Name = "Gabriel Cox", Position = "Coach", Email = "coxcox@gmail.com", Phone = "808-635-1221" });
        Members.Add(new Member { Number = "15", Character = "L", BgColor = (SolidColorBrush)converter.ConvertFromString("#0CA678"), Name = "Lena Jones", Position = "Manager", Email = "lena.offi@hotmail.com", Phone = "320-658-9174" });
        Members.Add(new Member { Number = "16", Character = "B", BgColor = (SolidColorBrush)converter.ConvertFromString("#6741D9"), Name = "Benjamin Caliword", Position = "Administrator", Email = "beni12@hotmail.com", Phone = "114-203-6258" });
        Members.Add(new Member { Number = "17", Character = "S", BgColor = (SolidColorBrush)converter.ConvertFromString("#FF6D00"), Name = "Sophia Muris", Position = "Coach", Email = "sophi.muri@gmail.com", Phone = "852-233-6854" });
        Members.Add(new Member { Number = "18", Character = "A", BgColor = (SolidColorBrush)converter.ConvertFromString("#FF5252"), Name = "Ali Pormand", Position = "Manager", Email = "alipor@yahoo.com", Phone = "968-378-4849" });
        Members.Add(new Member { Number = "19", Character = "F", BgColor = (SolidColorBrush)converter.ConvertFromString("#1E88E5"), Name = "Frank Underwood", Position = "Manager", Email = "frank@yahoo.com", Phone = "301-584-6966" });
        Members.Add(new Member { Number = "20", Character = "S", BgColor = (SolidColorBrush)converter.ConvertFromString("#0CA678"), Name = "Saeed Dasman", Position = "Coach", Email = "saeed.dasi@hotmail.com", Phone = "817-320-5052" });

        Members.Add(new Member { Number = "21", Character = "J", BgColor = (SolidColorBrush)converter.ConvertFromString("#1098AD"), Name = "John Doe", Position = "Coach", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
        Members.Add(new Member { Number = "22", Character = "R", BgColor = (SolidColorBrush)converter.ConvertFromString("#1E88E5"), Name = "Reza Alavi", Position = "Administrator", Email = "reza110@hotmail.com", Phone = "254-451-7893" });
        Members.Add(new Member { Number = "23", Character = "D", BgColor = (SolidColorBrush)converter.ConvertFromString("#FF8F00"), Name = "Dennis Castillo", Position = "Coach", Email = "deny.cast@gmail.com", Phone = "125-520-0141" });
        Members.Add(new Member { Number = "24", Character = "G", BgColor = (SolidColorBrush)converter.ConvertFromString("#FF5252"), Name = "Gabriel Cox", Position = "Coach", Email = "coxcox@gmail.com", Phone = "808-635-1221" });
        Members.Add(new Member { Number = "25", Character = "L", BgColor = (SolidColorBrush)converter.ConvertFromString("#0CA678"), Name = "Lena Jones", Position = "Manager", Email = "lena.offi@hotmail.com", Phone = "320-658-9174" });
        Members.Add(new Member { Number = "26", Character = "B", BgColor = (SolidColorBrush)converter.ConvertFromString("#6741D9"), Name = "Benjamin Caliword", Position = "Administrator", Email = "beni12@hotmail.com", Phone = "114-203-6258" });
        Members.Add(new Member { Number = "27", Character = "S", BgColor = (SolidColorBrush)converter.ConvertFromString("#FF6D00"), Name = "Sophia Muris", Position = "Coach", Email = "sophi.muri@gmail.com", Phone = "852-233-6854" });
        Members.Add(new Member { Number = "28", Character = "A", BgColor = (SolidColorBrush)converter.ConvertFromString("#FF5252"), Name = "Ali Pormand", Position = "Manager", Email = "alipor@yahoo.com", Phone = "968-378-4849" });
        Members.Add(new Member { Number = "29", Character = "F", BgColor = (SolidColorBrush)converter.ConvertFromString("#1E88E5"), Name = "Frank Underwood", Position = "Manager", Email = "frank@yahoo.com", Phone = "301-584-6966" });
        Members.Add(new Member { Number = "30", Character = "S", BgColor = (SolidColorBrush)converter.ConvertFromString("#0CA678"), Name = "Saeed Dasman", Position = "Coach", Email = "saeed.dasi@hotmail.com", Phone = "817-320-5052" });
        

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