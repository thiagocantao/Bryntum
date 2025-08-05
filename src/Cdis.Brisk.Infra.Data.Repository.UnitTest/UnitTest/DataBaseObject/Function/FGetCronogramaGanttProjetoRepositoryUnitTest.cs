using Cdis.Brisk.Domain.Domains.DataBaseObject.Function.Param;
using Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.Function;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.UnitTest.UnitTest.DataBaseObject.Function
{
    [TestClass]
    public class FGetCronogramaGanttProjetoRepositoryUnitTest
    {
        [TestMethod]
        public void FGetCronogramaGanttProjetoRepository_ExecFunction()
        {
            string strDev = "Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678";
            string strPBH = "Data Source=sql2008r2;Initial Catalog=dbCdisPortalEstrategia_PBH_HM; User ID=PEstrategia;Password=12345678";
            strDev = "Data Source=40.124.40.169;Initial Catalog=dbBRISK_desenv2_ptbr; User ID=usr_desenv2_ptbr;Password=nextOrg2023!";
                
            using (var uow = new UnitOfWorkRepository<Cdis.Brisk.Infra.Data.DomainContext>(strDev))
            {
                ParamFGetCronogramaGanttProjetoDomain param = new ParamFGetCronogramaGanttProjetoDomain
                {
                    CodigoProjeto = 24,
                    VersaoLinhaBase = 1,
                    CodigoRecurso = -1,
                    SoAtrasadas = "N",
                    SoMarcos = "N",
                    PercentualConcluido = null,
                    DataFiltro = null
                };

                try
                {
                    var test = uow.GetUowRepository<FGetCronogramaGanttProjetoRepository>().ExecFunction(param).ToList();
                }
                catch (Exception exc)
                {
                    throw exc;
                }


            }
        }
    }
}
