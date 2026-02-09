using System.Collections.ObjectModel;
using AAEICS.Client.Services.ThemeService;

using AAEICS.Shared.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.Windows.Shared;
using System.Windows;

namespace AAEICS.Client.ViewModels;


public partial class TableRowItem : ObservableObject
{
    [ObservableProperty]
    private bool _isConfirmed;
    
    [ObservableProperty]
    private string _description;
    
    [ObservableProperty]
    private string _value;
    
}



public partial class MainViewModel : ObservableObject
{
    private readonly IThemesService _themesService;

    [ObservableProperty]
    private bool _isPanelVisible = false;

    [ObservableProperty]
    private IEnumerable<Theme> _availableThemes;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDarkTheme))]
    private Theme _currentTheme;

    [ObservableProperty] 
    private ObservableCollection<TableRowItem> _tableItems;

    public MainViewModel(IThemesService themesDataService)
    {
        _themesService = themesDataService;
        AvailableThemes = _themesService.GetAllThemes();
        CurrentTheme = AvailableThemes.FirstOrDefault(t => t.Name == "Light");
        TableItems =
        [
            new TableRowItem { Description = "Приклад запису 1", Value = "100", IsConfirmed = true },
            new TableRowItem { Description = "Приклад запису 2", Value = "250", IsConfirmed = false }
        ];
    }
    

    [RelayCommand]
    private void CloseApp(ChromelessWindow window)
    {
        window.Close();
    }

    [RelayCommand]
    private void MaximizeApp(ChromelessWindow window)
    {
        window.WindowState = window.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
    }

    [RelayCommand]
    private void MinimizeApp(ChromelessWindow window)
    {
        window.WindowState = WindowState.Minimized;
    }
    
    [RelayCommand]
    public void ShowMenu()
    {
        IsPanelVisible = true;
    }
    
    [RelayCommand]
    private void CloseMenu()
    {
        IsPanelVisible = false;
    }

    [RelayCommand]
    private void ChangeTheme(string theme)
    {
        var newTheme = _themesService.GetThemeByName(theme);
        _themesService.SetTheme(newTheme);
        CurrentTheme = newTheme;
    }
    
    [RelayCommand]
    private void AddRow(object obj)
    {
        TableItems.Add(new TableRowItem { Description = "Новий запис", Value = "0", IsConfirmed = false });
    }


    // 2. Властивість-обгортка для ToggleButton
    public bool IsDarkTheme
    {
        get
        {
            // Логіка визначення: чи є поточна тема темною
            return CurrentTheme?.Path?.Contains("DarkTheme") ?? false;
        }
        set
        {
            // Коли натискаємо качельку:
            if (IsDarkTheme == value) return;
            
            string targetTheme = value ? "Dark" : "Light";
            ChangeTheme(targetTheme);
        }
    }
}
