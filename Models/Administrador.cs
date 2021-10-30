using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Apuestas.Models
{
    public class Administrador : Usuario
    {
        public override Rol Rol => Rol.Administrador;
    }
}