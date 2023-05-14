using Microsoft.EntityFrameworkCore;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.Shared.DataSource;
using PI6.WebApi.Extensions;
using PI6.WebApi.Helpers;

namespace PI6.WebApi.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly DBContext _context;

    public ApplicationRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<List<formularz>> GetForms()
    {
        return await _context.SqlQueryAsync<formularz>("exec dbo.p_formularz_pobierz null");
    }

    public async Task<List<formularz>> GetForm(int for_id)
    {
        SqlParam sqlParams = new();
        sqlParams.AddParam("for_id", for_id, System.Data.SqlDbType.Int);
        var param = sqlParams.Params();

        return await _context.SqlQueryAsync<formularz>("exec dbo.p_formularz_pobierz @for_id", param, default);
    }

    public async Task<List<formularz_typ>> GetFormType()
    {
        return await _context.SqlQueryAsync<formularz_typ>("exec dbo.p_formularz_typ_pobierz");
    }

    public async Task<List<FormularzKafelekDto>> GetFormTileDto()
    {
        return await _context.SqlQueryAsync<FormularzKafelekDto>("exec dbo.p_formularz_kafelek_pobierz");
    }

    public async Task CreateForm(FormularzDto form)
    {
        var xml = FormHelper.GenerateXml(form);

        SqlParam sqlParams = new();
        sqlParams.AddParam("xml", xml, System.Data.SqlDbType.Xml);
        var param = sqlParams.Params();

        await _context.SqlQueryAsync("exec dbo.p_formularz_zapisz @xml", param, default);
    }

    public async Task<List<formularz_pytanie>> GetFormQuestions(int for_id)
    {
        SqlParam sqlParams = new();
        sqlParams.AddParam("for_id", for_id, System.Data.SqlDbType.Int);
        var param = sqlParams.Params();

        return await _context.SqlQueryAsync<formularz_pytanie>("exec dbo.p_pobierz_pytania @for_id", param, default);
    }

    public async Task<List<formularz_pytanie_opcja>> GetFormOptions(int for_id)
    {
        SqlParam sqlParams = new();
        sqlParams.AddParam("for_id", for_id, System.Data.SqlDbType.Int);
        var param = sqlParams.Params();

        return await _context.SqlQueryAsync<formularz_pytanie_opcja>("exec dbo.p_pobierz_opcje @for_id", param, default);
    }

    public async Task SaveSolvedForm(FormularzPodejscieDto solvedForm)
    {
        var xml = FormHelper.GenerateXmlGeneric(solvedForm, "FormularzPodejscie");

        SqlParam sqlParams = new();
        sqlParams.AddParam("xml", xml, System.Data.SqlDbType.Xml);
        var param = sqlParams.Params();

        await _context.SqlQueryAsync("exec dbo.p_formularz_podejscie_zapisz @xml", param, default);
    }

    public async Task<List<account_type>> GetAccountTypes()
    {
        return await _context.SqlQueryAsync<account_type>("exec p_account_type_get");
    }

    public async Task CreateAccount(account account)
    {
        SqlParam sqlParams = new();
        sqlParams.AddParam("name", account.us_name, System.Data.SqlDbType.NVarChar);
        sqlParams.AddParam("surname", account.us_surname, System.Data.SqlDbType.NVarChar);
        sqlParams.AddParam("email", account.us_email, System.Data.SqlDbType.NVarChar);
        sqlParams.AddParam("pass", AccountHelper.HashPassword(account.us_pass), System.Data.SqlDbType.NVarChar);
        sqlParams.AddParam("ust_id", account.us_ust_id, System.Data.SqlDbType.Int);
        sqlParams.AddParam("activate", account.us_activate, System.Data.SqlDbType.DateTime);
        sqlParams.AddParam("deactivate", account.us_deactivate, System.Data.SqlDbType.DateTime);
        var param = sqlParams.Params();

        await _context.SqlQueryAsync("exec dbo.p_account_add @name, @surname, @email, @pass, @ust_id, @activate, @deactivate", param, default);
    }

    public async Task<string> GetAccountHashedPassword(account account)
    {
        SqlParam sqlParam = new();
        sqlParam.AddParam("us_id", account.us_id <= 0 ? null : account.us_id, System.Data.SqlDbType.Int);
        sqlParam.AddParam("email", account.us_email, System.Data.SqlDbType.NVarChar);
        var param = sqlParam.Params();

        var accounts = await _context.SqlQueryAsync<account>($"exec p_account_get @us_id, @email", param, default);

        return accounts.FirstOrDefault().us_pass;
    }

    public async Task<AccountDto> GetAccountDtoByEmail(string email)
    {
        SqlParam sqlParam = new();
        sqlParam.AddParam("us_id", null, System.Data.SqlDbType.Int);
        sqlParam.AddParam("email", email, System.Data.SqlDbType.NVarChar);
        var param = sqlParam.Params();

        var accounts = await _context.SqlQueryAsync<account>($"exec p_account_get @us_id, @email", param, default);
        var accountDto =
            from account in accounts
            join accountType in _context.account_type on account.us_ust_id equals accountType.ust_id
            select new AccountDto
            {
                UserId = account.us_id,
                UserEmail = account.us_email,
                UstId = accountType.ust_id,
                UstName = accountType.ust_name
            };

        return accountDto.FirstOrDefault();
    }

    public async Task<account> GetAccount(int id)
    {
        var acc =
            from account in _context.account
            where account.us_id == id
            select account;

        return await acc.FirstOrDefaultAsync();
    }

    public async Task<List<student_group>> GetStudentGroups(int us_id)
    {
        SqlParam sqlParams = new();
        sqlParams.AddParam("us_id", us_id, System.Data.SqlDbType.Int);
        var param = sqlParams.Params();

        return await _context.SqlQueryAsync<student_group>($"exec p_student_groups_get @us_id", param, default);
    }

    public async Task<List<StudentGroupMapDto>> GetStudentGroupMapDto(int us_id)
    {
        SqlParam sqlParams = new();
        sqlParams.AddParam("us_id", us_id, System.Data.SqlDbType.Int);
        var param = sqlParams.Params();
        var studentGroupMapDto = await _context.SqlQueryAsync<StudentGroupMapDto>($"exec p_student_group_map_get @us_id", param, default) ?? new List<StudentGroupMapDto>();

        return studentGroupMapDto;
    }
}