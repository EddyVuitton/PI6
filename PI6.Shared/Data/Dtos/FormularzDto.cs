namespace PI6.Shared.Data.Dtos;

public class FormularzDto
{
    public int ForId { get; set; }
    public string? Nazwa { get; set; }
    public DateTime DataStworzenia { get; set; }
    public DateTime DataOtwarcia { get; set; }
    public DateTime? DataZamkniecia { get; set; }
    public int? DozwolonePodejscia { get; set; }
    public int? LimitCzasu { get; set; }
    public int? ProgZal { get; set; }
    public int FortId { get; set; }
    public string? FortNazwa { get; set; }
    public int UserId { get; set; }

    public List<PytanieDto>? Pytania { get; set; }
}