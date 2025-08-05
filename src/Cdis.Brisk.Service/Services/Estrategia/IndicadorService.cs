using Cdis.Brisk.Domain.Domains.Entities;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.Estrategia;

namespace Cdis.Brisk.Service.Services.Estrategia
{
    /// <summary>
    /// Classe de serviço IndicadorService
    /// </summary>
    public class IndicadorService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço IndicadorService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço IndicadorService
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
        /// Construtor da classe de serviço IndicadorService
        /// </summary>
        public IndicadorService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Buscar o indicador pelo código
        /// </summary>        
        public IndicadorDomain GetIndicadorPorCodigo(int codIndicador)
        {
            return UowService.UowRepository.GetUowRepository<IndicadorRepository>().GetSingleById(codIndicador);
        }
        #endregion
    }
}
