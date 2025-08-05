using Cdis.Brisk.Domain.Domains.Cronograma;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.Cronograma;
using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.Service.Services.Cronograma
{
    /// <summary>
    /// Classe de serviço CronogramaProjetoMetaService
    /// </summary>
    public class CronogramaProjetoMetaService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço CronogramaProjetoMetaService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço CronogramaProjetoMetaService
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
        /// Construtor da classe de serviço CronogramaProjetoMetaService
        /// </summary>
        public CronogramaProjetoMetaService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Listar os itens para o cronograma das metas do projeto
        /// </summary>        
        public List<CronogramaProjetoMetaDomain> GetListCronogramaProjetoMeta(int codEntidade, int codUsuario, int codCarteira)
        {
            return UowService.UowRepository
                    .GetUowRepository<CronogramaProjetoMetaRepository>()
                    .GetQueryListCronogramaProjetoMeta(codEntidade, codUsuario, codCarteira)
                    .ToList();
        }
        #endregion
    }
}
