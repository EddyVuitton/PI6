using PI6.Shared.Data.Dtos;

namespace PI6.WebApi.Repositories;

public interface IApplicationRepository
{
    public Task<List<FormularzDto>> PobierzFormularzeDto();
    public Task ZapiszFormularz(FormularzDto form);
}