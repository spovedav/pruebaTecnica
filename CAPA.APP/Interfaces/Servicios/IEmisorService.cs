using CAPA.DOMAIN.DTOs;
using CAPA.DOMAIN.DTOs.Emisor;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAPA.APP.Interfaces.Servicios
{
    public interface IEmisorService
    {
        ResponseDto<RepuestaEmision> Emitir(EmisorDto documento);
    }
}
