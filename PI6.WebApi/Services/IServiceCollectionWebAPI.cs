using PI6.WebApi.Repositories;

namespace PI6.WebApi.Services;

public static class IServiceCollectionWebAPI
{
    public static IServiceCollection AddWebAPI(this IServiceCollection services)
    {
        services.AddScoped<IApplicationRepository, ApplicationRepository>();

        return services;
    }
}