namespace Cdis.Brisk.Infra.Data.Repository.Repositories.Relatorio.PlanoTrabalho.ResultSets
{
    /// <summary>
    /// SP para retornar o relatório plano de trabalho
    /// </summary>
    /// <Name>
    /// p_SESCOOP_RelatoriosProjetos
    /// </Name>
    public struct ProjetoGeralPlanoTrabalhoResultSet
    {
        public int CodigoProjeto { get; set; }
        public int CodigoUnidadeNegocio { get; set; }
        public int CodigousuarioResponsavel { get; set; }
        public string NomeProjeto { get; set; }
        public string TipoProjeto { get; set; }
        public string UnidadeNegocio { get; set; }
        public string Objetivos { get; set; }
        public string ResponsavelTecnico { get; set; }
        public string CarteiraPrincipal { get; set; }
        public string Categoria { get; set; }        
    }
}
