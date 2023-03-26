using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PI6.Shared.Data.Dtos;
using PI6.WebApi.Services;

namespace PI6.Components.Shared;

public partial class FormTilesIndex
{
    [Parameter] public List<FormularzKafelekDto> FormTiles { get; set; }
}