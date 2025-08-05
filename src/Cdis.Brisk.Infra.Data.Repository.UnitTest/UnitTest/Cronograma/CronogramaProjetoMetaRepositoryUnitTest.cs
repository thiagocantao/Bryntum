using Cdis.Brisk.Infra.Data.Repository.Repositories.Cronograma;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.UnitTest.UnitTest.Cronograma
{
    [TestClass]
    public class CronogramaProjetoMetaRepositoryUnitTest
    {
        [TestMethod]
        public void GetQueryListCronogramaProjetoMetaTestMethod()
        {
            using (var uow = new UnitOfWorkRepository<Cdis.Brisk.Infra.Data.DomainContext>("Data Source=40.124.40.169;Initial Catalog=dbBRISK_desenv2_ptbr; User ID=usr_desenv2_ptbr;Password=nextOrg2023!"))
            {
                int codEntidade = 111;
                int codUsuario = 1;
                int codCarteira = 24;
                var test = uow.GetUowRepository<CronogramaProjetoMetaRepository>().GetQueryListCronogramaProjetoMeta(codEntidade, codUsuario, codCarteira).ToList();
            }
        }
    }
}
