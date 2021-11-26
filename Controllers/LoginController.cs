using Apuestas.BaseDeDatos;
using Apuestas.Models;
using Apuestas.Rules;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Apuestas.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApuestasDbContext _context;
        private const string _Return_Url = "ReturnUrl"; 

        public LoginController(ApuestasDbContext context)
        {
            _context = context;
        }
       
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            TempData[_Return_Url] = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            string returnUrl = TempData[_Return_Url] as string;
            username = "pepe12222"; 
            password = "1234567890";

            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                
                var usuario = RNUsuarios.ObtenerUsuario(_context, username, password);


                if (usuario != null)
                {
                    ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                    identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Username));

                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));

                    identity.AddClaim(new Claim(ClaimTypes.GivenName, usuario.Username));

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();

                    TempData["LoggedIn"] = true;

                  
                     return RedirectToAction(nameof(HomeController.Index), "Home");
              
                } else
                {
                    var admin = RNAdministradores.ObtenerAdmin(_context, username, password);

                    if (admin != null)
                    {
                        ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                        identity.AddClaim(new Claim(ClaimTypes.Name, admin.Username));

                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()));

                        identity.AddClaim(new Claim(ClaimTypes.GivenName, admin.Username));

                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();

                        TempData["LoggedIn"] = true;


                        return RedirectToAction(nameof(HomeController.Index), "Home");

                    }

                }
            }

         
            ViewBag.Error = "Usuario o contraseña incorrectos";
            ViewBag.Username = username;
            TempData[_Return_Url] = returnUrl;

            return View();
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [Authorize]
        [HttpGet]
        public IActionResult NoAutorizado()
        {
            return View();
        }











    }
}
