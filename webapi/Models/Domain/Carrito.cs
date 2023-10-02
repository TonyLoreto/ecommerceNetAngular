namespace webapi.Models.Domain
{
    public class Carrito
    {
        public int Id { get; set; }

        public int IdTienda { get; set; }

        public int IdCliente { get; set; }

        public DateTime fecha { get; set; }
    }
}
