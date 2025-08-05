using Cdis.Brisk.Domain.Domains.Cronograma;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.Cronograma;
using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.Service.Services.Cronograma
{
    /// <summary>
    /// Classe de serviço CronogramaDependenciaService
    /// </summary>
    public class CronogramaDependenciaService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço CronogramaDependenciaService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço CronogramaDependenciaService
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
        /// Construtor da classe de serviço CronogramaDependenciaService
        /// </summary>
        public CronogramaDependenciaService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Listar todas as dependências do Cronograma para o componente do Bryntum. No gráfico não é possível utilizar o id da tarefa como 0.
        /// </summary>        
        public List<CronogramaDependenciaDomain> GetListPrevisaoFluxoCaixaBryntum(int codigoProjeto)
        {
            return
                    UowService
                    .UowRepository
                    .GetUowRepository<CronogramaDependenciaRepository>()
                    .GetIQueryableCronogramaDependencia(codigoProjeto)
                    .Select(c => new CronogramaDependenciaDomain { FromTask = c.FromTask + 1, ToTask = c.ToTask + 1, TipoLatencia = c.TipoLatencia })
                    .ToList();
        }


        #endregion
    }
}
