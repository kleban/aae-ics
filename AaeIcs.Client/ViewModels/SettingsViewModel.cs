using AAEICS.Client.Services.LanguageManager;
using AAEICS.Client.Services.ThemeManager;

using AAEICS.Shared.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AAEICS.Client.ViewModels;

public partial class SettingsViewModel: ObservableObject
{
    [ObservableProperty]
    private IEnumerable<Theme> _availableThemes;
    
    [ObservableProperty]
    private IEnumerable<string> _availableLanguageNames;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDarkTheme))]
    private Theme _currentTheme;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEnglishLanguage))]
    private Language _currentLanguage;
    
    private readonly IThemesService _themesService;
    private readonly ILanguageService _languageService;
    
    public SettingsViewModel(IThemesService themesService, ILanguageService languageService)
    {
        _themesService = themesService;
        _languageService = languageService;
        AvailableThemes = _themesService.GetAllThemes();
        CurrentTheme = _themesService.GetDefaultTheme();
        CurrentLanguage = _languageService.GetDefaultLanguage();
    }
    
    [RelayCommand]
    private void ChangeTheme(string theme)
    {
        var newTheme = _themesService.GetThemeByName(theme);
        _themesService.SetTheme(newTheme);
        CurrentTheme = newTheme;
    }
    
    public bool IsDarkTheme
    {
        get => CurrentTheme.Path.Contains("DarkColors");
        set
        {
            if (IsDarkTheme == value) return;
            
            var targetTheme = value ? "Dark" : "Light";
            ChangeTheme(targetTheme);
        }
    }
    
    [RelayCommand]
    private void ChangeLanguage(string language)
    {
        var newLanguage = _languageService.GetLanguageByName(language);
        _languageService.SetLanguage(newLanguage);
        CurrentLanguage = newLanguage;
    }
    
    public bool IsEnglishLanguage
    {
        get => CurrentLanguage.Path.Contains("Lang.en-US");
        set
        {
            if (IsEnglishLanguage == value) return;
            
            var targetLanguage = value ? "English" : "Ukrainian";
            ChangeLanguage(targetLanguage);
        }
    }
}