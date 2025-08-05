using Cdis.Brisk.Service.Services.DataBaseObject.Function;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Cdis.Brisk.Service.UnitTest.UnitTest.DataBaseObject.Function
{
    [TestClass]
    public class FOrcGetEtapasPrevisaoOrcamentariaServiceUnitTest
    {
        [TestMethod]
        public void GetListEtapasPrevisaoOrcamentariaUnitTest()
        {
            using (var uow = new UnitOfWorkService("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                var list = uow.GetUowService<FOrcGetEtapasPrevisaoOrcamentariaService>().GetListEtapasPrevisaoOrcamentaria(111).ToList();
            }
        }
    }
}
