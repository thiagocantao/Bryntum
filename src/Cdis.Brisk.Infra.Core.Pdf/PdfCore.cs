using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace Cdis.Brisk.Infra.Core.Pdf
{
    public static class PdfCore
    {
        /// <summary>
        /// Converter um documento HTML em PDF no tamanho de folha A2, horizontal
        /// </summary>            
        public static Stream ConvertHtmlToPdfFolhaA2HorizontalLandscape(string docHtml)
        {
            try
            {
                PdfGenerateConfig pdfGenerateConfig = new PdfGenerateConfig
                {
                    PageOrientation = PageOrientation.Landscape,
                    PageSize = PageSize.A2,
                    MarginLeft = 5,
                    MarginRight = 5,
                    MarginBottom = 5,
                    MarginTop = 5
                };
                return ConvertHtmlToPdf(docHtml, pdfGenerateConfig);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Montar o stream de todas as páginas html.
        /// </summary>        
        public static Stream GetStreamPageFromListHtml(List<string> listPageHtml, PdfGenerateConfig pdfGenerateConfig)
        {
            try
            {
                List<PdfDocument> listPdfDocument = new List<PdfDocument>();

                foreach (var pageHtml in listPageHtml)
                {
                    listPdfDocument.Add(PdfGenerator.GeneratePdf(pageHtml, pdfGenerateConfig));
                }

                return CombinePdfPage(listPdfDocument);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }


        /// <summary>
        /// Montar o stream de todas as páginas html.
        /// Margin = 5
        /// </summary>        
        public static Stream GetStreamPageFromListHtmlPageSizeA4PortraitMargin5(List<string> listPageHtml)
        {
            try
            {
                PdfGenerateConfig pdfGenerateConfig = new PdfGenerateConfig
                {
                    PageOrientation = PageOrientation.Portrait,
                    PageSize = PageSize.A4,
                    MarginLeft = 5,
                    MarginRight = 5,
                    MarginBottom = 5,
                    MarginTop = 5
                };

                return listPageHtml.Any() ? GetStreamPageFromListHtml(listPageHtml, pdfGenerateConfig) : null;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Montar o stream de todas as páginas html.
        /// Margin = 5
        /// </summary>        
        public static Stream GetStreamPageFromListHtmlPageSizeA4LandscapeMargin5(List<string> listPageHtml)
        {
            try
            {
                PdfGenerateConfig pdfGenerateConfig = new PdfGenerateConfig
                {
                    PageOrientation = PageOrientation.Landscape,
                    PageSize = PageSize.A4,
                    MarginLeft = 5,
                    MarginRight = 5,
                    MarginBottom = 5,
                    MarginTop = 5
                };

                return listPageHtml.Any() ? GetStreamPageFromListHtml(listPageHtml, pdfGenerateConfig) : null;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Montar o stream de todas as páginas html.
        /// Margin = 5
        /// </summary>        
        public static Stream GetStreamPageFromListHtmlPageSizeA2LandscapeMargin5(List<string> listPageHtml)
        {
            try
            {
                PdfGenerateConfig pdfGenerateConfig = new PdfGenerateConfig
                {
                    PageOrientation = PageOrientation.Landscape,
                    PageSize = PageSize.A2,
                    MarginLeft = 5,
                    MarginRight = 5,
                    MarginBottom = 5,
                    MarginTop = 5
                };

                return listPageHtml.Any() ? GetStreamPageFromListHtml(listPageHtml, pdfGenerateConfig) : null;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Converter um documento HTML em PDF
        /// </summary>            
        public static Stream ConvertHtmlToPdf(string docHtml, PdfGenerateConfig pdfGenerateConfig)
        {
            try
            {
                MemoryStream ms = new MemoryStream();

                var pdf = PdfGenerator.GeneratePdf(docHtml, pdfGenerateConfig);
                pdf.Save(ms);
                var arraBytes = ms.ToArray();
                Stream st = new MemoryStream(arraBytes);

                return st;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Agrupar os arquivos PdfDocument em um só arquivo pdf.
        /// </summary>        
        public static Stream CombinePdfPage(List<PdfDocument> listPdfDocument)
        {
            var combinedPdf = new PdfDocument();
            foreach (var pdfDocument in listPdfDocument)
            {
                PdfDocument pdfImport = ImportPdfDocument(pdfDocument);
                combinedPdf.Pages.Add(pdfImport.Pages[0]);
            }

            MemoryStream ms = new MemoryStream();
            combinedPdf.Save(ms);

            var arraBytes = ms.ToArray();
            Stream st = new MemoryStream(arraBytes);

            return st;
        }

        /// <summary>
        /// Importar o arquivo do pdf
        /// </summary>        
        private static PdfDocument ImportPdfDocument(PdfDocument pdfDocument)
        {
            using (var stream = new MemoryStream())
            {
                pdfDocument.Save(stream, false);
                stream.Position = 0;
                var result = PdfReader.Open(stream, PdfDocumentOpenMode.Import);
                return result;
            }
        }
    }
}
