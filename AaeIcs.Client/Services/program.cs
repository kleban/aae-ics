using AAEICS.Client.Services.LanguageManager;
using AAEICS.Client.Services.SyncfusionLicenseInitializer;
using AAEICS.Client.Services.ThemeManager;
using AAEICS.Client.Services.NavigationManager;

using Microsoft.Extensions.DependencyInjection;

namespace AAEICS.Client.Services;

public static class ClientServicesExtensions {
    public static IServiceCollection AddClientServices(this IServiceCollection services)
    {
        services.AddSingleton<ISyncfusionLicenseInitializerService, SyncfusionLicenseInitializerService>();
        services.AddSingleton<IThemesService, ThemesService>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<ILanguageService, LanguageService>();
        return services;
    }
}