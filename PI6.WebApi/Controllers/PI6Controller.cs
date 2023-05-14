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

    [HttpPost("GetAccountDtoByEmail")]
    public async Task<ActionResult<AccountDto>> GetAccountDtoByEmail(account account)
    {
        return Ok(await _applicationRepository.GetAccountDtoByEmail(account.us_email));
    }

    [HttpGet("GetAccount")]
    public async Task<ActionResult<account>> GetAccount(int id)
    {
        return Ok(await _applicationRepository.GetAccount(id));
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserToken>> Login(account account)
    {
        var result = false;
        var dbAccountPassword = await _applicationRepository.GetAccountHashedPassword(account);
        var hashedPassword = AccountHelper.HashPassword(account.us_pass);

        if (dbAccountPassword == hashedPassword)
            result = true;

        if (result)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var accountDtoInfo = await _applicationRepository.GetAccountDtoByEmail(account.us_email);
            return Ok(AccountHelper.BuildToken(accountDtoInfo, key));
        }
        else
            return BadRequest("Invalid login attempt");
    }

    [HttpGet("GetStudentGroups")]
    public async Task<ActionResult<List<student_group>>> GetStudentGroups(int us_id)
    {
        return Ok(await _applicationRepository.GetStudentGroups(us_id));
    }

    [HttpGet("GetStudentGroupMapDto")]
    public async Task<ActionResult<List<StudentGroupMapDto>>> GetStudentGroupMapDto(int us_id)
    {
        return Ok(await _applicationRepository.GetStudentGroupMapDto(us_id));
    }

    [HttpGet("GetAccountForms")]
    public async Task<ActionResult<List<formularz>>> GetAccountForms(int us_id)
    {
        return Ok(await _applicationRepository.GetAccountForms(us_id));
    }

    [HttpGet("GetFormApproaches")]
    public async Task<ActionResult<List<formularz_podejscie>>> GetFormApproaches(int for_id)
    {
        return Ok(await _applicationRepository.GetFormApproaches(for_id));
    }
}