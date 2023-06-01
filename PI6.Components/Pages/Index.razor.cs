using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PI6.Shared.Data.Dtos;
using PI6.WebApi.Helpers;
using PI6.WebApi.Services;

namespace PI6.Components.Pages;

public partial class Index
{
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Inject] public IJSRuntime JS { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }

    protected override async Task OnAfterRenderAsync(bool isFirstRender)
    {
        if (isFirstRender)
        {
            var loggedEmail = await JS.GetFromLocalStorage("email");
            if (!string.IsNullOrEmpty(loggedEmail))
            {
                NavigationManager.NavigateTo("/home");
                StateHasChanged();
            }
        }
    }
}