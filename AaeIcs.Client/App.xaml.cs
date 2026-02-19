using AAEICS.Client.Services;
using AAEICS.Client.Services.SyncfusionLicenseInitializer;
using AAEICS.Client.Views;
using AAEICS.Client.ViewModels;

using AAEICS.Database;
using AAEICS.Database.Context;
using AAEICS.Services;
using AAEICS.Services.InitialFolders;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using SplashScreen = AAEICS.Client.Views.SplashScreen;

namespace AAEICS.Client;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = default!;

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var splashScreen = new SplashScreen();
        splashScreen.Show();
        var services = new ServiceCollection();
        services.AddServices();
        splashScreen.ProgressBarStatus.Value += 10;
        services.AddClientServices();
        splashScreen.ProgressBarStatus.Value += 10;
        services.AddDatabase();
        splashScreen.ProgressBarStatus.Value += 10;
        services.AddViewModels();
        splashScreen.ProgressBarStatus.Value += 10;
        services.AddViews();
        splashScreen.ProgressBarStatus.Value += 10;
        
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
        splashScreen.ProgressBarStatus.Value += 20;

        Services.GetRequiredService<ISyncfusionLicenseInitializerService>().Register();

        var mainWindow = Services.GetRequiredService<MainWindow>();
        var homePage = Services.GetRequiredService<HomePage>();
        splashScreen.ProgressBarStatus.Value += 20;
        mainWindow.MainFrame.Navigate(homePage);
        mainWindow.Show();
        splashScreen.ProgressBarStatus.Value += 10;
        splashScreen.Close();
    }
}