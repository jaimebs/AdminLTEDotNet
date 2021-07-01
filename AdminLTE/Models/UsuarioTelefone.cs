using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminLTE.Models
{
    [Table("UsuarioTelefone")]
    public class UsuarioTelefone
    {
        [Key]
        public int ID { get; set; }
        public string Telefone { get; set; }
        public int UsuarioID { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}