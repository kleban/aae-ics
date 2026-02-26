using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace AAEICS.Client.Views.IncomingCertificatePageComponents.Controls;

public partial class ActLinesDataGrid : UserControl
{
    public ActLinesDataGrid()
    {
        InitializeComponent();
    }
    
    // Реєструємо DependencyProperty з іменем SourceItems
    public static readonly DependencyProperty SourceItemsProperty =
        DependencyProperty.Register(
            nameof(SourceItems),     // Ім'я властивості
            typeof(IEnumerable),     // Тип даних (IEnumerable підходить для будь-яких списків/колекцій)
            typeof(ActLinesDataGrid), // Кому належить
            new PropertyMetadata(null));    // Значення за замовчуванням

    // Звичайна обгортка для зручного використання в коді
    public IEnumerable SourceItems
    {
        get => (IEnumerable)GetValue(SourceItemsProperty);
        set => SetValue(SourceItemsProperty, value);
    }
}