using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.StoredProcedure
{
    /// <summary>
    /// Classe de repositório PEapBuscaCodigoEapRepository
    /// </summary>
    public class PEapBuscaCodigoEapRepository : RepositoryBaseQuery<PEapBuscaCodigoEapDomain>, IRepository
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de repositório PEapBuscaCodigoEapRepository
        /// </summary>
        private readonly UnitOfWorkRepository<DomainContext> _unitOfWorkRepository;

        /// <summary>
        /// Propriedade pública da unit of work da classe de repositório PEapBuscaCodigoEapRepository
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
        /// Construtor da classe de repositório PEapBuscaCodigoEapRepository
        /// </summary>
        public PEapBuscaCodigoEapRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }

        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>        
        public IQueryable<PEapBuscaCodigoEapDomain> ExecStoredProcedure(int codProjeto)
        {
            string query = string.Format("EXEC [{0}].[{1}].[p_eap_BuscaCodigoEAP] {2}",
                Core.Data.DatabaseInfo.GetDatabaseNameSqlServer(this.UowRepository.GetStringConnection()),
                Core.Data.DatabaseInfo.GetOwnerBb(),
                codProjeto);

            return this.GetUsingSqlQuery(query);
        }
        #endregion
    }
}
