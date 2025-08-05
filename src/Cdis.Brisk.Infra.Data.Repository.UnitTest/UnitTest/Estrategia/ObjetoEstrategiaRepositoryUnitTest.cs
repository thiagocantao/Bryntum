using Cdis.Brisk.Infra.Data.Repository.Repositories.Estrategia;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cdis.Brisk.Infra.Data.Repository.UnitTest.UnitTest.Estrategia
{
    [TestClass]
    public class ObjetoEstrategiaRepositoryUnitTest
    {
        [TestMethod]
        public void ObjetoEstrategiaRepository_GetObjetivo()
        {
            using (var uow = new UnitOfWorkRepository<Cdis.Brisk.Infra.Data.DomainContext>("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                int codObjetivoEstrategico = 1049;
                var test = uow.GetUowRepository<ObjetoEstrategiaRepository>().GetSingleById(codObjetivoEstrategico);
            }
        }
    }
}
