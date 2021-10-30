﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Apuestas.Models
{
    public class Jugador : Usuario
    {   
        public int Edad { get; set; }
        public float Saldo { get; set; }
        public override Rol Rol => Rol.Jugador;
    }
}
