using Microsoft.AspNetCore.Components;

namespace PI6.Components.Layout;

public partial class MainLayout
{
    [Inject] private NavigationManager _navigationManager { get; set; }
    private bool _drawerOpen = true;

    private string roles = "Admin,Lecturer";

    private void NavToIndex()
    {
        _navigationManager.NavigateTo($"/", true);
        StateHasChanged();
    }
}