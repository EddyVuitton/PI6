using Microsoft.AspNetCore.Components;
using MudBlazor;
using PI6.Components.Shared.Account;
using PI6.Shared.Data.Dtos;

namespace PI6.Components.Shared;

public partial class FormTilesIndex
{
    [Inject] private IDialogService DialogService { get; set; }

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

    private void OpenLoginDialog()
    {
        var options = new DialogOptions
        {
            CloseOnEscapeKey = true,
            NoHeader = true,
            MaxWidth = MaxWidth.Small,
            FullWidth = true
        };
        DialogService.Show<UserGroup>(null, options);
    }
}