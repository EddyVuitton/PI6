namespace PI6.Shared.Data.Entities;

public class student_group
{
    public int sgr_id { get; set; }
    public string sgr_name { get; set; }
    public string sgr_field_of_study { get; set; }
    public string sgr_speciality { get; set; }
    public DateTime sgr_start_date { get; set; }
    public DateTime? sgr_end_date { get; set; }
    public int sgr_us_id { get; set; }
}