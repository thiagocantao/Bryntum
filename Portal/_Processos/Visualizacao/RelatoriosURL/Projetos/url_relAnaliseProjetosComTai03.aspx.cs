using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using System.Data;
using DevExpress.Web;
using System.Drawing;

public partial class _Processos_Visualizacao_RelatoriosURL_Projetos_url_relAnaliseProjetosComTai03 : System.Web.UI.Page
{
    dados cDados;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoUsuario;
    private int codigoCarteira;
    public string larguraTela = "", alturaTela = "";
    public string larguraGrafico = "", alturaGrafico = "";
    private string dbName, dbOwner;

    public string alturaTabela = "";
    public string larguraTabela = "";
    private bool exportaOLAPTodosFormatos = false;
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
        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        codigoCarteira = int.TryParse(cDados.getInfoSistema("CodigoCarteira").ToString(), out codigoCarteira) == true ? codigoCarteira : -1;

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuario, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_ANLRSCFIEB");
        }


        if (!IsPostBack && !IsCallback)
        {
            DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos", "labelTipoProjeto", "labelQuestoes", "lblGeneroLabelQuestao");
            if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
            {

                exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
                string labelQuestoes = dsTemp.Tables[0].Rows[0]["labelQuestoes"].ToString();
                string generoQuestao = dsTemp.Tables[0].Rows[0]["lblGeneroLabelQuestao"].ToString();
                string labelTipoProjeto = dsTemp.Tables[0].Rows[0]["labelTipoProjeto"].ToString();
                labelQuestoes = string.Format("{0} Ativ{1}s", labelQuestoes, generoQuestao == "M" ? "o" : "a");

                fieldProblemasAtivos.Caption = labelQuestoes;
                fieldTipoProjeto.Caption = string.IsNullOrEmpty(labelTipoProjeto) ? "Tipo de Projeto" : labelTipoProjeto;

            }
        }

        buscaDadosGrid();
        defineLarguraTela();

        if (!IsPostBack && !IsCallback)
        {
            if (!hfGeral.Contains("tipoArquivo"))
            {
                hfGeral.Set("tipoArquivo", "XLS");
            }

            populaOpcoesExportacao();
        }

        //if (!IsPostBack)
        //{
        //    cDados.excluiNiveisAbaixo(1);
        //    cDados.insereNivel(1, this);
        //    //Master.geraRastroSite();
        //}
        CDIS_PivotGridLocalizer.Activate();  // ativa tradução dos textos da grid Pivot
        cDados.aplicaEstiloVisual(this);        
        this.Title = cDados.getNomeSistema();
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

    private void buscaDadosGrid()
    {
        string comandoSQL = string.Format(@" SELECT * FROM {0}.{1}.f_GetDadosOLAP_AnaliseProjetosComTAI03({2}, {3}, {4}) "
                , dbName, dbOwner, codigoUsuario, codigoEntidadeUsuarioResponsavel, codigoCarteira);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            pvgDadosProjeto.DataSource = ds.Tables[0];
            fieldUnidadeAtendimento.Caption = cDados.defineLabelUnidadeAtendimento().Replace(":", " ").TrimEnd(); 
            pvgDadosProjeto.DataBind();
        }        
    }

    private void defineLarguraTela()
    {
        int largura = 0;
        int altura = 0;
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        cDados.getLarguraAlturaTela(ResolucaoCliente, out largura, out altura);

        larguraTela = (largura - 58).ToString() + "px";
        alturaTela = (altura - 20).ToString() + "px";

        alturaTabela = (altura - 220) + "px";//a div vai ficar com essa altura
        larguraTabela = (largura - 200) + "px";
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuario;

            string nomeArquivo = "", app = "", erro = "";

            try
            {
                if (hfGeral.Get("tipoArquivo").ToString() == "HTML")
                {
                    string caminhoArquivo = HostingEnvironment.ApplicationPhysicalPath + "\\ArquivosTemporarios";
                    nomeArquivo = "OLAPAnaliseRiscosProjetos_" + dataHora + ".html";
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
                    nomeArquivo = "OLAPAnaliseRiscosProjetos_" + dataHora + ".pdf";
                    PdfExportOptions p = new PdfExportOptions();
                    p.DocumentOptions.Author = "CDIS Informática";
                    ASPxPivotGridExporter1.ExportToPdf(stream, p);
                    app = "application/pdf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "XLS")
                {
                    nomeArquivo = "OLAPAnaliseRiscosProjetos_" + dataHora + ".xls";
                    XlsExportOptionsEx x = new XlsExportOptionsEx();
                    x.TextExportMode = TextExportMode.Value;
                    x.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                    ASPxPivotGridExporter1.ExportToXls(stream, x);
                    //app = "application/vnd.ms-excel"; TIPO DE REFERENCIA MAIS UTILIZADA
                    app = "application/ms-excel";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "RTF")
                {
                    nomeArquivo = "OLAPAnaliseRiscosProjetos_" + dataHora + ".rtf";
                    ASPxPivotGridExporter1.ExportToRtf(stream);
                    app = "application/rtf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "CSV")
                {
                    nomeArquivo = "OLAPAnaliseRiscosProjetos_" + dataHora + ".csv";
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

    protected void pvgDadosProjeto_CustomCellDisplayText(object sender, DevExpress.Web.ASPxPivotGrid.PivotCellDisplayTextEventArgs e)
    {
        //bool indicaCampoCorDesempenho =
        //e.DataField == fieldCorFinanceiro ||
        //e.DataField == fieldCorFisico ||
        //e.DataField == fieldCorGeral;
        //if (indicaCampoCorDesempenho)
        //{
        //    if (e.DisplayText != null)
        //        e.DisplayText = "<img alt='' src='../../imagens/" + e.Value.ToString() + "Menor.gif' />";
        //}
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
    protected void pvgDadosProjeto_CustomFilterPopupItems(object sender, DevExpress.Web.ASPxPivotGrid.PivotCustomFilterPopupItemsEventArgs e)
    {
        if (e.ShowBlanksItem != null)
        {
            e.ShowBlanksItem.IsVisible = false;
            e.ShowBlanksItem.IsChecked = false;
        }
    }
    protected void pvgDadosIndicador_CustomSummary(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomSummaryEventArgs e)
    {
        if (e.FieldName.ToLower().StartsWith("cor"))
        {
            object cor = e.CreateDrillDownDataSource()[0][e.DataField];
            e.CustomValue = (cor ?? "branco").ToString().Trim();
        }

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

        if (campoEspecifico.Length > 0)
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

    protected void ASPxPivotGridExporter1_CustomExportCell(object sender, DevExpress.Web.ASPxPivotGrid.WebCustomExportCellEventArgs e)
    {
        if (e.DataField != null)
        {
            if (e.DataField.FieldName.ToLower().Contains("cor") && (e.Value != null))
            {
                e.Appearance.Font = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                e.Brick.Text = "l";
                e.Brick.TextValue = "l";
                if (e.Value.ToString().Equals("Vermelho"))
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else if (e.Value.ToString().Equals("Amarelo"))
                {
                    e.Appearance.ForeColor = Color.Yellow;
                }
                else if (e.Value.ToString().Equals("Verde"))
                {
                    e.Appearance.ForeColor = Color.Green;
                }
                else if (e.Value.ToString().Equals("Azul"))
                {
                    e.Appearance.ForeColor = Color.Blue;
                }
                else if (e.Value.ToString().Equals("Branco"))
                {
                    e.Appearance.ForeColor = Color.WhiteSmoke;
                }
                else if (e.Value.ToString().Equals("Laranja"))
                {
                    e.Appearance.ForeColor = Color.Orange;
                }
                else
                {
                    e.Brick.Text = " ";
                    e.Brick.Value = " ";
                }
            }
            //else if (e.DataField.FieldName.Contains("Variacao"))
            //{
            //    if (Convert.ToInt32(e.Value) != 0)
            //    {
            //        e.Appearance.ForeColor = Color.Red;
            //    }
            //}
            //else if (e.DataField.FieldName.Contains("Atras"))
            //{
            //    if (Convert.ToInt32(e.Value) > 0)
            //    {
            //        e.Appearance.ForeColor = Color.Red;
            //    }
            //}
        }
    }
}
