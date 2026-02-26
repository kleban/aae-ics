using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AAEICS.Client.Helpers; // УВАГА: Заміни на назву свого проєкту

// Клас AnketaGrid успадковує всі властивості звичайного Grid
public class FormGrid : Grid
{
    // Перевизначаємо метод OnRender, який відповідає за малювання елемента на екрані
    protected override void OnRender(DrawingContext dc)
    {
        // Викликаємо базовий метод, щоб Grid намалював свій фон та інші стандартні речі
        base.OnRender(dc);

        // Налаштовуємо наш "олівець" (Pen) для малювання ліній
        // Brushes.Gray - колір лінії (сірий)
        // 1.0 - товщина лінії в пікселях
        Pen dashedPen = new Pen(Brushes.Gray, 1.0);
            
        // Встановлюємо стиль лінії як "пунктир" (Dashed)
        dashedPen.DashStyle = DashStyles.Dash;

        // 1. Малюємо внутрішні горизонтальні лінії (між рядками)
        double currentY = 0; // Початкова позиція по вертикалі
            
        // Проходимо циклом по всіх рядках, ОКРІМ останнього (щоб не малювати зовнішню лінію знизу)
        for (int i = 0; i < RowDefinitions.Count - 1; i++)
        {
            // Додаємо висоту поточного рядка до нашої координати Y
            currentY += RowDefinitions[i].ActualHeight;
                
            // Малюємо лінію зліва направо на поточній висоті currentY
            dc.DrawLine(dashedPen, new Point(0, currentY), new Point(this.ActualWidth, currentY));
        }

        // 2. Малюємо внутрішні вертикальні лінії (між колонками)
        double currentX = 0; // Початкова позиція по горизонталі
            
        // Проходимо циклом по всіх колонках, ОКРІМ останньої (щоб не малювати зовнішню лінію справа)
        for (int i = 0; i < ColumnDefinitions.Count - 1; i++)
        {
            // Додаємо ширину поточної колонки до координати X
            currentX += ColumnDefinitions[i].ActualWidth;
                
            // Малюємо лінію зверху вниз на поточній ширині currentX
            dc.DrawLine(dashedPen, new Point(currentX, 0), new Point(currentX, this.ActualHeight));
        }
    }
}