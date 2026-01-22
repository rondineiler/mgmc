using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MGMC.Models
{
    public class Horario
    {
        public int Id { get; set; }

        [Required]
        public int MedicoId { get; set; }

        [ForeignKey("MedicoId")]
        public Medico Medico { get; set; }

        [Required]
        public DiaSemanaEnum DiaSemana { get; set; }

        [Required]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        public TimeSpan HoraFim { get; set; }

    }
}