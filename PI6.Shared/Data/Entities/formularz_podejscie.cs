namespace PI6.Shared.Data.Entities;

public class formularz_podejscie
{
    public int fpod_id { get; set; }
    public int fpod_user_id { get; set; }
    public int fpod_for_id { get; set; }
    public DateTime fpod_data_rozpoczenia { get; set; }
    public bool fpod_stan { get; set; }
    public DateTime? fpod_data_zakonczenia { get; set; }
    public int? fpod_wykorzystany_czas { get; set; }
}