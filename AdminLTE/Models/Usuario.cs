using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminLTE.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        public Usuario()
        {
            Telefones = new List<UsuarioTelefone>();
        }

        [Key]
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int CargoID { get; set; }
        public virtual Cargo Cargo { get; set; }
        public virtual ICollection<UsuarioTelefone> Telefones { get; set; }
    }
}