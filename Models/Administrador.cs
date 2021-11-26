using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Apuestas.Models
{
    public class Administrador
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }

        public string Mail { get; set; } = "";

        public Rol rol = Rol.Administrador;

    }
}