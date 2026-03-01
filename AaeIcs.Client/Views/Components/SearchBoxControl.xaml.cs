using System.Windows.Controls;

namespace AAEICS.Client.Views.Components;

public partial class SearchBoxControl : UserControl
{
    public SearchBoxControl()
    {
        InitializeComponent();
    }
    
    // Запобігаємо дратівливому автовиділенню тексту при відкритті списку
    private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is ComboBox comboBox && comboBox.IsEditable)
        {
            // Дістаємо внутрішній TextBox з шаблону ComboBox
            if (comboBox.Template?.FindName("PART_EditableTextBox", comboBox) is TextBox textBox)
            {
                // Якщо WPF спробував щось виділити...
                if (textBox.SelectionLength > 0)
                {
                    // Скидаємо виділення
                    textBox.SelectionLength = 0;
                    // Примусово ставимо курсор у кінець введеного тексту
                    textBox.CaretIndex = textBox.Text.Length;
                }
            }
        }
    }
    
    private void ComboBox_DropDownClosed(object sender, EventArgs e)
    {
        // Перевіряємо, чи це дійсно наш ComboBox
        if (sender is ComboBox comboBox && comboBox.IsEditable)
        {
            // Шукаємо те саме текстове поле PART_EditableTextBox, яке ми створили в XAML
            if (comboBox.Template.FindName("PART_EditableTextBox", comboBox) as TextBox is TextBox textBox)
            {
                // Ставимо курсор на індекс 0 (на самий початок)
                textBox.CaretIndex = 0;
            
                // Або можна використати метод прокрутки (працює так само надійно):
                // textBox.ScrollToHome();
            }
        }
    }
}