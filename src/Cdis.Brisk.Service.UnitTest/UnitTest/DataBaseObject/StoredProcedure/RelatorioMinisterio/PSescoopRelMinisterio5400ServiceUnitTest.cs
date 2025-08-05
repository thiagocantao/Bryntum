using Cdis.Brisk.Service.Services.DataBaseObject.StoredProcedure.RelatorioMinisterio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Cdis.Brisk.Service.UnitTest.UnitTest.DataBaseObject.StoredProcedure.RelatorioMinisterio
{
    [TestClass]
    public class PSescoopRelMinisterio5400ServiceUnitTest
    {
        [TestMethod]
        public void GetListPSescoopRelMinisterio5400UnitMethod()
        {
            using (var uow = new UnitOfWorkService("Data Source=10.1.0.30;Initial Catalog=DBPortalEstrategia; User ID=usrDBPortalEstrategia;Password=dbportalestrategia"))
            {
                string codColigada = "1";
                int ano1 = 2010;
                int ano2 = 2011;
                int ano3 = 2012;

                var test = uow.GetUowService<PSescoopRelMinisterio5400Service>().GetListPSescoopRelMinisterio5400(codColigada, ano1, ano2, ano3).ToList();

            }
        }
    }
}
