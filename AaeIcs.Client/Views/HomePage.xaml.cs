using System.Windows;
using System.Windows.Controls;
using AAEICS.Client.Services;
using AAEICS.Client.ViewModels;

namespace AAEICS.Client.Views;

public partial class HomePage : Page
{
    private const double PageMinimumWidth = 480;

    public HomePage(HomePageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    
    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        if (Window.GetWindow(this) is IWindowController windowController)
            windowController.RegisterPageMinWidth(PageMinimumWidth);
    }
}