using CAPA.DOMAIN.DTOs.Producto;
using CAPA.DOMAIN.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using CAPA.DOMAIN.DTOs.Transaccion;

namespace CAPA.HTTP.Interfaces
{
    public interface IApiAdminTransaccion
    {
        ResponseDto<List<TransaccionDto>> GetAll(string Token, object queryParameto = null);
    }
}
