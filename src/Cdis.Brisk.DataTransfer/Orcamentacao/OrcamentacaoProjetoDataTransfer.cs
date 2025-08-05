using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.DataTransfer.Orcamentacao
{
    public class OrcamentacaoProjetoDataTransfer
    {
        public List<OrcamentoContaProjetoDataTransfer> ListOrcamentoContaProjeto { get; set; }
        public List<ValorOrcamentacaoDataTransfer> TotalMes
        {
            get
            {
                List<ValorOrcamentacaoDataTransfer> list = new List<ValorOrcamentacaoDataTransfer>();
                if (ListOrcamentoContaProjeto.Any())
                {
                    foreach (var mes in Enumerable.Range(1, 2))
                    {
                        list.Add(new ValorOrcamentacaoDataTransfer
                        {
                            NumMes = mes,
                            Valor = ListOrcamentoContaProjeto.Sum(m => m.ListValor.Where(v => v.NumMes == mes).Sum(v => v.Valor))
                        });
                    }
                }

                return list;
            }
        }
    }
}
