using PI6.Shared.Entities;

namespace PI6.WebApi.Services;

public class ApplicationService : IApplicationService
{
    private readonly HttpClient _httpClient;

    public ApplicationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<formularz_typ>> GetAllFormularzTyp()
    {
        try
        {
            var typyFormularzy = await _httpClient.GetFromJsonAsync<IEnumerable<formularz_typ>>("api/pi6/Test");

            return typyFormularzy;
        }
        catch (Exception)
        {
            return null;
        }
    }
}