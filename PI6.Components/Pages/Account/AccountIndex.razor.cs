using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using PI6.Components.Helpers.Interfaces;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Helpers;
using PI6.WebApi.Services;

namespace PI6.Components.Pages.Account;

public partial class AccountIndex
{
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Inject] public IJSRuntime JS { get; set; }
    [Inject] public ISnackbarHelper ErrorHelper { get; set; }

    private AccountDto _accountDto = new();
    private account _account = new();
    private List<student_group> _studentGroups = new();
    private List<StudentGroupMapDto> _studentGroupMapDtos = new();
    private List<FormularzKafelekDto> _formTiles = new();
    private readonly List<formularz_podejscie> _approaches = new();
    private List<formularz> _forms = new();
    private List<FormResultDto> _formResultsDto = new();

    protected override async Task OnAfterRenderAsync(bool isFirstRender)
    {
        if (isFirstRender)
        {
            var loggedEmail = await JS.GetFromLocalStorage("email");
            _accountDto = await ApplicationService.GetAccountDtoByEmail(loggedEmail);
            _account = await ApplicationService.GetAccount(_accountDto.UserId);

            _formTiles = await ApplicationService.GetFormTileDto();

            foreach (var tile in _formTiles)
            {
                var tempApproach = await ApplicationService.GetFormApproaches(tile.ForId);
                foreach (var entry in tempApproach)
                    _approaches.Add(entry);
            }
            _studentGroups = await ApplicationService.GetStudentGroups(_account.us_id);
            _studentGroupMapDtos = await ApplicationService.GetStudentGroupMapDto(_account.us_id);
            _forms = await ApplicationService.GetAccountForms(_account.us_id);

            await LoadFormResultDto();
            StateHasChanged();
        }
    }

    private async Task LoadFormResultDto()
    {
        foreach (var i in _forms)
        {
            try
            {
                _formResultsDto = await ApplicationService.GetFormResultDto(i.for_id);
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowSnackbar($"Błąd przy wczytaniu wyników testu: {i.for_nazwa}", Severity.Warning);
                ErrorHelper.ShowSnackbar(ex.Message, Severity.Error);
            }
        }
    }
}