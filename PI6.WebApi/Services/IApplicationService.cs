using PI6.Shared.Dtos;

namespace PI6.WebApi.Services;

public interface IApplicationService
{
    Task<IEnumerable<FormularzDto>> PobierzFormularzeDto();
}