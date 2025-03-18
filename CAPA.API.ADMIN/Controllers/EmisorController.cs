using CAPA.APP.Interfaces.Servicios;
using CAPA.DOMAIN.DTOs.Producto;
using CAPA.DOMAIN.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CAPA.DOMAIN.DTOs.Emisor;

namespace CAPA.API.ADMIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmisorController : Controller
    {
        private readonly IEmisorService _service;
        public EmisorController(IEmisorService _service)
        {
            this._service = _service;
        }

        [HttpPost("Emitir")]
        public ActionResult<ResponseDto<RepuestaEmision>> Index(EmisorDto documento)
            => Ok(_service.Emitir(documento));
    }
}
