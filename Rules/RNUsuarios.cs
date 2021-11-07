using Apuestas.BaseDeDatos;
using Apuestas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apuestas.Rules
{
    public class RNUsuarios
    {
        

        public static Jugador ObtenerUsuario(ApuestasDbContext _context, string username, string password)
        {

            Jugador usuario = _context.Jugadores.FirstOrDefault(o => o.Username == username && password == o.Password);
            
            return usuario;
        }

    }
}
