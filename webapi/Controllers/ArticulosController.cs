using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models.Domain;
using webapi.Models.DTO;
using webapi.Repositories.Interface;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly IArticuloRepository articuloRepository;
        private readonly ApplicationDBContext DBContext;

        public ArticulosController(IArticuloRepository articuloRepository, ApplicationDBContext dbContext)
        {
            this.articuloRepository = articuloRepository;
            DBContext = dbContext;
        }


        //public ApplicationDBContext DBContext;

        [HttpPost]
        public async Task<IActionResult> CrearArticulo(CrearArticuloRequest request)
        {
            var articulo = new Articulo
            {
                IdTienda = request.IdTienda,
                Codigo = request.Codigo,
                Descripcion = request.Descripcion,
                Precio = request.Precio,
                Imagen = request.Imagen,
                Stock = request.Stock
            };

            await articuloRepository.CreateAsync(articulo);

            var response = new ArticuloDTO
            {
                IdTienda = articulo.IdTienda,
                Codigo = articulo.Codigo,
                Descripcion = articulo.Descripcion,
                Precio = articulo.Precio,
                Imagen = articulo.Imagen,
                Stock = articulo.Stock
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosArticulos()
        {
            var articulos = await articuloRepository.GetAllAsync();

            var response = articulos.Select(articulo => new ArticuloDTO
            {
                Id = articulo.Id,
                IdTienda = articulo.IdTienda,
                Codigo = articulo.Codigo,
                Descripcion = articulo.Descripcion,
                Precio = articulo.Precio,
                Imagen = articulo.Imagen,
                Stock = articulo.Stock
            }).ToList();

            return Ok(response);
        }

        [HttpGet("ObtenerProductosPorCarrito")]
        public IActionResult ObtenerProductosPorCarrito(int idCarrito)
        {
            var productosEnCarrito = from pc in DBContext.ProductoCarritos
                                     join a in DBContext.Articulos on pc.idArticulo equals a.Id
                                     where pc.idCarrito == idCarrito
                                     select new ProductoCarritoDTO
                                     {
                                         Id = pc.Id,
                                         IdCarrito = pc.idCarrito,
                                         IdArticulo = pc.idArticulo,
                                         Cantidad = pc.cantidad,
                                         IdTienda = a.IdTienda,
                                         Codigo = a.Codigo,
                                         Descripcion = a.Descripcion,
                                         Precio = a.Precio,
                                         Imagen = a.Imagen,
                                         Stock = a.Stock
                                     };

            return Ok(productosEnCarrito.ToList());
        }


    }
}
