using AAEICS.Services.AppConfiguration;

using AAEICS.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AAEICS.Database;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<AAEICSDbContext>((serviceProvider, options) =>
        {           
            var configService = serviceProvider.GetRequiredService<IAppConfigService>();
            var cn = configService.GetConnectionString();
            options.UseSqlite(cn, sqlite =>
            {               
                sqlite.MigrationsAssembly(typeof(AAEICSDbContext).Assembly.FullName);
            });
        });

        return services;
    }
}