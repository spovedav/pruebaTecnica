using CAPA.DOMAIN.DTOs;
using CAPA.DOMAIN.DTOs.Emisor;
using CAPA.DOMAIN.DTOs.Producto;
using CAPA.DOMAIN.Static;
using CAPA.HTTP.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace CAPA.HTTP.Servicio
{
    public class ApiAdminEmisor : BaseApiServicio, IApiAdminEmisor
    {
        private readonly IPeticionesHTTP _http;
        public ApiAdminEmisor(IPeticionesHTTP _http, IConfiguration configuration) : base(configuration)
        {
            this._http = _http;
            this._http.SetUrlBase(ParametrosConfi.UrlBaseAdmin);
        }

        public ResponseDto<RepuestaEmision> Emitir(EmisorDto documento, string Token)
            => _http.Post<EmisorDto, ResponseDto<RepuestaEmision>>(_metodo("Apis:Admin:Emisor:Emitir"), documento, Token);
    }
}
