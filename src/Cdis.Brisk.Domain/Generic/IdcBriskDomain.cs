namespace Cdis.Brisk.Domain.Generic
{
    public struct IdcBriskDomain
    {
        /// <summary>
        /// LogAcessoUsuario
        /// </summary>
        public struct LogAcessoUsuario
        {
            /// <summary>
            /// SituacaoAcesso
            /// </summary>
            public class SituacaoAcesso
            {
                /// <summary>
                /// Sucesso
                /// </summary>
                public struct Sucesso
                {
                    /// <summary>
                    /// Sigla
                    /// </summary>
                    public static readonly string IdcValue = "SC";
                    /// <summary>
                    /// Descrição
                    /// </summary>
                    public static readonly string IdcDescription = "Sucesso";
                }

                /// <summary>
                /// Errou a senha
                /// </summary>
                public struct ErrouASenha
                {
                    /// <summary>
                    /// Sigla
                    /// </summary>
                    public static readonly string IdcValue = "ES";
                    /// <summary>
                    /// Descrição
                    /// </summary>
                    public static readonly string IdcDescription = "Errou a senha";
                }

                /// <summary>
                /// Distribuicao
                /// </summary>
                public struct UsuarioSemCadastrado
                {
                    /// <summary>
                    /// Sigla
                    /// </summary>
                    public static readonly string IdcValue = "US";
                    /// <summary>
                    /// Descrição
                    /// </summary>
                    public static readonly string IdcDescription = "Usuário sem cadastrado";
                }

                /// <summary>
                /// Lista de valores possíveis
                /// </summary>
                //public virtual List<IIdc> ListIdc { get { return GetListBaseIdc<SituacaoAcesso>(); } }

                /// <summary>
                /// Lsita IdcTipoInstalacao
                /// </summary>
                //public static List<IIdc> ListStaticIdc { get { return new SituacaoAcesso().ListIdc; } }
            }

        }
    }
}
