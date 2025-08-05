using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cdis.Brisk.Service.UnitTest.UnitTest.Cronograma
{
    [TestClass]
    public class CronogramaProjetoServiceUnitTest
    {
        [TestMethod]
        public void GetListEtapasPrevisaoOrcamentariaUnitTest()
        {
            using (var uow = new UnitOfWorkService("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                //uow.GetUowService<CronogramaProjetoService>().DesbloquearCronograma(1009);
            }
        }
    }
}
