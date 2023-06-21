using Microsoft.AspNetCore.Components;
using MudBlazor;
using PI6.Components.Helpers.Interfaces;

namespace PI6.Components.Helpers;

public class SnackbarHelper : ISnackbarHelper
{
    [Inject] public ISnackbar Snackbar { get; set; }

    public SnackbarHelper(ISnackbar snackbar)
    {
        Snackbar = snackbar;
        Snackbar.Configuration.PreventDuplicates = false;
        Snackbar.Configuration.RequireInteraction = true;
        Snackbar.Configuration.HideTransitionDuration = 500;
    }

    public void ShowSnackbar(string message, Severity s, bool autoHide = false, bool showTime = false)
    {
        var now = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ";

        if (!showTime)
            now = string.Empty;

        if (autoHide)
            Snackbar.Configuration.RequireInteraction = false;

        Snackbar.Add($"{now}{message}", s);
    }
}