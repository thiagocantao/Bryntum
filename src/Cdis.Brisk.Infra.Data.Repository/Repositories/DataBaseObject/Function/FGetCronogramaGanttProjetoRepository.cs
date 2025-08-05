using Cdis.Brisk.Domain.Domains.DataBaseObject.Function;
using Cdis.Brisk.Domain.Domains.DataBaseObject.Function.Param;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.Function
{
    /// <summary>
    /// Classe de repositório FGetCronogramaGanttProjetoRepository
    /// </summary>
    public class FGetCronogramaGanttProjetoRepository : RepositoryBaseQuery<FGetCronogramaGanttProjetoDomain>, IRepository
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de repositório FGetCronogramaGanttProjetoRepository
        /// </summary>
        private readonly UnitOfWorkRepository<DomainContext> _unitOfWorkRepository;

        /// <summary>
        /// Propriedade pública da unit of work da classe de repositório FGetCronogramaGanttProjetoRepository
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
        /// Construtor da classe de repositório FGetCronogramaGanttProjetoRepository
        /// </summary>
        public FGetCronogramaGanttProjetoRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }

        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>        
        public IQueryable<FGetCronogramaGanttProjetoDomain> ExecFunction(ParamFGetCronogramaGanttProjetoDomain param)
        {
            string query = string.Format(
                    @" SELECT * FROM [{0}].[{1}].[f_GetCronogramaGanttProjeto] ({2}, {3}, {4}, '{5}', '{6}', {7}, {8}) as f
                          ORDER BY 
		                        CodigoProjeto, 
		                        CodigoCronogramaProjeto, 
		                        SequenciaTarefaCronograma",
                    Core.Data.DatabaseInfo.GetDatabaseNameSqlServer(this.UowRepository.GetStringConnection()),
                    Core.Data.DatabaseInfo.GetOwnerBb(),
                    param.CodigoProjeto,
                    param.VersaoLinhaBase,
                    param.CodigoRecurso,
                    param.SoAtrasadas,
                    param.SoMarcos,
                    string.IsNullOrEmpty(param.PercentualConcluido) ? "NULL" : param.PercentualConcluido,
                    param.DataFiltro.HasValue ? param.DataFiltro.ToString() : "NULL");

            return this.GetUsingSqlQuery(query);
        }
        #endregion
    }
}
