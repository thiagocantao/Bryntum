using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure;
using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure.Param;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.StoredProcedure
{
    public class PGetOrcamentacaoProjetoRepository : RepositoryBaseQuery<PGetOrcamentacaoProjetoDomain>, IRepository
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
        public PGetOrcamentacaoProjetoRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>        
        public IQueryable<PGetOrcamentacaoProjetoDomain> ExecStoredProcedure(ParamPGetOrcamentacaoProjetoDomain param, string indicaReceitaDespesa)
        {
            string query = string.Format("EXEC [{0}].[{1}].[p_GetOrcamentacaoProjeto]  {2}, {3}, NULL, '{4}', {5}, {6}",
                Core.Data.DatabaseInfo.GetDatabaseNameSqlServer(this.UowRepository.GetStringConnection()),
                Core.Data.DatabaseInfo.GetOwnerBb(),
                param.CodigoEntidade,
                param.CodigoProjeto,
                indicaReceitaDespesa,
                param.CodigoWorkflow.HasValue ? param.CodigoWorkflow.ToString() : "NULL",
                param.CodigoInstanciaWF.HasValue ? param.CodigoInstanciaWF.ToString() : "NULL");

            return this.GetUsingSqlQuery(query);
        }

        #endregion
    }
}
