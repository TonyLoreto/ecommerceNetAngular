using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models.Domain;
using webapi.Models.DTO;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private ApplicationDBContext dBContext;

        public ClientesController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente(CrearClienteRequest request)
        {
            var cliente = new Cliente
            {
                Nombres = request.Nombres,
                Apellidos = request.Apellidos,
                Direccion = request.Direccion,
                Usuario = request.Usuario,
                Contrasena = request.Contrasena
            };

            await dBContext.Clientes.AddAsync(cliente);
            await dBContext.SaveChangesAsync();

            var result = new Cliente
            {
                Id = cliente.Id,
                Nombres = cliente.Nombres,
                Apellidos = cliente.Apellidos,
                Direccion = cliente.Direccion,
                Usuario= cliente.Usuario,
                Contrasena= cliente.Contrasena
            };

            return Ok(result);
        }

        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> AutenticarCliente(string usuario, string contrasena)
        {
            var cliente = await dBContext.Clientes
                .Where(c => c.Usuario == usuario && c.Contrasena == contrasena)
                .FirstOrDefaultAsync();

            if (cliente == null)
            {
                return Unauthorized();
            }

            return Ok(new
            {
                Id = cliente.Id,
                Nombres = cliente.Nombres,
                Apellidos = cliente.Apellidos,
                Direccion = cliente.Direccion,
                Usuario = cliente.Usuario,
                IdCarrito = cliente.idCarrito
            });
        }
    }
}
