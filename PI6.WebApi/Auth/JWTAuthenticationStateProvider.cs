using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using PI6.WebApi.Helpers;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace PI6.WebApi.Auth;

public class JWTAuthenticationStateProvider : AuthenticationStateProvider, ILoginService
{
    //https://www.udemy.com/course/programming-in-blazor-aspnet-core/learn/lecture/17136788#overview
    private readonly IJSRuntime _js;
    private readonly HttpClient _httpClient;
    private const string _TOKENKEY = "TOKENKEY";
    private static AuthenticationState _anonymous => new(new ClaimsPrincipal(new ClaimsIdentity()));

    public JWTAuthenticationStateProvider(IJSRuntime js, HttpClient httpClient)
    {
        _js = js;
        _httpClient = httpClient;
    }

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var token = await _js.GetFromLocalStorage(_TOKENKEY);

            if (string.IsNullOrEmpty(token))
                return _anonymous;

            return BuildAuthenticationState(token);
        }
        catch
        {
            return await Task.FromResult(_anonymous);
        }
    }

    public AuthenticationState BuildAuthenticationState(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

        if (roles != null)
        {
            if (roles.ToString().Trim().StartsWith("["))
            {
                var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                foreach (var parsedRole in parsedRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                }
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
            }

            keyValuePairs.Remove(ClaimTypes.Role);
        }

        claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
        return claims;
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }

    public async Task Login(string token, string email)
    {
        await _js.SetInLocalStorage(_TOKENKEY, token);
        await _js.SetInLocalStorage("email", email);
        var authState = BuildAuthenticationState(token);
        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }

    public async Task Logout()
    {
        await CleanUp();
    }

    private async Task CleanUp()
    {
        await _js.RemoveItemFromLocalStorage(_TOKENKEY);
        await _js.RemoveItemFromLocalStorage("email");
        _httpClient.DefaultRequestHeaders.Authorization = null;
        NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));
    }
}