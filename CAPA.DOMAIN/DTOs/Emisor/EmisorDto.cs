using System;
using System.Collections.Generic;
using System.Text;

namespace CAPA.DOMAIN.DTOs.Emisor
{
    public class EmisorDto
    {
        public string Emisor { get; set; }
        public string Cliente { get; set; }
        public List<EmisorDetalleDto> Detalle { get; set; } = new List<EmisorDetalleDto>();

        public List<EmisorDetalleDto> Producto { get; set; } = new List<EmisorDetalleDto>();
    }

    public class EmisorDetalleDto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; } 
        public int Cantidad { get; set; }
        public int Precio { get; set; }
        public int Total { get; set; }
    }
}
