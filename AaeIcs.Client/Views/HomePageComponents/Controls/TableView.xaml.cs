using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using AAEICS.Client.Attributes;

namespace AAEICS.Client.Views.HomePageComponents.Controls;

public partial class TableView : UserControl
{
    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(TableView),
            new PropertyMetadata(null, OnItemsSourceChanged));

    public IEnumerable ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
    
    public static readonly DependencyProperty ShowOperationsColumnProperty =
        DependencyProperty.Register(nameof(ShowOperationsColumn), typeof(bool), typeof(TableView),
            new PropertyMetadata(false, OnItemsSourceChanged));

    public bool ShowOperationsColumn
    {
        get => (bool)GetValue(ShowOperationsColumnProperty);
        set => SetValue(ShowOperationsColumnProperty, value);
    }
    
    private ScrollViewer? _listboxScrollViewer;
    
    public TableView()
    {
        InitializeComponent();
        
        ItemsListBox.LayoutUpdated += ItemsListBox_LayoutUpdated;
    }
    
    private void ItemsListBox_LayoutUpdated(object? sender, EventArgs e)
    {
        // 1. Шукаємо фізичний ScrollViewer всередині ListBox (лише один раз)
        if (_listboxScrollViewer == null)
        {
            _listboxScrollViewer = FindVisualChild<ScrollViewer>(ItemsListBox);
        }

        if (_listboxScrollViewer != null)
        {
            // 2. Надійна математика: чи фізично контент більший за видиме вікно?
            bool needsScrollbar = _listboxScrollViewer.ExtentHeight > _listboxScrollViewer.ViewportHeight;

            // 3. Беремо точну ширину скролбару системи (замість жорстких 17px)
            double paddingRight = needsScrollbar ? SystemParameters.VerticalScrollBarWidth : 0;

            // 4. Застосовуємо відступ лише якщо він змінився (щоб уникнути нескінченного циклу оновлень макета)
            if (HeaderBorder.Padding.Right != paddingRight)
            {
                HeaderBorder.Padding = new Thickness(5, 5, paddingRight, 5);
            }
        }
    }

    // Допоміжний метод для пошуку реального елемента глибоко у візуальному дереві
    private static T? FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
    {
        if (parent == null) return null;
            
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
                
            if (child is T typedChild)
                return typedChild;
                    
            var result = FindVisualChild<T>(child);
            if (result != null)
                return result;
        }
        return null;
    }
    
    private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var table = (TableView)d;
        table.ItemsListBox.ItemsSource = e.NewValue as IEnumerable;
        table.GenerateColumns();
    }

    private void GenerateColumns()
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

        HeaderGrid.ColumnDefinitions.Clear();
        HeaderGrid.Children.Clear();

        var glc = new GridLengthConverter();
        StringBuilder xamlBuilder = new StringBuilder();
        
        xamlBuilder.Append("<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">");
        xamlBuilder.Append("<Border Style=\"{StaticResource TableItemStyle}\" >");
        xamlBuilder.Append("<Grid Width=\"{Binding ViewportWidth, RelativeSource={RelativeSource AncestorType={x:Type ScrollViewer}}}\">");
        xamlBuilder.Append("<Grid.ColumnDefinitions>");

        int columnIndex = 0;
        foreach (var item in properties)
        {
            GridLength width = (GridLength)glc.ConvertFromInvariantString(item.GridCol.Width);
            HeaderGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = width });

            TextBlock headerText = new TextBlock
            {
                Text = item.Display?.GetName() ?? item.Property.Name
            };
            
            headerText.SetResourceReference(StyleProperty, "TableHeaderTextStyle");
        
            Grid.SetColumn(headerText, columnIndex);
            HeaderGrid.Children.Add(headerText);
            
            xamlBuilder.Append($"<ColumnDefinition Width=\"{item.GridCol.Width}\" />");
            columnIndex++;
        }
        
        if (ShowOperationsColumn)
        {
            HeaderGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70) });

            TextBlock opsHeader = new TextBlock { Text = "Операції" };
            opsHeader.SetResourceReference(StyleProperty, "TableHeaderTextStyle");
            opsHeader.HorizontalAlignment = HorizontalAlignment.Center;
            
            Grid.SetColumn(opsHeader, columnIndex);
            HeaderGrid.Children.Add(opsHeader);

            xamlBuilder.Append("<ColumnDefinition Width=\"70\" />");
        }

        xamlBuilder.Append("</Grid.ColumnDefinitions>");
        
        columnIndex = 0;
        foreach (var item in properties)
        {
            string bindingFormat = !string.IsNullOrEmpty(item.GridCol.StringFormat)
                ? $", StringFormat='{{}}{{0:{item.GridCol.StringFormat}}}'"
                : "";

            xamlBuilder.Append($"<TextBlock Grid.Column=\"{columnIndex}\" ");
            xamlBuilder.Append($"Text=\"{{Binding {item.Property.Name}{bindingFormat}, ValidatesOnNotifyDataErrors=False}}\" ");
            xamlBuilder.Append("Style=\"{DynamicResource TableCellTextStyle}\" />");
        
            columnIndex++;
        }
        
        if (ShowOperationsColumn)
        {
            xamlBuilder.Append($"<ContentControl Grid.Column=\"{columnIndex}\" ");
            xamlBuilder.Append("Content=\"{Binding ValidatesOnNotifyDataErrors=False}\" ");
            xamlBuilder.Append("ContentTemplate=\"{DynamicResource OperationsCellTemplate}\" ");
            xamlBuilder.Append("HorizontalAlignment=\"Center\" VerticalAlignment=\"Center\" Margin=\"5,0\" />");
        }
        
        xamlBuilder.Append("</Grid></Border></DataTemplate>");
        
        var template = (DataTemplate)XamlReader.Parse(xamlBuilder.ToString());
        ItemsListBox.ItemTemplate = template;
    }
    
    private Type GetItemType(IEnumerable collection)
    {
        var type = collection.GetType();
        if (type.IsGenericType)
        {
            return type.GetGenericArguments()[0];
        }
        var enumerator = collection.GetEnumerator();
        if (enumerator.MoveNext() && enumerator.Current != null)
        {
            return enumerator.Current.GetType();
        }
        return null;
    }
}
