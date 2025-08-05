using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure.Param;
using Cdis.Brisk.Infra.Core.Interface;
using System.Data.Entity;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.StoredProcedure
{
    public class PGravaRegistroPrevisaoOrcamentariaProjetoRepository : RepositoryBase.RepositoryBase, IRepository
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
        public PGravaRegistroPrevisaoOrcamentariaProjetoRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Executar a stored procedure p_gravaRegistroPrevisaoOrcamentariaProjeto
        /// </summary>        
        public object ExecStoredProcedure(ParamPGravaRegistroPrevisaoOrcamentariaProjetoDomain param)
        {
            if (param.CodigoProjeto == 0)
            {
                throw new System.Exception("Código do projeto não informado");
            }

            if (param.CodigoPrevisao == 0)
            {
                throw new System.Exception("Código previsão não informado");
            }

            if (param.CodigoConta == 0)
            {
                throw new System.Exception("Código da conta não informado");
            }

            if (param.Ano == 0)
            {
                throw new System.Exception("Ano não informado");
            }

            if (param.Mes == 0)
            {
                throw new System.Exception("Mês não informado");
            }

            string query = string.Format("EXEC [{0}].[{1}].[p_gravaRegistroPrevisaoOrcamentariaProjeto] {2}, '{3}', {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}",
                Core.Data.DatabaseInfo.GetDatabaseNameSqlServer(this.UowRepository.GetStringConnection()),
                Core.Data.DatabaseInfo.GetOwnerBb(),
                param.CodigoProjeto,
                param.TipoPrevisaoOrcamentaria,
                param.CodigoConta,
                param.CodigoPessoaParticipe.HasValue ? param.CodigoPessoaParticipe.Value.ToString() : "NULL",
                param.CodigoPeriodicidade,
                param.Ano,
                param.Mes,
                param.ValorPrevisto.HasValue ? param.ValorPrevisto.Value.ToString().Replace(",", ".") : "NULL",
                param.CodigoPrevisao,
                param.CodigoWorkflow.HasValue ? param.CodigoWorkflow.ToString() : "NULL",
                param.CodigoInstanciaWF.HasValue ? param.CodigoInstanciaWF.ToString() : "NULL");

            return this.ExecSqlQuery(query);
        }

        #endregion
    }
}
