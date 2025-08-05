using System;

namespace Cdis.Brisk.Domain.Domains.DataBaseObject.Function.Param
{
    public class ParamFGetCronogramaGanttProjetoDomain
    {
        public int CodigoProjeto { get; set; }
        public short VersaoLinhaBase { get; set; }
        public int CodigoRecurso { get; set; }
        public string SoAtrasadas { get; set; }
        public string SoMarcos { get; set; }
        public string PercentualConcluido { get; set; }
        public DateTime? DataFiltro { get; set; }
    }
}
