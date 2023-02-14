using PI6.Shared.Dtos;
using PI6.WebApi.Data.Interfaces;
using PI6.WebApi.Extensions;

namespace PI6.WebApi.Data.Implementations;

public class ApplicationRepository : IApplicationRepository
{
    private readonly DBContext _context;

    public ApplicationRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<List<FormularzDto>> PobierzFormularzeDto()
    {
        return await DbContextExtensions.SqlQueryAsync<FormularzDto>(_context, "exec dbo.p_pobierz_formularze", null, default);
    }
}