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
    }
    
    private const double PageMinimumWidth = 840;
    
    // Коли сторінка з'являється - реєструємо її вимоги
    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        if (Window.GetWindow(this) is IWindowController windowController)
        {
            // Кажемо: "Для цієї сторінки контенту треба мінімум 800px"
            // (Меню сюди не входить, вікно саме додасть 200px зверху)
            windowController.RegisterPageMinWidth(PageMinimumWidth);
        }
    }
    
    private void BlockBrowseBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        // Кажемо WPF, що ця команда зараз НЕ може бути виконана
        e.CanExecute = false;
            
        // Вказуємо, що ми вже обробили цю подію, 
        // тому WPF не повинен шукати інші способи її виконати
        e.Handled = true; 
    }
}