using Cdis.Brisk.Domain.Domains.Entities;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories._Common
{
    /// <summary>
    /// Classe de repositório TextoPadraoSistemaRepository
    /// </summary>
    public class TextoPadraoSistemaRepository : RepositoryBaseQuery<TextoPadraoSistemaDomain>, IRepository
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de repositório TextoPadraoSistemaRepository
        /// </summary>
        private readonly UnitOfWorkRepository<DomainContext> _unitOfWorkRepository;

        /// <summary>
        /// Propriedade pública da unit of work da classe de repositório TextoPadraoSistemaRepository
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
        /// Construtor da classe de repositório TextoPadraoSistemaRepository
        /// </summary>
        public TextoPadraoSistemaRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }

        #endregion

        #region Methods
        public IQueryable<TextoPadraoSistemaDomain> GetAllTextoPadraoSistema()
        {
            return GetAll();
        }
        #endregion
    }
}
