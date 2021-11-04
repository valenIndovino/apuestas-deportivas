using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Apuestas.Models
{
    public class Jugador
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }

        public string Mail { get; set; } = "";

        public int Edad { get; set; }
        public float Saldo { get; set; }

        public Resultado obtenerApostado (String aposto)
        {
            Resultado r = Resultado.EMPATA;
            if(aposto.Equals("GANA"))
            {
                r = Resultado.GANA;
            } else if(aposto.Equals("PIERDE")) {
                r = Resultado.PIERDE;
            }
            return r;
            //Utilizar el botón y que el mismo devuelva el resultado
        }
        //Si llegamos implementamos el pagar por lo apostado.

        //RECIBIS NOMBRE EQUIPO
        //SI ELIGE LOCAL = nombreLocal
        //nombreLocal (buscarEquipo) -> devuelve un Equipo
        //Si encontró, compara ambas puntuaciones y paga.
        //Método llamado saldoAPagar => puntuacionLocal > puntuacionVisitante && Resultado.GANA => (puntuacionLocal - puntuacionVisitante) = diferencia => 1.40 + 0,02 * diferencia

        public void pagar()
        {
            this.Saldo += 150;
        }
    }
}
