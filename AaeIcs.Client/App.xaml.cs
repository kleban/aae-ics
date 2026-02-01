using System.Configuration;
using System.Data;
using System.Windows;
using Syncfusion.Licensing;

namespace AAEICS.Client;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        SyncfusionLicenseProvider.RegisterLicense("LALALALALAL");
    }
}