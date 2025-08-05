using Cdis.Brisk.Infra.Data.Repository.Repositories.Cronograma;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.UnitTest.UnitTest.Cronograma
{
    [TestClass]
    public class CronogramaBalanceamentoRepositoryUnitTest
    {
        [TestMethod]
        public void CronogramaBalanceamentoRepository_GetListBalanceamentoTestMethod()
        {
            using (var uow = new UnitOfWorkRepository<Cdis.Brisk.Infra.Data.DomainContext>("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                int codEntidade = 111;
                int codPortfolio = 51;
                int numCenario = 1;
                var test = uow.GetUowRepository<CronogramaBalanceamentoRepository>().GetListCronogramaBalanceamento(codEntidade, codPortfolio, numCenario).ToList();
            }
        }
    }
}
