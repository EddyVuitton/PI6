using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Temp;

namespace PI6.WebApi.Data;

public interface IApplicationRepository
{
    public Task<List<FormularzDto>> PobierzFormularzeDto();
    public Task ZapiszFormularz(Model1 model1);
}