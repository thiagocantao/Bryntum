using Cdis.Brisk.Service.Services.Relatorio.PlanoTrabalho;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cdis.Brisk.Service.UnitTest.UnitTest.Relatorio.PlanoTrabalho
{
    [TestClass]
    public class RelatorioPlanoTrabalhoServiceUnitTest
    {
        [TestMethod]
        public void GetListRelatorioPlanoTrabalhoUnidadeFakeTestMethod()
        {
            using (var uow = new UnitOfWorkService(""))
            {
                var list = uow.GetUowService<RelatorioPlanoTrabalhoService>().GetListRelatorioPlanoTrabalhoUnidadeFake(10);
            }
        }
    }
}
