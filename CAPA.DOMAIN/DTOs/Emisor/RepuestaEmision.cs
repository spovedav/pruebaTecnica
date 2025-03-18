using System;
using System.Collections.Generic;
using System.Text;

namespace CAPA.DOMAIN.DTOs.Emisor
{
    public class RepuestaEmision
    {
        public int EstadoEmision {  get; set; }
        public DateTime? FechaAutorizacion { get; set; }
        public string? Mensaje { get; set; }
    }
}
