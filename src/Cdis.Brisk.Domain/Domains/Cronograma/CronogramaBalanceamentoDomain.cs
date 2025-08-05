using System;

namespace Cdis.Brisk.Domain.Domains.Cronograma
{
    /// <summary>
    /// Retorno da busca das informações para o cronograma de balanceamento
    /// </summary>
    /// <tableName>
    /// CronogramaBalanceamentoDomain
    /// </tableName>
    /// <consulta>
    /// SELECT 
	///f.CodigoProjeto AS Codigo, 
	///f.NomeProjeto AS Descricao, 
	///f.Inicio, 
	///f.Termino, 
	///f.Cor AS Status, 
	///rp.PercentualRealizacao* 100 AS Concluido,
	///'0' AS Sumaria
    ///               FROM desenv_portalEstrategia.dbo.f_GetGanttProjetos(111, -1, 51) f INNER JOIN
    ///                    desenv_portalEstrategia.dbo.f_GetProjetosSelecaoBalanceamento(51, -1, 111) p ON p._CodigoProjeto = f.CodigoProjeto INNER JOIN
    ///                    desenv_portalEstrategia.dbo.ResumoProjeto rp ON rp.CodigoProjeto = f.CodigoProjeto
    ///             WHERE  IndicaCenario1 = 'S'
    /// </consulta>  
    public class CronogramaBalanceamentoDomain
    {
        /// <summary>
        /// Identificador único da tabela, controlado pelo banco de dados (Identity).
        /// </summary>                  
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Termino { get; set; }
        public string Cor { get; set; }
        public decimal Concluido { get; set; }
    }
}
