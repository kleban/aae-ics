using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using AAEICS.Client.Attributes;

namespace AAEICS.Client.Views.HomePageComponents.Controls;

public partial class CardView : UserControl
{
    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(CardView),
            new PropertyMetadata(null, OnItemsSourceChanged));

    public IEnumerable ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly DependencyProperty ShowOperationsColumnProperty =
        DependencyProperty.Register(nameof(ShowOperationsColumn), typeof(bool), typeof(CardView),
            new PropertyMetadata(false, OnItemsSourceChanged));

    public bool ShowOperationsColumn
    {
        get => (bool)GetValue(ShowOperationsColumnProperty);
        set => SetValue(ShowOperationsColumnProperty, value);
    }

    public CardView()
    {
        InitializeComponent();
    }

    private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var view = (CardView)d;
        view.ItemsListBox.ItemsSource = view.ItemsSource;
        view.GenerateCards();
    }

    private void GenerateCards()
    {
        if (ItemsSource == null) return;

        Type itemType = GetItemType(ItemsSource);
        if (itemType == null) return;

        var properties = itemType.GetProperties()
            .Where(p => Attribute.IsDefined(p, typeof(GridColumnAttribute)))
            .Select(p => new
            {
                Property = p,
                GridCol = (GridColumnAttribute)Attribute.GetCustomAttribute(p, typeof(GridColumnAttribute)),
                Display = (DisplayAttribute)Attribute.GetCustomAttribute(p, typeof(DisplayAttribute))
            })
            .ToList();

        if (!properties.Any()) return;

        StringBuilder xamlBuilder = new StringBuilder();
        
        // Відкриваємо шаблон картки
        xamlBuilder.Append("<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">");
        xamlBuilder.Append("<Border Style=\"{StaticResource CardItemStyle}\">");
        
        xamlBuilder.Append("<Grid>");
        xamlBuilder.Append("<Grid.ColumnDefinitions>");
        xamlBuilder.Append("<ColumnDefinition Width=\"*\" />");
        if (ShowOperationsColumn) xamlBuilder.Append("<ColumnDefinition Width=\"Auto\" />");
        xamlBuilder.Append("</Grid.ColumnDefinitions>");

        // Блок з даними (Ліва колонка)
        xamlBuilder.Append("<StackPanel Grid.Column=\"0\">");

        bool isFirst = true;
        foreach (var item in properties)
        {
            string headerTextValue = item.Display?.GetName() ?? item.Property.Name;
            string bindingFormat = !string.IsNullOrEmpty(item.GridCol.StringFormat)
                ? $", StringFormat='{{}}{{0:{item.GridCol.StringFormat}}}'"
                : "";

            if (isFirst)
            {
                // ПЕРША ВЛАСТИВІСТЬ: Робимо її заголовком картки (Жирним шрифтом)
                xamlBuilder.Append("<TextBlock FontWeight=\"Bold\" FontSize=\"16\" TextWrapping=\"Wrap\" Margin=\"0,0,0,5\">");
                xamlBuilder.Append($"<Run Text=\"{headerTextValue}: \" />");
                xamlBuilder.Append($"<Run Text=\"{{Binding {item.Property.Name}{bindingFormat}}}\" />");
                xamlBuilder.Append("</TextBlock>");
    
                isFirst = false;
            }
            else
            {
                // ІНШІ ВЛАСТИВОСТІ: Звичайний текст з сірим підписом
                xamlBuilder.Append("<TextBlock Margin=\"0,5,0,0\" TextWrapping=\"Wrap\">");
                xamlBuilder.Append($"<Run Text=\"{headerTextValue}: \" Foreground=\"Gray\" />");
                xamlBuilder.Append($"<Run Text=\"{{Binding {item.Property.Name}{bindingFormat}}}\" />");
                xamlBuilder.Append("</TextBlock>");
            }
        }

        xamlBuilder.Append("</StackPanel>");

        // Блок з кнопками (Права колонка)
        if (ShowOperationsColumn)
        {
            xamlBuilder.Append("<ContentControl Grid.Column=\"1\" Content=\"{Binding}\" ContentTemplate=\"{DynamicResource OperationsCellTemplate}\" VerticalAlignment=\"Top\" />");
        }

        xamlBuilder.Append("</Grid></Border></DataTemplate>");

        var template = (DataTemplate)XamlReader.Parse(xamlBuilder.ToString());
        ItemsListBox.ItemTemplate = template;
    }

    private Type GetItemType(IEnumerable collection)
    {
        var type = collection.GetType();
        if (type.IsGenericType) return type.GetGenericArguments()[0];
        var enumerator = collection.GetEnumerator();
        if (enumerator.MoveNext() && enumerator.Current != null) return enumerator.Current.GetType();
        return null;
    }
}
