using Microsoft.Extensions.DependencyInjection;

namespace AAEICS.Client.ViewModels;

public static class ViewModelExtensions
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddTransient<HomePageViewModel>();
        
        return services;
    }
}