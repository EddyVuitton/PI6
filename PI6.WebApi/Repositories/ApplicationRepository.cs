using PI6.Shared.DataSource;
using PI6.Shared.Dtos;
using PI6.WebApi.Extensions;

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
}