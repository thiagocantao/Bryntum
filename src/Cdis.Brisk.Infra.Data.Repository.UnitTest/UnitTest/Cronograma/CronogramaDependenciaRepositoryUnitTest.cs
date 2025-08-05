using Cdis.Brisk.Infra.Data.Repository.Repositories.Cronograma;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.UnitTest.UnitTest.Cronograma
{
    [TestClass]
    public class CronogramaDependenciaRepositoryUnitTest
    {
        [TestMethod]
        public void CronogramaDependenciaRepository_ExecFunction()
        {
            using (var uow = new UnitOfWorkRepository<Cdis.Brisk.Infra.Data.DomainContext>("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                string dt = DateTime.Now.ToString("yyyy'-'MM'-'dd");
                var test = uow.GetUowRepository<CronogramaDependenciaRepository>().GetIQueryableCronogramaDependencia(1009).ToList();
            }
        }
    }
}
