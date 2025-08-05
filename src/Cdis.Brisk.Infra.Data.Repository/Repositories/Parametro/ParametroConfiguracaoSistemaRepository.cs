using Cdis.Brisk.Domain.Domains.Entities;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.Parametro
{
    public class ParametroConfiguracaoSistemaRepository : RepositoryBaseQuery<ParametroConfiguracaoSistemaDomain>, IRepository
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
        public ParametroConfiguracaoSistemaRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get IQueryable de ParametroConfiguracaoSistemaDomain listando os parâmetros da entidade
        /// </summary>        
        public IQueryable<ParametroConfiguracaoSistemaDomain> GetIQueryableParametro(int codEntidade)
        {
            return GetWhere(p => p.CodigoEntidade == codEntidade);
        }

        #endregion
    }
}
