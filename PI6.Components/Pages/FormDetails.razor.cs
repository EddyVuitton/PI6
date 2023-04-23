using Microsoft.AspNetCore.Components;
using MudBlazor;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Helpers;
using PI6.WebApi.Services;

namespace PI6.Components.Pages;

public partial class FormDetails
{
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Parameter] public int FormId { get; set; }

    private FormularzDto _formDto = new();
    private List<PytanieDto> _formQuestionsDto = new();
    private formularz _form = new();
    private List<formularz_pytanie> _questions = new();
    private List<formularz_pytanie_opcja> _options = new();
    private IMask _pointsPatternMask = new PatternMask("00");
    private IMask _regHours = new RegexMask(@"^([1-9][0-9]|[0-9])$");
    private IMask _regMinutes = new RegexMask(@"^([0-5]?[0-9])$");
    private IMask _regSeconds = new RegexMask(@"^([0-5]?[0-9])$");
    private string _title = string.Empty;
    private DateTime _dateOpen = DateTime.Now;
    private DateTime? _dateClose;
    private int? _allowedNumberAppr;
    private string _requiredSeconds;
    private string _requiredMinutes;
    private string _requiredHours;
    private int? _passingThreshold;

    protected override async Task OnInitializedAsync()
    {
        _questions = await ApplicationService.GetFormQuestions(FormId);
        _options = await ApplicationService.GetFormOptions(FormId);
        _form = (await ApplicationService.GetForm(FormId)).FirstOrDefault();

        _formQuestionsDto = FormHelper.GetFormQuestionsDto(_questions, _options);
        _formDto = FormHelper.GetFormularzDto(_form, _formQuestionsDto);

        TimeSpan time = TimeSpan.FromSeconds(_formDto.LimitCzasu ?? 0);

        _title = _formDto.Nazwa;
        _dateOpen = _formDto.DataOtwarcia;
        _dateClose = _formDto.DataZamkniecia;
        _allowedNumberAppr = _formDto.DozwolonePodejscia;
        _requiredSeconds = FormatRequiredTime(time.Seconds);
        _requiredMinutes = FormatRequiredTime(time.Minutes);
        _requiredHours = FormatRequiredTime(time.Hours);
        _passingThreshold = _formDto.ProgZal;

        StateHasChanged();
    }

    private static string FormatRequiredTime(int time)
    {
        if (time < 10)
            return string.Concat("0", time);
        else
            return time.ToString();
    }

    private List<formularz_pytanie_opcja> GetQuestionOptions(int questionId) => _options.Where(x => x.fpop_forp_id == questionId).ToList();

    private int GetTotalPoints() => _questions.Sum(x => x.forp_punkty);
}