using CAPA.DOMAIN.DTOs.Emisor;
using CAPA.HTTP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CAPA.SITE.MVC.Controllers
{
    [Authorize]
    public class EmitirController : BaseController
    {
        private readonly IApiAdminEmisor _servicio;
        private readonly IApiAdminProducto _servicioProducto;
        public EmitirController(IApiAdminEmisor _servicio, IApiAdminProducto _servicioProducto)
        {
            this._servicio = _servicio;
            this._servicioProducto = _servicioProducto;
        }

        public IActionResult Index()
        {
            var documento = new EmisorDto()
            {
                Emisor = GetCorreo(),
                Cliente = "Cliene 01"
            };

            var lista = _servicioProducto.GetAll(GetToken());
            ViewBag.productos = lista.Data.Select(x => new EmisorDetalleDto() { 
                Detalle = x.Descripcion,
                Cantidad = x.Stock,
                IdProducto = x.Id,
                Nombre = x.Nombre,
                Precio = x.Precio,
                Total = 0,
            }).ToList();

            return View(documento);
        }

        [HttpPost]
        public IActionResult Index([FromForm] EmisorDto documento)
        {
            var result = _servicio.Emitir(documento, GetToken());

            if(result.TieneError) { ViewBag.Error = result.TieneError; }

            ViewBag.Estado = result.Data?.EstadoEmision;
            ViewBag.Mensaje = result.Data?.Mensaje;

            return RedirectToAction(nameof(Index));
        }
    }
}
