using System;
using System.Linq;
using System.Web.Mvc;
using AdminLTE.Models;
using AdminLTE.ViewsModel;
using BoletoNet;
using Boleto = BoletoNet.Boleto;

namespace AdminLTE.Controllers
{
    public class BoletoController : Controller
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
        
        public JsonResult Salvar(Models.Boleto boleto)
        {
            using (var db = new Context())
            {
                try
                {
                    if (boleto.ID > 0)
                    {
                        db.Entry(boleto).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.Boleto.Add(boleto);
                    }

                    db.SaveChanges();

                    return Json(new { retorno = true, mensagem = "Boleto salvo com sucesso!" },
                        JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { retorno = false, mensagem = "Falha: " + ex.Message },
                        JsonRequestBehavior.AllowGet);
                }
            }
        }

        public string GerarBoletoItau(Models.Boleto _boleto)
        {
            var boletoBancario = new BoletoBancario { CodigoBanco = Convert.ToInt16(_boleto.CodigoBanco) };
            //_boleto.CnpjCedente = _boleto.CnpjCedente.Replace(".", "").Replace("/", "").Replace("-", "");
            //_boleto.CpfSacado = _boleto.CpfSacado.Replace(".", "").Replace("-", "");
            DateTime vencimento = _boleto.Vencimento;
            
            Cedente c = new Cedente(_boleto.CnpjCedente, _boleto.NomeCedente, _boleto.AgenciaCedente, _boleto.DvAgenciaCedente, _boleto.ContaCedente, _boleto.DvContaCedente);
            //Na carteira 198 o código do Cedente é a conta bancária
            c.Codigo = _boleto.CodigoCedente;

            Boleto b = new Boleto(vencimento, _boleto.Valor, _boleto.Carteira, _boleto.NossoNumero, c, new EspecieDocumento(Convert.ToInt16(_boleto.CodigoBanco), "1"));
            b.LocalPagamento = "Até o vencimento, preferencialmente no Banco Itaú";
            b.NumeroDocumento = _boleto.NumeroDocumento;

            b.Sacado = new Sacado(_boleto.CpfSacado, _boleto.NomeSacado);
            b.Sacado.Endereco.End = _boleto.Endereco+" "+_boleto.Numero+" "+_boleto.Complemento;
            b.Sacado.Endereco.Bairro = _boleto.Bairro;
            b.Sacado.Endereco.Cidade = _boleto.Cidade;
            b.Sacado.Endereco.CEP = _boleto.Cep;
            b.Sacado.Endereco.UF = _boleto.Uf;
            
            // Exemplo de como adicionar mais informações ao sacado
            //b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + "2541245"));
            
            if (_boleto.Juros > 0)
            {
                //998 é o código para a instrução: Após vencimento cobrar juros de 10,00 % por dia de atraso//
                Instrucao_Itau item1 = new Instrucao_Itau(998, (double)_boleto.Juros, AbstractInstrucao.EnumTipoValor.Percentual);
                b.Instrucoes.Add(item1);
            }

            if (_boleto.Multa > 0)
            {
                //997 é o código para a instrução: Após vencimento cobrar multa de 2,50 %//
                Instrucao_Itau item2 = new Instrucao_Itau(997, (double)_boleto.Multa, AbstractInstrucao.EnumTipoValor.Percentual);
                b.Instrucoes.Add(item2);
            }

            if (_boleto.Desconto > 0)
            { 
                var valorComDesconto = _boleto.Valor - (_boleto.Valor * _boleto.Desconto / 100);
                //Instrucao_Itau item3 = new Instrucao_Itau(999, 0);
                Instrucao_Itau item3 = new Instrucao_Itau(0, 0);
                item3.Descricao += (string.Format(" Pagar o valor de {0} até a data de vencimento.", valorComDesconto.ToString("N")));
                b.Instrucoes.Add(item3);
            }

            if (!string.IsNullOrEmpty(_boleto.Instrucoes))
            {
                Instrucao_Itau item4 = new Instrucao_Itau(0, 0);
                item4.Descricao += (_boleto.Instrucoes);
                b.Instrucoes.Add(item4);
            }

            b.Aceite = _boleto.Aceite ? "S" : "N";

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();
            
            return boletoBancario.MontaHtmlEmbedded();
        }

        public ActionResult VisualizarBoleto(int id)
        {
            using (var db = new Context())
            {
                var boleto = db.Boleto.Find(id);

                switch (341)
                {
                    case 341:
                        ViewBag.Boleto = GerarBoletoItau(boleto);
                        break;
                    default:
                        ViewBag.Boleto = "Banco não implementado";
                        break;
                }
                return View();
            }
        }

        public JsonResult GetBoleto()
        {
            using (var db = new Context())
            {
                var lista = db.Boleto.ToList().Select(b => new
                {
                    b.ID,
                    b.NomeCedente,
                    b.NomeSacado,
                    b.NossoNumero,
                    b.Valor
                });
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetBoletoObj(int ID)
        {
            using (var db = new Context())
            {
                var boleto = db.Boleto.Find(ID);

                var boletoViewModel = new BoletoViewModel
                {
                    ID = boleto.ID,
                    CnpjCedente = boleto.CnpjCedente,
                    NomeCedente = boleto.NomeCedente,
                    AgenciaCedente = boleto.AgenciaCedente,
                    DvAgenciaCedente = boleto.DvAgenciaCedente,
                    ContaCedente = boleto.ContaCedente,
                    DvContaCedente = boleto.DvContaCedente,
                    CodigoCedente = boleto.CodigoCedente,
                    Vencimento = boleto.Vencimento,
                    Valor = boleto.Valor,
                    Carteira = boleto.Carteira,
                    NumeroDocumento = boleto.NumeroDocumento,
                    NossoNumero = boleto.NossoNumero,
                    CpfSacado = boleto.CpfSacado,
                    NomeSacado = boleto.NomeSacado,
                    Cep = boleto.Cep,
                    Endereco = boleto.Endereco,
                    Numero = boleto.Numero,
                    Bairro = boleto.Bairro,
                    Cidade = boleto.Cidade,
                    Uf = boleto.Uf,
                    Complemento = boleto.Complemento,
                    Instrucoes = boleto.Instrucoes,
                    CodigoBanco = boleto.CodigoBanco,
                    Aceite = boleto.Aceite,
                    VencimentoFormatado = boleto.Vencimento.ToShortDateString()
                };

                return Json(boletoViewModel, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ExcluirBoleto(int ID)
        {
            using (var db = new Context())
            {
                try
                {
                    var boleto = db.Boleto.Find(ID);
                    db.Boleto.Remove(boleto);
                    db.SaveChanges();

                    return Json(new {retorno = true,mensagem  = "Boleto excluído com sucesso!"});
                }
                catch (Exception ex)
                {
                    return Json(new { retorno = true, mensagem = "Falha: "+ex.Message });
                }
            }
        }
    }
}