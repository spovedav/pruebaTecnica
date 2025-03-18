using CAPA.DOMAIN.DTOs.Emisor;
using CAPA.DOMAIN.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAPA.HTTP.Interfaces
{
    public interface IApiAdminEmisor
    {
        ResponseDto<RepuestaEmision> Emitir(EmisorDto documento, string Token);
    }
}
