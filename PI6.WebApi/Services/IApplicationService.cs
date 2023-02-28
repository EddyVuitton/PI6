using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Temp;

namespace PI6.WebApi.Services;

public interface IApplicationService
{
    Task<IEnumerable<FormularzDto>> PobierzFormularzeDto();
    Task ZapiszFormularz(Model1 model1);
}