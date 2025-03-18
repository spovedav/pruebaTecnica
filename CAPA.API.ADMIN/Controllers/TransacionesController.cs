using CAPA.APP.Interfaces.Servicios;
using CAPA.DOMAIN.DTOs;
using CAPA.DOMAIN.DTOs.Transaccion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CAPA.API.ADMIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransacionesController : Controller
    {
        private readonly ITransaccionService _service;
        public TransacionesController(ITransaccionService _service)
        {
            this._service = _service;
        }

        [HttpGet("Get-All")]
        public ActionResult<ResponseDto<List<TransaccionDto>>> Index()
            => Ok(_service.GetAll());
    }
}
