using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PI6.Shared.Data.Dtos;

namespace PI6.Components.Shared;

public partial class FormTilesIndex
{
    [Inject] public IJSRuntime JSRuntime { get; set; }
    [Parameter] public List<FormularzKafelekDto> FormTiles { get; set; }

    private async Task ConsoleLog(string message)
    {
        await JSRuntime.InvokeVoidAsync("console.log", message);
    }
}