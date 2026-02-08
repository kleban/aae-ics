using AAEICS.Services.AppConfiguration;
using AAEICS.Services.InitialFolders;
using AAEICS.Services.IncomingCertificates;
using Microsoft.Extensions.DependencyInjection;

namespace AAEICS.Services;

public static class ServicesExtensions {
    public static IServiceCollection AddServices(this IServiceCollection services) 
    {
        services.AddSingleton<IAppConfigService, AppConfigService>();
        services.AddSingleton<IInitialFoldersService, InitialFoldersService>();
        services.AddScoped<IIncomingCertificateService, IncomingCertificateService>();
        return services;
    }
}