using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Helpers;
using PI6.WebApi.Services;

namespace PI6.Components.Shared.Form.Dialogs;

public partial class FormDates
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Inject] public IJSRuntime JS { get; set; }

    private AccountDto _accountDto = new();
    private account _account = new();

    protected override async Task OnAfterRenderAsync(bool isFirstRender)
    {
        var loggedEmail = await JS.GetFromLocalStorage("email");
        if (isFirstRender)
        {
            _accountDto = await ApplicationService.GetAccountDtoByEmail(loggedEmail);
            _account = await ApplicationService.GetAccount(_accountDto.UserId);
        }
        StateHasChanged();
    }

    void Cancel() => MudDialog.Cancel();
}