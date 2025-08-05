using Cdis.Brisk.Infra.Data.Repository.Repositories._Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.UnitTest.UnitTest._Common
{
    [TestClass]
    public class TextoPadraoSistemaRepositoryUnitTest
    {
        [TestMethod]
        public void GetTextoPadraoSistemaCorpoRedefSenhaTestMethod()
        {
            using (var uow = new UnitOfWorkRepository<Cdis.Brisk.Infra.Data.DomainContext>("Data Source=10.61.35.3; Initial Catalog=brisk_dev_br; User ID=sacdis; Password=qualiton;MultipleActiveResultSets=True;"))
            {
                var textoPadraoSistema = uow.GetUowRepository<TextoPadraoSistemaRepository>().GetFirstOrDefault(t => t.IniciaisTexto == "CorpoRedefSenha");
                var textoPadraoSistema1 = uow.GetUowRepository<TextoPadraoSistemaRepository>().GetWhere(t => t.IniciaisTexto == "CorpoRedefSenha").ToList();
                var textoPadraoSistema2 = uow.GetUowRepository<TextoPadraoSistemaRepository>().GetSingleOrDefault(t => t.IniciaisTexto == "CorpoRedefSenha");
            }
        }
    }
}
