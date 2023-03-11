using PI6.Shared.Data.Entities;

namespace PI6.Shared.Data.Dtos;

public class FormularzKafelekDto
{
    public int ForId { get; set; }
    public string? Nazwa { get; set; }
    public string? FortNazwa { get; set; }
    public DateTime DataStworzenia { get; set; }
    public DateTime DataOtwarcia { get; set; }
    public DateTime DataZamkniecia { get; set; }
    //public int FpodId { get; set; }
    //public int FpodUserId { get; set; }
    //public DateTime FpodDataRozpoczecia { get; set; }
    //public DateTime FpodDataZakonczenia { get; set; }
    //public bool FpodStan { get; set; }
    //public int FpodWykorzystanyCzas { get; set; }

    public List<formularz_podejscie>? Podejscia { get; set; }
}