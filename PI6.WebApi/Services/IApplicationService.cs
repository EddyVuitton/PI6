using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;

namespace PI6.WebApi.Services;

public interface IApplicationService
{
    public Task<List<formularz>> GetForms();
    public Task<formularz> GetForm(int for_id);
    public Task<List<formularz_typ>> GetFormType();
    public Task<List<FormularzKafelekDto>> GetFormTileDto();
    public Task<HttpResponseMessage> CreateForm(FormularzDto form);
    public Task<List<formularz_pytanie>> GetFormQuestions(int for_id);
    public Task<List<formularz_pytanie_opcja>> GetFormOptions(int for_id);
    public Task<HttpResponseMessage> SaveSolvedForm(FormularzPodejscieDto solvedForm);
    public Task<List<account_type>> GetAccountTypes();
    public Task<HttpResponseMessage> CreateAccount(account account);
    public Task<UserToken> Login(account account);
    public Task<AccountDto> GetAccountDtoByEmail(string email);
    public Task<account> GetAccount(int id);
    public Task<List<student_group>> GetStudentGroups(int us_id);
    public Task<List<StudentGroupMapDto>> GetStudentGroupMapDto(int us_id);
    public Task<List<formularz>> GetAccountForms(int us_id);
    public Task<List<formularz_podejscie>> GetFormApproaches(int for_id);
    public Task<List<group_assigned_forms>> GetGroupAssignedForms(int us_id);
    public Task<HttpResponseMessage> SaveGroupAssignedForms(List<GroupAssignedFormCheckDto> groupAssignedFormCheckDtos);
}