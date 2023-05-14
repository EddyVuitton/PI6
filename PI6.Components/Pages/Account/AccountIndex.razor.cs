using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Helpers;
using PI6.WebApi.Services;
using static MudBlazor.CategoryTypes;

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

    private readonly TableGroupDefinition<StudentGroupMapDto> _groupDefinition = new()
    {
        GroupName = "Grupa",
        Indentation = false,
        Expandable = false,
        Selector = (e) => e.SgrName
    };

    protected override async Task OnAfterRenderAsync(bool isFirstRender)
    {
        var loggedEmail = await JS.GetFromLocalStorage("email");
        if (isFirstRender)
        {
            _accountDto = await ApplicationService.GetAccountDtoByEmail(loggedEmail);
            _account = await ApplicationService.GetAccount(_accountDto.UserId);
            _deactivateDate = _account.us_deactivate == null ? "Nie określono" : _account.us_deactivate.Value.ToShortDateString();
            _studentGroups = await ApplicationService.GetStudentGroups(_account.us_id);
            _studentGroupMapDtos = await ApplicationService.GetStudentGroupMapDto(_account.us_id);
        }
        StateHasChanged();
    }
}