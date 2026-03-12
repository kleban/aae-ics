using System.ComponentModel.DataAnnotations;
using AAEICS.Client.Services.Validation; // Не забудь додати цей using для ValidationService
using AAEICS.Client.ViewModels.Components;
using AAEICS.Core.Contracts.Services;
using AAEICS.Core.DTO.General;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AAEICS.Client.Models;

// 1. ЗМІНИЛИ ObservableObject НА ObservableValidator
public partial class IncomingCertificateLine : ObservableValidator 
{
    [ObservableProperty] 
    private bool _isConfirmed;
    
    [ObservableProperty] 
    [NotifyDataErrorInfo]
    [CustomValidation(typeof(ValidationService), nameof(ValidationService.ValidateNotEmpty))] // <--- Додали перевірку
    [property: Display(Name = "CertificateLineName", ResourceType = typeof(Resources.Languages.Resources))]
    private string _name = string.Empty;

    [ObservableProperty] 
    [property: Display(Name = "CertificateLineNomenclatureCode", ResourceType = typeof(Resources.Languages.Resources))]
    private string? _nomenclatureCode;
    
    [ObservableProperty] 
    [NotifyDataErrorInfo]
    [CustomValidation(typeof(ValidationService), nameof(ValidationService.ValidateNotEmpty))] // <--- Додали перевірку
    [property: Display(Name = "CertificateLineBatchNumber", ResourceType = typeof(Resources.Languages.Resources))]
    private string _batchNumber = string.Empty;

    [ObservableProperty] 
    [property: Display(Name = "CertificateLineMeasureUnit", ResourceType = typeof(Resources.Languages.Resources))]
    private int _measureUnit;

    [ObservableProperty] 
    [NotifyDataErrorInfo]
    [CustomValidation(typeof(ValidationService), nameof(ValidationService.ValidatePositiveDouble))] // <--- Додали перевірку
    [property: Display(Name = "CertificateLinePricePerUnit", ResourceType = typeof(Resources.Languages.Resources))]
    private double _pricePerUnit;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [CustomValidation(typeof(ValidationService), nameof(ValidationService.ValidatePositiveDecimal))] // <--- Додали перевірку
    private decimal _quantitySent;

    [ObservableProperty]
    private decimal _categorySent;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [CustomValidation(typeof(ValidationService), nameof(ValidationService.ValidatePositiveDecimal))] // <--- Додали перевірку
    private decimal _quantityReceived;

    [ObservableProperty]
    private decimal _categoryReceived;

    [ObservableProperty] 
    [property: Display(Name = "CertificateLineNotes", ResourceType = typeof(Resources.Languages.Resources))]
    private string? _notes;

    [ObservableProperty] 
    [property: Display(Name = "CertificateLineMadeIn", ResourceType = typeof(Resources.Languages.Resources))]
    private string? _madeIn;
    
    public SearchBoxViewModel<MeasureUnitDTO> MeasureUnitSearchBox { get; }
    public SearchBoxViewModel<CategoryDTO> CategorySentSearchBox { get; }
    public SearchBoxViewModel<CategoryDTO> CategoryReceivedSearchBox { get; }

    public IncomingCertificateLine(
        IFuzzySearchService<MeasureUnitDTO> measureUnitSearch,
        IFuzzySearchService<CategoryDTO> categorySentSearch,
        IFuzzySearchService<CategoryDTO> categoryReceivedSearch)
    {
        MeasureUnitSearchBox = new SearchBoxViewModel<MeasureUnitDTO>(measureUnitSearch, u => u.Name);
        CategorySentSearchBox = new SearchBoxViewModel<CategoryDTO>(categorySentSearch, c => c.Name);
        CategoryReceivedSearchBox = new SearchBoxViewModel<CategoryDTO>(categoryReceivedSearch, c => c.Name);
    }

    // 2. ДОДАЛИ МЕТОД ДЛЯ ПРИМУСОВОЇ ПЕРЕВІРКИ РЯДКА
    public bool ValidateRow()
    {
        ValidateAllProperties();
        return !HasErrors;
    }
}