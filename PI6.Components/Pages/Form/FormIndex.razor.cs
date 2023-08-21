using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using PI6.Components.Helpers.Interfaces;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Helpers;
using PI6.WebApi.Services;
using System.Security.Claims;

namespace PI6.Components.Pages.Form;

public partial class FormIndex
{
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Inject] public IJSRuntime JS { get; set; }
    [Inject] public ISnackbarHelper ErrorHelper { get; set; }

    private List<FormularzKafelekDto> _formTiles;
    private List<FormularzKafelekDto> _activeFormTiles;
    private List<FormularzKafelekDto> _deactiveFormTiles;
    private List<formularz_podejscie> _solvedForms;
    private List<formularz_podejscie_odpowiedz> _solvedFormsAnswers;
    private AccountDto _accountDto = new();
    private readonly string _otherRoles = "Admin,Lecturer";
    private readonly string _studentRole = "Student";
    private readonly DateTime? _now = DateTime.Now;

    [CascadingParameter] private Task<AuthenticationState> authenticationState { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (authenticationState is not null)
        {
            var authState = await authenticationState;
            var user = authState?.User;

            if (user?.Identity is not null && user.Identity.IsAuthenticated)
            {
                try
                {
                    await LoadAccount();
                    _formTiles = await ApplicationService.GetFormTileDto();
                    _activeFormTiles = _formTiles.Where(x => x.DataZamkniecia >= _now).ToList();
                    _deactiveFormTiles = _formTiles.Where(x => x.DataZamkniecia < _now).ToList();
                    _solvedForms = await ApplicationService.GetSolvedForms(_accountDto.UserId);
                    _solvedFormsAnswers = new();
                    
                    //foreach (var aft in _activeFormTiles)
                    //{
                    //    foreach (var sf in _solvedForms)
                    //    {
                    //        if (aft.ForId == sf.fpod_for_id)
                    //            _activeFormTiles.Remove(aft);
                    //    }
                    //}

                    //foreach (var sf in _solvedForms)
                    //{
                    //    FormularzKafelekDto item = null;
                    //    if (_activeFormTiles.Exists(x => x.ForId == sf.fpod_for_id))
                    //    {
                    //        item = _activeFormTiles.First(x => x.ForId == sf.fpod_for_id);
                    //    }
                            
                    //    if (item is not null)
                    //        _activeFormTiles.Remove(item);
                    //}

                    await LoadAnswers();

                    StateHasChanged();
                }
                catch (Exception e)
                {
                    ErrorHelper.ShowSnackbar(e.Message, MudBlazor.Severity.Error, false, true);
                }
            }
        }        
    }

    private async Task LoadAccount()
    {
        var authState = await authenticationState;
        var user = authState?.User;
        var loggedEmail = user.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        _accountDto = await ApplicationService.GetAccountDtoByEmail(loggedEmail);
    }

    private async Task LoadAnswers()
    {
        foreach (var sF in _solvedForms)
        {
            var answers = await ApplicationService.GetSolvedFormsAnswers(sF.fpod_id);
            foreach (var ans in answers)
            {
                _solvedFormsAnswers.Add(ans);
            }
        }
    }
}