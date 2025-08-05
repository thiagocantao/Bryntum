using Cdis.Brisk.Domain.Domains.Usuario;
using Cdis.Brisk.Infra.Core.Interface;
using System.Data.Entity.ModelConfiguration;

namespace Cdis.Brisk.Infra.Data.Configurations.Usuario
{
    /// <summary>
    /// Classe de configuração UsuarioEsqueceuSenhaConfiguration
    /// </summary>
    public class UsuarioEsqueceuSenhaConfiguration : EntityTypeConfiguration<UsuarioEsqueceuSenhaDomain>, IDomainConfig
    {
        public UsuarioEsqueceuSenhaConfiguration()
        {
            this.HasKey(t => t.CodigoUsuarioEsqueceuSenha);

            this.Property(t => t.Chave).IsRequired();

            // Relationships                       
            this.HasRequired(t => t.Usuario)
               .WithMany(t => t.ListUsuarioEsqueceuSenha)
               .HasForeignKey(d => d.CodigoUsuario);

            // Table & Column Mappings
            this.ToTable("UsuarioEsqueceuSenha");
        }
    }
}
