using Cdis.Brisk.Domain.Domains.Projeto;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.Projeto;
using System.Linq;

namespace Cdis.Brisk.Service.Services.Projeto
{
    public class ProjetoUnidadeService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço CronogramaDependenciaService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço CronogramaDependenciaService
        /// </summary>
        private UnitOfWorkService UowService
        {
            get
            {
                return _unitOfWorkService;
            }
        }

        #endregion

        #region Constructor        
        /// <summary>
        /// Construtor da classe de serviço CronogramaDependenciaService
        /// </summary>
        public ProjetoUnidadeService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Listar todas as dependências do Cronograma
        /// </summary>        
        public ProjetoUnidadeDomain GetProjetoUnidade(int codigoProjeto)
        {
            return UowService.UowRepository.GetUowRepository<ProjetoUnidadeRepository>().GetProjetoUnidade(codigoProjeto).FirstOrDefault();
        }
        #endregion
    }
}
