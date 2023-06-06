using MudBlazor;

namespace PI6.Components.Helpers;

public interface IErrorHelper
{
    void ShowSnackbar(string message, Severity s);
}