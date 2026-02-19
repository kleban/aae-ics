using System.Windows.Controls;
using AAEICS.Client.ViewModels;

namespace AAEICS.Client.Views;

public partial class SettingsPage : Page
{
    public SettingsPage(SettingsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}