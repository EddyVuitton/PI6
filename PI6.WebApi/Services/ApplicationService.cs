using Newtonsoft.Json;
using PI6.Shared.Data.Dtos;
using System.Text;

namespace PI6.WebApi.Services;

public class ApplicationService : IApplicationService
{
    private readonly HttpClient _httpClient;

    public ApplicationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<FormularzDto>> PobierzFormularzeDto()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<FormularzDto>>("api/pi6/PobierzFormularzeDto");
    }

    public async Task ZapiszFormularz(FormularzDto form)
    {
        var json = JsonConvert.SerializeObject(form);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        await _httpClient.PostAsync("api/pi6/ZapiszFormularz", data);
    }

    public async Task ZapiszFormularz1(FormularzDto form)
    {
        var json = JsonConvert.SerializeObject(form);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        await _httpClient.PostAsync("api/pi6/ZapiszFormularz", data);
    }
}