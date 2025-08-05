using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.DataTransfer.Orcamentacao
{
    public class OrcamentoContaProjetoDataTransfer
    {
        public string GrupoConta { get; set; }
        public int? CodConta { get; set; }
        public string DescConta { get; set; }
        public short? NumAno { get; set; }
        public int? CodMemoriaCalculo { get; set; }
        public string DescMemoriaCalculo { get; set; }


        public decimal TotalValorOrcamentoAnterior
        {
            get
            {
                if (ListValor.Count > 0)
                {
                    return ListValor.Sum(v => v.ValorOrcamentoAnterior);
                }
                else
                {
                    return 0;
                }
            }
        }
        public decimal TotalVariacaoOrcamentoAnterior
        {
            get
            {
                if (ListValor.Count > 0)
                {
                    return ListValor.Sum(v => v.VariacaoOrcamentoAnterior);
                }
                else
                {
                    return 0;
                }
            }
        }
        public List<ValorOrcamentacaoDataTransfer> ListValor { get; set; }
        public short CodigoPrevisao { get; set; }
        public decimal? TotalPorConta
        {
            get
            {
                if (ListValor.Count > 0)
                {
                    return ListValor.Sum(v => v.Valor);
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
