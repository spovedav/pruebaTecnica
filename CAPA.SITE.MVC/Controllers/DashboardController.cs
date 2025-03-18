using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAPA.SITE.MVC.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        public DashboardController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
