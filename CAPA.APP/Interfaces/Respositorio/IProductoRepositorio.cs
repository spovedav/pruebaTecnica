using CAPA.DOMAIN.DTOs.Producto;
using CAPA.DOMAIN.Entity;
using System.Collections.Generic;

namespace CAPA.APP.Interfaces.Respositorio
{
    //POR SI VE ESTO PODRIA USAR UNA INTERFAS DE IRepository<Table> ... pero es cuestion tiempo
    public interface IProductoRepositorio
    {
        // POR SI VE ESTO, SERÍA MÁS RECOMENDALE USAR ASEumerable o AsQuerble para no traer todo en memoria, paginación
        List<Producto> GetAll();

        Producto GetProducto(int id);

        bool Mantemiento(Producto tabla);

        bool Add(Producto table);
        bool Update(Producto table);
    }
}
