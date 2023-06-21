using Microsoft.AspNetCore.Components;
using MudBlazor;
using PI6.Components.Shared.Form.Dialogs;
using PI6.Shared.Data.Dtos;

namespace PI6.Components.Shared;

public partial class FormTilesIndex
{
    [Inject] private IDialogService DialogService { get; set; }

    [Parameter] public List<FormularzKafelekDto> FormTiles { get; set; }
    [Parameter] public AccountDto Account { get; set; }

    private bool _isStudent = false;
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

        _isStudent = Account.UstName == "Student" ? true : false;
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

    private void OpenFormDatesDialog(int formId)
    {
        var parameters = new DialogParameters { 
            ["FormId"] = formId
        };

        var options = new DialogOptions
        {
            CloseOnEscapeKey = true,
            NoHeader = true
        };

        DialogService.Show<FormDates>(null, parameters, options);
    }
}