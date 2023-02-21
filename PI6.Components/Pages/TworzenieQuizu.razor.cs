using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PI6.Components.Shared.Objects;

namespace PI6.Components.Pages;

public partial class TworzenieQuizu
{
    [Inject] public IJSRuntime JSRuntime { get; set; }

    private string _title = string.Empty;
    private readonly List<Question> _questions = new();
    private readonly List<QuestionOption> _questionOptions = new();

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

            foreach (var qo in _questionOptions.Where(x => x.GetQuestionId() == q.GetId()))
            {
                qo.SetLp(lpOption);
                lpOption++;
            }

            lpOption = 1;
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

    private void AddOption(int questionId, string optionText)
    {
        int optionId = _questionOptions.Count == 0 ? 1 : _questionOptions.Max(x => x.GetId()) + 1;
        _questionOptions.Add(new QuestionOption(optionId, optionId, questionId, optionText));

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