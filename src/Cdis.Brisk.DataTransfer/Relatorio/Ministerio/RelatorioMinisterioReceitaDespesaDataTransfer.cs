using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdis.Brisk.DataTransfer.Relatorio.Ministerio
{
    public class RelatorioMinisterioReceitaDespesaDataTransfer
    {
        public int Codigo { get; set; }
        public string DscEspecificacao { get; set; }
        public decimal VlrAno1 { get; set; }
        public decimal VlrAno2 { get; set; }
        public decimal VlrAno3 { get; set; }
        public bool IsAgrupador { 
            get 
            {
                var strCod = Codigo.ToString().Replace("0", "");
                return (strCod.Length == 1 || strCod.Length == 2);
            } 
        }
    }
}
