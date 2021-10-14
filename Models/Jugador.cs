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
        public String Usuario { get; set; }
        public int Edad { get; set; }
        public String Mail { get; set; }
        public String Password { get; set; }
        

    }
}
