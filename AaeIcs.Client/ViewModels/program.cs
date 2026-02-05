using Microsoft.Extensions.DependencyInjection;

namespace AAEICS.Client.ViewModels;

public static class ViewModelExtensions
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        // ViewModels зазвичай реєструють як Transient (щоразу новий екземпляр)
        // або як Singleton (якщо це головна модель, що живе весь час)
        services.AddTransient<HomePageViewModel>();
       
        return services;
    }
}