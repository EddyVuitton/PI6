using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Helpers;
using System.Text;

namespace PI6.WebApi.Services;

public class ApplicationService : IApplicationService
{
    private readonly HttpClient _httpClient;

    public ApplicationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<formularz>> GetForms()
    {
        return await _httpClient.GetFromJsonAsync<List<formularz>>("api/pi6/GetForms") ?? new List<formularz>();
    }

    public async Task<List<formularz>> GetForm(int for_id)
    {
        return await _httpClient.GetFromJsonAsync<List<formularz>>($"api/pi6/GetForm?for_id={for_id}") ?? new List<formularz>();
    }

    public async Task<List<formularz_typ>> GetFormType()
    {
        return await _httpClient.GetFromJsonAsync<List<formularz_typ>>("api/pi6/GetFormType") ?? new List<formularz_typ>();
    }

    public async Task<List<FormularzKafelekDto>> GetFormTileDto()
    {
        return await _httpClient.GetFromJsonAsync<List<FormularzKafelekDto>>("api/pi6/GetFormTileDto") ?? new List<FormularzKafelekDto>();
    }

    public async Task CreateForm(FormularzDto form)
    {
        var json = JsonConvert.SerializeObject(form);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        await _httpClient.PostAsync("api/pi6/CreateForm", data);
    }

    public async Task<List<formularz_pytanie>> GetFormQuestions(int for_id)
    {
        return await _httpClient.GetFromJsonAsync<List<formularz_pytanie>>($"api/pi6/GetFormQuestions?for_id={for_id}") ?? new List<formularz_pytanie>();
    }

    public async Task<List<formularz_pytanie_opcja>> GetFormOptions(int for_id)
    {
        return await _httpClient.GetFromJsonAsync<List<formularz_pytanie_opcja>>($"api/pi6/GetFormOptions?for_id={for_id}") ?? new List<formularz_pytanie_opcja>();
    }

    public async Task SaveSolvedForm(FormularzPodejscieDto solvedForm)
    {
        var json = JsonConvert.SerializeObject(solvedForm);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        await _httpClient.PostAsync("api/pi6/SaveSolvedForm", data);
    }

    public async Task<List<account_type>> GetAccountTypes()
    {
        return await _httpClient.GetFromJsonAsync<List<account_type>>("api/pi6/GetAccountTypes") ?? new List<account_type>();
    }

    public async Task CreateAccount(account account)
    {
        var json = JsonConvert.SerializeObject(account);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        await _httpClient.PostAsync("api/pi6/CreateAccount", data);
    }

    public async Task<AccountDto> GetAccountDtoByEmail(string email)
    {
        var tempAccount = new account() { us_email = email };
        var json = JsonConvert.SerializeObject(tempAccount);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = _httpClient.PostAsync($"api/pi6/GetAccountDtoByEmail", data);
        var accountDto = new AccountDto();

        if (response.Result.IsSuccessStatusCode)
        {
            var responseContent = await response.Result.Content.ReadAsStringAsync();
            accountDto = JsonConvert.DeserializeObject<AccountDto>(responseContent);
        }

        return accountDto;
    }

    public async Task<account> GetAccount(int id)
    {
        var response = _httpClient.GetAsync($"api/pi6/GetAccount?id={id}");
        var account = new account();

        if (response.Result.IsSuccessStatusCode)
        {
            var responseContent = await response.Result.Content.ReadAsStringAsync();
            account = JsonConvert.DeserializeObject<account>(responseContent);
        }

        return account;
    }

    public async Task<UserToken> Login(account account)
    {
        var json = JsonConvert.SerializeObject(account);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/pi6/Login", data);
        var userToken = new UserToken();

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            userToken = JsonConvert.DeserializeObject<UserToken>(responseContent);
        }
        
        return userToken;
    }

    public async Task<List<student_group>> GetStudentGroups(int us_id)
    {
        return await _httpClient.GetFromJsonAsync<List<student_group>>($"api/pi6/GetStudentGroups?us_id={us_id}") ?? new List<student_group>();
    }

    public async Task<List<StudentGroupMapDto>> GetStudentGroupMapDto(int us_id)
    {
        return await _httpClient.GetFromJsonAsync<List<StudentGroupMapDto>>($"api/pi6/GetStudentGroupMapDto?us_id={us_id}") ?? new List<StudentGroupMapDto>();
    }
    public async Task<List<formularz>> GetAccountForms(int us_id)
    {
        return await _httpClient.GetFromJsonAsync<List<formularz>>($"api/pi6/GetAccountForms?us_id={us_id}") ?? new List<formularz>();
    }

    public async Task<List<formularz_podejscie>> GetFormApproaches(int for_id)
    {
        return await _httpClient.GetFromJsonAsync<List<formularz_podejscie>>($"api/pi6/GetFormApproaches?for_id={for_id}") ?? new List<formularz_podejscie>();
    }
}