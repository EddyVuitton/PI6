using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PI6.Shared.Data.Dtos;
using PI6.WebApi.Helpers;
using PI6.WebApi.Services;

namespace PI6.Components.Pages.Form;

public partial class FormIndex
{
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Inject] public IJSRuntime JS { get; set; }

    private List<FormularzKafelekDto> _formTiles;
    private AccountDto _accountDto = new();
    private readonly string _roles = "Admin,Lecturer";

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _formTiles = await ApplicationService.GetFormTileDto();
            StateHasChanged();
        }
        catch (Exception e)
        {
            ExceptionHelper.PrintException(e);
        }
    }

    protected override async Task OnAfterRenderAsync(bool isFirstRender)
    {
        var loggedEmail = await JS.GetFromLocalStorage("email");
        _accountDto = await ApplicationService.GetAccountDtoByEmail(loggedEmail);
    }
}