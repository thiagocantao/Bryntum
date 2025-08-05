using Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.Function;
using Cdis.Brisk.Infra.Data.Repository.Repositories.Relatorio.PlanoTabalho;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Cdis.Brisk.Infra.Data.Repository.UnitTest.UnitTest.Relatorio.PlanoTrabalho
{
    [TestClass]
    public class RelatorioPlanoTrabalhoRepositoryUnitTest
    {
        [TestMethod]
        public void GetListPlanoTrabalhoProjetoDomainTestMethod()
        {
            using (var uow = new UnitOfWorkRepository<Cdis.Brisk.Infra.Data.DomainContext>("Data Source=104.41.51.84;Initial Catalog=dbBRISK_SESCOOP; User ID=usr_sescoop_dv;Password=17091743"))
            {
                //var listFunc = uow.GetUowRepository<FGetCarteirasEntidadeRepository>().ExecFunction(352).ToList();

                var list = uow.GetUowRepository<RelatorioPlanoTrabalhoRepository>().GetListRelatorioPlanoTrabalhoDomain(352, 1, 363, null);

                var listCod = list.Select(l => l.CodigoProjeto).Distinct().OrderBy(o=>o).ToList();
                var listCodAt = list.SelectMany(l => l.ListPlanoTrabalhoAtividadeEntrega.Select(p=>p.CodigoProjeto).ToList()).Distinct().OrderBy(o=>o).ToList();
                var listDif = listCod.Except(listCodAt).ToList();

                //var listCod = list.Select(l => l.CodigoProjeto).ToList().Distinct();
            }
        }
    }
}