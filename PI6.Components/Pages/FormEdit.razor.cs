using Microsoft.AspNetCore.Components;
using MudBlazor;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;

namespace PI6.Components.Pages;

public partial class FormEdit
{
    [Parameter] public int FormId { get; set; }

    private readonly FormularzDto newForm = new();
    private readonly List<formularz_pytanie> _questions = new();
    private readonly List<formularz_pytanie_opcja> _options = new();
    private readonly IMask _pointsPatternMask = new PatternMask("00");
    private readonly IMask _regHours = new RegexMask(@"^([1-9][0-9]|[0-9])$", "00");
    private readonly IMask _regMinutes = new RegexMask(@"^([0-5]?[0-9])$", "00");
    private readonly IMask _regSeconds = new RegexMask(@"^([0-5]?[0-9])$", "00");
    private string _title = string.Empty;
    private DateTime _dateOpen = DateTime.Now;
    private DateTime? _dateClose;
    private int? _allowedNumberAppr;
    private int? _requiredSeconds;
    private int? _requiredMinutes;
    private int? _requiredHours;
    private int? _passingThreshold;

    protected override void OnInitialized()
    {
        AddQuestion();
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

    private void UpdateCorrectsOnChange(int questionId, ChangeEventArgs e)
    {
        if (e.Value.Equals(true))
            return;

        var options = GetQuestionOptions(questionId);
        var firstCorrectAnswert = options.FirstOrDefault(x => x.fpop_czy_poprawna == true);

        if (firstCorrectAnswert == null)
            return;

        foreach (var op in options)
        {
            if (op.forp_numer_opcji != firstCorrectAnswert.forp_numer_opcji)
                op.fpop_czy_poprawna = false;
        }

        StateHasChanged();
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

    private void DoNothing() { }
}