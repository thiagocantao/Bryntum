using Cdis.Brisk.Domain.Domains.DataBaseObject.Function;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.Function
{
    public class FGetStatusPrevisaoFluxoCaixaRepository : RepositoryBaseQuery<FGetStatusPrevisaoFluxoCaixaDomain>, IRepository
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de repositório FGetStatusPrevisaoFluxoCaixaRepository
        /// </summary>
        private readonly UnitOfWorkRepository<DomainContext> _unitOfWorkRepository;

        /// <summary>
        /// Propriedade pública da unit of work da classe de repositório FGetStatusPrevisaoFluxoCaixaRepository
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
        /// Construtor da classe de repositório FGetStatusPrevisaoFluxoCaixaRepository
        /// </summary>
        public FGetStatusPrevisaoFluxoCaixaRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>        
        public IQueryable<FGetStatusPrevisaoFluxoCaixaDomain> ExecFunction(int codigoStatusAtual)
        {
            string query = string.Format("SELECT CodigoStatusPrevisaoFluxoCaixa, DescricaoStatusPrevisaoFluxoCaixa FROM [{0}].[{1}].[f_getStatusPrevisaoFluxoCaixa] ({2})",
                Core.Data.DatabaseInfo.GetDatabaseNameSqlServer(this.UowRepository.GetStringConnection()),
                Core.Data.DatabaseInfo.GetOwnerBb(),
                codigoStatusAtual);

            return this.GetUsingSqlQuery(query);
        }

        #endregion
    }
}
