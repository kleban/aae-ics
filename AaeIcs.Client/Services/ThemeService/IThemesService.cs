using AAEICS.Shared.Models;

namespace AAEICS.Client.Services.ThemeService;

public interface IThemesService
{
    IEnumerable<Theme> GetAllThemes();
    Theme GetThemeByName(string name);
    void SetTheme(Theme theme);
}