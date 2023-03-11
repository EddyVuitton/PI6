namespace PI6.Shared.Data.Entities;

public class formularz_pytanie
{
    public int forp_id { get; set; }
    public string? forp_nazwa { get; set; }
    public int forp_punkty { get; set; }
    public bool forp_czy_wiele_odp { get; set; }
    public bool forp_czy_wymagane { get; set; }
    public int forp_for_id { get; set; }
    public int? forp_numer_pytania { get; set; }
}