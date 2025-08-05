using Cdis.Brisk.Application.Applications.Relatorio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.IO;

namespace Cdis.Brisk.Application.UnitTest.UnitTests.Relatorio
{
    [TestClass]
    public class RelatorioMinisterioApplicationUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var uow = new UnitOfWorkApplication("Data Source=10.6.0.40, 1433;Initial Catalog=DBPortalEstrategia; User ID=usrDBPortalEstrategia;Password=dbportalestrategia"))
            {               
                var bytePdf = uow.GetUowApplication<RelatorioMinisterioApplication>()
                    .GetByteArrayPdfStreamRelatorioMinisterio(352, 2019,2020,2021);

                string path = @"C://brisktest//";
                string filePdf = path + "relatorio-ministerio.pdf";

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

        public string GetFormatacaoValor(decimal vl)
        {
            var tvl = Math.Truncate(100 * vl) / 100;

            return tvl.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
        }
    }
}
