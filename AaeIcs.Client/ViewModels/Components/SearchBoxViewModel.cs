using AAEICS.Client.Messages;
using AAEICS.Core.Contracts.Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AAEICS.Client.ViewModels.Components;

public partial class SearchBoxViewModel<T> : ObservableObject where T : class
{
    private readonly IFuzzySearchService<T> _searchService;
    private readonly Func<T, string> _propertySelector;

    public string DisplayPropertyName { get; }

    [ObservableProperty] 
    private T _selectedItem;

    // Використовуємо стандартну генерацію
    [ObservableProperty] 
    private string _searchText;

    // Додаємо властивість для керування розгортанням списку
    [ObservableProperty] 
    private bool _isDropdownOpen;

    public ObservableCollection<T> SearchResults { get; } = new ObservableCollection<T>();

    public SearchBoxViewModel(
        IFuzzySearchService<T> searchService, 
        Func<T, string> propertySelector,
        string displayPropertyName = "Name")
    {
        _searchService = searchService;
        _propertySelector = propertySelector;
        DisplayPropertyName = displayPropertyName;
    }

// 1. ОНОВЛЕНИЙ МЕТОД: Реакція на введення тексту
    partial void OnSearchTextChanged(string value)
    {
        if (SelectedItem != null)
        {
            var selectedText = _propertySelector(SelectedItem);
            if (value == selectedText) return; 
            
            SelectedItem = null;
        }

        SearchResults.Clear();
        
        // ВАЖЛИВО: Ми ПРИБРАЛИ return! 
        // Якщо value порожнє, наш FuzzySearchService поверне перші N елементів з бази.
        var results = _searchService.Search(value, _propertySelector, maxDistance: 2, topCount: 5);
        foreach (var item in results) 
        {
            SearchResults.Add(item);
        }

        // Відкриваємо випадаючий список ТІЛЬКИ якщо користувач щось ввів руками і ми це знайшли.
        // Якщо текст порожній - не чіпаємо IsDropdownOpen, щоб користувач міг сам закрити/відкрити кліком.
        if (!string.IsNullOrWhiteSpace(value))
        {
            IsDropdownOpen = SearchResults.Count > 0;
        }
    }

    // 2. НОВИЙ МЕТОД: Реакція на клік по стрілочці ComboBox
    // Цей метод автоматично генерується Toolkit-ом для властивості IsDropdownOpen
    partial void OnIsDropdownOpenChanged(bool value)
    {
        // Якщо користувач відкрив список, а він порожній (наприклад, при першому запуску)
        if (value && SearchResults.Count == 0)
        {
            // Примусово завантажуємо дефолтні дані (можна передати порожній рядок)
            var results = _searchService.Search(string.Empty, _propertySelector, maxDistance: 2, topCount: 10);
            foreach (var item in results)
            {
                SearchResults.Add(item);
            }
        }
    }

    [RelayCommand]
    private void HandleEnter()
    {
        if (SelectedItem != null) return;

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            WeakReferenceMessenger.Default.Send(new CreateNewItemMessage<T>(
                SearchText, newItem => SelectedItem = newItem));
        }
    }
}