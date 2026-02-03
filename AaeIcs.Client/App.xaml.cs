using AAEICS.Client.Services;
using AAEICS.Database.Context;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using AAEICS.Services.AppConfiguration;
using AAEICS.Services.InitialCheckupService.Contracts;
using AAEICS.Services.InitialCheckupService.Core;
using AAEICS.Services.InitialCheckupService.Checkups.AppConfigCheckup;
using System.IO;
using AAEICS.Services.InitialFolders;

namespace AAEICS.Client;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = default!;

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var services = new ServiceCollection();
        services.AddSingleton<IAppConfigService, AppConfigService>();
        services.AddSingleton<IInitialFoldersService, InitialFoldersService>();

        ConfigureGuardFs(services, AppConfigService.ConfigFileName);
        ConfigureDatabase(services);
        ConfigureLicense(services);

        services.AddSingleton<MainWindow>();

        Services = services.BuildServiceProvider();
        
        var initFolders = Services.GetRequiredService<IInitialFoldersService>();
        initFolders.CreateInitialFolders();

       var guard = Services.GetRequiredService<GuardEngine>();
        try
        {
             await guard.ShieldUpAsync();

            using (var scope = Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AAEICSDbContext>();
                await dbContext.Database.MigrateAsync();
            }            
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Критична помилка цілісності: {ex.Message}", "GuardFS Block", MessageBoxButton.OK,
                MessageBoxImage.Error);
            Shutdown();
        } 

        Services.GetRequiredService<ISyncfusionLicenseInitializer>().Register();

        var mainWindow = Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void ConfigureGuardFs(IServiceCollection services, string appsettings_path)
    {        

        services.AddSingleton<GuardEngine>();        
        services.AddSingleton<IEnumerable<ICheckup>>(new List<ICheckup>
        {
            new AppConfigCheckup(
                "App Settings Checkup", 
                "Checks if the appsettings.json file in AppData directory exists and is valid", // Передаємо налаштування напряму
                appsettings_path
            )
        });
        
    }

    private void ConfigureDatabase(IServiceCollection services)
    {     
        services.AddDbContext<AAEICSDbContext>((serviceProvider, options) =>
        {           
            var configService = serviceProvider.GetRequiredService<IAppConfigService>();
            var cn = configService.GetConnectionString();
            options.UseSqlite(cn, sqlite =>
            {               
                sqlite.MigrationsAssembly(typeof(AAEICSDbContext).Assembly.FullName);
            });
        });
    }

    private void ConfigureLicense(IServiceCollection services)
    {     
        services.AddSingleton<ISyncfusionLicenseInitializer, SyncfusionLicenseInitializer>();
    }
}