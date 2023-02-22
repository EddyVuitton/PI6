using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PI6.Components.Shared.Objects;
using PI6.Shared.Data.Entities;

namespace PI6.Components.Pages;

public partial class TworzenieQuizu
{
    [Inject] public IJSRuntime JSRuntime { get; set; }

    private string _title = string.Empty;
    private readonly List<Question> _questions = new();
    private readonly List<QuestionOption> _questionOptions = new();

    private readonly List<formularz_pytanie> _pytaniaTest = new();
    private readonly List<formularz_pytanie_opcja> _opcjeTest = new();

    protected override void OnInitialized()
    {
        AddQuestion();
    }

    private async Task ConsoleLog(string message)
    {
        await JSRuntime.InvokeVoidAsync("console.log", message);
    }

    private void AddQuestion()
    {
        int questionId = _questions.Count == 0 ? 1 : _questions.Max(x => x.GetId()) + 1;

        _questions.Add(new Question(questionId, 1, string.Empty));

        _pytaniaTest.Add(new formularz_pytanie
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

        UpdateLPs();
        StateHasChanged();
    }

    private void UpdateLPs()
    {
        int lpQuestion = 1;

        foreach (var q in _questions)
        {
            q.SetLp(lpQuestion);

            int lpOption = 1;

            foreach (var qo in GetQuestionOptions(q.GetId()))
            {
                qo.SetLp(lpOption);
                lpOption++;
            }

            lpQuestion++;
        }

        lpQuestion = 1;

        foreach (var q in _pytaniaTest)
        {
            q.forp_numer_pytania = lpQuestion;

            int lpOption = 1;

            foreach (var qo in GetQuestionOptionsTest(q.forp_id))
            {
                qo.forp_numer_opcji = lpOption;
                lpOption++;
            }

            lpQuestion++;
        }

        StateHasChanged();
    }

    private void DeleteQuestion(Question question)
    {
        var questions = _questions;

        if (questions.Count > 1)
        {
            _questionOptions.RemoveAll(x => x.GetQuestionId() == question.GetId());
            _questions.Remove(question);
        }

        UpdateLPs();
        StateHasChanged();
    }

    private List<QuestionOption> GetQuestionOptions(int questionId) => _questionOptions.Where(x => x.GetQuestionId() == questionId).ToList();
    private List<formularz_pytanie_opcja> GetQuestionOptionsTest(int questionId) => _opcjeTest.Where(x => x.fpop_forp_id == questionId).ToList();

    private void AddOption(int questionId, string optionText)
    {
        int optionId = _questionOptions.Count == 0 ? 1 : _questionOptions.Max(x => x.GetId()) + 1;
        _questionOptions.Add(new QuestionOption(optionId, optionId, questionId, optionText));

        _opcjeTest.Add(new formularz_pytanie_opcja
        {
            fpop_id = optionId,
            fpop_forp_id = questionId,
            fpop_nazwa = optionText,
            fpop_czy_poprawna = false,
            forp_numer_opcji = optionId
        });

        UpdateLPs();
        StateHasChanged();
    }
    private void EditOption(int optionId, string optionText)
    {
        var questionOption = _questionOptions.FirstOrDefault(x => x.GetId() == optionId);
        questionOption.SetText(optionText);

        StateHasChanged();
    }

    private void DeleteOption(QuestionOption option)
    {
        var options = _questionOptions.Where(x => x.GetQuestionId() == option.GetQuestionId()).ToList();

        if (options.Count > 1)
            _questionOptions.Remove(option);

        UpdateLPs();
        StateHasChanged();
    }
}