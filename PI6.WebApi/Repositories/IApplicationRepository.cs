using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;

namespace PI6.WebApi.Repositories;

public interface IApplicationRepository
{
    public Task<List<formularz>> PobierzFormularze();
    public Task<List<formularz_typ>> PobierzFormularzTyp();
    public Task<List<FormularzKafelekDto>> PobierzFormularzKafelekDto();
    public Task ZapiszFormularz(FormularzDto form);
}