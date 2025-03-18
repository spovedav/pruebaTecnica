using CAPA.DOMAIN.DTOs;
using CAPA.DOMAIN.DTOs.Auth;
using CAPA.DOMAIN.Static;
using CAPA.HTTP.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CAPA.HTTP.Servicio
{
    public class ApiAuth : BaseApiServicio, IApiAuth
    {
        private readonly IPeticionesHTTP _http;
        public ApiAuth(IPeticionesHTTP _http, IConfiguration configuration) : base(configuration)
        {
            this._http = _http;
            this._http.SetUrlBase(ParametrosConfi.UrlBaseAuth);
        }

        public ResponseDto<AuthResponse> AutenticateGetToken(string UserName, string Password, string Token)
            => _http.Get<ResponseDto<AuthResponse>>(_metodo("Apis:Auth:auth"), Token, new { UserName, PassWord = Password });
    }
}
