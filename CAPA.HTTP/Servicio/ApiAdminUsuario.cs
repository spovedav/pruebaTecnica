using CAPA.DOMAIN.DTOs;
using CAPA.DOMAIN.Static;
using CAPA.HTTP.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace CAPA.HTTP.Servicio
{
    public class ApiAdminUsuario : BaseApiServicio, IApiAdminUsuario
    {
        private readonly IPeticionesHTTP _http;
        
        public ApiAdminUsuario(IPeticionesHTTP _http, IConfiguration configuration) : base(configuration) {
            this._http = _http;
            this._http.SetUrlBase(ParametrosConfi.UrlBaseAdmin);
        }

        public ResponseDto<List<UsuarioDto>> GetAll(string Token, object queryParameto = null)
            => _http.Get<ResponseDto<List<UsuarioDto>>>(_metodo("Apis:Admin:Usuario:get-all"), Token, queryParameto);

    }
}
