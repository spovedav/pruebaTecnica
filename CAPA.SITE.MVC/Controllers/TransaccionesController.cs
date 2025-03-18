using CAPA.DOMAIN.DTOs.Producto;
using CAPA.DOMAIN.DTOs.Transaccion;
using CAPA.HTTP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAPA.SITE.MVC.Controllers
{
    [Authorize]
    public class TransaccionesController : BaseController
    {
        private readonly IApiAdminTransaccion _servicio;
        public TransaccionesController(IApiAdminTransaccion service)
        {
            this._servicio = service;
        }

        public IActionResult Index()
        {
            try
            {
                var lista = _servicio.GetAll(GetToken());
                if (lista.TieneError) { ViewBag.error = lista.Mensaje; }

                return View(lista.Data ?? new System.Collections.Generic.List<TransaccionDto>());
            }
            catch (System.Exception ex)
            {
                ViewBag.error = "Error";
                // SE QUE ME HACE FALTA ALGO PARA GUARDAR DATOS,, YA SE DESDE AKI O UN MIDDLEARE
                return View();
            }
        }
    }
}
