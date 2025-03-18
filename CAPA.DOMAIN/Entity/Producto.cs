﻿using System;
using System.Collections.Generic;

namespace CAPA.DOMAIN.Entity
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public string Imagen { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public bool Estado { get; set; }

        public ICollection<Transaccion> Transacciones { get; set; }
    }
}
