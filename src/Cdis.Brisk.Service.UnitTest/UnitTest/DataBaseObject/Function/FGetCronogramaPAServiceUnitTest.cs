using Cdis.Brisk.Service.Services.DataBaseObject.Function;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Cdis.Brisk.Service.UnitTest.UnitTest.DataBaseObject.Function
{
    [TestClass]
    public class FGetCronogramaPAServiceUnitTest
    {
        [TestMethod]
        public void GetListCronogramaPlanoAcaoEntidadeUnitTest()
        {
            using (var uow = new UnitOfWorkService("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                int idObjeto = 1051;
                int idEntidade = 111;
                string iniciaisObjeto = "OB";
                var list = uow.GetUowService<FGetCronogramaPAService>().GetListCronogramaPlanoAcaoEntidade(iniciaisObjeto, idObjeto, idEntidade).ToList();
            }
        }

        [TestMethod]
        public void GetListCronogramaPlanoAcaoUnitTest()
        {
            using (var uow = new UnitOfWorkService("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                int idObjeto = 1051;
                int idPlanoAcao = 10;
                int idEntidade = 111;
                string iniciaisObjeto = "OB";
                var list = uow.GetUowService<FGetCronogramaPAService>().GetListCronogramaPlanoAcao(idPlanoAcao, iniciaisObjeto, idObjeto, idEntidade).ToList();
            }
        }

        [TestMethod]
        public void GetListCronogramaPlanoAcaoIniciativaEntidadeaUnitTest()
        {
            using (var uow = new UnitOfWorkService("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                int idObjeto = 1051;
                int idEntidade = 111;
                string iniciaisObjeto = "OB";
                var list = uow.GetUowService<FGetCronogramaPAService>().GetListCronogramaPlanoAcaoIniciativaEntidade(iniciaisObjeto, idObjeto, idEntidade).ToList();
            }
        }


        [TestMethod]
        public void GetListCronogramaPlanoAcaoIniciativaUnitTest()
        {
            using (var uow = new UnitOfWorkService("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                int idObjeto = 1051;
                int idPlanoAcao = 10;
                int idEntidade = 111;
                string iniciaisObjeto = "OB";
                var list = uow.GetUowService<FGetCronogramaPAService>().GetListCronogramaPlanoAcaoIniciativa(idPlanoAcao, iniciaisObjeto, idObjeto, idEntidade).ToList();
            }
        }
    }
}
