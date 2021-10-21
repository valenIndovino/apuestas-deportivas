using Apuestas.BaseDeDatos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apuestas.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApuestasDbContext _context;

        public LoginController(ApuestasDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
