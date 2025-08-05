namespace Cdis.Brisk.Infra.Data.Repository.Repositories.Relatorio.PlanoTrabalho.ResultSets
{
    /// <summary>
    /// Tabela responsável por cadastrar 
    /// </summary>
    /// <tableName>
    /// InfoProjetoPublicoAlvoDomain
    /// </tableName>
    /// <schemaName>
    /// 
    /// </schemaName>  
    public struct ProjetoPublicoAlvoResultSet
    {
        public int CodigoProjeto { get; set; }
        public string PublicoAlvo { get; set; }
        public string Observacao { get; set; }
    }
}
