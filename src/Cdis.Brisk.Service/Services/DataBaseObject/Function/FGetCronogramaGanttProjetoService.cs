using Cdis.Brisk.Domain.Domains.DataBaseObject.Function;
using Cdis.Brisk.Domain.Domains.DataBaseObject.Function.Param;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.Function;
using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.Service.Services.DataBaseObject.Function
{
    /// <summary>
    /// Classe de serviço FGetCronogramaGanttProjetoService
    /// </summary>
    public class FGetCronogramaGanttProjetoService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço FGetCronogramaGanttProjetoService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço FGetCronogramaGanttProjetoService
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
        /// Construtor da classe de serviço FGetCronogramaGanttProjetoService
        /// </summary>
        public FGetCronogramaGanttProjetoService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Listar as informações do cronograma
        /// </summary>        
        public List<FGetCronogramaGanttProjetoDomain> GetListPrevisaoFluxoCaixa(ParamFGetCronogramaGanttProjetoDomain param)
        {
            return UowService.UowRepository.GetUowRepository<FGetCronogramaGanttProjetoRepository>().ExecFunction(param).ToList();
        }
        #endregion
    }
}
