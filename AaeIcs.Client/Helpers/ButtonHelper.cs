using System.Windows;

namespace AAEICS.Client.Helpers;

public static class ButtonHelper
{
    // Реєструємо властивість CornerRadius для всіх елементів типу DependencyObject
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.RegisterAttached(
            "CornerRadius", 
            typeof(CornerRadius), 
            typeof(ButtonHelper), 
            new PropertyMetadata(new CornerRadius(0)));

    public static void SetCornerRadius(DependencyObject obj, CornerRadius value) => obj.SetValue(CornerRadiusProperty, value);
    public static CornerRadius GetCornerRadius(DependencyObject obj) => (CornerRadius)obj.GetValue(CornerRadiusProperty);
}
