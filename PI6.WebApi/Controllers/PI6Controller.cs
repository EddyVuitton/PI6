using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Helpers;
using PI6.WebApi.Repositories;
using System.Text;

namespace PI6.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PI6Controller : Controller
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IConfiguration _configuration;

    public PI6Controller(IApplicationRepository applicationRepository, IConfiguration configuration)
    {
        _applicationRepository = applicationRepository;
        _configuration = configuration;
    }

    [HttpGet("GetForms")]
    public async Task<ActionResult<List<formularz>>> GetForms()
    {
        return Ok(await _applicationRepository.GetForms());
    }

    [HttpGet("GetForm")]
    public async Task<ActionResult<List<formularz>>> GetForm(int for_id)
    {
        return Ok(await _applicationRepository.GetForm(for_id));
    }

    [HttpGet("GetFormType")]
    public async Task<ActionResult<List<formularz_typ>>> GetFormType()
    {
        return Ok(await _applicationRepository.GetFormType());
    }

    [HttpGet("GetFormTileDto")]
    public async Task<ActionResult<List<FormularzKafelekDto>>> GetFormTileDto()
    {
        return Ok(await _applicationRepository.GetFormTileDto());
    }

    [HttpPost("CreateForm")]
    public async Task CreateForm(FormularzDto form)
    {
        await _applicationRepository.CreateForm(form);
    }

    [HttpGet("GetFormQuestions")]
    public async Task<ActionResult<List<formularz_pytanie>>> GetFormQuestions(int for_id)
    {
        return Ok(await _applicationRepository.GetFormQuestions(for_id));
    }

    [HttpGet("GetFormOptions")]
    public async Task<ActionResult<List<formularz_pytanie_opcja>>> GetFormOptions(int for_id)
    {
        return Ok(await _applicationRepository.GetFormOptions(for_id));
    }

    [HttpPost("SaveSolvedForm")]
    public async Task SaveSolvedForm(FormularzPodejscieDto solvedForm)
    {
        await _applicationRepository.SaveSolvedForm(solvedForm);
    }

    [HttpGet("GetAccountTypes")]
    public async Task<ActionResult<List<account_type>>> GetAccountTypes()
    {
        return Ok(await _applicationRepository.GetAccountTypes());
    }

    [HttpPost("CreateAccount")]
    public async Task CreateAccount(account account)
    {
        await _applicationRepository.CreateAccount(account);
    }

    [HttpPost("Login")]
    public ActionResult<UserToken> Login(account account)
    {
        var result = false;
        var dbAccountPassword = _applicationRepository.GetAccountHashedPassword(account);
        
        if (AccountHelper.HashPassword(account.us_pass) == dbAccountPassword)
            result = true;

        if (result)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var accountDtoInfo = _applicationRepository.GetAccountByEmail(account.us_email);
            return AccountHelper.BuildToken(accountDtoInfo, key);
        }
        else
            return BadRequest("Invalid login attempt");
    }
}