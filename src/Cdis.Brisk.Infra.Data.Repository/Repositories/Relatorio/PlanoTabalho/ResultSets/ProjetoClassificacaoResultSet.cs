namespace Cdis.Brisk.Infra.Data.Repository.Repositories.Relatorio.PlanoTrabalho.ResultSets
{
    /// <summary>
    /// Tabela responsável por cadastrar 
    /// </summary>
    /// <tableName>
    /// ResultSetProjetoClassificacaoDomain
    /// </tableName>
    public struct ProjetoClassificacaoResultSet
    {
        public int CodigoProjeto { get; set; }
        public string Classificacao { get; set; }
        public string ObjetivoEstrategico { get; set; }
        public string LinhaAcao { get; set; }
        public string AreaAtuacao { get; set; }
        public string Natureza { get; set; }
        public string Funcao { get; set; }
        public string Subfuncao { get; set; }
        public string Programa { get; set; }        
        public string DataInicio { get; set; }
        public string DataTermino { get; set; }
    }
}
