using System.Windows;
using System.Windows.Controls;
using AAEICS.Client.Services;
using AAEICS.Client.ViewModels;
using Syncfusion.UI.Xaml.Grid;

namespace AAEICS.Client.Views;

public partial class HomePage : Page
{
// Ваше "бажання" по ширині для цієї конкретної сторінки
    private const double PageMinimumWidth = 840;

    public HomePage(HomePageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

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
    
// // Створюємо об'єкт опцій один раз, щоб не навантажувати пам'ять
//     private GridRowSizingOptions _sizingOptions = new GridRowSizingOptions();
//
//     private void DataGrid_QueryRowHeight(object sender, QueryRowHeightEventArgs e)
//     {
//         // Розраховуємо висоту для всіх рядків, крім заголовка (Index 0)
//         if (e.RowIndex > 0)
//         {
//             if (this.DataGrid.GridColumnSizer.GetAutoRowHeight(e.RowIndex, _sizingOptions, out double autoHeight))
//             {
//                 if (autoHeight > 32)
//                 {
//                     e.Height = autoHeight;
//                     e.Handled = true;
//                 }
//             }
//         }
//     }
//     private void DataGrid_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
//     {
//         // Перевіряємо, чи це текстова колонка
//         if (e.Column is GridTextColumn textColumn)
//         {
//             textColumn.TextWrapping = TextWrapping.Wrap;
//         }
//     }
}