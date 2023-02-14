using PI6.Shared.Data.Dtos;

namespace PI6.WebApi.Services;

public interface IApplicationService
{
    Task<IEnumerable<FormularzDto>> PobierzFormularzeDto();
}