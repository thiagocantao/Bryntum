using Cdis.Brisk.Domain.Domains.Entities;
using Cdis.Brisk.Domain.Domains.Usuario;
using Cdis.Brisk.Domain.Generic;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.StoredProcedure;
using Cdis.Brisk.Infra.Data.Repository.Repositories.Usuario;
using Cdis.Brisk.Service.Services._Common;
using Cdis.Brisk.Service.Services.DataBaseObject.StoredProcedure;
using Cdis.Brisk.Service.Services.Parametro;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Cdis.Brisk.Service.Services.Usuario
{
    /// <summary>
    /// Classe de serviço UsuarioEsqueceuSenhaService
    /// </summary>
    public class UsuarioEsqueceuSenhaService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço UsuarioEsqueceuSenhaService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;



        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço UsuarioEsqueceuSenhaService
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
        /// Construtor da classe de serviço UsuarioEsqueceuSenhaService
        /// </summary>
        public UsuarioEsqueceuSenhaService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>        
        public UsuarioEsqueceuSenhaDomain GetUsuarioEsqueceuSenhaPorChave(string chave)
        {
            return UowService.UowRepository
                    .GetUowRepository<UsuarioEsqueceuSenhaRepository>()
                    .GetWhere(f => f.Chave == chave)
                    .Include(u => u.Usuario)
                    .Include(u => u.UsuarioEsqueceuSenhaAtualizacao)
                    .FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>        
        public UsuarioEsqueceuSenhaDomain GetUsuarioEsqueceuSenhaPorChaveValida(string chave)
        {
            return UowService.UowRepository
                    .GetUowRepository<UsuarioEsqueceuSenhaRepository>()
                    .GetWhere(f => f.Chave == chave && f.UsuarioEsqueceuSenhaAtualizacao == null)
                    .Include(u => u.Usuario)
                    .FirstOrDefault();
        }

        /// <summary>
        /// Buscar a última solicitação válida para o esquecimento de senha
        /// </summary>        
        public UsuarioEsqueceuSenhaDomain GetUsuarioEsqueceuSenhaUltimaChaveValidaPorCodigoUsuario(int codUsuario)
        {
            return UowService.UowRepository
                    .GetUowRepository<UsuarioEsqueceuSenhaRepository>()
                    .GetWhere(f =>
                            f.CodigoUsuario == codUsuario
                            && DateTime.Now < f.DataExpiracao
                            && f.UsuarioEsqueceuSenhaAtualizacao == null)
                    .Include(u => u.Usuario)
                    .OrderByDescending(u => u.DataExpiracao)
                    .FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>        
        public bool GetIsChaveValida(string chave)
        {
            var usuarioEsqueceuSenha = GetUsuarioEsqueceuSenhaPorChaveValida(chave);
            DateTime dtAtual = DateTime.Now;
            bool isChaveValida = (usuarioEsqueceuSenha != null && dtAtual < usuarioEsqueceuSenha.DataExpiracao);

            return isChaveValida;
        }


        /// <summary>
        /// 
        /// </summary>
        public ResultRequestDomain SolicitarEsqueciSenha(string emailUsuario, string urlRedefinicaoSenha)
        {
            try
            {

                UsuarioDomain usuario = UowService.GetUowService<UsuarioService>().GetUsuarioPorEmail(emailUsuario);

                if (usuario != null)
                {
                    var EnviarEmailPeloBancoDeDados = UowService.GetUowService<ParametroConfiguracaoSistemaService>().GetValorParametroSistema<Char>(usuario.CodigoEntidadeAcessoPadrao, "EnviarEmailPeloBancoDeDados");

                    int parametroMinutos = UowService.GetUowService<ParametroConfiguracaoSistemaService>().GetParametroSistemaTempoMinutosValidadeTokenRs(usuario.CodigoEntidadeAcessoPadrao);

                    UsuarioEsqueceuSenhaDomain usuarioEsqueceuSenha =
                        GetUsuarioEsqueceuSenhaUltimaChaveValidaPorCodigoUsuario(usuario.CodigoUsuario)
                        ?? new UsuarioEsqueceuSenhaDomain
                        {
                            Chave = Guid.NewGuid().ToString(),
                            CodigoUsuario = usuario.CodigoUsuario,
                            DataSolicitacao = DateTime.Now,
                            DataExpiracao = DateTime.Now.AddMinutes(parametroMinutos)
                        };

                    if (usuarioEsqueceuSenha.CodigoUsuarioEsqueceuSenha == 0)
                    {
                        UowService.UowRepository.GetUowRepository<UsuarioEsqueceuSenhaRepository>().Save(usuarioEsqueceuSenha);
                    }

                    if (EnviarEmailPeloBancoDeDados.ToString().ToUpper().Equals("N"))
                    {
                        //Enviar email
                        List<ParametroConfiguracaoSistemaDomain> listParametro = UowService.GetUowService<ParametroConfiguracaoSistemaService>().GetListParametroEntidadeInfoSmtp(usuario.CodigoEntidadeAcessoPadrao);
                        string bodyEmail = UowService.GetUowService<TextoPadraoSistemaService>().GetTextoPadraoSistemaHtmlEsqueceuSenha(usuario.NomeUsuario, usuario.EMail, urlRedefinicaoSenha + usuarioEsqueceuSenha.Chave);
                        string subjectEmail = UowService.GetUowService<TextoPadraoSistemaService>().GetTextoPadraoSistemaAssuntoEsqueceuSenha();
                        if (listParametro.Any(i => i.Parametro == "smtpUser")
                            && listParametro.Any(i => i.Parametro == "smtpServer")
                            && listParametro.Any(i => i.Parametro == "smtpPort")
                            && listParametro.Any(i => i.Parametro == "smtpPassword")
                            && listParametro.Any(i => i.Parametro == "smtpUtilizaSSL"))
                        {
                            Cdis.Brisk.Infra.Core.Util.EmailUtil.Enviar(new Infra.Core.Util.EmailUtil.InfoEmail
                            {
                                BodyEmail = bodyEmail,
                                EmailFrom = listParametro.FirstOrDefault(f => f.Parametro == "smtpUser").Valor,
                                SubjectEmail = subjectEmail,
                                ListEmailTo = new List<string> { emailUsuario },
                                NameHost = listParametro.FirstOrDefault(f => f.Parametro == "smtpServer").Valor,
                                NumberPort = Convert.ToInt32(listParametro.FirstOrDefault(f => f.Parametro == "smtpPort").Valor),
                                CredentialUser = listParametro.FirstOrDefault(f => f.Parametro == "smtpUser").Valor,
                                CredentialPass = listParametro.FirstOrDefault(f => f.Parametro == "smtpPassword").Valor,
                                IsSsl = listParametro.FirstOrDefault(f => f.Parametro == "smtpUtilizaSSL").Valor == "S"
                            });

                            return new ResultRequestDomain(null, true, "Operação realizada com sucesso");
                        }
                        else
                        {
                            throw new Exception("O sistema não possui informações sobre o smtp");
                        }
                    } else
                    {
                        //Enviar email
                        List<ParametroConfiguracaoSistemaDomain> listParametro = UowService.GetUowService<ParametroConfiguracaoSistemaService>().GetListParametroEntidadeInfoSmtp(usuario.CodigoEntidadeAcessoPadrao);
                        string bodyEmail = UowService.GetUowService<TextoPadraoSistemaService>().GetTextoPadraoSistemaHtmlEsqueceuSenha(usuario.NomeUsuario, usuario.EMail, urlRedefinicaoSenha + usuarioEsqueceuSenha.Chave);
                        string subjectEmail = UowService.GetUowService<TextoPadraoSistemaService>().GetTextoPadraoSistemaAssuntoEsqueceuSenha();


                        var x = UowService.GetUowService<PEnviaEmailService>().EnviaEmailPortal(new Domain.Domains.DataBaseObject.StoredProcedure.Param.ParamEnviaEmail
                        {
                            Body = bodyEmail
                             ,
                            EmailDestinatario = emailUsuario
                             ,
                            AssuntoEmail = subjectEmail
                             ,
                            EmailCopia = string.Empty
                             ,
                            Anexo = string.Empty
                             ,
                            CodigoEntidadeMaster = null

                        });

                        return new ResultRequestDomain(null, true, "Operação realizada com sucesso");
                    }

                    

                }

                return new ResultRequestDomain(null, false, "Usuário não cadastrado");
            }
            catch (Exception exc)
            {
                return new ResultRequestDomain(exc);
            }
        }

        #endregion
    }
}
        