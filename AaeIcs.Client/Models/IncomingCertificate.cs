using System.ComponentModel.DataAnnotations;
using AAEICS.Client.Attributes;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AAEICS.Client.Models;

public partial class IncomingCertificate: ObservableObject
{
    [ObservableProperty] 
    [property: Display(Name = "CertificateEDRPOU", ResourceType = typeof(Resources.Languages.Resources))] 
    [property: GridColumn("70")]
    private int _edrpou;
    
    [ObservableProperty] 
    [property: Display(Name = "CertificateApprovePerson", ResourceType = typeof(Resources.Languages.Resources))] 
    [property: GridColumn("1.2*")]
    private string _approvePerson;
    
    [ObservableProperty] 
    [property: Display(Name = "CertificateApproveDate", ResourceType = typeof(Resources.Languages.Resources))] 
    [property: GridColumn("90", StringFormat = "MMMM dd yyyy")]
    private DateTime _approveDate;
    
    [ObservableProperty] 
    [property: Display(Name = "CertificateRegistrationDate", ResourceType = typeof(Resources.Languages.Resources))] 
    [property: GridColumn("90", StringFormat = "MMMM dd yyyy")]
    private DateTime _registrationDate;
    
    [ObservableProperty] 
    [property: Display(Name = "CertificateRegistrationPlace", ResourceType = typeof(Resources.Languages.Resources))]
    [property: GridColumn("1.1*")]
    private string _registrationPlace;
    
    [ObservableProperty] 
    [property: Display(Name = "CertificateTransferDateStart", ResourceType = typeof(Resources.Languages.Resources))]
    [property: GridColumn("90", StringFormat = "MMMM dd yyyy")]
    private DateTime _transferDateStart;
    
    [ObservableProperty] 
    [property: Display(Name = "CertificateTransferDateEnd", ResourceType = typeof(Resources.Languages.Resources))]
    [property: GridColumn("90", StringFormat = "MMMM dd yyyy")]
    private DateTime _transferDateEnd;
    
    [ObservableProperty]
    [property: Display(Name = "CertificateDonor", ResourceType = typeof(Resources.Languages.Resources))]
    [property: GridColumn("1.5*")]
    private string _donor;
    
    [ObservableProperty]
    [property: Display(Name = "CertificateRecipient", ResourceType = typeof(Resources.Languages.Resources))]
    [property: GridColumn("1.5*")]
    private string _recipient;
    
    [ObservableProperty]
    [property: Display(Name = "CertificateDeliveryCompany", ResourceType = typeof(Resources.Languages.Resources))]
    [property: GridColumn("1.5*")]
    private string _deliveryCompany;
}