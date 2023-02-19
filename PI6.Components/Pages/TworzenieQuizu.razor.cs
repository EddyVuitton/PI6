using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PI6.Components.Pages;

public partial class TworzenieQuizu
{
    [Inject] public IJSRuntime JSRuntime { get; set; }

    private string _title = string.Empty;
    private List<int> _questionList = new();
    private Dictionary<int, int> _questionOption = new();
    private Dictionary<int, string> _questionTitle = new();
    private Dictionary<int, string> _options = new();

    private async Task AddQuestion()
    {
        int questionMax = _questionList.Count == 0 ? 0 : _questionList.Max();
        int questionId = questionMax + 1;
        _questionList.Add(questionId);
        _questionTitle.Add(questionId, string.Empty);

        //await ConsoleLog($"Count: {_questionOption.Count}");

        await AddOption(questionId, string.Empty);

        //var questioOptionMax = (_questionOption.Count == 0 ? 0 : _questionOption.Max(x => x.Value)) + 1;
        //_questionOption.Add(questioOptionMax, questionId);
        //_options.Add(questioOptionMax, string.Empty);

        StateHasChanged();
    }

    private void DeleteQuestion(int id)
    {
        var options = _questionOption.Where(x => x.Key == id).ToArray();
        foreach (var o in options)
            _options.Remove(o.Key);

        _questionOption.Remove(id);
        _questionTitle.Remove(id);
        _questionList.Remove(id);

        StateHasChanged();
    }

    private async Task ConsoleLog(string message)
    {
        await JSRuntime.InvokeVoidAsync("console.log", message);
    }

    private async Task AddOption(int questionId, string optionText)
    {
        var questioOptionMax = (_questionOption.Count == 0 ? 0 : _questionOption.Max(x => x.Key)) + 1;

        await ConsoleLog($"Max _questionOption: {(_questionOption.Count == 0 ? 0 : _questionOption.Max(x => x.Value)) + 1}");

        _questionOption.Add(questioOptionMax, questionId);
        _options.Add(questioOptionMax, optionText);

        StateHasChanged();
    }
}