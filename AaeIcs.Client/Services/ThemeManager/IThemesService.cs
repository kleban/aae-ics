using AAEICS.Client.Models;

namespace AAEICS.Client.Services.ThemeManager;

public interface IThemesService
{
    IEnumerable<Theme> GetAllThemes();
    Theme GetDefaultTheme();
    Theme? GetThemeByName(string name);
    void SetTheme(Theme theme);
}