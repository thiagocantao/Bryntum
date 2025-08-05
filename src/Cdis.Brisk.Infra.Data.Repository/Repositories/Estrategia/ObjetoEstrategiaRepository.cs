using Cdis.Brisk.Domain.Domains.Entities;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.Estrategia
{
    /// <summary>
    /// Classe de repositório ObjetoEstrategiaRepository
    /// </summary>
    public class ObjetoEstrategiaRepository : RepositoryBaseQuery<ObjetoEstrategiaDomain>, IRepository
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de repositório ObjetoEstrategiaRepository
        /// </summary>
        private readonly UnitOfWorkRepository<DomainContext> _unitOfWorkRepository;

        /// <summary>
        /// Propriedade pública da unit of work da classe de repositório ObjetoEstrategiaRepository
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
        /// Construtor da classe de repositório ObjetoEstrategiaRepository
        /// </summary>
        public ObjetoEstrategiaRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }

        #endregion

        #region Methods

        #endregion
    }
}
