using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using webapi.Models.Domain;

namespace webapi.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tienda> Tiendas { get; set; }

        public DbSet<Articulo> Articulos { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Carrito> Carritos { get; set; }

        public DbSet<ProductoCarrito> ProductoCarritos { get; set; }
    }
}
