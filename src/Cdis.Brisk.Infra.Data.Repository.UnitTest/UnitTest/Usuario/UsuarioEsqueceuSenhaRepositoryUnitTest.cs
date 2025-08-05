using Cdis.Brisk.Domain.Domains.Usuario;
using Cdis.Brisk.Infra.Data.Repository.Repositories.Usuario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cdis.Brisk.Infra.Data.Repository.UnitTest.UnitTest.Usuario
{
    [TestClass]
    public class UsuarioEsqueceuSenhaRepositoryUnitTest
    {
        [TestMethod]
        public void SolicitarEsqueciMinhaSenhaUnitTest()
        {
            using (var uow = new UnitOfWorkRepository<Cdis.Brisk.Infra.Data.DomainContext>("Data Source=DESKTOP-0LUK4CB;Initial Catalog=brisk_pov;Integrated Security=True;MultipleActiveResultSets=True;"))
            {
                UsuarioEsqueceuSenhaDomain usuarioEsqueceuSenha = new UsuarioEsqueceuSenhaDomain
                {
                    CodigoUsuario = 1,
                    DataExpiracao = DateTime.Now.AddDays(1),
                    DataSolicitacao = DateTime.Now,
                    Chave = Guid.NewGuid().ToString()
                };

                var test = uow.GetUowRepository<UsuarioEsqueceuSenhaRepository>().Save(usuarioEsqueceuSenha);
            }
        }
    }
}
