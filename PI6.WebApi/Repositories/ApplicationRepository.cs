using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
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

    public async Task<List<formularz>> PobierzFormularze()
    {
        return await _context.SqlQueryAsync<formularz>("exec dbo.p_formularz_pobierz");
    }

    public async Task<List<formularz_typ>> PobierzFormularzTyp()
    {
        return await _context.SqlQueryAsync<formularz_typ>("exec dbo.p_formularz_typ_pobierz");
    }

    public async Task<List<FormularzKafelekDto>> PobierzFormularzKafelekDto()
    {
        return await _context.SqlQueryAsync<FormularzKafelekDto>("exec dbo.p_formularz_kafelek_pobierz");
    }

    public async Task ZapiszFormularz(FormularzDto form)
    {
        var xml = Formularz.GenerateXml(form);

        SqlParam sqlParams = new();
        sqlParams.AddParam("xml", xml, System.Data.SqlDbType.Xml);
        var param = sqlParams.Params();

        await _context.SqlQueryAsync("exec dbo.p_formularz_zapisz @xml", param, default);
    }
}