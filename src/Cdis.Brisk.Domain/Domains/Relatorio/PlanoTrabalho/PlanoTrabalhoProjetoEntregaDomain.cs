using System;

namespace Cdis.Brisk.Domain.Domains.Relatorio.PlanoTrabalho
{
    /// <summary>
    /// Tabela responsável por cadastrar 
    /// </summary>
    /// <tableName>
    /// PlanoTrabalhoEntregaDomain
    /// </tableName>    
    public class PlanoTrabalhoProjetoEntregaDomain
    {
        public string Acao { get; set; }
        public string EntregaPrevista{ get; set; }
        public string EntregaRealizada{ get; set; }
        public string StatusEntrega { get; set; }       
    }
}
