using Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.Function;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.UnitTest.UnitTest.DataBaseObject.Function
{
    [TestClass]
    public class FOrcGetEtapasPrevisaoOrcamentariaRepositoryUnitTest
    {
        [TestMethod]
        public void ExecStoredProcedure()
        {
            using (var uow = new UnitOfWorkRepository<Cdis.Brisk.Infra.Data.DomainContext>("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                var test = uow.GetUowRepository<FOrcGetEtapasPrevisaoOrcamentariaRepository>().ExecFunction(111).ToList();
            }
        }
    }
}
