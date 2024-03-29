﻿using System;
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
using System.Data.Entity;

namespace Apuestas.Controllers
{
    public class HistorialesController : Controller
    {
        private readonly ApuestasDbContext _context;

        public HistorialesController(ApuestasDbContext context)
        {
            _context = context;
        }

        // GET: Historiales
        public async Task<IActionResult> Index()
        {
            return View(await _context.Historials.ToListAsync());
        }

        // GET: Historiales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historial = await _context.Historials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historial == null)
            {
                return NotFound();
            }

            return View(historial);
        }

        // GET: Historiales/Create
        public IActionResult Create()
        {
            return View();
        }

        public async Task<RedirectToActionResult> CrearHistorial(float apuesta, String aposto, int? Id)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            int idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Partido partido = await _context.Partidos.FindAsync(Id);

            Historial h = new Historial(partido.Id, partido.Fecha, aposto, idUsuario, apuesta);

            DateTime fechaPartido;
            fechaPartido = partido.Fecha;
            DateTime fechaActual;
            fechaActual = DateTime.Now;
            Jugador j = await _context.Jugadores.FindAsync(idUsuario);
            if (fechaPartido > fechaActual && j.Saldo >= apuesta)
            {
                _ = Create(h);
                j.Saldo -= apuesta;
                _context.Jugadores.Update(j);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        // POST: Historiales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Partido,Fecha,Resultado,Jugador,CantApostado")] Historial historial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historial);
                _context.SaveChanges();
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(historial);
        }

        // GET: Historiales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historial = await _context.Historials.FindAsync(id);
            if (historial == null)
            {
                return NotFound();
            }
            return View(historial);
        }

        // POST: Historiales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Partido,Fecha,Resultado,Jugador,CantApostado")] Historial historial)
        {
            if (id != historial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistorialExists(historial.Id))
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
            return View(historial);
        }

        // GET: Historiales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historial = await _context.Historials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historial == null)
            {
                return NotFound();
            }

            return View(historial);
        }

        // POST: Historiales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historial = await _context.Historials.FindAsync(id);
            _context.Historials.Remove(historial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistorialExists(int id)
        {
            return _context.Historials.Any(e => e.Id == id);
        }

        public async Task<IActionResult> RecorrerLista()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            int idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Jugador j = await _context.Jugadores.FindAsync(idUsuario);
        

            foreach (Historial h in _context.Historials)
            {
                if (idUsuario == h.Jugador)
                {
                    if (h.Pagado == "NO")
                    {
                        Partido partido = await _context.Partidos.FindAsync(h.Partido);
                        DateTime fechaPartido = partido.Fecha;
                        DateTime fechaActual = DateTime.Now;
                        if (fechaActual >= fechaPartido.AddHours(24))
                        {
                            Equipo equipoRival = null;
                            Equipo equipoApostado = null;
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
                            else if (h.Resultado.Equals("EMPATA"))
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
                                h.Pagado = "SI";
                                _context.Historials.Update(h);
                                _context.Jugadores.Update(j);
                                _context.SaveChanges();
                            }
                        }
                    
                    }
                }
                //  }
                //Falta un botón en el navbar "Cobrar" que recorre la lista de apuestas en el historial (RecorrerLista)
                //de cada jugador.
                //Si se cumple el método paga y sino no paga.
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public async Task<int> Apostados()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            int idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            int i = 0;
            foreach (Historial h in _context.Historials)
            {
                if (idUsuario == h.Jugador)
                {
                    if (h.Pagado == "NO")
                    {
                        i++;
                    } else if(i > 0)
                    {
                        i--;
                    }
                    else
                    {
                        i = 0;
                    }
                }
            }

            return i;

            }
        }
}
