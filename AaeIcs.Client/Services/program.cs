using AAEICS.Client.Services.Language;
using AAEICS.Client.Services.Navigation;
using AAEICS.Client.Services.SyncfusionLicenseInitializer;
using AAEICS.Client.Services.Theme;
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