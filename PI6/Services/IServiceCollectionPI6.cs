using PI6.WebApi.Services;

namespace PI6.Server.Services;

public static class IServiceCollectionPI6
{
    public static IServiceCollection AddPI6(this IServiceCollection services)
    {
        services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44322/") });
        services.AddScoped<IApplicationService, ApplicationService>(); //webapi

        return services;
    }
}