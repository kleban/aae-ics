using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace AAEICS.Client.Services.Theme;

public class ThemesService : IThemesService
{
    private readonly ObservableCollection<Models.Theme> _themes = new();

    public ThemesService()
    {
        _themes.Add(new Models.Theme
        {
            Name = "Light",
            Path = "Resources/Palettes/LightColors.xaml"
        });

        _themes.Add(new Models.Theme
        {
            Name = "Dark",
            Path = "Resources/Palettes/DarkColors.xaml"
        });
    }
    
    public IEnumerable<Models.Theme> GetAllThemes()
    {
        return _themes;
    }

    public Models.Theme GetDefaultTheme()
    {
        return _themes[0];
    }

    public Models.Theme? GetThemeByName(string name)
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

    public void SetTheme(Models.Theme theme)
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
