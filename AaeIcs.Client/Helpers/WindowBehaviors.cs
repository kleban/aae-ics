using System.Windows;
using System.Windows.Input;

namespace AAEICS.Client.Helpers;

public static class WindowBehaviors
{
    // 1. Створюємо Attached Property "EnableDrag"
    public static readonly DependencyProperty EnableDragProperty =
        DependencyProperty.RegisterAttached(
            "EnableDrag",
            typeof(bool),
            typeof(WindowBehaviors),
            new PropertyMetadata(false, OnEnableDragChanged));

    public static bool GetEnableDrag(DependencyObject obj)
    {
        return (bool)obj.GetValue(EnableDragProperty);
    }

    public static void SetEnableDrag(DependencyObject obj, bool value)
    {
        obj.SetValue(EnableDragProperty, value);
    }

    // 2. Коли властивість змінюється (ми ставимо True в XAML), підписуємось на подію
    private static void OnEnableDragChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is UIElement element)
        {
            if ((bool)e.NewValue)
            {
                element.MouseLeftButtonDown += Element_MouseLeftButtonDown;
            }
            else
            {
                element.MouseLeftButtonDown -= Element_MouseLeftButtonDown;
            }
        }
    }

    // 3. Сама логіка перетягування
    private static void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        // Знаходимо вікно, якому належить цей елемент
        var window = Window.GetWindow((DependencyObject)sender);
            
        // Якщо клікнули двічі - розгортаємо (бонус!)
        if (e.ClickCount == 2)
        {
            window.WindowState = window.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
            e.Handled = true;
            return;
        }

        // Запускаємо стандартне перетягування
        window?.DragMove();
    }
}