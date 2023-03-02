using Microsoft.Data.SqlClient;
using PI6.Shared.Data.Dtos;
using PI6.Shared.DataSource;
using PI6.WebApi.Extensions;
using PI6.WebApi.Helpers;

namespace PI6.WebApi.Repositories;

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

    public async Task ZapiszFormularz(FormularzDto form)
    {
        var xml = Formularz.GenerateXml(form);

        var param = new object[]
        {
            new SqlParameter()
            {
                ParameterName = "xml",
                Value = xml,
                SqlDbType = System.Data.SqlDbType.Xml
            }
        };

        await _context.SqlQueryAsync("exec dbo.p_zapisz_formularz @xml", param, default);
    }
}