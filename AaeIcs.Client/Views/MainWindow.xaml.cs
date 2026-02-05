using Syncfusion.UI.Xaml.NavigationDrawer;
using Syncfusion.Windows.Shared;

using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;

namespace AAEICS.Client.Views;

public partial class MainWindow: ChromelessWindow
{
    private Point _startPoint;
    private bool _isDoubleClick;
    
    public MainWindow()
    {
        InitializeComponent();
        
        StateChanged += MainWindow_StateChanged;
        MainContentFrame.Navigate(new NewIncomingCertificatePage());
    }
    
    private void MainWindow_StateChanged(object? sender, EventArgs e)
    {
        if (WindowState == WindowState.Maximized)
        {
            CornerRadius = new CornerRadius(0);
            BorderThickness = new Thickness(0);
            ResizeBorderThickness = new Thickness(0);
        }
        else
        {
            CornerRadius = new CornerRadius(10);
            BorderThickness = new Thickness(1);
            ResizeBorderThickness = new Thickness(5);
        }
    }

    private void NavigationDrawer_ItemClicked(object sender, NavigationItemClickedEventArgs e)
    {
        var clickedItem = e.Item;

        if (clickedItem == null) return;
        
        switch (clickedItem.Tag.ToString())
        {
            case "home":
                MainContentFrame.Navigate(App.Services.GetRequiredService<HomePage>());
                Title = "AAC-ICS Client | Home";
                break;
            case "settings":
                MainContentFrame.Navigate(new SettingsPage());
                Title = "AAC-ICS Client | Settings";
                break;
        }
    }
    
    private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            if (e.ClickCount == 2)
            {
                _isDoubleClick = true;
                Maximize_Click(sender, e);
                e.Handled = true;
            }
            else
            {
                _isDoubleClick = false;
                _startPoint = e.GetPosition(this);
            }
        }
    }
    
    private void TitleBar_MouseMove(object sender, MouseEventArgs e)
    {
        if (_isDoubleClick) return;

        if (e.LeftButton == MouseButtonState.Pressed)
        {
            Point currentPoint = e.GetPosition(this);
            
            if (Math.Abs(currentPoint.X - _startPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(currentPoint.Y - _startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                try
                {
                    DragMove();
                }
                catch { }
            }
        }
    }
    
    private void ToggleSidebar_Click(object sender, RoutedEventArgs e)
    {
        NavigationDrawer.IsOpen = !NavigationDrawer.IsOpen;
    }

    private void Minimize_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void Maximize_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
