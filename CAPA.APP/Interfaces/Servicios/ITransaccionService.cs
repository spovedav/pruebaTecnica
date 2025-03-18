using CAPA.DOMAIN.DTOs.Transaccion;
using CAPA.DOMAIN.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAPA.APP.Interfaces.Servicios
{
    public interface ITransaccionService
    {
        ResponseDto<List<TransaccionDto>> GetAll();
    }
}
