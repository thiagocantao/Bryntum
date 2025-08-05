using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.Repository.Repositories._Common;

namespace Cdis.Brisk.Service.Services._Common
{
    /// <summary>
    /// Classe de serviço TextoPadraoSistemaService
    /// </summary>
    public class TextoPadraoSistemaService : IService
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de serviço TextoPadraoSistemaService
        /// </summary>
        private readonly UnitOfWorkService _unitOfWorkService;

        /// <summary>
        /// Propriedade pública da unit of work da classe de serviço TextoPadraoSistemaService
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
        /// Construtor da classe de serviço TextoPadraoSistemaService
        /// </summary>
        public TextoPadraoSistemaService(UnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Buscar o html correspondente ao esquecimento de senha
        /// </summary>        
        public string GetTextoPadraoSistemaHtmlEsqueceuSenha(string nomeUsuario, string loginUsuario, string urlRedefinicaoSenha)
        {
            string textoFormatado = "";
            var textoPadraoSistema = UowService.UowRepository.GetUowRepository<TextoPadraoSistemaRepository>().GetFirstOrDefault(t => t.IniciaisTexto == "CorpoRedefSenha");
            if (textoPadraoSistema != null)
            {
                textoFormatado = textoPadraoSistema.Texto
                    .Replace("@NomeUsuario", nomeUsuario)
                    .Replace("@loginUsuario", loginUsuario)
                    .Replace("@LinkTokenRedefinicaoSenha", urlRedefinicaoSenha);
            }

            return textoFormatado;
        }


        /// <summary>
        /// Buscar o título padrão para o e-mail da funcionalidade esqueci a minha senha
        /// </summary>        
        public string GetTextoPadraoSistemaAssuntoEsqueceuSenha()
        {
            string textoAssunto = "";
            var textoPadraoSistema = UowService.UowRepository.GetUowRepository<TextoPadraoSistemaRepository>().GetFirstOrDefault(t => t.IniciaisTexto == "AssRedefSenha");
            if (textoPadraoSistema != null)
            {
                textoAssunto = textoPadraoSistema.Texto;
            }

            return textoAssunto;
        }

        #endregion
    }
}
