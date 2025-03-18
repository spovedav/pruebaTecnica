using CAPA.HTTP.Interfaces;
using CAPA.SITE.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CAPA.SITE.MVC.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IApiAuth _apiAuth;
        public AuthController(IApiAuth _apiAuth)
        {
            this._apiAuth = _apiAuth;
        }

        public IActionResult Index()
            => RedirectToAction(nameof(Login));
        

        public IActionResult Login()
            => View("Index");
       
        public IActionResult RecuperarContracena()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] string usuario, [FromForm] string password)
        {
            var resultado = _apiAuth.AutenticateGetToken(usuario, usuario, null);

            if (!string.IsNullOrEmpty(resultado.Data?.Token))
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario),
                        new Claim("Correo", resultado?.Data?.Correo), // Ejemplo de claim personalizado
                        new Claim("Token", resultado?.Data?.Token)
                    };

                var identidad = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identidad);

                await HttpContext.SignInAsync("CookieAuth", principal);

                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.Error = "Usuario o contraseña inválidos.";
            return View();
        }

        // Acción para cerrar sesión
        public async Task<IActionResult> SingOut()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index");
        }
    }
}
