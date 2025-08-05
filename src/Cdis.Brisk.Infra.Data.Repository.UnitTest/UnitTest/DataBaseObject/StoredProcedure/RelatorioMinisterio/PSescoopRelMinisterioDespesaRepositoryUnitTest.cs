using Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.StoredProcedure.RelatorioMinisterio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.UnitTest.UnitTest.DataBaseObject.StoredProcedure.RelatorioMinisterio
{
    [TestClass]
    public class PSescoopRelMinisterioDespesaRepositoryUnitTest
    {
        [TestMethod]
        public void ExecStoredProcedure()
        {
            using (var uow = new UnitOfWorkRepository<Cdis.Brisk.Infra.Data.DomainContext>("Data Source=10.1.0.30;Initial Catalog=DBPortalEstrategia; User ID=usrDBPortalEstrategia;Password=dbportalestrategia"))
            {
                string codColigada = "1";
                int ano1 = 2010;
                int ano2 = 2011;
                int ano3 = 2012;

                var test = uow.GetUowRepository<PSescoopRelMinisterioDespesaRepository>().ExecStoredProcedure(codColigada, ano1, ano2, ano3).ToList();

            }
        }
    }
}
