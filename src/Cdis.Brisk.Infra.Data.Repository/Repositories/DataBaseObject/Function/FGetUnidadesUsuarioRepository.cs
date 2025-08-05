using Cdis.Brisk.Domain.Domains.DataBaseObject.Function;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.Function
{
    /// <summary>
    /// Classe de repositório FGetUnidadesUsuarioRepository
    /// </summary>
    public class FGetUnidadesUsuarioRepository : RepositoryBaseQuery<FGetUnidadesUsuarioDomain>, IRepository
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de repositório FGetUnidadesUsuarioRepository
        /// </summary>
        private readonly UnitOfWorkRepository<DomainContext> _unitOfWorkRepository;

        /// <summary>
        /// Propriedade pública da unit of work da classe de repositório FGetUnidadesUsuarioRepository
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
        /// Construtor da classe de repositório FGetUnidadesUsuarioRepository
        /// </summary>
        public FGetUnidadesUsuarioRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }

        #endregion

        #region Methods
        public IQueryable<FGetUnidadesUsuarioDomain> ExecFunction(int codUsuario, int codEntidade)
        {
            string query = string.Format("SELECT * FROM [{0}].[{1}].[f_GetUnidadesUsuario] ({2}, {3})",
                Core.Data.DatabaseInfo.GetDatabaseNameSqlServer(this.UowRepository.GetStringConnection()),
                Core.Data.DatabaseInfo.GetOwnerBb(),
                codUsuario,
                codEntidade);

            return this.GetUsingSqlQuery(query);
        }
        #endregion
    }
}
