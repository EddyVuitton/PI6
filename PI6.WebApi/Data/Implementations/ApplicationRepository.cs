using PI6.WebApi.Data.Interfaces;

namespace PI6.WebApi.Data.Implementations;

public class ApplicationRepository : IApplicationRepository
{
    private readonly DBContext _context;

    public ApplicationRepository(DBContext context)
    {
        _context = context;
    }
}