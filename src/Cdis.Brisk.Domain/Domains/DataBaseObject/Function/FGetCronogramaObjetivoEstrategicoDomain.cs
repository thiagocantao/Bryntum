using System;

namespace Cdis.Brisk.Domain.Domains.DataBaseObject.Function
{
    /// <function>
    /// f_GetCronogramaObjetivoEstrategico
    /// </function>    
    public class FGetCronogramaObjetivoEstrategicoDomain
    {
        public string CodigoNumeroTarefa { get; set; }
        public int? CodigoProjeto { get; set; }
        public string CodigoTarefa { get; set; }
        public decimal? Custo { get; set; }
        public decimal? CustoHoraExtra { get; set; }
        public decimal? CustoPrevisto { get; set; }
        public decimal? CustoReal { get; set; }
        public decimal? CustoRestante { get; set; }
        public int? Duracao { get; set; }
        public int? DuracaoPrevista { get; set; }
        public int? DuracaoReal { get; set; }
        public int? DuracaoRestante { get; set; }
        public decimal? EAC { get; set; }
        public string EstruturaHierarquica { get; set; }
        public decimal? IDC { get; set; }
        public decimal? IDP { get; set; }
        public byte IndicaCritica { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? InicioPrevisto { get; set; }
        public DateTime? InicioReal { get; set; }
        public byte Marco { get; set; }
        public Int16? Nivel { get; set; }
        public string NomeTarefa { get; set; }
        public decimal? PercentualPrevisto { get; set; }
        public decimal? PercentualReal { get; set; }
        public byte PossuiAnotacoes { get; set; }
        public string Predecessoras { get; set; }
        public int SequenciaTarefaCronograma { get; set; }
        public byte SubProjeto { get; set; }
        public byte TarefaResumo { get; set; }
        public string TarefaSuperior { get; set; }
        public DateTime? Termino { get; set; }
        public DateTime? TerminoPrevisto { get; set; }
        public DateTime? TerminoReal { get; set; }
        public decimal? Trabalho { get; set; }
        public decimal? TrabalhoHE { get; set; }
        public decimal? TrabalhoPrevisto { get; set; }
        public decimal? TrabalhoReal { get; set; }
        public decimal? TrabalhoRealHoraExtra { get; set; }
        public decimal? TrabalhoRestante { get; set; }
        public decimal? ValorAgregado { get; set; }
        public decimal? ValorPlanejado { get; set; }
        public decimal? ValorReal { get; set; }
        public decimal? VarCusto { get; set; }
        public int? VarDuracao { get; set; }
        public int? VarInicio { get; set; }
        public int? VarTermino { get; set; }
        public decimal? VarTrabalho { get; set; }
    }
}
