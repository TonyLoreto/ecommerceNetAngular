namespace webapi.Models.Domain
{
    public class ProductoCarrito
    {
        public int Id { get; set; }

        internal int idCliente { get; set; }

        public int idCarrito { get; set; }

        public int idArticulo { get; set; }

        public int cantidad { get; set; }
    }
}
