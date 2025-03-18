using CAPA.APP.Interfaces.Respositorio;
using CAPA.APP.Interfaces.Servicios;
using CAPA.DOMAIN.DTOs;
using CAPA.DOMAIN.DTOs.Transaccion;
using CAPA.DOMAIN.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAPA.APP.Servicios
{
    public class TransaccionService : ITransaccionService
    {
        private readonly ITransaccionRepositorio _repo;
        public TransaccionService(ITransaccionRepositorio _repo)
        {
            this._repo = _repo;
        }

        public ResponseDto<List<TransaccionDto>> GetAll()
        {
            var response = new ResponseDto<List<TransaccionDto>>();

            response.Data = _repo.GetAll().Select(x => new TransaccionDto()
            {
                Detalle = x.Detalle,
                Cantidad = x.Cantidad,
                Estado = x.Estado,
                Fecha = x.Fecha,
                FechaCreacion = x.FechaCreacion,
                Id = x.Id,
                PrecioTotal = Convert.ToInt32(x.PrecioTotal),
                PrecioUnitario = Convert.ToInt32(x.PrecioUnitario),
                Producto = "Producto",//x.Producto,
                ProductoId = x.ProductoId,
                Tipo = x.Tipo,
            }).ToList();

            return response;
        }
    }
}
