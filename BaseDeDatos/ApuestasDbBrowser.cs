using Apuestas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apuestas.BaseDeDatos
{
    public class ApuestasDbBrowser : DbContext
    {
        public ApuestasDbBrowser(DbContextOptions opciones) : base(opciones)
        {

        }
        public DbSet<Jugador> Jugadores { get; set; }

        //public DbSet<Equipo> Equipos { get; set; }

    }

}
