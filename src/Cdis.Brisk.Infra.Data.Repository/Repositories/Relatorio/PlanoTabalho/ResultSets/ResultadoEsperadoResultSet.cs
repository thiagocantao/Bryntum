using System;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.Relatorio.PlanoTrabalho.ResultSets
{
    /// <summary>
    /// Tabela responsável por cadastrar 
    /// </summary>     
    public struct ResultadoEsperadoResultSet
    {
        public int CodigoProjeto { get; set; }
        public string Transformacao { get; set; }
        public string Indicador { get; set; }
        public string Meta { get; set; }
        public DateTime? Prazo { get; set; }
    }
}
