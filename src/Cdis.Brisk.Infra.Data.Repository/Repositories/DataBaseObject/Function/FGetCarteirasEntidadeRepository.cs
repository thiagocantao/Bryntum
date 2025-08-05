using Cdis.Brisk.Domain.Domains.DataBaseObject.Function;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;
using System.Linq;


namespace Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.Function
{
    public class FGetCarteirasEntidadeRepository : RepositoryBaseQuery<FGetCarteirasEntidadeDomain>, IRepository
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de repositório ConexaoContratoRepository
        /// </summary>
        private readonly UnitOfWorkRepository<DomainContext> _unitOfWorkRepository;

        /// <summary>
        /// Propriedade pública da unit of work da classe de repositório ConexaoContratoRepository
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
        /// Construtor da classe de repositório CategoriaRepository
        /// </summary>
        public FGetCarteirasEntidadeRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>        
        public IQueryable<FGetCarteirasEntidadeDomain> ExecFunction(int codEntidade)
        {
            string query = string.Format("SELECT * FROM f_GetCarteirasEntidade ({2})",
                Core.Data.DatabaseInfo.GetDatabaseNameSqlServer(this.UowRepository.GetStringConnection()),
                Core.Data.DatabaseInfo.GetOwnerBb(),
                codEntidade);

            return this.GetUsingSqlQuery(query);
        }

        #endregion
    }
}
