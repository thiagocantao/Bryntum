using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;

namespace Cdis.Brisk.Infra.Data
{
    /// <summary>
    /// Classe de contexto
    /// </summary>
    public class DomainContext : DbContext
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public DomainContext(string strCon) : base(strCon)
        //: base("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678")
        {
            //

        }

        /// <summary>
        /// Definir as configurações banco de dados
        /// </summary>        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            ///--->todo -> Incluir esse trecho em um pacote Nuget separado
            ///--->Cdis.Infra.Data.Config.ConfigurationDomain.SetListConfigurations(modelBuilder);               
            modelBuilder.Configurations.AddFromAssembly(this.GetType().Assembly);
            ///<---

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(50));
        }

        private void ExecuteBefore()
        {

        }

        /// <summary>
        /// Efetuar alguma ação antes de concretizar o Save da entidade
        /// </summary>        
        public override int SaveChanges()
        {
            ExecuteBefore();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            ExecuteBefore();
            return base.SaveChangesAsync();
        }

    }
}
