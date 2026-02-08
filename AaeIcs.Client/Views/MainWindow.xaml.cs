using AAEICS.Client.ViewModels;

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
    
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    
    // private void NavigationDrawer_ItemClicked(object sender, NavigationItemClickedEventArgs e)
    // {
    //     var clickedItem = e.Item;
    //
    //     if (clickedItem == null) return;
    //     
    //     switch (clickedItem.Tag.ToString())
    //     {
    //         case "home":
    //             MainContentFrame.Navigate(App.Services.GetRequiredService<HomePage>());
    //             Title = "AAC-ICS Client | Home";
    //             break;
    //         case "settings":
    //             MainContentFrame.Navigate(new SettingsPage());
    //             Title = "AAC-ICS Client | Settings";
    //             break;
    //     }
    // }
    
    private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            if (e.ClickCount == 2)
            {
                _isDoubleClick = true;
                WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
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
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;

            var currentPoint = e.GetPosition(this);
            
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
    // private void ChromelessWindow_SizeChanged(object sender, SizeChangedEventArgs e)
    // {
    //     double windowWidth = e.NewSize.Width;
    //         
    //     // Захист від мінімальних значень при старті
    //     if (windowWidth < 100) return;
    //
    //     if (windowWidth <= 1001)
    //     {
    //         // === РЕЖИМ: МАЛЕНЬКЕ ВІКНО ===
    //             
    //         // 1. Встановлюємо фіксовану ширину колонки Grid
    //         SidebarColumn.Width = new GridLength(60);
    //             
    //         // 2. Налаштовуємо Drawer
    //         if (NavigationDrawer.DisplayMode != DisplayMode.Compact)
    //         {
    //             NavigationDrawer.DisplayMode = DisplayMode.Compact;
    //             NavigationDrawer.CompactModeWidth = 60;
    //             NavigationDrawer.IsToggleButtonVisible = true;
    //         }
    //     }
    //     else
    //     {
    //         // // === РЕЖИМ: ВЕЛИКЕ ВІКНО ===
    //         //     
    //         // // 1. Рахуємо 25% від ширини вікна
    //         double targetWidth = windowWidth * 0.25;
    //         //
    //         // // 2. Встановлюємо ширину колонки Grid в ПІКСЕЛЯХ
    //         // // Це виправляє баг "зникання", бо ми не використовуємо Star (пропорції), 
    //         // // які можуть ламатися при перерахунках.
    //         SidebarColumn.Width = new GridLength(targetWidth);
    //         //
    //         // 3. Змушуємо Drawer прийняти цю ширину
    //         // Це виправляє баг "не хоче розтягуватись"
    //         if (NavigationDrawer.DrawerWidth != targetWidth)
    //         {
    //             NavigationDrawer.ExpandedModeWidth = targetWidth;
    //         }
    //
    //         // 4. Вмикаємо Expanded
    //         if (NavigationDrawer.DisplayMode != DisplayMode.Expanded)
    //         {
    //             NavigationDrawer.DisplayMode = DisplayMode.Expanded;
    //             NavigationDrawer.IsOpen = true;
    //             NavigationDrawer.IsToggleButtonVisible = false;
    //         }
    //     }
    // }
}
