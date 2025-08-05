using System;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.Relatorio.PlanoTrabalho.ResultSets
{
    /// <summary>
    /// Tabela responsável por cadastrar 
    /// </summary>
    /// <tableName>
    /// EntregaResultSet
    /// </tableName>    
    public struct EntregaResultSet
    {
        public int CodigoProjeto { get; set; }
        public string Acao { get; set; }
        public string EntregaPrevista { get; set; }
        public string EntregaRealizada { get; set; }
        public string StatusEntrega { get; set; }
    }
}
