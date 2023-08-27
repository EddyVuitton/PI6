using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using PI6.Components.Helpers.Interfaces;
using PI6.Shared.Data.Dtos;
using PI6.WebApi.Services;

namespace PI6.Components.Pages;

public partial class Index
{
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Inject] public IJSRuntime JS { get; set; }
    [Inject] public ISnackbarHelper ErrorHelper { get; set; }
    [Inject] public IAccountHelper AccountHelper { get; set; }
    
    [CascadingParameter] private Task<AuthenticationState> authenticationState { get; set; }

    private AccountDto _account = new();

    protected override async Task OnInitializedAsync()
    {
        if (authenticationState is not null)
        {
            try
            {
                _account = await AccountHelper.LoadAccount(authenticationState, ApplicationService);
                if (_account is not null)
                {
                    NavigationManager.NavigateTo("/home", true);
                }
            }
            catch (Exception e)
            {
                ErrorHelper.ShowSnackbar(e.Message, MudBlazor.Severity.Error, false, true);
            }
        }
    }
}