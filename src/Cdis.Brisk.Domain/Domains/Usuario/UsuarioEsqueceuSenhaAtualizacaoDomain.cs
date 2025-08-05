using System;
using System.ComponentModel.DataAnnotations;

namespace Cdis.Brisk.Domain.Domains.Usuario
{
    /// <summary>
    /// Tabela responsável por cadastrar o momento em que o usuário atualiza a sua senha após o envio do email na funcionalidade esqueci a minha senha.
    /// </summary>
    /// <tableName>
    /// UsuarioEsqueceuSenhaAtualizacao
    /// </tableName>    
    public class UsuarioEsqueceuSenhaAtualizacaoDomain
    {
        /// <summary>
        /// Identificador único da tabela, como é um relacionamento 1 para 1 o valor dessa coluna vem da tabela UsuarioEsqueceuSenha.
        /// </summary>          
        [Key]
        public int CodigoUsuarioEsqueceuSenhaAtualizacao { get; set; }

        /// <summary>
        /// Data e hora da atualização da senha
        /// </summary>                  
        public DateTime DthAtualizacao { get; set; }

        /// <summary>
        /// Objeto Referência da tabela UsuarioEsqueceuSenha
        /// </summary>
        /// <noDesc/>
        public UsuarioEsqueceuSenhaDomain UsuarioEsqueceuSenha { get; set; }
    }
}
