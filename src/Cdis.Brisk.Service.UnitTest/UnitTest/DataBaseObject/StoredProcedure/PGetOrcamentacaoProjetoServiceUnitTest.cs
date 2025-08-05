using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure.Param;
using Cdis.Brisk.Infra.Core.Extensions;
using Cdis.Brisk.Service.Services.DataBaseObject.StoredProcedure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Cdis.Brisk.Service.UnitTest.UnitTest.DataBaseObject.StoredProcedure
{
    [TestClass]
    public class PGetOrcamentacaoProjetoServiceUnitTest
    {
        [TestMethod]
        public void PGetOrcamentacaoProjetoServiceUnitTestGetListPGetOrcamentacaoProjetoUnitTest()
        {
            using (var uow = new UnitOfWorkService("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                ParamPGetOrcamentacaoProjetoDomain param = new ParamPGetOrcamentacaoProjetoDomain
                {
                    CodigoEntidade = 111,
                    CodigoProjeto = 1000,
                    CodigoWorkflow = null,
                    CodigoInstanciaWF = null
                };
                var test = uow.GetUowService<PGetOrcamentacaoProjetoService>().GetListDespesaPGetOrcamentacaoProjeto(param).ToList();
                var jsTest = test.ToJson();
                var dsTest = test.ToDataSet();
                //
            }
        }
    }
}
