using Microsoft.Extensions.DependencyInjection;

namespace AAEICS.Client.Views;

public static class ViewExtensions
{
    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddTransient<HomePage>();
        services.AddSingleton<SettingsPage>();
        services.AddSingleton<SuccessPage>();
        services.AddSingleton<FailPage>();
        services.AddTransient<IncomingCertificatePage>();
        services.AddTransient<IssuanceCertificatePage>();
        services.AddTransient<WriteOffCertificatePage>();
        
        return services;
    }
}