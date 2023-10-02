using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models.Domain;
using webapi.Repositories.Interface;

namespace webapi.Repositories.Implementation
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly ApplicationDBContext dBContext;

        public ArticuloRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<Articulo> CreateAsync(Articulo articulo)
        {
            await dBContext.Articulos.AddAsync(articulo);
            await dBContext.SaveChangesAsync();
            return articulo;
        }

        public async Task<List<Articulo>> GetAllAsync()
        {
            return await dBContext.Articulos.ToListAsync();
        }
    }
}
