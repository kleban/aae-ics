using Microsoft.Extensions.DependencyInjection;

namespace AAEICS.Client.Views;

public static class ViewExtensions
{
    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddTransient<HomePage>();
        services.AddTransient<NewIncomingCertificatePage>();
        services.AddTransient<SettingsPage>();
        
        return services;
    }
}