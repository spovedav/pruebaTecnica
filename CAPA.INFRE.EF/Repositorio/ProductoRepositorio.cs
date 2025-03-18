using CAPA.APP.Interfaces.Respositorio;
using CAPA.DOMAIN.DTOs.Producto;
using CAPA.DOMAIN.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NUGET.TOOL.CORE._3_1.AES;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CAPA.INFRE.EF.Repositorio
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly AppDbContext _db;
        private readonly IAesEncryption aes;
        public ProductoRepositorio(AppDbContext _db, IAesEncryption aes)
        {
            this._db = _db; 
            this.aes = aes;
        }

        public List<Producto> GetAll()
            => _db.Productos.Where(x=> x.Estado == true).ToList();

        public Producto GetProducto(int id)
            => _db.Productos.FirstOrDefault(x=> x.Id == id);

        public bool Mantemiento(Producto tabla)
            => (tabla.Id > 0) ? Update(tabla) : Add(tabla);

        #region METODOS PRIVADOS
        public bool Add(Producto table)
        {
            _db.Productos.Add(table);
            return (_db.SaveChanges() > 0);
        }

        public bool Update(Producto table)
        {
            _db.Productos.Update(table);
            return (_db.SaveChanges() > 0);
        }
        #endregion

    }
}
