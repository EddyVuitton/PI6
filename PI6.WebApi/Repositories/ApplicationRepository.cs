using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Temp;
using PI6.Shared.DataSource;
using PI6.WebApi.Extensions;
using PI6.WebApi.Helpers;
using System.Data;

namespace PI6.WebApi.Data;

public class ApplicationRepository : IApplicationRepository
{
    private readonly DBContext _context;

    public ApplicationRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<List<FormularzDto>> PobierzFormularzeDto()
    {
        return await _context.SqlQueryAsync<FormularzDto>("exec dbo.p_pobierz_formularze", null, default);
    }

    public async Task ZapiszFormularz(Model1 model1)
    {
        var xml = Formularz.GenerateXml(model1.questions, model1.options);

        SqlParameter param = new()
        {
            ParameterName = "xml",
            Value = xml
        };

        object[] paramArray = { param };

        //await _context.SqlQueryAsync("exec dbo.p_zapisz_formularz", paramArray, default);
        var x = await _context.Database.ExecuteSqlAsync($"exec dbo.p_zapisz_formularz @xml = {xml}");

        var y = 5;
    }
}