using AAEICS.Client.Services.SyncfusionLicenseInitializerService;
using AAEICS.Client.Views;
using AAEICS.Client.ViewModels;

using AAEICS.Database;
using AAEICS.Database.Context;
using AAEICS.Services;
using AAEICS.Services.InitialFolders;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using AAEICS.Client.Services;

namespace AAEICS.Client;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = default!;

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var services = new ServiceCollection();
        services.AddServices();
        services.AddClientServices();
        services.AddDatabase();
        services.AddViewModels();
        services.AddViews();
        
        Services = services.BuildServiceProvider();
        
        try
        {
            var initFolders = Services.GetRequiredService<IInitialFoldersService>();
            initFolders.CreateInitialFolders();

            using var scope = Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AAEICSDbContext>();
            await dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Критична помилка цілісності: {ex.Message}", "GuardFS Block", MessageBoxButton.OK,
                MessageBoxImage.Error);
            Shutdown();
        } 

        Services.GetRequiredService<ISyncfusionLicenseInitializer>().Register();

        var mainWindow = Services.GetRequiredService<MainWindow>();
        var homePage = Services.GetRequiredService<HomePage>();
        
        // mainWindow.MainContentFrame.Navigate(homePage);
        mainWindow.Show();
    }
}