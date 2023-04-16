using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using PI6.Components.Objects;
using PI6.Components.Shared;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Helpers;
using PI6.WebApi.Services;

namespace PI6.Components.Pages;

public partial class FormSolve
{
    [Inject] public IJSRuntime JSRuntime { get; set; }
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Parameter] public int FormId { get; set; }
    [Parameter] public AppState AppState { get; set; } = new();

    private FormularzDto _formDto = new();
    private List<PytanieDto> _formQuestionsDto = new();
    private formularz _form = new();
    private List<formularz_pytanie> _questions = new();
    private List<formularz_pytanie_opcja> _options = new();
    private List<formularz_odpowiedz> _answers = new();
    private IMask _pointsPatternMask = new PatternMask("00");
    private string _title = string.Empty;
    private DateTime _dateOpen;
    private DateTime? _dateClose;
    private int? _allowedNumberAppr;
    private int _requiredTime;
    private DateTime _startDateTime = DateTime.Now;
    private DateTime _finishDateTime;

    private async Task ConsoleLog(string message)
    {
        await JSRuntime.InvokeVoidAsync("console.log", message);
    }

    protected override async void OnInitialized()
    {
        AppState.LoadStateChanged += OnStateChanged;

        _questions = await ApplicationService.GetFormQuestions(FormId);
        _options = await ApplicationService.GetFormOptions(FormId);
        _form = (await ApplicationService.GetForm(FormId)).FirstOrDefault();

        _formQuestionsDto = Formularz.GetFormQuestionsDto(_questions, _options);
        _formDto = Formularz.GetFormularzDto(_form, _formQuestionsDto);
        
        _title = _formDto.Nazwa;
        _dateOpen = _formDto.DataOtwarcia;
        _dateClose = _formDto.DataZamkniecia;
        _allowedNumberAppr = _formDto.DozwolonePodejscia;
        _requiredTime = _formDto.LimitCzasu ?? 0;
        AppState.RequiredTime = _requiredTime;

        UpdateOptionsOnInitial();

        foreach (var o in _options)
        {
            await ConsoleLog($"{o.fpop_id}");
        }

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

        var options = _options.FirstOrDefault(x => x.fpop_forp_id == questionId);
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

    private void CreateForm()
    {
        List<OpcjaDto> options = (
            from o in _options
            select new OpcjaDto
            {
                PytanieId = o.fpop_forp_id,
                OpcjaId = o.fpop_id,
                OpcjaNazwa = o.fpop_nazwa,
                OpcjaCzyPoprawna = o.fpop_czy_poprawna,
                OpcjaNumerOpc = (int)o.forp_numer_opcji
            }).ToList();

        List<PytanieDto> questions = (
            from q in _questions
            select new PytanieDto
            {
                PytanieId = q.forp_id,
                PytanieNazwa = q.forp_nazwa,
                PytaniePunkty = q.forp_punkty,
                PytanieCzyWieleOdp = q.forp_czy_wiele_odp,
                PytanieCzyWymagane = q.forp_czy_wymagane,
                PytanieForId = q.forp_for_id,
                PytanieNumerPyt = (int)q.forp_numer_pytania,
                Opcje = options.Where(x => x.PytanieId == q.forp_id).ToList()
            }).ToList();

        //ApplicationService.CreateForm(_formDto);
    }
}