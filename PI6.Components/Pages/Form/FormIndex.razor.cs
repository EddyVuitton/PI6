using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PI6.Components.Helpers.Interfaces;
using PI6.Shared.Data.Dtos;
using PI6.WebApi.Helpers;
using PI6.WebApi.Services;

namespace PI6.Components.Pages.Form;

public partial class FormIndex
{
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Inject] public IJSRuntime JS { get; set; }
    [Inject] public ISnackbarHelper ErrorHelper { get; set; }

    private List<FormularzKafelekDto> _formTiles;
    private AccountDto _accountDto = new();
    private readonly string _otherRoles = "Admin,Lecturer";
    private readonly string _studentRole = "Student";

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _formTiles = await ApplicationService.GetFormTileDto();
            await LoadAccount();

            StateHasChanged();
        }
        catch (Exception e)
        {
            ErrorHelper.ShowSnackbar(e.Message, MudBlazor.Severity.Error, false, true);
        }
    }

    private async Task LoadAccount()
    {
        var loggedEmail = await JS.GetFromLocalStorage("email");
        _accountDto = await ApplicationService.GetAccountDtoByEmail(loggedEmail);
    }
}