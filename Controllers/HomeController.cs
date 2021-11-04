using Apuestas.BaseDeDatos;
using Apuestas.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Apuestas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApuestasDbContext _context;

        public HomeController(ApuestasDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View( _context.Partidos.ToList());
        }

        public IActionResult Registro()
        {
            return View();
        }

        public IActionResult Apostar()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
