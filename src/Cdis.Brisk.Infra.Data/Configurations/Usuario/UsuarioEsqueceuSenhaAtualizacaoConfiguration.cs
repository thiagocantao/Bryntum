using Cdis.Brisk.Domain.Domains.Usuario;
using Cdis.Brisk.Infra.Core.Interface;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Cdis.Brisk.Infra.Data.Configurations.Usuario
{
    /// <summary>
    /// Classe de configuração UsuarioEsqueceuSenhaAtualizacaoConfiguration
    /// </summary>
    public class UsuarioEsqueceuSenhaAtualizacaoConfiguration : EntityTypeConfiguration<UsuarioEsqueceuSenhaAtualizacaoDomain>, IDomainConfig
    {
        public UsuarioEsqueceuSenhaAtualizacaoConfiguration()
        {
            this.HasKey(t => t.CodigoUsuarioEsqueceuSenhaAtualizacao);
            this.Property(t => t.CodigoUsuarioEsqueceuSenhaAtualizacao).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Relationships            
            this.HasRequired(t => t.UsuarioEsqueceuSenha)
                .WithOptional(t => t.UsuarioEsqueceuSenhaAtualizacao);

            // Table & Column Mappings
            this.ToTable("UsuarioEsqueceuSenhaAtualizacao");
        }
    }
}
