using Cdis.Brisk.Domain.Domains.DataBaseObject.Function;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.Function
{
    /// <summary>
    /// Classe de repositório FGetCronogramaObjetivoEstrategicoRepository
    /// </summary>
    public class FGetCronogramaObjetivoEstrategicoRepository : RepositoryBaseQuery<FGetCronogramaObjetivoEstrategicoDomain>, IRepository
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de repositório FGetCronogramaObjetivoEstrategicoRepository
        /// </summary>
        private readonly UnitOfWorkRepository<DomainContext> _unitOfWorkRepository;

        /// <summary>
        /// Propriedade pública da unit of work da classe de repositório FGetCronogramaObjetivoEstrategicoRepository
        /// </summary>
        private UnitOfWorkRepository<DomainContext> UowRepository
        {
            get
            {
                return _unitOfWorkRepository;
            }
        }


        public readonly string _query = @"SELECT
		                                        {0}
                                          FROM [{1}].[{2}].[f_GetCronogramaObjetivoEstrategico]({3}) f
			                              WHERE Inicio IS NOT NULL AND Termino IS NOT NULL
			                              ORDER BY EstruturaHierarquica, Inicio";

        #endregion

        #region Constructor

        /// <summary>
        /// Construtor da classe de repositório FGetCronogramaObjetivoEstrategicoRepository
        /// </summary>
        public FGetCronogramaObjetivoEstrategicoRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Executar a function f_GetCronogramaObjetivoEstrategico
        /// </summary>
        /// <returns></returns>
        public IQueryable<FGetCronogramaObjetivoEstrategicoDomain> ExecFunction(int codObjetivoEstrategico)
        {
            string query = string.Format(_query,
                        "f.*",
                        Core.Data.DatabaseInfo.GetDatabaseNameSqlServer(this.UowRepository.GetStringConnection()),
                        Core.Data.DatabaseInfo.GetOwnerBb(),
                        codObjetivoEstrategico);

            return this.GetUsingSqlQuery(query);
        }
        #endregion
    }
}
