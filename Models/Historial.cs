using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Apuestas.Models
{
    public class Historial

    {
        public Historial()
        {

        }
        public Historial(int partido, DateTime fecha, String resultado, int jugador, float apostado)
        { 
            this.Partido = partido;
            this.Fecha = fecha;
            this.Resultado = resultado;
            this.Jugador = jugador;
            this.CantApostado = apostado;
        }

        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Partido))]
        public int Partido { get; set; }
        public DateTime Fecha { get; set; }
        public String Resultado { get; set; }

        [ForeignKey(nameof(Jugador))]
        public int Jugador { get; set; }
        public float CantApostado { get; set; }

    }
}
