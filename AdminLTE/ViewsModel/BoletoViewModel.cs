using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminLTE.ViewsModel
{
    public class BoletoViewModel
    {
        public int ID { get; set; }
        public string CnpjCedente { get; set; }
        public string NomeCedente { get; set; }
        public string CodigoBanco { get; set; }
        public string AgenciaCedente { get; set; }
        public string DvAgenciaCedente { get; set; }
        public string ContaCedente { get; set; }
        public string CodigoCedente { get; set; }
        public string DvContaCedente { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal Valor { get; set; }
        public decimal Juros { get; set; }
        public decimal Multa { get; set; }
        public decimal Desconto { get; set; }
        public string Carteira { get; set; }
        public string NumeroDocumento { get; set; }
        public string NossoNumero { get; set; }
        public string CpfSacado { get; set; }
        public string NomeSacado { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public string Uf { get; set; }
        public string Complemento { get; set; }
        public bool Aceite { get; set; }
        public string Instrucoes { get; set; }
        public string VencimentoFormatado { get; set; }
        
    }
}