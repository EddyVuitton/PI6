using Microsoft.AspNetCore.Components.Authorization;
using PI6.Shared.Data.Dtos;
using PI6.WebApi.Services;

namespace PI6.Components.Helpers.Interfaces;

public interface IAccountHelper
{
    Task<AccountDto> LoadAccount(Task<AuthenticationState> authenticationState, IApplicationService applicationService);
}