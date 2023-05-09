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
}