using CAPA.DOMAIN.DTOs.Transaccion;
using CAPA.DOMAIN.DTOs;
using CAPA.HTTP.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using CAPA.DOMAIN.Static;
using CAPA.DOMAIN.DTOs.Producto;

namespace CAPA.HTTP.Servicio
{
    public class ApiAdminTransaccion : BaseApiServicio, IApiAdminTransaccion
    {
        private readonly IPeticionesHTTP _http;
        public ApiAdminTransaccion(IPeticionesHTTP _http, IConfiguration configuration) : base(configuration)
        {
            this._http = _http;
            this._http.SetUrlBase(ParametrosConfi.UrlBaseAdmin);
        }

        public ResponseDto<List<TransaccionDto>> GetAll(string Token, object queryParameto = null)
            => _http.Get<ResponseDto<List<TransaccionDto>>>(_metodo("Apis:Admin:Transaccion:get-all"), Token, queryParameto);
    }
}
