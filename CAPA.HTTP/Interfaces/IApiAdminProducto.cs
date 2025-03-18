using CAPA.DOMAIN.DTOs;
using CAPA.DOMAIN.DTOs.Producto;
using System.Collections.Generic;

namespace CAPA.HTTP.Interfaces
{
    public interface IApiAdminProducto
    {
        ResponseDto<List<ProductoDto>> GetAll(string Token, object queryParameto = null);
        ResponseDto<ProductoDto> GetProducto(int Id, string Token, object queryParameto = null);
        ResponseDto<bool> Mantenimiento(int Id, ProductoDto model, string Token);
    }
}
