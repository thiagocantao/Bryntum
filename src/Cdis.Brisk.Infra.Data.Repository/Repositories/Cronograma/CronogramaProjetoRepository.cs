using Cdis.Brisk.Domain.Domains.Entities;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;


namespace Cdis.Brisk.Infra.Data.Repository.Repositories.Cronograma
{
    /// <summary>
    /// Classe de repositório CronogramaProjetoRepository
    /// </summary>
    public class CronogramaProjetoRepository : RepositoryBaseQuery<CronogramaProjetoDomain>, IRepository
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de repositório CronogramaProjetoRepository
        /// </summary>
        private readonly UnitOfWorkRepository<DomainContext> _unitOfWorkRepository;

        /// <summary>
        /// Propriedade pública da unit of work da classe de repositório CronogramaProjetoRepository
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
        /// Construtor da classe de repositório CronogramaProjetoRepository
        /// </summary>
        public CronogramaProjetoRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }

        #endregion

        #region Methods

        #endregion
    }
}
