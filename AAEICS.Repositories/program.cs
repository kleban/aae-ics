using AAEICS.Core.Contracts.Repositories;
using AAEICS.Core.DTO.General;
using AAEICS.Database.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AAEICS.Repositories;

public static class ServicesExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // 1. Реєструємо AutoMapper з нашим профілем
        services.AddAutoMapper(config => { config.AddProfile<MappingProfile>(); });

        // 2. Реєструємо специфічні репозиторії
        services.AddScoped<IIncomingCertificateRepository, IncomingCertificateRepository>();

        // 3. Реєструємо UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // 4. 🔥 РЕЄСТРУЄМО ДОВІДНИКИ (Щоб DictionaryDataService міг їх знайти!)
        // Кажемо системі: "Коли хтось просить IGenericRepository<RankDTO>, 
        // дай йому DictionaryDataRepository<Rank, RankDTO>"
        services.AddScoped<IGenericRepository<RankDTO>, DictionaryDataRepository<Rank, RankDTO>>();
        services.AddScoped<IGenericRepository<PositionDTO>, DictionaryDataRepository<Position, PositionDTO>>();
        services.AddScoped<IGenericRepository<ReasonDTO>, DictionaryDataRepository<Reason, ReasonDTO>>();
        services
            .AddScoped<IGenericRepository<MeasureUnitDTO>, DictionaryDataRepository<MeasureUnit, MeasureUnitDTO>>();
        services.AddScoped<IGenericRepository<PersonnelDTO>, DictionaryDataRepository<Personnel, PersonnelDTO>>();

        return services;
    }
}
