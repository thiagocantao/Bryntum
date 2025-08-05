using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure.Param;
using Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.StoredProcedure;
using Cdis.Brisk.Service;
using Cdis.Brisk.Service.Services.Projeto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.UnitTest.DataBaseObject.UnitTest
{
    [TestClass]
    public class PGetOrcamentacaoProjetoRepositoryUnitTest
    {
        [TestMethod]
        public void ExecStoredProcedure()
        {
            using (var uow = new UnitOfWorkRepository<Cdis.Brisk.Infra.Data.DomainContext>("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                ParamPGetOrcamentacaoProjetoDomain param = new ParamPGetOrcamentacaoProjetoDomain
                {
                    CodigoEntidade = 111,
                    CodigoProjeto = 1000,
                    CodigoWorkflow = null,
                    CodigoInstanciaWF = null
                };
                var test = uow.GetUowRepository<PGetOrcamentacaoProjetoRepository>().ExecStoredProcedure(param, "D").ToList();
                var list = test.Select(a => a._Ano).Distinct().ToList();
                Assert.IsTrue(list.Count == 1);
            }
        }

        [TestMethod]
        public void AtualiarMemoriaCalculoTestMethod()
        {//Data Source = 104.41.51.84; Initial Catalog = dbBRISK_SESCOOP; User ID=usr_sescoop_dv; password = 17091743
            //view-source:localhost:42388/wfEngineInterno.aspx?CW=50&CI=189&CE=1&CS=1&CF=44&CP=350&Altura=365&Largura=1256&Popup=S
            using (var uow = new UnitOfWorkService("Data Source=104.41.51.84;Initial Catalog=dbBRISK_SESCOOP; User ID=usr_sescoop_dv;Password=17091743"))
            {
                var test = uow.GetUowService<MemoriaCalculoService>().AtualizarDescricaoMemoriaCalculo("Esta memoria de cálculo foi alterada com sucesso", null, 350, 11, 635);
                //var jsTest = test.ToJson();
                //var dsTest = test.ToDataSet();
            }
        }

    }
}
