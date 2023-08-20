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
    private List<FormularzKafelekDto> _activeFormTiles;
    private List<FormularzKafelekDto> _deactiveFormTiles;
    private AccountDto _accountDto = new();
    private readonly string _otherRoles = "Admin,Lecturer";
    private readonly string _studentRole = "Student";
    private readonly DateTime? _now = DateTime.Now;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _formTiles = await ApplicationService.GetFormTileDto();
            _activeFormTiles = _formTiles.Where(x => x.DataZamkniecia >= _now).ToList();
            _deactiveFormTiles = _formTiles.Where(x => x.DataZamkniecia < _now).ToList();
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