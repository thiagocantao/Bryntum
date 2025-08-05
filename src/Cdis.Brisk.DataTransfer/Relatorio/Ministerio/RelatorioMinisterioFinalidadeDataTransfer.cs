using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdis.Brisk.DataTransfer.Relatorio.Ministerio
{
    public class RelatorioMinisterioFinalidadeDataTransfer
    {
        public string Ano1 { get; set; }
        public string Ano2 { get; set; }
        public string Ano3 { get; set; }

        public string Programa { get; set; }
        public string Objetivo { get; set; }
        public string Indicador { get; set; }

        public string Funcao { get; set; }
        public string SubFuncao { get; set; }
        public string CodAcao { get; set; }
        public string Acao { get; set; }
        public string Unidade { get; set; }

        public string MetaAno1 { get; set; }
        public string MetaAno2 { get; set; }
        public string MetaAno3 { get; set; }

        public string Desc_Despesa1 { get; set; }
        public string ValorDespesa1_Ano1 { get; set; }
        public string ValorDespesa1_Ano2 { get; set; }
        public string ValorDespesa1_Ano3 { get; set; }

        public string Desc_Despesa2 { get; set; }
        public string ValorDespesa2_Ano1 { get; set; }
        public string ValorDespesa2_Ano2 { get; set; }
        public string ValorDespesa2_Ano3 { get; set; }

        public string Desc_Despesa3 { get; set; }
        public string ValorDespesa3_Ano1 { get; set; }
        public string ValorDespesa3_Ano2 { get; set; }
        public string ValorDespesa3_Ano3 { get; set; }
        
        public string TotalAno1 { get; set; }
        public string TotalAno2 { get; set; }
        public string TotalAno3 { get; set; }
    }
}