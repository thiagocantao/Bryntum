using System;

namespace Cdis.Brisk.Domain.Domains.Cronograma
{
    /// <summary>
    /// Tabela responsável por cadastrar 
    /// </summary>
    /// <tableName>
    /// CronogramaProjetoMetaDomain
    /// </tableName>
    /// <schemaName>
    /// 
    /// </schemaName>  
    public class CronogramaProjetoMetaDomain
    {
        public int Codigo { get; set; }
        public int? CodigoSuperior { get; set; }
        public string Descricao { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Termino { get; set; }
        public string Cor { get; set; }
        public decimal? Concluido { get; set; }
        public string SemCronograma { get; set; }
        public string Sumaria { get; set; }
        public string StatusProjeto { get; set; }
        public string GerenteProjeto { get; set; }
    }
}
