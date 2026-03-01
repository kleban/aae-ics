using System;
using System.Collections.ObjectModel;
using System.Reflection;
using AAEICS.Client.Models;
using AAEICS.Client.Services.Validation;
using AAEICS.Core.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AAEICS.Services; // Заміни на свій namespace для DictionaryDataService

namespace AAEICS.Client.ViewModels;

public partial class DynamicDialogViewModel : ObservableObject
{
    private readonly object _targetObject;
    private readonly IDictionaryDataService _dataService;

    // Делегат, який дозволить ViewModel "попросити" View закритися
    public Action<bool> RequestClose { get; set; }

    // Колекція для прив'язки до ItemsControl у XAML
    [ObservableProperty]
    private ObservableCollection<PropertyItem> _properties;

    // Заголовок вікна, можемо генерувати динамічно
    [ObservableProperty]
    private string _windowTitle;

    public DynamicDialogViewModel(object targetObject, IDictionaryDataService dataService)
    {
        _targetObject = targetObject;
        _dataService = dataService;
            
        Properties = new ObservableCollection<PropertyItem>();
        WindowTitle = $"Створення: {_targetObject.GetType().Name}";
    }
    
    public async Task InitializeAsync()
    {
        PropertyInfo[] objectProps = _targetObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in objectProps)
        {
            if (prop.Name.Contains("Id", StringComparison.OrdinalIgnoreCase)) continue;

            bool isSimpleType = prop.PropertyType.IsPrimitive || 
                                prop.PropertyType == typeof(string) || 
                                prop.PropertyType == typeof(decimal);

            var item = new PropertyItem
            {
                PropertyInfo = prop,
                TargetObject = _targetObject,
                Value = prop.GetValue(_targetObject),
                IsComplexType = !isSimpleType
            };

            // Завантаження даних для ComboBox
            if (!isSimpleType && _dataService != null)
            {
                MethodInfo getAllMethod = _dataService.GetType().GetMethod(nameof(DictionaryDataService.GetAllDataAsync));
                MethodInfo genericMethod = getAllMethod.MakeGenericMethod(prop.PropertyType);
                
                // 1. Викликаємо метод через рефлексію і кастуємо його до Task
                var task = (Task)genericMethod.Invoke(_dataService, null);
                
                // 2. Чекаємо завершення завантаження даних із бази/API
                await task;
                
                // 3. Дістаємо властивість Result із завершеного Task (наша IEnumerable<T>)
                var resultProperty = task.GetType().GetProperty("Result");
                item.AvailableOptions = (System.Collections.IEnumerable)resultProperty.GetValue(task);
            }

            Properties.Add(item);
        }
    }
    
    // 1. Створюємо метод, який перевіряє, чи МОЖНА зберігати (повертає true/false)
    private bool CanSave()
    {
        bool isValid = true;

        foreach (var prop in Properties)
        {
            // Про всяк випадок примусово валідуємо всі поля (раптом користувач ще нічого не вводив)
            prop.Validate();

            // Якщо хоча б в одного поля є повідомлення про помилку, зберігати не можна
            if (!string.IsNullOrEmpty(prop.ErrorMessage))
            {
                isValid = false;
            }
        }

        return isValid;
    }
    
    [RelayCommand]
    private void Save()
    {
        RequestClose?.Invoke(true);
    }
}
