using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.Windows.Shared;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using AAEICS.Client.Messages;
using AAEICS.Client.Services;
using AAEICS.Client.Services.Language;
using AAEICS.Client.Services.Navigation;
using AAEICS.Client.Views;

using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace AAEICS.Client.ViewModels;




public partial class MainViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly ILanguageService _languageService;
    
    public Action RequestExit { get; set; }
    public Action RequestMinimize { get; set; }
    public Action RequestRestore { get; set; }
    
    
    private string _currentPageKey = "HomeAppTitle";
    private double _currentPageMinWidth;

    [ObservableProperty]
    private bool _isSideMenuVisible = true;
    
    [ObservableProperty]
    private bool _isFlyoutMenuVisible = false;
    
    [ObservableProperty]
    private double _windowMinWidth;
    
    [ObservableProperty]
    private string _appTitle = "";
    
    public MainViewModel(INavigationService navigationService, ILanguageService languageService)
    {
        _navigationService = navigationService;
        _languageService = languageService;
        
        UpdateTitle();
        
        WeakReferenceMessenger.Default.Register<LanguageChangedMessage>(this, (r, m) => UpdateTitle());
        WeakReferenceMessenger.Default.Register<NewCertificateMessage>(this, (r, m) => ShowResultPage(m.Value));
    }

    private void ShowResultPage(bool isSuccess)
    {
        if (isSuccess)
            _navigationService.NavigateTo(App.Services.GetRequiredService<SuccessPage>());
        else
            _navigationService.NavigateTo(App.Services.GetRequiredService<FailPage>());
    }
    
    [RelayCommand]
    private void CloseApp()
    {
        RequestExit?.Invoke();
    }

    [RelayCommand]
    private void MaximizeApp(Window window)
    {
        RequestRestore?.Invoke();
    }

    [RelayCommand]
    private void MinimizeApp(Window window)
    {
        RequestMinimize?.Invoke();
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
    
    public void RegisterPageConstraints(double minWidth)
    {
        _currentPageMinWidth = minWidth;
        UpdateWindowConstraints();
    }
    
    public void CheckLayoutRules(double currentWindowWidth)
    {
        double threshold = _currentPageMinWidth + UIConfig.SideMenuWidth;

        if ((IsSideMenuVisible && currentWindowWidth < threshold) || Math.Abs(currentWindowWidth - WindowMinWidth) < 1)
            IsSideMenuVisible = false;
        
        else if (!IsSideMenuVisible && currentWindowWidth > threshold + UIConfig.SideMenuBuffer)
        {
            IsSideMenuVisible = true;
            IsFlyoutMenuVisible = false;
        }
    }

    private void UpdateWindowConstraints()
    {
        WindowMinWidth = _currentPageMinWidth + UIConfig.SideMenuWidth;
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
            case "newIssuanceCertificate":
                _navigationService.NavigateTo(App.Services.GetRequiredService<IssuanceCertificatePage>());
                _currentPageKey = "NewIssuanceCertificateAppTitle";
                break;
            case "newWriteOffCertificate":
                _navigationService.NavigateTo(App.Services.GetRequiredService<WriteOffCertificatePage>());
                _currentPageKey = "NewWriteOffCertificateAppTitle";
                break;
        }
        UpdateTitle();
    }
    
    private void UpdateTitle()
    {
        string pageName = _languageService[_currentPageKey];
        AppTitle = $"{pageName}";
    }
}
