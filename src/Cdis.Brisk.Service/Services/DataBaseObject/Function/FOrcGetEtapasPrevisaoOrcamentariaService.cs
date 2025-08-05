using Cdis.Brisk.Domain.Domains.DataBaseObject.Function;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.Function;
using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.Service.Services.DataBaseObject.Function
{
    public class FOrcGetEtapasPrevisaoOrcamentariaService : IService
    {
        #region Properties
        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço FOrcGetEtapasPrevisaoOrcamentariaService
        /// </summary>
        private UnitOfWorkService UowService { get; }

        #endregion

        #region Constructor        
        /// <summary>
        /// Construtor da classe de serviço FOrcGetEtapasPrevisaoOrcamentariaService
        /// </summary>
        public FOrcGetEtapasPrevisaoOrcamentariaService(UnitOfWorkService unitOfWorkService)
        {
            UowService = unitOfWorkService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Listar todas as previsões orçamentárias
        /// </summary>        
        public List<FOrcGetEtapasPrevisaoOrcamentariaDomain> GetListEtapasPrevisaoOrcamentaria(int codEntidade)
        {
            return UowService.UowRepository.GetUowRepository<FOrcGetEtapasPrevisaoOrcamentariaRepository>().ExecFunction(codEntidade).ToList();
        }

        #endregion
    }
}
