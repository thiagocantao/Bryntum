using Cdis.Brisk.Infra.Data.Repository.Repositories.Cronograma;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.UnitTest.UnitTest.Cronograma
{
    [TestClass]
    public class CronogramaProjetoRepositoryUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var uow = new UnitOfWorkRepository<Cdis.Brisk.Infra.Data.DomainContext>("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                var test = uow.GetUowRepository<CronogramaProjetoRepository>()
                    .GetWhereAsNoTracking(u =>
                        u.DataCheckoutCronograma.HasValue
                     && u.CodigoUsuarioCheckoutCronograma.HasValue
                     && !u.DataExclusao.HasValue
                     && u.CodigoEntidade == 111
                     && u.CodigoProjeto == 1009)
                    .ToList();
            }
        }
    }
}
