using CAPA.DOMAIN.DTOs;
using CAPA.DOMAIN.DTOs.Producto;
using System.Collections.Generic;

namespace CAPA.APP.Interfaces.Servicios
{
    public interface IProductoService
    {
        ResponseDto<List<ProductoDto>> GetAll();
        ResponseDto<ProductoDto> GetProducto(int Id);
        ResponseDto<bool> Mantemiento(int Id, ProductoDto model);
    }
}
