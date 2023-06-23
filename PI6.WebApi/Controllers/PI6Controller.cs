using Microsoft.AspNetCore.Mvc;
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
        try
        {
            var result = await _applicationRepository.GetForms();
            return Ok(result);
        }
        catch (Exception e)
        {
            //ExceptionHelper.PrintException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetForm")]
    public async Task<ActionResult<formularz>> GetForm(int for_id)
    {
        try
        {
            var result = await _applicationRepository.GetForm(for_id);
            return Ok(result);
        }
        catch (Exception e)
        {
            //ExceptionHelper.PrintException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetFormType")]
    public async Task<ActionResult<List<formularz_typ>>> GetFormType()
    {
        try
        {
            var result = await _applicationRepository.GetFormType();
            return Ok(result);
        }
        catch (Exception e)
        {
            //ExceptionHelper.PrintException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetFormTileDto")]
    public async Task<ActionResult<List<FormularzKafelekDto>>> GetFormTileDto()
    {
        try
        {
            var result = await _applicationRepository.GetFormTileDto();
            return Ok(result);
        }
        catch (Exception e)
        {
            //ExceptionHelper.PrintException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpPost("CreateForm")]
    public async Task<ActionResult> CreateForm(FormularzDto form)
    {
        try
        {
            await _applicationRepository.CreateForm(form);
            return Ok();
        }
        catch (Exception e)
        {
            //ExceptionHelper.PrintException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetFormQuestions")]
    public async Task<ActionResult<List<formularz_pytanie>>> GetFormQuestions(int for_id)
    {
        try
        {
            var result = await _applicationRepository.GetFormQuestions(for_id);
            return Ok(result);
        }
        catch (Exception e)
        {
            //ExceptionHelper.PrintException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetFormOptions")]
    public async Task<ActionResult<List<formularz_pytanie_opcja>>> GetFormOptions(int for_id)
    {
        try
        {
            var result = await _applicationRepository.GetFormOptions(for_id);
            return Ok(result);
        }
        catch (Exception e)
        {
            //ExceptionHelper.PrintException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpPost("SaveSolvedForm")]
    public async Task<ActionResult> SaveSolvedForm(FormularzPodejscieDto solvedForm)
    {
        try
        {
            await _applicationRepository.SaveSolvedForm(solvedForm);
            return Ok();
        }
        catch (Exception e)
        {
            //ExceptionHelper.PrintException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetAccountTypes")]
    public async Task<ActionResult<List<account_type>>> GetAccountTypes()
    {
        try
        {
            var result = await _applicationRepository.GetAccountTypes();
            return Ok(result);
        }
        catch (Exception e)
        {
            //ExceptionHelper.PrintException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpPost("CreateAccount")]
    public async Task<ActionResult> CreateAccount(account account)
    {
        try
        {
            await _applicationRepository.CreateAccount(account);
            return Ok();
        }
        catch (Exception e)
        {
            //ExceptionHelper.PrintException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpPost("GetAccountDtoByEmail")]
    public async Task<ActionResult<AccountDto>> GetAccountDtoByEmail(account account)
    {
        try
        {
            var result = await _applicationRepository.GetAccountDtoByEmail(account.us_email);
            return Ok(result);
        }
        catch (Exception e)
        {
            //ExceptionHelper.PrintException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetAccount")]
    public async Task<ActionResult<account>> GetAccount(int id)
    {
        try
        {
            var result = await _applicationRepository.GetAccount(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            //ExceptionHelper.PrintException(e);
            return BadRequest(e.Message);
        }
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
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
                var accountDtoInfo = await _applicationRepository.GetAccountDtoByEmail(account.us_email);
                var token = AccountHelper.BuildToken(accountDtoInfo, key);
                return Ok(token);
            }
            catch (Exception e)
            {
                //ExceptionHelper.PrintException(e);
                return BadRequest(e.Message);
            }
        }
        else
            return BadRequest("Invalid login attempt");
    }

    [HttpGet("GetStudentGroups")]
    public async Task<ActionResult<List<student_group>>> GetStudentGroups(int us_id)
    {
        try
        {
            var result = await _applicationRepository.GetStudentGroups(us_id);
            return Ok(result);
        }
        catch (Exception e)
        {
            //ExceptionHelper.PrintException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetStudentGroupMapDto")]
    public async Task<ActionResult<List<StudentGroupMapDto>>> GetStudentGroupMapDto(int us_id)
    {
        try
        {
            var result = await _applicationRepository.GetStudentGroupMapDto(us_id);
            return Ok(result);
        }
        catch (Exception e)
        {
            //ExceptionHelper.PrintException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetAccountForms")]
    public async Task<ActionResult<List<formularz>>> GetAccountForms(int us_id)
    {
        try
        {
            var result = await _applicationRepository.GetAccountForms(us_id);
            return Ok(result);
        }
        catch (Exception e)
        {
            //ExceptionHelper.PrintException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetFormApproaches")]
    public async Task<ActionResult<List<formularz_podejscie>>> GetFormApproaches(int for_id)
    {
        try
        {
            var result = await _applicationRepository.GetFormApproaches(for_id);
            return Ok(result);
        }
        catch (Exception e)
        {
            //ExceptionHelper.PrintException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetGroupAssignedForms")]
    public async Task<ActionResult<List<group_assigned_forms>>> GetGroupAssignedForms(int us_id)
    {
        try
        {
            var result = await _applicationRepository.GetGroupAssignedForms(us_id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("SaveGroupAssignedForms")]
    public async Task<ActionResult> SaveGroupAssignedForms(List<GroupAssignedFormCheckDto> groupAssignedFormCheckDtos)
    {
        try
        {
            await _applicationRepository.SaveGroupAssignedForms(groupAssignedFormCheckDtos);
            return Ok();
        }
        catch (Exception e)
        {
            //ExceptionHelper.PrintException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpPost("SaveFormDates")]
    public async Task<ActionResult> SaveFormDates(FormDatesDto dto)
    {
        try
        {
            await _applicationRepository.SaveFormDates(dto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetFormResultDto")]
    public async Task<ActionResult<List<FormResultDto>>> GetFormResultDto(int for_id)
    {
        try
        {
            var result = await _applicationRepository.GetFormResultDto(for_id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}