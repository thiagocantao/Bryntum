using Cdis.Brisk.Infra.Data.Repository.Repositories.Estrategia;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cdis.Brisk.Infra.Data.Repository.UnitTest.UnitTest.Estrategia
{
    [TestClass]
    public class IndicadorRepositoryUnitTest
    {
        [TestMethod]
        public void IndicadorRepository_GetIndicador()
        {
            using (var uow = new UnitOfWorkRepository<Cdis.Brisk.Infra.Data.DomainContext>("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                int codIndicador = 209;
                var test = uow.GetUowRepository<IndicadorRepository>().GetSingleById(codIndicador);
            }
        }
    }
}
