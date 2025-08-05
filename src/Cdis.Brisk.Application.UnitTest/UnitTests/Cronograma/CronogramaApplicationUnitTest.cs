using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Cdis.Brisk.Application.UnitTest.UnitTests.Cronograma
{
    [TestClass]
    public class CronogramaApplicationUnitTest
    {
        [TestMethod]
        public void GetGanttDatasetDataTransferUnitTest()
        {
            using (var uow = new UnitOfWorkApplication("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                //var ganttDatasetDataTransfer = uow.GetUowApplication<CronogramaGanttApplication>().GetGanttDatasetDataTransfer(1009, "pt-BR");           
            }
        }

        [TestMethod]
        public void MountHtmlTaskGanttUnitTest()
        {
            using (var uow = new UnitOfWorkApplication("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {

                //var ganttDatasetDataTransfer = uow.GetUowApplication<CronogramaGanttApplication>().GetGanttDatasetDataTransfer(1426);
                //string html = uow.GetUowApplication<CronogramaApplication>().MountHtmlTaskGantt(ganttDatasetDataTransfer.tasks.rows);
            }
        }

        [TestMethod]
        public void GetHashToken()
        {
            string vl = Guid.NewGuid().ToString();
            string strHash = string.Empty;
            Enumerable.Range(48, 75)
              .OrderBy(o => new Random().Next())
              .ToList()
              .ForEach(i => strHash += Convert.ToChar(i));

            string token = strHash.GetHashCode().ToString("X2");
        }
    }
}
