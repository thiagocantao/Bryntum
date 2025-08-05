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
using DevExpress.Web;
using System.IO;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using System.Collections.Generic;
using DevExpress.Web.ASPxPivotGrid;


public partial class _DashBoard_VisaoCorporativa_OLAP_Estagios1 : System.Web.UI.Page
{
    #region --- [Variáveis da classe]

    dados cDados;

    private string dbName;
    private string dbOwner;

    DateTime dteInicio, dteFim;

    int codigoUsuarioLogado;
    //int codigoEntidadeLogada = 0;

    public string alturaTabela = "";
    public string larguraTabela = "";

    public bool exportaOLAPTodosFormatos = false;
    private List<string> Meses = new List<string>();

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
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
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

        codigoUsuarioLogado     = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
//        codigoEntidadeLogada    = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        cDados.aplicaEstiloVisual(this);

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
            
        }

        AtribuiPeriodo();

        Meses.Clear();
        Meses.Add("Jan");
        Meses.Add("Fev");
        Meses.Add("Mar");
        Meses.Add("Abr");
        Meses.Add("Mai");
        Meses.Add("Jun");
        Meses.Add("Jul");
        Meses.Add("Ago");
        Meses.Add("Set");
        Meses.Add("Out");
        Meses.Add("Nov");
        Meses.Add("Dez");
        
        CDIS_PivotGridLocalizer.Activate();  // ativa tradução dos textos da grid Pivot
        populaGrid();
        defineAlturaTela();
    }
    private void AtribuiPeriodo()
    {
        if (cDados.getInfoSistema("InicioOlapEstagio") != null && cDados.getInfoSistema("InicioOlapEstagio").ToString() != "")
        {
            dteInicio = DateTime.Parse(cDados.getInfoSistema("InicioOlapEstagio").ToString());
            dteFim = DateTime.Parse(cDados.getInfoSistema("TerminoOlapEstagio").ToString());
        }
    }
    private void populaGrid()
    {
        int? codigoEntidade;
        codigoEntidade = null;
        if ((dteInicio == null) || (dteFim == null))
            return;

        string strDataInicial = string.Format("{0}/{1}/{2}", dteInicio.Day, dteInicio.Month, dteInicio.Year);
        string strDataFinal = string.Format("{0}/{1}/{2}", dteFim.Day, dteFim.Month, dteFim.Year);

        DataSet ds = cDados.SME_getOLAPEstagio1(codigoUsuarioLogado, codigoEntidade, strDataInicial, strDataFinal);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            pvgEstagios.DataSource = ds.Tables[0];
            pvgEstagios.DataBind();
            AjustaVisibilidadeTotais(pvgEstagios);  // ajusta visibilidade de totais
        }
    }
    
    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        alturaTabela = (alturaPrincipal - 191) + "px";//a div vai ficar com essa altura
        larguraTabela = (larguraPrincipal - 10) + "px";

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
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioLogado;

            string nomeArquivo = "", app = "", erro = "";

            try
            {
                if (hfGeral.Get("tipoArquivo").ToString() == "HTML")
                {
                    string caminhoArquivo = HostingEnvironment.ApplicationPhysicalPath + "\\ArquivosTemporarios";
                    nomeArquivo = "NumerosEstagiosPorRegional_" + dataHora + ".html";
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
                    nomeArquivo = "NumerosEstagiosPorRegional_" + dataHora + ".pdf";
                    PdfExportOptions p = new PdfExportOptions();
                    p.DocumentOptions.Author = "CDIS Informática";
                    ASPxPivotGridExporter1.ExportToPdf(stream, p);
                    app = "application/pdf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "XLS")
                {
                    nomeArquivo = "NumerosEstagiosPorRegional_" + dataHora + ".xls";
                    XlsExportOptionsEx x = new XlsExportOptionsEx();
                    x.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                    ASPxPivotGridExporter1.ExportToXls(stream, x);
                    //app = "application/vnd.ms-excel"; TIPO DE REFERENCIA MAIS UTILIZADA
                    app = "application/ms-excel";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "RTF")
                {
                    nomeArquivo = "NumerosEstagiosPorRegional_" + dataHora + ".rtf";
                    ASPxPivotGridExporter1.ExportToRtf(stream);
                    app = "application/rtf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "CSV")
                {
                    nomeArquivo = "NumerosEstagiosPorRegional_" + dataHora + ".csv";
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

    protected void pvgEstagios_CustomCallback(object sender, PivotGridCustomCallbackEventArgs e)
    {
        populaGrid();
    }
    private void AjustaVisibilidadeTotais(ASPxPivotGrid grid)
    {
        DevExpress.XtraPivotGrid.PivotTotalsVisibility visibilidadeLinha, visibilidadeColuna, visibilidade;

        // se o campo dado não estiver visível, nenhum total será permitido
        if (false == fldDado.Visible)
        {
            visibilidadeLinha = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            visibilidadeColuna = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            //grandTotalLinha = false;
            //grandTotalColuna = false;
        }
        else if (1 == fldDado.FilterValues.Count)
        {
            visibilidadeLinha = DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals;
            visibilidadeColuna = DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals;
        }
        else
            switch (fldDado.Area)
            {
                case DevExpress.XtraPivotGrid.PivotArea.FilterArea:
                    // se estiver ná área de filtro, desativa os totais por que certamente estará 
                    // filtro mais de um valor, caso contrário teria entrado no 'else if' acima
                    visibilidadeLinha = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
                    visibilidadeColuna = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
                    break;
                case DevExpress.XtraPivotGrid.PivotArea.ColumnArea:
                    visibilidadeLinha = DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals;
                    visibilidadeColuna = DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals;
                    break;
                case DevExpress.XtraPivotGrid.PivotArea.RowArea:
                    visibilidadeLinha = DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals;
                    visibilidadeColuna = DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals;
                    break;
                default:
                    visibilidadeLinha = DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals;
                    visibilidadeColuna = DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals;
                    break;

            } // switch (fldDado.Area)

        List<PivotGridField> rowFields = grid.GetFieldsByArea(DevExpress.XtraPivotGrid.PivotArea.RowArea);
        List<PivotGridField> colFields = grid.GetFieldsByArea(DevExpress.XtraPivotGrid.PivotArea.ColumnArea);

        foreach (PivotGridField campo in rowFields)
        {
            if (DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals == visibilidadeLinha)
            {   // se a visibilidade Global for custom, é preciso avaliar as posições das colunas

                if (campo.AreaIndex >= fldDado.AreaIndex)
                    visibilidade = DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals;
                else
                    visibilidade = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            }
            else
                visibilidade = visibilidadeLinha;

            campo.TotalsVisibility = visibilidade;
        } // foreach (PivotGridField campo in campos)

        foreach (PivotGridField campo in colFields)
        {
            if (DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals == visibilidadeColuna)
            {   // se a visibilidade Global for custom, é preciso avaliar as posições das colunas

                if (campo.AreaIndex >= fldDado.AreaIndex)
                    visibilidade = DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals;
                else
                    visibilidade = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            }
            else
                visibilidade = visibilidadeColuna;

            campo.TotalsVisibility = visibilidade;
        } // foreach (PivotGridField campo in campos)

        if (DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals == visibilidadeLinha)
            grid.OptionsView.ShowRowGrandTotals = true;
        else
            grid.OptionsView.ShowRowGrandTotals = false;

        if (DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals == visibilidadeColuna)
            grid.OptionsView.ShowColumnGrandTotals = true;
        else
            grid.OptionsView.ShowColumnGrandTotals = false;
    }

    protected void pvgEstagios_FieldAreaChanged(object sender, PivotFieldEventArgs e)
    {
        // se estiver mudando o campo Tópido de área, ajusta o alinhamento horizontal
        if (fldTopico == e.Field)
        {
            if (e.Field.Area == DevExpress.XtraPivotGrid.PivotArea.ColumnArea)
                e.Field.ValueStyle.HorizontalAlign = HorizontalAlign.Center;
            else
                e.Field.ValueStyle.HorizontalAlign = HorizontalAlign.Left;
        }
        AjustaVisibilidadeTotais((ASPxPivotGrid)sender);
    }
    protected void pvgEstagios_FieldAreaIndexChanged(object sender, PivotFieldEventArgs e)
    {
        AjustaVisibilidadeTotais((ASPxPivotGrid)sender);
    }
    protected void pvgEstagios_FieldFilterChanged(object sender, PivotFieldEventArgs e)
    {
        AjustaVisibilidadeTotais((ASPxPivotGrid)sender);
    }
    protected void pvgEstagios_FieldVisibleChanged(object sender, PivotFieldEventArgs e)
    {
        // não permite esconder o campo 'Dado'
        if ((fldDado == e.Field) || (fldTopico == e.Field))
            e.Field.Visible = true;

        AjustaVisibilidadeTotais((ASPxPivotGrid)sender);
    }
    protected void pvgEstagios_CustomFieldSort(object sender, PivotGridCustomFieldSortEventArgs e)
    {
        if (e.Field == fldMes)
        {
            if (e.Value1 != null && e.Value2 != null)
            {
                object valor1 = Meses.IndexOf(e.Value1.ToString());
                object valor2 = Meses.IndexOf(e.Value2.ToString());
                e.Result = System.Collections.Comparer.Default.Compare(valor1, valor2);
                e.Handled = true;
            }
        }
    }
}
