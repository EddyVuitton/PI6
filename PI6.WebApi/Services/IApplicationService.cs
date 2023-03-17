using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;

namespace PI6.WebApi.Services;

public interface IApplicationService
{
    public Task<List<formularz>> PobierzFormularze();
    public Task<List<formularz>> PobierzFormularz(int for_id);
    public Task<List<formularz_typ>> PobierzFormularzTyp();
    public Task<List<FormularzKafelekDto>> PobierzFormularzKafelekDto();
    public Task ZapiszFormularz(FormularzDto form);
    public Task<List<formularz_pytanie>> PobierzPytaniaFormularza(int for_id);
    public Task<List<formularz_pytanie_opcja>> PobierzOpcjeFormularza(int for_id);
}