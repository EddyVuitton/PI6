using Microsoft.AspNetCore.Components.Authorization;
using PI6.Components.Helpers;
using PI6.Components.Helpers.Interfaces;
using PI6.WebApi.Auth;
using PI6.WebApi.Services;

namespace PI6.Server.Services;

public static class IServiceCollectionPI6
{
    public static IServiceCollection AddPI6(this IServiceCollection services)
    {
        services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44322/") });
        services.AddScoped<IApplicationService, ApplicationService>(); //webapi
        services.AddScoped<ISnackbarHelper, SnackbarHelper>();
        services.AddScoped<IAccountHelper, AccountHelper>();

        services.AddScoped<JWTAuthenticationStateProvider>();
        services.AddScoped<AuthenticationStateProvider, JWTAuthenticationStateProvider>(provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());
        services.AddScoped<ILoginService, JWTAuthenticationStateProvider>(provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());

        return services;
    }
}