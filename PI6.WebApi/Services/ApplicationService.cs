using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Temp;
using System;
using System.Net.Http.Headers;
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

    public async Task ZapiszFormularz(Model1 model1)
    {
        //var json = JsonConvert.SerializeObject(model1, Formatting.None);
        //var stringContent = new StringContent(json);
        //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //var response = await _httpClient.PostAsJsonAsync("api/pi6/ZapiszFormularz", stringContent);
        //var responseString = await response.Content.ReadAsStringAsync();

        var json = JsonConvert.SerializeObject(model1);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/pi6/ZapiszFormularz", data);
        var result = await response.Content.ReadAsStringAsync();
    }
}