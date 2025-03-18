using CAPA.APP.Interfaces.Servicios;
using CAPA.DOMAIN.DTOs;
using CAPA.DOMAIN.DTOs.Producto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CAPA.API.ADMIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductoController : Controller
    {
        private readonly IProductoService _service;
        public ProductoController(IProductoService _service)
        {
            this._service = _service;
        }

        // POR SI VE ESTO Y SE PREGUNTA EN CASO DE ERROR DONDE SE GUARDA,, ESTA EN UN MIDDLEWARE
        [HttpGet("Get-All")]
        public ActionResult<ResponseDto<List<ProductoDto>>> Index()
            => Ok(_service.GetAll());

        [HttpGet("Get")]
        public ActionResult<ResponseDto<ProductoDto>> GetProducto([FromQuery] int Id)
            => Ok(_service.GetProducto(Id));

        [HttpPost("Mantenimiento")]
        public ActionResult<ResponseDto<ProductoDto>> Mantenimiento([FromQuery] int Id, ProductoDto model)
            => Ok(_service.Mantemiento(Id, model));
    }
}
