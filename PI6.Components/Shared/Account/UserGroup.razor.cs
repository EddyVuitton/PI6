using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace PI6.Components.Shared.Account;

public partial class UserGroup
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }

    void Cancel() => MudDialog.Cancel();
}