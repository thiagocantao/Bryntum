using Cdis.Brisk.Domain.Domains.Entities;
using Cdis.Brisk.Domain.Generic;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.Cronograma;
using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.Service.Services.Cronograma
{
    /// <summary>
    /// Classe de serviço CronogramaProjetoService
    /// </summary>
    public class CronogramaProjetoService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço CronogramaProjetoService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço CronogramaProjetoService
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
        /// Construtor da classe de serviço CronogramaProjetoService
        /// </summary>
        public CronogramaProjetoService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Verificar se o projeto possui DataUltimaGravacaoDesktop
        /// </summary>        
        public List<CronogramaProjetoDomain> GetListDataUltimaGravacaoDesktopProjeto(int codigoProjeto)
        {
            return UowService.UowRepository.GetUowRepository<CronogramaProjetoRepository>().GetWhere(p => p.CodigoProjeto == codigoProjeto && p.DataUltimaGravacaoDesktop.HasValue).ToList();
        }

        /// <summary>
        /// Buscar CronogramaProjeto Bloqueado para edição
        /// </summary>        
        public CronogramaProjetoDomain GetCronogramaProjetoBloqueadoParaEdicao(int codEntidade, int codProjeto)
        {
            return UowService.UowRepository.GetUowRepository<CronogramaProjetoRepository>()
                    .GetWhereAsNoTracking(u =>
                        u.DataCheckoutCronograma.HasValue
                     && u.CodigoUsuarioCheckoutCronograma.HasValue
                     && !u.DataExclusao.HasValue
                     && u.CodigoEntidade == codEntidade
                     && u.CodigoProjeto == codProjeto)
                    .FirstOrDefault();
        }

        /// <summary>
        /// Debloquear cronograma
        /// </summary>        
        public ResultRequestDomain DesbloquearCronograma(int codProjeto, int codUsuario)
        {
            try
            {
                if (codProjeto == 0)
                {
                    return new ResultRequestDomain(null, false, "Dados não informados.");
                }
                UowService.UowRepository.GetUowRepository<CronogramaProjetoRepository>()
                    .GetWhere(u => !u.DataExclusao.HasValue && u.CodigoProjeto == codProjeto)
                    .Update(u => new CronogramaProjetoDomain { DataCheckoutCronograma = DateTime.Now, CodigoUsuarioCheckoutCronograma = codUsuario });
                return new ResultRequestDomain(null, true, "Cronograma desbloqueado com sucesso.");
            }
            catch (Exception ex)
            {
                return new ResultRequestDomain(ex);
            }
        }

        /// <summary>
        /// Bloquear cronograma
        /// </summary>        
        public ResultRequestDomain BloquearCronograma(int codProjeto, int codUsuario)
        {
            try
            {
                if (codProjeto == 0 || codUsuario == 0)
                {
                    return new ResultRequestDomain(null, false, "Dados não informados.");
                }
                UowService.UowRepository.GetUowRepository<CronogramaProjetoRepository>()
                     .GetWhere(u => u.CodigoProjeto == codProjeto)
                     .Update(u => new CronogramaProjetoDomain { DataCheckoutCronograma = DateTime.Now, CodigoUsuarioCheckoutCronograma = codUsuario });
                return new ResultRequestDomain(null, true, "Cronograma bloqueado com sucesso.");
            }
            catch (Exception ex)
            {
                return new ResultRequestDomain(ex);
            }

        }
        #endregion
    }
}
