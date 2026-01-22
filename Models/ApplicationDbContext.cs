using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;



namespace MGMC.Models
{
    public class ApplicationDbContext : DbContext
    {
        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public ApplicationDbContext()
              : base("DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
    }
}