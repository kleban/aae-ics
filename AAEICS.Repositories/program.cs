using AAEICS.Core.Contracts.Repositories;
using AAEICS.Repositories.IncomingCertificates;

using Microsoft.Extensions.DependencyInjection;

namespace AAEICS.Repositories;

public static class ServicesExtensions {
    public static IServiceCollection AddRepositories(this IServiceCollection services) 
    {
        services.AddTransient<IIncomingCertificateRepository, IncomingCertificateRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
}