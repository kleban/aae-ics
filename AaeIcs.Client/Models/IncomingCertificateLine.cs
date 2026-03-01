using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AAEICS.Client.ViewModels.Components;
using AAEICS.Core.Contracts.Services;
using AAEICS.Core.DTO.General;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.VisualBasic;


namespace AAEICS.Client.Models;

public partial class IncomingCertificateLine: ObservableObject
{
    [ObservableProperty] private bool _isConfirmed;
    
    [ObservableProperty] [property: Display(Name = "CertificateLineName", ResourceType = typeof(Resources.Languages.Resources))]
    private string _name;

    [ObservableProperty] [property: Display(Name = "CertificateLineNomenclatureCode", ResourceType = typeof(Resources.Languages.Resources))]
    private string? _nomenclatureCode;
    
    [ObservableProperty] [property: Display(Name = "CertificateLineBatchNumber", ResourceType = typeof(Resources.Languages.Resources))]
    private string _batchNumber;

    [ObservableProperty] [property: Display(Name = "CertificateLineMeasureUnit", ResourceType = typeof(Resources.Languages.Resources))]
    private int _measureUnit;

    [ObservableProperty] [property: Display(Name = "CertificateLinePricePerUnit", ResourceType = typeof(Resources.Languages.Resources))]
    private double _pricePerUnit;

    [ObservableProperty]
    private decimal _quantitySent;

    [ObservableProperty]
    private decimal _categorySent;

    [ObservableProperty]
    private decimal _quantityReceived;

    [ObservableProperty]
    private decimal _categoryReceived;

    [ObservableProperty] [property: Display(Name = "CertificateLineNotes", ResourceType = typeof(Resources.Languages.Resources))]
    private string? _notes;

    [ObservableProperty] [property: Display(Name = "CertificateLineMadeIn", ResourceType = typeof(Resources.Languages.Resources))]
    private string? _madeIn;
    
    // ВЛАСНІ "мізки" для кожного ComboBox у цьому рядку
    public SearchBoxViewModel<MeasureUnitDTO> MeasureUnitSearchBox { get; }
    public SearchBoxViewModel<CategoryDTO> CategorySentSearchBox { get; }
    public SearchBoxViewModel<CategoryDTO> CategoryReceivedSearchBox { get; }

    // У конструктор рядка ми передаємо сервіси пошуку з головної ViewModel
    public IncomingCertificateLine(
        IFuzzySearchService<MeasureUnitDTO> measureUnitSearch,
        IFuzzySearchService<CategoryDTO> categorySentSearch,
        IFuzzySearchService<CategoryDTO> categoryReceivedSearch)
    {
        // Створюємо незалежні екземпляри для конкретно цього рядка
        MeasureUnitSearchBox = new SearchBoxViewModel<MeasureUnitDTO>(measureUnitSearch, u => u.Name);
        CategorySentSearchBox = new SearchBoxViewModel<CategoryDTO>(categorySentSearch, c => c.Name);
        CategoryReceivedSearchBox = new SearchBoxViewModel<CategoryDTO>(categoryReceivedSearch, c => c.Name);
    }
}