using PI6.Shared.Data.Entities;

namespace PI6.Shared.Data.Dtos;

public class FormularzPodejscieDto
{
    public int FpodId { get; set; }
    public int FpodUserId { get; set; }
    public int FormId { get; set; }
    public DateTime FpodDataRozpoczenia { get; set; }
    public bool FpodStan { get; set; }
    public DateTime? FpodDataZakonczenia { get; set; }
    public int? FpodWykorzystanyCzas { get; set; }

    public List<formularz_odpowiedz> Odpowiedzi { get; set; }
}