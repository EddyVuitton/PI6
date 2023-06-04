using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
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
        var response = await _httpClient.GetAsync("api/pi6/GetForms");
        
        if (!response.IsSuccessStatusCode)
            return new List<formularz>();

        var responseContent = await response.Content.ReadAsStringAsync();
        var deserialisedResponse = JsonConvert.DeserializeObject<List<formularz>>(responseContent);
        
        return deserialisedResponse;
    }

    public async Task<formularz> GetForm(int for_id)
    {
        var response = await _httpClient.GetAsync($"api/pi6/GetForm?for_id={for_id}");

        if (!response.IsSuccessStatusCode)
            return new formularz();

        try
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var deserialisedResponse = JsonConvert.DeserializeObject<formularz>(responseContent);

            return deserialisedResponse;
        }
        catch (Exception e)
        {
            //ToDo - logowanie
            return new formularz();
        }
    }

    public async Task<List<formularz_typ>> GetFormType()
    {
        var response = await _httpClient.GetAsync("api/pi6/GetFormType");

        if (!response.IsSuccessStatusCode)
            return new List<formularz_typ>();

        var responseContent = await response.Content.ReadAsStringAsync();
        var deserialisedResponse = JsonConvert.DeserializeObject<List<formularz_typ>>(responseContent);
        
        return deserialisedResponse;
    }

    public async Task<List<FormularzKafelekDto>> GetFormTileDto()
    {
        var response = await _httpClient.GetAsync("api/pi6/GetFormTileDto");

        if (!response.IsSuccessStatusCode)
            return new List<FormularzKafelekDto>();

        var responseContent = await response.Content.ReadAsStringAsync();
        var deserialisedResponse = JsonConvert.DeserializeObject<List<FormularzKafelekDto>>(responseContent);
        
        return deserialisedResponse;
    }

    public async Task<HttpResponseMessage> CreateForm(FormularzDto form)
    {
        var json = JsonConvert.SerializeObject(form);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/pi6/CreateForm", data);

        return response;  
    }

    public async Task<List<formularz_pytanie>> GetFormQuestions(int for_id)
    {
        var response = await _httpClient.GetAsync($"api/pi6/GetFormQuestions?for_id={for_id}");

        if (!response.IsSuccessStatusCode)
            return new List<formularz_pytanie>();

        var responseContent = await response.Content.ReadAsStringAsync();
        var deserialisedResponse = JsonConvert.DeserializeObject<List<formularz_pytanie>>(responseContent);
        
        return deserialisedResponse;
    }

    public async Task<List<formularz_pytanie_opcja>> GetFormOptions(int for_id)
    {
        var response = await _httpClient.GetAsync($"api/pi6/GetFormOptions?for_id={for_id}");

        if (!response.IsSuccessStatusCode)
            return new List<formularz_pytanie_opcja>();

        var responseContent = await response.Content.ReadAsStringAsync();
        var deserialisedResponse = JsonConvert.DeserializeObject<List<formularz_pytanie_opcja>>(responseContent);
        
        return deserialisedResponse;
    }

    public async Task<HttpResponseMessage> SaveSolvedForm(FormularzPodejscieDto solvedForm)
    {
        var json = JsonConvert.SerializeObject(solvedForm);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/pi6/SaveSolvedForm", data);

        return response;
    }

    public async Task<List<account_type>> GetAccountTypes()
    {
        var response = await _httpClient.GetAsync("api/pi6/GetAccountTypes");

        if (!response.IsSuccessStatusCode)
            return new List<account_type>();

        var responseContent = await response.Content.ReadAsStringAsync();
        var deserialisedResponse = JsonConvert.DeserializeObject<List<account_type>>(responseContent);
        
        return deserialisedResponse;
    }

    public async Task<HttpResponseMessage> CreateAccount(account account)
    {
        var json = JsonConvert.SerializeObject(account);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/pi6/CreateAccount", data);

        return response;
    }

    public async Task<AccountDto> GetAccountDtoByEmail(string email)
    {
        var tempAccount = new account() { us_email = email };
        var json = JsonConvert.SerializeObject(tempAccount);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"api/pi6/GetAccountDtoByEmail", data);

        if (!response.IsSuccessStatusCode)
            return new AccountDto();

        var responseContent = await response.Content.ReadAsStringAsync();
        var deserialisedResponse = JsonConvert.DeserializeObject<AccountDto>(responseContent);
       
        return deserialisedResponse;
    }

    public async Task<account> GetAccount(int id)
    {
        var response = await _httpClient.GetAsync($"api/pi6/GetAccount?id={id}");

        if (!response.IsSuccessStatusCode)
            return new account();

        var responseContent = await response.Content.ReadAsStringAsync();
        var deserialisedResponse = JsonConvert.DeserializeObject<account>(responseContent);

        return deserialisedResponse;
    }

    public async Task<UserToken> Login(account account)
    {
        var json = JsonConvert.SerializeObject(account);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = _httpClient.PostAsync("api/pi6/Login", data);

        if (!response.IsCompletedSuccessfully)
            return new UserToken();

        var responseContent = await response.Result.Content.ReadAsStringAsync();
        var deserialisedResponse = JsonConvert.DeserializeObject<UserToken>(responseContent);

        return deserialisedResponse;
    }

    public async Task<List<student_group>> GetStudentGroups(int us_id)
    {
        var response = await _httpClient.GetAsync($"api/pi6/GetStudentGroups?us_id={us_id}");

        if (!response.IsSuccessStatusCode)
            return new List<student_group>();

        var responseContent = await response.Content.ReadAsStringAsync();
        var deserialisedResponse = JsonConvert.DeserializeObject<List<student_group>>(responseContent);

        return deserialisedResponse;
    }

    public async Task<List<StudentGroupMapDto>> GetStudentGroupMapDto(int us_id)
    {
        var response = await _httpClient.GetAsync($"api/pi6/GetStudentGroupMapDto?us_id={us_id}");

        if (!response.IsSuccessStatusCode)
            return new List<StudentGroupMapDto>();

        var responseContent = await response.Content.ReadAsStringAsync();
        var deserialisedResponse = JsonConvert.DeserializeObject<List<StudentGroupMapDto>>(responseContent);

        return deserialisedResponse;
    }

    public async Task<List<formularz>> GetAccountForms(int us_id)
    {
        var response = await _httpClient.GetAsync($"api/pi6/GetAccountForms?us_id={us_id}");

        if (!response.IsSuccessStatusCode)
            return new List<formularz>();

        var responseContent = await response.Content.ReadAsStringAsync();
        var deserialisedResponse = JsonConvert.DeserializeObject<List<formularz>>(responseContent);

        return deserialisedResponse;
    }

    public async Task<List<formularz_podejscie>> GetFormApproaches(int for_id)
    {
        var response = await _httpClient.GetAsync($"api/pi6/GetFormApproaches?for_id={for_id}");

        if (!response.IsSuccessStatusCode)
            return new List<formularz_podejscie>();

        var responseContent = await response.Content.ReadAsStringAsync();
        var deserialisedResponse = JsonConvert.DeserializeObject<List<formularz_podejscie>>(responseContent);

        return deserialisedResponse;
    }

    public async Task<List<group_assigned_forms>> GetGroupAssignedForms(int us_id)
    {
        var response = await _httpClient.GetAsync($"api/pi6/GetGroupAssignedForms?us_id={us_id}");

        if (!response.IsSuccessStatusCode)
            return new List<group_assigned_forms>();

        var responseContent = await response.Content.ReadAsStringAsync();
        var deserialisedResponse = JsonConvert.DeserializeObject<List<group_assigned_forms>>(responseContent);

        return deserialisedResponse;
    }

    public async Task<HttpResponseMessage> SaveGroupAssignedForms(List<GroupAssignedFormCheckDto> groupAssignedFormCheckDtos)
    {
        var json = JsonConvert.SerializeObject(groupAssignedFormCheckDtos);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/pi6/SaveGroupAssignedForms", data);

        return response;
    }
}