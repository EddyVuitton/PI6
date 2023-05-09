using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using PI6.Components.Objects;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Helpers;
using PI6.WebApi.Services;

namespace PI6.Components.Pages.Form;

public partial class FormSolve
{
    [Inject] public IJSRuntime JSRuntime { get; set; }
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Parameter] public int FormId { get; set; }
    [Parameter] public AppState AppState { get; set; } = new();

    private FormularzPodejscieDto _solvedForm = new();
    private FormularzDto _formDto = new();
    private List<PytanieDto> _formQuestionsDto = new();
    private formularz _form = new();
    private List<formularz_pytanie> _questions = new();
    private List<formularz_pytanie_opcja> _options = new();
    private List<formularz_odpowiedz> _answers = new();
    private IMask _pointsPatternMask = new PatternMask("00");
    private string _title = string.Empty;
    private int _requiredTime;
    private DateTime _startDateTime = DateTime.Now;
    private DateTime _finishDateTime;

    private async Task ConsoleLog(string message)
    {
        await JSRuntime.InvokeVoidAsync("console.log", message);
    }

    protected override async Task OnInitializedAsync()
    {
        AppState.LoadStateChanged += OnStateChanged;

        _questions = await ApplicationService.GetFormQuestions(FormId);
        _options = await ApplicationService.GetFormOptions(FormId);
        _form = (await ApplicationService.GetForm(FormId)).FirstOrDefault();

        _formQuestionsDto = FormHelper.GetFormQuestionsDto(_questions, _options);
        _formDto = FormHelper.GetFormularzDto(_form, _formQuestionsDto);
        
        _title = _formDto.Nazwa;
        _requiredTime = _formDto.LimitCzasu ?? 0;
        AppState.RequiredTime = _requiredTime;

        UpdateOptionsOnInitial();

        StateHasChanged();
    }
    
    private void OnStateChanged() => this.InvokeAsync(StateHasChanged);

    private List<formularz_pytanie_opcja> GetQuestionOptions(int questionId) => _options.Where(x => x.fpop_forp_id == questionId).ToList();

    private int GetTotalPoints() => _questions.Sum(x => x.forp_punkty);

    private void UpdateOptionsOnInitial()
    {
        //First update to false for all options
        foreach (var o in _options)
            o.fpop_czy_poprawna = false;

        //Then update first options of required questions to true
        //UpdateFirstOptions();
    }

    private void UpdateOtherOptionsOnChange(formularz_pytanie_opcja option, bool isMultiAvailable)
    {
        AnswerOnChange(option);

        if (isMultiAvailable)
            return;

        var options = GetQuestionOptions(option.fpop_forp_id);

        foreach (var op in options)
        {
            if (op.forp_numer_opcji == option.forp_numer_opcji)
                op.fpop_czy_poprawna = true;
            else
                op.fpop_czy_poprawna = false;
        }

        StateHasChanged();
    }

    private void AnswerOnChange(formularz_pytanie_opcja answer)
    {
        var questionId = answer.fpop_forp_id;

        var options = _options.Where(x => x.fpop_forp_id == questionId);
        var answersToAdd = _options.Where(x => x.fpop_forp_id == questionId && x.fpop_czy_poprawna == true);

        _answers.RemoveAll(x => x.fodp_forp_id == questionId);

        foreach (var a in answersToAdd)
        {
            _answers.Add(new formularz_odpowiedz
            {
                fodp_id = -1,
                fodp_for_id = FormId,
                fodp_forp_id = questionId,
                fodp_wybrana_odp = a.fpop_id
            });
        }
    }

    private List<formularz_odpowiedz> GetAllAnswers()
    {
        var allAnswers = _options.Where(x => x.fpop_czy_poprawna == true).ToList();

        return FormHelper.GetFormAnswer(allAnswers, FormId);
    }

    private async Task SaveSolvedForm()
    {
        _finishDateTime = DateTime.Now;

        _solvedForm.FpodId = -1;
        _solvedForm.FpodUserId = -1;
        _solvedForm.FormId = FormId;
        _solvedForm.FpodDataRozpoczenia = _startDateTime;
        _solvedForm.FpodStan = true;
        _solvedForm.FpodDataZakonczenia = _finishDateTime;
        _solvedForm.FpodWykorzystanyCzas = (int)_finishDateTime.Subtract(_startDateTime).TotalSeconds;
        _solvedForm.Odpowiedzi = GetAllAnswers();

        await ApplicationService.SaveSolvedForm(_solvedForm);
    }
}