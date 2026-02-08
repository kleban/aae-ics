using AAEICS.Shared.Models;

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace AAEICS.Client.Services.ThemeService
{
    public class ThemesService : IThemesService
    {
        private readonly ObservableCollection<Theme> _themes = new();

        public ThemesService()
        {
            // Ми точно знаємо, які теми у нас є, тому просто додаємо їх до списку.
            // Шляхи вказуємо відносно кореня проекту (папка Themes).

            _themes.Add(new Theme
            {
                Name = "Light",
                Path = "Themes/LightTheme.xaml"
            });

            _themes.Add(new Theme
            {
                Name = "Dark",
                Path = "Themes/DarkTheme.xaml"
            });
        }

        #region IThemesDataService

        public IEnumerable<Theme> GetAllThemes()
        {
            return _themes;
        }

        public Theme GetThemeByName(string name)
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
                // Важливий момент: Index [2] вказує на третій словник у App.xaml.
                // Переконайся, що в App.xaml порядок такий:
                // 0: Styles.xaml
                // 1: DataTemplates.xaml
                // 2: DarkTheme.xaml (або будь-яка інша тема за замовчуванням) -> саме це ми міняємо

                var themeUri = new Uri(theme.Path, UriKind.RelativeOrAbsolute);
                Application.Current.Resources.MergedDictionaries[2].Source = themeUri;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error setting theme: " + ex.Message);
            }
        }

        #endregion
    }
}