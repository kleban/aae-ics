using AAEICS.Client.Messages;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace AAEICS.Client.Services.LanguageManager;

public class LanguageService: ObservableObject, ILanguageService
{
    private CultureInfo _currentCulture = CultureInfo.CurrentUICulture;
    
    public string this[string key]
    {
        get
        {
            var translation = Resources.Languages.Resources.ResourceManager.GetString(key, _currentCulture);
            
            return translation ?? $"#{key}#"; 
        }
    }
    
    public string GetDefaultLanguage()
    {
        return CultureInfo.CurrentUICulture.Name;
    }
    
    public void SetLanguage(string cultureCode)
    {
        _currentCulture = new CultureInfo(cultureCode);
        
        Thread.CurrentThread.CurrentCulture = _currentCulture;
        Thread.CurrentThread.CurrentUICulture = _currentCulture;
        
        OnPropertyChanged(new PropertyChangedEventArgs(Binding.IndexerName));
        WeakReferenceMessenger.Default.Send(new LanguageChangedMessage(cultureCode));
    }
}