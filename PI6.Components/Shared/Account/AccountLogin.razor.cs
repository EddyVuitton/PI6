using Microsoft.AspNetCore.Components;
using MudBlazor;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Auth;
using PI6.WebApi.Services;

namespace PI6.Components.Shared.Account;

public partial class AccountLogin
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public ILoginService LoginService { get; set; }

    private readonly account _account = new();
    private bool _isLoginFailed = false;

    private async Task Login()
    {
        var userToken = await ApplicationService.Login(_account);
        
        if (!string.IsNullOrWhiteSpace(userToken.Token))
        {
            await LoginService.Login(userToken.Token);
            NavigationManager.NavigateTo("");
        }
        else
            _isLoginFailed = true;
    }

    void Cancel() => MudDialog.Cancel();
}