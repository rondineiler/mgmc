using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MGMC.Models
{
    public class Consulta
    {
        public int Id { get; set; }

        [Required]
        public int PacienteId { get; set; }

        [ForeignKey("PacienteId")]
        public Paciente Paciente { get; set; }

        [Required]
        public int MedicoId { get; set; }

        [ForeignKey("MedicoId")]
        public Medico Medico { get; set; }

        [Required]
        public DateTime DataConsulta { get; set; }

        [Required]
        public TimeSpan HoraConsulta { get; set; }

        public string Estado { get; set; }


    }
}