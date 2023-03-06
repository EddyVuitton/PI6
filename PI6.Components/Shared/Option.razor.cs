using Microsoft.AspNetCore.Components;
using PI6.Shared.Data.Entities;

namespace PI6.Components.Shared;

public partial class Option
{
    [Parameter] public formularz_pytanie_opcja OptionParam { get; set; }
    [Parameter] public bool IsMultipleOptions { get; set; }
    private bool _isCorrect;
    private string _text;
    private int? _lp;

    protected override void OnInitialized()
    {
        _isCorrect = OptionParam.fpop_czy_poprawna;
        _text = OptionParam.fpop_nazwa;
        _lp = OptionParam.forp_numer_opcji;
    }

    private void Delete()
    {
        //var questionOptions = _options.Where(x => x.fpop_forp_id == option.fpop_forp_id).ToList();
        //if (questionOptions.Count > 1)
        //    _options.Remove(option);

        //UpdateLPs();
        StateHasChanged();
    }

    private void UpdateOtherOptionsOnChange()
    {
        if (IsMultipleOptions)
            return;

        //var options = GetQuestionOptions(option.fpop_forp_id);

        //foreach (var op in options)
        //{
        //    if (op.forp_numer_opcji == option.forp_numer_opcji)
        //        op.fpop_czy_poprawna = true;
        //    else
        //        op.fpop_czy_poprawna = false;
        //}

        StateHasChanged();
    }
}