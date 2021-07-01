using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminLTE.Models
{
    [Table("Cargo")]
    public class Cargo
    {
        public Cargo()
        {
            Usuarios = new List<Usuario>();
        }

        [Key]
        public int ID { get; set; }
        public string Codigo { get; set; }
        public string Ocupacao { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}