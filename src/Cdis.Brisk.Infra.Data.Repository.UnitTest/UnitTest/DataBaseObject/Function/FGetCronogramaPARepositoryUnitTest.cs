using Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.Function;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.UnitTest.UnitTest.DataBaseObject.Function
{
    [TestClass]
    public class FGetCronogramaPARepositoryUnitTest
    {
        [TestMethod]
        public void FCronoGetVersoesLbProjetoRepository_ExecFunction()
        {
            using (var uow = new UnitOfWorkRepository<Cdis.Brisk.Infra.Data.DomainContext>("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                string iniciaisObjeto = "OB";
                int codObjeto = 1051;
                int codEntidade = 111;

                var test = uow.GetUowRepository<FGetCronogramaPARepository>().ExecFunction(iniciaisObjeto, codObjeto, codEntidade).ToList();
            }
        }
    }
}
