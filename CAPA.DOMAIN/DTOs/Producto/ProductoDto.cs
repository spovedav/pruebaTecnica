using CAPA.DOMAIN.Entity;
using System;

namespace CAPA.DOMAIN.DTOs.Producto
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Categoria { get; set; }
        public string CategoriaName { 
            get 
            { 
               return Categoria == 1 ? "Servicio" : "Bien"; 
            } 
        }
        public string Imagen { get; set; }
        public int Precio { get; set; }
        public int Stock { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }

    }
}
