using Microsoft.AspNetCore.Mvc;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Repositories;

namespace PI6.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PI6Controller : Controller
{
    private readonly IApplicationRepository _applicationRepository;

    public PI6Controller(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    [HttpGet("PobierzFormularze")]
    public async Task<ActionResult<List<formularz>>> PobierzFormularze()
    {
        return Ok(await _applicationRepository.PobierzFormularze());
    }


    [HttpGet("PobierzFormularz")]
    public async Task<ActionResult<List<formularz>>> PobierzFormularz(int for_id)
    {
        return Ok(await _applicationRepository.PobierzFormularz(for_id));
    }

    [HttpGet("PobierzFormularzTyp")]
    public async Task<ActionResult<List<formularz_typ>>> PobierzFormularzTyp()
    {
        return Ok(await _applicationRepository.PobierzFormularzTyp());
    }

    [HttpGet("PobierzFormularzKafelekDto")]
    public async Task<ActionResult<List<FormularzKafelekDto>>> PobierzFormularzKafelekDto()
    {
        return Ok(await _applicationRepository.PobierzFormularzKafelekDto());
    }

    [HttpPost("ZapiszFormularz")]
    public async Task ZapiszFormularz(FormularzDto form)
    {
        await _applicationRepository.ZapiszFormularz(form);
    }

    [HttpGet("PobierzPytaniaFormularza")]
    public async Task<ActionResult<List<formularz_pytanie>>> PobierzPytaniaFormularza(int for_id)
    {
        return Ok(await _applicationRepository.PobierzPytaniaFormularza(for_id));
    }

    [HttpGet("PobierzOpcjeFormularza")]
    public async Task<ActionResult<List<formularz_pytanie_opcja>>> PobierzOpcjeFormularza(int for_id)
    {
        return Ok(await _applicationRepository.PobierzOpcjeFormularza(for_id));
    }
}