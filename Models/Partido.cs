﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Apuestas.Models
{
    public class Partido
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Equipo))]
        public String NombreLocal { get; set; }
        [ForeignKey(nameof(Equipo))]
        public String NombreVisitante { get; set; }
        [Required]
        public DateTime Fecha { get; set; }

        public int GolesLocal { get; set; }
        public int GolesVisitante { get; set; }

        public Resultado obtenerGanador(Partido partido)
        {
            Resultado ganador = Resultado.EMPATA;
            if (partido.GolesLocal > partido.GolesVisitante)
            {
                ganador = Resultado.GANA;
            }
            else if (partido.GolesVisitante > partido.GolesLocal)
            {
                ganador = Resultado.PIERDE;
            }
            
            return ganador;
        }

        
    }
}
