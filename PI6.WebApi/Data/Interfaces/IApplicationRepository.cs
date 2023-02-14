using PI6.Shared.Dtos;

namespace PI6.WebApi.Data.Interfaces;

public interface IApplicationRepository
{
    public Task<List<FormularzDto>> PobierzFormularzeDto();
}