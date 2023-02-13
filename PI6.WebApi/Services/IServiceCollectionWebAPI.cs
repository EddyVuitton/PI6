using PI6.WebApi.Data.Implementations;
using PI6.WebApi.Data.Interfaces;

namespace PI6.WebApi.Services;

public static class IServiceCollectionWebAPI
{
    public static IServiceCollection AddWebAPI(this IServiceCollection services)
    {
        services.AddScoped<IFormularzTypRepository, FormularzTypRepository>();
        services.AddScoped<IApplicationRepository, ApplicationRepository>();

        return services;
    }
}