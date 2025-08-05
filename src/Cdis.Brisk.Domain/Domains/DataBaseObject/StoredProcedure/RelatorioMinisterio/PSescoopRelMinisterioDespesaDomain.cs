using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure.RelatorioMinisterio
{
    public class PSescoopRelMinisterioDespesaDomain
    {
        public int NumConta { get { return Convert.ToInt32(Conta.Replace(".", "")); } }
        public string Conta { get; set; }
        public string Descricao { get; set; }
        public decimal Valor_0 { get; set; }
        public decimal Valor_1 { get; set; }
        public decimal Valor_2 { get; set; }
    }
}
