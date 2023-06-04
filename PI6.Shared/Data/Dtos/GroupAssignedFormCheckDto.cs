namespace PI6.Shared.Data.Dtos;

public class GroupAssignedFormCheckDto
{
    public int UsId { get; set; }
    public int GrpId { get; set; }
    public string GrpName { get; set; }
    public int ForId { get; set; }
    public bool Check { get; set; }
}