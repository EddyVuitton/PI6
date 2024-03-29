﻿using Microsoft.EntityFrameworkCore;
using PI6.Shared.Data;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
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
        return await _context.SqlQueryAsync<formularz>("exec dbo.p_formularz_pobierz null, null");
    }

    public async Task<formularz> GetForm(int for_id)
    {
        SqlParam sqlParams = new();
        sqlParams.AddParam("for_id", for_id, System.Data.SqlDbType.Int);
        var param = sqlParams.Params();
        var result = await _context.SqlQueryAsync<formularz>("exec dbo.p_formularz_pobierz @for_id", param);

        return result.FirstOrDefault();
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

        await _context.SqlQueryAsync("exec dbo.p_formularz_zapisz @xml", param);
    }

    public async Task<List<formularz_pytanie>> GetFormQuestions(int for_id)
    {
        SqlParam sqlParams = new();
        sqlParams.AddParam("for_id", for_id, System.Data.SqlDbType.Int);
        var param = sqlParams.Params();

        return await _context.SqlQueryAsync<formularz_pytanie>("exec dbo.p_pobierz_pytania @for_id", param);
    }

    public async Task<List<formularz_pytanie_opcja>> GetFormOptions(int for_id)
    {
        SqlParam sqlParams = new();
        sqlParams.AddParam("for_id", for_id, System.Data.SqlDbType.Int);
        var param = sqlParams.Params();

        return await _context.SqlQueryAsync<formularz_pytanie_opcja>("exec dbo.p_pobierz_opcje @for_id", param);
    }

    public async Task SaveSolvedForm(FormularzPodejscieDto solvedForm)
    {
        var xml = FormHelper.GenerateXmlGeneric(solvedForm, "FormularzPodejscie");

        SqlParam sqlParams = new();
        sqlParams.AddParam("xml", xml, System.Data.SqlDbType.Xml);
        var param = sqlParams.Params();

        await _context.SqlQueryAsync("exec dbo.p_formularz_podejscie_zapisz @xml", param);
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
        sqlParams.AddParam("pass", AuthHelper.HashPassword(account.us_pass), System.Data.SqlDbType.NVarChar);
        sqlParams.AddParam("ust_id", account.us_ust_id, System.Data.SqlDbType.Int);
        sqlParams.AddParam("activate", account.us_activate, System.Data.SqlDbType.DateTime);
        sqlParams.AddParam("deactivate", account.us_deactivate, System.Data.SqlDbType.DateTime);
        var param = sqlParams.Params();

        await _context.SqlQueryAsync("exec dbo.p_account_add @name, @surname, @email, @pass, @ust_id, @activate, @deactivate", param);
    }

    public async Task<string> GetAccountHashedPassword(account account)
    {
        SqlParam sqlParam = new();
        sqlParam.AddParam("us_id", account.us_id <= 0 ? null : account.us_id, System.Data.SqlDbType.Int);
        sqlParam.AddParam("email", account.us_email, System.Data.SqlDbType.NVarChar);
        var param = sqlParam.Params();

        var accounts = await _context.SqlQueryAsync<account>($"exec p_account_get @us_id, @email", param);

        return accounts.FirstOrDefault().us_pass;
    }

    public async Task<AccountDto> GetAccountDtoByEmail(string email)
    {
        SqlParam sqlParam = new();
        sqlParam.AddParam("us_id", null, System.Data.SqlDbType.Int);
        sqlParam.AddParam("email", email, System.Data.SqlDbType.NVarChar);
        var param = sqlParam.Params();

        var accounts = await _context.SqlQueryAsync<account>($"exec p_account_get @us_id, @email", param);
        var accountDto =
            from account in accounts
            join accountType in _context.account_type on account.us_ust_id equals accountType.ust_id
            where account.us_email == email
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

        return await _context.SqlQueryAsync<student_group>($"exec p_student_groups_get @us_id", param);
    }

    public async Task<List<StudentGroupMapDto>> GetStudentGroupMapDto(int us_id)
    {
        SqlParam sqlParams = new();
        sqlParams.AddParam("us_id", us_id, System.Data.SqlDbType.Int);
        var param = sqlParams.Params();
        var studentGroupMapDto = await _context.SqlQueryAsync<StudentGroupMapDto>($"exec p_student_group_map_get @us_id", param) ?? new List<StudentGroupMapDto>();

        return studentGroupMapDto;
    }

    public async Task<List<formularz>> GetAccountForms(int us_id)
    {
        SqlParam sqlParams = new();
        sqlParams.AddParam("us_id", us_id, System.Data.SqlDbType.Int);
        var param = sqlParams.Params();
        var forms = await _context.SqlQueryAsync<formularz>($"exec p_account_forms_get @us_id", param) ?? new List<formularz>();

        return forms;
    }

    public async Task<List<formularz_podejscie>> GetFormApproaches (int for_id)
    {
        SqlParam sqlParams = new();
        sqlParams.AddParam("for_id", for_id, System.Data.SqlDbType.Int);
        var param = sqlParams.Params();
        var approaches = await _context.SqlQueryAsync<formularz_podejscie>($"exec p_form_approach_get @for_id", param) ?? new List<formularz_podejscie>();

        return approaches;
    }

    public async Task<List<group_assigned_forms>> GetGroupAssignedForms(int us_id)
    {
        var gafs =
            from gaf in _context.group_assigned_forms
            where gaf.gaf_us_id == us_id
            select gaf;

        return await gafs.ToListAsync();
    }

    public async Task SaveGroupAssignedForms(List<GroupAssignedFormCheckDto> groupAssignedFormCheckDtos)
    {
        var usId = groupAssignedFormCheckDtos.FirstOrDefault().UsId;
        var forId = groupAssignedFormCheckDtos.FirstOrDefault().ForId;
        var allUserGafs = await _context.group_assigned_forms.Where(x => x.gaf_us_id == usId && x.gaf_for_id == forId).ToListAsync();

        await _context.Database.BeginTransactionAsync();

        try
        {
            SqlParam sqlParams = new();

            sqlParams.AddParam("us_id", usId, System.Data.SqlDbType.Int);
            sqlParams.AddParam("for_id", forId, System.Data.SqlDbType.Int);
            var param = sqlParams.Params();
            await _context.SqlQueryAsync($"exec p_delete_all_group_assigned_forms @us_id, @for_id", param);

            foreach (var gafc in groupAssignedFormCheckDtos.Where(x => x.Check))
            {
                sqlParams.ClearParams();

                sqlParams.AddParam("us_id", usId, System.Data.SqlDbType.Int);
                sqlParams.AddParam("sgr_id", gafc.GrpId, System.Data.SqlDbType.Int);
                sqlParams.AddParam("for_id", forId, System.Data.SqlDbType.Int);
                param = sqlParams.Params();
                await _context.SqlQueryAsync($"exec p_save_group_assigned_forms @us_id, @sgr_id, @for_id", param);
            }

            await _context.SaveChangesAsync();
            await _context.Database.CommitTransactionAsync();
        }
        catch
        {
            await _context.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task SaveFormDates(FormDatesDto dto)
    {
        await _context.Database.BeginTransactionAsync();

        try
        {
            SqlParam sqlParams = new();
            sqlParams.AddParam("for_id", dto.FormId, System.Data.SqlDbType.Int);
            sqlParams.AddParam("start", dto.StartDate, System.Data.SqlDbType.DateTime);
            sqlParams.AddParam("end", dto.EndDate, System.Data.SqlDbType.DateTime);
            await _context.SqlQueryAsync($"exec p_form_dates_set @for_id, @start, @end", sqlParams.Params());

            await _context.SaveChangesAsync();
            await _context.Database.CommitTransactionAsync();
        }
        catch
        {
            await _context.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<List<FormResultDto>> GetFormResultDto(int form_id)
    {
        SqlParam sqlParams = new();
        sqlParams.AddParam("for_id", form_id, System.Data.SqlDbType.Int);
        var param = sqlParams.Params();
        var result = await _context.SqlQueryAsync<FormResultDto>($"exec p_form_result_get @for_id", param) ?? new List<FormResultDto>();

        return result;
    }
    public async Task<formularz_podejscie> GetSolvedForm(int fpod_id)
    {
        SqlParam sqlParams = new();
        sqlParams.AddParam("fpod_id", fpod_id, System.Data.SqlDbType.Int);
        var param = sqlParams.Params();
        var result = await _context.SqlQueryAsync<formularz_podejscie>($"select \r\n\tfpod_id, fpod_us_id, fpod_for_id, fpod_data_rozpoczenia, fpod_stan, fpod_data_zakonczenia, fpod_wykorzystany_czas\r\nfrom formularz_podejscie\r\njoin formularz on for_id = fpod_for_id\r\nwhere fpod_id = @fpod_id", param);

        return result.FirstOrDefault();
    }

    public async Task<List<formularz_podejscie>> GetSolvedForms(int us_id)
    {
        SqlParam sqlParams = new();
        sqlParams.AddParam("us_id", us_id, System.Data.SqlDbType.Int);
        var param = sqlParams.Params();
        var result = await _context.SqlQueryAsync<formularz_podejscie>($"select \r\n\tfpod_id, fpod_us_id, fpod_for_id, fpod_data_rozpoczenia, fpod_stan, fpod_data_zakonczenia, fpod_wykorzystany_czas\r\nfrom formularz_podejscie\r\njoin formularz on for_id = fpod_for_id\r\nwhere fpod_us_id = @us_id", param) ?? new List<formularz_podejscie>();

        return result;
    }

    public async Task<List<formularz_podejscie_odpowiedz>> GetSolvedFormsAnswers(int fpod_id)
    {
        SqlParam sqlParams = new();
        sqlParams.AddParam("fpod_id", fpod_id, System.Data.SqlDbType.Int);
        var param = sqlParams.Params();
        var result = await _context.SqlQueryAsync<formularz_podejscie_odpowiedz>($"select *\r\nfrom formularz_podejscie_odpowiedz\r\nwhere fodp_fpod_id = @fpod_id", param) ?? new List<formularz_podejscie_odpowiedz>();

        return result;
    }
}