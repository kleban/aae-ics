using System.Collections;
using System.Reflection;
using AAEICS.Client.Services.Validation;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AAEICS.Client.Models;

public partial class PropertyItem : ObservableObject
{
    private object _value;
        
    public PropertyInfo PropertyInfo { get; set; } 
    public object TargetObject { get; set; } 

    public string Name => PropertyInfo.Name;

    public bool IsComplexType { get; set; }
    public IEnumerable AvailableOptions { get; set; }
    
    // Делегат, який ми будемо викликати, коли значення змінюється
    public Action OnValueChanged { get; set; }
    
    
    // 🔥 Нова властивість для зберігання тексту помилки
    [ObservableProperty]
    private string _errorMessage;


    public object Value
    {
        get => _value;
        set
        {
            if (SetProperty(ref _value, value))
            {
                if (value != null && !IsComplexType)
                {
                    var convertedValue = Convert.ChangeType(value, PropertyInfo.PropertyType);
                    PropertyInfo.SetValue(TargetObject, convertedValue);
                }
                else
                {
                    PropertyInfo.SetValue(TargetObject, value);
                }

                // 🔥 Одразу запускаємо валідацію при зміні тексту!
                Validate();
                
                // Повідомляємо ViewModel, щоб вона перевірила кнопку "Зберегти"
                OnValueChanged?.Invoke();
            }
        }
    }

    // 🔥 Метод, який підбирає правильну перевірку залежно від типу поля
    public void Validate()
    {
        if (IsComplexType)
        {
            ErrorMessage = ValidationService.ValidateComplex(Name, Value);
        }
        else if (PropertyInfo.PropertyType == typeof(string))
        {
            ErrorMessage = ValidationService.ValidateText(Name, Value);
        }
        else if (PropertyInfo.PropertyType == typeof(decimal) || PropertyInfo.PropertyType == typeof(int))
        {
            ErrorMessage = ValidationService.ValidateNumber(Name, Value);
        }
        else
        {
            ErrorMessage = null; // Для інших типів помилок поки не передбачено
        }
    }
}
