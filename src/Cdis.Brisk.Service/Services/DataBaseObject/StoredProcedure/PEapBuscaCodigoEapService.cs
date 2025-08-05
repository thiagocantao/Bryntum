using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.StoredProcedure;
using System.Linq;

namespace Cdis.Brisk.Service.Services.DataBaseObject.StoredProcedure
{
    /// <summary>
    /// Classe de serviço PEapBuscaCodigoEapService
    /// </summary>
    public class PEapBuscaCodigoEapService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço PEapBuscaCodigoEapService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço PEapBuscaCodigoEapService
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
        /// Construtor da classe de serviço PEapBuscaCodigoEapService
        /// </summary>
        public PEapBuscaCodigoEapService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Buscar as informações da EAP
        /// </summary>        
        public PEapBuscaCodigoEapDomain GetEapBuscaCodigoEap(int codProjeto)
        {
            return UowService.UowRepository.GetUowRepository<PEapBuscaCodigoEapRepository>().ExecStoredProcedure(codProjeto).FirstOrDefault();
        }
        #endregion
    }
}
