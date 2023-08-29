using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using PI6.Components.Helpers.Interfaces;
using PI6.Components.Shared.Form.Dialogs;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Services;

namespace PI6.Components.Shared;

public partial class FormTilesIndex
{
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Inject] public ISnackbarHelper ErrorHelper { get; set; }
    [Inject] public IAccountHelper AccountHelper { get; set; }

    [CascadingParameter] private Task<AuthenticationState> authenticationState { get; set; }

    [Parameter] public List<FormularzKafelekDto> FormTiles { get; set; }

    //private List<formularz_podejscie> _solvedForms;
    private List<formularz> _forms;
    private Dictionary<formularz, int> _formsPointsSum;

    private AccountDto _accountDto;
    private bool _isStudent = false;
    private string _href = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        if (authenticationState is not null)
        {
            try
            {
                _accountDto = await AccountHelper.LoadAccount(authenticationState, ApplicationService);
                if (_accountDto is not null)
                {
                    await LoadData();
                    StateHasChanged();
                }
            }
            catch (Exception e)
            {
                ErrorHelper.ShowSnackbar(e.Message, Severity.Error, false, true);
            }
        }
    }

    private void OpenAssignFormDialog(int formId)
    {
        var parameters = new DialogParameters { ["FormId"] = formId };

        var options = new DialogOptions
        {
            CloseOnEscapeKey = true,
            NoHeader = true,
            MaxWidth = MaxWidth.Small,
            FullWidth = true
        };
        DialogService.Show<AssignForm>(null, parameters, options);
        StateHasChanged();
    }

    private void OpenFormDatesDialog(int formId)
    {
        var parameters = new DialogParameters { 
            ["FormId"] = formId
        };

        var options = new DialogOptions
        {
            CloseOnEscapeKey = true,
            NoHeader = true
        };

        DialogService.Show<FormDates>(null, parameters, options);
    }

    private void OpenAskToStartDialog(string href)
    {
        var parameters = new DialogParameters
        {
            { "Href", href }
        };

        var options = new DialogOptions { CloseOnEscapeKey = true };
        DialogService.Show<AskToStartTest>("Czy na pewno chcesz rozpocząć test?", parameters, options);
    }

    private async Task LoadData()
    {
        _forms = new();
        _formsPointsSum = new();

        _href = _accountDto.UstName switch
        {
            "Lecturer" => "formdetails",
            "Student" => "formsolve",
            "Admin" => "formdetails",
            _ => "formdetails"
        };

        _isStudent = _accountDto.UstName == "Student";
        //_solvedForms = await ApplicationService.GetSolvedForms(_accountDto.UserId);
        foreach (var fT in FormTiles)
        {
            var form = await ApplicationService.GetForm(fT.ForId);
            var questions = await ApplicationService.GetFormQuestions(fT.ForId);

            _forms.Add(form);
            _formsPointsSum.Add(form, questions.Sum(x => x.forp_punkty));
        }
    }

    private static string FormatRequiredTime(int time)
    {
        if (time < 10)
            return string.Concat("0", time);
        else
            return time.ToString();
    }
}