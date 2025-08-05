using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure.RelatorioMinisterio;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.StoredProcedure.RelatorioMinisterio
{
    public class PSescoopRelMinisterioReceitaRepository : RepositoryBaseQuery<PSescoopRelMinisterioReceitaDomain>, IRepository
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
        public PSescoopRelMinisterioReceitaRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }

        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>        
        public IQueryable<PSescoopRelMinisterioReceitaDomain> ExecStoredProcedure(string codigoColigada, int ano1, int ano2, int ano3)
        {
            string query = string.Format("EXEC [{0}].[{1}].[p_sescoop_relMinisterio_receita] {2}, {3}, {4}, {5}",
                Core.Data.DatabaseInfo.GetDatabaseNameSqlServer(this.UowRepository.GetStringConnection()),
                Core.Data.DatabaseInfo.GetOwnerBb(),
                codigoColigada, ano1, ano2, ano3);

            return this.GetUsingSqlQuery(query);
        }
        #endregion
    }
}
