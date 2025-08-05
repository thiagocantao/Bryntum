using Cdis.Brisk.Domain.Domains.Usuario;
using Cdis.Brisk.Infra.Data.Repository.Repositories.Usuario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cdis.Brisk.Infra.Data.Repository.UnitTest.UnitTest.Usuario
{
    [TestClass]
    public class UsuarioEsqueceuSenhaAtualizacaoRepositoryUnitTest
    {
        [TestMethod]
        public void AtualizarMinhaSenhaUnitTest()
        {
            using (var uow = new UnitOfWorkRepository<Cdis.Brisk.Infra.Data.DomainContext>("Data Source=DESKTOP-0LUK4CB;Initial Catalog=brisk_pov;Integrated Security=True;MultipleActiveResultSets=True;"))
            {
                UsuarioEsqueceuSenhaAtualizacaoDomain usuarioEsqueceuSenha = new UsuarioEsqueceuSenhaAtualizacaoDomain
                {
                    CodigoUsuarioEsqueceuSenhaAtualizacao = 1,
                    DthAtualizacao = DateTime.Now
                };

                var test = uow.GetUowRepository<UsuarioEsqueceuSenhaAtualizacaoRepository>().Save(usuarioEsqueceuSenha);
            }
        }
    }
}
