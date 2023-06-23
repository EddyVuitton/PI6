using Microsoft.AspNetCore.Components;
using PI6.Shared.Data.Entities;

namespace PI6.Components.Shared.Account;

public partial class Classes
{
    [Parameter] public List<student_group> StudentGroups { get; set; }
}