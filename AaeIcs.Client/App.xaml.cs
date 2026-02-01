using AaeIcs.Client.Services;
using Microsoft.Extensions.Configuration;
using Syncfusion.Licensing;
using System.Configuration;
using System.Data;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace AAEICS.Client;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = default!;

    public App()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IAppConfigService, AppConfigService>();
        services.AddSingleton<ISyncfusionLicenseInitializer, SyncfusionLicenseInitializer>();

        Services = services.BuildServiceProvider();

        Services.GetRequiredService<ISyncfusionLicenseInitializer>().Register();
    }
}