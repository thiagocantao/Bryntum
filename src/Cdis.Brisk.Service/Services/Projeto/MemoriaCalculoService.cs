using Cdis.Brisk.Domain.Domains.Entities;
using Cdis.Brisk.Domain.Generic;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.Projeto;
using System;

namespace Cdis.Brisk.Service.Services.Projeto
{
    /// <summary>
    /// Classe de serviço MemoriaCalculoService
    /// </summary>
    public class MemoriaCalculoService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço MemoriaCalculoService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço MemoriaCalculoService
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
        /// Construtor da classe de serviço MemoriaCalculoService
        /// </summary>
        public MemoriaCalculoService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Atualizar a descrição da MemoriaCalculo
        /// </summary>        
        public ResultRequestDomain AtualizarDescricaoMemoriaCalculo(string descricaoMemoriaCalculo, int? codMemoriaCalculo, int? codProjeto, int? codPrevisao, int? codConta)
        {
            try
            {
                if ((!codMemoriaCalculo.HasValue || codMemoriaCalculo == 0) && (!codProjeto.HasValue || !codPrevisao.HasValue || !codConta.HasValue))
                {
                    return new ResultRequestDomain(null, false, "Dados não informados.");
                }
                MemoriaCalculoDomain memoriaCalculo = new MemoriaCalculoDomain();
                if (codMemoriaCalculo.HasValue)
                {
                    memoriaCalculo = UowService.UowRepository.GetUowRepository<MemoriaCalculoRepository>().GetSingleById(codMemoriaCalculo.Value);
                    memoriaCalculo.DescricaoMemoriaCalculo = descricaoMemoriaCalculo;
                }
                else
                {
                    memoriaCalculo =
                     new MemoriaCalculoDomain
                     {
                         DescricaoMemoriaCalculo = descricaoMemoriaCalculo,
                         CodigoPrevisao = codPrevisao.Value,
                         CodigoProjeto = codProjeto.Value,
                         CodigoConta = codConta.Value
                     };
                }

                UowService.UowRepository.GetUowRepository<MemoriaCalculoRepository>().Save(memoriaCalculo);

                return new ResultRequestDomain(null, true, "Operação realizada com sucesso");
            }
            catch (Exception ex)
            {
                return new ResultRequestDomain(ex);
            }
        }
        #endregion
    }
}
