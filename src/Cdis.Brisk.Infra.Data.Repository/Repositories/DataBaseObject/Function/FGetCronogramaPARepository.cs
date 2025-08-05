using Cdis.Brisk.Domain.Domains.DataBaseObject.Function;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.Function
{
    /// <summary>
    /// Classe de repositório FGetCronogramaPARepository
    /// </summary>
    public class FGetCronogramaPARepository : RepositoryBaseQuery<FGetCronogramaPADomain>, IRepository
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de repositório FGetCronogramaPARepository
        /// </summary>
        private readonly UnitOfWorkRepository<DomainContext> _unitOfWorkRepository;

        /// <summary>
        /// Propriedade pública da unit of work da classe de repositório FGetCronogramaPARepository
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
        /// Construtor da classe de repositório FGetCronogramaPARepository
        /// </summary>
        public FGetCronogramaPARepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }

        #endregion

        #region Methods
        public IQueryable<FGetCronogramaPADomain> ExecFunction(string iniciaisObjeto, int codObjeto, int codEntidade)
        {
            string query = string.Format(
                    @"SELECT f.Descricao,
			                 f.Inicio,
			                 f.Termino,
			                 f.Responsavel,
			                 convert(decimal, f.PercentualConcluido) as PercentualConcluido,
			                 f.Status,
			                 f.Trabalho,
			                 f.Custo,
			                 f.EstruturaHierarquica,
			                 f.CodigoPlanoAcao,
			                 f.Nivel,
                             'Iniciativa' as Tipo
                     FROM [{0}].[{1}].[f_GetCronogramaPA]('{2}', {3}, {4}) as f",
                    Core.Data.DatabaseInfo.GetDatabaseNameSqlServer(this.UowRepository.GetStringConnection()),
                    Core.Data.DatabaseInfo.GetOwnerBb(),
                    iniciaisObjeto,
                    codObjeto,
                    codEntidade);

            return this.GetUsingSqlQuery(query);
        }

        /// <summary>
        /// Listar o plano de ação das iniciativas.
        /// </summary>        
        public IQueryable<FGetCronogramaPADomain> ExecFunctionIniciativa(int codObjetivoEstrategico)
        {
            string query = string.Format(UowRepository.GetUowRepository<FGetCronogramaObjetivoEstrategicoRepository>()._query,
                        @"  f.NomeTarefa as Descricao, 
                            f.Inicio, 
                            f.Termino, 
                            null as Responsavel, 
                            f.PercentualReal AS PercentualConcluido, 
                            null as Status, 
                            f.Trabalho, 
                            f.Custo, 
                            f.EstruturaHierarquica, 
                            convert(int, f.CodigoNumeroTarefa) as CodigoPlanoAcao, 
                            convert(smallint, f.Nivel + 1) as Nivel,
                            'Iniciativa' as Tipo",
                        Core.Data.DatabaseInfo.GetDatabaseNameSqlServer(this.UowRepository.GetStringConnection()),
                        Core.Data.DatabaseInfo.GetOwnerBb(),
                        codObjetivoEstrategico);

            return this.GetUsingSqlQuery(query);
        }
        #endregion
    }
}
