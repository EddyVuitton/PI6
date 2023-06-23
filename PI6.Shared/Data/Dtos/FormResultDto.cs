namespace PI6.Shared.Data.Dtos;

public class FormResultDto
{
    public string FormName { get; set; }
    public int FpodId { get; set; }
    public int UsId { get; set; }
    public string UserFullName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int UsedTime { get; set; }
    public int NumberOfCorrectAnswers { get; set; }
    public int NumberOfAllCorrectAnswers { get; set; }
}