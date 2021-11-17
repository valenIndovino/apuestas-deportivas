using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Apuestas.BaseDeDatos;
using Apuestas.Models;
using System.Security.Claims;
using System.Threading;

namespace Apuestas.Controllers
{
    public class JugadoresController : Controller
    {
        private readonly ApuestasDbContext _context;

        public JugadoresController(ApuestasDbContext context)
        {
            _context = context;
        }

        // GET: Jugadores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Jugadores.ToListAsync());
        }

        // GET: Jugadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jugador = await _context.Jugadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jugador == null)
            {
                return NotFound();
            }

            return View(jugador);
        }

        // GET: Jugadores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jugadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Jugador jugador)
        {
            if (ModelState.IsValid)
            {
                var listaJugadores = _context.Jugadores.ToList();
                bool noSeRepite = !listaJugadores.Any(jug => jug.Username.Equals(jugador.Username));
                if (!noSeRepite)
                {
                    ViewBag.Error = "Usuario Repetido";
                    return View(jugador);
                }
                bool validarRepeticion = !listaJugadores.Any(jug => jug.Mail.Equals(jugador.Mail));
                if (!validarRepeticion)
                {
                    ViewBag.Error = "Usuario existente";
                    return View(jugador);
                }
            }
            if (this.ValidarRepeticion(jugador))
            {
                _context.Add(jugador);
                jugador.Saldo += 1000;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(LoginController.Login), "Login");
            }
            else
            {
                ViewBag.Error = "Email Repetido";
                return View(jugador);
            }
            //return View(jugador);
        }

        private bool ValidarRepeticion(Jugador usuario)
    {
        List<Jugador> listaUsuarios = _context.Jugadores.ToList<Jugador>();
        listaUsuarios = _context.Jugadores.ToList<Jugador>();
        //2. Por cada usuario, ver si el email se repite contra el email recibido(usuario)
        var noSeRepite = !listaUsuarios
            .Where(a => a.Mail != null)
            .Any(usu => usu.Mail.Equals(usuario.Mail, StringComparison.OrdinalIgnoreCase) &&
            usu.Id != usuario.Id);

        return noSeRepite;
    }

    // GET: Jugadores/Edit/5
    public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jugador = await _context.Jugadores.FindAsync(id);
            if (jugador == null)
            {
                return NotFound();
            }
            return View(jugador);
        }

        // POST: Jugadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Usuario,Edad,Mail,Password, Saldo")] Jugador jugador)
        {
            if (id != jugador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jugador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JugadorExists(jugador.Id))
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
            return View(jugador);
        }


        // GET: Jugadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jugador = await _context.Jugadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jugador == null)
            {
                return NotFound();
            }

            return View(jugador);
        }

        // POST: Jugadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jugador = await _context.Jugadores.FindAsync(id);
            _context.Jugadores.Remove(jugador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JugadorExists(int id)
        {
            return _context.Jugadores.Any(e => e.Id == id);
        }

        public async Task<float> MostrarSaldo()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            int idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Jugador jugador = await _context.Jugadores.FindAsync(idUsuario);
            return jugador.Saldo;
        }
    }
}
