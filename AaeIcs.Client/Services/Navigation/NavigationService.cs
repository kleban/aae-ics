using System.Windows.Controls;

namespace AAEICS.Client.Services.Navigation;

public class NavigationService: INavigationService
{
    public Frame MainFrame { get; set; } = new();
    
    public void NavigateTo(Page page)
    {
        MainFrame.Navigate(page);
    }
}