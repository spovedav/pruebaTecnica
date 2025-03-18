using CAPA.DOMAIN.DTOs.Producto;
using CAPA.HTTP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAPA.SITE.MVC.Controllers
{
    [Authorize]
    public class ProductoController : BaseController
    {
        private readonly IApiAdminProducto _servicio;

        public ProductoController(IApiAdminProducto service)
        { 
            this._servicio = service;
        }

        public IActionResult Index()
        {
            try
            {
                var lista = _servicio.GetAll(GetToken());
                if (lista.TieneError) { ViewBag.error = lista.Mensaje; }

                return View(lista.Data ?? new System.Collections.Generic.List<ProductoDto>());
            }
            catch (System.Exception ex)
            {
                ViewBag.error = "Error";
                // SE QUE ME HACE FALTA ALGO PARA GUARDAR DATOS,, YA SE DESDE AKI O UN MIDDLEARE
                return View();
            }
        }

        public IActionResult Mantenimiento(int id)
        {
            var producto = _servicio.GetProducto(id,GetToken()) ?? new DOMAIN.DTOs.ResponseDto<ProductoDto>();
            if (producto.TieneError) { ViewBag.error = producto.Mensaje; }

            return View(producto.Data ?? new ProductoDto());
        }

        [HttpPost]
        public IActionResult Mantenimiento([FromForm] ProductoDto producto) {

            var result = _servicio.Mantenimiento(producto.Id, producto, GetToken());

            if (result.TieneError) { ViewBag.Error = result.Mensaje; /*PENDIENTE DE GUARDAR LOG*/ }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Eliminar(int id)
        {
            var modelo = new ProductoDto() { Id = id, Estado = false };

            var result = _servicio.Mantenimiento(modelo.Id, modelo, GetToken());

            if(result.TieneError) { ViewBag.Error = result.Mensaje; } 

            return RedirectToAction(nameof(Index));
        }
    }
}
