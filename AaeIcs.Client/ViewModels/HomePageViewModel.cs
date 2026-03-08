using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.ObjectModel;
using System.Windows;
using AAEICS.Client.Models;
using AAEICS.Core.Contracts.Services;

namespace AAEICS.Client.ViewModels;

public partial class HomePageViewModel : ObservableObject
{
    private readonly IIncomingCertificateService _incomingCertificateService;
    private readonly IIssuanceCertificateService _issuanceCertificateService;

    [ObservableProperty]
    private ObservableCollection<DashboardCard> _dashboardItems = new();
    
    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private ObservableCollection<IncomingCertificate> _incomingCertificates = new();
    
    [ObservableProperty]
    private ObservableCollection<IssuanceCertificate> _issuanceCertificates = new();
    
    private bool _isSyncing;
    
    [ObservableProperty]
    private bool _isAllConfirmed;
    
    public HomePageViewModel(
        IIncomingCertificateService incomingCertificateService,
        IIssuanceCertificateService issuanceCertificateService)
    {
        _incomingCertificateService = incomingCertificateService;
        _issuanceCertificateService = issuanceCertificateService;

        DashboardItems =
        [
            new DashboardCard(
                titleKey: nameof(Resources.Languages.Resources.CreatedIncomingCertificatesTodayDashboardTitle),
                descriptionKey: nameof(Resources.Languages.Resources.CreatedIncomingCertificatesTodayDashboardDescription),
                icon: Application.Current.Resources["Icon.Warehouse"],
                initialValue: 0
            ),

            new DashboardCard(
                titleKey: nameof(Resources.Languages.Resources.CreatedIssuanceCertificatesTodayDashboardTitle),
                descriptionKey: nameof(Resources.Languages.Resources.CreatedIssuanceCertificatesTodayDashboardDescription),
                icon: Application.Current.Resources["Icon.Stocks"],
                initialValue: 0
            ),
        ];
        
        _ = LoadDataAsync();
    }
    
    public void UpdateDashboardData(DashboardCard dashboardCard, int newValue)
    {
        dashboardCard.CardValue = newValue;
    }
    
    private async Task LoadDataAsync()
    {
        IsBusy = true;
        try
        {
            var incomingCertificates = await _incomingCertificateService.GetIncomingCertificates();
            var issuanceCertificates = await _issuanceCertificateService.GetIssuanceCertificates();

            IncomingCertificates.Clear();
            IssuanceCertificates.Clear();
            IncomingCertificates = new ObservableCollection<IncomingCertificate>(
                incomingCertificates.Select(item => new IncomingCertificate
                {
                    Edrpou = item.Edrpou,
                    ApprovePerson = item.ApprovePerson.LastName,
                    ApproveDate = item.ApproveDate,
                    RegistrationDate = item.RegistrationDate,
                    RegistrationPlace = item.RegistrationPlace,
                    TransferDateStart = item.TransferDateStart,
                    TransferDateEnd = item.TransferDateEnd,
                    Donor = item.Donor.Name,
                    Recipient = item.Recipient.Name,
                    DeliveryCompany = item.DeliveryCompany.Name,
                })
            );

            IssuanceCertificates = new ObservableCollection<IssuanceCertificate>(
                issuanceCertificates.Select(item => new IssuanceCertificate
                {
                    Edrpou = item.Edrpou,
                    ApprovePerson = item.ApprovePerson.LastName,
                    ApproveDate = item.ApproveDate,
                    RegistrationDate = item.RegistrationDate,
                    RegistrationPlace = item.RegistrationPlace,
                    TransferDateStart = item.TransferDateStart,
                    TransferDateEnd = item.TransferDateEnd,
                    Donor = item.Donor.Name,
                    Recipient = item.Recipient.Name,
                    DeliveryCompany = item.DeliveryCompany.Name,
                })
            );

        UpdateDashboardData(DashboardItems[0], IncomingCertificates.Count);
        UpdateDashboardData(DashboardItems[1], IssuanceCertificates.Count);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Помилка завантаження даних: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }
}