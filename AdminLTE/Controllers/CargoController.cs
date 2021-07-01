using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminLTE.Models;
using AdminLTE.ViewsModel;

namespace AdminLTE.Controllers
{
    public class CargoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Novo()
        {
            return View();
        }

        public ActionResult Editar()
        {
            return View();
        }

        public JsonResult Salvar(Cargo cargo)
        {
            using (var db = new Context())
            {
                try
                {
                    if (cargo.ID > 0)
                    {
                        db.Entry(cargo).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.Cargo.Add(cargo);
                    }

                    db.SaveChanges();

                    return Json(new {retorno = true, mensagem = "Cargo salvo com sucesso!"},
                        JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new {retorno = false, mensagem = "Falha: " + ex.Message},
                        JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult GetCargo()
        {
            using (var db = new Context())
            {
                var lista = db.Cargo.Select(c => new
                {
                    c.ID,
                    c.Codigo,
                    c.Ocupacao
                }).ToList();
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetCargoObj(int ID)
        {
            using (var db = new Context())
            {
                var cargo = db.Cargo.Find(ID);

                var cargoViewModel = new CargoViewModel
                {
                    ID = cargo.ID,
                    Codigo = cargo.Codigo,
                    Ocupacao = cargo.Ocupacao
                };

                foreach (var item in cargo.Usuarios)
                {
                    var usuarioViewModel = new UsuarioViewModel
                    { 
                        ID = item.ID,
                        Nome = item.Nome,
                        Email = item.Email
                    };
                    cargoViewModel.Usuarios.Add(usuarioViewModel);
                }

                return Json(cargoViewModel, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ExcluirCargo(int ID)
        {
            using (var db = new Context())
            {
                try
                {
                    var cargo = db.Cargo.Find(ID);
                    db.Cargo.Remove(cargo);
                    db.SaveChanges();
                    return Json(new { retorno = true });
                }
                catch (Exception)
                {
                    return Json(new {retorno = false,mensagem = "Esta cargo esta vinculado, por isso não pode ser excluído!"});
                }
               
            }
        }
    }
}