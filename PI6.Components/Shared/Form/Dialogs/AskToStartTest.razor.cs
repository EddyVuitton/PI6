using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace PI6.Components.Shared.Form.Dialogs;

public partial class AskToStartTest
{
    [Inject] public NavigationManager NavigationManager { get; set; }

    [CascadingParameter] MudDialogInstance MudDialog { get; set; }

    [Parameter] public string Href { get; set; }

    void Cancel() => MudDialog.Cancel();

    private void SendToSolveTest()
    {
        NavigationManager.NavigateTo(Href);
    }
}