using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using PI6.Components.Helpers.Interfaces;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Services;

namespace PI6.Components.Pages.Form;

public partial class FormIndex
{
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Inject] public IJSRuntime JS { get; set; }
    [Inject] public ISnackbarHelper ErrorHelper { get; set; }
    [Inject] public IAccountHelper AccountHelper { get; set; }

    [CascadingParameter] private Task<AuthenticationState> authenticationState { get; set; }

    private List<FormularzKafelekDto> _formTiles;
    private List<FormularzKafelekDto> _activeFormTiles;
    private List<FormularzKafelekDto> _deactiveFormTiles;
    private List<formularz_podejscie> _solvedForms;
    private List<formularz_podejscie_odpowiedz> _solvedFormsAnswers;
    private readonly Dictionary<formularz, int> _formsPointsSum = new();
    private readonly List<formularz> _forms = new();
    private readonly List<formularz_pytanie> _formsQuestions = new();
    private readonly List<formularz_pytanie_opcja> _questionsCorrectOptions = new();
    private readonly Dictionary<formularz, int> _formsUserPointsSum = new();
    private AccountDto _accountDto = new();
    private readonly string _otherRoles = "Admin,Lecturer";
    private readonly string _studentRole = "Student";
    private readonly DateTime? _now = DateTime.Now;

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
                ErrorHelper.ShowSnackbar(e.Message, MudBlazor.Severity.Error, false, true);
            }
        }      
    }

    private async Task LoadData()
    {
        _formTiles = await ApplicationService.GetFormTileDto();
        _activeFormTiles = _formTiles.Where(x => x.DataZamkniecia >= _now).ToList();
        _deactiveFormTiles = _formTiles.Where(x => x.DataZamkniecia < _now).ToList();
        _solvedForms = await ApplicationService.GetSolvedForms(_accountDto.UserId);
        _solvedFormsAnswers = new();

        foreach (var sF in _solvedForms)
        {
            var answers = await ApplicationService.GetSolvedFormsAnswers(sF.fpod_id);
            foreach (var ans in answers)
                _solvedFormsAnswers.Add(ans);
        }

        foreach (var sf in _solvedForms)
            _forms.Add(await ApplicationService.GetForm(sf.fpod_for_id));

        foreach (var f in _forms)
        {
            var questions = await ApplicationService.GetFormQuestions(f.for_id);
            foreach (var question in questions)
                _formsQuestions.Add(question);

            var options = await ApplicationService.GetFormOptions(f.for_id);
            foreach (var option in options.Where(x => x.fpop_czy_poprawna))
                _questionsCorrectOptions.Add(option);

            _formsPointsSum.Add(f, questions.Sum(x => x.forp_punkty));

            var userCorrectAnswers =
                (from sfa in _solvedFormsAnswers
                 join co in _questionsCorrectOptions on sfa.fodp_wybrana_odp equals co.fpop_id
                 select co).ToList();

            var userFormSumPoints =
                (from q in questions
                 join uca in userCorrectAnswers on q.forp_id equals uca.fpop_forp_id
                 select q).Sum(x => x.forp_punkty);

            _formsUserPointsSum.Add(f, userFormSumPoints);
        }
               
        DeleteActiveForms();
    }

    private void DeleteActiveForms()
    {
        var formToDelete =
            (from aft in _activeFormTiles
             join sf in _solvedForms on aft.ForId equals sf.fpod_for_id
             select aft).ToList();

        for (int i = 0; i < formToDelete.Count; i++)
        {
            _activeFormTiles.Remove(formToDelete[i]);
        }
    }
}