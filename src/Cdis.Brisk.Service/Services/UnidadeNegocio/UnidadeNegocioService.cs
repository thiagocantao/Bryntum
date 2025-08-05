using Cdis.Brisk.Domain.Domains.Entities;
using Cdis.Brisk.Domain.Domains.Entities.Simple;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.Function;
using Cdis.Brisk.Infra.Data.Repository.Repositories.UnidadeNegocio;
using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.Service.Services.UnidadeNegocio
{
    /// <summary>
    /// Classe de serviço UnidadeNegocioService
    /// </summary>
    public class UnidadeNegocioService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço UnidadeNegocioService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço UnidadeNegocioService
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
        /// Construtor da classe de serviço UnidadeNegocioService
        /// </summary>
        public UnidadeNegocioService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Listar as unidades de negócio da entidade que estão ativas e o usuário tem acesso
        /// </summary>        
        public List<UnidadeNegocioSimpleDomain> GetListUnidadeNegocioUsuario(int codUsuario, int codEntidade)
        {
            List<int> listCodUnidadeUsuario = UowService.UowRepository.GetUowRepository<FGetUnidadesUsuarioRepository>().ExecFunction(codUsuario, codEntidade).Select(f => f.CodigoUnidadeNegocio).Distinct().ToList();

            return UowService.UowRepository
                .GetUowRepository<UnidadeNegocioRepository>()
                .GetWhereAsNoTracking(u =>
                        !u.DataExclusao.HasValue
                      && u.IndicaUnidadeNegocioAtiva == "S"
                      && u.CodigoEntidade == codEntidade
                      && listCodUnidadeUsuario.Contains(u.CodigoUnidadeNegocio))
                .Select(u => new UnidadeNegocioSimpleDomain { CodigoUnidadeNegocio = u.CodigoUnidadeNegocio, NomeUnidadeNegocio = u.NomeUnidadeNegocio })
                .ToList();
        }

        /// <summary>
        /// Buscar a unidade coligada.
        /// </summary>
        public UnidadeNegocioDomain GetUnidadeNegocioColigada(int codEntidade)
        {
            return
                UowService.UowRepository
                .GetUowRepository<UnidadeNegocioRepository>()
                .GetFirstOrDefault(u => u.CodigoEntidade == codEntidade && u.CodigoEntidade == u.CodigoUnidadeNegocio && !string.IsNullOrEmpty(u.CodigoReservado));
        }

        #endregion
    }
}
