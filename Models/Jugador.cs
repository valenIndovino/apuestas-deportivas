using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Apuestas.Models
{
    public class Jugador
    {   
        public int Id { get; set; }
        [Required] //agregue para obligar a que no quede vacia por .NET (REVISAR) clase 6 min 1:29
        public String Usuario { get; set; }
        public int Edad { get; set; }
        public String Mail { get; set; }
        public String Password { get; set; }
        public float Saldo { get; set; }
    }
}
