using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;


namespace AAEICS.Client.Models;

public partial class IncomingCertificateLine: ObservableObject
{
    [ObservableProperty] private bool _isConfirmed;
    
    [ObservableProperty] [property: DisplayName("Name")]
    private string _name;

    [ObservableProperty] [property: DisplayName("Nomenclature Code")]
    private string? _nomenclatureCode;
    
    [ObservableProperty] [property: DisplayName("Batch Number")]
    private string _batchNumber;

    [ObservableProperty] [property: DisplayName("Measure Unit")]
    private int _measureUnit;

    [ObservableProperty] [property: DisplayName("Price Per Unit")]
    private double _pricePerUnit;

    [ObservableProperty] [property: DisplayName("Quantity Sent")]
    private decimal _quantitySent;

    [ObservableProperty] [property: DisplayName("Category Sent")]
    private decimal _categorySent;

    [ObservableProperty] [property: DisplayName("Quantity Received")]
    private decimal _quantityReceived;

    [ObservableProperty] [property: DisplayName("Category Received")]
    private decimal _categoryReceived;

    [ObservableProperty] [property: DisplayName("Notes")]
    private string? _notes;

    [ObservableProperty] [property: DisplayName("Made In")]
    private string? _madeIn;
}