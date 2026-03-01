namespace AAEICS.Client.Services.Validation;

public static class ValidationService
{
    
    // Перевіряє, чи об'єкт взагалі існує
    public static bool IsNotNull(object value) => value != null;

    // Перевіряє, чи рядок не порожній
    public static bool IsNotEmptyString(object value) 
    {
        if (value is string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }
        return true; // Якщо це не рядок, ця перевірка його пропускає
    }

    // Перевіряє, чи число більше нуля (наприклад, для ціни)
    public static bool IsPositiveNumber(object value)
    {
        if (value is decimal dec) return dec > 0;
        if (value is int i) return i > 0;
        return true;
    }

    // 🔥 Твій "возик": комбінуємо базові перевірки для тексту
    public static bool IsValidText(object value)
    {
        return IsNotNull(value) && IsNotEmptyString(value);
    }

    // Комбінація для чисел
    public static bool IsValidNumber(object value)
    {
        return IsNotNull(value) && IsPositiveNumber(value);
    }



        // Валідація для тексту
        public static string ValidateText(string propertyName, object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return $"Поле '{propertyName}' не може бути порожнім.";
            
            return null; // Помилок немає
        }

        // Валідація для чисел (ціна, кількість тощо)
        public static string ValidateNumber(string propertyName, object value)
        {
            if (value == null) 
                return $"Поле '{propertyName}' є обов'язковим.";

            // Намагаємось перетворити значення на число і перевіряємо, чи воно > 0
            if (decimal.TryParse(value.ToString(), out decimal result) && result <= 0)
                return $"Значення '{propertyName}' має бути більшим за нуль.";

            return null;
        }

        // Валідація для випадаючого списку (ComboBox)
        public static string ValidateComplex(string propertyName, object value)
        {
            if (value == null)
                return $"Будь ласка, оберіть '{propertyName}' зі списку.";
            
            return null;
        }
    }
