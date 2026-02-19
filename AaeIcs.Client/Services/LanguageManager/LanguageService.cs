using System.Collections.ObjectModel;
using System.Diagnostics;
using AAEICS.Shared.Models;
using System.Windows;
using AAEICS.Client.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace AAEICS.Client.Services.LanguageManager;

public class LanguageService: ILanguageService
{
    private readonly ObservableCollection<Language> _languages = new();

    public LanguageService()
    {
        _languages.Add(new Language 
        {
            Name="English",
            Code="en-US",
            Path="Resources/Languages/Lang.en-US.xaml"
        });
        
        _languages.Add(new Language 
        {
            Name="Ukrainian",
            Code="uk-UA",
            Path="Resources/Languages/Lang.uk-UA.xaml"
        });
    }
    
    public IEnumerable<Language> GetAllLanguages()
    {
        return _languages;
    }
    
    public Language GetDefaultLanguage()
    {
        return _languages[0];
    }
    
    public Language? GetLanguageByName(string name)
    {
        foreach (var language in _languages)
        {
            if (string.Equals(language.Name, name, StringComparison.OrdinalIgnoreCase))
            {
                return language;
            }
        }
        Trace.WriteLine($"Theme not found: {name}");
        return null;
    }
    
    public void SetLanguage(Language language)
    {
        if (language == null)
        {
            Trace.WriteLine("Error setting theme: Attempting to set theme to null.");
            return;
        }

        try
        {
            var languageUri = new Uri(language.Path, UriKind.RelativeOrAbsolute);
            Application.Current.Resources.MergedDictionaries[2].Source = languageUri;
            WeakReferenceMessenger.Default.Send(new LanguageChangedMessage(language.Code));
        }
        catch (Exception ex)
        {
            Trace.WriteLine("Error setting language: " + ex.Message);
        }
    }
}