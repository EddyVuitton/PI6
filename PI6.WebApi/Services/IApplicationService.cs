using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;

namespace PI6.WebApi.Services;

public interface IApplicationService
{
    public Task<List<formularz>> GetForms();
    public Task<List<formularz>> GetForm(int for_id);
    public Task<List<formularz_typ>> GetFormType();
    public Task<List<FormularzKafelekDto>> GetFormTileDto();
    public Task CreateForm(FormularzDto form);
    public Task<List<formularz_pytanie>> GetFormQuestions(int for_id);
    public Task<List<formularz_pytanie_opcja>> GetFormOptions(int for_id);
    public Task SaveSolvedForm(FormularzPodejscieDto solvedForm);
}