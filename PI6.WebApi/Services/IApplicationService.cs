using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;

namespace PI6.WebApi.Services;

public interface IApplicationService
{
    Task<List<formularz>> PobierzFormularze();
    Task<List<formularz_typ>> PobierzFormularzeTyp();
    Task<List<FormularzKafelekDto>> PobierzFormularzKafelekDto();
    Task ZapiszFormularz(FormularzDto form);
}