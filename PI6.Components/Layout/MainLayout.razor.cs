using Microsoft.AspNetCore.Components;

namespace PI6.Components.Layout;

public partial class MainLayout
{
    [Inject] public NavigationManager NavigationManager { get; set; }
    private bool _drawerOpen = true;

    private string _roles = "Admin,Lecturer";

    private void NavToIndex()
    {
        NavigationManager.NavigateTo($"/", true);
        StateHasChanged();
    }
}