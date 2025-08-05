using Cdis.Brisk.Domain.Domains.DataBaseObject.Function;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.Function
{
    /// <summary>
    /// Classe de repositório FCronoGetVersoesLbProjetoRepository
    /// </summary>
    public class FCronoGetVersoesLbProjetoRepository : RepositoryBaseQuery<FCronoGetVersoesLbProjetoDomain>, IRepository
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de repositório FCronoGetVersoesLbProjetoRepository
        /// </summary>
        private readonly UnitOfWorkRepository<DomainContext> _unitOfWorkRepository;

        /// <summary>
        /// Propriedade pública da unit of work da classe de repositório FCronoGetVersoesLbProjetoRepository
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
        /// Construtor da classe de repositório FCronoGetVersoesLbProjetoRepository
        /// </summary>
        public FCronoGetVersoesLbProjetoRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }

        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>        
        public IQueryable<FCronoGetVersoesLbProjetoDomain> ExecFunction(int codProjeto)
        {
            string query = string.Format(
                    @"SELECT 
                        lb.Anotacoes,
					    lb.CodigoCronogramaProjeto,
					    lb.DataSolicitacao,
					    lb.DataStatusAprovacao,
					    lb.ModeloLinhaBase,
					    lb.NomeAprovador,
					    lb.NomeSolicitante,
					    lb.NumeroVersao,
					    lb.StatusAprovacao,
					    lb.VersaoLinhaBase
                     FROM [{0}].[{1}].[f_crono_GetVersoesLBProjeto] ({2}) as lb",
                    Core.Data.DatabaseInfo.GetDatabaseNameSqlServer(this.UowRepository.GetStringConnection()),
                    Core.Data.DatabaseInfo.GetOwnerBb(),
                    codProjeto);

            return this.GetUsingSqlQuery(query);
        }
        #endregion
    }
}
