using System;

namespace Cdis.Brisk.Domain.Domains.DataBaseObject.Function
{
    /// <summary>
    /// Retorno da função [dbo].[f_orc_GetEtapasPrevisaoOrcamentaria]
    /// </summary>
    public class FOrcGetEtapasPrevisaoOrcamentariaDomain
    {
        public int CodigoPrevisao { get; set; }
        public string DescricaoPrevisao { get; set; }
        public string Observacao { get; set; }
        public short? AnoOrcamento { get; set; }
        public byte? MesInicioBloqueio { get; set; }
        public byte? MesTerminoBloqueio { get; set; }
        public DateTime? InicioPeriodoElaboracao { get; set; }
        public DateTime? TerminoPeriodoElaboracao { get; set; }
        public short CodigoStatusPrevisaoFluxoCaixa { get; set; }
        public string DescricaoStatusPrevisaoFluxoCaixa { get; set; }
        public string PodeExcluir { get; set; }
        public string PodeAlterarStatus { get; set; }
        public string PodeIgualarPrevistoRealizado { get; set; }
        public short? CodigoNovoStatusPermitido { get; set; }
        public short? CodigoStatusAnteriorPermitido { get; set; }
    }
}
