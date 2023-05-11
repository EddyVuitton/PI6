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
}