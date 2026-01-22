using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MGMC.Models
{
    public class Medico
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Especialidade { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public string Foto { get; set; }

        public bool Ativo { get; set; } = true;




    }
}