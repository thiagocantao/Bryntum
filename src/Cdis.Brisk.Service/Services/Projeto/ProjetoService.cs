using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.Projeto;
using System.Linq;

namespace Cdis.Brisk.Service.Services.Projeto
{
    /// <summary>
    /// Classe de serviço ProjetoService
    /// </summary>
    public class ProjetoService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço ProjetoService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço ProjetoService
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
        /// Construtor da classe de serviço ProjetoService
        /// </summary>
        public ProjetoService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Buscar o nome do projeto.
        /// </summary>        
        public string GetNomeProjeto(int codProjeto)
        {
            return UowService.UowRepository
                .GetUowRepository<ProjetoRepository>()
                .GetWhere(p => p.CodigoProjeto == codProjeto)
                .Select(p => p.NomeProjeto)
                .FirstOrDefault();
        }
        #endregion
    }
}
