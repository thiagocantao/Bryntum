using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Cdis.Brisk.Domain.Domains.DataBaseObject.Function
{
    /// <summary>
    /// Tabela responsável por cadastrar 
    /// </summary>
    /// <tableName>
    /// FGetCronogramaPADomain
    /// </tableName>
    /// <function>
    /// [f_GetCronogramaPA] Params - (@IniciaisObjetoParam Char(2), @CodigoObjetoParam Int, @CodigoEntidadeParam Int)
    /// </function>     
    public class FGetCronogramaPADomain
    {
        public string Descricao { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Termino { get; set; }
        public string Responsavel { get; set; }
        public decimal? PercentualConcluido { get; set; }
        public string Status { get; set; }
        public decimal? Trabalho { get; set; }
        public decimal? Custo { get; set; }
        public string EstruturaHierarquica { get; set; }
        public int CodigoPlanoAcao { get; set; }
        public Int16? Nivel { get; set; }
        public string Tipo { get; set; }

        [NotMapped]
        public string TarefaSuperior
        {
            get
            {
                string tarefaSuperior = null;
                List<string> listItemTarefa = EstruturaHierarquica.Split('.').ToList();
                if (listItemTarefa.Count > 0)
                {
                    listItemTarefa.RemoveAt(listItemTarefa.Count - 1);
                    tarefaSuperior = string.Join(".", listItemTarefa);
                }

                return tarefaSuperior;
            }
        }
    }
}
