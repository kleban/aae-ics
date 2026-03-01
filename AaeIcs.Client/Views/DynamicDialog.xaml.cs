using System.Windows;

using AAEICS.Client.ViewModels;
using AAEICS.Core.Contracts.Services;
using AAEICS.Services;

namespace AAEICS.Client.Views;

public partial class DynamicDialog : Window
{
    private object _targetObject;
    private readonly DictionaryDataService _dataService;
    
    public DynamicDialog(object targetObject, IDictionaryDataService dataService)
    {
        InitializeComponent();

        // Створюємо екземпляр ViewModel
        var viewModel = new DynamicDialogViewModel(targetObject, dataService);

        // "Вчимо" ViewModel закривати це вікно
        viewModel.RequestClose = (dialogResult) =>
        {
            DialogResult = dialogResult;
            Close();
        };

        // Прив'язуємо ViewModel до інтерфейсу
        DataContext = viewModel;
    }
    
    // У коді вікна (Code-Behind), наприклад DynamicDialogWindow.xaml.cs
    private async void DynamicDialog_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is DynamicDialogViewModel viewModel)
            await viewModel.InitializeAsync();
    }
}