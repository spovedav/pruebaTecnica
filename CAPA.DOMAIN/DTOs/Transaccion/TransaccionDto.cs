using System;
using System.Collections.Generic;
using System.Text;

namespace CAPA.DOMAIN.DTOs.Transaccion
{
    public class TransaccionDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public int ProductoId { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public int PrecioUnitario { get; set; }
        public int PrecioTotal { get; set; }
        public string Detalle { get; set; }

        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }
    }
}
