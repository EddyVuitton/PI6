using Microsoft.AspNetCore.Components;
using MudBlazor;
using PI6.Components.Helpers.Interfaces;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Services;

namespace PI6.Components.Pages.Form;

public partial class FormSolvedDetails
{
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Inject] public ISnackbarHelper ErrorHelper { get; set; }

    [Parameter] public int FpodId { get; set; }

    private formularz_podejscie _solvedForm;
    private formularz _form;
    private List<formularz_pytanie> _questions;
    private List<formularz_pytanie_opcja> _options;
    private List<formularz_podejscie_odpowiedz> _solvedFormsAnswers;

    private int _formId;
    private string _title;
    private DateTime _dateStart;
    private DateTime? _dateEnd;
    private string _usedSeconds;
    private string _usedMinutes;
    private string _usedHours;
    private int _userFormSumPoints;

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
        StateHasChanged();
    }

    private List<formularz_pytanie_opcja> GetQuestionOptions(int questionId) => _options.Where(x => x.fpop_forp_id == questionId).ToList();

    private int GetTotalPoints()
    {
        var totalPoints = _questions is not null ? _questions.Sum(x => x.forp_punkty) : 0;

        return totalPoints;
    }

    private async Task LoadData()
    {
        try
        {
            _solvedForm = await ApplicationService.GetSolvedForm(FpodId);
            _formId = _solvedForm.fpod_for_id;
            _form = await ApplicationService.GetForm(_formId);
            _questions = await ApplicationService.GetFormQuestions(_formId);
            _options = await ApplicationService.GetFormOptions(_formId);
            _solvedFormsAnswers = await ApplicationService.GetSolvedFormsAnswers(FpodId);

            _title = _form.for_nazwa;
            _dateStart = _solvedForm.fpod_data_rozpoczenia;
            _dateEnd = _solvedForm.fpod_data_zakonczenia;
            
            TimeSpan timeSpan = TimeSpan.FromSeconds((_dateEnd - _dateStart).Value.TotalSeconds);
            _usedSeconds = FormatRequiredTime(timeSpan.Seconds);
            _usedMinutes = FormatRequiredTime(timeSpan.Minutes);
            _usedHours = FormatRequiredTime(timeSpan.Hours);

            var userCorrectAnswers =
                (from sfa in _solvedFormsAnswers
                 join co in _options on sfa.fodp_wybrana_odp equals co.fpop_id
                 where co.fpop_czy_poprawna
                 select co).ToList();

            _userFormSumPoints =
                (from q in _questions
                 join uca in userCorrectAnswers on q.forp_id equals uca.fpop_forp_id
                 select q).Sum(x => x.forp_punkty);
        }
        catch (Exception ex)
        {
            ErrorHelper.ShowSnackbar(ex.Message, Severity.Error, false);
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