using Microsoft.EntityFrameworkCore;
using PI6.Shared.Entities;
using PI6.WebApi.Data.Interfaces;

namespace PI6.WebApi.Data.Implementations
{
    public class FormularzTypRepository : IFormularzTypRepository
    {
        private readonly DBContext _context;

        public FormularzTypRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<List<formularz_typ>> GetFormularzTyp()
        {
            return await _context.formularz_typ.ToListAsync();
        }

        public async Task<formularz_typ> GetFormularzTypById(int id)
        {
            return await _context.formularz_typ.FirstOrDefaultAsync(x => x.fort_id == id);
        }

        public Task CreateFormularzTyp(formularz_typ formularzTyp)
        {
            throw new NotImplementedException();
        }

        public Task<formularz_typ> UpdateFormularzTyp(formularz_typ formularzTyp)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFormularzTyp(int id)
        {
            throw new NotImplementedException();
        }
    }
}