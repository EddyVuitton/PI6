using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using PI6.Components.Shared.Account;
using PI6.WebApi.Auth;
using PI6.WebApi.Services;

namespace PI6.Components.Shared.Auth;

public partial class LoginLinks
{
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public ILoginService LoginService { get; set; }

    private void OpenLoginDialog()
    {
        var options = new DialogOptions
        {
            CloseOnEscapeKey = true,
            NoHeader = true,
            MaxWidth = MaxWidth.Small,
            FullWidth = true
        };
        DialogService.Show<AccountLogin>(null, options);
    }

    private async Task LogOut()
    {
        await LoginService.Logout();
        NavigationManager.NavigateTo("");
    }
}