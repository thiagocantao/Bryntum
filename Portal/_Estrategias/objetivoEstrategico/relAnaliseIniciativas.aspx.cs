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
using System.Drawing;
using DevExpress.Web;
using DevExpress.Web.ASPxPivotGrid;
using System.IO;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using System.Collections.Generic;

public partial class _Estrategias_objetivoEstrategico_relAnaliseIniciativas : System.Web.UI.Page
{
    #region --- [Variáveis da classe]

    dados cDados;

    private string dbName;
    private string dbOwner;

    int codigoUsuarioResponsavel;

    public string alturaTabela = "";
    public string larguraTabela = "";

    object metaAtual = new object();
    object resultadoAtual = new object();
    bool exportaOLAPTodosFormatos = false;

    private int codigoObjetivoEstrategico = 0;

    private int codigoEntidade = 0;

    private string tipoAssociacao = "";
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
        if (Request.QueryString["COE"] != null && Request.QueryString["COE"].ToString() + "" != "")
            codigoObjetivoEstrategico = int.Parse(Request.QueryString["COE"].ToString());
        
        if (Request.QueryString["TA"] != null && Request.QueryString["TA"].ToString() + "" != "")
            tipoAssociacao = Request.QueryString["TA"].ToString();

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        if (!IsPostBack)
        {
            DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");
            if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
                exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
            populaOpcoesExportacao();
        }


        cDados.aplicaEstiloVisual(this);

        if (!IsPostBack )
        {
            carregaCampos();
            if (!hfGeral.Contains("tipoArquivo"))
                hfGeral.Set("tipoArquivo", "XLS");
        }

        CDIS_PivotGridLocalizer.Activate();  // ativa tradução dos textos da grid Pivot

        populaGrid();
        defineAlturaTela();
    }

    private void carregaCampos()
    {
        DataTable dt = cDados.getObjetivoEstrategico(null, codigoObjetivoEstrategico, "").Tables[0];

        if (dt.Rows.Count > 0)
        {
            txtPerspectiva.Text = dt.Rows[0]["Perspectiva"].ToString();
            txtObjetivoEstrategico.Text = dt.Rows[0]["DescricaoObjetoEstrategia"].ToString();
            txtMapa.Text = dt.Rows[0]["TituloMapaEstrategico"].ToString();
            txtResponsavel.Text = dt.Rows[0]["NomeUsuario"].ToString();
            txtTema.Text = dt.Rows[0]["Tema"].ToString();
            hfGeral.Set("hfNomeObjetivo", txtObjetivoEstrategico.Text);
        }
        else
        {
            txtPerspectiva.Text = "";
            txtObjetivoEstrategico.Text = "";
            txtMapa.Text = "";
            txtResponsavel.Text = "";
            txtTema.Text = "";
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

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        alturaTabela = (alturaPrincipal - 210) + "px";//a div vai ficar com essa altura
        larguraTabela = (larguraPrincipal - 200) + "px";
    }

    protected void btnSelecionar_Click(object sender, EventArgs e)
    {
        populaGrid();
    }

    private void populaGrid()
    {
        string comandoSQL = string.Format(@"
             SELECT * FROM {0}.{1}.NOTHING
        ", dbName, dbOwner, codigoUsuarioResponsavel);
        
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            pvgDadosIndicador.DataSource = ds.Tables[0];
            pvgDadosIndicador.DataBind();
        }
    }

    #region --- [Pivot Grid]


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
                    nomeArquivo = "analiseIniciativas_" + dataHora + ".html";
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
                    nomeArquivo = "analiseIniciativas_" + dataHora + ".pdf";
                    PdfExportOptions p = new PdfExportOptions();
                    p.DocumentOptions.Author = "CDIS Informática";
                    ASPxPivotGridExporter1.ExportToPdf(stream, p);
                    app = "application/pdf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "XLS")
                {
                    nomeArquivo = "analiseIniciativas_" + dataHora + ".xls";
                    XlsExportOptionsEx x = new XlsExportOptionsEx();
                    x.TextExportMode = TextExportMode.Value;
                    x.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                    ASPxPivotGridExporter1.ExportToXls(stream, x);
                    //app = "application/vnd.ms-excel"; TIPO DE REFERENCIA MAIS UTILIZADA
                    app = "application/ms-excel";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "RTF")
                {
                    nomeArquivo = "analiseIniciativas_" + dataHora + ".rtf";
                    ASPxPivotGridExporter1.ExportToRtf(stream);
                    app = "application/rtf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "CSV")
                {
                    nomeArquivo = "analiseIniciativas_" + dataHora + ".csv";
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

    #endregion

    protected void ASPxPivotGridExporter1_CustomExportCell(object sender, DevExpress.Web.ASPxPivotGrid.WebCustomExportCellEventArgs e)
    {
        if (e.DataField != null)
        {
            if (e.DataField.FieldName.Contains("Desempenho") && (e.Value != null))
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
                else if (e.Value.ToString().Equals("Laranja"))
                {
                    e.Appearance.ForeColor = Color.Orange;
                }
                else if (e.Value.ToString().Equals("Branco"))
                {
                    e.Appearance.ForeColor = Color.WhiteSmoke;
                }
                else
                {
                    e.Brick.Text = " ";
                    e.Brick.TextValue = " ";
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
}
