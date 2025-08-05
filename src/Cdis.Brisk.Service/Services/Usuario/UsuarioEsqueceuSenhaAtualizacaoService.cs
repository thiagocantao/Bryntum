using Cdis.Brisk.Domain.Domains.Usuario;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.Usuario;
using System;

namespace Cdis.Brisk.Service.Services.Usuario
{
    /// <summary>
    /// Classe de serviço UsuarioEsqueceuSenhaAtualizacaoService
    /// </summary>
    public class UsuarioEsqueceuSenhaAtualizacaoService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço UsuarioEsqueceuSenhaAtualizacaoService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço UsuarioEsqueceuSenhaAtualizacaoService
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
        /// Construtor da classe de serviço UsuarioEsqueceuSenhaAtualizacaoService
        /// </summary>
        public UsuarioEsqueceuSenhaAtualizacaoService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Salvar a data que a senha foi atualizada.
        /// </summary>        
        public void SalvarAtualizacaoSenha(int codUsuarioEsqueceuSenha)
        {
            UsuarioEsqueceuSenhaAtualizacaoDomain usuarioEsqueceuSenhaAtualizacao = new UsuarioEsqueceuSenhaAtualizacaoDomain
            {
                CodigoUsuarioEsqueceuSenhaAtualizacao = codUsuarioEsqueceuSenha,
                DthAtualizacao = DateTime.Now
            };
            UowService.UowRepository.GetUowRepository<UsuarioEsqueceuSenhaAtualizacaoRepository>().Save(usuarioEsqueceuSenhaAtualizacao);
        }
        #endregion
    }
}
