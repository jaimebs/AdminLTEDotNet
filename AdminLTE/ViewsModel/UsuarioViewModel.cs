using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminLTE.Models;

namespace AdminLTE.ViewsModel
{
    public class UsuarioViewModel
    {
        public UsuarioViewModel()
        {
            Telefones = new List<UsuarioTelefoneViewModel>();
        }
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int CargoID { get; set; }
        public ICollection<UsuarioTelefoneViewModel> Telefones { get; set; } 
    }
}