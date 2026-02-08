using AAEICS.Client.Services.SyncfusionLicenseInitializerService;
using AAEICS.Client.Services.ThemeService;
using Microsoft.Extensions.DependencyInjection;

namespace AAEICS.Client.Services;

public static class ClientServicesExtensions {
    public static IServiceCollection AddClientServices(this IServiceCollection services)
    {
        services.AddSingleton<IThemesService, ThemesService>();
        services.AddSingleton<ISyncfusionLicenseInitializer, SyncfusionLicenseInitializer>();
        return services;
    }
}