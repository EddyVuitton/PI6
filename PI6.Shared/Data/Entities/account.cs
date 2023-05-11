namespace PI6.Shared.Data.Entities;

public class account
{
    public int us_id { get; set; }
    public string us_name { get; set; }
    public string us_surname { get; set; }
    public string us_email { get; set; }
    public string us_pass { get; set; }
    public int us_ust_id { get; set; }
    public DateTime us_activate { get; set; }
    public DateTime? us_deactivate { get; set; }
    public int us_is_active { get; set; }
}