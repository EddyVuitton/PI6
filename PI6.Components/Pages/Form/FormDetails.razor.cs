using Microsoft.AspNetCore.Components;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Helpers;
using PI6.WebApi.Services;

namespace PI6.Components.Pages.Form;

public partial class FormDetails
{
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Parameter] public int FormId { get; set; }

    private FormularzDto _formDto = new();
    private List<PytanieDto> _formQuestionsDto = new();
    private formularz _form = new();
    private List<formularz_pytanie> _questions = new();
    private List<formularz_pytanie_opcja> _options = new();
    private string _title = string.Empty;
    private DateTime _dateOpen = DateTime.Now;
    private DateTime? _dateClose;
    private string _requiredSeconds;
    private string _requiredMinutes;
    private string _requiredHours;

    protected override async Task OnInitializedAsync()
    { 
        _questions = await ApplicationService.GetFormQuestions(FormId);
        _options = await ApplicationService.GetFormOptions(FormId);
        _form = await ApplicationService.GetForm(FormId);

        _formQuestionsDto = FormHelper.GetFormQuestionsDto(_questions, _options);
        _formDto = FormHelper.GetFormularzDto(_form, _formQuestionsDto);

        TimeSpan time = TimeSpan.FromSeconds(_formDto.LimitCzasu ?? 0);

        _title = _formDto.Nazwa;
        _dateOpen = _formDto.DataOtwarcia;
        _dateClose = _formDto.DataZamkniecia;
        _requiredSeconds = FormatRequiredTime(time.Seconds);
        _requiredMinutes = FormatRequiredTime(time.Minutes);
        _requiredHours = FormatRequiredTime(time.Hours);

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