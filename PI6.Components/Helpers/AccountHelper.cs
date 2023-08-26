using Microsoft.AspNetCore.Components.Authorization;
using PI6.WebApi.Services;
using System.Security.Claims;
using PI6.Shared.Data.Dtos;
using PI6.Components.Helpers.Interfaces;

namespace PI6.Components.Helpers;

public class AccountHelper : IAccountHelper
{
    public async Task<AccountDto> LoadAccount(Task<AuthenticationState> authenticationState, IApplicationService applicationService)
    {
        var authState = await authenticationState;
        var user = authState?.User;
        var loggedEmail = user.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        var account = await applicationService.GetAccountDtoByEmail(loggedEmail);

        return account;
    }
}