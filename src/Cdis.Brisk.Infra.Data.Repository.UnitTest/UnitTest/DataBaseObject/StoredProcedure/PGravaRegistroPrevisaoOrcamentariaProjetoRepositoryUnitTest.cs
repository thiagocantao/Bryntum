using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure.Param;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cdis.Brisk.Infra.Data.Repository.UnitTest.DataBaseObject.UnitTest
{
    [TestClass]
    public class PGravaRegistroPrevisaoOrcamentariaProjetoRepositoryUnitTest
    {
        [TestMethod]
        public void ExecStoredProcedureTestMethod()
        {
            using (var uow = new UnitOfWorkRepository<Cdis.Brisk.Infra.Data.DomainContext>("Data Source=sql2008r2;Initial Catalog=desenv_portalEstrategia; User ID=usr_desenvolvimento;Password=12345678"))
            {
                ParamPGravaRegistroPrevisaoOrcamentariaProjetoDomain param = new ParamPGravaRegistroPrevisaoOrcamentariaProjetoDomain
                {
                    CodigoConta = 1,

                };
                //var test = uow.GetUowRepository<PGravaRegistroPrevisaoOrcamentariaProjetoRepository>().ExecStoredProcedure(param);                
            }
        }
    }
}
