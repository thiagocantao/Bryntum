using DevExpress.Web;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;

public partial class _Projetos_DadosProjeto_relatorioStatusReportCNI : System.Web.UI.Page
{
    dados cDados;
    private int codigoEntidade;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        defineAlturaTela(cDados.getInfoSistema("ResolucaoCliente").ToString());

        //cDados.aplicaEstiloVisual(this);

        if (!IsPostBack)
        {
            int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        }

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        populaInstituicao();
        populaArea();
        //populaConsultor(); O cliente pediu pra modificar o filtro do consultor para projeto
        carregaGvDados();
    }

    private void populaConsultor()
    {

        string comandoSQL = string.Format(@"
        SELECT CodigoUsuario AS Codigo,
               NomeUsuario AS Descricao
          FROM Usuario AS u 
          INNER JOIN DetalhesTGPWfPessoasEspecificas AS d ON (d.IdentificadorRecurso = u.CodigoUsuario)
          WHERE d.CodigoPerfilWF = 148
            AND DataDesativacaoRegistro IS NULL
            AND StatusRegistro = 'A' ORDER BY 2 ");

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {

            ddlConsultor.TextField = "Descricao";
            ddlConsultor.ValueField = "Codigo";
            ddlConsultor.DataSource = ds;
            ddlConsultor.DataBind();
            ListEditItem li = ddlConsultor.Items.FindByValue(null);
            if (li == null)
            {
                ListEditItem liNulo = new ListEditItem("", null);
                ddlConsultor.Items.Insert(0, liNulo);
            }
        }
    }

    private void populaArea()
    {

        string comandoSQL = string.Format(@"
        SELECT en.[CodigoUnidadeNegocio], en.[NomeUnidadeNegocio]
          FROM {0}.{1}.[UnidadeNegocio] en
         WHERE en.[CodigoEntidade] = 108
           AND en.[CodigoEntidade] != en.[CodigoUnidadeNegocio]
           AND en.[DataExclusao] IS NULL
           ORDER BY en.[NomeUnidadeNegocio]", cDados.getDbName(), cDados.getDbOwner());

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {

            ddlArea.TextField = "NomeUnidadeNegocio";
            ddlArea.ValueField = "CodigoUnidadeNegocio";
            ddlArea.DataSource = ds;
            ddlArea.DataBind();
            ListEditItem li = ddlArea.Items.FindByValue(null);
            if (li == null)
            {
                ListEditItem liNulo = new ListEditItem("", null);
                ddlArea.Items.Insert(0, liNulo);
            }
        }
    }

    private void populaInstituicao()
    {
        string comandoSQL = string.Format(@"
        SELECT 'SESI' as sigla
        UNION
        SELECT 'SISTEMA INDÚSTRIA' as sigla
        UNION
        SELECT  'SENAI' as sigla
        UNION
        SELECT  'IEL' as sigla
        UNION
        SELECT  'CNI' as sigla ");

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {

            ddlInstituicao.TextField = "sigla";
            ddlInstituicao.ValueField = "sigla";
            ddlInstituicao.DataSource = ds;
            ddlInstituicao.DataBind();
            ListEditItem li = ddlInstituicao.Items.FindByValue(null);
            if (li == null)
            {
                ListEditItem liNulo = new ListEditItem("", null);
                ddlInstituicao.Items.Insert(0, liNulo);
            }
        }
    }

    private void carregaGvDados()
    {

        string siglaEntidade = (ddlInstituicao.Value != null) ? "'" + ddlInstituicao.Value.ToString() + "'" : "NULL";
        string codigoConsultor = (ddlConsultor.Text != "") ? "'" + ddlConsultor.Text + "'" : "NULL";
        string dataInicioPeriodoTarefa = (!string.IsNullOrEmpty(periodoDe.Text)) ? "CONVERT(DateTime, '" + periodoDe.Date.ToString("dd/MM/yyyy") + "', 103)" : "NULL";
        string dataFimPeriodoTarefa = (!string.IsNullOrEmpty(periodoAte.Text)) ? "CONVERT(DateTime, '" + periodoAte.Date.ToString("dd/MM/yyyy") + "', 103)" : "NULL";
        string statusProjeto = (ddlStatusProjeto.Value != null) ? "'" + ddlStatusProjeto.Value.ToString() + "'" : "'Exec_Plan'";

        string codigoUnidadeNegocio = (ddlArea.Value != null) ? ddlArea.Value.ToString() : "NULL";

        string comandoSQL = string.Format(@"
        DECLARE @RC int
        DECLARE @in_SiglaEntidade varchar(50)
        DECLARE @in_CodigoUnidadeNegocio int
        DECLARE @in_NomeProjeto varchar(256)
        DECLARE @in_DataInicioPeriodoTarefa datetime
        DECLARE @in_DataFimPeriodoTarefa datetime
        DECLARE @in_StatusProjeto varchar(50)

        SET @in_SiglaEntidade = {2}
        SET @in_NomeProjeto = {3}
        SET @in_DataInicioPeriodoTarefa  = {4}
        SET @in_DataFimPeriodoTarefa = {5}
        SET @in_StatusProjeto  = {6}
        SET @in_CodigoUnidadeNegocio = {7}

EXECUTE @RC = [dbo].[p_cni_ImprimeRelatorioStatusDIRCOM] 
           @in_SiglaEntidade
          ,@in_CodigoUnidadeNegocio
          ,@in_NomeProjeto
          ,@in_DataInicioPeriodoTarefa
          ,@in_DataFimPeriodoTarefa
          ,@in_StatusProjeto
", cDados.getDbName(), cDados.getDbOwner(),
        siglaEntidade,
        codigoConsultor,
        dataInicioPeriodoTarefa,
       dataFimPeriodoTarefa,
       statusProjeto,
       codigoUnidadeNegocio);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
            //O cliente pediu pra modificar o filtro do consultor para projeto
            ddlConsultor.DataSource = ds.Tables[1];
            ddlConsultor.TextField = "NomeProjeto";
            ddlConsultor.DataBind();


            ListEditItem liNulo = new ListEditItem("", null);
            ddlConsultor.Items.Insert(0, liNulo);

        }

        string cabecalhoPretoDoRelatorio = "STATUS REPORT";

        if (ddlInstituicao.Value != null)
        {
            cabecalhoPretoDoRelatorio += " - " + ddlInstituicao.Text;
        }
        if (!periodoDe.Date.ToString().Contains("01/01/0001"))
        {
            cabecalhoPretoDoRelatorio += " -  " + periodoDe.Date.ToString("dd.MM");
        }
        if (!periodoAte.Date.ToString().Contains("01/01/0001"))
        {
            cabecalhoPretoDoRelatorio += " a " + periodoAte.Date.ToString("dd.MM.yyyy");
        }
        ((ASPxTextBox)gvDados.FindTitleTemplateControl("txtTituloGrid")).Text = cabecalhoPretoDoRelatorio;
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        int largura = 0;
        int altura = 0;
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        gvDados.Settings.VerticalScrollableHeight = altura - 370;
    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "RelSRDircom", "Status Report DIRCOM", this);
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        gvExporter.PreserveGroupRowStates = true;

        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_");

            string nomeArquivo = "";

            nomeArquivo = "Exportacao_" + dataHora;

            if (parameter == "XLS" || parameter == "XLSX")
            {
                gvExporter.WriteXlsToResponse(nomeArquivo, new DevExpress.XtraPrinting.XlsExportOptionsEx()
                {
                    ExportType = DevExpress.Export.ExportType.WYSIWYG
                });
            }
        }
    }

    public void setaDefinicoesBotoesInserirExportar(ASPxMenu menu, bool podeIncluir, string funcaoJSbtnIncluir, bool mostraBtnIncluir, bool mostraBtnExportar, bool mostraBtnLayout, string iniciaisLayout, string tituloPagina, Page pagina)
    {
        #region EXPORTAÇÃO

        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                pagina.Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            pagina.Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            pagina.Response.End();
        }

        DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");

        bool exportaOLAPTodosFormatos = false;

        if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
        {
            exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
        }
        exportaOLAPTodosFormatos = true;
        DevExpress.Web.MenuItem btnExportar = menu.Items.FindByName("btnExportar");

        btnExportar.ClientVisible = mostraBtnExportar;

        if (!exportaOLAPTodosFormatos)
        {
            btnExportar.Items.Clear();
            btnExportar.Image.Url = "~/imagens/botoes/btnExcel.png";
            btnExportar.ToolTip = "Exportar para XLS";
        }

        #endregion

        #region INCLUIR

        DevExpress.Web.MenuItem btnIncluir = menu.Items.FindByName("btnIncluir");
        btnIncluir.ClientEnabled = podeIncluir;

        btnIncluir.ClientVisible = mostraBtnIncluir;

        if (podeIncluir == false)
            btnIncluir.Image.Url = "~/imagens/botoes/incluirRegDes.png";

        #endregion

        #region JS

        string clickBotaoExportar = "";

        if (exportaOLAPTodosFormatos)
            clickBotaoExportar = @"
            else if(e.item.name == 'btnPDF')
	        {
                 lpLoading.Show();
                 cbExportacao.PerformCallback();
                 e.processOnServer = false;		                                        
	        }";

        menu.ClientSideEvents.ItemClick =
        @"function(s, e){ 

            e.processOnServer = false;

            if(e.item.name == 'btnIncluir')
            {
                " + funcaoJSbtnIncluir + @"
            }" + clickBotaoExportar + @"		                     
	        else if(e.item.name != 'btnLayout')
	        {
                e.processOnServer = true;		                                        
	        }	
        }";

        #endregion

        #region LAYOUT

        DevExpress.Web.MenuItem btnLayout = menu.Items.FindByName("btnLayout");

        btnLayout.ClientVisible = mostraBtnLayout;

        if (mostraBtnLayout && !pagina.IsPostBack)
        {
            DataSet ds = cDados.getDataSet("SELECT 1 FROM Lista WHERE CodigoEntidade = " + cDados.getInfoSistema("CodigoEntidade") + " AND IniciaisListaControladaSistema = '" + iniciaisLayout + "'");

            if (ds.Tables[0].Rows.Count == 0)
            {
                int regAf = 0;

                cDados.execSQL(cDados.constroiInsertLayoutColunas((menu.Parent as GridViewHeaderTemplateContainer).Grid, iniciaisLayout, tituloPagina), ref regAf);
            }

            cDados.InitData((menu.Parent as GridViewHeaderTemplateContainer).Grid, iniciaisLayout);
        }

        #endregion
    }

    protected void cbExportacao_Callback(object source, CallbackEventArgs e)
    {

        relStatusReportDIRCOM relatorio;
        relatorio = new relStatusReportDIRCOM();
        string montaNomeArquivo = "";

        byte[] vetorBytes = null;

        DataSet ds = cDados.getLogoEntidade(codigoEntidade, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            if (ds.Tables[0].Rows[0]["LogoUnidadeNegocio"] != null && ds.Tables[0].Rows[0]["LogoUnidadeNegocio"].ToString() != "")
            {
                vetorBytes = (byte[])ds.Tables[0].Rows[0]["LogoUnidadeNegocio"];
            }
        }

        ASPxBinaryImage image1 = new ASPxBinaryImage();
        try
        {
            image1.ContentBytes = vetorBytes;

            if (image1.ContentBytes != null)
            {
                montaNomeArquivo = HostingEnvironment.ApplicationPhysicalPath + "/ArquivosTemporarios/" + "logo" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Trim(' ') + ".png";
                FileStream fs = new FileStream(montaNomeArquivo, FileMode.CreateNew);
                fs.Write(image1.ContentBytes, 0, image1.ContentBytes.Length);
                fs.Close();
                fs.Dispose();
            }
        }
        catch
        {

        }

        string cabecalhoPretoDoRelatorio = "STATUS REPORT";

        if (ddlInstituicao.Value != null)
        {
            cabecalhoPretoDoRelatorio += " - " + ddlInstituicao.Text;
        }
        if (!periodoDe.Date.ToString().Contains("01/01/0001"))
        {
            cabecalhoPretoDoRelatorio += " -  " + periodoDe.Date.ToString("dd.MM");
        }
        if (!periodoAte.Date.ToString().Contains("01/01/0001"))
        {
            cabecalhoPretoDoRelatorio += " a " + periodoAte.Date.ToString("dd.MM.yyyy");
        }

        string siglaEntidade = (ddlInstituicao.Value != null) ? "'" + ddlInstituicao.Value.ToString() + "'" : "NULL";
        string codigoConsultor = (ddlConsultor.Value != null) ? "'" + ddlConsultor.Value.ToString() + "'" : "NULL";
        string dataInicioPeriodoTarefa = (!string.IsNullOrEmpty(periodoDe.Text)) ? "CONVERT(DateTime, '" + periodoDe.Date.ToString() + "', 103)" : "NULL";
        string dataFimPeriodoTarefa = (!string.IsNullOrEmpty(periodoAte.Text)) ? "CONVERT(DateTime, '" + periodoAte.Date.ToString() + "', 103)" : "NULL";
        string statusProjeto = (ddlStatusProjeto.Value != null) ? "'" + ddlStatusProjeto.Value.ToString() + "'" : "'Todos'";
        string statusBriefing = /*(ddlSituacaoBriefing.Value != null) ? "'" + ddlSituacaoBriefing.Value.ToString() + "'" :*/ "'Todos'";


        string codigoUnidadeNegocio = (ddlArea.Value != null) ? "'" + ddlArea.Value.ToString() + "'" : "NULL";


        relatorio.Parameters["pTextoDoCabecalhoPretoDoRelatorio"].Value = cabecalhoPretoDoRelatorio;
        relatorio.Parameters["pSiglaEntidade"].Value = siglaEntidade;
        relatorio.Parameters["pCodigoConsultor"].Value = codigoConsultor;
        relatorio.Parameters["pDataInicioPeriodoTarefa"].Value = dataInicioPeriodoTarefa;
        relatorio.Parameters["pDataFimPeriodoTarefa"].Value = dataFimPeriodoTarefa;
        relatorio.Parameters["pStatusBriefing"].Value = statusBriefing;
        relatorio.Parameters["pStatusProjeto"].Value = statusProjeto;
        relatorio.Parameters["pCodigoUnidadeNegocio"].Value = codigoUnidadeNegocio;


        MemoryStream stream = new MemoryStream();
        relatorio.ExportToPdf(stream);
        Session["exportStream"] = stream;
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Name == "celulaDataTerminoTarefa")
        {
            DateTime valorData = DateTime.MinValue;
            string valorCelula = e.CellValue.ToString();
            DateTime.TryParse(valorCelula, out valorData);
            if (valorData != DateTime.MinValue)
            {
                e.Cell.Text = string.Format(@"{0:dd/MMM}", valorData);
            }
            else
            {
                e.Cell.Text = valorCelula;
            }
            e.Cell.Font.Bold = true;
        }
        if (e.DataColumn.Name == "colunaUnidadeDemandante")
        {
            e.Cell.Font.Bold = true;
        }

        if (e.DataColumn.Name == "colunaDataBriefing")
        {
            e.Cell.Font.Bold = true;
        }
        if (e.DataColumn.Name == "colunaNomeProjeto")
        {
            e.Cell.Font.Bold = true;
        }
        if (e.DataColumn.Name == "colunaDataInicioTarefa")
        {
            e.Cell.Font.Bold = true;
        }
        if (e.DataColumn.Name == "colunaDiasUteisCorridos")
        {
            e.Cell.Font.Bold = true;
        }
    }

    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (e.Column.Name == "colunaUnidadeDemandante")
            {
                e.BrickStyle.SetAlignment(DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center);
                e.Column.ExportWidth = 30;
                e.BrickStyle.Font = new System.Drawing.Font("Verdana", 8, System.Drawing.FontStyle.Bold);
            }
            else if (e.Column.Name == "colunaDataBriefing")
            {
                e.BrickStyle.SetAlignment(DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center);
                e.Column.ExportWidth = 50;
                e.BrickStyle.Font = new System.Drawing.Font("Verdana", 8, System.Drawing.FontStyle.Bold);
            }
            else if (e.Column.Name == "colunaNomeProjeto")
            {
                e.BrickStyle.SetAlignment(DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center);
                e.Column.ExportWidth = 100;
                e.BrickStyle.Font = new System.Drawing.Font("Verdana", 8, System.Drawing.FontStyle.Bold);
            }
            else if (e.Column.Name == "colunaNomeGerenteProjeto")
            {
                e.BrickStyle.SetAlignment(DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center);
                e.Column.ExportWidth = 100;
            }
            else if (e.Column.Name == "colunaDataInicioTarefa")
            {
                e.BrickStyle.SetAlignment(DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center);
                e.Column.ExportWidth = 50;
                e.BrickStyle.Font = new System.Drawing.Font("Verdana", 8, System.Drawing.FontStyle.Bold);

            }
            else if (e.Column.Name == "colunaNomeTarefa")
            {
                e.BrickStyle.SetAlignment(DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center);
                e.Column.ExportWidth = 100;
            }
            else if (e.Column.Name == "colunaRecursosAlocadosTarefa")
            {
                e.BrickStyle.SetAlignment(DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center);
                e.Column.ExportWidth = 140;
            }
            else if (e.Column.Name == "celulaDataTerminoTarefa")
            {
                e.BrickStyle.SetAlignment(DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center);
                e.Column.ExportWidth = 50;
                e.BrickStyle.Font = new System.Drawing.Font("Verdana", 8, System.Drawing.FontStyle.Bold);
                //e.TextValue = string.Format("",e.Value)
            }
            else if (e.Column.Name == "colunaDiasUteisCorridos")
            {
                e.BrickStyle.SetAlignment(DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center);
                e.Column.ExportWidth = 100;
                e.BrickStyle.Font = new System.Drawing.Font("Verdana", 8, System.Drawing.FontStyle.Bold);
            }
            else if (e.Column.Name == "colunaStatusBriefing")
            {
                e.BrickStyle.SetAlignment(DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center);
                e.Column.ExportWidth = 100;
                e.BrickStyle.Font = new System.Drawing.Font("Verdana", 8, System.Drawing.FontStyle.Bold);
            }
        }
    }
}