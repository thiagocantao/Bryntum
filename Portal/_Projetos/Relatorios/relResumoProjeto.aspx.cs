using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DevExpress.Web;
using System.Data;
using DevExpress.XtraReports.Web;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;

public partial class _Projetos_Relatorios_relResumoProjeto : System.Web.UI.Page
{
    private const int INT_width = 100;
    private const int INT_height = 100;
    public string alturaTela;
    private int codigoProjeto;
    dados cDados;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        cDados = CdadosUtil.GetCdados(null);

        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            Response.End();
        }

        SqlDataSource1.ConnectionString = cDados.classeDados.getStringConexao();

        int alturaInt;
        if (int.TryParse(Request.QueryString["Altura"], out alturaInt))
            alturaTela = (alturaInt).ToString();
        if (!string.IsNullOrEmpty(alturaTela))
        {
            gvDados.Settings.VerticalScrollableHeight = alturaInt - 115;
            pcRelatorio.Height = new Unit(alturaInt - 25, UnitType.Pixel);
            ReportViewer1.Height = new Unit(alturaInt - 150, UnitType.Pixel);
            DivPrincipal.Style.Add("height", alturaTela + "px");
            DivPrincipal.Style.Add("overflow", "auto");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        bool codigoProjetoValido = int.TryParse(Request.QueryString["CP"], out codigoProjeto);

        if (!codigoProjetoValido)
            codigoProjeto = -1;

        cDados.aplicaEstiloVisual(this);
    }

    private void GerarRelatorio(FormatoArquivo formato)
    {
        string codigosAnexos = ObtemCodigosAnexos();
        int codigoContrato;
        DataSet dsTemp = cDados.getParametrosSistema("labelPrevistoParcelaContrato");

        string labelValorMedido = "Valor Medido";

        if (cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0]))
        {
            if (dsTemp.Tables[0].Rows[0]["labelPrevistoParcelaContrato"].ToString() != "")
                labelValorMedido = dsTemp.Tables[0].Rows[0]["labelPrevistoParcelaContrato"].ToString();
        }
        rel_ResumoProjeto relResumoProjeto = new rel_ResumoProjeto(codigoProjeto, codigosAnexos);
        relResumoProjeto.Parameters["parametroLabelValorMedido"].Value = labelValorMedido;
        relResumoProjeto.CreateDocument();

        if (relResumoProjeto.ds.DadosProjeto.Rows.Count > 0)
        {
            DsResumoProjeto.DadosProjetoRow row =
                (DsResumoProjeto.DadosProjetoRow)relResumoProjeto.ds.DadosProjeto.Rows[0];
            var percentualConcluido = new int?();
            var data = new DateTime?();
            relImprimeCronograma relCronograma = new relImprimeCronograma(codigoProjeto, -1, -1, 0, 0, 0, 0, row.NomeProjeto, "", "", "", "N", percentualConcluido, data);
            relCronograma.CreateDocument();
            relResumoProjeto.Pages.AddRange(relCronograma.Pages);

            try
            {
                codigoContrato = row.CodigoContrato;
            }
            catch (System.Data.StrongTypingException)
            {
                codigoContrato = -1;
            }

            if (codigoContrato != -1)
            {
                rel_Contrato relContrato = new rel_Contrato(codigoContrato);
                relContrato.CreateDocument();
                relResumoProjeto.Pages.AddRange(relContrato.Pages);
            }

            ExportReport(relResumoProjeto, "relatorio", formato.ToString(), false);
        }
    }

    public void ExportReport(XtraReport report, string fileName, string fileType, bool inline)
    {
        MemoryStream stream = new MemoryStream();

        Response.Clear();

        if (fileType == "xls")
        {
            XlsExportOptionsEx x = new XlsExportOptionsEx();
            x.ExportType = DevExpress.Export.ExportType.WYSIWYG;
            report.ExportToXls(stream, x);
        }
            
        if (fileType == "pdf")
            report.ExportToPdf(stream);
        if (fileType == "rtf")
            report.ExportToRtf(stream);
        if (fileType == "csv")
            report.ExportToCsv(stream);

        Response.ContentType = "application/" + fileType;
        Response.AddHeader("Accept-Header", stream.Length.ToString());
        Response.AddHeader("Content-Disposition", string.Format("{0}; filename={1}.{2}", 
            (inline ? "Inline" : "Attachment"), fileName, fileType));
        Response.AddHeader("Content-Length", stream.Length.ToString());
        //Response.ContentEncoding = System.Text.Encoding.Default;
        Response.BinaryWrite(stream.ToArray());
        Response.End();
        
    }


    protected static System.Drawing.Image CreateThumb(System.Drawing.Image image)
    {
        int width = INT_width;
        int height = INT_height;
        return CreateThumb(width, height, image);
    }

    protected static System.Drawing.Image CreateThumb(int width, int height, System.Drawing.Image image)
    {
        System.Drawing.Image.GetThumbnailImageAbort callBack = new System.Drawing.Image.GetThumbnailImageAbort(callBackDummy); //este dummy é requerido pelo método que redimensiona a imagem
        image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone); //"workaround"
        image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone); //
        System.Drawing.Image thumb = image.GetThumbnailImage(width, height, callBack, System.IntPtr.Zero); //este é o método que irá redimensionar sua imagem
        return thumb;
    }

    public static System.Drawing.Image CreateThumb(byte[] picture)
    {
        int width = INT_width;
        int height = INT_height;
        return CreateThumb(width, height, picture);
    }

    public static System.Drawing.Image CreateThumb(int width, int height, byte[] picture)
    {
        System.IO.MemoryStream stream = new System.IO.MemoryStream(picture); //criamos um stream a partir do byte para poder enviar para o leitor
        System.Drawing.Image image = System.Drawing.Image.FromStream(stream); //aqui temos a imagem no tamanho original, se você olhar os outros métodos do System.Drawing.Image você poderá  ver que você pode pegar a imagem direto de um arquivo ou um HBitmap que é um formato que a maioria dos Windows Forms usam para armazenar as imagens
        return CreateThumb(width, height, image);
    }

    private static bool callBackDummy() { return true; }

    protected byte[] ObtemMiniaturaImagemObra(byte[] anexo)
    {
        System.Drawing.Image image = CreateThumb(anexo);

        if (image == null)
        {
            Response.Write("null");
            return null;
        }

        MemoryStream memoryStream = new MemoryStream();
        image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

        memoryStream.Close();
        memoryStream.Dispose();
        image.Dispose();

        return memoryStream.ToArray();
    }

    protected void gvDados_CustomUnboundColumnData(object sender, DevExpress.Web.ASPxGridViewColumnDataEventArgs e)
    {
        if (e.IsGetData && e.Column.FieldName == "Miniatura")
        {
            byte[] foto = (byte[])e.GetListSourceFieldValue(e.ListSourceRowIndex, "Anexo");
            byte[] miniatura = ObtemMiniaturaImagemObra(foto);
            e.Value = miniatura;
        }
    }

    private string ObtemCodigosAnexos()
    {
        List<string> keys = gvDados.GetSelectedFieldValues(gvDados.KeyFieldName).ConvertAll<string>(Convert.ToString);
        string codigos = string.Join(", ", keys.ToArray());
        if (string.IsNullOrEmpty(codigos))
            return "-1";

        return codigos;
    }
    protected void btnExecutar_Click(object sender, EventArgs e)
    {
        FormatoArquivo formato;
        formato = sender == btnPdf ? FormatoArquivo.pdf : FormatoArquivo.rtf;
        GerarRelatorio(formato);
    }

    enum FormatoArquivo
    {
        pdf,
        rtf
    }
}