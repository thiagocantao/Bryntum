using Cdis.Brisk.Domain.Domains.DataBaseObject.Function;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.Function;
using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.Service.Services.DataBaseObject.Function
{
    /// <summary>
    /// Classe de serviço FGetCarteirasEntidadeService
    /// </summary>
    public class FGetCarteirasEntidadeService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço FCronoGetVersoesLbProjetoService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço FCronoGetVersoesLbProjetoService
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
        /// Construtor da classe de serviço FCronoGetVersoesLbProjetoService
        /// </summary>
        public FGetCarteirasEntidadeService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>                
        public List<FGetCarteirasEntidadeDomain> GetListFGetCarteirasEntidade(int codEntidade)
        {
            return UowService.UowRepository.GetUowRepository<FGetCarteirasEntidadeRepository>()
                   .ExecFunction(codEntidade)                   
                   .ToList();
        }
        #endregion
    }
}
