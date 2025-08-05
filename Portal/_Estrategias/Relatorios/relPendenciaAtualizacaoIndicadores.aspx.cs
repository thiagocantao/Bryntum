using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using System.Web.Hosting;
using DevExpress.Web;

public partial class _Estrategias_Relatorios_relPendenciaAtualizacaoIndicadores : System.Web.UI.Page
{
    #region --- [Variáveis da classe]

    dados cDados;

    private string resolucaoCliente;
    int codigoUsuarioResponsavel;
    int codigoEntidade = 0;

    public string alturaTabela = "";
    public string larguraTabela = "";

    object metaAtual = new object();
    object resultadoAtual = new object();

    public bool exportaOLAPTodosFormatos = false;

    #endregion


    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
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

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidade, codigoEntidade, "null", "EN", 0, "null", "EN_EstRelPndInd");
        }

        this.Title = cDados.getNomeSistema();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        carregaGrid();
        defineAlturaTela();
        if (!IsPostBack)
        {
            if (!hfGeral.Contains("tipoArquivo"))
            {
                hfGeral.Set("tipoArquivo", "XLS");
            }
            DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");
            if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
                exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
            populaOpcoesExportacao();


            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

    }

    private void defineAlturaTela()
    {
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        gvDados.Settings.VerticalScrollableHeight = altura - 300;
        gvDados.SettingsPager.PageSize = (int)(altura - 300) / 25;
    }

    private void carregaGrid()
    {
        gvDados.DataSource = cDados.getPendenciaAtualizacaoIndicadores(codigoEntidade);
        gvDados.DataBind();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel;

            string nomeArquivo = "", app = "", erro = "";

            try
            {

                if (hfGeral.Get("tipoArquivo").ToString() == "PDF")
                {
                    nomeArquivo = "logAtualizacaoInformacoes_" + dataHora + ".pdf";
                    PdfExportOptions p = new PdfExportOptions();
                    p.DocumentOptions.Author = "CDIS Informática";
                    gvExporter.WritePdfToResponse(p);
                    app = "application/pdf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "XLS")
                {
                    nomeArquivo = "logAtualizacaoInformacoes_" + dataHora + ".xls";
                    XlsExportOptionsEx x = new XlsExportOptionsEx();
                    gvExporter.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
                    app = "application/ms-excel";
                }
            }
            catch
            {
                erro = "S";
            }
            //app = "application/ms-excel";

            if (erro == "")
            {
                if (hfGeral.Get("tipoArquivo").ToString() != "HTML")
                {
                    nomeArquivo = "\"" + nomeArquivo + "\"";
                    Response.Clear();
                    Response.Buffer = false;
                    Response.AppendHeader("Content-Type", app);
                    Response.AppendHeader("Content-Transfer-Encoding", "binary");
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + nomeArquivo);
                    Response.BinaryWrite(stream.GetBuffer());
                    Response.End();
                }
            }
            else
            {
                string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Erro ao exportar os dados. Verifique se não foi ultrapassado o número máximo de 256 colunas!', 'erro', true, false, null);                                 
                                 </script>";
                ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
            }
        }
    }


    private void populaOpcoesExportacao()
    {
        ddlExporta.Items.Clear();
        ListEditItem liXLS = new ListEditItem("XLS", "XLS");
        liXLS.ImageUrl = "~/imagens/menuExportacao/iconoExcel.png";

        ddlExporta.Items.Add(liXLS);
        ddlExporta.ClientEnabled = false;

        if (exportaOLAPTodosFormatos)
        {
            ddlExporta.ClientEnabled = true;

            ListEditItem liPDF = new ListEditItem("PDF", "PDF");
            liPDF.ImageUrl = "~/imagens/menuExportacao/iconoPDF.png";
            ddlExporta.Items.Add(liPDF);
        }

        ddlExporta.SelectedIndex = 0;
    }


    public void StartProcess(string path)
    {
        Process process = new Process();
        try
        {
            process.StartInfo.FileName = path;
            process.Start();
            process.WaitForInputIdle();
        }
        catch { }
    }
    protected void pnImage_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string nomeArquivo = "";


        if (e.Parameter == "HTML")
            nomeArquivo = "~/imagens/menuExportacao/iconoHtml.png";

        if (e.Parameter == "PDF")
            nomeArquivo = "~/imagens/menuExportacao/iconoPDF.png";

        if (e.Parameter == "XLS")
            nomeArquivo = "~/imagens/menuExportacao/iconoExcel.png";

        if (e.Parameter == "RTF")
            nomeArquivo = "~/imagens/menuExportacao/iconoPortfolio.png";

        if (e.Parameter == "CSV")
            nomeArquivo = "~/imagens/menuExportacao/iconoCSV.png";

        imgExportacao.ImageUrl = nomeArquivo;
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.CellValue.ToString() == "1")
        {
            e.Cell.Text = "Janeiro";
        }
        if (e.CellValue.ToString() == "2")
        {
            e.Cell.Text = "Fevereiro";
        }
        if (e.CellValue.ToString() == "3")
        {
            e.Cell.Text = "Março";
        }
        if (e.CellValue.ToString() == "4")
        {
            e.Cell.Text = "Abril";
        }
        if (e.CellValue.ToString() == "5")
        {
            e.Cell.Text = "Maio";
        }
        if (e.CellValue.ToString() == "6")
        {
            e.Cell.Text = "Junho";
        }
        if (e.CellValue.ToString() == "7")
        {
            e.Cell.Text = "Julho";
        }
        if (e.CellValue.ToString() == "8")
        {
            e.Cell.Text = "Agosto";
        }
        if (e.CellValue.ToString() == "9")
        {
            e.Cell.Text = "Setembro";
        }
        if (e.CellValue.ToString() == "10")
        {
            e.Cell.Text = "Outubro";
        }
        if (e.CellValue.ToString() == "11")
        {
            e.Cell.Text = "Novembro";
        }
        if (e.CellValue.ToString() == "12")
        {
            e.Cell.Text = "Dezembro";
        }
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

    }
    protected void gvExporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Group)
        {
            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }
    }
}
