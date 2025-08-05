using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure;
using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure.Param;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.StoredProcedure;
using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.Service.Services.DataBaseObject.StoredProcedure
{
    public class PEnviaEmailService : IService
    {
        #region Properties
        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço EmailService
        /// </summary>
        private UnitOfWorkService UowService { get; }

        #endregion

        #region Constructor        
        /// <summary>
        /// Construtor da classe de serviço EmailService
        /// </summary>
        public PEnviaEmailService(UnitOfWorkService unitOfWorkService)
        {
            UowService = unitOfWorkService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Envia email traves de acionamento de procedure
        /// </summary>        
               
        public object EnviaEmailPortal(ParamEnviaEmail param)
        {
            var list = UowService.UowRepository.GetUowRepository<PEnviaEmailPortalRepository>().ExecStoredProcedure(param);
            return list;
        }
        #endregion
    }
}
