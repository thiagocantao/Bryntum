using System.Collections.Generic;

namespace Cdis.Brisk.Domain.Domains.Relatorio.PlanoTrabalho
{
    /// <summary>
    /// Objeto que retorna a consulta de plano de trabalho
    /// </summary> 
    public class RelatorioPlanoTrabalhoDomain
    {
        public int CodigoProjeto { get; set; }
        public int CodigoUnidadeNegocio { get; set; }
        public int CodigousuarioResponsavel { get; set; }        
        public string NomeProjeto { get; set; }
        public string TipoProjeto { get; set; }
        public string UnidadeNegocio { get; set; }
        public string Objetivos { get; set; }
        public string ResponsavelTecnico { get; set; }
        public string CarteiraPrincipal { get; set; }

        public string PublicoAlvo { get; set; }
        public string Observacao { get; set; }

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

        public List<PlanoTrabalhoProjetoEntregaDomain> ListPlanoTrabalhoEntrega { get; set; }
        public List<PlanoTrabalhoAtividadeEntregaDomain> ListPlanoTrabalhoAtividadeEntrega { get; set; }
        public List<PlanoTrabalhoProjetoProdutoDomain> ListPlanoTrabalhoProduto { get; set; }
        public List<PlanoTrabalhoProjetoResultadoEsperadoDomain> ListPlanoTrabalhoResultadoEsperado { get; set; }

    }
}
