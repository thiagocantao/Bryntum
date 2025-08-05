using System;

namespace Cdis.Brisk.Domain.Domains.Relatorio.PlanoTrabalho
{
    /// <summary>
    /// Tabela responsável por cadastrar 
    /// </summary>
    /// <tableName>
    /// PlanoTrabalhoResultadoEsperadoDomain
    /// </tableName>
    /// <schemaName>
    /// 
    /// </schemaName>  
    public class PlanoTrabalhoProjetoResultadoEsperadoDomain
    {
        public string Transformacao { get; set; }
        public string Indicador { get; set; }
        public string Meta { get; set; }
        public DateTime? Prazo { get; set; }
    }
}
