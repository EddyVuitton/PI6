using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using PI6.Components.Helpers.Interfaces;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Helpers;
using PI6.WebApi.Services;

namespace PI6.Components.Shared.Form.Dialogs;

public partial class AssignForm
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    [Parameter] public int FormId { get; set; }
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Inject] public IJSRuntime JS { get; set; }
    [Inject] public ISnackbarHelper ErrorHelper { get; set; }

    private AccountDto _accountDto = new();
    private account _account = new();
    private List<student_group> _studentGroups = new();
    private List<group_assigned_forms> _groupAssignedForms = new();
    private List<GroupAssignedFormCheckDto> _groupAssignedFormCheckDtos = new();

    protected override async Task OnAfterRenderAsync(bool isFirstRender)
    {
        var loggedEmail = await JS.GetFromLocalStorage("email");
        if (isFirstRender)
        {
            _accountDto = await ApplicationService.GetAccountDtoByEmail(loggedEmail);
            _account = await ApplicationService.GetAccount(_accountDto.UserId);
            _studentGroups = await ApplicationService.GetStudentGroups(_account.us_id);
            _groupAssignedForms = await ApplicationService.GetGroupAssignedForms(_account.us_id);
            _groupAssignedFormCheckDtos = FormHelper.GetGroupAssignedFormCheckDto(_studentGroups, _groupAssignedForms, _account.us_id, FormId);
        }
        StateHasChanged();
    }

    private void Cancel() => MudDialog.Cancel();

    private async Task Save()
    {
        try
        {
            var responseMessage = await ApplicationService.SaveGroupAssignedForms(_groupAssignedFormCheckDtos) ?? throw new NullReferenceException();

            if (!responseMessage.IsSuccessStatusCode)
                throw new Exception(responseMessage.ReasonPhrase);
                
            ErrorHelper.ShowSnackbar("Poprawnie przypisano test", Severity.Success);
        }
        catch (NullReferenceException)
        {
            ErrorHelper.ShowSnackbar("Błąd przypisania testu", Severity.Warning);
            ErrorHelper.ShowSnackbar("Obiekt nie został poprawnie zainicjonowany", Severity.Error);
        }
        catch (Exception e)
        {
            ErrorHelper.ShowSnackbar("Błąd przypisania testu", Severity.Warning);
            ErrorHelper.ShowSnackbar(e.Message, Severity.Error);
        }

        Cancel();
    }
}