using AAEICS.Client.Services;

using AAEICS.Database.Context;
using AAEICS.GuardFS.Core;
using AAEICS.GuardFS.Checkups.AppConfigCheckup;
using AAEICS.GuardFS.Contracts;

using AAEICS.Services.AppConfig;

using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace AAEICS.Client;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = default!;

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var services = new ServiceCollection();

        ConfigureGuardFs(services);
        ConfigureDatabase(services);
        ConfigureLicense(services);

        Services = services.BuildServiceProvider();

        var guard = Services.GetRequiredService<GuardEngine>();
        try
        {
            await guard.ShieldUpAsync();
            Services.GetRequiredService<ISyncfusionLicenseInitializer>().Register();
            
            using (var scope = Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AAEICSDbContext>();
                await dbContext.Database.MigrateAsync();
            }

            var mainWindow = Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Критична помилка цілісності: {ex.Message}", "GuardFS Block", MessageBoxButton.OK,
                MessageBoxImage.Error);
            Shutdown();
        }
    }

    private void ConfigureGuardFs(IServiceCollection services)
    {
        services.AddSingleton<IAppConfigService, AppConfigService>();

        services.AddSingleton<GuardEngine>();
        
        services.AddSingleton<IEnumerable<ICheckup>>(new List<ICheckup>
        {
            new AppConfigCheckup(
                "App Settings Checkup", 
                "Checks if the appsettings.json file in AppData directory exists and is valid" // Передаємо налаштування напряму
            )
        });
        services.AddSingleton<MainWindow>();
    }

    private void ConfigureDatabase(IServiceCollection services)
    {
        services.AddDbContext<AAEICSDbContext>((serviceProvider, options) =>
        {
            var configService = serviceProvider.GetRequiredService<IAppConfigService>();
            options.UseSqlite(configService.Get("Database"));
        });
    }

    private void ConfigureLicense(IServiceCollection services)
    {
        services.AddSingleton<IAppConfigService, AppConfigService>();
        services.AddSingleton<ISyncfusionLicenseInitializer, SyncfusionLicenseInitializer>();
    }
}