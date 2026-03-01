namespace AAEICS.Client.Services.Theme;

public interface IThemesService
{
    IEnumerable<Models.Theme> GetAllThemes();
    Models.Theme GetDefaultTheme();
    Models.Theme? GetThemeByName(string name);
    void SetTheme(Models.Theme theme);
}