using Cdis.Brisk.Domain.Domains.Cronograma;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.Cronograma
{
    /// <summary>
    /// Classe de repositório CronogramaBalanceamentoRepository
    /// </summary>
    public class CronogramaBalanceamentoRepository : RepositoryBaseQuery<CronogramaBalanceamentoDomain>, IRepository
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de repositório CronogramaBalanceamentoRepository
        /// </summary>
        private readonly UnitOfWorkRepository<DomainContext> _unitOfWorkRepository;

        /// <summary>
        /// Propriedade pública da unit of work da classe de repositório CronogramaBalanceamentoRepository
        /// </summary>
        private UnitOfWorkRepository<DomainContext> UowRepository
        {
            get
            {
                return _unitOfWorkRepository;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Construtor da classe de repositório CronogramaBalanceamentoRepository
        /// </summary>
        public CronogramaBalanceamentoRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Listar as informações do cronograma de balanceamento
        /// </summary>        
        public IQueryable<CronogramaBalanceamentoDomain> GetListCronogramaBalanceamento(int codEntidade, int codPortfolio, int numCenario)
        {
            if (numCenario < 1 || numCenario > 9)
            {
                throw new System.Exception("Número do cenário é inválido");
            }
            string fieldIndicaCenario = "IndicaCenario" + numCenario;
            string query = string.Format(
                    @" 
                        SELECT 
	                        f.CodigoProjeto AS Codigo, 
	                        f.NomeProjeto AS Descricao, 
	                        f.Inicio, 
	                        f.Termino, 
	                        f.Cor, 
	                        rp.PercentualRealizacao * 100 AS Concluido	                     
                        FROM [{0}].[{1}].f_GetGanttProjetos({2}, -1, {3}) f INNER JOIN
                             [{0}].[{1}].f_GetProjetosSelecaoBalanceamento({3}, -1, {2}) p ON p._CodigoProjeto = f.CodigoProjeto INNER JOIN
                             [{0}].[{1}].ResumoProjeto rp ON rp.CodigoProjeto = f.CodigoProjeto
                        WHERE 
                            {4} = 'S' 
                ",
                Core.Data.DatabaseInfo.GetDatabaseNameSqlServer(this.UowRepository.GetStringConnection()),
                Core.Data.DatabaseInfo.GetOwnerBb(),
                codEntidade,
                codPortfolio,
                fieldIndicaCenario);

            return this.GetUsingSqlQuery(query);
        }
        #endregion
    }
}
