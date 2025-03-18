using Microsoft.AspNetCore.Mvc;

namespace CAPA.SITE.MVC.Controllers
{
    public class PagesController : Controller
    {
        public IActionResult Index()
            => Content("Pages Inicial");

        [ActionName("page-500")]
        public IActionResult Page500()
            => Content("este en en caso de fallas");

        [ActionName("page-401")]
        public IActionResult Page401()
            => Content("no tiene acceso");

        [ActionName("page-dev")]
        public IActionResult Desarrollador()
            => Content("Soy un desarrollador");
    }
}
