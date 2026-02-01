using Microsoft.Extensions.Configuration;
using Syncfusion.Licensing;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace AAEICS.Client;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true)
        .Build();

        string? licenseKey = config["Syncfusion:LicenseKey"];
        if (!string.IsNullOrEmpty(licenseKey))
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(licenseKey);
        }
    }
}