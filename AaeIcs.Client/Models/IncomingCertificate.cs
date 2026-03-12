using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AAEICS.Client.Attributes;
using AAEICS.Client.Services.Validation;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AAEICS.Client.Models;

public partial class IncomingCertificate: ObservableValidator
{
    [ObservableProperty] 
    [NotifyDataErrorInfo]
    [CustomValidation(typeof(ValidationService), nameof(ValidationService.ValidateEdrpou))]
    [property: Display(Name = "CertificateEDRPOU", ResourceType = typeof(Resources.Languages.Resources))] 
    [property: GridColumn("70")]
    private string _edrpou;

    [ObservableProperty] 
    [NotifyDataErrorInfo]
    [CustomValidation(typeof(ValidationService), nameof(ValidationService.ValidateNotEmpty))]
    [property: Display(Name = "CertificateApprovePerson", ResourceType = typeof(Resources.Languages.Resources))] 
    [property: GridColumn("1.2*")]
    private string _approvePerson;

    [ObservableProperty] 
    [NotifyDataErrorInfo]
    [CustomValidation(typeof(ValidationService), nameof(ValidationService.ValidateDate))]
    [property: Display(Name = "CertificateApproveDate", ResourceType = typeof(Resources.Languages.Resources))] 
    [property: GridColumn("90", StringFormat = "MMMM dd yyyy")]
    private DateTime _approveDate = DateTime.Now;

    [ObservableProperty] 
    [NotifyDataErrorInfo]
    [CustomValidation(typeof(ValidationService), nameof(ValidationService.ValidateDate))]
    [property: Display(Name = "CertificateRegistrationDate", ResourceType = typeof(Resources.Languages.Resources))] 
    [property: GridColumn("90", StringFormat = "MMMM dd yyyy")]
    private DateTime _registrationDate = DateTime.Now;

    [ObservableProperty] 
    [NotifyDataErrorInfo]
    [CustomValidation(typeof(ValidationService), nameof(ValidationService.ValidateNotEmpty))]
    [property: Display(Name = "CertificateRegistrationPlace", ResourceType = typeof(Resources.Languages.Resources))]
    [property: GridColumn("1.1*")]
    private string _registrationPlace;

    [ObservableProperty] 
    [NotifyDataErrorInfo]
    [CustomValidation(typeof(ValidationService), nameof(ValidationService.ValidateDate))]
    [property: Display(Name = "CertificateTransferDateStart", ResourceType = typeof(Resources.Languages.Resources))]
    [property: GridColumn("90", StringFormat = "MMMM dd yyyy")]
    private DateTime _transferDateStart = DateTime.Now;

    [ObservableProperty] 
    [NotifyDataErrorInfo]
    [CustomValidation(typeof(ValidationService), nameof(ValidationService.ValidateDate))]
    [property: Display(Name = "CertificateTransferDateEnd", ResourceType = typeof(Resources.Languages.Resources))]
    [property: GridColumn("90", StringFormat = "MMMM dd yyyy")]
    private DateTime _transferDateEnd = DateTime.Now;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [CustomValidation(typeof(ValidationService), nameof(ValidationService.ValidateNotEmpty))]
    [property: Display(Name = "CertificateDonor", ResourceType = typeof(Resources.Languages.Resources))]
    [property: GridColumn("1.5*")]
    private string _donor;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [CustomValidation(typeof(ValidationService), nameof(ValidationService.ValidateNotEmpty))]
    [property: Display(Name = "CertificateRecipient", ResourceType = typeof(Resources.Languages.Resources))]
    [property: GridColumn("1.5*")]
    private string _recipient;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [CustomValidation(typeof(ValidationService), nameof(ValidationService.ValidateNotEmpty))]
    [property: Display(Name = "CertificateDeliveryCompany", ResourceType = typeof(Resources.Languages.Resources))]
    [property: GridColumn("1.5*")]
    private string _deliveryCompany;
    
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        var dateProperties = new[] { "RegistrationDate", "TransferDateStart", "TransferDateEnd" };

        if (dateProperties.Contains(e.PropertyName))
        {
            var prop = GetType().GetProperty(e.PropertyName);
            var value = (DateTime)prop.GetValue(this);
            prop.SetValue(this, value.Date);
        }
    }
}