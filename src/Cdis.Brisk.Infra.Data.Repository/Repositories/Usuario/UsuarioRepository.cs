using Cdis.Brisk.Domain.Domains.Entities;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.Usuario
{
    /// <summary>
    /// Classe de repositório UsuarioRepository
    /// </summary>
    public class UsuarioRepository : RepositoryBaseCommand<UsuarioDomain>, IRepository
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de repositório UsuarioRepository
        /// </summary>
        private readonly UnitOfWorkRepository<DomainContext> _unitOfWorkRepository;

        /// <summary>
        /// Propriedade pública da unit of work da classe de repositório UsuarioRepository
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
        /// Construtor da classe de repositório UsuarioRepository
        /// </summary>
        public UsuarioRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }

        #endregion

        #region Methods

        #endregion
    }
}
