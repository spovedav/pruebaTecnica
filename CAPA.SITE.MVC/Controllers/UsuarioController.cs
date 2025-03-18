using CAPA.HTTP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CAPA.SITE.MVC.Controllers
{
    [Authorize]
    public class UsuarioController : BaseController
    {
        private readonly IApiAdminUsuario _servicio;
        public UsuarioController(IApiAdminUsuario _servicio)
        {
            this._servicio = _servicio;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var lista = _servicio.GetAll(GetToken());
                if (lista.TieneError) { ViewBag.error = lista.Mensaje; }

                return View(lista.Data ?? new System.Collections.Generic.List<DOMAIN.DTOs.UsuarioDto>());
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
