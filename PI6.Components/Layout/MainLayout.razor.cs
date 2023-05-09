using Microsoft.AspNetCore.Components;
using MudBlazor;
using PI6.Components.Shared.Account;

namespace PI6.Components.Layout;

public partial class MainLayout
{
    [Inject] private NavigationManager _navigationManager { get; set; }
    [Inject] private IDialogService DialogService { get; set; }

    private void NavToIndex()
    {
        _navigationManager.NavigateTo($"/", true);
        StateHasChanged();
    }

    private void OpenDialog()
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
}