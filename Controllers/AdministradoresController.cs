using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Apuestas.BaseDeDatos;
using Apuestas.Models;

namespace Apuestas.Controllers
{
    public class AdministradoresController : Controller
    {
        private readonly ApuestasDbContext _context;

        public AdministradoresController(ApuestasDbContext context)
        {
            _context = context;
        }

        // GET: Administradores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Administradores.ToListAsync());
        }

        // GET: Administradores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrador = await _context.Administradores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administrador == null)
            {
                return NotFound();
            }

            return View(administrador);
        }

        // GET: Administradores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administradores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Mail,Password")] Administrador administrador)
        {
            if (ModelState.IsValid)
            {
                var listaAdministradores = _context.Administradores.ToList();
                bool noSeRepite = !listaAdministradores.Any(jug => jug.Username.Equals(administrador.Username));
                if (!noSeRepite)
                {
                    ViewBag.Error = "Administrador existente";
                    return View(administrador);
                }
                bool validarMail = !listaAdministradores.Any(jug => jug.Mail.Equals(administrador.Mail));
                if (!validarMail)
                {
                    ViewBag.Error = "Administrador existente";
                    return View(administrador);
                }
                if (this.ValidarRepeticion(administrador))
                {
                    _context.Add(administrador);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Email Repetido";
                    return View(administrador);
                }
            }
            return View(administrador);
        }
        private bool ValidarRepeticion(Administrador administrador)
        {
            List<Administrador> listaUsuarios = _context.Administradores.ToList<Administrador>();
            listaUsuarios = _context.Administradores.ToList<Administrador>();
            //2. Por cada usuario, ver si el email se repite contra el email recibido(usuario)
            var noSeRepite = !listaUsuarios
                .Where(a => a.Mail != null)
                .Any(usu => usu.Mail.Equals(administrador.Mail, StringComparison.OrdinalIgnoreCase) &&
                usu.Id != administrador.Id);

            return noSeRepite;
        }
        // GET: Administradores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrador = await _context.Administradores.FindAsync(id);
            if (administrador == null)
            {
                return NotFound();
            }
            return View(administrador);
        }

        // POST: Administradores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password")] Administrador administrador)
        {
            if (id != administrador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(administrador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministradorExists(administrador.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(administrador);
        }

        // GET: Administradores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrador = await _context.Administradores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administrador == null)
            {
                return NotFound();
            }

            return View(administrador);
        }

        // POST: Administradores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var administrador = await _context.Administradores.FindAsync(id);
            _context.Administradores.Remove(administrador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdministradorExists(int id)
        {
            return _context.Administradores.Any(e => e.Id == id);
        }
    }
}
