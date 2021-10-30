using Apuestas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apuestas.BaseDeDatos
{
    public class ApuestasDbContext : DbContext
    {
        public ApuestasDbContext(DbContextOptions opciones) : base(opciones)
        {

        }
        public DbSet<Jugador> Jugadores { get; set; }

        public DbSet<Equipo> Equipos { get; set; }

        public DbSet<Partido> Partidos { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public object Partido { get; internal set; }
    }

}
