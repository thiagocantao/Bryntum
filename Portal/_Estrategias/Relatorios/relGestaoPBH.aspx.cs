using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.IO;
using System.Diagnostics;

public partial class _Estrategias_Relatorios_relGestaoPBH : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    public string alturaTabela;
    public string larguraTabela = "";
    private bool exportaOLAPTodosFormatos = false;

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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_RelGesFiPBHPDTI");
        }
    }

    protected void pnImage_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        /*string nomeArquivo = "";

        if (e.Parameter == "HTML")
            nomeArquivo = "~/imagens/menuExportacao/iconoHtml.png";

        if (e.Parameter == "PDF")
            nomeArquivo = "~/imagens/menuExportacao/iconoPDF.png";

        if (e.Parameter == "XLS")
            nomeArquivo = "~/imagens/menuExportacao/iconoExcel.png";

        if (e.Parameter == "RTF")
            nomeArquivo = "~/imagens/menuExportacao/iconoPortfolio.png";

        if (e.Parameter == "CSV")
            nomeArquivo = "~/imagens/menuExportacao/iconoCSV.png";*/

        //imgExportacao.ImageUrl = nomeArquivo;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
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
            cDados.aplicaEstiloVisual(Page);
        }
        HeaderOnTela();
        carregaGrid();
        defineAlturaTela();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
        this.Title = cDados.getNomeSistema();
    }

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/relGestaoPBH.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "relGestaoPBH"));
    }

    private void carregaGrid()
    {
        string comandoSQL = string.Format(@"
        BEGIN
            DECLARE  @CodigoUsuario as int
            DECLARE  @CodigoEntidade AS int
            DECLARE  @CodigoCarteira as int

            SET  @CodigoUsuario = {2}
            SET  @CodigoEntidade = {3}
            select @CodigoCarteira = codigocarteirapadrao from {0}.{1}.usuario where codigousuario = @CodigoUsuario


            SELECT * FROM {0}.{1}.f_pbh_RelatorioGestaoPDTI(@CodigoUsuario, @CodigoEntidade, @CodigoCarteira)
            WHERE Descricao like '%{4}%'

        END", cDados.getDbName(), cDados.getDbOwner(), codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, txtDescricao.Text);
        DataSet ds = cDados.getDataSet(comandoSQL);
        tlDados.DataSource = ds.Tables[0];
        tlDados.DataBind();
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        alturaTabela = (alturaPrincipal - 210) + "px";//a div vai ficar com essa altura
        larguraTabela = (larguraPrincipal - 10) + "px";

        tlDados.Settings.ScrollableHeight = alturaPrincipal - 230;

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

            //ListEditItem liPDF = new ListEditItem("PDF", "PDF");
            //liPDF.ImageUrl = "~/imagens/menuExportacao/iconoPDF.png";
            //ddlExporta.Items.Add(liPDF);


            //ListEditItem liHTML = new ListEditItem("HTML", "HTML");
            //liHTML.ImageUrl = "~/imagens/menuExportacao/iconoHtml.png";
            //ddlExporta.Items.Add(liHTML);

            //ListEditItem liRTF = new ListEditItem("RTF", "RTF");
            //liRTF.ImageUrl = "~/imagens/menuExportacao/iconoPortfolio.png";
            //ddlExporta.Items.Add(liRTF);

            //ListEditItem liCSV = new ListEditItem("CSV", "CSV");
            //liCSV.ImageUrl = "~/imagens/menuExportacao/iconoCSV.png";
            //ddlExporta.Items.Add(liCSV);
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
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel;
            string nomeArquivo = "", app = "", erro = "";

            nomeArquivo = "gestaoPBHLOA_" + dataHora + ".xls";
            try
            {
                ASPxTreeListExporter1.WriteXls(stream);
            }
            catch
            {
                erro = "S";
            }

            app = "application/ms-excel";

            if (erro == "")
            {
                Response.Clear();
                Response.Buffer = false;
                Response.AppendHeader("Content-Type", app);
                Response.AppendHeader("Content-Transfer-Encoding", "binary");
                Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + nomeArquivo + "\"");
                Response.BinaryWrite(stream.GetBuffer());
                Response.End();
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



    protected void pnCallbackDados_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {

    }
    protected void tlDados_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
    {
        carregaGrid();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel;
            string nomeArquivo = "", app = "", erro = "";

            nomeArquivo = "gestaoPBH_PDTI" + dataHora + ".xls";
            try
            {
                ASPxTreeListExporter1.WriteXls(stream);
            }
            catch
            {
                erro = "S";
            }

            app = "application/ms-excel";

            if (erro == "")
            {
                Response.Clear();
                Response.Buffer = false;
                Response.AppendHeader("Content-Type", app);
                Response.AppendHeader("Content-Transfer-Encoding", "binary");
                Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + nomeArquivo + "\"");
                Response.BinaryWrite(stream.GetBuffer());
                Response.End();
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
}
