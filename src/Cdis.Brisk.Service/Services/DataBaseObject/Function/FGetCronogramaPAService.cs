using Cdis.Brisk.Domain.Domains.DataBaseObject.Function;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.Function;
using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.Service.Services.DataBaseObject.Function
{
    /// <summary>
    /// Classe de serviço FGetCronogramaPAService
    /// </summary>
    public class FGetCronogramaPAService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço FGetCronogramaPAService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço FGetCronogramaPAService
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
        /// Construtor da classe de serviço FGetCronogramaPAService
        /// </summary>
        public FGetCronogramaPAService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>        
        public List<FGetCronogramaPADomain> GetListCronogramaPlanoAcao(int idPlanoAcao, string iniciaisObjeto, int codObjeto, int codEntidade)
        {
            return UowService
                    .UowRepository
                    .GetUowRepository<FGetCronogramaPARepository>()
                    .ExecFunction(iniciaisObjeto, codObjeto, codEntidade)
                    .Where(p => p.CodigoPlanoAcao == idPlanoAcao).ToList();
        }


        /// <summary>
        /// 
        /// </summary>        
        public List<FGetCronogramaPADomain> GetListCronogramaPlanoAcaoIniciativa(int idPlanoAcao, string iniciaisObjeto, int codObjetivoEstrategico, int codEntidade)
        {
            return UowService
                        .UowRepository
                        .GetUowRepository<FGetCronogramaPARepository>()
                        .ExecFunction(iniciaisObjeto, codObjetivoEstrategico, codEntidade)
                        .Where(p => p.CodigoPlanoAcao == idPlanoAcao)
                        .Union(UowService
                                .UowRepository
                                .GetUowRepository<FGetCronogramaPARepository>()
                                .ExecFunctionIniciativa(codObjetivoEstrategico))
                    .ToList();
        }

        /// <summary>
        /// 
        /// </summary>        
        public List<FGetCronogramaPADomain> GetListCronogramaPlanoAcaoEntidade(string iniciaisObjeto, int codObjeto, int codEntidade)
        {
            return UowService
                    .UowRepository
                    .GetUowRepository<FGetCronogramaPARepository>()
                    .ExecFunction(iniciaisObjeto, codObjeto, codEntidade).ToList();
        }


        /// <summary>
        /// 
        /// </summary>        
        public List<FGetCronogramaPADomain> GetListCronogramaPlanoAcaoIniciativaEntidade(string iniciaisObjeto, int codObjetivoEstrategico, int codEntidade)
        {
            return UowService
                        .UowRepository
                        .GetUowRepository<FGetCronogramaPARepository>()
                        .ExecFunction(iniciaisObjeto, codObjetivoEstrategico, codEntidade)
                        .Union(UowService
                                .UowRepository
                                .GetUowRepository<FGetCronogramaPARepository>()
                                .ExecFunctionIniciativa(codObjetivoEstrategico))
                    .ToList();
        }

        #endregion
    }
}
