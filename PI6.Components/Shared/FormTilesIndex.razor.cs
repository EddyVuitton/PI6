using Microsoft.AspNetCore.Components;
using PI6.Shared.Data.Dtos;

namespace PI6.Components.Shared;

public partial class FormTilesIndex
{
    [Parameter] public List<FormularzKafelekDto> FormTiles { get; set; }
    [Parameter] public AccountDto Account { get; set; }

    private string _href = string.Empty;

    protected override void OnInitialized()
    {
        _href = Account.UstName switch
        {
            "Lecturer" => "formdetails",
            "Student" => "formsolve",
            "Admin" => "formdetails",
            _ => "formdetails"
        };
    }
}