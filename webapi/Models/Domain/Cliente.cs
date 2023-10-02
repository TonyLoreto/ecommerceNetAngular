namespace webapi.Models.Domain
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Direccion { get; set;}

        public string Usuario { get; set;}

        public string Contrasena { get; set;}

        public int idCarrito { get; set;}
    }
}
