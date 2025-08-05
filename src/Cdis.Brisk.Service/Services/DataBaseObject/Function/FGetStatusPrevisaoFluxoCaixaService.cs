using Cdis.Brisk.Domain.Domains.DataBaseObject.Function;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.Function;
using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.Service.Services.DataBaseObject.Function
{
    public class FGetStatusPrevisaoFluxoCaixaService : IService
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
        public FGetStatusPrevisaoFluxoCaixaService(UnitOfWorkService unitOfWorkService)
        {
            UowService = unitOfWorkService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Listar a previsão anterior e a próxima previsão a partir do código atual
        /// </summary>        
        public List<FGetStatusPrevisaoFluxoCaixaDomain> GetListPrevisaoFluxoCaixa(int codigoStatusAtual)
        {
            return UowService.UowRepository.GetUowRepository<FGetStatusPrevisaoFluxoCaixaRepository>().ExecFunction(codigoStatusAtual).ToList();
        }

        #endregion
    }
}
