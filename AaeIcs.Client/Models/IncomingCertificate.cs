using CommunityToolkit.Mvvm.ComponentModel;

namespace AAEICS.Client.Models;

public partial class IncomingCertificate: ObservableObject
{
    [ObservableProperty] private int _edrpou;
    
    // Зберігаємо ID особи, що затвердила (для ComboBox SelectedValue)
    [ObservableProperty] private int _approvePersonId; 

    [ObservableProperty] private DateTime? _approveDate;
    [ObservableProperty] private DateTime _registrationDate;
    [ObservableProperty] private string _registrationPlace;
    [ObservableProperty] private DateTime _transferDateStart;
    [ObservableProperty] private DateTime _transferDateEnd;

    // Зберігаємо ID донора, отримувача тощо
    [ObservableProperty] private int _donorId;
    [ObservableProperty] private int _recipientId;
    [ObservableProperty] private int _deliveryCompanyId;
    [ObservableProperty] private int _reasonId;
}