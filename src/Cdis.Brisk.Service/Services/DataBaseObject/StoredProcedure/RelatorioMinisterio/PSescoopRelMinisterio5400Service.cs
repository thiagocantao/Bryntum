using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure.RelatorioMinisterio;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.StoredProcedure.RelatorioMinisterio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdis.Brisk.Service.Services.DataBaseObject.StoredProcedure.RelatorioMinisterio
{
    public class PSescoopRelMinisterio5400Service : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço PSescoopRelMinisterio5400Service
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço PSescoopRelMinisterio5400Service
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
        /// Construtor da classe de serviço PSescoopRelMinisterio5400Service
        /// </summary>
        public PSescoopRelMinisterio5400Service(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Listar PSescoopRelMinisterio5400Domain
        /// </summary>        
        public List<PSescoopRelMinisterio5400Domain> GetListPSescoopRelMinisterio5400(string codigoColigada, int ano1, int ano2, int ano3)
        {
            return UowService.UowRepository.GetUowRepository<PSescoopRelMinisterio5400Repository>().ExecStoredProcedure(codigoColigada, ano1, ano2, ano3).ToList();
        }
        #endregion
    }
}
