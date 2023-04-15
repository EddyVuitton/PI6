using Microsoft.AspNetCore.Components;
using MudBlazor;
using PI6.Components.Objects;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Helpers;
using PI6.WebApi.Services;

namespace PI6.Components.Pages;

public partial class FormSolve
{
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Parameter] public int FormId { get; set; }
    [CascadingParameter] public AppState AppState { get; set; } = new();

    private FormularzDto _formDto = new();
    private List<PytanieDto> _formQuestionsDto = new();
    private formularz _form = new();
    private List<formularz_pytanie> _questions = new();
    private List<formularz_pytanie_opcja> _options = new();
    private IMask _pointsPatternMask = new PatternMask("00");
    private string _title = string.Empty;
    private DateTime _dateOpen = DateTime.Now;
    private DateTime? _dateClose;
    private int? _allowedNumberAppr;
    private int _requiredTime;

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
        AppState.RequiredTime = 5;//_requiredTime;

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

    private void UpdateFirstOptions()
    {
        //First option of required questions
        var options =
            from option in _options
            join question in _questions on option.fpop_forp_id equals question.forp_id
            where option.forp_numer_opcji == 1 && question.forp_czy_wymagane == true
            select option;

        foreach (var op in options)
            _options.FirstOrDefault(x => x.fpop_id == op.fpop_id).fpop_czy_poprawna = true;
    }
}