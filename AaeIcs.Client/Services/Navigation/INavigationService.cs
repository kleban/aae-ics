using System.Windows.Controls;

namespace AAEICS.Client.Services.Navigation;

public interface INavigationService
{
    public Frame MainFrame { get; set; }
    public void NavigateTo(Page page);
}