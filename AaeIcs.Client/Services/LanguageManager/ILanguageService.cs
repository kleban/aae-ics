using System.ComponentModel;

namespace AAEICS.Client.Services.LanguageManager;

public interface ILanguageService: INotifyPropertyChanged
{
    string this[string key] { get; }
    string GetDefaultLanguage();
    void SetLanguage(string cultureCode);
}