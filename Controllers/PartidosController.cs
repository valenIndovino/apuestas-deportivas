using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Apuestas.BaseDeDatos;
using Apuestas.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading;
using System.ComponentModel.DataAnnotations;

namespace Apuestas.Controllers
{
    //[Authorize]
    public class PartidosController : Controller
    {
        private readonly ApuestasDbContext _context;

        public PartidosController(ApuestasDbContext context)
        {
            _context = context;
        }

        // GET: Partidos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Partidos.ToListAsync());
        }

        // GET: Partidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partido = await _context.Partidos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partido == null)
            {
                return NotFound();
            }

            return View(partido);
        }


        // GET: Partidos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Partidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("Id,NombreLocal,NombreVisitante,Fecha")] Partido partido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(partido);
        }

        // GET: Partidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partido = await _context.Partidos.FindAsync(id);
            if (partido == null)
            {
                return NotFound();
            }
            return View(partido);
        }

        // POST: Partidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreLocal,NombreVisitante,Fecha,GolesLocal,GolesVisitante")] Partido partido)
        {
            if (id != partido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartidoExists(partido.Id))
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
            return View(partido);
        }

        // GET: Partidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partido = await _context.Partidos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partido == null)
            {
                return NotFound();
            }

            return View(partido);
        }

        // POST: Partidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partido = await _context.Partidos.FindAsync(id);
            _context.Partidos.Remove(partido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartidoExists(int id)
        {
            return _context.Partidos.Any(e => e.Id == id);
        }
        //[Authorize(Roles = "Jugador")]
        public async Task<IActionResult> Cobrar(Historial h)
        {
            Partido partido = await _context.Partidos.FindAsync(h.Partido);
            if (partido == null)
            {
                return NotFound();
            }
                Equipo equipoRival = null;
                Equipo equipoApostado = null;
                Jugador j = await _context.Jugadores.FindAsync(h.Jugador);
                if (h.Resultado.Equals("GANA"))
                {
                    equipoRival = _context.Equipos.FirstOrDefault(e => e.Nombre == partido.NombreVisitante);
                    equipoApostado = _context.Equipos.FirstOrDefault(e => e.Nombre == partido.NombreLocal);
                }
                else if (h.Resultado.Equals("PIERDE"))
                {
                    equipoRival = _context.Equipos.FirstOrDefault(e => e.Nombre == partido.NombreLocal);
                    equipoApostado = _context.Equipos.FirstOrDefault(e => e.Nombre == partido.NombreVisitante);
                }
                else if(h.Resultado.Equals("EMPATA"))
                {
                    equipoRival = _context.Equipos.FirstOrDefault(e => e.Nombre == partido.NombreLocal);
                    equipoApostado = _context.Equipos.FirstOrDefault(e => e.Nombre == partido.NombreVisitante);
                }
                Resultado aposte = j.obtenerApostado(h.Resultado);
                Resultado resultado = partido.obtenerGanador(partido);
                
                //Si llegamos se paga por lo apostado
                if (aposte == resultado)
                {
                        j.pagar(h.CantApostado, h.Resultado, equipoApostado, equipoRival);
                        _context.Jugadores.Update(j);
                        _context.SaveChanges();
                }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

    }



}
