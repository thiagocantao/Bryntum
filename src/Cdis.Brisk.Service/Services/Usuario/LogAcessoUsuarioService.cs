using Cdis.Brisk.Domain.Domains.Entities;
using Cdis.Brisk.Domain.Generic;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.Usuario;
using System;

namespace Cdis.Brisk.Service.Services.Usuario
{
    /// <summary>
    /// Classe de serviço LogAcessoUsuarioService
    /// </summary>
    public class LogAcessoUsuarioService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço LogAcessoUsuarioService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço LogAcessoUsuarioService
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
        /// Construtor da classe de serviço LogAcessoUsuarioService
        /// </summary>
        public LogAcessoUsuarioService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Esse método busca o usuário por e-mail caso ele tenha cadastro, informa no registro o código do usuário e o código da entidade, caso não encontre, 
        /// ele insere apenas o email informado.
        /// </summary>
        public void SalvarAcessoUsuarioPorEmail(string email)
        {
            UsuarioDomain usuario = UowService.GetUowService<UsuarioService>().GetUsuarioPorEmail(email);

            if (usuario == null)
            {
                SalvarAcessoSemEmailCadastrado(email);
            }
            else
            {
                SalvarAcessoErrouSenha(usuario.CodigoUsuario, usuario.CodigoEntidadeAcessoPadrao);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SalvarAcessoSemEmailCadastrado(string emailSemCadastro)
        {
            LogAcessoUsuarioDomain logAcesso = new LogAcessoUsuarioDomain
            {
                DataAcesso = DateTime.Now,
                EmailSemCadastro = emailSemCadastro,
                SituacaoAcesso = IdcBriskDomain.LogAcessoUsuario.SituacaoAcesso.UsuarioSemCadastrado.IdcValue
            };
            UowService.UowRepository.GetUowRepository<LogAcessoUsuarioRepository>().InsertLogAcessoUsuario(logAcesso);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SalvarAcessoErrouSenha(int codUsuario, int codEntidade)
        {
            LogAcessoUsuarioDomain logAcesso = new LogAcessoUsuarioDomain
            {
                DataAcesso = DateTime.Now,
                CodigoUsuario = codUsuario,
                CodigoEntidade = codEntidade,
                SituacaoAcesso = IdcBriskDomain.LogAcessoUsuario.SituacaoAcesso.ErrouASenha.IdcValue
            };
            UowService.UowRepository.GetUowRepository<LogAcessoUsuarioRepository>().InsertLogAcessoUsuario(logAcesso);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SalvarAcessoComSucesso(int codUsuario, int codEntidade)
        {
            LogAcessoUsuarioDomain logAcesso = new LogAcessoUsuarioDomain
            {
                DataAcesso = DateTime.Now,
                CodigoUsuario = codUsuario,
                CodigoEntidade = codEntidade,
                SituacaoAcesso = IdcBriskDomain.LogAcessoUsuario.SituacaoAcesso.Sucesso.IdcValue
            };
            UowService.UowRepository.GetUowRepository<LogAcessoUsuarioRepository>().InsertLogAcessoUsuario(logAcesso);
        }
        #endregion
    }
}
