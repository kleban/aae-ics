using System.Windows;
using System.Windows.Input;
using Syncfusion.UI.Xaml.NavigationDrawer;
using Syncfusion.Windows.Shared;

namespace aae_ics.client;

public partial class MainWindow: ChromelessWindow
{
    private Point _startPoint;
    private bool _isDoubleClick;
    
    public MainWindow()
    {
        InitializeComponent();
        
        this.StateChanged += MainWindow_StateChanged;
        MainContentFrame.Navigate(new HomePage());
    }

    private void NavigationDrawer_ItemClicked(object sender, NavigationItemClickedEventArgs e)
    {
        // e.Item - це натиснутий NavigationItem
        var clickedItem = e.Item;

        if (clickedItem == null) return;
        
        switch (clickedItem.Tag.ToString())
        {
            case "home":
                MainContentFrame.Navigate(new HomePage());
                this.Title = "AEMs Client | Home";
                break;
            case "settings":
                MainContentFrame.Navigate(new SettingsPage());
                this.Title = "AEMs Client | Settings";
                break;
        }
    }

    // Виправляємо баг із радіусом і рамками при зміні стану
    private void MainWindow_StateChanged(object sender, EventArgs e)
    {
        if (this.WindowState == WindowState.Maximized)
        {
            this.CornerRadius = new CornerRadius(0);
            this.BorderThickness = new Thickness(0);
            this.ResizeBorderThickness = new Thickness(0);
        }
        else
        {
            this.CornerRadius = new CornerRadius(10);
            this.BorderThickness = new Thickness(1);
            this.ResizeBorderThickness = new Thickness(5);
        }
    }

    // Виправляємо баг із подвійним кліком
    // 1. Запам'ятовуємо точку натискання і обробляємо подвійний клік
    private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            // Якщо подвійний клік — змінюємо стан
            if (e.ClickCount == 2)
            {
                _isDoubleClick = true;
                Maximize_Click(sender, e);
                e.Handled = true;
            }
            else
            {
                // Якщо одинарний — просто запам'ятовуємо де натиснули
                // НЕ викликаємо DragMove тут!
                _isDoubleClick = false;
                _startPoint = e.GetPosition(this);
            }
        }
    }
    
    // 2. Запускаємо перетягування тільки якщо мишка реально рухається
    private void TitleBar_MouseMove(object sender, MouseEventArgs e)
    {
        if (_isDoubleClick) return;
        // Перевіряємо, чи натиснута ліва кнопка
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            Point currentPoint = e.GetPosition(this);
    
            // Розраховуємо різницю між точкою натискання і поточною позицією
            // SystemParameters.MinimumHorizontalDragDistance — це системний поріг "тремтіння" миші
            if (Math.Abs(currentPoint.X - _startPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(currentPoint.Y - _startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                // Тільки тепер запускаємо перетягування
                // Try/Catch потрібен, бо іноді DragMove може конфліктувати з іншими подіями
                try
                {
                    this.DragMove();
                }
                catch { }
            }
        }
    }

    // 2. ЛОГІКА КНОПОК
    private void ToggleSidebar_Click(object sender, RoutedEventArgs e)
    {
        NavigationDrawer.IsOpen = !NavigationDrawer.IsOpen;
    }

    private void Minimize_Click(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    private void Maximize_Click(object sender, RoutedEventArgs e)
    {
        this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
