//Revisado
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
using DevExpress.Web.ASPxPivotGrid;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using System.Diagnostics;

public partial class _Processos_Visualizacao_RelatoriosURL_Projetos_url_relAnaliseFinanceira : System.Web.UI.Page
{

    #region --- [Variáveis da classe]

    dados cDados;

    private string dbName;
    private string dbOwner;

    int codigoUsuarioResponsavel;
    public int codigoEntidade;
    public string alturaTabela = "";
    public string larguraTabela = "";
    public bool exportaOLAPTodosFormatos = false;

    List<string> valores = new List<string>(12);
    
#endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

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
        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuarioResponsavel, codigoEntidade, codigoEntidade, "null", "EN", 0, "null", "EN_PrjRelPrj");
        }

        cDados.aplicaEstiloVisual(this);

        CDIS_PivotGridLocalizer.Activate();  // ativa tradução dos textos da grid Pivot

        populaGrid();
        defineAlturaTela();
        if (!IsPostBack && !IsCallback)
        {
            if (!hfGeral.Contains("tipoArquivo"))
            {
                hfGeral.Set("tipoArquivo", "XLS");
            }
            DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");
            if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
                exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
            populaOpcoesExportacao();
        }

        //if (!IsPostBack)
        //{
        //    cDados.excluiNiveisAbaixo(1);
        //    cDados.insereNivel(1, this);
        //    Master.geraRastroSite();
        //}
        this.Title = cDados.getNomeSistema();

        valores.Add("Janeiro");//indice 1
        valores.Add("Fevereiro");//indice 2
        valores.Add("Março");
        valores.Add("Abril");
        valores.Add("Maio");
        valores.Add("Junho");
        valores.Add("Julho");
        valores.Add("Agosto");
        valores.Add("Setembro");
        valores.Add("Outubro");
        valores.Add("Novembro");
        valores.Add("Dezembro");
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        alturaTabela = (alturaPrincipal - 220) + "px";//a div vai ficar com essa altura
        larguraTabela = (larguraPrincipal - 200) + "px";

    }

    protected void btnSelecionar_Click(object sender, EventArgs e)
    {
        populaGrid();
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
                    nomeArquivo = "analiseFinanceira_" + dataHora + ".html";
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
                    nomeArquivo = "analiseFinanceira_" + dataHora + ".pdf";
                    PdfExportOptions p = new PdfExportOptions();
                    p.DocumentOptions.Author = "CDIS Informática";
                    ASPxPivotGridExporter1.ExportToPdf(stream, p);
                    app = "application/pdf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "XLS")
                {
                    nomeArquivo = "analiseFinanceira_" + dataHora + ".xls";
                    XlsExportOptionsEx x = new XlsExportOptionsEx();
                    x.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                    ASPxPivotGridExporter1.ExportToXls(stream, x);
                    app = "application/ms-excel";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "RTF")
                {
                    nomeArquivo = "analiseFinanceira_" + dataHora + ".rtf";
                    ASPxPivotGridExporter1.ExportToRtf(stream);
                    app = "application/rtf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "CSV")
                {
                    nomeArquivo = "analiseFinanceira_" + dataHora + ".csv";
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

    protected void pvgDadosIndicador_CustomSummary(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomSummaryEventArgs e)
    {
        string campoTotal = string.Empty;
        string campoEspecifico = string.Empty;
        double valorTotal = 0;
        double valorEspecifico = 0;
        double dblAux;

        if (e.FieldName.Contains("IndiceDesempenho"))
            campoTotal = "ValorAgregado";        

        if (e.FieldName.Equals("IndiceDesempenhoPrazo"))
            campoEspecifico = "ValorPlanejado";
        else if (e.FieldName.Equals("IndiceDesempenhoCusto"))
            campoEspecifico = "CustoReal";

        if (campoEspecifico.Length>0)
        {
            DevExpress.XtraPivotGrid.PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
            foreach (DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow in ds)
            {
                if ((summaryRow[campoTotal] != null) && double.TryParse(summaryRow[campoTotal].ToString(), out dblAux))
                    valorTotal += dblAux;

                if ((summaryRow[campoEspecifico] != null) && double.TryParse(summaryRow[campoEspecifico].ToString(), out dblAux))
                    valorEspecifico += dblAux;
            }
            if (valorTotal == 0)
                e.CustomValue = 0;
            else
                e.CustomValue = valorEspecifico / valorTotal;
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
        DataSet ds = cDados.getRelatorioAnaliseFinanceira(codigoEntidade, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()), codigoUsuarioResponsavel);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            pvgDadosProjeto.DataSource = ds.Tables[0];
            pvgDadosProjeto.DataBind();
        }
    }

    protected void pvgDadosProjeto_CustomCellStyle(object sender, PivotCustomCellStyleEventArgs e)
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
    protected void pvgDadosProjeto_CustomFieldSort(object sender, PivotGridCustomFieldSortEventArgs e)
    {
        if (e.Field.FieldName == "Mes")
        {
            object valor1 = valores.IndexOf(e.Value1.ToString());
            object valor2 = valores.IndexOf(e.Value2.ToString());
            e.Result = System.Collections.Comparer.Default.Compare(valor1, valor2);
            e.Handled = true;
        }
    }
}
