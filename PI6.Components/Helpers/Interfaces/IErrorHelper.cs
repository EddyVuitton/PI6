using MudBlazor;

namespace PI6.Components.Helpers.Interfaces;

public interface IErrorHelper
{
    void ShowSnackbar(string message, Severity s);
}