using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure.Param;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.StoredProcedure;

namespace Cdis.Brisk.Service.Services.DataBaseObject.StoredProcedure
{
    public class PGravaRegistroPrevisaoOrcamentariaProjetoService : IService
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
        public PGravaRegistroPrevisaoOrcamentariaProjetoService(UnitOfWorkService unitOfWorkService)
        {
            UowService = unitOfWorkService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Atualizar valores orçamentários anual        
        /// </summary>
        public void AtualizarValorPrevisaoOrcamentaria(ParamValorOrcamentarioDomain param)
        {
            ParamPGravaRegistroPrevisaoOrcamentariaProjetoDomain paramPrevisao = new ParamPGravaRegistroPrevisaoOrcamentariaProjetoDomain
            {
                CodigoConta = param.CodigoConta,
                CodigoProjeto = param.CodigoProjeto,
                Ano = param.Ano,
                Mes = param.Mes,
                CodigoPrevisao = param.CodigoPrevisao,
                ValorPrevisto = param.ValorPrevisto,
                CodigoPeriodicidade = 1,
                TipoPrevisaoOrcamentaria = "P",
                CodigoWorkflow = param.CodigoWorkflow,
                CodigoInstanciaWF = param.CodigoInstanciaWF
            };
            UowService.UowRepository.GetUowRepository<PGravaRegistroPrevisaoOrcamentariaProjetoRepository>().ExecStoredProcedure(paramPrevisao);
        }
        #endregion
    }
}
