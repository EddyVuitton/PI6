using PI6.Shared.Data.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using PI6.WebApi.Services;
using PI6.WebApi.Helpers;
using PI6.Shared.Data.Temp;

namespace PI6.Components.Pages;

public partial class TworzenieQuizu
{
    [Inject] public IJSRuntime JSRuntime { get; set; }
    [Inject] public IApplicationService ApplicationService { get; set; }

    private string _title = string.Empty;
    private readonly List<formularz_pytanie> _questions = new();
    private readonly List<formularz_pytanie_opcja> _options = new();
    private readonly IMask _pointsPatternMask = new PatternMask("00");

    protected override void OnInitialized()
    {
        AddQuestion();
    }

    private async Task ConsoleLog(string message)
    {
        await JSRuntime.InvokeVoidAsync("console.log", message);
    }

    private List<formularz_pytanie_opcja> GetQuestionOptions(int questionId) => _options.Where(x => x.fpop_forp_id == questionId).ToList();

    private int GetTotalPoints() => _questions.Sum(x => x.forp_punkty);

    private void AddQuestion()
    {
        bool isFirst = !_questions.Any();
        int questionId = isFirst ? 1 : _questions.Max(x => x.forp_id) + 1;

        _questions.Add(new formularz_pytanie
        {
            forp_id = questionId,
            forp_nazwa = string.Empty,
            forp_punkty = 1,
            forp_czy_wiele_odp = false,
            forp_czy_wymagane = false,
            forp_for_id = -1,
            forp_numer_pytania = 1
        });

        AddOption(questionId, string.Empty);
        AddOption(questionId, string.Empty);

        UpdateLPs();
        StateHasChanged();
    }

    private void UpdateLPs()
    {
        int lpQuestion = 1;

        foreach (var q in _questions)
        {
            q.forp_numer_pytania = lpQuestion;

            int lpOption = 1;
            foreach (var qo in GetQuestionOptions(q.forp_id))
            {
                qo.forp_numer_opcji = lpOption;
                lpOption++;
            }

            lpQuestion++;
        }

        StateHasChanged();
    }

    private void DeleteQuestion(formularz_pytanie question)
    {
        if (_questions.Count > 1)
        {
            _options.RemoveAll(x => x.fpop_forp_id == question.forp_id);
            _questions.Remove(question);
        }

        UpdateLPs();
        StateHasChanged();
    }

    private void AddOption(int questionId, string optionText)
    {
        bool isFirst = !_options.Any(x => x.fpop_forp_id == questionId);
        int optionId = isFirst ? 1 : _options.Max(x => x.fpop_id) + 1;

        _options.Add(new formularz_pytanie_opcja
        {
            fpop_id = optionId,
            fpop_forp_id = questionId,
            fpop_nazwa = optionText,
            fpop_czy_poprawna = isFirst,
            forp_numer_opcji = optionId
        });

        UpdateLPs();
        StateHasChanged();
    }

    private void DeleteOption(formularz_pytanie_opcja option)
    {
        var questionOptions = _options.Where(x => x.fpop_forp_id == option.fpop_forp_id).ToList();
        if (questionOptions.Count > 1)
            _options.Remove(option);

        UpdateLPs();
        StateHasChanged();
    }

    private void CreateForm()
    {
        Model1 model = new()
        {
            questions = _questions,
            options = _options
        };

        ApplicationService.ZapiszFormularz(model);
    }
}