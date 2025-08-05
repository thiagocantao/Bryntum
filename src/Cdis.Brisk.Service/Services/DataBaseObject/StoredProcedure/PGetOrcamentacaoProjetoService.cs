using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure;
using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure.Param;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.StoredProcedure;
using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.Service.Services.DataBaseObject.StoredProcedure
{
    public class PGetOrcamentacaoProjetoService : IService
    {
        #region Properties
        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço CategoriaService
        /// </summary>
        private UnitOfWorkService UowService { get; }

        #endregion

        #region Constructor        
        /// <summary>
        /// Construtor da classe de serviço CategoriaService
        /// </summary>
        public PGetOrcamentacaoProjetoService(UnitOfWorkService unitOfWorkService)
        {
            UowService = unitOfWorkService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Listar todas as despesas
        /// </summary>        
        public List<PGetOrcamentacaoProjetoDomain> GetListDespesaPGetOrcamentacaoProjeto(ParamPGetOrcamentacaoProjetoDomain param)
        {
            var list = UowService.UowRepository.GetUowRepository<PGetOrcamentacaoProjetoRepository>().ExecStoredProcedure(param, "D").ToList();
            return list ?? new List<PGetOrcamentacaoProjetoDomain>();
        }

        /// <summary>
        /// Listar todas as receitas
        /// </summary>        
        public List<PGetOrcamentacaoProjetoDomain> GetListReceitaPGetOrcamentacaoProjeto(ParamPGetOrcamentacaoProjetoDomain param)
        {
            var list = UowService.UowRepository.GetUowRepository<PGetOrcamentacaoProjetoRepository>().ExecStoredProcedure(param, "R").ToList();
            return list ?? new List<PGetOrcamentacaoProjetoDomain>();
        }
        #endregion
    }
}
