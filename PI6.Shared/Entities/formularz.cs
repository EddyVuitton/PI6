namespace PI6.Shared.Entities;

public class formularz
{
    public int for_id { get; set; }
    public string for_nazwa { get; set; }
    public int for_fort_id { get; set; }
    public DateTime for_data_stworzenia { get; set; }

    public virtual formularz_typ formularz_typ { get; set; }
}