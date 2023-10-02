using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Data;
using webapi.Models.Domain;
using webapi.Models.DTO;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiendasController : ControllerBase
    {
        private ApplicationDBContext dBContext;

        public TiendasController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpPost]
        public async Task<IActionResult> CrearTienda(CrearTiendaRequest request)
        {
            var tienda = new Tienda
            {
                Sucursal = request.Sucursal,
                Dirección = request.Dirección
            };

            await dBContext.Tiendas.AddAsync(tienda);
            await dBContext.SaveChangesAsync();

            var result = new TiendaDTO
            {
                Sucursal = tienda.Sucursal,
                Dirección = tienda.Dirección
            };

            return Ok(result);
        }
    }
}
