using Cdis.Brisk.Domain.Domains.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cdis.Brisk.Domain.Domains.Usuario
{
    /// <summary>
    /// Tabela responsável por cadastrar o momento em o usuário informa que esqueceu a sua senha.
    /// </summary>
    /// <tableName>
    /// UsuarioEsqueceuSenhaDomain
    /// </tableName>    
    public class UsuarioEsqueceuSenhaDomain
    {
        /// <summary>
        /// Identificador único da tabela controlado pelo banco de dados (Identity).
        /// </summary>          
        [Key]
        public int CodigoUsuarioEsqueceuSenha { get; set; }

        /// <summary>
        /// Chave gerada pelo sistema Brisk para permitir que o usuário atualize a sua senha
        /// </summary>                  
        public string Chave { get; set; }

        /// <summary>
        /// Data e hora limite para a utilização do campo Chave
        /// </summary>                  
        public DateTime DataExpiracao { get; set; }
        /// <summary>
        /// Data e hora da solicitação da funcionalidade esqueci a minha senha
        /// </summary>                  
        public DateTime DataSolicitacao { get; set; }
        /// <summary>
        /// Chave estrangeira da tabela Usuario
        /// </summary>                  
        public int CodigoUsuario { get; set; }

        /// <summary>
        /// Objeto da tabela Usuario
        /// </summary>
        /// <noDesc/>        
        public UsuarioDomain Usuario { get; set; }

        /// <summary>
        /// UsuarioEsqueceuSenhaAtualizacao
        /// </summary>
        /// <noDesc/>
        public UsuarioEsqueceuSenhaAtualizacaoDomain UsuarioEsqueceuSenhaAtualizacao { get; set; }

    }
}
