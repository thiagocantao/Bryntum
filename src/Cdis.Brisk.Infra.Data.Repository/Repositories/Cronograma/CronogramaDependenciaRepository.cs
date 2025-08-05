using Cdis.Brisk.Domain.Domains.Cronograma;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.Cronograma
{
    /// <summary>
    /// Classe de repositório CronogramaDependenciaRepository
    /// </summary>
    public class CronogramaDependenciaRepository : RepositoryBaseQuery<CronogramaDependenciaDomain>, IRepository
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de repositório CronogramaDependenciaRepository
        /// </summary>
        private readonly UnitOfWorkRepository<DomainContext> _unitOfWorkRepository;

        /// <summary>
        /// Propriedade pública da unit of work da classe de repositório CronogramaDependenciaRepository
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
        /// Construtor da classe de repositório CronogramaDependenciaRepository
        /// </summary>
        public CronogramaDependenciaRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }

        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>        
        public IQueryable<CronogramaDependenciaDomain> GetIQueryableCronogramaDependencia(int codigoProjeto)
        {
            string query = string.Format(
                    @" SELECT 
	                        tcpp.codigoTarefa AS ToTask, 
	                        tcpp.codigoTarefaPredecessora AS FromTask, 
	                        tipoLatencia AS TipoLatencia
                        FROM 
	                        [{0}].[{1}].[TarefaCronogramaProjetoPredecessoras] tcpp 
	                        INNER JOIN [{0}].[{1}].[CronogramaProjeto] cp 
		                        ON (cp.CodigoCronogramaProjeto = tcpp.CodigoCronogramaProjeto AND cp.CodigoProjeto = {2})
                ",
                Core.Data.DatabaseInfo.GetDatabaseNameSqlServer(this.UowRepository.GetStringConnection()),
                Core.Data.DatabaseInfo.GetOwnerBb(),
                codigoProjeto);

            return this.GetUsingSqlQuery(query);
        }
        #endregion
    }
}
