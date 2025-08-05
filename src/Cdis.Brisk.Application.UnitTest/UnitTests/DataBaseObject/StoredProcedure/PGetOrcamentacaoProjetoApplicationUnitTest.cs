using Cdis.Brisk.Application.Applications.StoredProcedure;
using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure.Param;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cdis.Brisk.Application.UnitTest.UnitTests.DataBaseObject.StoredProcedure
{
    [TestClass]
    public class PGetOrcamentacaoProjetoApplicationUnitTest
    {
        [TestMethod]
        public void PGetOrcamentacaoProjetoServiceUnitTestGetListPGetOrcamentacaoProjetoUnitTest()
        {
            using (var uow = new UnitOfWorkApplication("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                ParamPGetOrcamentacaoProjetoDomain param = new ParamPGetOrcamentacaoProjetoDomain
                {
                    CodigoEntidade = 111,
                    CodigoProjeto = 1000,
                    CodigoWorkflow = null,
                    CodigoInstanciaWF = null
                };
                var orcamentacaoDespesa = uow.GetUowApplication<PGetOrcamentacaoProjetoApplication>().GetDataTableDespesaOrcamentacaoProjeto(param);

                //
            }
        }
    }
}
