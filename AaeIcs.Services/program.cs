using AAEICS.Core.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AAEICS.Services;

public static class ServicesExtensions {
    public static IServiceCollection AddServices(this IServiceCollection services) 
    {
// Твої існуючі сервіси
        services.AddSingleton<IAppConfigService, AppConfigService>();
        services.AddSingleton<IInitialFoldersService, InitialFoldersService>();
        
        // Відкриті (Generic) типи реєструються саме через typeof()
        services.AddTransient(typeof(IFuzzySearchService<>), typeof(FuzzySearchService<>));
        
        // Реєстрація специфічних сервісів
        services.AddTransient<IIncomingCertificateService, IncomingCertificateService>();
        
        // Тепер це запрацює, бо DictionaryDataService успадковує IDictionaryDataService!
        services.AddTransient<IDictionaryDataService, DictionaryDataService>();
        
        return services;
    }
}