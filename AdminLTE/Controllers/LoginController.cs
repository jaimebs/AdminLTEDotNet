using System;
using System.Linq;
using System.Web.Mvc;
using AdminLTE.Models;

namespace AdminLTE.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult Logar(Usuario usuario)
        {
            using (var db = new Context())
            {
                try
                {
                    var user = db.Usuario.FirstOrDefault(p => p.Email == usuario.Email && p.Senha == usuario.Senha);

                    if (user == null)
                        return Json(new { retorno = false, mensagem = "Login Inválido!" }, JsonRequestBehavior.AllowGet);
                    Session["Usuario"] = user.Nome;
                    Session["Cargo"] = user.Cargo.Ocupacao;
                    return Json(new { retorno = true }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { retorno = false, mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
                }
               
            }
        }
    }
}