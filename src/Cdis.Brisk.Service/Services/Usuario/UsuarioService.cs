using Cdis.Brisk.Domain.Domains.Entities;
using Cdis.Brisk.Domain.Domains.Usuario;
using Cdis.Brisk.Domain.Generic;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.Usuario;
using System;
using System.Transactions;

namespace Cdis.Brisk.Service.Services.Usuario
{
    /// <summary>
    /// Classe de serviço UsuarioService
    /// </summary>
    public class UsuarioService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço UsuarioService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço UsuarioService
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
        /// Construtor da classe de serviço UsuarioService
        /// </summary>
        public UsuarioService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods       

        /// <summary>
        /// Buscar o usuário pelo código
        /// </summary>        
        public UsuarioDomain GetUsuario(int codUsuario)
        {
            return UowService.UowRepository.GetUowRepository<UsuarioRepository>().GetSingleById(codUsuario);
        }

        /// <summary>
        /// Buscar o usuário pelo email
        /// </summary>        
        public UsuarioDomain GetUsuarioPorEmail(string email)
        {
            return UowService.UowRepository.GetUowRepository<UsuarioRepository>().GetFirstOrDefault(u => u.EMail == email);
        }

        /// <summary>
        /// Salva a nova senha do usuário.
        /// Por enquanto o método de geração da senha está na classe dados.cs no método ObtemCodigoHash.
        /// Quando as funcionalidades da dados.cs forem migradas para as camadas , a senha será criptografada nesse método.
        /// </summary>                    
        public ResultRequestDomain SalvarNovaSenha(string chave, int hashSenha)
        {
            try
            {
                UsuarioEsqueceuSenhaDomain usuarioEsqueceuSenha = UowService.GetUowService<UsuarioEsqueceuSenhaService>().GetUsuarioEsqueceuSenhaPorChave(chave);

                if (usuarioEsqueceuSenha.Usuario != null)
                {
                    usuarioEsqueceuSenha.Usuario.SenhaAcessoAutenticacaoSistema = hashSenha.ToString();
                    usuarioEsqueceuSenha.Usuario.DataUltimaAlteracao = DateTime.Now;
                    using (var scope = new TransactionScope())
                    {
                        UowService.UowRepository.GetUowRepository<UsuarioRepository>().Save(usuarioEsqueceuSenha.Usuario);
                        UowService.GetUowService<UsuarioEsqueceuSenhaAtualizacaoService>().SalvarAtualizacaoSenha(usuarioEsqueceuSenha.CodigoUsuarioEsqueceuSenha);

                        scope.Complete();
                    }

                    return new ResultRequestDomain(null, true, "Operação realizada com sucesso");
                }
                else
                {
                    return new ResultRequestDomain(null, false, "Usuário não cadastrado");
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        #endregion
    }
}
