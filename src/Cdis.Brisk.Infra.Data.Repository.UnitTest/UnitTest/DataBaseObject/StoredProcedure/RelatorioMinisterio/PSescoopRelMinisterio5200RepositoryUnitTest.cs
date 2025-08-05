using Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.StoredProcedure.RelatorioMinisterio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.UnitTest.UnitTest.DataBaseObject.StoredProcedure.RelatorioMinisterio
{
    [TestClass]
    public class PSescoopRelMinisterio5200RepositoryUnitTest
    {
        [TestMethod]
        public void ExecStoredProcedure()
        {
            using (var uow = new UnitOfWorkRepository<Cdis.Brisk.Infra.Data.DomainContext>("Data Source=10.6.0.40;Initial Catalog=DBPortalEstrategia; User ID=usrDBPortalEstrategia;Password=dbportalestrategia"))
            {
                string codColigada = "1";
                int ano1 = 2019;
                int ano2 = 2020;
                int ano3 = 2021;

                var test = uow.GetUowRepository<PSescoopRelMinisterio5200Repository>().ExecStoredProcedure(codColigada, ano1, ano2, ano3).ToList();
                
            }
        }
    }
}
