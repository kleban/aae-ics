using AAEICS.Shared.Dto;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using AAEICS.Services.IncomingCertificates;

namespace AAEICS.Client.ViewModels;

public partial class HomePageViewModel(IIncomingCertificateService incomingCertificateService) : ObservableObject
{
    [ObservableProperty] private ObservableCollection<IncomingCertificateDto> _incomingCertificates = new();

    [RelayCommand]
    private async Task LoadData()
    {
        try{
            var certificates = await incomingCertificateService.GetIncomingCertificates();
            IncomingCertificates.Clear();
            foreach (var cert in certificates)
            {
                IncomingCertificates.Add(cert);
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., log error, show message to user)
            Console.WriteLine($"Error loading data: {ex.Message}");
        }
    }
}