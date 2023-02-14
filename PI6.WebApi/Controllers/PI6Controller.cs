using Microsoft.AspNetCore.Mvc;
using PI6.Shared.Dtos;
using PI6.WebApi.Data;

namespace PI6.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PI6Controller : Controller
{
    private readonly IApplicationRepository _applicationRepository;

    public PI6Controller
    (
        IApplicationRepository applicationRepository
    )
    {
        _applicationRepository = applicationRepository;
    }

    [HttpGet("PobierzFormularzeDto")]
    public async Task<ActionResult<IEnumerable<FormularzDto>>> PobierzFormularzeDto()
    {
        return Ok(await _applicationRepository.PobierzFormularzeDto());
    }
}