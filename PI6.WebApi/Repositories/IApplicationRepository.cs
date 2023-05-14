using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;

namespace PI6.WebApi.Repositories;

public interface IApplicationRepository
{
    public Task<List<formularz>> GetForms();
    public Task<List<formularz>> GetForm(int for_id);
    public Task<List<formularz_typ>> GetFormType();
    public Task<List<FormularzKafelekDto>> GetFormTileDto();
    public Task CreateForm(FormularzDto form);
    public Task<List<formularz_pytanie>> GetFormQuestions(int for_id);
    public Task<List<formularz_pytanie_opcja>> GetFormOptions(int for_id);
    public Task SaveSolvedForm(FormularzPodejscieDto solvedForm);
    public Task<List<account_type>> GetAccountTypes();
    public Task CreateAccount(account account);
    public Task<string> GetAccountHashedPassword(account account);
    public Task<AccountDto> GetAccountDtoByEmail(string email);
    public Task<account> GetAccount(int id);
    public Task<List<student_group>> GetStudentGroups(int us_id);
    public Task<List<StudentGroupMapDto>> GetStudentGroupMapDto(int us_id);
    public Task<List<formularz>> GetAccountForms(int us_id);
    public Task<List<formularz_podejscie>> GetFormApproaches(int for_id);
}