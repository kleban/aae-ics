using AAEICS.Core.DTO.Certificates;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using AAEICS.Client.Models;
using AAEICS.Services.IncomingCertificates;

namespace AAEICS.Client.ViewModels;

public partial class TableRowItem : ObservableObject
{
    [ObservableProperty]
    private bool _isConfirmed;
    
    [ObservableProperty]
    private string _description;
    
    [ObservableProperty]
    private string _value;
    
    [ObservableProperty]
    private string _value1;
    
    [ObservableProperty]
    private string _value2;

}

public partial class HomePageViewModel : ObservableObject
{
    
    // Це і є та сама властивість DashboardItems, до якої ми робили Binding у XAML!
    [ObservableProperty]
    private ObservableCollection<DashboardCard> _dashboardItems = new();
    
    // Приклад того, як ви зможете оновлювати значення з коду пізніше
    public void UpdateSalesData(string newSalesValue)
    {
        // Оскільки це ObservableProperty, зміна цього поля автоматично оновить цифру на екрані!
        DashboardItems[0].CardValue = newSalesValue; 
    }

    [ObservableProperty]
    private ObservableCollection<IncomingCertificateDTO> _incomingCertificates = new();
    
    private bool _isSyncing;
    
    [ObservableProperty] 
    private ObservableCollection<TableRowItem> _tableItems;

    [ObservableProperty]
    private bool _isAllConfirmed;
    
    public HomePageViewModel()
    {
        TableItems =
        [
            new TableRowItem { Description = "Приклад запису 1", Value = "100", IsConfirmed = true, Value1 = "100", Value2 = "100" },
            new TableRowItem { Description = "Приклад запису 2", Value = "250", IsConfirmed = false, Value1 = "100", Value2 = "100" }
        ];
        
        // Ініціалізуємо наш список
        DashboardItems =
        [
            new DashboardCard(
                // ВИКОРИСТОВУЄМО nameof() ! Ми передаємо КЛЮЧ, а не сам текст.
                titleKey: nameof(Resources.Languages.Resources.CreatedIncomingCertificatesTodayDashboardTitle),
                descriptionKey: nameof(Resources.Languages.Resources.CreatedIncomingCertificatesTodayDashboardTitle),
                icon: Application.Current.Resources["Icon.Product"],
                initialValue: "0"
            ),

            new DashboardCard(
                titleKey: nameof(Resources.Languages.Resources.CreatedIssuanceCertificatesTodayDashboardTitle),
                descriptionKey: nameof(Resources.Languages.Resources.CreatedIssuanceCertificatesTodayDashboardTitle),
                icon: Application.Current.Resources["Icon.User"],
                initialValue: "0"
            ),
            
            new DashboardCard(
                titleKey: nameof(Resources.Languages.Resources.CreatedWtriteOffCertificatesTodayDashboardTitle), // Наприклад: "Продажі"
                icon: Application.Current.Resources["Icon.Product"],
                descriptionKey: nameof(Resources.Languages.Resources.CreatedIncomingCertificatesTodayDashboardTitle), // Наприклад: "За цей місяць"
                initialValue: "0" // Початкове значення
            ),


            new DashboardCard(
                titleKey: nameof(Resources.Languages.Resources.CreatedIssuanceCertificatesTodayDashboardTitle),
                descriptionKey: nameof(Resources.Languages.Resources.CreatedIssuanceCertificatesTodayDashboardTitle),
                icon: Application.Current.Resources["Icon.User"],
                initialValue: "0"
            )

            // Ви можете додати стільки карток, скільки вам потрібно!
            // 1. Слухаємо зміни в колекції (додавання/видалення рядків)

        ];

        // Заповнюємо мозаїку дашбордів. 
        // Підтягуємо текст із файлів локалізації (Resources)
        // Іконки беремо з ресурсів додатку (якщо вони у вас там лежать)

        // Ви можете додати стільки карток, скільки вам потрібно!
        // 1. Слухаємо зміни в колекції (додавання/видалення рядків)
        TableItems.CollectionChanged += OnTableItemsCollectionChanged;

        // 2. Підписуємось на події вже існуючих елементів
        foreach (var item in TableItems)
        {
            item.PropertyChanged += OnItemPropertyChanged;
        }

        // 3. Робимо первинну перевірку заголовка
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

            if (TableItems != null)
            {
                foreach (var item in TableItems)
                {
                    item.IsConfirmed = value;
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
    private void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TableRowItem.IsConfirmed))
        {
            CheckHeaderState();
        }
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
            IsAllConfirmed = TableItems.All(x => x.IsConfirmed);
        }
        finally
        {
            _isSyncing = false;
        }
    }

    // Обробка додавання/видалення нових рядків
    private void OnTableItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        // Якщо додали нові рядки - підписуємось на їх зміни
        if (e.NewItems != null)
        {
            foreach (TableRowItem item in e.NewItems)
            {
                item.PropertyChanged += OnItemPropertyChanged;
            }
        }

        // Якщо видалили рядки - відписуємось (щоб не було витоку пам'яті)
        if (e.OldItems != null)
        {
            foreach (TableRowItem item in e.OldItems)
            {
                item.PropertyChanged -= OnItemPropertyChanged;
            }
        }

        // Перевіряємо заголовок, бо кількість елементів змінилася
        CheckHeaderState();
    }
    
    [RelayCommand]
    private void AddRow(object obj)
    {
        TableItems.Add(new TableRowItem { Description = "Новий запис", Value = "0", IsConfirmed = false });
    }

    [RelayCommand]
    private void DeleteRow(object item)
    {
        if (item is TableRowItem rowItem)
        { 
            TableItems.Remove(rowItem); 
        }
        
    }
}