using CAPA.APP.Interfaces.Servicios;
using CAPA.DOMAIN.DTOs.Auth;
using CAPA.DOMAIN.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAPA.API.AUTH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioServices _usuarioServices;
        public AuthController(IUsuarioServices _usuarioServices)
        {
            this._usuarioServices = _usuarioServices;
        }

        [HttpGet]
        public ActionResult<AuthResponse> Get([FromQuery] string UserName, [FromQuery] string PassWord)
        {
            var username = HttpContext.Items["Username"]?.ToString() ?? UserName;
            var password = HttpContext.Items["Password"]?.ToString() ?? PassWord;

            string mensajeError = null;
            var response = _usuarioServices.AutenticarLoguin(new AuthDto() { UserName = username, PassWord = password }, ref mensajeError);

            if (response is null)
                return Conflict(mensajeError);

            return Ok(response);
        }
    }
}
