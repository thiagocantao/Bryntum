using Cdis.Brisk.Service.Services.Usuario;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cdis.Brisk.Service.UnitTest.UnitTest.Usuario
{
    [TestClass]
    public class UsuarioEsqueceuSenhaServiceUnitTest
    {
        [TestMethod]
        public void GetListEtapasPrevisaoOrcamentariaUnitTest()
        {
            using (var uow = new UnitOfWorkService("Data Source=10.61.35.3; Initial Catalog=brisk_dev_br; User ID=sacdis; Password=qualiton;MultipleActiveResultSets=True;"))
            {
                uow.GetUowService<UsuarioEsqueceuSenhaService>().SolicitarEsqueciSenha("atendimento@cdis.com.br", "brisk.dev.cdis.net.br/login.aspx?tkEs=");
            }
        }
    }
}
