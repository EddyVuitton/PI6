using PI6.Shared.Data.Dtos;

namespace PI6.WebApi.Data;

public interface IApplicationRepository
{
    public Task<List<FormularzDto>> PobierzFormularzeDto();
}