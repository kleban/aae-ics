using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using AAEICS.Client.ViewModels.Components;

namespace AAEICS.Client.Helpers;

public static class DataGridBehavior
{
    public static readonly DependencyProperty KeepEditingTextProperty =
        DependencyProperty.RegisterAttached(
            "KeepEditingText",
            typeof(bool),
            typeof(DataGridBehavior),
            new PropertyMetadata(false, OnKeepEditingTextChanged));
    
    private static readonly DependencyProperty SavedOriginalTextProperty =
        DependencyProperty.RegisterAttached(
            "SavedOriginalText", 
            typeof(string), 
            typeof(DataGridBehavior));

    public static bool GetKeepEditingText(DependencyObject obj) => 
        (bool)obj.GetValue(KeepEditingTextProperty);
    public static void SetKeepEditingText(DependencyObject obj, bool value) => 
        obj.SetValue(KeepEditingTextProperty, value);
    
    private static void OnKeepEditingTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not DataGrid dataGrid) return;
        
        if ((bool)e.NewValue)
        {
            dataGrid.BeginningEdit += DataGrid_BeginningEdit;
            dataGrid.PreparingCellForEdit += DataGrid_PreparingCellForEdit;
        }
        else
        {
            dataGrid.BeginningEdit -= DataGrid_BeginningEdit;
            dataGrid.PreparingCellForEdit -= DataGrid_PreparingCellForEdit;
        }
    }
    
    private static void DataGrid_BeginningEdit(object? sender, DataGridBeginningEditEventArgs e)
    {
        if (sender is not DataGrid dataGrid) return;
        
        if (e.Column is DataGridTextColumn)
        {
            var cellContent = e.Column.GetCellContent(e.Row);
            if (cellContent is TextBlock textBlock)
                dataGrid.SetValue(SavedOriginalTextProperty, textBlock.Text);
        }
    }
    
    private static void DataGrid_PreparingCellForEdit(object? sender, DataGridPreparingCellForEditEventArgs e)
    {
        if (e.EditingElement is not TextBox textBox || sender is not DataGrid dataGrid) return;
        
        string originalText = (string)dataGrid.GetValue(SavedOriginalTextProperty) ?? string.Empty;
        
        if (e.EditingEventArgs is TextCompositionEventArgs textArgs)
        {
            textBox.Text = originalText + textArgs.Text;
            
            textArgs.Handled = true;
            
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                textBox.Focus();
                textBox.SelectionLength = 0;
                textBox.CaretIndex = textBox.Text.Length;
            }), DispatcherPriority.Normal);
        }
        else
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                textBox.Focus();
                textBox.SelectionLength = 0;
                textBox.CaretIndex = textBox.Text.Length;
            }), DispatcherPriority.Background);
        }
    }
}