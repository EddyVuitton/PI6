namespace PI6.Shared.Data.Dtos;

public class FormularzKafelekDto
{
    public int ForId { get; set; }
    public string? Nazwa { get; set; }
    public string? FortNazwa { get; set; }
    public DateTime DataStworzenia { get; set; }
    public DateTime DataOtwarcia { get; set; }
    public DateTime? DataZamkniecia { get; set; }
    public int? IloscPodejsc { get; set; }
}