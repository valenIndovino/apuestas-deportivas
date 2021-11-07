using Apuestas.BaseDeDatos;
using Apuestas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apuestas.Rules
{
    public class RNAdministradores
    {


        public static Administrador ObtenerAdmin(ApuestasDbContext _context, string username, string password)
        {

            Administrador admin = _context.Administradores.FirstOrDefault(o => o.Username == username && password == o.Password);

            return admin;
        }

    }
}
