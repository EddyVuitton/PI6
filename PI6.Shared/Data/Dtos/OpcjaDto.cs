namespace PI6.Shared.Data.Dtos;

public class OpcjaDto
{
    public int PytanieId { get; set; }
    public int OpcjaId { get; set; }
    public string? OpcjaNazwa { get; set; }
    public bool OpcjaCzyPoprawna { get; set; }
    public int OpcjaNumerOpc { get; set; }
}