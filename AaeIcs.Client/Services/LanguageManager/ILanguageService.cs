using AAEICS.Shared.Models;

namespace AAEICS.Client.Services.LanguageManager;

public interface ILanguageService
{
    IEnumerable<Language> GetAllLanguages();
    Language GetDefaultLanguage();
    Language? GetLanguageByName(string name);
    void SetLanguage(Language language);
}