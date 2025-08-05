using System;

namespace Cdis.Brisk.Domain.Domains.DataBaseObject.Function
{
    public class FGetCronogramaGanttProjetoDomain
    {
        public string Edt { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public string EstruturaHierarquica { get; set; }
        public int CodigoTarefa { get; set; }
        public string NomeTarefa { get; set; }
        public decimal? Concluido { get; set; }
        public decimal Trabalho { get; set; }
        public decimal? Custo { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }
        public DateTime? InicioLB { get; set; }
        public DateTime? TerminoLB { get; set; }
        public byte IndicaMarco { get; set; }
        public byte IndicaTarefaSumario { get; set; }
        public int Nivel { get; set; }
        public byte IndicaCritica { get; set; }
        public DateTime? TerminoReal { get; set; }
        public decimal DuracaoLB { get; set; }
        public decimal Duracao { get; set; }
        public DateTime? InicioPrevisto { get; set; }
        public DateTime? TerminoPrevisto { get; set; }
        public string Predecessoras { get; set; }
        public string CodigoRealTarefa { get; set; }
        public int CodigoProjeto { get; set; }
        public int SequenciaTarefaCronograma { get; set; }
        public string TarefaSuperior { get; set; }
        public int? Desvio { get; set; }
        public decimal? PercentualPrevisto { get; set; }
        public string CodigoCronogramaProjeto { get; set; }
        public decimal? ValorPesoTarefaLB { get; set; }
        public decimal PercentualPesoTarefa { get; set; }
        public string StringAlocacaoRecursoTarefa { get; set; }
        public string UnidadeDuracao { get; set; }
        public string TarefaResumo { get; set; }
        public decimal? PercentualReal { get; set; }
        public string IDTarefa { get; set; }
        public decimal? DuracaoLBSemConversao { get; set; }
        public string IndicaTarefaSerExcluida { get; set; }
    }
}
