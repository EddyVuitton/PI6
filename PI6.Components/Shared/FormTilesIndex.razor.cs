using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting.Server;
using MudBlazor;
using PI6.Components.Shared.Account;
using PI6.Components.Shared.Form.Dialogs;
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

    private void OpenAssignFormDialog(int formId)
    {
        var parameters = new DialogParameters { ["FormId"] = formId };

        var options = new DialogOptions
        {
            CloseOnEscapeKey = true,
            NoHeader = true,
            MaxWidth = MaxWidth.Small,
            FullWidth = true
        };
        DialogService.Show<AssignForm>(null, parameters, options);
        StateHasChanged();
    }

    private void OpenFormDatesDialog()
    {
        var options = new DialogOptions
        {
            CloseOnEscapeKey = true,
            NoHeader = true,
            MaxWidth = MaxWidth.Small,
            FullWidth = true
        };
        DialogService.Show<FormDates>(null, options);
        StateHasChanged();
    }
}