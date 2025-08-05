using Cdis.Brisk.Domain.Domains.Entities;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.Projeto
{
    /// <summary>
    /// Classe de repositório MemoriaCalculoRepository
    /// </summary>
    public class MemoriaCalculoRepository : RepositoryBaseCommand<MemoriaCalculoDomain>, IRepository
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de repositório MemoriaCalculoRepository
        /// </summary>
        private readonly UnitOfWorkRepository<DomainContext> _unitOfWorkRepository;

        /// <summary>
        /// Propriedade pública da unit of work da classe de repositório MemoriaCalculoRepository
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
        /// Construtor da classe de repositório MemoriaCalculoRepository
        /// </summary>
        public MemoriaCalculoRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }

        #endregion

        #region Methods

        #endregion
    }
}
