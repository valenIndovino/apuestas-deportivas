using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Apuestas.Models
{
    public class Apostador
    {
        [Key]
        public int Id { get; set; }

        //[ForeignKey(nameof(Jugador))]
        public String Nombre { get; set; }
        public String Resultado { get; set; }

        public float Ganancia{ get; set; }

    }
}
