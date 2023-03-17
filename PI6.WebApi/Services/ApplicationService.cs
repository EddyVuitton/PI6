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

    public async Task<List<formularz>> PobierzFormularze()
    {
        return await _httpClient.GetFromJsonAsync<List<formularz>>("api/pi6/PobierzFormularze") ?? new List<formularz>();
    }

    public async Task<List<formularz>> PobierzFormularz(int for_id)
    {
        return await _httpClient.GetFromJsonAsync<List<formularz>>($"api/pi6/PobierzFormularz?for_id={for_id}") ?? new List<formularz>();
    }

    public async Task<List<formularz_typ>> PobierzFormularzTyp()
    {
        return await _httpClient.GetFromJsonAsync<List<formularz_typ>>("api/pi6/PobierzFormularzTyp") ?? new List<formularz_typ>();
    }

    public async Task<List<FormularzKafelekDto>> PobierzFormularzKafelekDto()
    {
        return await _httpClient.GetFromJsonAsync<List<FormularzKafelekDto>>("api/pi6/PobierzFormularzKafelekDto") ?? new List<FormularzKafelekDto>();
    }

    public async Task ZapiszFormularz(FormularzDto form)
    {
        var json = JsonConvert.SerializeObject(form);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        await _httpClient.PostAsync("api/pi6/ZapiszFormularz", data);
    }

    public async Task<List<formularz_pytanie>> PobierzPytaniaFormularza(int for_id)
    {
        return await _httpClient.GetFromJsonAsync<List<formularz_pytanie>>($"api/pi6/PobierzPytaniaFormularza?for_id={for_id}") ?? new List<formularz_pytanie>();
    }

    public async Task<List<formularz_pytanie_opcja>> PobierzOpcjeFormularza(int for_id)
    {
        return await _httpClient.GetFromJsonAsync<List<formularz_pytanie_opcja>>($"api/pi6/PobierzOpcjeFormularza?for_id={for_id}") ?? new List<formularz_pytanie_opcja>();
    }
}