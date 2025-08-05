using Cdis.Brisk.Infra.Data.Repository.Repositories.UnidadeNegocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.UnitTest.UnitTest.UnidadeNegocio
{
    [TestClass]
    public class UnidadeNegocioRepositoryUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var uow = new UnitOfWorkRepository<Cdis.Brisk.Infra.Data.DomainContext>("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                var test = uow.GetUowRepository<UnidadeNegocioRepository>().GetWhere(u => u.IndicaUnidadeNegocioAtiva == "S" && !u.DataExclusao.HasValue).ToList();
            }
        }
    }
}
