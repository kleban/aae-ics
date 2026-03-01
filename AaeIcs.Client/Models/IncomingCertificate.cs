using CommunityToolkit.Mvvm.ComponentModel;

namespace AAEICS.Client.Models;

public partial class IncomingCertificate: ObservableObject
{
    [ObservableProperty] private int _edrpou;
    
    [ObservableProperty] private DateTime _approveDate;
    
    [ObservableProperty] private DateTime _registrationDate;
    
    [ObservableProperty] private string _registrationPlace;
    
    [ObservableProperty] private DateTime _transferDateStart;
    
    [ObservableProperty] private DateTime _transferDateEnd;
}