using Cdis.Brisk.Domain.Domains.Projeto;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.Projeto
{
    public class ProjetoUnidadeRepository : RepositoryBaseQuery<ProjetoUnidadeDomain>, IRepository
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
        public ProjetoUnidadeRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Buscar o objeto ProjetoUnidadeDomain
        /// </summary>        
        public IQueryable<ProjetoUnidadeDomain> GetProjetoUnidade(int codigoProjeto)
        {
            string query = string.Format(
                    @" SELECT  TOP 1
	                            P.NomeProjeto, 
				                P.CodigoUnidadeNegocio,				
				                U.NomeUnidadeNegocio,				
			                    (select LogoUnidadeNegocio from UnidadeNegocio where CodigoUnidadeNegocio =	p.CodigoEntidade) as LogoEntidade
                        FROM 
	                        [{0}].[{1}].[Projeto] P 
	                        INNER JOIN [{0}].[{1}].[UnidadeNegocio] U ON P.CodigoUnidadeNegocio = U.CodigoUnidadeNegocio
                        WHERE CodigoProjeto = {2} ",
                Core.Data.DatabaseInfo.GetDatabaseNameSqlServer(this.UowRepository.GetStringConnection()),
                Core.Data.DatabaseInfo.GetOwnerBb(),
                codigoProjeto);

            return this.GetUsingSqlQuery(query);
        }
        #endregion
    }
}
