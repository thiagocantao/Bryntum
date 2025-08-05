using System;

namespace Cdis.Brisk.Domain.Domains.Relatorio.PlanoTrabalho
{
    /// <summary>
    /// Tabela responsável por cadastrar 
    /// </summary>
    /// <tableName>
    /// PlanoTrabalhoEntregaDomain
    /// </tableName>    
    public class PlanoTrabalhoAtividadeEntregaDomain
    {
        public int CodigoProjeto { get; set; }
        public string ProdutoServico { get; set; }
        public string Meta{ get; set; }
        public string Prazo{ get; set; }
        public string EntregaRealizada { get; set; }
        public string StatusEntrega { get; set; }       
    }
}
