using System.ComponentModel.DataAnnotations; // Додай цей using нагорі

namespace AAEICS.Client.Services.Validation;

public static class ValidationService
{
    // Перевірка для дат (не раніше 2000 року і не далі ніж 1 рік у майбутнє)
    public static ValidationResult? ValidateDate(DateTime date, ValidationContext context)
    {
        if (date.Year < 2000 || date > DateTime.Now.AddYears(1))
        {
            return new ValidationResult($"Поле '{context.MemberName}' містить нереалістичну дату.");
        }
        return ValidationResult.Success;
    }

    // Перевірка для чисел (тільки додатні, > 0)
    public static ValidationResult? ValidatePositiveNumber(decimal value, ValidationContext context)
    {
        if (value <= 0)
        {
            return new ValidationResult($"Значення '{context.MemberName}' має бути більшим за нуль.");
        }
        return ValidationResult.Success;
    }

    // Перевірка для тексту (не порожній)
    public static ValidationResult? ValidateNotEmpty(string value, ValidationContext context)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new ValidationResult($"Поле '{context.MemberName}' не може бути порожнім.");
        }
        return ValidationResult.Success;
    }
    
    // Перевірка спеціально для ЄДРПОУ (якщо це int)
// ValidationService.cs
    public static ValidationResult? ValidateEdrpou(string? value, ValidationContext context)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new ValidationResult("ЄДРПОУ не може бути порожнім.");
        }
    
        // Перевіряємо, чи складається рядок ТІЛЬКИ з цифр
        if (!value.All(char.IsDigit))
        {
            return new ValidationResult("ЄДРПОУ має містити лише цифри.");
        }

        // ЄДРПОУ юридичної особи має 8 цифр
        if (value.Length != 8)
        {
            return new ValidationResult("ЄДРПОУ повинен складатися рівно з 8 цифр.");
        }
    
        return ValidationResult.Success;
    }
    
    // Перевірка для decimal (Кількість)
    public static ValidationResult? ValidatePositiveDecimal(decimal value, ValidationContext context)
    {
        if (value <= 0)
        {
            return new ValidationResult("Значення має бути більшим за нуль.", new[] { context.MemberName });
        }
        return ValidationResult.Success;
    }

// Перевірка для double (Ціна)
    public static ValidationResult? ValidatePositiveDouble(double value, ValidationContext context)
    {
        if (value <= 0)
        {
            return new ValidationResult("Ціна має бути більшою за нуль.", new[] { context.MemberName });
        }
        return ValidationResult.Success;
    }

    
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
