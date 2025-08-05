using Cdis.Brisk.Application.Applications.Relatorio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Cdis.Brisk.Application.UnitTest.UnitTests.Relatorio
{
    [TestClass]
    public class RelatorioPlanoTrabalhoApplicationUnitTest
    {

        [TestMethod]
        public void GetHtmlRelatorioPlanoTrabalhoSESCOOPTestMethod()
        {
            using (var uow = new UnitOfWorkApplication("Data Source=40.124.40.169;Initial Catalog=dbBRISK_desenv_ptbr; User ID=usr_desenv_ptbr; Password=orgnext2021"))
            {
                var codEntidade = 352;
                var codUsuario = 1;
                int? codUnidade = 363;

                var bytePdf = uow.GetUowApplication<RelatorioPlanoTrabalhoApplication>().GetByteArrayPdfStreamRelatorioPlanoTrabalho(codEntidade, codUsuario, codUnidade, null);

                string path = @"C://brisktest//";
                string filePdf = path + "planoTrabalho.pdf";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    if (!File.Exists(filePdf))
                    {
                        using (FileStream fs = File.Create(filePdf))
                        {
                            fs.Write(bytePdf, 0, bytePdf.Length);
                        }
                    }
                }
                else
                {
                    File.WriteAllBytes(filePdf, bytePdf);
                }
            }
        }

        [TestMethod]
        public void GetCapaTestMethod()
        {
            using (var uow = new UnitOfWorkApplication("Data Source=104.41.51.84;Initial Catalog=dbBRISK_SESCOOP; User ID=usr_sescoop_dv;Password=17091743"))
            {
                int? codigoCarteira = 352;
                int? codigoUnidade = 363;

                var bytePdf = uow.GetUowApplication<RelatorioPlanoTrabalhoApplication>().GetCapa("2020", "NOVEMBRO", "Desenvolvimento", "Todas", "Brisk ppm");

                string path = @"C://brisktest//";
                string filePdf = path + "capa.pdf";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    if (!File.Exists(filePdf))
                    {
                        using (FileStream fs = File.Create(filePdf))
                        {
                            fs.Write(bytePdf, 0, bytePdf.Length);
                        }
                    }
                }
                else
                {
                    File.WriteAllBytes(filePdf, bytePdf);
                }
            }
        }


        [TestMethod]
        public void GetPlanoNovoTestMethod()
        {
            using (var uow = new UnitOfWorkApplication("Data Source=104.41.51.84;Initial Catalog=dbBRISK_SESCOOP; User ID=usr_sescoop_dv;Password=17091743"))
            {
                var codEntidade = 352;
                var codUsuario = 1;
                int? codUnidade = 363;

                var bytePdf = uow.GetUowApplication<RelatorioPlanoTrabalhoApplication>().GetByteArrayPdfStreamRelatorioPlanoTrabalhoNew(codEntidade, codUsuario, codUnidade, null, "Unidade de negocio tal", "Carteira tal", "Entidade fulana de tal");

                string path = @"C://brisktest//";
                string filePdf = path + "planoTrabalhoNew1.pdf";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    if (!File.Exists(filePdf))
                    {
                        using (FileStream fs = File.Create(filePdf))
                        {
                            fs.Write(bytePdf, 0, bytePdf.Length);
                        }
                    }
                }
                else
                {
                    File.WriteAllBytes(filePdf, bytePdf);
                }
            }
        }
    }
}