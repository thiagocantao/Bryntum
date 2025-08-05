using System;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.Relatorio.PlanoTrabalho.ResultSets
{
    /// <summary>
    /// Entrega da atividade
    /// </summary>
    /// <tableName>
    /// EntregaResultSet
    /// </tableName>    
    public struct EntregaAtividadeResultSet
    {
        public int CodigoProjeto { get; set; }
        public string ProdutoServico { get; set; }
        public string Meta { get; set; }
        public string Prazo { get; set; }
        public string EntregaRealizada { get; set; }
        public string StatusEntrega { get; set; }
    }
}
