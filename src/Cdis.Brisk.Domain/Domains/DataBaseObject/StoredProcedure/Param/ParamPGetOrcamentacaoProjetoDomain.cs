namespace Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure.Param
{
    public struct ParamPGetOrcamentacaoProjetoDomain
    {
        public int CodigoEntidade { get; set; }
        public int CodigoProjeto { get; set; }
        public int? CodigoWorkflow { get; set; }
        public long? CodigoInstanciaWF { get; set; }
    }
}
