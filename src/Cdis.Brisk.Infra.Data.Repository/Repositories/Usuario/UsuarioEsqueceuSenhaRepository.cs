using Cdis.Brisk.Domain.Domains.Usuario;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.Usuario
{
    /// <summary>
    /// Classe de repositório UsuarioEsqueceuSenhaRepository
    /// </summary>
    public class UsuarioEsqueceuSenhaRepository : RepositoryBaseCommand<UsuarioEsqueceuSenhaDomain>, IRepository
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de repositório UsuarioEsqueceuSenhaRepository
        /// </summary>
        private readonly UnitOfWorkRepository<DomainContext> _unitOfWorkRepository;

        /// <summary>
        /// Propriedade pública da unit of work da classe de repositório UsuarioEsqueceuSenhaRepository
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
        /// Construtor da classe de repositório UsuarioEsqueceuSenhaRepository
        /// </summary>
        public UsuarioEsqueceuSenhaRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }

        #endregion

        #region Methods

        #endregion
    }
}
