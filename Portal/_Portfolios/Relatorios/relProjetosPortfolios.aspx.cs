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
using System.Drawing;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using System.Diagnostics;

public partial class _Portfolios_Relatorios_relProjetosPortfolios : System.Web.UI.Page
{
    #region --- [Variáveis da classe]

    dados cDados;

    

    int codigoUsuarioResponsavel;
    int codigoEntidade;
    public string alturaTabela = "";
    public string larguraTabela = "";
    private bool exportaOLAPTodosFormatos = false;

    #endregion
    
    protected void Page_Load(object sender, EventArgs e)
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

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        //carregaComboMapas();
         

        CDIS_PivotGridLocalizer.Activate();  // ativa tradução dos textos da grid Pivot
        if (!Page.IsPostBack)
        {
            carregaPortfolios();
            if (!hfGeral.Contains("tipoArquivo"))
            {
                hfGeral.Set("tipoArquivo", "XLS");
            }
            DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");
            if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
                exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
            populaOpcoesExportacao();
        }
        populaGrid();
        cDados.aplicaEstiloVisual(this);
        defineAlturaTela();

    }

    private void carregaPortfolios()
    {
        DataSet ds = cDados.getPortfolios(codigoEntidade, "");
        if (cDados.DataSetOk(ds))
        {
            ddlPortfolios.ValueField = "CodigoPortfolio";
            ddlPortfolios.TextField = "DescricaoPortfolio";
            ddlPortfolios.DataSource = ds.Tables[0];
            ddlPortfolios.DataBind();
            if (ddlPortfolios.Items != null && ddlPortfolios.Items.Count > 0)
            {
                ddlPortfolios.SelectedIndex = 0;
            }
        }
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        alturaTabela = (alturaPrincipal - 200) + "px";//a div vai ficar com essa altura
        larguraTabela = (larguraPrincipal - 5) + "px";

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
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel;

            string nomeArquivo = "", app = "", erro = "";

            try
            {
                if (hfGeral.Get("tipoArquivo").ToString() == "HTML")
                {
                    string caminhoArquivo = HostingEnvironment.ApplicationPhysicalPath + "\\ArquivosTemporarios";
                    nomeArquivo = "relProjetosPortfolios_" + dataHora + ".html";
                    nomeArquivo = caminhoArquivo + "\\" + nomeArquivo;
                    HtmlExportOptions h = new HtmlExportOptions();
                    h.ExportMode = HtmlExportMode.SingleFile;
                    //ASPxPivotGridExporter1.ExportToHtml(stream, h);
                    ASPxPivotGridExporter1.ExportToHtml(nomeArquivo, h);
                    StartProcess(nomeArquivo);
                    app = "text/html";

                }
                if (hfGeral.Get("tipoArquivo").ToString() == "PDF")
                {
                    nomeArquivo = "relProjetosPortfolios_" + dataHora + ".pdf";
                    PdfExportOptions p = new PdfExportOptions();
                    p.DocumentOptions.Author = "CDIS Informática";
                    ASPxPivotGridExporter1.ExportToPdf(stream, p);
                    app = "application/pdf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "XLS")
                {
                    nomeArquivo = "relProjetosPortfolios_" + dataHora + ".xls";
                    XlsExportOptionsEx x = new XlsExportOptionsEx();
                    x.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                    ASPxPivotGridExporter1.ExportToXls(stream, x);
                    //app = "application/vnd.ms-excel"; TIPO DE REFERENCIA MAIS UTILIZADA
                    app = "application/ms-excel";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "RTF")
                {
                    nomeArquivo = "relProjetosPortfolios_" + dataHora + ".rtf";
                    ASPxPivotGridExporter1.ExportToRtf(stream);
                    app = "application/rtf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "CSV")
                {
                    nomeArquivo = "relProjetosPortfolios_" + dataHora + ".csv";
                    ASPxPivotGridExporter1.ExportToText(stream, ";");
                    app = "application/CSV";
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

        ListEditItem liExcel = new ListEditItem("XLS", "XLS");
        liExcel.ImageUrl = "~/imagens/menuExportacao/iconoExcel.png";


        ddlExporta.Items.Add(liExcel);
        ddlExporta.ClientEnabled = false;
        if (exportaOLAPTodosFormatos)
        {
            ddlExporta.ClientEnabled = true;
            //ddlExporta.Items.Add(new ListEditItem("XLS", "XLS"));

            ListEditItem liPDF = new ListEditItem("PDF", "PDF");
            liPDF.ImageUrl = "~/imagens/menuExportacao/iconoPDF.png";
            ddlExporta.Items.Add(liPDF);


            ListEditItem liHTML = new ListEditItem("HTML", "HTML");
            liHTML.ImageUrl = "~/imagens/menuExportacao/iconoHtml.png";
            ddlExporta.Items.Add(liHTML);

            ListEditItem liRTF = new ListEditItem("RTF", "RTF");
            liRTF.ImageUrl = "~/imagens/menuExportacao/iconoPortfolio.png";
            ddlExporta.Items.Add(liRTF);

            ListEditItem liCSV = new ListEditItem("CSV", "CSV");
            liCSV.ImageUrl = "~/imagens/menuExportacao/iconoCSV.png";
            ddlExporta.Items.Add(liCSV);

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

    private void populaGrid()
    {
        if (ddlPortfolios.SelectedItem == null)
            return;
        DataSet ds = cDados.getRelatorioProjetosPortfolio(int.Parse(ddlPortfolios.SelectedItem.Value.ToString()), codigoEntidade, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()), codigoUsuarioResponsavel);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            pvgDadosProjeto.DataSource = ds.Tables[0];
            pvgDadosProjeto.DataBind();
        }

    }

    protected void pvgDadosProjeto_CustomSummary(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomSummaryEventArgs e)
    {
        if (e.FieldName.Equals("IndiceDesempenhoPrazo"))
        {
            //(%) idp = valor agregado / valor planejado
            double ValorAgregado = 0, ValorPlanejado = 0;
            double dblAux;

            DevExpress.XtraPivotGrid.PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
            foreach (DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow in ds)
            {
                if (summaryRow["ValorAgregado"] != null && double.TryParse(summaryRow["ValorAgregado"].ToString(), out dblAux))
                    ValorAgregado += dblAux;

                if (summaryRow["ValorPlanejado"] != null && double.TryParse(summaryRow["ValorPlanejado"].ToString(), out dblAux))
                    ValorPlanejado += dblAux;
            }
            if (ValorAgregado == 0)
                e.CustomValue = 0;
            else
                e.CustomValue = ValorAgregado / ValorPlanejado;

        }

        if (e.FieldName.Equals("IndiceDesempenhoCusto"))
        {
            //(%) idp = valor agregado / custo real
            double ValorAgregado = 0, CustoReal = 0;
            double dblAux;

            DevExpress.XtraPivotGrid.PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
            foreach (DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow in ds)
            {
                if (summaryRow["ValorAgregado"] != null && double.TryParse(summaryRow["ValorAgregado"].ToString(), out dblAux))
                    ValorAgregado += dblAux;

                if (summaryRow["CustoReal"] != null && double.TryParse(summaryRow["CustoReal"].ToString(), out dblAux))
                    CustoReal += dblAux;
            }
            if (ValorAgregado == 0)
                e.CustomValue = 0;
            else
                e.CustomValue = ValorAgregado / CustoReal;
        }
    }
    protected void pvgDadosProjeto_CustomCellStyle(object sender, DevExpress.Web.ASPxPivotGrid.PivotCustomCellStyleEventArgs e)
    {
        if (e.Value != null && e.DataField.FieldName == "VariacaoTrabalho")
        {
            if (Convert.ToDecimal(e.Value.ToString()) != 0)
            {
                e.CellStyle.ForeColor = Color.Red;
            }
        }
        if (e.Value != null && e.DataField.FieldName == "VariacaoCusto")
        {
            if (e.Value != null && Convert.ToDecimal(e.Value.ToString()) != 0)
            {
                e.CellStyle.ForeColor = Color.Red;
            }
        }
        if (e.Value != null && e.DataField.FieldName == "VariacaoReceita")
        {
            if (e.Value != null && Convert.ToDecimal(e.Value.ToString()) != 0)
            {
                e.CellStyle.ForeColor = Color.Red;
            }
        }
    }
    protected void ASPxPivotGridExporter1_CustomExportCell(object sender, DevExpress.Web.ASPxPivotGrid.WebCustomExportCellEventArgs e)
    {
        if (e.DataField != null)
        {
            if (e.DataField.FieldName.Contains("Variacao"))
            {
                if (Convert.ToInt32(e.Value) != 0)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
            if (e.DataField.FieldName.Contains("Atras"))
            {
                if (Convert.ToInt32(e.Value) > 0)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }
    }
}
