using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminLTE.Models;

namespace AdminLTE.ViewsModel
{
    public class CargoViewModel
    {
        public CargoViewModel()
        {
            Usuarios = new List<UsuarioViewModel>();
        }
        public int ID { get; set; }
        public string Codigo { get; set; }
        public string Ocupacao { get; set; }
        public ICollection<UsuarioViewModel> Usuarios { get; set; }
    }
}