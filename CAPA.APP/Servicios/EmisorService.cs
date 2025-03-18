using CAPA.APP.Interfaces.Servicios;
using CAPA.DOMAIN.DTOs.Emisor;
using CAPA.DOMAIN.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using CAPA.APP.Interfaces.Respositorio;
using Microsoft.EntityFrameworkCore.Internal;
using CAPA.DOMAIN.Entity;
using CAPA.DOMAIN.Enum;

namespace CAPA.APP.Servicios
{
    public class EmisorService : IEmisorService
    {
        private readonly ITransaccionRepositorio _transaccionRepositorio;
        private readonly IProductoRepositorio _productoRepositorio;
        public EmisorService(IProductoRepositorio _productoRepositorio, ITransaccionRepositorio transaccionRepositorio)
        {
            this._productoRepositorio = _productoRepositorio;
            this._transaccionRepositorio = transaccionRepositorio;
        }

        public ResponseDto<RepuestaEmision> Emitir(EmisorDto documento)
        {
            var repuesta = new ResponseDto<RepuestaEmision>();

            bool estado = !(DateTime.Now.Second == 2);

            if (estado)
                repuesta.Data = new RepuestaEmision() { EstadoEmision = 2, FechaAutorizacion = DateTime.Now, Mensaje = "Autorizada. la factura fue aprovada" };
            else
                repuesta.Data = new RepuestaEmision() { EstadoEmision = 6, Mensaje = "Devuelta SRI. Documento no pasa la validación" };

            if(repuesta.Data.EstadoEmision == 2) {  //AUTORIZADA
                if (documento.Detalle.Any())
                {
                    foreach (var item in documento.Detalle)
                    {
                        if(DescontarStock(item))
                            GuardarTrasaccionVenta(item);
                    }
                }
            }
            
            return repuesta;
        }

        private bool DescontarStock(EmisorDetalleDto item)
        {
            var producto = _productoRepositorio.GetProducto(item.IdProducto);
            if (producto is null)
                return false;

            producto.Stock -= item.Cantidad;

            return _productoRepositorio.Update(producto);
        }

        private void GuardarTrasaccionVenta(EmisorDetalleDto item)
        {
            Transaccion entity = new Transaccion()
            {
                Cantidad = item.Cantidad,
                FechaCreacion = DateTime.Now,
                Detalle = item.Detalle,
                Estado = true,
                Fecha = DateTime.Now,
                PrecioTotal = item.Precio,
                PrecioUnitario = item.Precio,
                ProductoId = item.IdProducto,
                Tipo = nameof(EnumTipoTran.Venta),
                UsuarioCreacion = "System"
            };

            _transaccionRepositorio.Add(entity);
        }
    }
}
