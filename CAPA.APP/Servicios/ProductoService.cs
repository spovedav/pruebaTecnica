using CAPA.APP.Interfaces.Servicios;
using CAPA.DOMAIN.DTOs.Producto;
using CAPA.DOMAIN.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using CAPA.APP.Interfaces.Respositorio;
using System.Linq;
using NUGET.TOOL.CORE._3_1.AES;
using CAPA.DOMAIN.Entity;
using CAPA.DOMAIN.DTOs.Emisor;
using CAPA.DOMAIN.Enum;

namespace CAPA.APP.Servicios
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepositorio _productoRepositorio; // POR SI VE ESTO, YO PREFIERO USAR IunitOfWork pero por el tiempo y esto es solo de prueba no lo implemento
        private readonly IAesEncryption aes;
        private readonly ITransaccionRepositorio _transaccionRepositorio;
        public ProductoService(ITransaccionRepositorio _transaccionRepositorio, IProductoRepositorio _productoRepositorio, IAesEncryption aes)
        {
            this._productoRepositorio = _productoRepositorio;
            this._transaccionRepositorio = _transaccionRepositorio;
            this.aes = aes;
        }

        public ResponseDto<List<ProductoDto>> GetAll()
        {
            var respose = new ResponseDto<List<ProductoDto>>();

            respose.Data = _productoRepositorio.GetAll().Select(x => new ProductoDto()
            {
                Descripcion = x.Descripcion,
                Categoria= x.CategoriaId,
                FechaCreacion=x.FechaCreacion,
                Estado=x.Estado,
                Id= x.Id,//aes.Encrypt(x.Id.ToString()),
                Imagen=x.Imagen,
                Nombre=x.Nombre,
                Precio= Convert.ToInt32(x.Precio),
                Stock=x.Stock,
            }).ToList();

            return respose;
        }

        public ResponseDto<ProductoDto> GetProducto(int Id)
        {
            var respose = new ResponseDto<ProductoDto>();

            var pro = _productoRepositorio.GetProducto(Id);

            if(pro is null)
            {
                respose.TieneError = true;
                respose.Mensaje = "No se puedo obtener el producto";
                return respose;
            }

            respose.Data = new ProductoDto()
            {
                Descripcion = pro.Descripcion,
                Categoria = pro.CategoriaId,
                Estado = pro.Estado,
                FechaCreacion = pro.FechaCreacion,
                Id = pro.Id,//aes.Encrypt(pro.Id.ToString()),
                Imagen = pro.Imagen,
                Nombre = pro.Nombre,
                Precio = Convert.ToInt32(pro.Precio),
                Stock =pro.Stock
            };

            return respose;
        }

        public ResponseDto<bool> Mantemiento(int Id,ProductoDto model)
        {
            var result = new ResponseDto<bool>();

            var producto = _productoRepositorio.GetProducto(Id);

            if(producto != null)
            {
                if (model.Estado == false)
                {
                    producto.Estado = false;
                    producto.FechaEliminacion = DateTime.Now;
                    producto.UsuarioEliminacion = "Systema";
                }
            }
            else
            {
                producto = Mapper(model, Id);
            }

            result.Data = _productoRepositorio.Mantemiento(producto);
            model.Id = producto.Id;
            if (result.Data)
            {
                GuardarTrasaccionVenta(model);
            }

            return result;
        }

        #region METODOS PRIVADOS
        private Producto Mapper(ProductoDto pro, int IdProducto)
        {
            return new Producto()
            {
                Descripcion = pro.Descripcion,
                CategoriaId = pro.Categoria,
                Estado = pro.Estado,
                FechaCreacion = DateTime.Now, //se que esta mal pero queda pendiente correguir
                Id = IdProducto,
                Imagen = pro.Imagen,
                Nombre = pro.Nombre,
                Precio = pro.Precio,
                Stock = pro.Stock,
                UsuarioCreacion = "System"
            };
        }

        private ProductoDto Mapper(Producto pro, int IdProducto)
        {
            return new ProductoDto()
            {
                Descripcion = pro.Descripcion,
                Categoria = pro.CategoriaId,
                Estado = pro.Estado,
                FechaCreacion = pro.FechaCreacion,
                Id = IdProducto, //aes.Encrypt(IdProducto.ToString()),
                Imagen = pro.Imagen,
                Nombre = pro.Nombre,
                Precio = Convert.ToInt32(pro.Precio),
                Stock = pro.Stock
            };
        }

        private void GuardarTrasaccionVenta(ProductoDto item)
        {
            Transaccion entity = new Transaccion()
            {
                Cantidad = item.Stock,
                FechaCreacion = DateTime.Now,
                Detalle = item.Descripcion,
                Estado = true,
                Fecha = DateTime.Now,
                PrecioTotal = item.Precio,
                PrecioUnitario = item.Precio,
                ProductoId = item.Id,
                Tipo = nameof(EnumTipoTran.Compra),
                UsuarioCreacion = "System"
            };

            _transaccionRepositorio.Add(entity);
        }
        #endregion
    }
}
