using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Apuestas.Models
{
    public class Equipo
    {
        [Key]
        public String Nombre { get; set; }
        [Required]
        public int Puntuacion { get; set; }

    }
}
