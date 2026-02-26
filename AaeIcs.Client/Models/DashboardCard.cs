using AAEICS.Client.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace AAEICS.Client.Models;

public partial class DashboardCard : ObservableObject
{
    private readonly string _titleKey;
    private readonly string _descriptionKey;
    
    [ObservableProperty] private string _cardTitle;
    [ObservableProperty] private string _cardDescription;
    [ObservableProperty] private string _cardValue;
    public object CardIcon { get; }

    public DashboardCard(string titleKey, string descriptionKey, object icon, string initialValue)
    {
        _titleKey = titleKey;
        _descriptionKey = descriptionKey;
        CardIcon = icon;
        CardValue = initialValue;
        
        UpdateTranslations();
        
        WeakReferenceMessenger.Default.Register<LanguageChangedMessage>(this, (r, m) => UpdateTranslations());
    }

    private void UpdateTranslations()
    {
        CardTitle = Resources.Languages.Resources.ResourceManager.GetString(_titleKey) ?? _titleKey;
        CardDescription = Resources.Languages.Resources.ResourceManager.GetString(_descriptionKey) ?? _descriptionKey;
    }
}
