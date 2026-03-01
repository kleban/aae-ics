using System.Windows;
using System.Windows.Controls;

namespace AAEICS.Client.Views.Components;

public partial class QuestionBoxControl : UserControl
{
    public QuestionBoxControl()
    {
        InitializeComponent();
    }

    // Властивість для тексту питання
    public string QuestionText
    {
        get => (string)GetValue(QuestionTextProperty);
        set => SetValue(QuestionTextProperty, value);
    }

    public static readonly DependencyProperty QuestionTextProperty =
        DependencyProperty.Register(nameof(QuestionText), typeof(string), typeof(QuestionBoxControl), new PropertyMetadata(string.Empty));

    // Властивість для вкладених елементів керування (TextBox, DatePicker тощо)
    public object InputControls
    {
        get => GetValue(InputControlsProperty);
        set => SetValue(InputControlsProperty, value);
    }

    public static readonly DependencyProperty InputControlsProperty =
        DependencyProperty.Register(nameof(InputControls), typeof(object), typeof(QuestionBoxControl), new PropertyMetadata(null));
}
