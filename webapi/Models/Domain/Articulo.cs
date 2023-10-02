﻿namespace webapi.Models.Domain
{
    public class Articulo
    {
        public int Id { get; set; }

        public int IdTienda { get; set; }
       
        public string Codigo { get; set; }

        public string Descripcion { get; set;}

        public float Precio { get; set; }

        public string Imagen { get; set; }

        public int Stock { get; set; }
    }
}
