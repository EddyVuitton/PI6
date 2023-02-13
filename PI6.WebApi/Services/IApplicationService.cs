using PI6.Shared.Entities;

namespace PI6.WebApi.Services;

public interface IApplicationService
{
    Task<IEnumerable<formularz_typ>> GetAllFormularzTyp();
}