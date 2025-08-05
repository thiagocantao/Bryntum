using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.DataTransfer.Gantt
{
    public class TaskGanttDataTransfer
    {
        public int id { get; set; }
        public string edtcode { get; set; }
        public string isCaminhoCriticoStr { get; set; }
        public string name { get; set; }
        public string nomeTarefa { get; set; }
        public decimal previsto { get; set; }
        public decimal? custo { get; set; }
        public string previstoStr { get { return previsto.ToString() + " %"; } }
        public decimal percentDone { get; set; }
        public string realizado { get; set; }
        public string percentDoneStr { get; set; }
        public string pesoLb { get; set; }
        public string peso { get; set; }
        public int duracao { get; set; }
        public string duracaoStr { get { return duracao.ToString(); } }
        public decimal? duracaoLb { get; set; }
        public string trabalho { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string inicio { get; set; }
        public string inicioLb { get; set; }
        public string termino { get; set; }
        public string terminoReal { get; set; }
        public string terminoLb { get; set; }
        public string isMarcoStr { get; set; }
        public string isAtrasoStr { get; set; }
        public string recurso { get; set; }
        public string codTarefa { get; set; }
        public bool expanded
        {
            get
            {
                return children != null && children.Any();
            }
        }
        public List<TaskGanttDataTransfer> children { get; set; }
    }
}
