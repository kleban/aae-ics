using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.Windows.Shared;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using AAEICS.Client.Messages;
using AAEICS.Client.Services;
using AAEICS.Client.Services.LanguageManager;
using AAEICS.Client.Services.NavigationManager;
using AAEICS.Client.Views;

using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace AAEICS.Client.ViewModels;




public partial class MainViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
      // Прапорець, щоб уникнути зациклення подій
    
    private string _currentPageKey = "HomeAppTitle";

    [ObservableProperty]
    private bool _isSideMenuVisible = true;
    
    [ObservableProperty]
    private bool _isFlyoutMenuVisible = false;
    
    [ObservableProperty]
    private double _windowMinWidth;
    
    [ObservableProperty]
    private string _appTitle = "";



    public MainViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        WeakReferenceMessenger.Default.Register<LanguageChangedMessage>(this, (r, m) =>
        {
            UpdateTitle();
        });
        
        UpdateTitle(); // Первинне встановлення
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
    private void ShowSideMenu()
    {
        IsSideMenuVisible = true;
    }

    [RelayCommand]
    private void HideSideMenu()
    {
        IsSideMenuVisible = false;
    }
    
    [RelayCommand]
    private void ShowFlyoutMenu()
    {
        IsFlyoutMenuVisible = true;
    }
    
    [RelayCommand]
    private void CloseFlyoutMenu()
    {
        IsFlyoutMenuVisible = false;
    }
    
    // partial void OnWindowWidthChanged(double value)
    // {
    //     if (value > 1200 && IsFlyoutMenuVisible)
    //     {
    //         IsFlyoutMenuVisible = false;
    //         IsSideMenuVisible = true;
    //     }
    // }
    
    private double _currentPageMinWidth;

    // Цей метод викликає сторінка, коли вона завантажується
    public void RegisterPageConstraints(double minWidth)
    {
        _currentPageMinWidth = minWidth;
        UpdateWindowConstraints();
    }

    // Цей метод викликає сторінка, коли змінюється розмір вікна
    public void CheckLayoutRules(double currentWindowWidth)
    {
        // ПРАВИЛО 1: Якщо нам тісно (ширина менше суми сторінки і меню) -> ховаємо меню
        double threshold = _currentPageMinWidth + UIConfig.SideMenuWidth;

        if (IsSideMenuVisible && currentWindowWidth < threshold)
        {
            IsSideMenuVisible = false;
        }
        // ПРАВИЛО 2: Якщо місця стало багато -> показуємо меню назад (опціонально)
        else if (!IsSideMenuVisible && currentWindowWidth > threshold + 50) // +50 буфер, щоб не миготіло
        {
            IsSideMenuVisible = true;
            IsFlyoutMenuVisible = false;
        }
    }

    private void UpdateWindowConstraints()
    {
        // Мінімальна ширина вікна = Мінімум сторінки + (Ширина меню, якщо воно є)
        double menuPart = IsSideMenuVisible ? UIConfig.SideMenuWidth : 0;
        WindowMinWidth = _currentPageMinWidth + menuPart;
    }
    
    [RelayCommand]
    private void NavigateTo(string pageName)
    {
        if (string.IsNullOrEmpty(pageName)) return;
        
        switch (pageName)
        {
            case "home":
                _navigationService.NavigateTo(App.Services.GetRequiredService<HomePage>());
                _currentPageKey = "HomeAppTitle";
                break;
            case "settings":
                _navigationService.NavigateTo(App.Services.GetRequiredService<SettingsPage>());
                _currentPageKey = "SettingsAppTitle";
                break;
            case "newIncomingCertificate":
                _navigationService.NavigateTo(App.Services.GetRequiredService<IncomingCertificatePage>());
                _currentPageKey = "NewIncomingCertificateAppTitle";
                break;
        }
        UpdateTitle();
    }
    
    private void UpdateTitle()
    {
        string pageName = Application.Current.TryFindResource(_currentPageKey) as string ?? "";
        AppTitle = $"{pageName}";
    }
}
