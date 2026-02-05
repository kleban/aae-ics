using AAEICS.Services.AppConfiguration;
using AAEICS.Services.InitialFolders;
using AAEICS.Services.IncomingCertificates.Contracts;
using AAEICS.Services.IncomingCertificates.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace AAEICS.Services;

public static class ServicesExtensions {
    public static IServiceCollection AddServices(this IServiceCollection services) {
        services.AddScoped<IAppConfigService, AppConfigService>();
        services.AddScoped<IInitialFoldersService, InitialFoldersService>();
        services.AddScoped<IIncomingCertificateService, IncomingCertificateService>();
        return services;
    }
}