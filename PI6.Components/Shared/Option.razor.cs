using PI6.Shared.Data.Entities;

namespace PI6.Components.Shared;

public partial class Option
{
    private formularz_pytanie_opcja _option { get; set; }
    private bool _isCorrect;
    private string _text;
    private int? _lp;

    protected override void OnInitialized()
    {
        _isCorrect = _option.fpop_czy_poprawna;
        _text = _option.fpop_nazwa;
        _lp = _option.forp_numer_opcji;
    }

    private void Delete()
    {

    }
}