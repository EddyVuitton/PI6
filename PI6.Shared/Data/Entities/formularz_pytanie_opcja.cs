namespace PI6.Shared.Data.Entities;

public class formularz_pytanie_opcja
{
    public int fpop_id { get; set; }
    public int fpop_forp_id { get; set; }
    public string? fpop_nazwa { get; set; }
    public bool fpop_czy_poprawna { get; set; }
    public int? forp_numer_opcji { get; set; }
}