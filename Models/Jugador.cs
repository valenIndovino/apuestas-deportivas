using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading;
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

        public Rol rol = Rol.Jugador;

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
        }

        public int CalcularDiferencia(Equipo equipoApostado, Equipo equipoRival)
        {
            int diferencia;

            if (equipoApostado.Puntuacion < equipoRival.Puntuacion)
            {
                diferencia = equipoRival.Puntuacion - equipoApostado.Puntuacion;
            } else
            {
                diferencia = 0;
            }
            return diferencia;
        }
        public void pagar(float apostado, String apuesta, Equipo equipoApostado, Equipo equipoRival)
        {
            int diferencia = CalcularDiferencia(equipoApostado, equipoRival);
            if (apuesta.Equals("GANA"))
            {
                this.Saldo += apostado * (1.40f + (0.02f * diferencia));
            } else if (apuesta.Equals("PIERDE"))
            {
                this.Saldo += apostado * (1.60f + (0.04f * diferencia));
            } else if (apuesta.Equals("EMPATA"))
            {
                this.Saldo += apostado * 1.50f;
            }
        }
    }
}
