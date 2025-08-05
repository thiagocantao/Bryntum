using System;

namespace Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure
{
    /// <summary>
    /// Retorno da SP dbo.p_GetOrcamentacaoProjeto
    /// </summary>
    public class PGetOrcamentacaoProjetoDomain
    {
        public Int16? _Ano { get; set; }
        public string Periodo { get; set; }
        public Int16? _Mes { get; set; }
        public int? _CodigoConta { get; set; }
        public string DescricaoConta { get; set; }
        public string IndicaEntradaSaida { get; set; }
        public decimal? Valor { get; set; }
        public string TipoConta { get; set; }
        public string Editavel { get; set; }
        public string GrupoConta { get; set; }
        public Int16? CodigoPeriodicidade { get; set; }
        public Int16? IntervaloMeses { get; set; }
        public short CodigoPrevisao { get; set; }
        public decimal ValorOrcamentoAnterior { get; set; }
        public decimal VariacaoOrcamentoAnterior { get; set; }
        public int? CodigoMemoriaCalculo { get; set; }
        public string DescricaoMemoriaCalculo { get; set; }
    }
}
