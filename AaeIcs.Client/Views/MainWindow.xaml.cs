using AAEICS.Client.ViewModels;

using Syncfusion.Windows.Shared;

using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using AAEICS.Client.Services;
using AAEICS.Client.Services.NavigationManager;

namespace AAEICS.Client.Views;

public partial class MainWindow: ChromelessWindow, IWindowController
{
    private MainViewModel ViewModel => DataContext as MainViewModel;
    
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        App.Services.GetRequiredService<INavigationService>().MainFrame = MainFrame;
    }
    
    public void ShowMenu() => ViewModel.IsSideMenuVisible = true;
    public void HideMenu() => ViewModel.IsSideMenuVisible = false;
    
    public void RegisterPageMinWidth(double minWidth) => ViewModel.RegisterPageConstraints(minWidth);
    
    public void CheckLayoutRules(double currentWindowWidth) => ViewModel.CheckLayoutRules(currentWindowWidth);
    
    public bool IsMenuVisible => ViewModel.IsSideMenuVisible;
    
    private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        CheckLayoutRules(e.NewSize.Width);
    }
}
