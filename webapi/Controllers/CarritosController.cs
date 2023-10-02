using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models.Domain;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritosController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;

        public CarritosController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrito>>> GetCarritos()
        {
            return await _dbContext.Carritos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Carrito>> GetCarrito(Guid id)
        {
            var carrito = await _dbContext.Carritos.FindAsync(id);

            if (carrito == null)
            {
                return NotFound();
            }

            return carrito;
        }

        [HttpPost]
        public async Task<ActionResult<Carrito>> PostCarrito(Carrito carrito)
        {
            carrito.fecha = DateTime.Now; 

            _dbContext.Carritos.Add(carrito);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCarrito), new { id = carrito.Id }, carrito);
        }

        [HttpPost("AgregarArticulo")]
        public async Task<ActionResult<ProductoCarrito>> AgregarArticuloAlCarrito(ProductoCarrito productoCarrito)
        {
            try
            {
                _dbContext.ProductoCarritos.Add(productoCarrito);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCarrito), new { id = productoCarrito.Id }, productoCarrito);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al agregar el artículo al carrito: {ex.Message}");
            }
        }

        [HttpDelete("EliminarArticulo/{id}")]
        public async Task<ActionResult<ProductoCarrito>> EliminarArticuloDelCarrito(Guid id)
        {
            try
            {
                var productoCarrito = await _dbContext.ProductoCarritos.FindAsync(id);
                if (productoCarrito == null)
                {
                    return NotFound();
                }

                _dbContext.ProductoCarritos.Remove(productoCarrito);
                await _dbContext.SaveChangesAsync();

                return Ok($"Artículo con ID {id} eliminado del carrito");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar el artículo del carrito: {ex.Message}");
            }
        }

        [HttpGet("ProductosDelCarrito")]
        public async Task<ActionResult<IEnumerable<ProductoCarrito>>> ObtenerProductosDelCarrito()
        {
            try
            {
                var productosDelCarrito = await _dbContext.ProductoCarritos.ToListAsync();

                return Ok(productosDelCarrito);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener los productos del carrito: {ex.Message}");
            }
        }

        [HttpGet("ProductosDelCarritoPorCliente/{idCliente}")]
        public async Task<ActionResult<IEnumerable<ProductoCarrito>>> ObtenerProductosDelCarritoPorCliente(int idCliente)
        {
            try
            {
                // Realiza una consulta para obtener los productos del carrito por ID del cliente
                var productosDelCarrito = await _dbContext.ProductoCarritos
                    .Where(pc => pc.idCliente == idCliente)
                    .ToListAsync();

                return Ok(productosDelCarrito);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener los productos del carrito: {ex.Message}");
            }
        }



    }
}
