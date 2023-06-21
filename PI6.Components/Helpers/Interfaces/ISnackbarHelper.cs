using MudBlazor;

namespace PI6.Components.Helpers.Interfaces;

public interface ISnackbarHelper
{
    //void ShowSnackbar(string message, Severity s);
    void ShowSnackbar(string message, Severity s, bool autoHide = true, bool showTime = false);
}