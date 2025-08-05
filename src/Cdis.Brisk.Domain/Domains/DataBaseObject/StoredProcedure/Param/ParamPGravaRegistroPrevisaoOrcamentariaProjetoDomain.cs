namespace Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure.Param
{
    public struct ParamPGravaRegistroPrevisaoOrcamentariaProjetoDomain
    {
        public int CodigoProjeto { get; set; }
        public string TipoPrevisaoOrcamentaria { get; set; }
        public int CodigoConta { get; set; }
        public int? CodigoPessoaParticipe { get; set; }
        public short CodigoPeriodicidade { get; set; }
        public short Ano { get; set; }
        public short Mes { get; set; }
        public decimal? ValorPrevisto { get; set; }
        public int CodigoPrevisao { get; set; }
        public int? CodigoWorkflow { get; set; }
        public int? CodigoInstanciaWF { get; set; }

    }
}
