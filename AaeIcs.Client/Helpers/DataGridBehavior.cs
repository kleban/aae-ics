using System.Windows;
using System.Windows.Controls;

namespace AAEICS.Client.Helpers;

public static class DataGridBehavior
{
    // Створюємо властивість, яку можна вмикати в XAML
    public static readonly DependencyProperty KeepEditingTextProperty =
        DependencyProperty.RegisterAttached(
            "KeepEditingText",
            typeof(bool),
            typeof(DataGridBehavior),
            new PropertyMetadata(false, OnKeepEditingTextChanged));

    public static bool GetKeepEditingText(DependencyObject obj)
    {
        return (bool)obj.GetValue(KeepEditingTextProperty);
    }

    public static void SetKeepEditingText(DependencyObject obj, bool value)
    {
        obj.SetValue(KeepEditingTextProperty, value);
    }

    private static void OnKeepEditingTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DataGrid dataGrid)
        {
            if ((bool)e.NewValue)
            {
                dataGrid.PreparingCellForEdit += DataGrid_PreparingCellForEdit;
            }
            else
            {
                dataGrid.PreparingCellForEdit -= DataGrid_PreparingCellForEdit;
            }
        }
    }

    private static void DataGrid_PreparingCellForEdit(object? sender, DataGridPreparingCellForEditEventArgs e)
    {
        // Шукаємо TextBox всередині клітинки
        if (e.EditingElement is TextBox textBox)
        {
            // Ця магія змушує DataGrid "забути", що він хотів замінити текст
            textBox.Focus();
            
            // Ставимо курсор в самий кінець тексту
            textBox.Select(textBox.Text.Length, 0);
        }
    }
}
