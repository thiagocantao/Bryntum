namespace Cdis.Brisk.DataTransfer.Orcamentacao
{
    public class ValorOrcamentacaoDataTransfer
    {
        public short NumAno { get; set; }
        public int NumMes { get; set; }
        public decimal? Valor { get; set; }
        public decimal ValorOrcamentoAnterior { get; set; }
        public decimal VariacaoOrcamentoAnterior { get; set; }
        public bool IsEditavel { get; set; }
    }
}
