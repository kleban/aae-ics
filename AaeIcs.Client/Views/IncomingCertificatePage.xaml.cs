using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AAEICS.Client.Services;
using AAEICS.Client.ViewModels;

namespace AAEICS.Client.Views;

public partial class IncomingCertificatePage : Page
{
    public IncomingCertificatePage(IncomingCertificateViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        Loaded += async (s, e) => 
        {
            await viewModel.InitializeAsync();
        };
    }
    
    private const double PageMinimumWidth = 840;
    
    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        if (Window.GetWindow(this) is IWindowController windowController)
            windowController.RegisterPageMinWidth(PageMinimumWidth);
    }
    
    private void BlockBrowseBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = false;
        e.Handled = true; 
    }
    
    private void DatePicker_ValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
    {
        if (dependencyObject is FrameworkElement element && element.Tag != null)
        {
            string propertyName = element.Tag.ToString();
            
            if (dependencyPropertyChangedEventArgs.NewValue is DateTime newDate && DataContext is IncomingCertificateViewModel vm)
                vm.DatesChanged(propertyName, newDate);
        }
    }
}