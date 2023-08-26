using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using PI6.WebApi.Services;
using System.Security.Claims;
using PI6.Shared.Data.Dtos;
using PI6.Components.Helpers.Interfaces;

namespace PI6.Components.Helpers;

public class AccountHelper : IAccountHelper
{
    [Inject] public IApplicationService ApplicationService { get; set; }

    [CascadingParameter] private Task<AuthenticationState> authenticationState { get; set; }

    public async Task<AccountDto> LoadAccount()
    {
        var authState = await authenticationState;
        var user = authState?.User;
        var loggedEmail = user.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        var account = await ApplicationService.GetAccountDtoByEmail(loggedEmail);

        return account;
    }
}