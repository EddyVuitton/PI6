using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using PI6.Components.Helpers.Interfaces;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Services;

namespace PI6.Components.Shared.Form.Dialogs;

public partial class FormDates
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    [Parameter] public int FormId { get; set; }
    [Inject] public IApplicationService ApplicationService { get; set; }
    [Inject] public IJSRuntime JS { get; set; }
    [Inject] public ISnackbarHelper ErrorHelper { get; set; }

    private formularz _form = new();
    private FormDatesDto _dto = new();

    protected override async Task OnInitializedAsync()
    {
        _form = await ApplicationService.GetForm(FormId);
        _dto.FormId = FormId;
        _dto.StartDate = _form.for_data_otwarcia;
        _dto.EndDate = _form.for_data_zamkniecia;
    }

    private void Cancel() => MudDialog.Cancel();

    private async Task Save()
    {
        try
        {
            var responseMessage = await ApplicationService.SaveFormDates(_dto);

            if (responseMessage == null)
                throw new NullReferenceException();

            if (!responseMessage.IsSuccessStatusCode)
                throw new Exception(responseMessage.ReasonPhrase);

            ErrorHelper.ShowSnackbar("Poprawnie zapisano", Severity.Success);
        }
        catch (NullReferenceException e)
        {
            ErrorHelper.ShowSnackbar("Błąd przy zapisywaniu", Severity.Warning, false);
            ErrorHelper.ShowSnackbar("Obiekt nie został poprawnie zainicjonowany", Severity.Error, false);
        }
        catch (Exception e)
        {
            ErrorHelper.ShowSnackbar("Błąd przy zapisywaniu", Severity.Warning, false);
            ErrorHelper.ShowSnackbar(e.Message, Severity.Error, false);
        }

        Cancel();
    }
}