using PI6.Shared.Entities;

namespace PI6.WebApi.Data.Interfaces;

public interface IFormularzTypRepository
{
    Task<List<formularz_typ>> GetFormularzTyp();
    Task<formularz_typ> GetFormularzTypById(int id);
    Task CreateFormularzTyp(formularz_typ formularzTyp);
    Task<formularz_typ> UpdateFormularzTyp(formularz_typ formularzTyp);
    Task DeleteFormularzTyp(int id);
}