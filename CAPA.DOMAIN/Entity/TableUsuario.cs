using System;
using System.Collections.Generic;
using System.Text;

namespace CAPA.DOMAIN.Entity
{
    public class Usuario
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }

        public DateTime FechaCreacion { get; set; } 
        public bool Estado { get; set; }    
    }
}
