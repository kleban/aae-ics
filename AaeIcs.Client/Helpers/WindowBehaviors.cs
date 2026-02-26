using System.Windows;
using System.Windows.Input;

namespace AAEICS.Client.Helpers;

public static class WindowBehaviors
{   
    private static bool _isDoubleClick;
    
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
    
    private static void OnEnableDragChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not UIElement element) return;

        if ((bool)e.NewValue)
        {
            element.MouseDown += Element_MouseDown;
            element.MouseMove += Element_MouseMove;
        }


        else
        {
            element.MouseDown -= Element_MouseDown; 
            element.MouseMove -= Element_MouseMove;
        }
    }
    
    private static void Element_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton != MouseButton.Left) return;
        
        var window = Window.GetWindow((DependencyObject)sender);
        
        if (e.ClickCount == 2)
        {
            _isDoubleClick = true;
            window?.WindowState = window.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
            e.Handled = true;
        }
        else
            _isDoubleClick = false;
    }
    
    private static void Element_MouseMove(object sender, MouseEventArgs e)
    {
        if (_isDoubleClick) return;
        if (e.LeftButton != MouseButtonState.Pressed) return;
        
        var window = Window.GetWindow((DependencyObject)sender);
        
        try
        {
            window?.WindowState = WindowState.Normal;
            window?.DragMove();
        }
        catch { }
    }
}