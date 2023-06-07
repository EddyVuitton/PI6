using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using PI6.Components.Helpers.Interfaces;
using PI6.WebApi.Helpers;
using PI6.WebApi.Services;

namespace PI6.Components.Pages;

public partial class Index
{
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Inject] public IJSRuntime JS { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IErrorHelper ErrorHelper { get; set; }

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

    /*
    private async Task Click()
    {
        try
        {
            //Snackbar.Add("Rozpoczęcie importu", Severity.Info);
            ErrorHelper.ShowSnackbar("Rozpoczęcie importu", Severity.Info);
            await Task.Delay(5000);

            //throw new Exception("To jest przykładowy wyjątek");
            var response = await ApplicationService.Test();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }

            ErrorHelper.ShowSnackbar("Import zakończony sukcesem", Severity.Success);
        }
        catch (Exception ex)
        {
            ErrorHelper.ShowSnackbar("Import zakończony błędem", Severity.Warning);

            ErrorHelper.ShowSnackbar(ex.Message, Severity.Error);
        }
    }
    */
}