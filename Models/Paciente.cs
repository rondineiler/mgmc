using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MGMC.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Genero { get; set; }
    }
}