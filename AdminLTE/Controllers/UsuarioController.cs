using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminLTE.Models;
using AdminLTE.ViewsModel;
using System.Data.Entity;

namespace AdminLTE.Controllers
{
    public class UsuarioController : Controller
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

        public JsonResult GetUsuario()
        {
            using (var db = new Context())
            {
                var lista = db.Usuario.Select(p => new
                {
                    p.ID,
                    p.Nome,
                    p.Email,
                    p.Cargo.Ocupacao
                }).ToList();

                return Json(lista, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Salvar(Usuario usuario)
        {
            using (var db = new Context())
            {
                try
                {
                    foreach (var item in usuario.Telefones)
                    {
                        db.Entry(item).State = item.ID > 0 ? EntityState.Modified : EntityState.Added;
                    }
                    db.Entry(usuario).State = usuario.ID > 0 ? EntityState.Modified : EntityState.Added;
                    db.SaveChanges();

                    return Json(new { retorno = true, mensagem = "Usuario salvo com sucesso!" },
                        JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { retorno = false, mensagem = "Falha: " + ex.Message },
                        JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult GetUsuarioObj(int ID)
        {
            using (var db = new Context())
            {
                var usuario = db.Usuario.Find(ID);
                var usuarioViewModel = new UsuarioViewModel
                {
                    ID = usuario.ID,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Senha = usuario.Senha,
                    CargoID = usuario.CargoID
                };

                foreach (var item in usuario.Telefones)
                {
                    var usuarioTelefoneViewModel = new UsuarioTelefoneViewModel
                    {
                        ID = item.ID,
                        Telefone = item.Telefone,
                        UsuarioID = item.UsuarioID
                        
                    };

                    usuarioViewModel.Telefones.Add(usuarioTelefoneViewModel);
                }

                return Json(usuarioViewModel, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ExcluirUsuario(int ID)
        {
            using (var db = new Context())
            {
                try
                {
                    var usuario = db.Usuario.Find(ID);
                    db.UsuarioTelefone.RemoveRange(usuario.Telefones);
                    db.Usuario.Remove(usuario);
                    db.SaveChanges();
                    return Json(new { retorno = true }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { retorno = false, mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
                }

            }
        }

        public JsonResult ExcluirUsuarioTelefone(int ID)
        {
            using (var db = new Context())
            {
                try
                {
                    var usuarioTelefone = db.UsuarioTelefone.Find(ID);
                    db.UsuarioTelefone.Remove(usuarioTelefone);
                    db.SaveChanges();
                    return Json(new { retorno = true }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { retorno = false, mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
                }

            }
        }

        public JsonResult Upload(HttpPostedFileBase arquivo)
        {
            int arquivosSalvos = 0;
            //for (int i = 0; i < Request.Files.Count; i++) {
            //    //HttpPostedFileBase arquivo = Request.Files[i];
            //    //Suas validações ...... //Salva o arquivo if (arquivo.ContentLength > 0) 
            //    { 
            //        var uploadPath = Server.MapPath("~/Content/Uploads");
            //        string caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(arquivo.FileName)); 
            //        arquivo.SaveAs(caminhoArquivo); arquivosSalvos++; 
            //    }
            //} 

            return Json(new { retorno = true }, JsonRequestBehavior.AllowGet);

        }

    }
}