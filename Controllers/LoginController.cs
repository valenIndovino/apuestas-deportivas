using Apuestas.BaseDeDatos;
using Apuestas.Models;
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
        private int _Return_Url;

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
