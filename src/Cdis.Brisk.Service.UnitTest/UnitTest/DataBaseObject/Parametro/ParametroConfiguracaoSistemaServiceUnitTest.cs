using Cdis.Brisk.Service.Services.Parametro;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.Service.UnitTest.UnitTest.DataBaseObject.Parametro
{
    [TestClass]
    public class ParametroConfiguracaoSistemaServiceUnitTest
    {
        [TestMethod]
        public void GetListParametroEntidadeTestMethod()
        {
            using (var uow = new UnitOfWorkService("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                var list = uow.GetUowService<ParametroConfiguracaoSistemaService>().GetListParametroEntidade(111).ToList();
            }
        }

        [TestMethod]
        public void GetListParametroEntidadeParamTestMethod()
        {
            using (var uow = new UnitOfWorkService("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                var list = uow.GetUowService<ParametroConfiguracaoSistemaService>().GetListParametroEntidade(111, "utilizaCronoInstalado", "utilizaNovaEAP").ToList();

                List<string> listParamStr = new List<string> { "utilizaCronoInstalado", "utilizaNovaEAP" };
                var listParam = uow.GetUowService<ParametroConfiguracaoSistemaService>().GetListParametroEntidade(111, listParamStr).ToList();

                Assert.IsTrue(list.Count == listParam.Count);
            }
        }
    }
}
