using webapi.Models.Domain;

namespace webapi.Repositories.Interface
{
    public interface IArticuloRepository
    {
        Task<Articulo> CreateAsync(Articulo articulo);
        //Task<IEnumerable<object>> GetAllAsync();
        Task<List<Articulo>> GetAllAsync();
    }
}
