using Cdis.Brisk.Domain.Domains.Cronograma;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.Cronograma;
using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.Service.Services.Cronograma
{
    /// <summary>
    /// Classe de serviço CronogramaBalanceamentoService
    /// </summary>
    public class CronogramaBalanceamentoService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço CronogramaBalanceamentoService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço CronogramaBalanceamentoService
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
        /// Construtor da classe de serviço CronogramaBalanceamentoService
        /// </summary>
        public CronogramaBalanceamentoService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Listar o cronograma de balanceamento.
        /// </summary>        
        public List<CronogramaBalanceamentoDomain> GetListCronogramaBalanceamento(int codEntidade, int codPortfolio, int numCenario)
        {
            return UowService.UowRepository.GetUowRepository<CronogramaBalanceamentoRepository>().GetListCronogramaBalanceamento(codEntidade, codPortfolio, numCenario).ToList();
        }
        #endregion
    }
}
