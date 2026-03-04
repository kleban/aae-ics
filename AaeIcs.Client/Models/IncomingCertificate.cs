using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AAEICS.Client.Models;

public partial class IncomingCertificate: ObservableObject
{
    [ObservableProperty] // [property: Display(Name = "CertificateEDRPOU", ResourceType = typeof(Resources.Languages.Resources))]
    private int _edrpou;
    
    [ObservableProperty] // [property: Display(Name = "CertificateApprovePerson", ResourceType = typeof(Resources.Languages.Resources))]
    private string _approvePerson;
    
    [ObservableProperty] // [property: Display(Name = "CertificateApproveDate", ResourceType = typeof(Resources.Languages.Resources))]
    private DateTime _approveDate;
    
    [ObservableProperty] // [property: Display(Name = "CertificateRegistrationDate", ResourceType = typeof(Resources.Languages.Resources))]
    private DateTime _registrationDate;
    
    [ObservableProperty] // [property: Display(Name = "CertificateRegistrationPlace", ResourceType = typeof(Resources.Languages.Resources))]
    private string _registrationPlace;
    
    [ObservableProperty] // [property: Display(Name = "CertificateTransferDateStart", ResourceType = typeof(Resources.Languages.Resources))]
    private DateTime _transferDateStart;
    
    [ObservableProperty] // [property: Display(Name = "CertificateTransferDateEnd", ResourceType = typeof(Resources.Languages.Resources))]
    private DateTime _transferDateEnd;
    
    [ObservableProperty] // [property: Display(Name = "CertificateDonor", ResourceType = typeof(Resources.Languages.Resources))]
    private string _donor;
    
    [ObservableProperty] // [property: Display(Name = "CertificateRecipient", ResourceType = typeof(Resources.Languages.Resources))]
    private string _recipient;
    
    [ObservableProperty] // [property: Display(Name = "CertificateDeliveryCompany", ResourceType = typeof(Resources.Languages.Resources))]
    private string _deliveryCompany;
}