using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Apuestas.Models
{
    public abstract class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }

        public string Mail { get; set; } = "";

        public abstract Rol Rol { get; }
    }

}
