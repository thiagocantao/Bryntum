using Cdis.Brisk.Domain.Domains.Entities;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.Estrategia;

namespace Cdis.Brisk.Service.Services.Estrategia
{
    /// <summary>
    /// Classe de serviço ObjetoEstrategiaService
    /// </summary>
    public class ObjetoEstrategiaService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço ObjetoEstrategiaService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço ObjetoEstrategiaService
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
        /// Construtor da classe de serviço ObjetoEstrategiaService
        /// </summary>
        public ObjetoEstrategiaService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Buscar o ObjetoEstrategia pelo código
        /// </summary>        
        public ObjetoEstrategiaDomain GetObjetoEstrategiaPorCodigo(int codIndicador)
        {
            return UowService.UowRepository.GetUowRepository<ObjetoEstrategiaRepository>().GetSingleById(codIndicador);
        }
        #endregion
    }
}
