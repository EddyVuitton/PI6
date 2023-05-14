using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Helpers;
using PI6.WebApi.Services;

namespace PI6.Components.Pages.Account;

public partial class AccountIndex
{
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Inject] public IJSRuntime JS { get; set; }

    private AccountDto _accountDto = new();
    private account _account = new();
    private string _deactivateDate = string.Empty;
    private List<student_group> _studentGroups = new();
    private List<StudentGroupMapDto> _studentGroupMapDtos = new();
    private List<FormularzKafelekDto> _formTiles = new();
    private List<formularz_podejscie> _approaches = new();
    private List<formularz> _forms = new();
    private readonly TableGroupDefinition<StudentGroupMapDto> _groupDefinition = new();

    protected override async Task OnInitializedAsync()
    {
        InitGroupDefinition(_groupDefinition);
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
    }

    protected override async Task OnAfterRenderAsync(bool isFirstRender)
    {
        var loggedEmail = await JS.GetFromLocalStorage("email");
        if (isFirstRender)
        {
            _accountDto = await ApplicationService.GetAccountDtoByEmail(loggedEmail);
            _account = await ApplicationService.GetAccount(_accountDto.UserId);
            _deactivateDate = _account.us_deactivate == null ? "Nie określono" : _account.us_deactivate.Value.ToShortDateString();
        }
        StateHasChanged();
    }

    private static void InitGroupDefinition (TableGroupDefinition<StudentGroupMapDto> def)
    {
        def.GroupName = "Grupa";
        def.Indentation = false;
        def.Expandable = false;
        def.Selector = (e) => e.SgrName;
    }
}