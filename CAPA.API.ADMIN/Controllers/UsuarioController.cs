using CAPA.APP.Interfaces.Servicios;
using CAPA.DOMAIN.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CAPA.API.ADMIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServices usuarioService;

        public UsuarioController(IUsuarioServices usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        // POR SI VE ESTO Y SE PREGUNTA EN CASO DE ERROR DONDE SE GUARDA,, ESTA EN UN MIDDLEWARE
        [HttpGet("Get-All")]
        public ActionResult<ResponseDto<List<UsuarioDto>>> GetAll()
            => Ok(usuarioService.GetAll());
        
    }
}
