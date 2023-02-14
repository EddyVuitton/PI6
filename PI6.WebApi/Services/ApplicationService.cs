using PI6.Shared.Dtos;

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
        try
        {
            var formularze = await _httpClient.GetFromJsonAsync<IEnumerable<FormularzDto>>("api/pi6/PobierzFormularzeDto");

            return formularze;
        }
        catch (Exception)
        {
            return null;
        }
    }
}