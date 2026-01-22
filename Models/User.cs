using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MGMC.Models
{

    [Table("users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        [StringLength(100)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O tipo de utilizador é obrigatório")]
        [StringLength(20)]
        public string Perfil { get; set; }   // Administrador | Medico | Rececionista

        [StringLength(255)]
        public string Foto { get; set; }

        public bool Ativo { get; set; } = true;




    }
}