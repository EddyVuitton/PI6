using Microsoft.AspNetCore.Mvc;
using PI6.Shared.Entities;
using PI6.WebApi.Data.Interfaces;

namespace PI6.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PI6Controller : Controller
{
    private readonly IFormularzTypRepository _formularzTypRepository;

    public PI6Controller
    (
        IFormularzTypRepository formularzTypRepository
    )
    {
        _formularzTypRepository = formularzTypRepository;
    }

    [HttpGet("Test")]
    public async Task<ActionResult<IEnumerable<formularz_typ>>> Test()
    {
        return Ok(await _formularzTypRepository.GetFormularzTyp());
    }
}