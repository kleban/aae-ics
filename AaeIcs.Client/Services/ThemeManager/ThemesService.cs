using AAEICS.Shared.Models;

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace AAEICS.Client.Services.ThemeManager;

public class ThemesService : IThemesService
{
    private readonly ObservableCollection<Theme> _themes = new();

    public ThemesService()
    {
        _themes.Add(new Theme
        {
            Name = "Light",
            Path = "Resources/Palettes/LightColors.xaml"
        });

        _themes.Add(new Theme
        {
            Name = "Dark",
            Path = "Resources/Palettes/DarkColors.xaml"
        });
    }
    
    public IEnumerable<Theme> GetAllThemes()
    {
        return _themes;
    }

    public Theme GetDefaultTheme()
    {
        return _themes[0];
    }

    public Theme? GetThemeByName(string name)
    {
        foreach (var theme in _themes)
        {
            if (string.Equals(theme.Name, name, StringComparison.OrdinalIgnoreCase))
            {
                return theme;
            }
        }
        Trace.WriteLine($"Theme not found: {name}");
        return null;
    }

    public void SetTheme(Theme theme)
    {
        if (theme == null)
        {
            Trace.WriteLine("Error setting theme: Attempting to set theme to null.");
            return;
        }

        try
        {
            var themeUri = new Uri(theme.Path, UriKind.RelativeOrAbsolute);
            Application.Current.Resources.MergedDictionaries[1].Source = themeUri;
        }
        catch (Exception ex)
        {
            Trace.WriteLine("Error setting theme: " + ex.Message);
        }
    }
}
