using Microsoft.AspNetCore.Components;
using MudBlazor;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Services;

namespace PI6.Components.Shared.Account;

public partial class AccountLogin
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    [Inject] public IApplicationService ApplicationService { get; set; }
    private account _account = new();

    protected override async Task OnInitializedAsync()
    {
    }

    private async Task Login()
    {
    }

    void Cancel() => MudDialog.Cancel();
}