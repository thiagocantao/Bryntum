/*
 * 29/06/2010: Modificação por Alejandro.
 *              Adicionar novo panel para controlar a ações automáticas de cancelamento do Workflow.
 *              Function: private string geraInstrucaoPublicacaoAcaoWorkflow()
 * 
 * 08/04/2011 :: Alejandro : Inicialização do estado do objeto hiddenfield 'hfValoresTela.Get("XMLWF")'
 *                           no .Load da tela.
    
*/
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraPrinting;
using DevExpress.Web;

public partial class administracao_adm_edicaoWorkflows : System.Web.UI.Page
{
    #region ----[Variaveis globais da classe.]

    DataTable tabelaMenu = new DataTable();
    dados cDados;
    private XmlDocument __xmlDoc = new XmlDocument();
    //pega a data e hora atual do sistema
    private string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";
    private string __strXmlWorkflow = string.Empty;
    private string VersaoWorkflow = string.Empty;
    private string nomeFluxo = string.Empty;
    private string _key = System.Configuration.ConfigurationManager.AppSettings["key"].ToString();
    private string IdUsuarioLogado = string.Empty;
    private string resolucaoCliente = "";
    private string gruposEnLaLista = ""; //Variavel pra listar CodigoPerfilWf nas grid do Conector, Timer, Etapa.
    public string alturaDivFlash = "";
    public string larguraDivFlash = "";
    private const int Kn_IndiceTable_AcoesAutomaticas_Connector = 0;
    private const int Kn_IndiceTable_AcoesAutomaticas_Timer = 1;
    private const int Kn_IndiceTable_AcoesAutomaticas_Wf = 2;
    private int alturaPrincipal = 0;
    private int CodigoEntidade = 0;
    private int CodigoFluxo;
    private int _colorConetor = 1;
    private bool exportaOLAPTodosFormatos = false;
    public int alturaGrafico = 0;
    public int larguraGrafico = 0;
    private DataTable dtResult;
    string utilizaAssinaturaDigitalFormularios = "N";
    public bool utilizaAcionamentoAPIFluxos = false;

    protected class configuracaoWF
    {
        public string parametro;
        public string valor;
    }
    private List<configuracaoWF> configuracoesWf = new List<configuracaoWF>();

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

        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        IdUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado").ToString();

        if (null == Request.QueryString["CF"])
            CodigoFluxo = 0;
        else
        {
            string s = Request.QueryString["CF"].ToString();
            if (false == int.TryParse(s, out CodigoFluxo))
                CodigoFluxo = 0;
        }

    }



    protected void Page_Load(object sender, EventArgs e)
    {
        string sufixo = System.Threading.Thread.CurrentThread.CurrentCulture.Name.StartsWith("pt", StringComparison.InvariantCultureIgnoreCase) == true ? "pt-BR" : "en-US";
        uc_crud_caminhoCondicionalptBR.Visible = (sufixo == "pt-BR");
        uc_crud_caminhoCondicionalenUS.Visible = (sufixo == "en-US");
        mnTelaEdicaoWorkflow.Items.FindByName("resumo").Visible = (sufixo.ToLower().Contains("br") == true);
        mnTelaEdicaoWorkflow.Items.FindByName("mneCaminhoCondicional").Visible = (sufixo.ToLower().Contains("br") == true);
        mnTelaEdicaoWorkflow.Items.FindByName("mneSubprocessos").Visible = (sufixo.ToLower().Contains("br") == true);

        tr_MenuEsquerdoCaminhoCondicional.Style.Add("display", (sufixo.ToLower().Contains("br") == true) ? "block" : "none");
        tr_MenuEsquerdoSubProcesso.Style.Add("display", (sufixo.ToLower().Contains("br") == true) ? "block" : "none");

        string sNomeFluxo = "";
        string sVersaoFluxo = "";
        HeaderOnTela();

        if (!IsPostBack)
        {
            hfValoresTela.Set("XMLWF", "");
            hfValoresTela.Set("nomeFluxo", "");
            hfValoresTela.Set("versaoWorkflow", "");
            hfValoresTela.Set("descricaoVersao", "");
            hfValoresTela.Set("observacaoVersao", "");
            hfValoresTela.Set("LarguraDivFlash", "");
            hfValoresTela.Set("AlturaDivFlash", "");
            hfValoresTela.Set("AcaoWf", "");

            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);

            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

        // Verifica o valor do parâmetro [utilizaAssinaturaDigitalFormularios], que indica se esta entidade utiliza Certificação Digital nos formulários de seus fluxos.
        DataSet dsParametros = cDados.getParametrosSistema("utilizaAssinaturaDigitalFormularios", "utilizaAcionamentoAPIFluxos");
        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            utilizaAssinaturaDigitalFormularios = dsParametros.Tables[0].Rows[0]["utilizaAssinaturaDigitalFormularios"].ToString();
            utilizaAcionamentoAPIFluxos = dsParametros.Tables[0].Rows[0]["utilizaAcionamentoAPIFluxos"].ToString().ToUpper().Trim() + "" == "S";
        }

        hfValoresTela.Set("utilizaAssinaturaDigitalFormularios", utilizaAssinaturaDigitalFormularios);
        tcDivConector.TabPages[2].ClientVisible = utilizaAcionamentoAPIFluxos;
        carregaComboIcones();
        carregaComboFluxos();

        if (!IsPostBack && !IsCallback)
        {
            if (!hfTipoArquivo.Contains("tipoArquivo"))
            {
                hfTipoArquivo.Set("tipoArquivo", "XLS");
            }
            populaOpcoesExportacao();

            if (null == Request.QueryString["CWF"])
                hfCodigoWorkflow.Value = "0";
            else
            {
                int codigoWorkflow = 0;
                string s = Request.QueryString["CWF"].ToString();
                int.TryParse(s, out codigoWorkflow);

                if (0 != codigoWorkflow)
                    hfCodigoWorkflow.Value = codigoWorkflow.ToString();
                else
                    hfCodigoWorkflow.Value = "0";
            }

            AtribuiDatasetGridFormularios_etp();
            AtribuiDatasetGridGrupos_etp();
            AtribuiDatasetGridGrupos_cnt();
            AtribuiDatasetGridGrupos_tmr();
            AtribuiDatasetGridAcoes();
            AtribuiDatasetGridAcionamentosAPI();
            AtribuiDatasetGridParametrosAcionamentosAPI();
            BuscaDadosWorkflow();

            //funcion que otem as configurações preenchidas pelo usuario
            getConfiguracaoSistemaWorflow();
            SetearReferenciaColor();
            //-------- fim confgurações preenchidas pelo usuario
        }
        VersaoWorkflow = hfValoresTela.Get("versaoWorkflow").ToString();
        defineTitulosGrids();
        this.Title = cDados.getNomeSistema();

        // -- Montagem da tela do Relatório de Resumo do Fluxo -------------------
        defineTamanhoPopUpRelatorioResumo();

        if (hfValoresTela.Contains("nomeFluxo"))
            sNomeFluxo = hfValoresTela.Get("nomeFluxo").ToString();
        if (hfValoresTela.Contains("versaoWorkflow"))
            sVersaoFluxo = hfValoresTela.Get("versaoWorkflow").ToString();

        pcResumoWf.HeaderText = Resources.traducao.adm_edicaoWorkflows_resumo_do_modelo_de_fluxo;
        if (sNomeFluxo.Length > 0)
            pcResumoWf.HeaderText += ": " + sNomeFluxo;
        if (sVersaoFluxo.Length > 0)
            pcResumoWf.HeaderText += " - " + Resources.traducao.adm_edicaoWorkflows_vers_o + " " + sVersaoFluxo;

        try
        {
            carregaRelResumo();
        }
        catch (Exception)
        {
            //Redirecionamento para novo editor workflow
            irNovoFluxo(sVersaoFluxo);
        }
        irNovoFluxo(sVersaoFluxo);
        //cDados.aplicaEstiloVisual(this);

        gvFormularios_etp.Settings.ShowFilterRow = false;
        gvFormularios_etp.SettingsPager.AlwaysShowPager = false;

        gv_PessoasAcessos_etp.Settings.ShowFilterRow = false;
        gv_PessoasAcessos_etp.SettingsPager.AlwaysShowPager = false;

        gv_GruposNotificados_tmr.Settings.ShowFilterRow = false;
        gv_GruposNotificados_tmr.SettingsPager.AlwaysShowPager = false;

        gv_Acoes_tmr.Settings.ShowFilterRow = false;
        gv_Acoes_tmr.SettingsPager.AlwaysShowPager = false;

        gv_GruposNotificados_cnt.Settings.ShowFilterRow = false;
        gv_GruposNotificados_cnt.SettingsPager.AlwaysShowPager = false;

        gv_Acoes_cnt.Settings.ShowFilterRow = false;
        gv_Acoes_cnt.SettingsPager.AlwaysShowPager = false;

        gv_GruposNotificados_wf.Settings.ShowFilterRow = false;
        gv_GruposNotificados_wf.SettingsPager.AlwaysShowPager = false;

        gv_Acoes_wf.Settings.ShowFilterRow = false;
        gv_Acoes_wf.SettingsPager.AlwaysShowPager = false;

        gv_Acionamentos.Settings.ShowFilterRow = false;
        gv_Acionamentos.SettingsPager.AlwaysShowPager = false;

        gv_ParametrosAcionamentos.Settings.ShowFilterRow = false;
        gv_ParametrosAcionamentos.SettingsPager.AlwaysShowPager = false;

        divEtapa.ShowHeader = false;
        divSubprocesso.ShowHeader = false;
    }

    private void AtribuiDatasetGridParametrosAcionamentosAPI()
    {
        DataSet dsResult = new DataSet();

        DataTable tabelas = new DataTable();
        dsResult.Tables.Add(tabelas);

        DataColumn NewColumn = null;

        NewColumn = new DataColumn("IdKey", Type.GetType("System.String"));
        NewColumn.Caption = "IdKey";
        NewColumn.ReadOnly = true;
        dsResult.Tables[0].Columns.Add(NewColumn);

        NewColumn = new DataColumn("Id", Type.GetType("System.Int32"));
        NewColumn.Caption = "Id";
        NewColumn.ReadOnly = false;
        dsResult.Tables[0].Columns.Add(NewColumn);

        NewColumn = new DataColumn("parameter", Type.GetType("System.String"));
        NewColumn.Caption = "#";
        NewColumn.ReadOnly = false;
        dsResult.Tables[0].Columns.Add(NewColumn);

        NewColumn = new DataColumn("dataType", Type.GetType("System.String"));
        NewColumn.Caption = "Tipo de dados";
        NewColumn.ReadOnly = false;
        dsResult.Tables[0].Columns.Add(NewColumn);

        NewColumn = new DataColumn("httpPart", Type.GetType("System.String"));
        NewColumn.Caption = "Seção";
        NewColumn.ReadOnly = false;
        dsResult.Tables[0].Columns.Add(NewColumn);

        NewColumn = new DataColumn("sendNull", Type.GetType("System.Boolean"));
        NewColumn.Caption = "Nulo";
        NewColumn.ReadOnly = false;
        dsResult.Tables[0].Columns.Add(NewColumn);

        NewColumn = new DataColumn("value", Type.GetType("System.String"));
        NewColumn.Caption = "Valor";
        NewColumn.ReadOnly = false;
        dsResult.Tables[0].Columns.Add(NewColumn);

        Session["dsParametrosAcionamentosAPI"] = dsResult;

        gv_ParametrosAcionamentos.DataSource = dsResult.Tables[0];
        gv_ParametrosAcionamentos.DataBind();
    }

    private void AtribuiDatasetGridAcionamentosAPI()
    {
        DataSet dsResult = new DataSet();

        DataTable tabelas = new DataTable();
        dsResult.Tables.Add(tabelas);

        DataColumn NewColumn = null;
        NewColumn = new DataColumn("Id", Type.GetType("System.Int32"));
        NewColumn.Caption = "Id";
        NewColumn.ReadOnly = true;
        dsResult.Tables[0].Columns.Add(NewColumn);

        NewColumn = new DataColumn("activationSequence", Type.GetType("System.Int32"));
        NewColumn.Caption = "#";
        NewColumn.ReadOnly = false;
        dsResult.Tables[0].Columns.Add(NewColumn);

        NewColumn = new DataColumn("webServiceURL", Type.GetType("System.String"));
        NewColumn.Caption = "URL";
        NewColumn.ReadOnly = false;
        dsResult.Tables[0].Columns.Add(NewColumn);

        NewColumn = new DataColumn("enabled", Type.GetType("System.Boolean"));
        NewColumn.Caption = "Ativo?";
        NewColumn.ReadOnly = false;
        dsResult.Tables[0].Columns.Add(NewColumn);

        Session["dsAcionamentosAPI"] = dsResult;

        gv_Acionamentos.DataSource = dsResult.Tables[0];
        gv_Acionamentos.DataBind();
    }

    private void carregaComboIcones()
    {
        cmbIconeBotao_cnt.Items.Clear();
        DirectoryInfo Dir = new DirectoryInfo(Server.MapPath("~/imagens/iconesBotoesFluxo"));
        // Busca automaticamente todos os arquivos em todos os subdiretórios
        FileInfo[] Files = Dir.GetFiles("*.png", SearchOption.AllDirectories);

        foreach (FileInfo File in Files)
        {
            ListEditItem lei = new ListEditItem();
            lei.Value = File.Name;
            lei.ImageUrl = string.Format(@"{0}imagens/iconesBotoesFluxo/{1}", cDados.getPathSistema(), File.Name);
            lei.Text = File.Name.Replace(File.Extension, "");
            if (lei.Text == "Sem Imagem")
                cmbIconeBotao_cnt.Items.Insert(0, lei);
            else
                cmbIconeBotao_cnt.Items.Add(lei);
            //string text = string.Format(@"<img style='width:16px; height:16px' src='{0}imagens/iconesBotoesFluxo/{1}'/>", cDados.getPathSistema(), File.Name);
            //cmbIconeBotao_cnt.Items.Add(text, File.Name);
        }
        cmbIconeBotao_cnt.ItemImage.Width = 16;
        cmbIconeBotao_cnt.ItemImage.Height = 16;
    }

    private void carregaComboFluxos()
    {
        string where = "";
        where += string.Format(@" AND f.CodigoEntidade = {0} ", CodigoEntidade);
        where += string.Format(@" AND f.CodigoFluxo != {0} AND f.StatusFluxo = 'A' ", CodigoFluxo);
        where += string.Format(@" AND EXISTS( SELECT TOP 1 1 FROM {0}.{1}.Workflows wf WHERE wf.CodigoFluxo = f.CodigoFluxo 
                    AND wf.DataPublicacao IS NOT NULL AND wf.DataRevogacao IS NULL ) ", cDados.getDbName(), cDados.getDbOwner());
        DataTable dtFluxos = cDados.getFluxos(where).Tables[0];

        cmbFluxos_sub.DataSource = dtFluxos;
        cmbFluxos_sub.ValueField = "CodigoFluxo";
        cmbFluxos_sub.TextField = "NomeFluxo";
        cmbFluxos_sub.DataBind();
    }

    private void irNovoFluxo(String sVersaoFluxo, Boolean abrir = false)
    {
        if (abrir || Request.QueryString["CWF"] != null && Request.QueryString["novo"] != null)
        {
            Response.Redirect(String.Format("~/workflow/fluxo.aspx?cW={0}&cF={1}&vF={2}&tW={3}", Request.QueryString["CWF"].ToString(), CodigoFluxo, sVersaoFluxo, hfCodigoWorkflow.Value));
        }
    }

    private void defineTamanhoPopUpRelatorioResumo()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        pcResumoWf.Width = (larguraPrincipal - 80);
    }

    private void carregaRelResumo()
    {
        try
        {
            RelatorioResumoWorkflow();

            // 'cpAcaoExecutadaComSucesso' -> informação necessária no endCallBack
            pnlCbkEdicaoElementos.JSProperties["cpAcaoExecutadaComSucesso"] = "1";
        }
        catch (Exception ex)
        {
            // 'cpAcaoExecutadaComSucesso' -> informação necessária no endCallBack
            pnlCbkEdicaoElementos.JSProperties["cpAcaoExecutadaComSucesso"] = "0";
            string msg = "Erro ao gerar o relatório resumo do modelo de fluxo: " + ex.Message;
            throw new Exception(msg);
        }
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        string comando = string.Format(@"<script type='text/javascript'>renderizaFlash();</script>");
        this.ClientScript.RegisterStartupScript(this.GetType(), "onLoadCall", comando);

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<link rel='stylesheet' type='text/css' href='../estilos/Workflow.css' />"));
        //Header.Controls.Add(cDados.getLiteral("<script type='text/javascript' language='Javascript' src='scripts/uc_crud_caminhoCondicional.js'></script>"));
        Header.Controls.Add(cDados.getLiteral("<script type='text/javascript' language='Javascript' src='../scripts/PowerChartsJS/FusionCharts.js'></script>"));
        Header.Controls.Add(cDados.getLiteral("<script type='text/javascript' language='Javascript' src='../scripts/WorkflowCharts.js'></script>"));
        Header.Controls.Add(cDados.getLiteral("<script type='text/javascript' language='Javascript' src='../scripts/dom-drag.js'></script>"));
        Header.Controls.Add(cDados.getLiteral("<script type='text/javascript' language='Javascript' src='../scripts/textArea - textInsertion.js'></script>"));
        Header.Controls.Add(cDados.getLiteral("<script type='text/javascript' language='Javascript' src='../scripts/adm_edicaoWorkflows.js'></script>"));
        Header.Controls.Add(cDados.getLiteral(@"  
                <script type='text/javascript' language='javascript'>
                    var elEditor;	// declaración necesaria para el funcionamiento de la librería editor.js
                    var fazerPost = true;
                </script>  "));

        string frameName = string.Empty; // nome do frame no qual a tela será colocada (se algum)
        if (null == Request.QueryString["_NF"])
            frameName = "";
        else
            frameName = Request.QueryString["_NF"].ToString();

        // atribui valor à variável javascript que irá conter o nome do frame dentro da qual estará a tela.
        Header.Controls.Add(cDados.getLiteral(@"<script type='text/javascript' language='javascript'>__frameName = '" + frameName + @"' </script>  "));
        this.TH(this.TS("FusionCharts", "WorkflowCharts", "adm_edicaoWorkflows", "uc_crud_caminhoCondicional"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        //Calcula a altura da tela
        int alturaAux = 0;
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        int altura = (alturaPrincipal - 190);
        if (altura > 0)
            alturaAux = altura - 85;
        alturaDivFlash = alturaAux.ToString() + "px";
        larguraDivFlash = larguraPrincipal - 75 + "px";
        alturaGrafico = alturaAux - 20;
        larguraGrafico = larguraPrincipal - 95;
        //pnFlash.Height = alturaAux + 25;
        pnFlash.Width = new Unit((larguraPrincipal - 75) + "px");
        pnFlash.Style.Add("OVERFLOW", "scroll");

        hfValoresTela.Set("AlturaDivFlash", alturaDivFlash.Replace("px", ""));
        hfValoresTela.Set("LarguraDivFlash", larguraDivFlash.Replace("px", ""));
    }

    private void defineTitulosGrids()
    {
        gvFormularios_etp.SettingsText.Title = @"<table cellpadding=""0"" cellspacing=""0"" style=""width: 100%""><tr><td style=""width: 50px"">
                                            <img style=""cursor: pointer"" onclick=""gvFormularios_etp.AddNewRow();"" src=""../imagens/botoes/incluirReg02.png"" alt=""" + Resources.traducao.adm_edicaoWorkflows_adicionar_novo_formul_rio + @"""/>                                               
                                            </td><td align=""center"">" + Resources.traducao.formul_rios_associados___etapa + @"</td><td style=""width: 50px""></td></tr></table>";

        gv_PessoasAcessos_etp.SettingsText.Title = @"<table cellpadding=""0"" cellspacing=""0"" style=""width: 100%""><tr><td style=""width: 50px"">
                                            <img style=""cursor: pointer"" onclick=""gv_PessoasAcessos_etp.AddNewRow();"" src=""../imagens/botoes/incluirReg02.png"" alt=""" + Resources.traducao.adm_edicaoWorkflows_adicionar_novo_perfil + @"""/>                                               
                                            </td><td align=""center"">" + Resources.traducao.pessoas_com_acesso___etapa + @"</td><td style=""width: 50px""></td></tr></table>";

        gv_GruposNotificados_tmr.SettingsText.Title = @"<table cellpadding=""0"" cellspacing=""0"" style=""width: 100%""><tr><td style=""width: 50px"">
                                            <img style=""cursor: pointer"" onclick=""gv_GruposNotificados_tmr.AddNewRow();"" src=""../imagens/botoes/incluirReg02.png"" alt=""" + Resources.traducao.adm_edicaoWorkflows_adicionar_novo_perfil + @"""/>                                               
                                            </td><td align=""center"">" + Resources.traducao.perfis_notificados + @"</td><td style=""width: 50px""></td></tr></table>";

        gv_Acoes_tmr.SettingsText.Title = @"<table cellpadding=""0"" cellspacing=""0"" style=""width: 100%""><tr><td style=""width: 50px"">
                                          <img style=""cursor: pointer"" onclick="" gv_Acoes_tmr.AddNewRow();"" src=""../imagens/botoes/incluirReg02.png"" alt=""" + Resources.traducao.adm_edicaoWorkflows_adicionar_nova_a__o + @"""/>                                               
                                           </td><td align=""center"">" + Resources.traducao.a__es_autom_ticas + @"</td><td style=""width: 50px""></td></tr></table>";

        gv_GruposNotificados_cnt.SettingsText.Title = @"<table cellpadding=""0"" cellspacing=""0"" style=""width: 100%""><tr><td style=""width: 50px"">
                                            <img style=""cursor: pointer"" onclick=""gv_GruposNotificados_cnt.AddNewRow();"" src=""../imagens/botoes/incluirReg02.png"" alt=""" + Resources.traducao.adm_edicaoWorkflows_adicionar_novo_perfil + @"""/>                                               
                                            </td><td align=""center"">" + Resources.traducao.perfis_notificados + @"</td><td style=""width: 50px""></td></tr></table>";

        gv_Acoes_cnt.SettingsText.Title = @"<table cellpadding=""0"" cellspacing=""0"" style=""width: 100%""><tr><td style=""width: 50px"">
                                            <img style=""cursor: pointer"" onclick=""gv_Acoes_cnt.AddNewRow();"" src=""../imagens/botoes/incluirReg02.png"" alt=""" + Resources.traducao.adm_edicaoWorkflows_adicionar_nova_a__o + @"""/>                                               
                                            </td><td align=""center"">" + Resources.traducao.a__es_autom_ticas + @"</td><td style=""width: 50px""></td></tr></table>";

        gv_GruposNotificados_wf.SettingsText.Title = @"<table cellpadding=""0"" cellspacing=""0"" style=""width: 100%""><tr><td style=""width: 50px"">
                                            <img style=""cursor: pointer"" onclick=""gv_GruposNotificados_wf.AddNewRow();"" src=""../imagens/botoes/incluirReg02.png"" alt=""" + Resources.traducao.adm_edicaoWorkflows_adicionar_novo_perfil + @"""/>                                               
                                            </td><td align=""center"">" + Resources.traducao.perfis_notificados + @"</td><td style=""width: 50px""></td></tr></table>";

        gv_Acoes_wf.SettingsText.Title = @"<table cellpadding=""0"" cellspacing=""0"" style=""width: 100%""><tr><td style=""width: 50px"">
                                            <img style=""cursor: pointer"" onclick=""gv_Acoes_wf.AddNewRow();"" src=""../imagens/botoes/incluirReg02.png"" alt=""Adicionar Nova Ação""/>                                               
                                            </td><td align=""center"">" + Resources.traducao.a__es_autom_ticas + @"</td><td style=""width: 50px""></td></tr></table>";

        gv_Acionamentos.SettingsText.Title = @"<table runat""server"" cellpadding=""0"" cellspacing=""0"" style=""width: 100%""><tr><td style=""width: 50px"">
                                            <img id=""tabelaAcionamentosAPI"" style=""cursor: pointer"" onclick=""gv_Acionamentos.AddNewRow();"" src=""../imagens/botoes/incluirReg02.png"" alt=""Adicionar Nova URL de API""/>                                               
                                            </td><td align=""center"">" + "Acionamentos de API" + @"</td><td style=""width: 50px""></td></tr></table>";

        gv_ParametrosAcionamentos.SettingsText.Title = @"<table cellpadding=""0"" cellspacing=""0"" style=""width: 100%""><tr><td style=""width: 50px"">
                                            <img style=""cursor: pointer"" onclick=""processaClickIncluirGridParametros();"" src=""../imagens/botoes/incluirReg02.png"" alt=""Adicionar Novo Parâmetro de API""/>                                               
                                            </td><td align=""center"">" + "Parâmetros" + @"</td><td style=""width: 50px""></td></tr></table>";

    }

    private void limpaGrids(ASPxGridView grid)
    {
        int linhas = grid.VisibleRowCount;
        for (int i = 0; i <= linhas; i++)
        {
            grid.DeleteRow(0);
        }
    }

    private void RecriarColumnas(int columnas)
    {   //Ista funçõe setea a visibilidade das columnas da grid gvEdicaoElementos. Sus valores são:
        // 1 - visible so Descrição
        // 2 - visible so Descrição, Origem y Destino.
        if (columnas == 1)
        {
            //gvEdicaoElementos.Columns[
            gvEdicaoElementos.Columns["origem"].Visible = false;
            gvEdicaoElementos.Columns["destino"].Visible = false;
        }
        else
        {
            gvEdicaoElementos.Columns["origem"].Visible = true;
            gvEdicaoElementos.Columns["destino"].Visible = true;
        }
    }

    protected void pnlCbkEdicaoElementos_Callback(object sender, CallbackEventArgsBase e)
    {
        //--------------------[Variaveis locais.
        string opcao = e.Parameter; //pego a opção que foi feito click no menu
        hfValoresTela.Set("AcaoWf", "");

        // 'cpItemEmEdicao' -> informação necessária no endCallBack, e no customButtonClick
        gvEdicaoElementos.JSProperties["cpItemEmEdicao"] = opcao;
        pnlCbkEdicaoElementos.JSProperties["cpAcaoEscolhida"] = "";
        pnlCbkEdicaoElementos.JSProperties["cpOpcaoClicada"] = opcao;
        string nomeEtapa = string.Empty;
        XmlNodeList list = obtemListaDataSetFromXmlTela();
        XmlNodeList listaTipoElemento;

        //--------------------[Variavel para tabela no memoria.
        DataColumn Etapa = new DataColumn();
        Etapa.Caption = "idEtapa";
        Etapa.ColumnName = "idEtapa";
        tabelaMenu.Columns.Add(Etapa);

        DataColumn connector = new DataColumn();
        connector.Caption = "Elemento";
        connector.ColumnName = "descricao";
        tabelaMenu.Columns.Add(connector);

        DataColumn fromColum = new DataColumn();
        fromColum.Caption = "Inicio";
        fromColum.ColumnName = "origem";
        tabelaMenu.Columns.Add(fromColum);

        DataColumn idfromColum = new DataColumn();
        idfromColum.ColumnName = "idorigem";
        tabelaMenu.Columns.Add(idfromColum);

        DataColumn toColum = new DataColumn();
        toColum.Caption = "Fim";
        toColum.ColumnName = "destino";
        tabelaMenu.Columns.Add(toColum);

        DataColumn idtoColum = new DataColumn();
        idtoColum.ColumnName = "iddestino";
        tabelaMenu.Columns.Add(idtoColum);

        DataRow linha;

        //----------[ETAPAS
        if ((opcao == "Etapas") || (opcao == "Subprocessos")) //si foi feito click no etapas...
        {
            RecriarColumnas(1); //En la grid de Etapa, ten 1 columna a grid.
            listaTipoElemento = obtemListaTipoElementoXMLTela("1");
            foreach (XmlElement etapa in listaTipoElemento)
            {
                string nomeElemento = etapa.Attributes["name"].Value;
                string idEtapa = etapa.Attributes["id"].Value;
                int codigoSubfluxo = int.Parse(etapa.Attributes["codigoSubfluxo"].Value);

                if (((opcao == "Etapas") && (codigoSubfluxo == 0)) || ((opcao == "Subprocessos") && (codigoSubfluxo > 0)))
                {

                    linha = tabelaMenu.NewRow();
                    linha[0] = idEtapa;
                    linha[1] = nomeElemento;

                    tabelaMenu.Rows.Add(linha);
                }
                //System.Diagnostics.Debug.WriteLine(nomeElemento);
            }
            tabelaMenu.DefaultView.Sort = "descricao";
            gvEdicaoElementos.DataSource = tabelaMenu;

            gvEdicaoElementos.DataBind();
        }

        //-----------[CONECTORES.
        else if (opcao == "Conectores")
        {   //NOVO METODO
            string idOrigem = "", nomeOrigem = "", idOrigemFinal = "", nomeOrigemFinal = "";
            string idDestino = "", nomeDestino = "", idDestinoFinal = "", nomeDestinoFinal = "";
            string acao = "";
            string connectorLabel = "";
            string[] labels;

            XmlNodeList conectores;
            XmlElement elementoOrigem, elementoDestino;
            string tipoElementoOrigem, tipoElementoDestino;

            RecriarColumnas(2);

            conectores = obtemListaConnectorsFromXmlTela();

            foreach (XmlElement ligacao in conectores)
            {
                idOrigem = ligacao.GetAttribute("from");
                //if ("Início" == idOrigem)
                //    idOrigem = "0";
                elementoOrigem = (XmlElement)obtemListaElementosXmlTelaPorId(idOrigem).Item(0);
                tipoElementoOrigem = elementoOrigem.GetAttribute("tipoElemento");

                idDestino = ligacao.GetAttribute("to");
                elementoDestino = (XmlElement)obtemListaElementosXmlTelaPorId(idDestino).Item(0);
                tipoElementoDestino = elementoDestino.GetAttribute("tipoElemento");

                if ((tipoElementoOrigem.LastIndexOfAny(new char[] { '0', '3', '7' }) >= 0) ||
                    (tipoElementoDestino.LastIndexOfAny(new char[] { '0', '3', '7' }) >= 0))

                    continue; // se a origem ou o destino for o início, um timer, ou o caminho condicional, desconsidera.
                else
                {
                    connectorLabel = ligacao.GetAttribute("label");
                    nomeOrigem = obtemNomeEtapaFromXmlTela(elementoOrigem);
                    nomeDestino = obtemNomeEtapaFromXmlTela(elementoDestino);
                    string arrowAtEnd = ligacao.GetAttribute("arrowAtEnd");

                    // se houve '/' no label, é sinal de que a conexão é bidirecional
                    labels = connectorLabel.Split(new Char[] { '/' });

                    // faz loop por no máximo 2 vezes
                    for (int i = 0; ((i < labels.Length) && (i < 2)); i++)
                    {
                        acao = labels[i];
                        if ((1 == i) || (arrowAtEnd.Equals("0")))
                        {
                            idOrigemFinal = idDestino;
                            nomeOrigemFinal = nomeDestino;
                            idDestinoFinal = idOrigem;
                            nomeDestinoFinal = nomeOrigem;
                        }
                        else
                        {
                            idOrigemFinal = idOrigem;
                            nomeOrigemFinal = nomeOrigem;
                            idDestinoFinal = idDestino;
                            nomeDestinoFinal = nomeDestino;
                        }

                        linha = tabelaMenu.NewRow();
                        linha[1] = acao;
                        linha[2] = nomeOrigemFinal;
                        linha[3] = idOrigemFinal;
                        linha[4] = nomeDestinoFinal;
                        linha[5] = idDestinoFinal;
                        tabelaMenu.Rows.Add(linha);
                    }// for                
                } // else (("3" == tipoElementoOrigem) || ("3" == tipoElementoDestino))
            } // foreach (XmlElement ligacao in conectores)

            //Ordenar o dataTable pela columna origem.
            DataTable dtAux = tabelaMenu.Clone();
            DataRow[] drs = tabelaMenu.Select(" 1 = 1 ", "origem");
            foreach (DataRow dr in drs)
            {
                dtAux.ImportRow(dr);
            }
            tabelaMenu.DefaultView.Sort = "origem";
            gvEdicaoElementos.DataSource = dtAux;
            gvEdicaoElementos.DataBind();

            //lblCaptionConector.Text = "Informações do Conector -";
        } // else if (opcao == "Conectores")

        //----------[TIMERS.
        else if (opcao == "Timers")
        {
            string idTimer = "";
            string idOrigem = "";
            string origem = "";
            string timeout = "";
            string idDestino = "";
            string destino = "";
            string toolText = "";

            RecriarColumnas(2);
            listaTipoElemento = obtemListaTipoElementoXMLTela("3");

            foreach (XmlElement timers in listaTipoElemento)
            {
                idTimer = timers.GetAttribute("id");

                idOrigem = obtemIdFromTimer(idTimer);
                origem = obtemNomeEtapaFromXmlTela(idOrigem);
                idDestino = obtemIdToTimer(idTimer);
                destino = obtemNomeEtapaFromXmlTela(idDestino);

                toolText = timers.GetAttribute("toolText");
                timeout = toolText.Substring(0, toolText.IndexOf(Resources.traducao.adm_edicaoWorkflows_ap_s));
                linha = tabelaMenu.NewRow();
                linha[0] = idTimer;
                linha[1] = timeout;
                linha[2] = origem;
                linha[3] = idOrigem;
                linha[4] = destino;
                linha[5] = idDestino;
                tabelaMenu.Rows.Add(linha);
            }

            DataTable dtAux = tabelaMenu.Clone();
            DataRow[] drs = tabelaMenu.Select(" 1 = 1 ", "descricao");
            foreach (DataRow dr in drs)
            {
                dtAux.ImportRow(dr);
            }
            tabelaMenu.DefaultView.Sort = "origem";
            gvEdicaoElementos.DataSource = tabelaMenu;
            gvEdicaoElementos.DataBind();
        }

        //----------[DISJUNÇÕES.
        else if (opcao == "Disjuncoes") //si foi feito click no ...
        {
            RecriarColumnas(1); //En la grid de Etapa, ten 1 columna a grid.
            listaTipoElemento = obtemListaTipoElementoXMLTela("4");

            foreach (XmlElement etapa in listaTipoElemento)
            {
                string nomeElemento = etapa.Attributes["toolText"].Value;
                string idElemento = etapa.Attributes["id"].Value;

                linha = tabelaMenu.NewRow();
                linha[0] = idElemento;
                linha[1] = nomeElemento;

                tabelaMenu.Rows.Add(linha);
                //System.Diagnostics.Debug.WriteLine(nomeElemento);
            }
            gvEdicaoElementos.DataSource = tabelaMenu;
            gvEdicaoElementos.DataBind();
        }

        //----------[JUNÇÕES.
        else if (opcao == "Juncoes") //si foi feito click no ...
        {
            RecriarColumnas(1); //En la grid de Etapa, ten 1 columna a grid.
            listaTipoElemento = obtemListaTipoElementoXMLTela("5");

            foreach (XmlElement etapa in listaTipoElemento)
            {
                string nomeElemento = etapa.Attributes["toolText"].Value;
                string idElemento = etapa.Attributes["id"].Value;

                linha = tabelaMenu.NewRow();
                linha[0] = idElemento;
                linha[1] = nomeElemento;

                tabelaMenu.Rows.Add(linha);
                //System.Diagnostics.Debug.WriteLine(nomeElemento);
            }
            gvEdicaoElementos.DataSource = tabelaMenu;
            gvEdicaoElementos.DataBind();
        }

        //----------[FINS.
        else if (opcao == "Fins") //si foi feito click no ...
        {
            RecriarColumnas(1); //En la grid de Etapa, ten 1 columna a grid.
            listaTipoElemento = obtemListaTipoElementoXMLTela("6");

            foreach (XmlElement etapa in listaTipoElemento)
            {
                string nomeElemento = etapa.Attributes["toolText"].Value;
                string idElemento = etapa.Attributes["id"].Value;

                linha = tabelaMenu.NewRow();
                linha[0] = idElemento;
                linha[1] = nomeElemento;

                tabelaMenu.Rows.Add(linha);
                //System.Diagnostics.Debug.WriteLine(nomeElemento);
            }
            gvEdicaoElementos.DataSource = tabelaMenu;
            gvEdicaoElementos.DataBind();
        }

        //-----------[AÇÃOS INICIA / CANCELA FLUXO.
        else if (opcao == "AcaoWf")
        {
            hfValoresTela.Set("AcaoWf", "S");

            XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
            XmlNodeList inicioFluxo;
            XmlNodeList etapas;
            string idInicioFluxo = "0";
            string nomeInicioFluxo = Resources.traducao.adm_edicaoWorkflows_in_cio;

            string nomeEtapaInicial = "";
            string toEtapaInicial = "";
            string nomeToEtapaInicial = "";
            string actionType = "";

            inicioFluxo = xmlDoc.SelectNodes(string.Format("/chart/dataSet/set[@id='{0}']/acoes/acao", idInicioFluxo));
            RecriarColumnas(1);

            foreach (XmlElement inicio in inicioFluxo)
            {
                nomeEtapaInicial = inicio.GetAttribute("id");
                toEtapaInicial = inicio.GetAttribute("to");
                actionType = inicio.GetAttribute("actionType");
                etapas = xmlDoc.SelectNodes(string.Format("/chart/dataSet/set[@id='{0}']", toEtapaInicial));
                foreach (XmlElement nomeEtapaTo in etapas)
                {
                    nomeToEtapaInicial = nomeEtapaTo.GetAttribute("name");
                }
                linha = tabelaMenu.NewRow();
                linha[1] = nomeEtapaInicial;
                linha[2] = nomeInicioFluxo;
                linha[3] = idInicioFluxo;
                linha[4] = nomeToEtapaInicial;
                linha[5] = toEtapaInicial;
                tabelaMenu.Rows.Add(linha);
            }

            DataTable dtAux = tabelaMenu.Clone();
            DataRow[] drs = tabelaMenu.Select(" 1 = 1 ", "origem");
            foreach (DataRow dr in drs)
            {
                dtAux.ImportRow(dr);
            }
            tabelaMenu.DefaultView.Sort = "origem";
            gvEdicaoElementos.DataSource = dtAux;
            gvEdicaoElementos.DataBind();

            //lblCaptionConector.Text = "Açãos Inicia / Cancela Fluxo -";
        } // else if (opcao == "AcaoWf")

        // Caminho condicional - ACG: 23/11/2015
        else if ("CaminhoCondicional" == opcao)
        {
            RecriarColumnas(1);
            listaTipoElemento = obtemListaTipoElementoXMLTela("7");
            foreach (XmlElement etapa in listaTipoElemento)
            {
                string nomeElemento = etapa.Attributes["name"].Value;
                string idEtapa = etapa.Attributes["id"].Value;

                linha = tabelaMenu.NewRow();
                linha[0] = idEtapa;
                linha[1] = nomeElemento;

                tabelaMenu.Rows.Add(linha);
            }
            tabelaMenu.DefaultView.Sort = "descricao";
            gvEdicaoElementos.DataSource = tabelaMenu;
            gvEdicaoElementos.DataBind();
        }

        //-----------[SALVAR
        else if ("salvar" == opcao)
        {
            // 'cpAcaoEscolhida' -> informação necessária no endCallBack
            pnlCbkEdicaoElementos.JSProperties["cpAcaoEscolhida"] = "salvar";

            try
            {
                GravaWorkflow();

                // 'cpAcaoExecutadaComSucesso' -> informação necessária no endCallBack
                pnlCbkEdicaoElementos.JSProperties["cpAcaoExecutadaComSucesso"] = "1";
            }
            catch (Exception ex)
            {
                // 'cpAcaoExecutadaComSucesso' -> informação necessária no endCallBack
                pnlCbkEdicaoElementos.JSProperties["cpAcaoExecutadaComSucesso"] = "0";
                string msg = "Erro ao gravar o modelo de fluxo: " + ex.Message;
                throw new Exception(msg);
            }
        }

        //-----------[PUBLICAR
        else if ("publicar" == opcao)
        {
            // 'cpAcaoEscolhida' -> informação necessária no endCallBack
            pnlCbkEdicaoElementos.JSProperties["cpAcaoEscolhida"] = "publicar";
            try
            {
                PublicaVersaoWorkflow();

                // 'cpAcaoExecutadaComSucesso' -> informação necessária no endCallBack
                pnlCbkEdicaoElementos.JSProperties["cpAcaoExecutadaComSucesso"] = "1";
            }
            catch (Exception ex)
            {
                // 'cpAcaoExecutadaComSucesso' -> informação necessária no endCallBack
                pnlCbkEdicaoElementos.JSProperties["cpAcaoExecutadaComSucesso"] = "0";
                string msg = "Erro ao publicar versão: " + ex.Message;
                throw new Exception(msg);
            }
        }
    }

    #endregion

    #region RELATÓRIO RESUMO

    private void RelatorioResumoWorkflow()
    {
        string grupoWorkflow;
        string idSubWorkFlow;
        string[] sIdPrimeiraEtapa;
        string[] sIdEtapasFinais;
        string[] sIdExceto = new string[32];
        DataSet dsXML = new DataSet();
        XmlReader xmlwf = XmlReader.Create(new StringReader(hfValoresTela.Get("XMLWF").ToString()));
        dsXML.ReadXml(xmlwf);

        // -------------------------------------------------------
        //        DataTable dtResult = new DataTable();
        dtResult = new DataTable();
        dtResult.Columns.Add("Codigo");
        dtResult.Columns.Add("CodigoPai");
        dtResult.Columns.Add("Icone");
        dtResult.Columns.Add("Hint");
        dtResult.Columns.Add("Nome");
        dtResult.Columns.Add("Descricao");
        dtResult.Columns.Add("id");
        // -------------------------------------------------------

        if (dsXML.Tables["subworkflow"] != null)
        {
            if (dsXML.Tables["subworkflow"].Rows.Count > 1)
            {
                // Obtém a etapa Início
                getDadosInicio(dsXML.Tables["set"], ref dtResult);

                foreach (DataRow drWf in dsXML.Tables["workflows"].Rows)
                {
                    foreach (DataRow drSubWf in dsXML.Tables["subworkflow"].Select("workflows_Id='" + drWf["workflows_Id"] + "'", "id"))
                    {
                        idSubWorkFlow = "SWK_" + drSubWf["id"];
                        grupoWorkflow = drSubWf["id"].ToString();

                        // Insere o registro do subworkflow --------------------------------
                        DataRow novaLinha = dtResult.NewRow();
                        novaLinha["Codigo"] = idSubWorkFlow;
                        novaLinha["CodigoPai"] = "";
                        novaLinha["Icone"] = "subworkflow";
                        if (drSubWf["etapaIniciadora"].ToString() == "0")
                        {
                            novaLinha["Hint"] = Resources.traducao.adm_edicaoWorkflows_fluxo_principal;
                            novaLinha["Nome"] = "Fluxo Principal";
                        }
                        else
                        {
                            novaLinha["Hint"] = Resources.traducao.adm_edicaoWorkflows_subfluxo;
                            novaLinha["Nome"] = "SubFluxo " + drSubWf["id"].ToString();
                        }
                        novaLinha["Descricao"] = "Etapa inicial: " + dsXML.Tables["set"].Select("id='" + drSubWf["etapaInicialSubWorkflow"] + "'")[0]["toolText"].ToString();
                        dtResult.Rows.Add(novaLinha);
                        // ------------------------------------------------------------------

                        getDadosSet(dsXML.Tables["set"], grupoWorkflow, idSubWorkFlow, ref dtResult, null, new string[1]);
                    }
                }
                getDadosPrazoPrevisto(dsXML.Tables["prazoPrevisto"], ref dtResult);
                getDadosGruposComAcesso(dsXML.Tables["gruposComAcesso"], dsXML.Tables["grupo"], ref dtResult);
                getDadosFormularios(dsXML, dsXML.Tables["formularios"], dsXML.Tables["formulario"], ref dtResult);
                getDadosAcoes(dsXML, dsXML.Tables["acoes"], dsXML.Tables["acao"], ref dtResult);
                getDadosGruposNotificados(dsXML, dsXML.Tables["gruposNotificados"], dsXML.Tables["grupo"], ref dtResult);
                getDadosAcoesAutomaticas(dsXML, dsXML.Tables["acoesAutomaticas"], dsXML.Tables["acaoAutomatica"], ref dtResult);

                // Obtém a(s) etapa(s) de Fim
                getDadosFim(dsXML.Tables["set"], ref dtResult);

            }
            else
            {
                // Obtém a etapa Início
                getDadosInicio(dsXML.Tables["set"], ref dtResult);

                // Obtém a primeira etapa do fluxo
                sIdPrimeiraEtapa = getEtapaInicial(dsXML);

                //Obtém as etapas Finais
                sIdEtapasFinais = getEtapasFinais(dsXML);

                sIdEtapasFinais.CopyTo(sIdExceto, 0);
                adicionaElementoNumArray(ref sIdExceto, sIdPrimeiraEtapa[0]);

                grupoWorkflow = dsXML.Tables["subworkflow"].Rows[0]["id"].ToString();

                getDadosSet(dsXML.Tables["set"], grupoWorkflow, "", ref dtResult, sIdPrimeiraEtapa, sIdEtapasFinais);
                getDadosSet(dsXML.Tables["set"], grupoWorkflow, "", ref dtResult, null, sIdExceto);
                getDadosSet(dsXML.Tables["set"], grupoWorkflow, "", ref dtResult, sIdEtapasFinais, new string[1]);

                getDadosFormularios(dsXML, dsXML.Tables["formularios"], dsXML.Tables["formulario"], ref dtResult);
                getDadosPrazoPrevisto(dsXML.Tables["prazoPrevisto"], ref dtResult);
                getDadosAcoes(dsXML, dsXML.Tables["acoes"], dsXML.Tables["acao"], ref dtResult);
                getDadosGruposComAcesso(dsXML.Tables["gruposComAcesso"], dsXML.Tables["grupo"], ref dtResult);
                getDadosGruposNotificados(dsXML, dsXML.Tables["gruposNotificados"], dsXML.Tables["grupo"], ref dtResult);
                getDadosAcoesAutomaticas(dsXML, dsXML.Tables["acoesAutomaticas"], dsXML.Tables["acaoAutomatica"], ref dtResult);

                // Obtém a(s) etapa(s) de Fim
                getDadosFim(dsXML.Tables["set"], ref dtResult);

            }
        }
        tlResumoWf.DataSource = dtResult;
        tlResumoWf.DataBind();
    }

    private void getDadosSet(DataTable dtIn, string idGrupoWorkflow, string subWfPai, ref DataTable dtOut, string[] somenteEstasEtapas, string[] excetoEstasEtapas)
    {
        DataTable dtAux = dtIn.Clone();

        DataRow[] drAux = dtIn.Select("grupoWorkflow = '" + idGrupoWorkflow + "'", "id");

        foreach (DataRow dr in drAux)
            dtAux.ImportRow(dr);

        foreach (DataRow dr in dtAux.Rows)
        {
            DataRow novaLinha = dtOut.NewRow();

            if ((somenteEstasEtapas == null || verificaArrayContemElemento(somenteEstasEtapas, dr["id"].ToString())) && (excetoEstasEtapas != null && !verificaArrayContemElemento(excetoEstasEtapas, dr["id"].ToString())))
            {

                if (dr["shape"].ToString() != "POLYGON" && dr["shape"].ToString() != "CIRCLE")
                {
                    novaLinha["Codigo"] = "SET_" + dr["set_Id"];
                    novaLinha["CodigoPai"] = subWfPai;
                    if (dr["tipoElemento"].ToString() == "1")
                    {
                        novaLinha["Icone"] = "Etapa";
                        novaLinha["Hint"] = Resources.traducao.adm_edicaoWorkflows_etapa__duplo_clique_para_editar_;
                        novaLinha["id"] = dr["id"];
                        novaLinha["Nome"] = dr["name"].ToString();
                    }
                    else if (dr["tipoElemento"].ToString() == "4")
                    {
                        novaLinha["Icone"] = "Disjuncao";
                        novaLinha["Hint"] = Resources.traducao.adm_edicaoWorkflows_disjun__o;
                        novaLinha["Nome"] = "Disjunção";
                    }
                    else if (dr["tipoElemento"].ToString() == "5")
                    {
                        novaLinha["Icone"] = "Juncao";
                        novaLinha["Hint"] = Resources.traducao.adm_edicaoWorkflows_jun__o;
                        novaLinha["Nome"] = "Junçãosdfsdf";
                    }
                    else if (dr["tipoElemento"].ToString() == "6")
                    {
                        novaLinha["Icone"] = "Fim";
                        novaLinha["Hint"] = Resources.traducao.adm_edicaoWorkflows_fim;
                        novaLinha["Nome"] = dr["toolText"].ToString(); ;
                    }
                    else
                        continue;

                    if (dr.Table.Columns.Contains("descricao"))
                        novaLinha["Descricao"] = dr["descricao"] + "";
                    else
                        novaLinha["Descricao"] = "";

                    dtOut.Rows.Add(novaLinha);
                }
            }
        }
    }

    private bool verificaArrayContemElemento(string[] sArray, string sElemento)
    {
        foreach (string sAux in sArray)
        {
            if (sAux == sElemento)
                return true;
        }
        return false;
    }

    private void adicionaElementoNumArray(ref string[] sArray, string Elemento)
    {
        for (int i = 0; i < sArray.Length; i++)
        {
            if (sArray[i] == null)
            {
                sArray[i] = Elemento;
                break;
            }
        }
        return;

    }

    private void getDadosAcoes(DataSet dsIn_Completo, DataTable dtIn1, DataTable dtIn2, ref DataTable dtOut)
    {
        if (dtIn1 == null || dtIn2 == null)
            return;

        string atributosAcao;
        string codigoAcao;
        int i = 678;

        foreach (DataRow dr in dtIn1.Rows)
        {
            DataRow[] drs = dtIn2.Select("acoes_Id = '" + dr["acoes_Id"] + "'");

            foreach (DataRow drAcao in drs)
            {
                i++;
                DataRow novaLinha = dtOut.NewRow();
                atributosAcao = "";
                codigoAcao = (drAcao.Table.Columns.Contains("acao_Id") ? "ACT_" + drAcao["acao_Id"] : "ACS_" + i.ToString());
                novaLinha["Codigo"] = codigoAcao;
                novaLinha["CodigoPai"] = "SET_" + dr["set_Id"];

                //                <acao id="timer" nextStageId="7" to="7" actionType="T" timeoutValue="2" timeoutUnit="dias">

                if (drAcao["id"].ToString() == "timer" && drAcao["actionType"].ToString() == "T")
                {
                    novaLinha["Icone"] = "Timer";
                    novaLinha["Hint"] = "Timer";
                    novaLinha["Nome"] = drAcao["timeoutValue"].ToString() + " " + drAcao["timeoutUnit"].ToString();
                }
                else
                {
                    novaLinha["Icone"] = "Acao";
                    novaLinha["Hint"] = Resources.traducao.adm_edicaoWorkflows_a__o;
                    novaLinha["Nome"] = Resources.traducao.adm_edicaoWorkflows_a__o_ + " " + drAcao["id"].ToString();
                }
                if (drAcao["to"].ToString().Trim().Length > 0)
                    if (dsIn_Completo.Tables["set"].Select("id='" + drAcao["to"] + "'").Length > 0)
                        atributosAcao = Resources.traducao.adm_edicaoWorkflows_etapa_destino_ + " " + dsIn_Completo.Tables["set"].Select("id='" + drAcao["to"] + "'")[0]["toolText"].ToString();
                novaLinha["Descricao"] = atributosAcao;
                dtOut.Rows.Add(novaLinha);

                if (drAcao.Table.Columns.Contains("assuntoNotificacao") && drAcao["assuntoNotificacao"].ToString().Length > 0)
                {
                    DataRow novaLinha2 = dtOut.NewRow();
                    novaLinha2["Codigo"] = "TX1_" + drAcao["acao_Id"];
                    novaLinha2["CodigoPai"] = codigoAcao;
                    novaLinha2["Icone"] = "Texto1";
                    novaLinha2["Hint"] = Resources.traducao.adm_edicaoWorkflows_notifica__o__a__o_;
                    novaLinha2["Nome"] = Resources.traducao.adm_edicaoWorkflows_assunto_ + " " + drAcao["assuntoNotificacao"].ToString();
                    novaLinha2["Descricao"] = drAcao["textoNotificacao"].ToString();
                    dtOut.Rows.Add(novaLinha2);
                }
                if (drAcao.Table.Columns.Contains("assuntoNotificacao2") && drAcao["assuntoNotificacao2"].ToString().Length > 0)
                {
                    DataRow novaLinha3 = dtOut.NewRow();
                    novaLinha3["Codigo"] = "TX2_" + drAcao["acao_Id"];
                    novaLinha3["CodigoPai"] = codigoAcao;
                    novaLinha3["Icone"] = "Texto";
                    novaLinha3["Hint"] = Resources.traducao.adm_edicaoWorkflows_notifica__o__acompanhamento_;
                    novaLinha3["Nome"] = Resources.traducao.adm_edicaoWorkflows_assunto_ + " " + drAcao["assuntoNotificacao2"].ToString();
                    novaLinha3["Descricao"] = drAcao["textoNotificacao2"].ToString();
                    dtOut.Rows.Add(novaLinha3);
                }

            }
        }
    }

    private void getDadosGruposNotificados(DataSet dsIn_Completo, DataTable dtIn1, DataTable dtIn2, ref DataTable dtOut)
    {
        if (dtIn1 == null || dtIn2 == null)
            return;

        int nContaAux = 0;
        foreach (DataRow dr in dtIn1.Rows)
        {
            DataRow[] drs = dtIn2.Select("gruposNotificados_Id = '" + dr["gruposNotificados_Id"] + "'");

            foreach (DataRow drgrpNot in drs)
            {
                DataRow novaLinha = dtOut.NewRow();
                novaLinha["Codigo"] = "GRN_" + dr["gruposNotificados_Id"] + "_" + nContaAux.ToString();
                novaLinha["CodigoPai"] = "ACT_" + dr["acao_Id"];
                novaLinha["Icone"] = "GrupoNotificado";
                novaLinha["Hint"] = Resources.traducao.adm_edicaoWorkflows_grupo_notificado;
                novaLinha["Nome"] = Resources.traducao.adm_edicaoWorkflows_grupo_notificado_ + " " + drgrpNot["name"].ToString();
                if (drgrpNot.Table.Columns.Contains("msgBox"))
                    if (drgrpNot["msgBox"].ToString() == Resources.traducao.adm_edicaoWorkflows_a__o)
                        novaLinha["Descricao"] = Resources.traducao.adm_edicaoWorkflows_notifica__o_a__o;
                    else if (drgrpNot["msgBox"].ToString() == Resources.traducao.adm_edicaoWorkflows_acompanhamento)
                        novaLinha["Descricao"] = Resources.traducao.adm_edicaoWorkflows_notifica__o_acompanhamento;
                dtOut.Rows.Add(novaLinha);
                nContaAux++;
            }
        }
    }

    private void getDadosAcoesAutomaticas(DataSet dsIn_Completo, DataTable dtIn1, DataTable dtIn2, ref DataTable dtOut)
    {
        if (dtIn1 == null || dtIn2 == null)
            return;

        int nContaAux = 0;
        foreach (DataRow dr in dtIn1.Rows)
        {
            DataRow[] drs = dtIn2.Select("acoesAutomaticas_Id = '" + dr["acoesAutomaticas_Id"] + "'");

            foreach (DataRow drAcaoAuto in drs)
            {
                DataRow novaLinha = dtOut.NewRow();
                novaLinha["Codigo"] = "AUT_" + drAcaoAuto["id"] + "_" + nContaAux.ToString();
                novaLinha["CodigoPai"] = "ACT_" + dr["acao_Id"];
                novaLinha["Icone"] = "AcaoAutomatica";
                novaLinha["Hint"] = "Ação automática";
                novaLinha["Nome"] = "Ação automática: " + drAcaoAuto["name"].ToString();
                dtOut.Rows.Add(novaLinha);
                nContaAux++;
            }
        }
    }

    private void getDadosFormularios(DataSet dsIn_Completo, DataTable dtIn1, DataTable dtIn2, ref DataTable dtOut)
    {
        if (dtIn1 == null || dtIn2 == null)
            return;

        string atributosFormulario;
        string etapaOrigemFormulario;
        int nContaAux = 0;
        foreach (DataRow dr in dtIn1.Rows)
        {
            DataRow[] drs = dtIn2.Select("formularios_Id = '" + dr["formularios_Id"] + "'");

            foreach (DataRow drFrm in drs)
            {
                DataRow novaLinha = dtOut.NewRow();
                atributosFormulario = "";

                novaLinha["Codigo"] = "FRM_" + drFrm["id"] + "_" + nContaAux.ToString();
                novaLinha["CodigoPai"] = "SET_" + dr["set_Id"];
                novaLinha["Icone"] = "Formulario";
                novaLinha["Hint"] = Resources.traducao.adm_edicaoWorkflows_formul_rio;
                novaLinha["Nome"] = Resources.traducao.adm_edicaoWorkflows_formul_rio_ + " " + drFrm["name"].ToString() + " - " + drFrm["title"].ToString();
                atributosFormulario = drFrm["newOnEachOcurrence"].ToString() == "1" ? Resources.traducao.adm_edicaoWorkflows_novo_em_cada_ocorr_ncia + " / " : "";
                atributosFormulario += drFrm["required"].ToString() == "1" ? Resources.traducao.adm_edicaoWorkflows_obrigat_rio + " / " : "";
                atributosFormulario += drFrm["readOnly"].ToString() == "1" ? Resources.traducao.adm_edicaoWorkflows_somente_leitura + "   " : "";
                atributosFormulario = atributosFormulario.Length > 0 ? "(" + atributosFormulario.Substring(0, atributosFormulario.Length - 3) + ")" : "";
                etapaOrigemFormulario = drFrm["originalStageId"].ToString();
                if (int.Parse(etapaOrigemFormulario) > 0)
                {
                    if (dsIn_Completo.Tables["set"].Select("id='" + etapaOrigemFormulario + "'").Length > 0)
                        atributosFormulario = Resources.traducao.adm_edicaoWorkflows_etapa_origem_ + " " + dsIn_Completo.Tables["set"].Select("id='" + etapaOrigemFormulario + "'")[0]["name"].ToString() + "    " + atributosFormulario;
                }
                novaLinha["Descricao"] = atributosFormulario;
                dtOut.Rows.Add(novaLinha);
                nContaAux++;
            }
        }
    }

    private void getDadosPrazoPrevisto(DataTable dtIn1, ref DataTable dtOut)
    {
        if (dtIn1 == null)
            return;

        foreach (DataRow drPrazo in dtIn1.Rows)
        {
            DataRow novaLinha = dtOut.NewRow();
            novaLinha["Codigo"] = "PRZ_" + drPrazo["set_Id"];
            novaLinha["CodigoPai"] = "SET_" + drPrazo["set_Id"];
            novaLinha["Icone"] = "Prazo";
            novaLinha["Hint"] = Resources.traducao.adm_edicaoWorkflows_prazo_previsto;
            novaLinha["Nome"] = Resources.traducao.adm_edicaoWorkflows_prazo_previsto_ + " " + drPrazo["timeoutValue"].ToString() + " " + drPrazo["timeoutUnit"].ToString();
            if (drPrazo.Table.Columns.Contains("timeoutOffset"))
                if (drPrazo["timeoutOffset"].ToString() == "IETP")
                    novaLinha["Descricao"] = Resources.traducao.adm_edicaoWorkflows_a_partir_do_in_cio_da_etapa;
                else if (drPrazo["timeoutOffset"].ToString() == "IFLX")
                    novaLinha["Descricao"] = Resources.traducao.adm_edicaoWorkflows_a_partir_do_in_cio_do_fluxo;
            dtOut.Rows.Add(novaLinha);
        }

    }

    private void getDadosGruposComAcesso(DataTable dtIn1, DataTable dtIn2, ref DataTable dtOut)
    {
        if (dtIn1 == null || dtIn2 == null)
            return;

        int nContaAux = 0;
        foreach (DataRow dr in dtIn1.Rows)
        {
            DataRow[] drs = dtIn2.Select("gruposComAcesso_Id = " + dr["gruposComAcesso_Id"]);

            foreach (DataRow drGrupo in drs)
            {
                DataRow novaLinha = dtOut.NewRow();

                novaLinha["Codigo"] = "GRU_" + drGrupo["id"] + "_" + nContaAux.ToString(); ;
                novaLinha["CodigoPai"] = "SET_" + dr["set_Id"];
                novaLinha["Icone"] = "GrupoAcesso";
                novaLinha["Hint"] = Resources.traducao.adm_edicaoWorkflows_grupo_com_acesso;
                novaLinha["Nome"] = Resources.traducao.adm_edicaoWorkflows_grupo_com_acesso_ + " " + drGrupo["name"].ToString();
                novaLinha["Descricao"] = drGrupo["accessType"].ToString();
                dtOut.Rows.Add(novaLinha);
                nContaAux++;
            }
        }
    }

    private void getDadosInicio(DataTable dtIn, ref DataTable dtOut)
    {
        DataRow dr = dtIn.Select("shape='CIRCLE'")[0];

        DataRow novaLinha = dtOut.NewRow();
        novaLinha["Codigo"] = "SET_" + dr["set_Id"];
        novaLinha["CodigoPai"] = "";
        novaLinha["Icone"] = "Inicio";
        novaLinha["Hint"] = Resources.traducao.adm_edicaoWorkflows_in_cio;
        novaLinha["Nome"] = "Inicio";
        novaLinha["Descricao"] = "";
        dtOut.Rows.Add(novaLinha);
    }

    private string[] getEtapaInicial(DataSet dsIn)
    {
        string[] idEtapaInicial = new string[1];

        DataRow[] dr = dsIn.Tables["subworkflow"].Select("etapaIniciadora=0");
        if (dr != null && dr.Length > 0)
            idEtapaInicial[0] = dr[0]["etapaInicialSubWorkflow"].ToString();

        return idEtapaInicial;
    }

    private string[] getEtapasFinais(DataSet dsIn)
    {
        string[] iDsEtapasFinais = new string[32];
        DataRow[] drIds;
        int i = 0;
        DataRow[] drPontosFinais = dsIn.Tables["set"].Select("shape='POLYGON'");
        foreach (DataRow dr in drPontosFinais)
        {
            drIds = dsIn.Tables["connector"].Select("to='" + dr["id"] + "'");
            foreach (DataRow dr1 in drIds)
            {
                iDsEtapasFinais[i++] = dr1["from"].ToString();
            }

        }
        return iDsEtapasFinais;
    }

    private void getDadosFim(DataTable dtIn, ref DataTable dtOut)
    {
        DataRow[] dr = dtIn.Select("shape='POLYGON'");

        foreach (DataRow drFim in dr)
        {
            DataRow novaLinha = dtOut.NewRow();
            novaLinha["Codigo"] = "SET_" + drFim["set_Id"];
            novaLinha["CodigoPai"] = "";
            novaLinha["Icone"] = "Fim";
            novaLinha["Hint"] = Resources.traducao.adm_edicaoWorkflows_fim;
            novaLinha["Nome"] = drFim["toolText"];
            novaLinha["Descricao"] = "";
            dtOut.Rows.Add(novaLinha);
        }
    }

    public string getDescricaoObjeto()
    {
        string eventoImagem = "", eventoTable = "";

        if (Eval("id") + "" != "")
        {
            eventoImagem = string.Format(@"style='cursor:pointer' ondblclick='xmlWorkflowOk(); painelCallbackEtapas.PerformCallback(""0;{0}"");'", Eval("id"));
            eventoTable = string.Format(@"onclick='if(divEtapa.GetVisible()) painelCallbackEtapas.PerformCallback(""0;{0}"");'", Eval("id"));
        }

        return string.Format(@"<table {4}><tr><td style='padding-right:5px' align='center'><img src='../imagens/Workflow/{0}.jpg' title='{2}' {3} /></td><td>{1}</td></tr></table>"
            , Eval("Icone")
            , Eval("Nome")
            , Eval("Hint")
            , eventoImagem
            , eventoTable);
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

            ListEditItem liRTF = new ListEditItem("RTF", "RTF");
            liRTF.ImageUrl = "~/imagens/menuExportacao/iconoPortfolio.png";
            ddlExporta.Items.Add(liRTF);

        }
        ddlExporta.SelectedIndex = 0;
    }

    protected void pnImage_Callback(object sender, CallbackEventArgsBase e)
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
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_");

            string nomeArquivo = "", app = "", erro = "";

            try
            {
                if (hfTipoArquivo.Get("tipoArquivo").ToString() == "XLS")
                {
                    nomeArquivo = "Fluxo_" + dataHora + ".xls";
                    XlsExportOptions x = new XlsExportOptions();

                    ASPxTreeListExporter1.WriteXls(stream);
                    app = "application/vnd.ms-excel"; //TIPO DE REFERENCIA MAIS UTILIZADA
                    //app = "application/ms-excel";
                }
                if (hfTipoArquivo.Get("tipoArquivo").ToString() == "RTF")
                {
                    nomeArquivo = "recursosHumanosProjetos_" + dataHora + ".rtf";
                    ASPxTreeListExporter1.WriteRtf(stream);
                    app = "application/rtf";
                }
            }
            catch
            {
                erro = "S";
            }
            //app = "application/ms-excel";

            if (erro == "")
            {
                if (hfTipoArquivo.Get("tipoArquivo").ToString() != "HTML")
                {
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

    #endregion

    #region ATUALIZAÇÃO DO VERSÃO WORKFLOW

    private void getConfiguracaoSistemaWorflow()
    {
        DataSet dsConfWf = cDados.getConfiguracaoSistemaWorflow();
        if (cDados.DataSetOk(dsConfWf))
        {
            foreach (DataRow dr in dsConfWf.Tables[0].Rows) //Obtem as configurações do worflow.
            {
                configuracaoWF cWF = new configuracaoWF();
                cWF.parametro = dr["Parametro"].ToString();
                cWF.valor = dr["Valor"].ToString();
                configuracoesWf.Add(cWF);
            }
        }
    }

    private void SetearReferenciaColor()
    {
        Color c;
        string xCol = "";
        xCol = configuracoesWf[_colorConetor].valor.ToString();
        c = System.Drawing.ColorTranslator.FromHtml(xCol);
        lblDescricaoAcoes.ForeColor = c;
    }

    private void BuscaDadosWorkflow()
    {
        string where = string.Empty;
        string descricaoVersao = string.Empty;
        string observacaoVersao = string.Empty;
        string larguraWF = "1150";
        string alturaWF = "508";
        string strXml;

        if ("0" == hfCodigoWorkflow.Value.ToString())
        {
            VersaoWorkflow = cDados.getProximaVersaoFluxo(CodigoFluxo);
            nomeFluxo = cDados.getNomeFluxo(CodigoFluxo);
            strXml = @"<?xml version='1.0' encoding='UTF-8'?>
<chart palette='2' xAxisMinValue='0' xAxisMaxValue='100' yAxisMinValue='0' yAxisMaxValue='100' is3D='1' showFormBtn='0' viewMode='1' showAboutMenuItem='0'>
  <dataSet id='1' seriesName='DS1'>
    <set shape='CIRCLE' id='0' name=' ' toolText='" + Resources.traducao.adm_edicaoWorkflows_in_cio + @"' x='3' y='50' radius='10' color='33C1FE' tipoElemento='0' grupoWorkflow='0' idElementoInicioFluxo='-1'>
      <acoes>
        <acao id='" + Resources.traducao.adm_edicaoWorkflows_in_cio + @"' nextStageId='' to='' actionType='I'>
            <gruposNotificados></gruposNotificados>
            <assuntoNotificacao></assuntoNotificacao>
            <textoNotificacao></textoNotificacao>
            <assuntoNotificacao2></assuntoNotificacao2>
            <textoNotificacao2></textoNotificacao2>
            <acoesAutomaticas></acoesAutomaticas>
        </acao>
        <acao id='Cancelamento' nextStageId='' to='' actionType='C'>
            <gruposNotificados></gruposNotificados>
            <assuntoNotificacao></assuntoNotificacao>
            <textoNotificacao></textoNotificacao>
            <assuntoNotificacao2></assuntoNotificacao2>
            <textoNotificacao2></textoNotificacao2>
            <acoesAutomaticas></acoesAutomaticas>
        </acao>
      </acoes>
    </set>
  </dataSet>
  <connectors stdThickness='5'>
  </connectors>
  <workflows xmlVersion='001.1.028' width='1150' height='508'>
  </workflows>
</chart>";
            atualizaXmlHiddenField(strXml);
        }
        else
        {
            where = "AND [codigoWorkFlow] = " + hfCodigoWorkflow.Value.ToString();

            DataSet ds = cDados.getWorkFlows(where);
            if ((cDados.DataSetOk(ds)) && (cDados.DataTableOk(ds.Tables[0])))
            {
                string dataPublicacao;

                nomeFluxo = ds.Tables[0].Rows[0]["NomeFluxo"].ToString();
                VersaoWorkflow = ds.Tables[0].Rows[0]["VersaoWorkflow"].ToString();
                dataPublicacao = ds.Tables[0].Rows[0]["DataPublicacao"].ToString();

                descricaoVersao = ds.Tables[0].Rows[0]["DescricaoVersao"].ToString();
                observacaoVersao = ds.Tables[0].Rows[0]["Observacao"].ToString();

                strXml = ds.Tables[0].Rows[0]["textoXML"].ToString();

                atualizaXmlHiddenField(strXml);
                atualizaFormatoXmlWorkflow();
                obtemDimensoesWf(ref larguraWF, ref alturaWF);

                // se for edição de uma versão já publicada, gera nova versão e zera código do workflow
                // para que seja gravada uma nova versão;
                if ("" != dataPublicacao)
                {
                    VersaoWorkflow = cDados.getProximaVersaoFluxo(CodigoFluxo);
                    hfCodigoWorkflow.Value = "0";
                }
            }
        }
        AtualizaIdentificacaoWorkflow(nomeFluxo, VersaoWorkflow, descricaoVersao, observacaoVersao, larguraWF, alturaWF);
    }

    private void obtemDimensoesWf(ref string larguraWF, ref string alturaWF)
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList lista = xmlDoc.GetElementsByTagName("workflows");

        foreach (XmlElement elemento in lista)
        {
            larguraWF = elemento.GetAttribute("width");
            alturaWF = elemento.GetAttribute("height");
        }
    }

    private void atualizaXmlHiddenField(string strXml)
    {
        try
        {
            hfValoresTela.Set("XMLWF", strXml);
            XmlReader xmlR = XmlReader.Create(new StringReader(strXml));
            __xmlDoc.Load(xmlR);
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    private string getXmlAsStringFromXmlDoc(XmlDocument xmlDoc)
    {
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Encoding = Encoding.Default;
        //settings.OmitXmlDeclaration = true;

        StringBuilder strBuilder = new StringBuilder();
        XmlWriter xmlWrt = XmlWriter.Create(strBuilder, settings);

        xmlDoc.Save(xmlWrt);

        return strBuilder.ToString();
    }

    /// <summary>
    /// Coloca o XML do workflow no formato final.
    /// </summary>
    /// <remarks>
    /// De acordo com a versão que se encontra o workflow, a função faz as alterações necessárias nele para que
    /// fique no formato da última versão trabalhada
    /// </remarks>
    private void atualizaFormatoXmlWorkflow()
    {
        string versaoFormatoXml = getVersaoFormatoXmlFromXmlTela();

        if (versaoFormatoXml.Equals(""))
            versaoFormatoXml = atualizaVersao11014();

        if (versaoFormatoXml.Equals("001.1.015"))
            versaoFormatoXml = atualizaVersao11015();

        if (versaoFormatoXml.Equals("001.1.016"))
            versaoFormatoXml = atualizaVersao11016();

        if (versaoFormatoXml.Equals("001.1.017"))
            versaoFormatoXml = atualizaVersao11017();

        if (versaoFormatoXml.Equals("001.1.018"))
            versaoFormatoXml = atualizaVersao11018();

        if (versaoFormatoXml.Equals("001.1.019"))
            versaoFormatoXml = atualizaVersao11019();

        if (versaoFormatoXml.Equals("001.1.020"))
            versaoFormatoXml = atualizaVersao11020();

        if (versaoFormatoXml.Equals("001.1.021"))
            versaoFormatoXml = atualizaVersao11021();

        if (versaoFormatoXml.Equals("001.1.022"))
            versaoFormatoXml = atualizaVersao11022();

        if (versaoFormatoXml.Equals("001.1.023"))
            versaoFormatoXml = atualizaVersao11023();

        if (versaoFormatoXml.Equals("001.1.024"))
            versaoFormatoXml = atualizaVersao11024();

        if (versaoFormatoXml.Equals("001.1.025"))
            versaoFormatoXml = atualizaVersao11025();

        if (versaoFormatoXml.Equals("001.1.026"))
            versaoFormatoXml = atualizaVersao11026();

        if (versaoFormatoXml.Equals("001.1.027"))
            versaoFormatoXml = atualizaVersao11027();

        if (versaoFormatoXml.Equals("001.1.028"))
            versaoFormatoXml = atualizaVersao11028();

        if (versaoFormatoXml.Equals("001.1.029"))
            versaoFormatoXml = atualizaVersao11029();
    }

    private string atualizaVersao11014()
    {
        string toConnFrom;
        string toConnTo;
        string nomeAcao;
        string codigoAcao;
        XmlNodeList acoes;

        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList connetores = xmlDoc.SelectNodes("/chart/connectors/connector");
        XmlNode ActionsNode;

        foreach (XmlElement conn in connetores)
        {
            toConnFrom = conn.GetAttribute("from");
            toConnTo = conn.GetAttribute("to");
            ActionsNode = xmlDoc.SelectSingleNode(string.Format("/chart/dataSet/set[@id='{0}']/acoes/acao[@to='{1}']/acoesAutomaticas", toConnFrom, toConnTo));

            if (ActionsNode != null)
            {
                acoes = ActionsNode.SelectNodes("acaoAutomatica");

                foreach (XmlElement acaoAutomatica in acoes)
                {
                    nomeAcao = acaoAutomatica.GetAttribute("id");
                    codigoAcao = cDados.getCodigoAcaoAutomatica(nomeAcao);
                    if (codigoAcao != "")
                    {
                        acaoAutomatica.SetAttribute("name", nomeAcao);
                        acaoAutomatica.SetAttribute("id", codigoAcao);
                    }
                    else
                        ActionsNode.RemoveChild(acaoAutomatica);
                }
            }
        }

        setVersaoFormatoXmlToXmlTela(xmlDoc, "001.1.015");
        __strXmlWorkflow = getXmlAsStringFromXmlDoc(xmlDoc);
        atualizaXmlHiddenField(__strXmlWorkflow);
        return getVersaoFormatoXmlFromXmlTela();
    }

    private string atualizaVersao11015()
    {
        string idEtapa;

        XmlNodeList formularios;

        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList etapas = xmlDoc.SelectNodes("/chart/dataSet/set");

        foreach (XmlElement etapa in etapas)
        {
            idEtapa = etapa.GetAttribute("id");

            formularios = etapa.SelectNodes("formularios/formulario");
            foreach (XmlElement formulario in formularios)
                formulario.SetAttribute("required", "1");
        }

        setVersaoFormatoXmlToXmlTela(xmlDoc, "001.1.016");
        __strXmlWorkflow = getXmlAsStringFromXmlDoc(xmlDoc);
        atualizaXmlHiddenField(__strXmlWorkflow);

        return getVersaoFormatoXmlFromXmlTela();
    }

    private string atualizaVersao11016()
    {
        string toConnFrom;
        string toConnTo;
        XmlNodeList acoes;

        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList connetores = xmlDoc.SelectNodes("/chart/connectors/connector");

        foreach (XmlElement conn in connetores)
        {
            toConnFrom = conn.GetAttribute("from");
            toConnTo = conn.GetAttribute("to");
            acoes = xmlDoc.SelectNodes(string.Format("/chart/dataSet/set[@id='{0}']/acoes/acao[@to='{1}']/acoesAutomaticas/acaoAutomatica", toConnFrom, toConnTo));

            if (acoes.Count > 0)
                conn.SetAttribute("dashed", "1");
        }

        setVersaoFormatoXmlToXmlTela(xmlDoc, "001.1.017");
        __strXmlWorkflow = getXmlAsStringFromXmlDoc(xmlDoc);
        atualizaXmlHiddenField(__strXmlWorkflow);
        return getVersaoFormatoXmlFromXmlTela();
    }

    private string atualizaVersao11017()
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();

        setWidhtHeightXmlToXmlTela("1150", "508");
        setVersaoFormatoXmlToXmlTela(xmlDoc, "001.1.018");
        __strXmlWorkflow = getXmlAsStringFromXmlDoc(xmlDoc);
        atualizaXmlHiddenField(__strXmlWorkflow);
        return getVersaoFormatoXmlFromXmlTela();
    }

    private string atualizaVersao11018()
    {   //metodo llamado por "atualizaFormatoXmlWorkflow()" para atualizar a la versión 1.1.018.

        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList setS = xmlDoc.SelectNodes("/chart/dataSet/set");
        XmlNodeList grupos;
        XmlNodeList grupoAux;
        string nomeGrupo;
        string idGrupo;

        foreach (XmlElement grupo in setS)
        {
            grupos = grupo.SelectNodes("gruposComAcesso");
            if (null != grupos)
            {
                foreach (XmlElement grupoAcesso in grupos)
                {
                    grupoAux = grupoAcesso.SelectNodes("grupo");

                    foreach (XmlElement grupoId in grupoAux)
                    {
                        nomeGrupo = grupoId.GetAttribute("id");
                        idGrupo = cDados.getCodigoGrupoPessoasWf(nomeGrupo);
                        if (idGrupo != "")
                        {
                            grupoId.SetAttribute("name", nomeGrupo);
                            grupoId.SetAttribute("id", idGrupo);
                        }
                    }
                }
            }
            grupos = grupo.SelectNodes("acoes/acao/gruposNotificados");
            if (null != grupos)
            {
                foreach (XmlElement grupoNotificado in grupos)
                {
                    grupoAux = grupoNotificado.SelectNodes("grupo");
                    foreach (XmlElement grupoId in grupoAux)
                    {
                        nomeGrupo = grupoId.GetAttribute("id");
                        idGrupo = cDados.getCodigoGrupoPessoasWf(nomeGrupo);
                        if (idGrupo != "")
                        {
                            grupoId.SetAttribute("name", nomeGrupo);
                            grupoId.SetAttribute("id", idGrupo);
                        }
                    }
                }
            }
        }

        setVersaoFormatoXmlToXmlTela(xmlDoc, "001.1.019");
        __strXmlWorkflow = getXmlAsStringFromXmlDoc(xmlDoc);
        atualizaXmlHiddenField(__strXmlWorkflow);
        return getVersaoFormatoXmlFromXmlTela();
    }

    private string atualizaVersao11019()
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList setS = xmlDoc.SelectNodes("/chart/dataSet/set[@id='0']");
        XmlNodeList acoes;
        string tipoElemento;

        foreach (XmlElement grupo in setS)
        {
            tipoElemento = grupo.GetAttribute("tipoElemento");
            if ("0" == tipoElemento)
            {
                acoes = grupo.SelectNodes("acoes");
                if (acoes.Count == 0)
                {
                    XmlNode newAcoes = xmlDoc.CreateNode(XmlNodeType.Element, "acoes", "");
                    string innerDefinition = @"
                    <acao id='Início' nextStageId='' to='1' actionType='I' />
                    <acao id='Cancelamento' nextStageId='' to='1' actionType='C' />";
                    newAcoes.InnerXml = innerDefinition;
                    grupo.AppendChild(newAcoes);
                }
                break; // sai do loop por que só tem um elemento do tipo="0"
            }
        }

        setVersaoFormatoXmlToXmlTela(xmlDoc, "001.1.020");
        __strXmlWorkflow = getXmlAsStringFromXmlDoc(xmlDoc);
        atualizaXmlHiddenField(__strXmlWorkflow);
        return getVersaoFormatoXmlFromXmlTela();
    }

    private string atualizaVersao11020()
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList etapas = xmlDoc.SelectNodes("/chart/dataSet/set");
        XmlNode prazo;

        foreach (XmlElement etapa in etapas)
        {
            prazo = etapa.SelectSingleNode("prazoPrevisto");
            if (null != prazo)
            {
                XmlAttribute offSet = xmlDoc.CreateAttribute("timeoutOffset");
                offSet.Value = "IETP";
                prazo.Attributes.Append(offSet);
            }
        }

        setVersaoFormatoXmlToXmlTela(xmlDoc, "001.1.021");
        __strXmlWorkflow = getXmlAsStringFromXmlDoc(xmlDoc);
        atualizaXmlHiddenField(__strXmlWorkflow);
        return getVersaoFormatoXmlFromXmlTela();
    }

    private string atualizaVersao11021()
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList etapas = xmlDoc.SelectNodes("/chart/dataSet/set");
        XmlNodeList acoes, grupos;
        XmlNode newNode;
        string TipoMensagem;

        foreach (XmlElement etapa in etapas)
        {
            acoes = etapa.SelectNodes("acoes/acao");
            foreach (XmlElement acao in acoes)
            {
                newNode = xmlDoc.CreateNode(XmlNodeType.Element, "assuntoNotificacao2", "");
                acao.AppendChild(newNode);
                newNode = xmlDoc.CreateNode(XmlNodeType.Element, "textoNotificacao2", "");
                acao.AppendChild(newNode);
            }

            grupos = etapa.SelectNodes("acoes/acao/gruposNotificados/grupo");
            foreach (XmlElement grupo in grupos)
            {
                TipoMensagem = grupo.GetAttribute("msgBox");
                if (TipoMensagem.Equals("Caixa de Saída"))
                    TipoMensagem = "Acompanhamento";
                else
                    TipoMensagem = "Ação";

                grupo.SetAttribute("msgBox", TipoMensagem);
            }
        }

        setVersaoFormatoXmlToXmlTela(xmlDoc, "001.1.022");
        __strXmlWorkflow = getXmlAsStringFromXmlDoc(xmlDoc);
        atualizaXmlHiddenField(__strXmlWorkflow);
        return getVersaoFormatoXmlFromXmlTela();
    }

    private string atualizaVersao11022()
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList etapas = obtemListaTipoElementoXMLTela("1");

        foreach (XmlElement etapa in etapas)
        {
            etapa.SetAttribute("ocultaBotoesAcao", "0");
        }

        setVersaoFormatoXmlToXmlTela(xmlDoc, "001.1.023");
        __strXmlWorkflow = getXmlAsStringFromXmlDoc(xmlDoc);
        atualizaXmlHiddenField(__strXmlWorkflow);
        return getVersaoFormatoXmlFromXmlTela();
    }

    private string atualizaVersao11023()
    {

        string idEtapa;

        XmlNodeList formularios;

        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList etapas = xmlDoc.SelectNodes("/chart/dataSet/set");

        foreach (XmlElement etapa in etapas)
        {
            idEtapa = etapa.GetAttribute("id");

            formularios = etapa.SelectNodes("formularios/formulario");
            foreach (XmlElement formulario in formularios)
                formulario.SetAttribute("requerAssinaturaDigital", "0");                                   // CERTDIG
        }

        setVersaoFormatoXmlToXmlTela(xmlDoc, "001.1.024");
        __strXmlWorkflow = getXmlAsStringFromXmlDoc(xmlDoc);
        atualizaXmlHiddenField(__strXmlWorkflow);

        // fazer aqui procedimentos para atualizar da versão 23 para a próxima, quando for criada a versão 24.
        return getVersaoFormatoXmlFromXmlTela();
    }

    private string atualizaVersao11024()
    {
        string idEtapa;
        int ordemForm;

        XmlNodeList formularios;

        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList etapas = xmlDoc.SelectNodes("/chart/dataSet/set");

        foreach (XmlElement etapa in etapas)
        {
            idEtapa = etapa.GetAttribute("id");
            ordemForm = 1;
            formularios = etapa.SelectNodes("formularios/formulario");
            foreach (XmlElement formulario in formularios)
            {
                formulario.SetAttribute("ordemFormularioEtapa", ordemForm.ToString());                                   // OrdemForm
                ordemForm++;
            }
        }

        setVersaoFormatoXmlToXmlTela(xmlDoc, "001.1.025");
        __strXmlWorkflow = getXmlAsStringFromXmlDoc(xmlDoc);
        atualizaXmlHiddenField(__strXmlWorkflow);

        // fazer aqui procedimentos para atualizar da versão 24 para a próxima, quando for criada a versão 25.
        return getVersaoFormatoXmlFromXmlTela();
    }

    private string atualizaVersao11025()
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList etapas = xmlDoc.SelectNodes("/chart/dataSet/set");
        XmlNodeList acoes;

        foreach (XmlElement etapa in etapas)
        {
            acoes = etapa.SelectNodes("acoes/acao");
            foreach (XmlElement acao in acoes)
            {
                acao.SetAttribute("solicitaAssinaturaDigital", "0");
            }

        }

        setVersaoFormatoXmlToXmlTela(xmlDoc, "001.1.026");
        __strXmlWorkflow = getXmlAsStringFromXmlDoc(xmlDoc);
        atualizaXmlHiddenField(__strXmlWorkflow);
        return getVersaoFormatoXmlFromXmlTela();
    }

    private string atualizaVersao11026()
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList etapas = xmlDoc.SelectNodes("/chart/dataSet/set");
        XmlNodeList acoes;

        foreach (XmlElement etapa in etapas)
        {
            acoes = etapa.SelectNodes("acoes/acao");
            foreach (XmlElement acao in acoes)
            {
                acao.SetAttribute("corBotao", "");
                acao.SetAttribute("iconeBotao", "");
            }
        }

        setVersaoFormatoXmlToXmlTela(xmlDoc, "001.1.027");
        __strXmlWorkflow = getXmlAsStringFromXmlDoc(xmlDoc);
        atualizaXmlHiddenField(__strXmlWorkflow);
        return getVersaoFormatoXmlFromXmlTela();
    }

    private string atualizaVersao11027()
    {

        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList etapas = obtemListaTipoElementoXMLTela("1");

        foreach (XmlElement etapa in etapas)
        {
            etapa.SetAttribute("codigoSubfluxo", "0");
        }

        setVersaoFormatoXmlToXmlTela(xmlDoc, "001.1.028");
        __strXmlWorkflow = getXmlAsStringFromXmlDoc(xmlDoc);
        atualizaXmlHiddenField(__strXmlWorkflow);
        return getVersaoFormatoXmlFromXmlTela();
    }

    private string atualizaVersao11028()
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList etapas = obtemListaTipoElementoXMLTela("1");
        XmlNodeList acoes;

        foreach (XmlElement etapa in etapas)
        {
            acoes = etapa.SelectNodes("acoes/acao");
            foreach (XmlElement acao in acoes)
            {
                acao.SetAttribute("decodedCondition", "");
                acao.SetAttribute("condition", "");
                acao.SetAttribute("conditionIndex", "");
            }
        }

        setVersaoFormatoXmlToXmlTela(xmlDoc, "001.1.029");
        __strXmlWorkflow = getXmlAsStringFromXmlDoc(xmlDoc);
        atualizaXmlHiddenField(__strXmlWorkflow);
        return getVersaoFormatoXmlFromXmlTela();
    }

    private string atualizaVersao11029()
    {

        // fazer aqui procedimentos para atualizar da versão 28 para a próxima, quando for criada a versão 29.
        return getVersaoFormatoXmlFromXmlTela();
    }

    private void AtualizaIdentificacaoWorkflow(string nomeFluxo, string versaoWorkflow, string descricaoVersao,
                                               string observacaoVersao, string larguraWf, string alturaWf)
    {
        lblTituloTela.Text = Resources.traducao.adm_edicaoWorkflows_edi__o_de_modelo_de_fluxo_ + " \"" + nomeFluxo + "\" - " + Resources.traducao.adm_edicaoWorkflows_vers_o + " " + versaoWorkflow + " - ";
        hfValoresTela.Set("nomeFluxo", nomeFluxo);
        hfValoresTela.Set("versaoWorkflow", VersaoWorkflow);
        hfValoresTela.Set("descricaoVersao", descricaoVersao);
        hfValoresTela.Set("observacaoVersao", observacaoVersao);
        hfValoresTela.Set("LarguraDivFlash", larguraWf);
        hfValoresTela.Set("AlturaDivFlash", alturaWf);
    }

    #endregion

    #region "----[ Gravação das Informações"

    private void GravaWorkflow()
    {
        //Setear el XML con los valores Width y Height setados.
        string heightXml = hfValoresTela.Get("AlturaDivFlash").ToString();
        string widthXml = hfValoresTela.Get("LarguraDivFlash").ToString();
        setWidhtHeightXmlToXmlTela(widthXml, heightXml);

        if ("0" == hfCodigoWorkflow.Value.ToString())
        {
            int codigoWorkflow = cDados.gravaWorkflow(_key, CodigoFluxo, VersaoWorkflow, IdUsuarioLogado,
                                                      txtDescricaoVersaoWf.Text, memObservacaoWf.Text,
                                                      hfValoresTela.Get("XMLWF").ToString());
            hfCodigoWorkflow.Value = codigoWorkflow.ToString();
        }
        else
            cDados.atualizaWorkflow(_key, int.Parse(hfCodigoWorkflow.Value), hfValoresTela.Get("XMLWF").ToString(),
                                    txtDescricaoVersaoWf.Text, memObservacaoWf.Text);

    }

    #endregion

    protected void gvEdicaoElementos_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        string acaoWf = "";
        if (hfValoresTela.Contains("AcaoWf"))
            acaoWf = hfValoresTela.Get("AcaoWf").ToString();

        if (e.ButtonID == "btnExcluir")
        {
            if ("S" == acaoWf)
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
            else
                e.Enabled = true;
        }
    }

    #region ETAPA

    protected void painelCallbackEtapas_Callback(object sender, CallbackEventArgsBase e)
    {
        string codigo = string.Empty;
        string timeoutValue = string.Empty;
        string timeoutUnit = string.Empty;
        string timeoutOffset = string.Empty;

        // XmlNodeList list = obtemListaDataSetFromXmlTela();
        XmlNodeList prazoPrevistoList;
        XmlNodeList listaTipoElemento;

        int _indexRow;

        if (e.Parameter.Contains(";"))
            codigo = e.Parameter.Split(';')[1];
        else
        {
            if (int.TryParse(e.Parameter, out _indexRow))
            {
                codigo = gvEdicaoElementos.GetRowValues(_indexRow, "idEtapa").ToString();

            }
        }
        // e.Parameter conterá o índice da linha clicada na gvEdicaoElementos
        if (codigo != "")
        {
            //            codigo = gvEdicaoElementos.GetRowValues(_indexRow, "idEtapa").ToString();

            listaTipoElemento = obtemListaElementosXmlTelaPorId(codigo);

            foreach (XmlElement etapa in listaTipoElemento)
            {
                string idEtapa = etapa.Attributes["name"].Value.Substring(0, (etapa.Attributes["name"].Value.IndexOf(". ")));
                string nomeElemento = etapa.Attributes["name"].Value.Substring((etapa.Attributes["name"].Value.IndexOf(". ") + 2));
                string descricao = etapa.Attributes["toolText"].Value;
                string descricaoDetalhada = obtemDescricaoEtapaFromXmlTela(codigo);
                bool ocultaBotoesAcao = etapa.Attributes["ocultaBotoesAcao"].Value == "1" ? true : false;

                prazoPrevistoList = obtemPrazoPrevistoEtapaFromXmlTela(codigo);
                foreach (XmlElement prazoPrevisto in prazoPrevistoList)
                {
                    timeoutValue = prazoPrevisto.GetAttribute("timeoutValue");
                    timeoutUnit = prazoPrevisto.GetAttribute("timeoutUnit");
                    timeoutOffset = prazoPrevisto.GetAttribute("timeoutOffset");
                }

                carregaEtapa(nomeElemento, descricao, descricaoDetalhada, idEtapa, timeoutValue, timeoutUnit, timeoutOffset, ocultaBotoesAcao);
            }

            AtribuiDatasetGridFormularios_etp();
            AtribuiDatasetGridGrupos_etp();
            carregaGridFormularios(codigo);
            carregaGridAcessos(codigo);
        }
    }

    private void carregaEtapa(string nome, string descricaoResumida, string descricaoDetalhada, string codigo, string timeoutValue, string timeoutUnit, string timeOutOffset, bool ocultaBotoesAcao)
    {
        edtIdEtapa_etp.Text = codigo;
        edtNomeAbreviado_etp.Text = nome;
        edtDescricaoResumida_etp.Text = descricaoResumida;
        mmDescricao_etp.Text = descricaoDetalhada;
        txtQtdTempo.Text = timeoutValue;
        ddlUnidadeTempo.Value = timeoutUnit;
        ddlReferenciaTempo.Value = timeOutOffset;
        cbOcultaBotoes.Checked = ocultaBotoesAcao;
    }

    #region gvFORMULARIO - ETAPAS

    private void carregaGridFormularios(string codigo)
    {
        DataTable dtSessao = (DataTable)Session["dtForms"];
        DataRow linha;
        string originalStageId;
        int iOriginalId;

        int ordemForm = 1;

        foreach (XmlElement elementos in obtemFormulariosEtapaFromXmlTela(codigo))
        {
            linha = dtSessao.NewRow();

            linha[0] = elementos.GetAttribute("id");
            linha[1] = elementos.GetAttribute("name");
            linha[2] = elementos.GetAttribute("title");
            linha[3] = (elementos.GetAttribute("readOnly") == "1" ? true : false);
            linha[4] = (elementos.GetAttribute("required") == "1" ? true : false);
            linha[5] = (elementos.GetAttribute("newOnEachOcurrence") == "1" ? true : false);
            if (utilizaAssinaturaDigitalFormularios == "S")  // utiliza Certificação digital
                linha[6] = (elementos.GetAttribute("requerAssinaturaDigital") == "1" ? true : false);     // CERTDIG
            else
                linha[6] = false;     // CERTDIG 
            originalStageId = elementos.Attributes["originalStageId"].Value;

            linha[7] = 0;
            linha[8] = (elementos.GetAttribute("ordemFormularioEtapa") == "" ? (ordemForm++).ToString() : elementos.GetAttribute("ordemFormularioEtapa"));                              // OrdemForm
            linha[9] = "";
            linha[10] = elementos.GetHashCode();

            if (true == int.TryParse(originalStageId, out iOriginalId))
            {
                linha[9] = obtemNomeEtapaFromXmlTela(originalStageId);

                if (linha[9].ToString() != "")
                    linha[7] = iOriginalId;
            }
            dtSessao.Rows.Add(linha);
        }

        gvFormularios_etp.DataSource = dtSessao;
        gvFormularios_etp.DataBind();
    }

    private void AtribuiDatasetGridFormularios_etp()
    {
        DataTable dtResult = new DataTable();
        DataColumn NewColumn = null;

        NewColumn = new DataColumn("CodigoModeloFormulario", Type.GetType("System.Int32"));
        NewColumn.Caption = "Formulário";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("NomeFormulario", Type.GetType("System.String"));
        NewColumn.Caption = "Formulário";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("TituloFormulario", Type.GetType("System.String"));
        NewColumn.Caption = "Título";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("TipoAcessoFormulario", Type.GetType("System.Boolean"));
        NewColumn.Caption = "Somente Leitura";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("PreenchimentoObrigatorio", Type.GetType("System.Boolean"));
        NewColumn.Caption = "Obrigatório?";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("NovoCadaOcorrencia", Type.GetType("System.Boolean"));
        NewColumn.Caption = "Novo Cada Ocorrência";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("RequerAssinaturaDigital", Type.GetType("System.Boolean"));           // CERTDIG
        NewColumn.Caption = "Requer Assinatura Digital";
        NewColumn.ReadOnly = (utilizaAssinaturaDigitalFormularios == "N") ? true : false;
        NewColumn.DefaultValue = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("CodigoEtapaWfOrigem", Type.GetType("System.Int32"));
        NewColumn.Caption = "Etapa Origem";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("OrdemFormularioEtapa", Type.GetType("System.Int32"));               // OrdemForm
        NewColumn.Caption = "Ordem Formulário";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);


        NewColumn = new DataColumn("idEtapa", Type.GetType("System.String"));
        NewColumn.Caption = "Etapa Origem";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("HashCode", Type.GetType("System.Int32"));
        NewColumn.Caption = "Hash";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        Session["dtForms"] = dtResult;

        gvFormularios_etp.DataSource = dtResult;
        gvFormularios_etp.DataBind();

        if (utilizaAssinaturaDigitalFormularios == "N")
        {
            foreach (GridViewDataColumn c in gvFormularios_etp.DataColumns)
            {
                if (c.FieldName.ToString() == "RequerAssinaturaDigital")
                {
                    c.Visible = false;
                }
            }
        }

    }

    protected void gvFormularios_etp_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CodigoModeloFormulario")
        {
            string where = string.Format(@" AND [CodigoEntidade] = {0} ", CodigoEntidade);

            ASPxComboBox combo = (ASPxComboBox)e.Editor;

            DataTable dtForms = cDados.getFormulariosWorkflow(_key, where).Tables[0];
            combo.DataSource = dtForms;
            combo.ValueField = "codigoModeloFormulario";
            combo.TextField = "nomeFormulario";
            combo.DataBind();
        }

        else if (e.Column.FieldName == "CodigoEtapaWfOrigem todo:") // todo: ver se há como iniciar para a coluna etapa de origem do form.
        {
            string where = "";

            ASPxComboBox combo = (ASPxComboBox)e.Editor;

            DataTable dtForms = cDados.getFormulariosWorkflow(_key, where).Tables[0];
            combo.DataSource = dtForms;
            combo.ValueField = "codigoModeloFormulario";
            combo.TextField = "nomeFormulario";
            combo.DataBind();
        }
        else if (e.Column.FieldName == "OrdemFormularioEtapa")
        {
            int qtdLinhas = gvFormularios_etp.VisibleRowCount;
            ASPxTextBox ordem = (ASPxTextBox)e.Editor;
            if (ordem.Value == null)
                ordem.Value = qtdLinhas + 1;
        }
        else if (e.Column.FieldName == "RequerAssinaturaDigital")
        {
            if (utilizaAssinaturaDigitalFormularios == "N")
                e.Column.FieldName.Remove(6);
        }


        if (e.Column.FieldName == "CodigoModeloFormulario" && e.KeyValue != null)
        {
            ASPxComboBox combo = (ASPxComboBox)e.Editor;
            combo.ClientEnabled = false;
        }
    }

    protected void gvFormularios_etp_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dt = (DataTable)Session["dtForms"];
        DataRow dr = dt.NewRow();

        int ordermForm = 1;
        if (e.NewValues["CodigoModeloFormulario"] != null)
        {
            dr["CodigoModeloFormulario"] = e.NewValues["CodigoModeloFormulario"];

            DataSet ds = cDados.getFormulariosWorkflow(_key, "AND CodigoModeloFormulario = " + e.NewValues["CodigoModeloFormulario"]);
            if ((true == cDados.DataSetOk(ds)) && (true == cDados.DataTableOk(ds.Tables[0])))
            {
                dr["NomeFormulario"] = ds.Tables[0].Rows[0]["nomeFormulario"] + "";
            }
        }

        dr["TituloFormulario"] = (e.NewValues["TituloFormulario"] != null) ? e.NewValues["TituloFormulario"] : "";

        dr["TipoAcessoFormulario"] = (e.NewValues["TipoAcessoFormulario"] != null) ? e.NewValues["TipoAcessoFormulario"] : false;
        dr["PreenchimentoObrigatorio"] = (e.NewValues["PreenchimentoObrigatorio"] != null) ? e.NewValues["PreenchimentoObrigatorio"] : false;
        dr["NovoCadaOcorrencia"] = (e.NewValues["NovoCadaOcorrencia"] != null) ? e.NewValues["NovoCadaOcorrencia"] : false;
        if (utilizaAssinaturaDigitalFormularios == "S")  // utiliza Certificação digital
        {
            dr["RequerAssinaturaDigital"] = (e.NewValues["RequerAssinaturaDigital"] != null) ? e.NewValues["RequerAssinaturaDigital"] : false;          // CERTDIG
        }
        //else
        //{
        //    dr["RequerAssinaturaDigital"] = false;
        //}

        dr["CodigoEtapaWfOrigem"] = "0";
        dr["idEtapa"] = "";
        if (e.NewValues["CodigoEtapaWfOrigem"] != null)
        {
            string etapaOrigem = e.NewValues["CodigoEtapaWfOrigem"].ToString();
            if (("" != etapaOrigem) && ("0" != etapaOrigem))
            {
                dr["CodigoEtapaWfOrigem"] = etapaOrigem;
                dr["idEtapa"] = obtemNomeEtapaFromXmlTela(etapaOrigem);
            }
        }
        dr["OrdemFormularioEtapa"] = (e.NewValues["OrdemFormularioEtapa"] != null && e.NewValues["OrdemFormularioEtapa"].ToString() != "") ? e.NewValues["OrdemFormularioEtapa"] : (ordermForm++).ToString();                 // OrdemForm
        dr["HashCode"] = dr.GetHashCode();

        dt.Rows.Add(dr);

        gvFormularios_etp.DataSource = dt;

        gvFormularios_etp.DataBind();
        e.Cancel = true;
        gvFormularios_etp.CancelEdit();
    }

    protected void gvFormularios_etp_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dtForms = (DataTable)Session["dtForms"];

        try
        {

            int ordermForm = 1;
            foreach (DataRow dr in dtForms.Rows)
            {
                if ((null != e.Keys["HashCode"]) &&
                    (dr["HashCode"].ToString() == e.Keys["HashCode"].ToString()))
                {
                    DataSet ds = cDados.getFormulariosWorkflow(_key, "AND CodigoModeloFormulario = " + e.NewValues["CodigoModeloFormulario"]);
                    if ((true == cDados.DataSetOk(ds)) && (true == cDados.DataTableOk(ds.Tables[0])))
                    {
                        dr["NomeFormulario"] = ds.Tables[0].Rows[0]["nomeFormulario"] + "";
                    }
                    dr["TituloFormulario"] = (e.NewValues["TituloFormulario"] != null) ? e.NewValues["TituloFormulario"] : "";
                    dr["TipoAcessoFormulario"] = (e.NewValues["TipoAcessoFormulario"] != null) ? e.NewValues["TipoAcessoFormulario"] : false;
                    dr["PreenchimentoObrigatorio"] = (e.NewValues["PreenchimentoObrigatorio"] != null) ? e.NewValues["PreenchimentoObrigatorio"] : false;
                    dr["NovoCadaOcorrencia"] = (e.NewValues["NovoCadaOcorrencia"] != null) ? e.NewValues["NovoCadaOcorrencia"] : false;
                    if (utilizaAssinaturaDigitalFormularios == "S")  // utiliza Certificação digital
                    {
                        dr["RequerAssinaturaDigital"] = (e.NewValues["RequerAssinaturaDigital"] != null) ? e.NewValues["RequerAssinaturaDigital"] : false;            // CERTDIG
                    }
                    //else
                    //{
                    //    dr["RequerAssinaturaDigital"] = false;
                    //}

                    dr["CodigoEtapaWfOrigem"] = "0";
                    dr["idEtapa"] = "";
                    dr["OrdemFormularioEtapa"] = (e.NewValues["OrdemFormularioEtapa"] != null && e.NewValues["OrdemFormularioEtapa"].ToString() != "") ? e.NewValues["OrdemFormularioEtapa"] : (ordermForm++).ToString();                      // OrdemForm

                    if (e.NewValues["CodigoEtapaWfOrigem"] != null)
                    {
                        string etapaOrigem = e.NewValues["CodigoEtapaWfOrigem"].ToString();
                        if (("" != etapaOrigem) && ("0" != etapaOrigem))
                        {
                            dr["CodigoEtapaWfOrigem"] = etapaOrigem;
                            dr["idEtapa"] = obtemNomeEtapaFromXmlTela(etapaOrigem);
                        }
                    }

                    dtForms.AcceptChanges();
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            string xxx = ex.Message;
            throw new Exception(xxx);
        }

        Session["dtForms"] = dtForms;

        gvFormularios_etp.DataSource = dtForms;

        gvFormularios_etp.DataBind();

        e.Cancel = true;
        gvFormularios_etp.CancelEdit();
    }

    protected void gvFormularios_etp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        DataTable dtForms = (DataTable)Session["dtForms"];

        foreach (DataRow dr in dtForms.Rows)
        {
            if (dr["HashCode"] + "" == e.Values["HashCode"] + "")
            {
                dr.Delete();
                dtForms.AcceptChanges();
                break;
            }
        }

        Session["dtForms"] = dtForms;
        gvFormularios_etp.DataSource = dtForms;
        gvFormularios_etp.DataBind();
        e.Cancel = true;
        gvFormularios_etp.CancelEdit();
    }

    protected void gvFormularios_etp_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters.ToString() == "Cancelar")
        {
            AtribuiDatasetGridFormularios_etp();
            AtribuiDatasetGridGrupos_etp();
        }
    }

    protected void gvFormularios_etp_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        if (e.CallbackName == "CUSTOMCALLBACK")
        {
            limpaGrids(gvFormularios_etp);
            AtribuiDatasetGridFormularios_etp();
        }
    }

    #endregion

    #region gvPESSOASACESSO - ETAPAS"

    private void AtribuiDatasetGridGrupos_etp()
    {
        DataTable dtResult = new DataTable();
        DataColumn NewColumn = null;

        NewColumn = new DataColumn("CodigoPerfilWf", Type.GetType("System.Int32"));
        NewColumn.Caption = "CodigoPerfil";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("NomePerfilWf", Type.GetType("System.String"));
        NewColumn.Caption = "Perfil";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("TipoAcesso", Type.GetType("System.String"));
        NewColumn.Caption = "Acesso";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        Session["dtGruposEtapas"] = dtResult;

        gv_PessoasAcessos_etp.DataSource = dtResult;
        gv_PessoasAcessos_etp.DataBind();
    }

    private void carregaGridAcessos(string codigo)
    {
        DataTable dtSessao = (DataTable)Session["dtGruposEtapas"];
        DataRow linha;

        foreach (XmlElement elementos in obtemGruposDeAcessosEtapaFromXmlTela(codigo))
        {
            linha = dtSessao.NewRow();

            linha[0] = elementos.Attributes["id"].Value;
            linha[1] = elementos.Attributes["name"].Value;
            linha[2] = elementos.Attributes["accessType"].Value;

            dtSessao.Rows.Add(linha);
        }

        gv_PessoasAcessos_etp.DataSource = dtSessao;
        gv_PessoasAcessos_etp.DataBind();
    }

    protected void gv_PessoasAcessos_etp_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        gruposEnLaLista = "";
        if (e.Column.FieldName == "CodigoPerfilWf")
        {
            //DataTable dtSessao = (DataTable)Session["dtGruposEtapas"];
            string where = "";

            for (int i = 0; i < gv_PessoasAcessos_etp.VisibleRowCount; i++)
            { //Listar los CodigoPerfilWf que se encuentran en la Grid. De esta forma se evitará su 
                //listado en el combo salvando de una selección repetitiva.
                if (e.KeyValue != gv_PessoasAcessos_etp.GetRowValues(i, "CodigoPerfilWf"))
                {
                    if (gv_PessoasAcessos_etp.GetRowValues(i, "CodigoPerfilWf") != null && gv_PessoasAcessos_etp.GetRowValues(i, "CodigoPerfilWf") != "")
                        gruposEnLaLista += "," + gv_PessoasAcessos_etp.GetRowValues(i, "CodigoPerfilWf").ToString().Trim();
                }

            }
            if (gruposEnLaLista != "")
            {
                if (gruposEnLaLista[gruposEnLaLista.Length - 1] == ',')
                {
                    gruposEnLaLista = gruposEnLaLista.Substring(0, gruposEnLaLista.Length - 1);
                }
                where += " AND CodigoPerfilWf NOT IN(" + gruposEnLaLista.Substring(1) + ")";
            }

            where += string.Format(@" AND CodigoEntidade = {0} ", CodigoEntidade);

            ASPxComboBox combo = (ASPxComboBox)e.Editor;
            DataTable dtForms = cDados.getTiposGruposPessoasWf(_key, where).Tables[0];

            combo.DataSource = dtForms;
            combo.ValueField = "CodigoPerfilWf";
            combo.TextField = "NomePerfilWf";
            combo.DataBind();
        }

        if (e.Column.FieldName == "CodigoPerfilWf" && e.KeyValue != null)
        {
            ASPxComboBox combo = (ASPxComboBox)e.Editor;
            combo.ClientEnabled = false;
        }
    }

    protected void gv_PessoasAcessos_etp_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dtForms = (DataTable)Session["dtGruposEtapas"];

        DataRow dr = dtForms.NewRow();

        if (e.NewValues["CodigoPerfilWf"] != null)
        {
            dr["CodigoPerfilWf"] = int.Parse(e.NewValues["CodigoPerfilWf"].ToString());

            DataSet ds2 = cDados.getTiposGruposPessoasWf(_key, "AND CodigoPerfilWf = " + e.NewValues["CodigoPerfilWf"]);
            if ((true == cDados.DataSetOk(ds2)) && (true == cDados.DataTableOk(ds2.Tables[0])))
                dr["NomePerfilWf"] = ds2.Tables[0].Rows[0]["NomePerfilWf"] + "";
        }

        if (e.NewValues["TipoAcesso"] != null)
            dr["TipoAcesso"] = (e.NewValues["TipoAcesso"] + "" == "C") ? Resources.traducao.adm_edicaoWorkflows_consulta : Resources.traducao.adm_edicaoWorkflows_a__o;
        else
            dr["TipoAcesso"] = Resources.traducao.adm_edicaoWorkflows_consulta;

        dtForms.Rows.Add(dr);

        gv_PessoasAcessos_etp.DataSource = dtForms;
        gv_PessoasAcessos_etp.DataBind();

        e.Cancel = true;
        gv_PessoasAcessos_etp.CancelEdit();
    }

    protected void gv_PessoasAcessos_etp_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dtForms = (DataTable)Session["dtGruposEtapas"];

        foreach (DataRow dr in dtForms.Rows)
        {
            if ((null != e.NewValues["CodigoPerfilWf"]) &&
                (dr["CodigoPerfilWf"].ToString() == e.Keys["CodigoPerfilWf"].ToString()))
            {
                dr["CodigoPerfilWf"] = e.NewValues["CodigoPerfilWf"];
                DataSet ds2 = cDados.getTiposGruposPessoasWf(_key, "AND CodigoPerfilWf = " + e.NewValues["CodigoPerfilWf"]);
                if ((true == cDados.DataSetOk(ds2)) && (true == cDados.DataTableOk(ds2.Tables[0])))
                    dr["NomePerfilWf"] = ds2.Tables[0].Rows[0]["NomePerfilWf"] + "";

                if (null == e.NewValues["TipoAcesso"])
                    dr["TipoAcesso"] = Resources.traducao.adm_edicaoWorkflows_consulta;
                else
                    dr["TipoAcesso"] = (e.NewValues["TipoAcesso"] + "" == "C") ? Resources.traducao.adm_edicaoWorkflows_consulta : Resources.traducao.adm_edicaoWorkflows_a__o;

                dtForms.AcceptChanges();
                break;
            }
        }

        Session["dtGruposEtapas"] = dtForms;

        gv_PessoasAcessos_etp.DataSource = dtForms;
        gv_PessoasAcessos_etp.DataBind();

        e.Cancel = true;
        gv_PessoasAcessos_etp.CancelEdit();
    }

    protected void gv_PessoasAcessos_etp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        DataTable dtForms = (DataTable)Session["dtGruposEtapas"];

        foreach (DataRow dr in dtForms.Rows)
        {
            if (dr["CodigoPerfilWf"].ToString() == e.Keys["CodigoPerfilWf"].ToString())
            {
                dr.Delete();
                dtForms.AcceptChanges();
                break;
            }
        }

        Session["dtGruposEtapas"] = dtForms;

        gv_PessoasAcessos_etp.DataSource = dtForms;

        gv_PessoasAcessos_etp.DataBind();

        e.Cancel = true;
        gv_PessoasAcessos_etp.CancelEdit();
    }

    protected void gv_PessoasAcessos_etp_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        if (e.CallbackName == "CUSTOMCALLBACK")
        {
            limpaGrids(gv_PessoasAcessos_etp);
            AtribuiDatasetGridGrupos_etp();
        }
    }

    #endregion

    #endregion

    #region Subprocesso
    protected void painelCallbackSubprocesso_Callback(object sender, CallbackEventArgsBase e)
    {
        string codigo = string.Empty;

        // XmlNodeList list = obtemListaDataSetFromXmlTela();
        XmlNodeList listaTipoElemento;

        int _indexRow;

        if (e.Parameter.Contains(";"))
            codigo = e.Parameter.Split(';')[1];
        else
        {
            if (int.TryParse(e.Parameter, out _indexRow))
            {
                codigo = gvEdicaoElementos.GetRowValues(_indexRow, "idEtapa").ToString();

            }
        }
        // e.Parameter conterá o índice da linha clicada na gvEdicaoElementos
        if (codigo != "")
        {
            //            codigo = gvEdicaoElementos.GetRowValues(_indexRow, "idEtapa").ToString();

            listaTipoElemento = obtemListaElementosXmlTelaPorId(codigo);

            foreach (XmlElement etapa in listaTipoElemento)
            {
                string idEtapa = etapa.Attributes["name"].Value.Substring(0, (etapa.Attributes["name"].Value.IndexOf(". ")));
                string nomeElemento = etapa.Attributes["name"].Value.Substring((etapa.Attributes["name"].Value.IndexOf(". ") + 2));
                string descricao = etapa.Attributes["toolText"].Value;
                string descricaoDetalhada = obtemDescricaoEtapaFromXmlTela(codigo);
                int codigoSubfluxo = int.Parse(etapa.Attributes["codigoSubfluxo"].Value);

                carregaCamposSubprocesso(nomeElemento, descricao, descricaoDetalhada, idEtapa, codigoSubfluxo);
            }
        }
    }

    private void carregaCamposSubprocesso(string nome, string descricaoResumida, string descricaoDetalhada, string codigoEtapa, int codigoSubfluxo)
    {
        edtIdEtapa_sub.Text = codigoEtapa;
        edtNomeAbreviado_sub.Text = nome;
        edtDescricaoResumida_sub.Text = descricaoResumida;
        mmDescricao_sub.Text = descricaoDetalhada;
        if (codigoSubfluxo > 0)
        {
            ListEditItem a = cmbFluxos_sub.Items.FindByValue(codigoSubfluxo);
            cmbFluxos_sub.SelectedItem = a;
        }
    }
    #endregion

    #region CONECTORES

    protected void painelCallbackConectores_Callback(object sender, CallbackEventArgsBase e)
    {
        XmlNode acaoEtapa;
        painelCallbackConectores.JSProperties["cp_EtapaInicio"] = "";
        string codigoFrom = "";
        string descricao = "";
        string nomeOrigem = "";
        string nomeDestino = "";
        string texto1 = "";
        string assunto1 = "";
        string texto2 = "";
        string assunto2 = "";
        string solicitaAssinaturaDigital = "0";
        string corBotao = "";
        string iconeBotao = "";

        int _indexRow;

        // e.Parameter conterá o índice da linha clicada na gvEdicaoElementos
        if (int.TryParse(e.Parameter, out _indexRow))
        {
            codigoFrom = gvEdicaoElementos.GetRowValues(_indexRow, "idorigemUnbound").ToString();
            nomeOrigem = gvEdicaoElementos.GetRowValues(_indexRow, "origem").ToString();
            nomeDestino = gvEdicaoElementos.GetRowValues(_indexRow, "destino").ToString();
            descricao = gvEdicaoElementos.GetRowValues(_indexRow, "descricao").ToString();

            //if ("Inicio del Fluxo" == descricao || "Cancelamento del Fluxo" == descricao)
            //    descricao = "";

            acaoEtapa = obtemAcaoEtapaFromXmlTela(codigoFrom, descricao);

            AtribuiDatasetGridGrupos_cnt();
            AtribuiDatasetGridAcoes();
            AtribuiDatasetGridAcionamentosAPI();
            AtribuiDatasetGridParametrosAcionamentosAPI();

            if (null != acaoEtapa)
            {
                texto1 = obtemInfoNodeFromAcao(acaoEtapa, "textoNotificacao");
                assunto1 = obtemInfoNodeFromAcao(acaoEtapa, "assuntoNotificacao");
                texto2 = obtemInfoNodeFromAcao(acaoEtapa, "textoNotificacao2");
                assunto2 = obtemInfoNodeFromAcao(acaoEtapa, "assuntoNotificacao2");
                solicitaAssinaturaDigital = acaoEtapa.Attributes["solicitaAssinaturaDigital"].Value;
                corBotao = acaoEtapa.Attributes["corBotao"].Value;
                iconeBotao = acaoEtapa.Attributes["iconeBotao"].Value;

                carregaGridGruposNotificados(acaoEtapa);
                carregaGridAcoesAutomaticas_cnt(acaoEtapa);
                carregaGridAcionamentos_api(acaoEtapa);
                carregaGridParametrosAcionamentos_api(acaoEtapa);
            }
            carregaCamposConectores(nomeOrigem, descricao, nomeDestino, texto1, assunto1, texto2, assunto2, solicitaAssinaturaDigital, corBotao, iconeBotao);

            if (codigoFrom == "0")
                painelCallbackConectores.JSProperties["cp_EtapaInicio"] = "S";
        }
    }

    private void carregaGridAcionamentos_api(XmlNode acao)
    {

        DataSet dtSessao = ((DataSet)Session["dsAcionamentosAPI"]).Copy();
        DataTable dtSessao1 = dtSessao.Tables[0];
        DataRow linha;

        foreach (XmlElement elementos in acao.SelectNodes("acionamentosAPIs/acionamentoAPI"))
        {
            linha = dtSessao1.NewRow();

            linha["Id"] = elementos.Attributes["Id"].Value;
            linha["webServiceURL"] = elementos.Attributes["webServiceURL"].Value;
            linha["enabled"] = elementos.Attributes["enabled"].Value;
            linha["activationSequence"] = elementos.Attributes["activationSequence"].Value;


            dtSessao1.Rows.Add(linha);
        }
        Session["dsAcionamentosAPI"] = dtSessao;
        gv_Acionamentos.DataSource = dtSessao1;
        gv_Acionamentos.DataBind();

        // seleciona a primeira linha da grid de acionamento para 'facilitar' para o usuário,
        // // já que a grid de parâmetros irá exigir que se selecione, caso o usuário vá direto no botão "+" da grid de parâmetros
        if (gv_Acionamentos.VisibleRowCount > 0)
            gv_Acionamentos.FocusedRowIndex = 0;
    }
    
    private void carregaGridParametrosAcionamentos_api(XmlNode acao)
    {
        DataSet dtSessao = ((DataSet)Session["dsParametrosAcionamentosAPI"]).Copy();
        DataTable dtSessao1 = dtSessao.Tables[0];
        DataRow linha;

        foreach (XmlElement elementos in acao.SelectNodes("acionamentosAPIs/acionamentoAPI/parametrosAPI/parametroAPI"))
        {
            linha = dtSessao1.NewRow();

            Guid novaKey = Guid.NewGuid();
            linha["Id"] = "-1";
            linha["parameter"] = elementos.Attributes["parameter"].Value;
            linha["dataType"] = elementos.Attributes["dataType"].Value;
            linha["httpPart"] = elementos.Attributes["httpPart"].Value;
            linha["sendNull"] = elementos.Attributes["sendNull"].Value;
            linha["value"] = elementos.Attributes["value"].Value == "null" ? "" : elementos.Attributes["value"].Value;
            linha["IdKey"] = novaKey.ToString();
            dtSessao1.Rows.Add(linha);
        }
        Session["dsParametrosAcionamentosAPI"] = dtSessao;
        gv_ParametrosAcionamentos.DataSource = dtSessao1;
        gv_ParametrosAcionamentos.DataBind();
    }

    private void carregaCamposConectores(string nomeOrigem, string opcao, string nomeDestino, string texto1, string assunto1, string texto2, string assunto2, string solicitaAssinaturaDigital, string corBotao, string iconeBotao)
    {
        cmbEtapaOrigem_cnt.Text = nomeOrigem;
        if (null == cmbEtapaOrigem_cnt.SelectedItem)
            cmbEtapaOrigem_cnt.JSProperties["cpOriginalFrom"] = "";
        else
            cmbEtapaOrigem_cnt.JSProperties["cpOriginalFrom"] = cmbEtapaOrigem_cnt.SelectedItem.Value.ToString();

        cmbEtapaDestino_cnt.Text = nomeDestino;
        if (null == cmbEtapaDestino_cnt.SelectedItem)
            cmbEtapaDestino_cnt.JSProperties["cpOriginalTo"] = "";
        else
            cmbEtapaDestino_cnt.JSProperties["cpOriginalTo"] = cmbEtapaDestino_cnt.SelectedItem.Value.ToString();

        edtAcao_cnt.Text = opcao;
        edtAcao_cnt.JSProperties["cpOriginalAction"] = opcao;

        mmTexto1_cnt.Text = texto1;
        txtAssunto1_cnt.Text = assunto1;
        mmTexto2_cnt.Text = texto2;
        txtAssunto2_cnt.Text = assunto2;

        ceCorBotao_cnt.Text = corBotao;
        cmbIconeBotao_cnt.Text = iconeBotao;

        cbSolicitarAssinaturaDigital.Checked = solicitaAssinaturaDigital.Equals("1");
    }

    #region gvGRUPOSNOTIFICADOS CONECTORES

    private void AtribuiDatasetGridGrupos_cnt()
    {
        DataTable dtResult = new DataTable();
        DataColumn NewColumn = null;

        NewColumn = new DataColumn("CodigoPerfilWf", Type.GetType("System.Int32"));
        NewColumn.Caption = "CodigoPerfil";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("NomePerfilWf", Type.GetType("System.String"));
        NewColumn.Caption = "NomePerfil";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("Mensagem", Type.GetType("System.String"));
        NewColumn.Caption = "Tipo Mensagem";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        Session["dtGruposConectores"] = dtResult;

        gv_GruposNotificados_cnt.DataSource = dtResult;
        gv_GruposNotificados_cnt.DataBind();
    }

    private void carregaGridGruposNotificados(XmlNode acao)
    {
        DataTable dtSessao = (DataTable)Session["dtGruposConectores"];
        DataRow linha;

        foreach (XmlElement elementos in acao.SelectNodes("gruposNotificados/grupo"))
        {
            linha = dtSessao.NewRow();

            linha[0] = elementos.Attributes["id"].Value;
            linha[1] = elementos.Attributes["name"].Value;
            linha[2] = elementos.Attributes["msgBox"].Value;

            dtSessao.Rows.Add(linha);
        }

        gv_GruposNotificados_cnt.DataSource = dtSessao;
        gv_GruposNotificados_cnt.DataBind();
    }

    protected void gv_GruposNotificados_cnt_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        gruposEnLaLista = "";
        if (e.Column.FieldName == "CodigoPerfilWf")
        {
            DataTable dtSessao = (DataTable)Session["dtGruposConectores"];
            string where = "";

            for (int i = 0; i < gv_GruposNotificados_cnt.VisibleRowCount; i++)
            { //Listar los CodigoPerfilWf que se encuentran en la Grid. De esta forma se evitará su 
              //listado en el combo salvando de una selección repetitiva.
                if (e.KeyValue != gv_GruposNotificados_cnt.GetRowValues(i, "CodigoPerfilWf"))
                    gruposEnLaLista += "," + gv_GruposNotificados_cnt.GetRowValues(i, "CodigoPerfilWf").ToString();
            }
            if (gruposEnLaLista != "")
            {
                where += " AND CodigoPerfilWf NOT IN(" + gruposEnLaLista.Substring(1) + ")";
            }

            where += string.Format(@" AND CodigoEntidade = {0} ", CodigoEntidade);
            ASPxComboBox combo = (ASPxComboBox)e.Editor;
            DataTable dtForms = cDados.getTiposGruposPessoasWf(_key, where).Tables[0];

            combo.DataSource = dtForms;
            combo.ValueField = "CodigoPerfilWf";
            combo.TextField = "NomePerfilWf";
            combo.DataBind();
        }

        if (e.Column.FieldName == "CodigoPerfilWf" && e.KeyValue != null)
        {
            ASPxComboBox combo = (ASPxComboBox)e.Editor;
            combo.ClientEnabled = false;
        }

    }

    protected void gv_GruposNotificados_cnt_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dtForms = (DataTable)Session["dtGruposConectores"];

        DataRow dr = dtForms.NewRow();

        if (e.NewValues["CodigoPerfilWf"] != null)
        {
            dr["CodigoPerfilWf"] = e.NewValues["CodigoPerfilWf"];

            DataSet ds2 = cDados.getTiposGruposPessoasWf(_key, "AND CodigoPerfilWf = " + e.NewValues["CodigoPerfilWf"]);
            if ((true == cDados.DataSetOk(ds2)) && (true == cDados.DataTableOk(ds2.Tables[0])))
                dr["NomePerfilWf"] = ds2.Tables[0].Rows[0]["NomePerfilWf"] + "";
        }

        dr["Mensagem"] = (e.NewValues[1] != null) ? e.NewValues[1] : "E";

        if (e.NewValues[1] != null)
            dr["Mensagem"] = (e.NewValues[1] + "" == "S") ? Resources.traducao.adm_edicaoWorkflows_acompanhamento : Resources.traducao.adm_edicaoWorkflows_a__o;
        else
            dr["Mensagem"] = Resources.traducao.adm_edicaoWorkflows_a__o;

        dtForms.Rows.Add(dr);

        gv_GruposNotificados_cnt.DataSource = dtForms;
        gv_GruposNotificados_cnt.DataBind();

        e.Cancel = true;
        gv_GruposNotificados_cnt.CancelEdit();
    }

    protected void gv_GruposNotificados_cnt_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dtForms = (DataTable)Session["dtGruposConectores"];

        foreach (DataRow dr in dtForms.Rows)
        {
            if (dr["CodigoPerfilWf"] + "" == e.Keys["CodigoPerfilWf"] + "")
            {
                if (null != e.NewValues["CodigoPerfilWf"])
                {
                    dr["CodigoPerfilWf"] = e.NewValues["CodigoPerfilWf"];
                    DataSet ds2 = cDados.getTiposGruposPessoasWf(_key, "AND CodigoPerfilWf = " + e.NewValues["CodigoPerfilWf"]);
                    if ((true == cDados.DataSetOk(ds2)) && (true == cDados.DataTableOk(ds2.Tables[0])))
                        dr["NomePerfilWf"] = ds2.Tables[0].Rows[0]["NomePerfilWf"] + "";
                }

                if (e.NewValues[1] != null)
                    dr["Mensagem"] = (e.NewValues[1] + "" == "S") ? Resources.traducao.adm_edicaoWorkflows_acompanhamento : Resources.traducao.adm_edicaoWorkflows_a__o;
                else
                    dr["Mensagem"] = Resources.traducao.adm_edicaoWorkflows_a__o;

                dtForms.AcceptChanges();
                break;
            }
        }

        Session["dtGruposConectores"] = dtForms;

        gv_GruposNotificados_cnt.DataSource = dtForms;

        gv_GruposNotificados_cnt.DataBind();

        e.Cancel = true;
        gv_GruposNotificados_cnt.CancelEdit();
    }

    protected void gv_GruposNotificados_cnt_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        DataTable dtForms = (DataTable)Session["dtGruposConectores"];

        foreach (DataRow dr in dtForms.Rows)
        {
            if (dr["CodigoPerfilWf"] + "" == e.Keys["CodigoPerfilWf"] + "")
            {
                dr.Delete();
                dtForms.AcceptChanges();
                break;
            }
        }

        Session["dtGruposConectores"] = dtForms;

        gv_GruposNotificados_cnt.DataSource = dtForms;

        gv_GruposNotificados_cnt.DataBind();

        e.Cancel = true;
        gv_GruposNotificados_cnt.CancelEdit();

    }

    protected void gv_GruposNotificados_cnt_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        if ((e.CallbackName == "CUSTOMCALLBACK") && (e.Args[0] == "LimpaGrid"))
        {
            limpaGrids(gv_GruposNotificados_cnt);
            AtribuiDatasetGridGrupos_cnt();
        }
    }

    #endregion

    #region gvACOES CONECTORES

    private string obtemInfoNodeFromAcao(XmlNode acao, string tag)
    {
        string texto = "";
        XmlNodeList aux = acao.SelectNodes(tag);

        if ((null != aux) && (aux.Count > 0))
        {
            if ((null != aux.Item(0).FirstChild) && (null != aux.Item(0).FirstChild.Value))
                texto = aux.Item(0).FirstChild.Value.ToString();
        }
        return texto;
    }

    private void carregaGridAcoesAutomaticas_cnt(XmlNode acao)
    {
        DataTable dtSessao = ((DataSet)Session["dtAcoesAutomaticas"]).Tables[Kn_IndiceTable_AcoesAutomaticas_Connector];
        DataRow linha;

        foreach (XmlElement elementos in acao.SelectNodes("acoesAutomaticas/acaoAutomatica"))
        {
            linha = dtSessao.NewRow();

            linha[0] = elementos.Attributes["id"].Value;
            linha[1] = elementos.Attributes["name"].Value;

            dtSessao.Rows.Add(linha);
        }

        gv_Acoes_cnt.DataSource = dtSessao;
        gv_Acoes_cnt.DataBind();
    }

    private void AtribuiDatasetGridAcoes()
    {
        DataSet dsResult = new DataSet();

        DataTable tabelas = new DataTable();
        dsResult.Tables.Add(tabelas);

        tabelas = new DataTable();
        dsResult.Tables.Add(tabelas);
        tabelas = new DataTable();
        dsResult.Tables.Add(tabelas);

        DataColumn NewColumn = null;

        for (int i = 0; i < 3; i++)
        {
            NewColumn = new DataColumn("CodigoAcaoAutomaticaWf", Type.GetType("System.Int16"));
            NewColumn.Caption = "Codigo";
            NewColumn.ReadOnly = false;
            dsResult.Tables[i].Columns.Add(NewColumn);

            NewColumn = new DataColumn("Nome", Type.GetType("System.String"));
            NewColumn.Caption = "Ações";
            NewColumn.ReadOnly = false;
            dsResult.Tables[i].Columns.Add(NewColumn);
        }

        Session["dtAcoesAutomaticas"] = dsResult;

        gv_Acoes_cnt.DataSource = dsResult.Tables[Kn_IndiceTable_AcoesAutomaticas_Connector];
        gv_Acoes_cnt.DataBind();

        gv_Acoes_tmr.DataSource = dsResult.Tables[Kn_IndiceTable_AcoesAutomaticas_Timer];
        gv_Acoes_tmr.DataBind();

        gv_Acoes_wf.DataSource = dsResult.Tables[Kn_IndiceTable_AcoesAutomaticas_Wf];
        gv_Acoes_wf.DataBind();
    }

    //10/01/2011 : Copia by Alejandro
    private void AtribuiDatasetGridAcoesWf()
    {
        DataSet dsResult = new DataSet();

        DataTable tabelas = new DataTable();
        dsResult.Tables.Add(tabelas);

        DataColumn NewColumn = null;

        for (int i = 0; i < 1; i++)
        {
            NewColumn = new DataColumn("CodigoAcaoAutomaticaWf", Type.GetType("System.Int16"));
            NewColumn.Caption = "Codigo";
            NewColumn.ReadOnly = false;
            dsResult.Tables[i].Columns.Add(NewColumn);

            NewColumn = new DataColumn("Nome", Type.GetType("System.String"));
            NewColumn.Caption = "Ações";
            NewColumn.ReadOnly = false;
            dsResult.Tables[i].Columns.Add(NewColumn);
        }

        Session["dtAcoesAutomaticas"] = dsResult;

        gv_Acoes_wf.DataSource = dsResult.Tables[Kn_IndiceTable_AcoesAutomaticas_Connector];
        gv_Acoes_wf.DataBind();
    }

    protected void gv_Acoes_cnt_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CodigoAcaoAutomaticaWf")
        {
            string where = "";

            ASPxComboBox combo = (ASPxComboBox)e.Editor;

            DataTable dt = cDados.getTiposAcoesAutomaticasWf(_key, where).Tables[0];

            combo.DataSource = dt;

            combo.ValueField = "CodigoAcaoAutomaticaWf";

            combo.TextField = "Nome";

            combo.DataBind();
        }
    }

    protected void gv_Acoes_cnt_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        DataSet ds = (DataSet)Session["dtAcoesAutomaticas"];

        foreach (DataRow dr in ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Connector].Rows)
        {
            if (dr["CodigoAcaoAutomaticaWf"] + "" == e.Values["CodigoAcaoAutomaticaWf"] + "")
            {
                dr.Delete();
                ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Connector].AcceptChanges();
                break;
            }
        }

        Session["dtAcoesAutomaticas"] = ds;

        gv_Acoes_cnt.DataSource = ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Connector];

        gv_Acoes_cnt.DataBind();

        e.Cancel = true;
        gv_Acoes_cnt.CancelEdit();
    }

    protected void gv_Acoes_cnt_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataSet ds = (DataSet)Session["dtAcoesAutomaticas"];
        DataTable dt = ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Connector];
        DataRow dr = dt.NewRow();

        if (e.NewValues["CodigoAcaoAutomaticaWf"] != null)
        {
            dr["CodigoAcaoAutomaticaWf"] = int.Parse(e.NewValues["CodigoAcaoAutomaticaWf"].ToString());

            DataSet ds2 = cDados.getTiposAcoesAutomaticasWf(_key, "AND CodigoAcaoAutomaticaWf = " + e.NewValues["CodigoAcaoAutomaticaWf"]);
            if ((true == cDados.DataSetOk(ds2)) && (true == cDados.DataTableOk(ds2.Tables[0])))
            {
                dr["Nome"] = ds2.Tables[0].Rows[0]["Nome"] + "";
            }
        }

        dt.Rows.Add(dr);

        gv_Acoes_cnt.DataSource = dt;
        gv_Acoes_cnt.DataBind();

        e.Cancel = true;
        gv_Acoes_cnt.CancelEdit();
    }

    protected void gv_Acoes_cnt_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataSet ds = (DataSet)Session["dtAcoesAutomaticas"];

        foreach (DataRow dr in ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Connector].Rows)
        {
            if (dr["CodigoAcaoAutomaticaWf"] + "" == e.Keys["CodigoAcaoAutomaticaWf"] + "")
            {
                if (null != e.NewValues["CodigoAcaoAutomaticaWf"])
                {
                    dr["CodigoAcaoAutomaticaWf"] = e.NewValues["CodigoAcaoAutomaticaWf"];

                    DataSet ds2 = cDados.getTiposAcoesAutomaticasWf(_key, "AND CodigoAcaoAutomaticaWf = " + e.NewValues["CodigoAcaoAutomaticaWf"]);
                    if ((true == cDados.DataSetOk(ds2)) && (true == cDados.DataTableOk(ds2.Tables[0])))
                    {
                        dr["Nome"] = ds2.Tables[0].Rows[0]["Nome"] + "";
                    }
                }

                ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Connector].AcceptChanges();
                break;
            }
        }

        Session["dtAcoesAutomaticas"] = ds;
        gv_Acoes_cnt.DataSource = ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Connector];
        gv_Acoes_cnt.DataBind();
        e.Cancel = true;
        gv_Acoes_cnt.CancelEdit();
    }

    protected void gv_Acoes_cnt_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        if (e.CallbackName == "CUSTOMCALLBACK")
        {
            limpaGrids(gv_Acoes_cnt);
            AtribuiDatasetGridAcoes();
        }
    }

    #endregion

    #endregion

    #region TIMER

    private void carregaCamposTimers(string timerID, string nomeOrigem, string valorTempo, string unidadeTempo, string nomeDestino, string assuntoNotificacao, string textoNotificacao, string assuntoNotificacao2, string textoNotificacao2)
    {
        cmbEtapaOrigem_tmr.JSProperties["cpTimerID"] = timerID;
        cmbEtapaOrigem_tmr.JSProperties["cpOriginalTimerID"] = timerID;
        cmbEtapaOrigem_tmr.Text = nomeOrigem;

        if (null == cmbEtapaOrigem_tmr.SelectedItem)
            cmbEtapaOrigem_tmr.JSProperties["cpOriginalFrom"] = "";
        else
            cmbEtapaOrigem_tmr.JSProperties["cpOriginalFrom"] = cmbEtapaOrigem_tmr.SelectedItem.Value.ToString();

        cmbEtapaDestino_tmr.Text = nomeDestino;
        if (null == cmbEtapaDestino_tmr.SelectedItem)
            cmbEtapaDestino_tmr.JSProperties["cpOriginalTo"] = "";
        else
            cmbEtapaDestino_tmr.JSProperties["cpOriginalTo"] = cmbEtapaDestino_tmr.SelectedItem.Value.ToString();

        edtQtdTempo_tmr.Text = valorTempo;
        cmbUnidadeTempo_tmr.Text = unidadeTempo;
        ddlUnidadeTempo.Text = unidadeTempo;

        mmTexto1_tmr.Text = textoNotificacao;
        txtAssunto1_tmr.Text = assuntoNotificacao;
        mmTexto2_tmr.Text = textoNotificacao2;
        txtAssunto2_tmr.Text = assuntoNotificacao2;
    }

    protected void painelCallbackTimers_Callback(object sender, CallbackEventArgsBase e)
    {
        XmlNode acaoEtapa;

        string timerID = "";
        string codigoFrom = "";
        string descricao = "";
        string nomeOrigem = "";
        string nomeDestino = "";
        string textoNotificacao = "";
        string assuntoNotificacao = "";
        string textoNotificacao2 = "";
        string assuntoNotificacao2 = "";
        string valorTempo = "";
        string unidadeTempo = "";

        int _indexRow;

        // e.Parameter conterá o índice da linha clicada na gvEdicaoElementos
        if (int.TryParse(e.Parameter, out _indexRow))
        {
            codigoFrom = gvEdicaoElementos.GetRowValues(_indexRow, "idorigemUnbound").ToString();
            nomeOrigem = gvEdicaoElementos.GetRowValues(_indexRow, "origem").ToString();
            nomeDestino = gvEdicaoElementos.GetRowValues(_indexRow, "destino").ToString();
            descricao = gvEdicaoElementos.GetRowValues(_indexRow, "descricao").ToString();
            timerID = gvEdicaoElementos.GetRowValues(_indexRow, "idEtapa").ToString();

            acaoEtapa = obtemAcaoEtapaFromXmlTela(codigoFrom, "timer");

            AtribuiDatasetGridGrupos_tmr();
            AtribuiDatasetGridAcoes();

            if (acaoEtapa != null)
            {
                valorTempo = acaoEtapa.Attributes["timeoutValue"].Value;
                unidadeTempo = acaoEtapa.Attributes["timeoutUnit"].Value;
                textoNotificacao = obtemInfoNodeFromAcao(acaoEtapa, "textoNotificacao");
                assuntoNotificacao = obtemInfoNodeFromAcao(acaoEtapa, "assuntoNotificacao");
                textoNotificacao2 = obtemInfoNodeFromAcao(acaoEtapa, "textoNotificacao2");
                assuntoNotificacao2 = obtemInfoNodeFromAcao(acaoEtapa, "assuntoNotificacao2");

                carregaGridGruposNotificados_tmr(acaoEtapa);
                carregaGridAcoesAutomaticas_tmr(acaoEtapa);
            }

            carregaCamposTimers(timerID, nomeOrigem, valorTempo, unidadeTempo, nomeDestino, assuntoNotificacao, textoNotificacao, assuntoNotificacao2, textoNotificacao2);
        }
    }

    #region gvGRUPOSNOTIFICADOS TIMER

    private void AtribuiDatasetGridGrupos_tmr()
    {
        DataTable dtResult = new DataTable();
        DataColumn NewColumn = null;

        NewColumn = new DataColumn("CodigoPerfilWf", Type.GetType("System.Int32"));
        NewColumn.Caption = "CodigoPerfil";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("NomePerfilWf", Type.GetType("System.String"));
        NewColumn.Caption = "NomePerfil";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("Mensagem", Type.GetType("System.String"));
        NewColumn.Caption = "Mensagem";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        Session["dtGruposConectores"] = dtResult;

        gv_GruposNotificados_tmr.DataSource = dtResult;
        gv_GruposNotificados_tmr.DataBind();
    }

    private void carregaGridGruposNotificados_tmr(XmlNode acao)
    {
        DataTable dtSessao = (DataTable)Session["dtGruposConectores"];
        DataRow linha;

        foreach (XmlElement elementos in acao.SelectNodes("gruposNotificados/grupo"))
        {
            linha = dtSessao.NewRow();

            linha[0] = elementos.Attributes["id"].Value;
            linha[1] = elementos.Attributes["name"].Value;
            linha[2] = elementos.Attributes["msgBox"].Value;

            dtSessao.Rows.Add(linha);
        }

        gv_GruposNotificados_tmr.DataSource = dtSessao;
        gv_GruposNotificados_tmr.DataBind();
    }

    protected void gv_GruposNotificados_tmr_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        gruposEnLaLista = "";
        if (e.Column.FieldName == "CodigoPerfilWf")
        {
            string where = "";

            //Listar los CodigoPerfilWf que se encuentran en la Grid. De esta forma se evitará su 
            //listado en el combo salvando de una selección repetitiva.
            for (int i = 0; i < gv_GruposNotificados_tmr.VisibleRowCount; i++)
            {
                if (e.KeyValue != gv_GruposNotificados_tmr.GetRowValues(i, "CodigoPerfilWf"))
                    gruposEnLaLista += "," + gv_GruposNotificados_tmr.GetRowValues(i, "CodigoPerfilWf").ToString();
            }
            if (gruposEnLaLista != "")
            {
                where += " AND CodigoPerfilWf NOT IN(" + gruposEnLaLista.Substring(1) + ")";
            }
            where += string.Format(@" AND CodigoEntidade = {0} ", CodigoEntidade);
            ASPxComboBox combo = (ASPxComboBox)e.Editor;
            DataTable auxDt = cDados.getTiposGruposPessoasWf(_key, where).Tables[0];

            combo.DataSource = auxDt;
            combo.ValueField = "CodigoPerfilWf";
            combo.TextField = "NomePerfilWf";
            combo.DataBind();
        }

        if (e.Column.FieldName == "CodigoPerfilWf" && e.KeyValue != null)
        {
            ASPxComboBox combo = (ASPxComboBox)e.Editor;
            combo.ClientEnabled = false;
        }
    }

    protected void gv_GruposNotificados_tmr_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dtForms = (DataTable)Session["dtGruposConectores"];

        DataRow dr = dtForms.NewRow();

        if (e.NewValues["CodigoPerfilWf"] != null)
        {
            dr["CodigoPerfilWf"] = e.NewValues["CodigoPerfilWf"];

            DataSet ds2 = cDados.getTiposGruposPessoasWf(_key, "AND CodigoPerfilWf = " + e.NewValues["CodigoPerfilWf"]);
            if ((true == cDados.DataSetOk(ds2)) && (true == cDados.DataTableOk(ds2.Tables[0])))
                dr["NomePerfilWf"] = ds2.Tables[0].Rows[0]["NomePerfilWf"] + "";
        }

        dr["Mensagem"] = (e.NewValues[1] != null) ? e.NewValues[1] : "E";

        if (e.NewValues[1] != null)
            dr["Mensagem"] = (e.NewValues[1] + "" == "S") ? Resources.traducao.adm_edicaoWorkflows_acompanhamento : Resources.traducao.adm_edicaoWorkflows_a__o;
        else
            dr["Mensagem"] = Resources.traducao.adm_edicaoWorkflows_a__o;

        dtForms.Rows.Add(dr);

        gv_GruposNotificados_tmr.DataSource = dtForms;
        gv_GruposNotificados_tmr.DataBind();

        e.Cancel = true;
        gv_GruposNotificados_tmr.CancelEdit();
    }

    protected void gv_GruposNotificados_tmr_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dtForms = (DataTable)Session["dtGruposConectores"];

        foreach (DataRow dr in dtForms.Rows)
        {
            if (dr["CodigoPerfilWf"] + "" == e.Keys["CodigoPerfilWf"] + "")
            {
                if (null != e.NewValues["CodigoPerfilWf"])
                {
                    dr["CodigoPerfilWf"] = e.NewValues["CodigoPerfilWf"];
                    DataSet ds2 = cDados.getTiposGruposPessoasWf(_key, "AND CodigoPerfilWf = " + e.NewValues["CodigoPerfilWf"]);
                    if ((true == cDados.DataSetOk(ds2)) && (true == cDados.DataTableOk(ds2.Tables[0])))
                        dr["NomePerfilWf"] = ds2.Tables[0].Rows[0]["NomePerfilWf"] + "";
                }

                if (e.NewValues[1] != null)
                    dr["Mensagem"] = (e.NewValues[1] + "" == "S") ? Resources.traducao.adm_edicaoWorkflows_acompanhamento : Resources.traducao.adm_edicaoWorkflows_a__o;
                else
                    dr["Mensagem"] = Resources.traducao.adm_edicaoWorkflows_a__o;

                dtForms.AcceptChanges();
                break;
            }
        }

        //Session["dtGruposConectores"] = dtForms;

        gv_GruposNotificados_tmr.DataSource = dtForms;

        gv_GruposNotificados_tmr.DataBind();

        e.Cancel = true;
        gv_GruposNotificados_tmr.CancelEdit();
    }

    protected void gv_GruposNotificados_tmr_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        DataTable dtForms = (DataTable)Session["dtGruposConectores"];

        foreach (DataRow dr in dtForms.Rows)
        {
            if (dr["CodigoPerfilWf"] + "" == e.Keys["CodigoPerfilWf"] + "")
            {
                dr.Delete();
                dtForms.AcceptChanges();
                break;
            }
        }

        //Session["dtGruposConectores"] = dtForms;

        gv_GruposNotificados_tmr.DataSource = dtForms;

        gv_GruposNotificados_tmr.DataBind();

        e.Cancel = true;
        gv_GruposNotificados_tmr.CancelEdit();

    }

    protected void gv_GruposNotificados_tmr_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        if ((e.CallbackName == "CUSTOMCALLBACK") && (e.Args[0] == "LimpaGrid"))
        {
            limpaGrids(gv_GruposNotificados_tmr);
            AtribuiDatasetGridGrupos_tmr();
        }
    }

    #endregion

    #region gvACOES TIMER

    private void carregaGridAcoesAutomaticas_tmr(XmlNode acao)
    {
        DataTable dtSessao = ((DataSet)Session["dtAcoesAutomaticas"]).Tables[Kn_IndiceTable_AcoesAutomaticas_Timer];
        DataRow linha;

        foreach (XmlElement elementos in acao.SelectNodes("acoesAutomaticas/acaoAutomatica"))
        {
            linha = dtSessao.NewRow();

            linha[0] = elementos.Attributes["id"].Value;
            linha[1] = elementos.Attributes["name"].Value;

            dtSessao.Rows.Add(linha);
        }

        gv_Acoes_tmr.DataSource = dtSessao;
        gv_Acoes_tmr.DataBind();
    }

    protected void gv_Acoes_tmr_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CodigoAcaoAutomaticaWf")
        {
            string where = "";

            ASPxComboBox combo = (ASPxComboBox)e.Editor;

            DataTable dtForms = cDados.getTiposAcoesAutomaticasWf(_key, where).Tables[0];

            combo.DataSource = dtForms;

            combo.ValueField = "CodigoAcaoAutomaticaWf";

            combo.TextField = "Nome";

            combo.DataBind();
        }
    }

    protected void gv_Acoes_tmr_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        DataSet ds = (DataSet)Session["dtAcoesAutomaticas"];

        foreach (DataRow dr in ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Timer].Rows)
        {
            if (dr["CodigoAcaoAutomaticaWf"] + "" == e.Values["CodigoAcaoAutomaticaWf"] + "")
            {
                dr.Delete();
                ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Timer].AcceptChanges();
                break;
            }
        }

        Session["dtAcoesAutomaticas"] = ds;

        gv_Acoes_tmr.DataSource = ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Timer];

        gv_Acoes_tmr.DataBind();

        e.Cancel = true;
        gv_Acoes_tmr.CancelEdit();
    }

    protected void gv_Acoes_tmr_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataSet ds = (DataSet)Session["dtAcoesAutomaticas"];
        DataTable dt = ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Timer];
        DataRow dr = dt.NewRow();

        if (e.NewValues["CodigoAcaoAutomaticaWf"] != null)
        {
            dr["CodigoAcaoAutomaticaWf"] = int.Parse(e.NewValues["CodigoAcaoAutomaticaWf"].ToString());

            DataSet ds2 = cDados.getTiposAcoesAutomaticasWf(_key, "AND CodigoAcaoAutomaticaWf = " + e.NewValues["CodigoAcaoAutomaticaWf"]);
            if ((true == cDados.DataSetOk(ds2)) && (true == cDados.DataTableOk(ds2.Tables[0])))
            {
                dr["Nome"] = ds2.Tables[0].Rows[0]["Nome"] + "";
            }
        }

        dt.Rows.Add(dr);

        gv_Acoes_tmr.DataSource = dt;
        gv_Acoes_tmr.DataBind();

        e.Cancel = true;
        gv_Acoes_tmr.CancelEdit();
    }

    protected void gv_Acoes_tmr_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataSet ds = (DataSet)Session["dtAcoesAutomaticas"];

        foreach (DataRow dr in ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Timer].Rows)
        {
            if (dr["CodigoAcaoAutomaticaWf"] + "" == e.Keys["CodigoAcaoAutomaticaWf"] + "")
            {
                if (null != e.NewValues["CodigoAcaoAutomaticaWf"])
                {
                    dr["CodigoAcaoAutomaticaWf"] = e.NewValues["CodigoAcaoAutomaticaWf"];

                    DataSet ds2 = cDados.getTiposAcoesAutomaticasWf(_key, "AND CodigoAcaoAutomaticaWf = " + e.NewValues["CodigoAcaoAutomaticaWf"]);
                    if ((true == cDados.DataSetOk(ds2)) && (true == cDados.DataTableOk(ds2.Tables[0])))
                    {
                        dr["Nome"] = ds2.Tables[0].Rows[0]["Nome"] + "";
                    }
                }

                ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Timer].AcceptChanges();
                break;
            }
        }

        Session["dtAcoesAutomaticas"] = ds;

        gv_Acoes_tmr.DataSource = ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Timer];

        gv_Acoes_tmr.DataBind();

        e.Cancel = true;
        gv_Acoes_tmr.CancelEdit();
    }

    protected void gv_Acoes_tmr_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        if (e.CallbackName == "CUSTOMCALLBACK")
        {
            limpaGrids(gv_Acoes_tmr);
            AtribuiDatasetGridAcoes();
        }
    }

    #endregion

    #endregion

    #region DISJUNÇÃO-JUNÇÃO

    protected void pnlCnkDisJunFim_Callback(object sender, CallbackEventArgsBase e)
    {
        string descricao = "";
        string codigoFrom = "";

        int _indexRow;

        if (int.TryParse(e.Parameter, out _indexRow))
        {
            string elementName = string.Empty;
            int spacePos, hyphenPos;

            codigoFrom = gvEdicaoElementos.GetRowValues(_indexRow, "idEtapa").ToString();
            descricao = gvEdicaoElementos.GetRowValues(_indexRow, "descricao").ToString();

            pnlCnkDisJunFim.JSProperties["cpElementID"] = codigoFrom;

            spacePos = descricao.IndexOf(' ');
            if (-1 != spacePos)
            {
                pnlCnkDisJunFim.JSProperties["cpElementName"] = descricao.Substring(0, spacePos);

                hyphenPos = descricao.IndexOf('-');
                if (hyphenPos > spacePos)
                {
                    pnlCnkDisJunFim.JSProperties["cpCaptionID"] = descricao.Substring(spacePos, hyphenPos - spacePos).Trim();
                    pnlCnkDisJunFim.JSProperties["cpElementCaption"] = descricao.Substring(hyphenPos).Replace("-", "").Trim();
                } // if (hyphenPos > spacePos)
                else
                {
                    pnlCnkDisJunFim.JSProperties["cpCaptionID"] = descricao.Substring(spacePos).Trim();
                    pnlCnkDisJunFim.JSProperties["cpElementCaption"] = "";
                }
            } // if (-1 != spacePos)
            else
            {
                pnlCnkDisJunFim.JSProperties["cpElementName"] = descricao;
                pnlCnkDisJunFim.JSProperties["cpCaptionID"] = "";
                pnlCnkDisJunFim.JSProperties["cpElementCaption"] = "";
            }

        }
    }

    #endregion

    #region AÇÃO FLUXO (INICIO/CANCELAMENTO)

    private void AtribuiDatasetGridGrupos_wf()
    {
        DataTable dtResult = new DataTable();
        DataColumn NewColumn = null;

        NewColumn = new DataColumn("CodigoPerfilWf", Type.GetType("System.Int32"));
        NewColumn.Caption = "CodigoPerfil";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("NomePerfilWf", Type.GetType("System.String"));
        NewColumn.Caption = "NomePerfil";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("Mensagem", Type.GetType("System.String"));
        NewColumn.Caption = "Mensagem";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        Session["dtGruposConectores"] = dtResult;

        gv_GruposNotificados_wf.DataSource = dtResult;
        gv_GruposNotificados_wf.DataBind();
    }

    private void carregaCamposAcaoWf(string texto, string assunto, string descricao, string texto2, string assunto2)
    {

        /*
           var acao = lblAcaoWf.GetText();
    if ("cancelado." == acao)
        acao = "Cancelamento";
    else if ("iniciado." == acao)
        acao = "Início";
    __wf_acaoWfObj.acao = acao
         
         */


        if (hfValoresTela.Contains("nomeFluxo"))
            lbWorkflow_acaoWf.Text = hfValoresTela.Get("nomeFluxo").ToString();
        if (Resources.traducao.adm_edicaoWorkflows_in_cio == descricao)
        {
            lblCaptionDivAcaoWf.Text = descricao + " " + Resources.traducao.adm_edicaoWorkflows_do + " " + lblCaptionDivAcaoWf.Text;
            lblMensagemWf.Text = Resources.traducao.adm_edicaoWorkflows_aten__o_____as_notifica__es_e_a__es_autom_ticas_informadas_nesta_tela_ocorrer_o_quando_o_fluxo_for_;
            lblAcaoWf.Text = "iniciado.";
        }
        else if ("Cancelamento" == descricao)
        {
            lblCaptionDivAcaoWf.Text = descricao + " " + Resources.traducao.adm_edicaoWorkflows_do + " " + lblCaptionDivAcaoWf.Text;
            lblMensagemWf.Text = Resources.traducao.adm_edicaoWorkflows_aten__o_____as_notifica__es_e_a__es_autom_ticas_informadas_nesta_tela_ocorrer_o_quando_e_se_o_fluxo_for_;
            lblAcaoWf.Text = "cancelado.";
        }
        mmTexto1_wf.Text = texto;
        txtAssunto1_wf.Text = assunto;
        mmTexto2_wf.Text = texto2;
        txtAssunto2_wf.Text = assunto2;
    }

    protected void painelCallbackAcoesWf_Callback(object sender, CallbackEventArgsBase e)
    {
        XmlNode acaoEtapa;
        painelCallbackAcoesWf.JSProperties["cp_EtapaInicio"] = "";
        string codigoFrom = "";
        string descricao = "";
        string nomeOrigem = "";
        string nomeDestino = "";
        string texto = "";
        string assunto = "";
        string texto2 = "";
        string assunto2 = "";

        int _indexRow;

        // e.Parameter conterá o índice da linha clicada na gvEdicaoElementos
        if (int.TryParse(e.Parameter, out _indexRow))
        {
            codigoFrom = gvEdicaoElementos.GetRowValues(_indexRow, "idorigemUnbound").ToString();
            nomeOrigem = gvEdicaoElementos.GetRowValues(_indexRow, "origem").ToString();
            nomeDestino = gvEdicaoElementos.GetRowValues(_indexRow, "destino").ToString();
            descricao = gvEdicaoElementos.GetRowValues(_indexRow, "descricao").ToString();

            //if ("Inicio del Fluxo" == descricao || "Cancelamento del Fluxo" == descricao)
            //    descricao = "";

            acaoEtapa = obtemAcaoEtapaFromXmlTela(codigoFrom, descricao);

            AtribuiDatasetGridGrupos_wf();
            AtribuiDatasetGridAcoes();

            if (null != acaoEtapa)
            {
                texto = obtemInfoNodeFromAcao(acaoEtapa, "textoNotificacao");
                assunto = obtemInfoNodeFromAcao(acaoEtapa, "assuntoNotificacao");
                texto2 = obtemInfoNodeFromAcao(acaoEtapa, "textoNotificacao2");
                assunto2 = obtemInfoNodeFromAcao(acaoEtapa, "assuntoNotificacao2");

                carregaGridGruposNotificadosAcaoWf(acaoEtapa);
                carregaGridAcoesAutomaticas_wf(acaoEtapa);
            }

            carregaCamposAcaoWf(texto, assunto, descricao, texto2, assunto2);

            if (codigoFrom == "0")
                painelCallbackAcoesWf.JSProperties["cp_EtapaInicio"] = "S";
        }
    }

    #region GRUPO NOTIFICADOS

    private void carregaGridGruposNotificadosAcaoWf(XmlNode acao)
    {
        DataTable dtSessao = (DataTable)Session["dtGruposConectores"];
        DataRow linha;

        foreach (XmlElement elementos in acao.SelectNodes("gruposNotificados/grupo"))
        {
            linha = dtSessao.NewRow();

            linha[0] = elementos.Attributes["id"].Value;
            linha[1] = elementos.Attributes["name"].Value;
            linha[2] = elementos.Attributes["msgBox"].Value;

            dtSessao.Rows.Add(linha);
        }

        gv_GruposNotificados_wf.DataSource = dtSessao;
        gv_GruposNotificados_wf.DataBind();
    }

    protected void gv_GruposNotificados_wf_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        gruposEnLaLista = "";
        if (e.Column.FieldName == "CodigoPerfilWf")
        {
            DataTable dtSessao = (DataTable)Session["dtGruposConectores"];
            string where = "";

            for (int i = 0; i < gv_GruposNotificados_wf.VisibleRowCount; i++)
            { //Listar los CodigoPerfilWf que se encuentran en la Grid. De esta forma se evitará su 
                //listado en el combo salvando de una selección repetitiva.
                if (e.KeyValue != gv_GruposNotificados_wf.GetRowValues(i, "CodigoPerfilWf"))
                    gruposEnLaLista += "," + gv_GruposNotificados_wf.GetRowValues(i, "CodigoPerfilWf").ToString();
            }
            if (gruposEnLaLista != "")
            {
                where += " AND CodigoPerfilWf NOT IN(" + gruposEnLaLista.Substring(1) + ")";
            }

            where += string.Format(@" AND CodigoEntidade = {0} ", CodigoEntidade);
            ASPxComboBox combo = (ASPxComboBox)e.Editor;
            DataTable dtForms = cDados.getTiposGruposPessoasWf(_key, where).Tables[0];

            combo.DataSource = dtForms;
            combo.ValueField = "CodigoPerfilWf";
            combo.TextField = "NomePerfilWf";
            combo.DataBind();
        }
    }

    protected void gv_GruposNotificados_wf_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dtForms = (DataTable)Session["dtGruposConectores"];
        DataRow dr = dtForms.NewRow();

        if (e.NewValues["CodigoPerfilWf"] != null)
        {
            dr["CodigoPerfilWf"] = e.NewValues["CodigoPerfilWf"];

            DataSet ds2 = cDados.getTiposGruposPessoasWf(_key, "AND CodigoPerfilWf = " + e.NewValues["CodigoPerfilWf"]);
            if ((true == cDados.DataSetOk(ds2)) && (true == cDados.DataTableOk(ds2.Tables[0])))
                dr["NomePerfilWf"] = ds2.Tables[0].Rows[0]["NomePerfilWf"] + "";
        }

        dr["Mensagem"] = (e.NewValues[1] != null) ? e.NewValues[1] : "E";

        if (e.NewValues[1] != null)
            dr["Mensagem"] = (e.NewValues[1] + "" == "S") ? Resources.traducao.adm_edicaoWorkflows_acompanhamento : Resources.traducao.adm_edicaoWorkflows_a__o;
        else
            dr["Mensagem"] = Resources.traducao.adm_edicaoWorkflows_a__o;

        dtForms.Rows.Add(dr);
        gv_GruposNotificados_wf.DataSource = dtForms;
        gv_GruposNotificados_wf.DataBind();
        e.Cancel = true;
        gv_GruposNotificados_wf.CancelEdit();
    }

    protected void gv_GruposNotificados_wf_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dtForms = (DataTable)Session["dtGruposConectores"];

        foreach (DataRow dr in dtForms.Rows)
        {
            if (dr["CodigoPerfilWf"] + "" == e.Keys["CodigoPerfilWf"] + "")
            {
                if (null != e.NewValues["CodigoPerfilWf"])
                {
                    dr["CodigoPerfilWf"] = e.NewValues["CodigoPerfilWf"];
                    DataSet ds2 = cDados.getTiposGruposPessoasWf(_key, "AND CodigoPerfilWf = " + e.NewValues["CodigoPerfilWf"]);
                    if ((true == cDados.DataSetOk(ds2)) && (true == cDados.DataTableOk(ds2.Tables[0])))
                        dr["NomePerfilWf"] = ds2.Tables[0].Rows[0]["NomePerfilWf"] + "";
                }

                if (e.NewValues[1] != null)
                    dr["Mensagem"] = (e.NewValues[1] + "" == "S") ? Resources.traducao.adm_edicaoWorkflows_acompanhamento : Resources.traducao.adm_edicaoWorkflows_a__o;
                else
                    dr["Mensagem"] = Resources.traducao.adm_edicaoWorkflows_a__o;

                dtForms.AcceptChanges();
                break;
            }
        }

        Session["dtGruposConectores"] = dtForms;
        gv_GruposNotificados_wf.DataSource = dtForms;
        gv_GruposNotificados_wf.DataBind();
        e.Cancel = true;
        gv_GruposNotificados_wf.CancelEdit();
    }

    protected void gv_GruposNotificados_wf_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        DataTable dtForms = (DataTable)Session["dtGruposConectores"];

        foreach (DataRow dr in dtForms.Rows)
        {
            if (dr["CodigoPerfilWf"] + "" == e.Keys["CodigoPerfilWf"] + "")
            {
                dr.Delete();
                dtForms.AcceptChanges();
                break;
            }
        }

        Session["dtGruposConectores"] = dtForms;
        gv_GruposNotificados_wf.DataSource = dtForms;
        gv_GruposNotificados_wf.DataBind();
        e.Cancel = true;
        gv_GruposNotificados_wf.CancelEdit();
    }

    protected void gv_GruposNotificados_wf_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        if ((e.CallbackName == "CUSTOMCALLBACK") && (e.Args[0] == "LimpaGrid"))
        {
            limpaGrids(gv_GruposNotificados_wf);
            AtribuiDatasetGridGrupos_cnt();
        }
    }

    #endregion

    #region AÇÕES AUTOMATICAS

    private void carregaGridAcoesAutomaticas_wf(XmlNode acao)
    {
        DataTable dtSessao = ((DataSet)Session["dtAcoesAutomaticas"]).Tables[Kn_IndiceTable_AcoesAutomaticas_Connector];
        DataRow linha;

        foreach (XmlElement elementos in acao.SelectNodes("acoesAutomaticas/acaoAutomatica"))
        {
            linha = dtSessao.NewRow();

            linha[0] = elementos.Attributes["id"].Value;
            linha[1] = elementos.Attributes["name"].Value;

            dtSessao.Rows.Add(linha);
        }

        gv_Acoes_wf.DataSource = dtSessao;
        gv_Acoes_wf.DataBind();
    }

    protected void gv_Acoes_wf_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        if (e.CallbackName == "CUSTOMCALLBACK")
        {
            limpaGrids(gv_Acoes_cnt);
            AtribuiDatasetGridAcoes();
        }
    }

    protected void gv_Acoes_wf_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataSet ds = (DataSet)Session["dtAcoesAutomaticas"];
        DataTable dt = ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Connector];
        DataRow dr = dt.NewRow();

        if (e.NewValues["CodigoAcaoAutomaticaWf"] != null)
        {
            dr["CodigoAcaoAutomaticaWf"] = int.Parse(e.NewValues["CodigoAcaoAutomaticaWf"].ToString());

            DataSet ds2 = cDados.getTiposAcoesAutomaticasWf(_key, "AND CodigoAcaoAutomaticaWf = " + e.NewValues["CodigoAcaoAutomaticaWf"]);
            if ((true == cDados.DataSetOk(ds2)) && (true == cDados.DataTableOk(ds2.Tables[0])))
            {
                dr["Nome"] = ds2.Tables[0].Rows[0]["Nome"] + "";
            }
        }

        dt.Rows.Add(dr);
        gv_Acoes_wf.DataSource = dt;
        gv_Acoes_wf.DataBind();
        e.Cancel = true;
        gv_Acoes_wf.CancelEdit();
    }

    protected void gv_Acoes_wf_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataSet ds = (DataSet)Session["dtAcoesAutomaticas"];

        foreach (DataRow dr in ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Connector].Rows)
        {
            if (dr["CodigoAcaoAutomaticaWf"] + "" == e.Keys["CodigoAcaoAutomaticaWf"] + "")
            {
                if (null != e.NewValues["CodigoAcaoAutomaticaWf"])
                {
                    dr["CodigoAcaoAutomaticaWf"] = e.NewValues["CodigoAcaoAutomaticaWf"];

                    DataSet ds2 = cDados.getTiposAcoesAutomaticasWf(_key, "AND CodigoAcaoAutomaticaWf = " + e.NewValues["CodigoAcaoAutomaticaWf"]);
                    if ((true == cDados.DataSetOk(ds2)) && (true == cDados.DataTableOk(ds2.Tables[0])))
                    {
                        dr["Nome"] = ds2.Tables[0].Rows[0]["Nome"] + "";
                    }
                }

                ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Connector].AcceptChanges();
                break;
            }
        }

        Session["dtAcoesAutomaticas"] = ds;
        gv_Acoes_wf.DataSource = ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Connector];
        gv_Acoes_wf.DataBind();
        e.Cancel = true;
        gv_Acoes_wf.CancelEdit();
    }

    protected void gv_Acoes_wf_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        DataSet ds = (DataSet)Session["dtAcoesAutomaticas"];

        foreach (DataRow dr in ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Connector].Rows)
        {
            if (dr["CodigoAcaoAutomaticaWf"] + "" == e.Values["CodigoAcaoAutomaticaWf"] + "")
            {
                dr.Delete();
                ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Connector].AcceptChanges();
                break;
            }
        }

        Session["dtAcoesAutomaticas"] = ds;
        gv_Acoes_wf.DataSource = ds.Tables[Kn_IndiceTable_AcoesAutomaticas_Connector];
        gv_Acoes_wf.DataBind();
        e.Cancel = true;
        gv_Acoes_wf.CancelEdit();
    }

    protected void gv_Acoes_wf_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CodigoAcaoAutomaticaWf")
        {
            string where = "";
            ASPxComboBox combo = (ASPxComboBox)e.Editor;
            DataTable dt = cDados.getTiposAcoesAutomaticasWf(_key, where).Tables[0];

            combo.DataSource = dt;
            combo.ValueField = "CodigoAcaoAutomaticaWf";
            combo.TextField = "Nome";
            combo.DataBind();
        }
    }

    #endregion

    #endregion

    #region GRABAR WORKFLOW -- by Alejandro

    private void PublicaVersaoWorkflow()
    {
        // grava primeiramente o workflow
        GravaWorkflow();

        int registrosAfetados = 0;
        StringBuilder comandoSQL = new StringBuilder();

        comandoSQL.Append(@"
        BEGIN TRAN
        BEGIN TRY
            ");

        comandoSQL.Append(geraInstrucaoPublicacaoWorkflow());
        comandoSQL.Append(@"
        
        -- ---------- Ação Automatica : Cancelação do Workflow
        ");
        comandoSQL.Append(geraInstrucaoPublicacaoAcaoWorkflow());

        comandoSQL.Append(@"
            COMMIT
        END TRY
        BEGIN CATCH
		    DECLARE 
			      @ErrorMessage		Nvarchar(4000)
			    , @ErrorSeverity	Int
			    , @ErrorState		Int
			    , @ErrorNumber		Int;

		    SET @ErrorMessage		= ERROR_MESSAGE();
		    SET @ErrorSeverity	= ERROR_SEVERITY();
		    SET @ErrorState			= ERROR_STATE();
		    SET @ErrorNumber		= ERROR_NUMBER();
            ROLLBACK TRANSACTION
			RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
        END CATCH
        ");
        cDados.execSQL(comandoSQL.ToString(), ref registrosAfetados);
    }

    private string apagarWorkflowBanco(string idWorkflow)
    {
        string commandoSQL = string.Format(@"
                DECLARE @CodigoWorkflow int
                SET @CodigoWorkflow = 64

                DELETE dbo.AcoesAutomaticasEtapasWf WHERE CodigoWorkflow = @CodigoWorkflow
                DELETE dbo.RegrasNotificacoesRecursosWf WHERE CodigoWorkflow = @CodigoWorkflow
                DELETE dbo.AcoesEtapasWf WHERE CodigoWorkflow = @CodigoWorkflow
                DELETE dbo.AcessosEtapasWf WHERE CodigoWorkflow = @CodigoWorkflow
                DELETE dbo.FormulariosEtapasWf WHERE CodigoWorkflow = @CodigoWorkflow
                DELETE dbo.SubWorkflows WHERE CodigoSubWorkflow = @CodigoWorkflow
                DELETE dbo.SubWorkflows WHERE CodigoWorkflow = @CodigoWorkflow
                UPDATE dbo.Workflows SET CodigoEtapaInicial = NULL WHERE CodigoWorkflow = @CodigoWorkflow
                DELETE dbo.EtapasWf WHERE CodigoWorkflow = @CodigoWorkflow
                DELETE dbo.Workflows WHERE CodigoWorkflow = @CodigoWorkflow
                
        ", idWorkflow);
        return commandoSQL;
    }

    protected class gruposXml
    { //class utilizada para definir uma variavel de tipo estructura
        public string idWorkflows; //codigoWorkflow;
        public string etapaInicialSubWorkflow;
        public string WorkflowPai; //idWorkflowPai;
        public string etapaInicadora; //idDisjuncao;
    }

    private void chiraPalavraAntesPunto(ref string nameEtapa, string name)
    { //function que pega a palavra despois do punto que utiliza como unióm. Exemplo: de 'ale.fuentes' -> fica 'fuentes'.
        bool esPunto = false;
        foreach (char letra in name)
        {
            if (esPunto)
                nameEtapa += letra.ToString();
            if (letra.Equals('.'))
                esPunto = true;
        }
    }

    private string geraInstrucaoPublicacaoWorkflow()
    {
        XmlNodeList listaSets = obtemListaSubWorkflowsFromXmlTela();
        XmlNodeList auxNodeList, acoes, listaSubWorkflows;
        List<gruposXml> gruposWf = new List<gruposXml>();

        int contadorSubworkflow = 0;

        string dbBanco = cDados.getDbName();
        string dbOwner = cDados.getDbOwner();

        string comandoSQL = string.Empty;
        string idEtapaPai = string.Empty;
        string versaoFormatoXml = string.Empty;

        //Cargo todos os ID do workflows do grafico.
        foreach (XmlElement nodoWf in listaSets)
        {
            gruposXml grupo = new gruposXml();

            /*grupo.codigoWorkflow*/
            grupo.idWorkflows = nodoWf.GetAttribute("id");
            /*grupo.idDisjuncao*/
            grupo.etapaInicialSubWorkflow = nodoWf.GetAttribute("etapaInicialSubWorkflow");
            /*grupo.idWorkflowPai*/
            grupo.WorkflowPai = nodoWf.GetAttribute("workflowPai");
            /*grupo.idWorkflowPai */
            grupo.etapaInicadora = nodoWf.GetAttribute("etapaIniciadora");

            gruposWf.Add(grupo);
        }

        versaoFormatoXml = getVersaoFormatoXmlFromXmlTela();

        string comandoSQLWorkflow = string.Empty;
        string comandoSQLSubWorkflow = @"
        ---- --------[salvando SubWorkflow";
        string comandoSQLEtapa = string.Empty;
        string comandoSQLEformulario = @" 
        -- --------[salvando FORMULARIOS das Etapas";
        string comandoSQLAcesso = @" 
        -- --------[salvando ACESSOS das Etapas";
        string comandoSQLAcoes = string.Empty;
        string comandoSQLUpdateWorkflow = @" 
        -- --------[atualizando WORKFLOW [CodigoEtapaInicial]";
        string tipoElemento = string.Empty;

        //Genero a consulta SQL segundo o grafico Workflow feito pelo usuario
        for (int i = 0; i < gruposWf.Count; i++)
        {
            contadorSubworkflow++;
            string idWorkflowPai = gruposWf[i].WorkflowPai; //gruposWf[i].idWorkflowPai;
            string idEtapaInicialWf = gruposWf[i].etapaInicialSubWorkflow; // gruposWf[i].idDisjuncao;
            string idSubWorkflow = gruposWf[i].idWorkflows; // gruposWf[i].codigoWorkflow;
            string idEtapaIniciadora = gruposWf[i].etapaInicadora;
            string nomeSubWorkflow = string.Format("subWorkflow_{0}", contadorSubworkflow);
            string versaoSubWorkflow = string.Format("{0}.{1}", VersaoWorkflow, contadorSubworkflow);

            listaSubWorkflows = obtemListaElementosXmlTelaPorAtributo("grupoWorkflow", idSubWorkflow); //obtemSubWorkflowFromXmlTela(idSubWorkflow);


            if (idWorkflowPai == "0")
            {
                comandoSQLWorkflow += string.Format(@"

                SET NOCOUNT ON
-- --------[revogando as versões existentes para o fluxo ainda não revogadas
                UPDATE {0}.{1}.[Workflows] SET [DataRevogacao] = GETDATE() WHERE 
                    [CodigoFluxo] = {3} AND [CodigoWorkFlow] != {2} AND [DataPublicacao] IS NOT NULL AND [DataRevogacao] IS NULL

                DECLARE @CodigoSubWorkflow      int
                DECLARE @CodigoWorkflowPai_{5}  int
                DECLARE @{6}                    int
                DECLARE @CodigoAcaoWf           int
                DECLARE @CodigoEtapa            int
                DECLARE @IDConfiguracaoAcionamento as bigint

-- --------[atualizando data de publicação no workflow atual
                UPDATE {0}.{1}.[Workflows] SET [DataPublicacao] = GETDATE(), [IdentificadorUsuarioPublicacao] = '{4}' WHERE 
                    [CodigoWorkFlow] = {2} 

                SET @CodigoWorkflowPai_{5}  = {2}
                SET @{6}      = {2}
                ", dbBanco, dbOwner, hfCodigoWorkflow.Value.ToString(), CodigoFluxo, IdUsuarioLogado, idSubWorkflow, nomeSubWorkflow);

                comandoSQLUpdateWorkflow += string.Format(@"

                UPDATE {0}.{1}.Workflows
                SET CodigoEtapaInicial = {2}
                WHERE CodigoWorkflow = @CodigoWorkflowPai_{3}", dbBanco, dbOwner, idEtapaInicialWf, idSubWorkflow);

            }
            else
            {
                comandoSQLWorkflow += @"
                -- ----Sub Workflow";
                comandoSQLWorkflow += string.Format(@"
                INSERT INTO {0}.{1}.Workflows ([CodigoFluxo], [VersaoWorkflow], [VersaoFormatoXml], [DataCriacao], 
                    [IdentificadorUsuarioCriacao])
                VALUES ('{2}', '{3}', '{4}', GETDATE(), '{5}')

                DECLARE @CodigoWorkflowPai_{6} int
                DECLARE @{7}    int
                SET @CodigoWorkflowPai_{6}  = SCOPE_IDENTITY()
                SET @{7}      = SCOPE_IDENTITY()
                ", dbBanco, dbOwner, CodigoFluxo, versaoSubWorkflow, versaoFormatoXml, IdUsuarioLogado, idSubWorkflow, nomeSubWorkflow);

                comandoSQLUpdateWorkflow += string.Format(@"

                UPDATE {0}.{1}.Workflows
                SET CodigoEtapaInicial = {2}
                WHERE CodigoWorkflow = @CodigoWorkflowPai_{3}", dbBanco, dbOwner, idEtapaInicialWf, idSubWorkflow);

                comandoSQLSubWorkflow += string.Format(@"
                INSERT INTO {0}.{1}.SubWorkflows (CodigoWorkflow, CodigoEtapaWf, CodigoSubWorkflow)
                VALUES (@CodigoWorkflowPai_{2}, {3}, @{4})

                ", dbBanco, dbOwner, idWorkflowPai, idEtapaIniciadora, nomeSubWorkflow);
                //{0}             {1}               {2}
            }
            comandoSQLEtapa += @"
        -- --------[salvando ETAPAS

        ";
            foreach (XmlElement swf in listaSubWorkflows)
            {
                tipoElemento = swf.GetAttribute("tipoElemento");
                //----SALVAR ETAPAS (tipoElemento 1) ou DISJUNÇÃO (tipoElemento 4) ou DECISÃO (tipoElemento 7).
                if (tipoElemento == "1" || tipoElemento == "4" || tipoElemento == "7")
                {
                    string nameEtapa = string.Empty;
                    string name = string.Empty;
                    string timeoutValue = "NULL";
                    string timeoutUnit = "NULL";
                    string timeoutOffset = "'IE'";
                    string indicaOcultaBotoesDeAcao = "0";
                    string strCodigoSubfluxo;
                    int codigoSubfluxo = 0;
                    int inicioSubWorkflow, indicaEtapaDecisao;
                    string nomeEtapaReduzido = string.Empty;

                    if ("4" == tipoElemento) // se for uma disjunção
                    {
                        inicioSubWorkflow = 1;
                        nameEtapa = swf.GetAttribute("toolText");
                    }
                    else // se for uma etapa ou ... 
                    {
                        inicioSubWorkflow = 0;
                        name = swf.GetAttribute("name");
                        chiraPalavraAntesPunto(ref nameEtapa, name);
                    }

                    indicaEtapaDecisao = ("7" == tipoElemento) ? 1 : 0;

                    nomeEtapaReduzido = swf.GetAttribute("toolText");
                    idEtapaPai = swf.GetAttribute("id");
                    indicaOcultaBotoesDeAcao = swf.GetAttribute("ocultaBotoesAcao") == "1" ? "S" : "N";

                    // obtem o código do subfluxo, se for o caso
                    if (("4" == tipoElemento) || ("7" == tipoElemento))  // se for uma disjunção ou decisão
                    {
                        codigoSubfluxo = 0;
                        strCodigoSubfluxo = "NULL"; // atribuindo NULL 
                    }
                    else
                    {
                        strCodigoSubfluxo = swf.GetAttribute("codigoSubfluxo");
                        codigoSubfluxo = int.Parse(strCodigoSubfluxo);
                        if (codigoSubfluxo == 0)
                            strCodigoSubfluxo = "NULL"; // troca "0" por "NULL" caso a etapa não tenha um subfluxo associado.
                    }

                    // obtem a descrição da etapa
                    string descricao = formataParaCampoAlfabetico(obtemDescricaoEtapaFromXmlTela(idEtapaPai));

                    //------ obtenção do prazo previsto de uma etapa
                    auxNodeList = obtemPrazoPrevistoEtapaFromXmlTela(idEtapaPai);
                    foreach (XmlElement prazoPrevisto in auxNodeList)
                    {
                        timeoutValue = prazoPrevisto.GetAttribute("timeoutValue");
                        timeoutUnit = "'" + prazoPrevisto.GetAttribute("timeoutUnit") + "'";
                        timeoutOffset = "'" + prazoPrevisto.GetAttribute("timeoutOffset") + "'";

                        if (timeoutValue == "")
                            timeoutValue = "NULL";

                        if (timeoutUnit == "'minutos'")
                            timeoutUnit = "'mi'";
                        else if (timeoutUnit == "'horas'")
                            timeoutUnit = "'hh'";
                        else if (timeoutUnit == "'dias'")
                            timeoutUnit = "'dd'";
                        else if (timeoutUnit == "'diasuteis'")
                            timeoutUnit = "'du'";
                        else if (timeoutUnit == "'semanas'")
                            timeoutUnit = "'ww'";
                        else if (timeoutUnit == "'meses'")
                            timeoutUnit = "'mm'";
                        else
                            timeoutUnit = "NULL";

                        if (timeoutOffset == "'IETP'")
                            timeoutOffset = "'IE'";
                        else if (timeoutOffset == "'IFLX'")
                            timeoutOffset = "'IF'";
                    }

                    comandoSQLEtapa += string.Format(@"
                INSERT INTO {0}.{1}.EtapasWf (CodigoWorkflow, CodigoEtapawf, NomeEtapawf, DescricaoEtapa, InicioSubWorkflow, ValorTimeoutEtapa, UnidadeMedidaTimeout, NomeEtapaReduzidoWf, IndicaDataCalculo, IndicaOcultaBotoesAcao, CodigoFluxoSubprocesso, IndicaEtapaDecisao)
                VALUES ( @{5}, {2}, '{3}', {4}, {6}, {7}, {8}, '{9}', {10}, '{11}', {12}, {13})

                    ", dbBanco, dbOwner, idEtapaPai, nameEtapa.Trim().Replace("'", "''"), descricao, nomeSubWorkflow, inicioSubWorkflow, timeoutValue, timeoutUnit, nomeEtapaReduzido.Replace("'", "''"), timeoutOffset, indicaOcultaBotoesDeAcao, strCodigoSubfluxo, indicaEtapaDecisao);

                    //------ Gravação dos formulários e grupos de acessos quando se tratando de uma ETAPA
                    if ((tipoElemento == "1") && (codigoSubfluxo == 0))
                    {
                        string ordemFormulario = "0"; //todo: definir ordem do formulário no XML -> checar se a ordem incluída na grid se mantém

                        XmlNodeList formularios = obtemFormulariosEtapaFromXmlTela(idEtapaPai);
                        foreach (XmlElement formulario in formularios)
                        {
                            string idFormulario = formulario.GetAttribute("id");
                            string tituloFormulario = formulario.GetAttribute("title");

                            string readOnly = (formulario.GetAttribute("readOnly") == "1" ? "R" : "W");
                            //                            ordemFormulario++; // todo: definir no XML

                            string required = (formulario.GetAttribute("required") == "1" ? "1" : "0");

                            string newOnEachOcorrence = formulario.GetAttribute("newOnEachOcurrence");
                            if ("1" != newOnEachOcorrence) // aceita somente 1 ou 0
                                newOnEachOcorrence = "0";

                            string requerAssinaturaDigital = formulario.GetAttribute("requerAssinaturaDigital");                // CERTDIG
                            if ("1" != requerAssinaturaDigital) // aceita somente 1 ou 0
                                requerAssinaturaDigital = "0";

                            ordemFormulario = formulario.GetAttribute("ordemFormularioEtapa");                 // OrdemForm
                            string originalStageId = formulario.GetAttribute("originalStageId");
                            string originalWorkflow;

                            if (("" == originalStageId) || ("0" == originalStageId))
                            {
                                originalStageId = "NULL";
                                originalWorkflow = "NULL";
                            }
                            else
                            {
                                originalWorkflow = string.Format(@"@CodigoWorkflowPai_{0}",
                                obtemGrupoWorkflowEtapaFromXmlTela(originalStageId));
                            }

                            comandoSQLEformulario += string.Format(@"
                    INSERT INTO {0}.{1}.FormulariosEtapasWf (CodigoWorkflow, CodigoEtapaWf, CodigoModeloFormulario, TituloFormulario, TipoAcessoFormulario, PreenchimentoObrigatorio, 
                                                     OrdemFormularioEtapa, NovoCadaOcorrencia, RequerAssinaturaDigital, CodigoWorkflowOrigemFormulario, CodigoEtapaWfOrigemFormulario)
                    VALUES (@{12}, {2}, {3}, '{4}', '{5}', {6}, {7}, {8}, {9}, {10}, {11})
                                
                    ", dbBanco, dbOwner, idEtapaPai, idFormulario, tituloFormulario.Replace("'", "''"), readOnly, required, ordemFormulario, newOnEachOcorrence, requerAssinaturaDigital, originalWorkflow, originalStageId, nomeSubWorkflow);    // CERTDIG
                        } // foreach (XmlElement formulario  in formularios)

                        //------[ filho: gruposComAcesso

                        XmlNodeList gruposDeAcessos = obtemGruposDeAcessosEtapaFromXmlTela(idEtapaPai);
                        foreach (XmlElement grupo in gruposDeAcessos)
                        {
                            string idgrupo = grupo.GetAttribute("id");
                            string acesso = grupo.GetAttribute("accessType").ToString().Substring(0, 1);

                            if (grupo.GetAttribute("accessType").ToString() == Resources.traducao.adm_edicaoWorkflows_a__o)
                            {
                                acesso = "A";
                            }
                            else
                            {
                                acesso = "V";
                            }

                            comandoSQLAcesso += string.Format(@"
                        INSERT INTO {0}.{1}.AcessosEtapasWf (CodigoWorkflow, CodigoEtapaWf, CodigoPerfilWf, TipoAcesso)
                        VALUES (@{5}, {2}, {3}, '{4}')

                        ", dbBanco, dbOwner, idEtapaPai, idgrupo, acesso, nomeSubWorkflow);
                        }
                    } //fim if(tipoElemento==1)

                    //------ Gravação das ações da ETAPA

                    // Se for uma disjunção, as ações a serem gravadas neste tipo de etapa são 
                    // as ações existentes no elemento junção que termina o subfluxo iniciada pela disjunção
                    if (tipoElemento == "4")
                        acoes = obtemAcoesJuncao(idEtapaPai);
                    else
                        acoes = obtemAcaoEtapaFromXmlTela(idEtapaPai);

                    if (acoes.Count > 0)
                    {
                        comandoSQLAcoes += "SET @CodigoAcaoWf = 0";
                        foreach (XmlElement acao in acoes)
                        {
                            string textoAcao = acao.GetAttribute("id");
                            string CodigoNovaEtapa = acao.GetAttribute("nextStageId");
                            string tipoNotificacao = "E";
                            string tipoAcao = acao.GetAttribute("actionType");
                            string valorTimeoutAcao = (tipoAcao == "T" ? acao.GetAttribute("timeoutValue") : "0");
                            string indiceCondicao = acao.GetAttribute("conditionIndex");
                            string condicaoExtenso = acao.GetAttribute("condition");
                            string condicaoAvaliada = acao.GetAttribute("decodedCondition");

                            if (CodigoNovaEtapa.Length == 0)
                                CodigoNovaEtapa = "NULL";

                            if (indiceCondicao.Length == 0)
                                indiceCondicao = "NULL";

                            if (condicaoExtenso.Length == 0)
                                condicaoExtenso = "NULL";
                            else
                                condicaoExtenso = "'" + condicaoExtenso.Replace("'", "''") + "'";

                            if (condicaoAvaliada.Length == 0)
                                condicaoAvaliada = "NULL";
                            else
                                condicaoAvaliada = "'" + condicaoAvaliada.Replace("'", "''") + "'";

                            string solicitaAssinaturaDigital = acao.GetAttribute("solicitaAssinaturaDigital");
                            if ("1" != solicitaAssinaturaDigital) // aceita somente 1 ou 0
                                solicitaAssinaturaDigital = "0";

                            string corBotao = acao.GetAttribute("corBotao");
                            string iconeBotao = acao.GetAttribute("iconeBotao");

                            string UnidadeMedidaTimeout = (tipoAcao == "T" ? "'" + acao.GetAttribute("timeoutUnit") + "'" : "NULL");
                            if (UnidadeMedidaTimeout == "'" + Resources.traducao.adm_edicaoWorkflows_minutos + "'")
                                UnidadeMedidaTimeout = "'mi'";
                            if (UnidadeMedidaTimeout == "'" + Resources.traducao.adm_edicaoWorkflows_horas + "'")
                                UnidadeMedidaTimeout = "'hh'";
                            if (UnidadeMedidaTimeout == "'" + Resources.traducao.adm_edicaoWorkflows_dias + "'")
                                UnidadeMedidaTimeout = "'dd'";
                            if (UnidadeMedidaTimeout == "'" + Resources.traducao.adm_edicaoWorkflows_semanas + "'")
                                UnidadeMedidaTimeout = "'ww'";
                            if (UnidadeMedidaTimeout == "'" + Resources.traducao.adm_edicaoWorkflows_meses + "'")
                                UnidadeMedidaTimeout = "'mm'";

                            string TextoNotificacao = formataParaCampoAlfabetico(obtemInfoNodeFromAcao(acao, "textoNotificacao"));
                            string AssuntoNotificacao = formataParaCampoAlfabetico(obtemInfoNodeFromAcao(acao, "assuntoNotificacao"));
                            string TextoNotificacao2 = formataParaCampoAlfabetico(obtemInfoNodeFromAcao(acao, "textoNotificacao2"));
                            string AssuntoNotificacao2 = formataParaCampoAlfabetico(obtemInfoNodeFromAcao(acao, "assuntoNotificacao2"));

                            // Se for uma disjunção, as ações serão do tipo "S" (fim de subfluxo)
                            if ((tipoElemento == "4") || (codigoSubfluxo > 0))
                            {
                                textoAcao = "Fim de subfluxo";
                                tipoAcao = "S";
                            }

                            if (textoAcao != "")
                            {
                                comandoSQLAcoes += string.Format(@"
                            -- --------[salvando ACOES das Etapas 

                            SET @CodigoAcaoWf = @CodigoAcaoWf + 1

                            INSERT INTO {0}.{1}.AcoesEtapasWf (Codigoworkflow, CodigoEtapaWf, CodigoAcaoWf, TextoAcao, CodigoNovaEtapa, TipoNotificacao, TipoAcao, 
                                                    ValorTimeoutAcao, UnidadeMedidaTimeout, TextoNotificacao, AssuntoNotificacao, TextoNotificacao2, AssuntoNotificacao2, 
                                                    SolicitaAssinaturaDigital, corBotao, IconeBotao, IndiceExpressaoCondicional, ExpressaoCondicionalExtenso, ExpressaoCondicionalAvaliada)
                            VALUES (@{10}, {2}, @CodigoAcaoWf, '{3}', {4}, '{5}', '{6}', {7}, {8}, {9},{11},{12},{13},{14}, '{15}', '{16}', {17}, {18}, {19} )

                            ", dbBanco, dbOwner, idEtapaPai, textoAcao.Replace("'", "''"), CodigoNovaEtapa, tipoNotificacao, tipoAcao, valorTimeoutAcao, UnidadeMedidaTimeout, TextoNotificacao, nomeSubWorkflow, AssuntoNotificacao, TextoNotificacao2, AssuntoNotificacao2, solicitaAssinaturaDigital, corBotao, iconeBotao, indiceCondicao, condicaoExtenso, condicaoAvaliada);
                                //{0}     {1}       {2}         {3}          {4}             {5}             {6}         {7}                {8}                   {9}               {10}          {11}               {12}               {13}                   {14}                      {15}     {16}
                            }

                            //------[ filho: acoes/gruposNotificados
                            comandoSQLAcoes += string.Format(@"
                             ---- --------[salvando REGRAS NOTIFICACOES das Acoes  de Etapas .

                            ");

                            XmlNodeList gruposNotificacoes = acao.SelectNodes("gruposNotificados/grupo");
                            // XmlNodeList listaFilhoSetsGruposNotificados = obtemListaFilhosSetFromXmlTela(idEtapaPai, string.Format("acoes/acao[@id='{0}']/gruposNotificados/grupo", textoAcao));
                            foreach (XmlElement grupo in gruposNotificacoes)
                            {
                                string NomeTipoGrupo = grupo.GetAttribute("id");
                                string caixaNotifica = (grupo.GetAttribute("msgBox") == Resources.traducao.adm_edicaoWorkflows_acompanhamento ? "S" : "E");

                                comandoSQLAcoes += string.Format(@"
                            INSERT INTO {0}.{1}.RegrasNotificacoesRecursosWf (CodigoWorkflow, CodigoEtapaWf, CodigoAcaoWf, CodigoPerfilWf, CaixaNotificacao)
                            VALUES (@{5}, {2}, @CodigoAcaoWf, {3}, '{4}')

                            ", dbBanco, dbOwner, idEtapaPai, NomeTipoGrupo, caixaNotifica, nomeSubWorkflow);
                            }

                            //------[ filho: acoes/acoesAutomaticas
                            comandoSQLAcoes += string.Format(@"
                            ---- --------[salvando ACOES AUTOMATICAS das Acoes  de Etapas .

                            ");

                            XmlNodeList acoesAutomaticas = acao.SelectNodes("acoesAutomaticas/acaoAutomatica");
                            // XmlNodeList listaFilhoSetsAcoesAutomaticas = obtemListaFilhosSetFromXmlTela(idEtapaPai, string.Format("acoes/acao[@id='{0}']/acoesAutomaticas/acaoAutomatica", textoAcao));
                            foreach (XmlElement acaoAutomatica in acoesAutomaticas)
                            {
                                string acaoAutomaticaId = acaoAutomatica.GetAttribute("id");

                                comandoSQLAcoes += string.Format(@"
                                INSERT INTO {0}.{1}.AcoesAutomaticasEtapasWf (CodigoWorkflow, CodigoEtapaWf, CodigoAcaoWf, CodigoAcaoAutomaticaWf)
                                VALUES (@{4}, {2}, @CodigoAcaoWf, {3})
                            
                                ", dbBanco, dbOwner, idEtapaPai, acaoAutomaticaId, nomeSubWorkflow);
                            } //-[Fim foreach automaticas

                            XmlNodeList acionamentosAPIs = acao.SelectNodes("acionamentosAPIs/acionamentoAPI");
                            
                            // XmlNodeList listaFilhoSetsAcoesAutomaticas = obtemListaFilhosSetFromXmlTela(idEtapaPai, string.Format("acoes/acao[@id='{0}']/acoesAutomaticas/acaoAutomatica", textoAcao));
                            if (acionamentosAPIs.Count > 0)
                            {
                                //------[ filho: acionamentosAPIs/acionamentoAPI
                                comandoSQLAcoes += string.Format(@"
                                -------------[salvando Acionamentos API dos conectores .
                                ");

                                foreach (XmlElement acionamentoAPI in acionamentosAPIs)
                                {
                                    string acionamentoAutomaticoID = acionamentoAPI.GetAttribute("id");
                                    string sequenciaAcionamento = acionamentoAPI.GetAttribute("activationSequence");
                                    string urlApiAcionamento = acionamentoAPI.GetAttribute("webServiceURL");
                                    string indicaAcionamentoAtivo = acionamentoAPI.GetAttribute("enabled").Trim().ToLower() == "true" ? "S" : "N";
                                    comandoSQLAcoes += string.Format(@"
                                    INSERT INTO {0}.{1}.AcionamentosAPIEtapasWf (CodigoWorkflow, CodigoEtapaWf,  CodigoAcaoWf, SequenciaAcionamento, UrlApiAcionamento, IndicaAcionamentoAtivo)
                                                                         VALUES (          @{4},           {2}, @CodigoAcaoWf,                  {3},             '{5}',                 '{6}')
                            
                                     SET @IDConfiguracaoAcionamento = SCOPE_IDENTITY()
                                    ", dbBanco, dbOwner, idEtapaPai, sequenciaAcionamento, nomeSubWorkflow, urlApiAcionamento, indicaAcionamentoAtivo);

                                    XmlNodeList parametrosAPI = acionamentoAPI.SelectNodes("parametrosAPI/parametroAPI");
                                    foreach (XmlElement parametroAPI in parametrosAPI)
                                    {
                                        string IdentificacaoParametro = parametroAPI.GetAttribute("parameter");
                                        string TipoDadoParametro = parametroAPI.GetAttribute("dataType");
                                        string TipoEnvioParametro = parametroAPI.GetAttribute("httpPart").Substring(0, 1);
                                        string IndicaEnvioValorNulo = parametroAPI.GetAttribute("sendNull").ToLower().Trim() == "true" ? "S" : "N"; 
                                        string ValorParametro = parametroAPI.GetAttribute("value");
                                        comandoSQLAcoes += string.Format(@"
                                    INSERT INTO [ParametrosAcionamentosAPIEtapasWF] ([IDConfiguracaoAcionamento], [IdentificacaoParametro], [TipoDadoParametro], [TipoEnvioParametro], [IndicaEnvioValorNulo], [ValorParametro])
                                                                              VALUES(@IDConfiguracaoAcionamento ,                    '{0}',               '{1}',               '{2}' ,                 '{3}' ,'{4}')",
                                                                              IdentificacaoParametro, TipoDadoParametro, TipoEnvioParametro, IndicaEnvioValorNulo, ValorParametro);

                                    }

                                } //-[Fim ACIONAMENTOS API
                            }
                        } // foreach (XmlElement acao in acoes)
                    } // if (acoes.Count > 0)
                } // if (tipoElemento == "1" || tipoElemento == "4")
            } //foreach (XmlElement swf in listaSubWorkflows)
        } // for (int i = 0; i < gruposWf.Count; i++)

        comandoSQL += comandoSQLWorkflow + comandoSQLEtapa + comandoSQLSubWorkflow + comandoSQLEformulario + comandoSQLAcesso + comandoSQLAcoes + comandoSQLUpdateWorkflow;
        return comandoSQL;
    }

    //
    private string geraInstrucaoPublicacaoAcaoWorkflow()
    {
        string dbBanco = cDados.getDbName();
        string dbOwner = cDados.getDbOwner();
        string comandoSQLEtapa = string.Empty;
        string comandoSQLAcoes = string.Empty;
        string comandoSQL = string.Empty;
        string idEtapaPai = string.Empty;
        string nameEtapa = string.Empty;
        string descricao = string.Empty;
        string timeoutValue = "NULL";
        string timeoutUnit = "NULL";
        string nomeSubWorkflow = "CodigoWorkflowPai_1";
        int inicioSubWorkflow = 0;

        XmlNodeList acoes, listaSubWorkflows;

        listaSubWorkflows = obtemListaElementosXmlTelaPorAtributo("grupoWorkflow", "0");
        foreach (XmlElement swf in listaSubWorkflows)
        {
            idEtapaPai = swf.GetAttribute("id");
            nameEtapa = swf.GetAttribute("toolText").Trim().Replace("'", "''");
            descricao = formataParaCampoAlfabetico(obtemDescricaoEtapaFromXmlTela(idEtapaPai));

            comandoSQLEtapa += string.Format(@"
        INSERT INTO {0}.{1}.EtapasWf (CodigoWorkflow, CodigoEtapawf, NomeEtapawf, DescricaoEtapa, InicioSubWorkflow, ValorTimeoutEtapa, UnidadeMedidaTimeout, nomeEtapaReduzidoWf, IndicaDataCalculo)
        VALUES ( @{5}, {2}, '{3}', {4}, {6}, {7}, {8}, '{3}', 'IE')

        ", dbBanco, dbOwner, idEtapaPai, nameEtapa, descricao, nomeSubWorkflow, inicioSubWorkflow, timeoutValue, timeoutUnit);

            acoes = obtemAcaoEtapaFromXmlTela(idEtapaPai);

            if (acoes.Count > 0)
            {
                foreach (XmlElement acao in acoes)
                {
                    string tipoAcao = acao.GetAttribute("actionType").ToString();
                    string textoAcao = acao.GetAttribute("id").ToString();
                    string CodigoNovaEtapa = (acao.GetAttribute("nextStageId").ToString() == "" ? "NULL" : acao.GetAttribute("nextStageId").ToString());

                    string TextoNotificacao = formataParaCampoAlfabetico(obtemInfoNodeFromAcao(acao, "textoNotificacao"));
                    string AssuntoNotificacao = formataParaCampoAlfabetico(obtemInfoNodeFromAcao(acao, "assuntoNotificacao"));
                    string TextoNotificacao2 = formataParaCampoAlfabetico(obtemInfoNodeFromAcao(acao, "textoNotificacao2"));
                    string AssuntoNotificacao2 = formataParaCampoAlfabetico(obtemInfoNodeFromAcao(acao, "assuntoNotificacao2"));

                    XmlNodeList gruposNotificacoes = acao.SelectNodes("gruposNotificados/grupo");
                    XmlNodeList acoesAutomaticas = acao.SelectNodes("acoesAutomaticas/acaoAutomatica");

                    if ((gruposNotificacoes.Count > 0) || (acoesAutomaticas.Count > 0))
                    {
                        comandoSQLAcoes += string.Format(@"
                            -- --------[salvando ACOES das Etapas 

                        SELECT @CodigoAcaoWf = CASE '{6}' WHEN 'Cancelamento' THEN 2 ELSE 1 END

                        INSERT INTO {0}.{1}.AcoesEtapasWf (Codigoworkflow, CodigoEtapaWf, CodigoAcaoWf, TextoAcao, CodigoNovaEtapa, TipoNotificacao, TipoAcao, 
                                                ValorTimeoutAcao, UnidadeMedidaTimeout, TextoNotificacao, AssuntoNotificacao, TextoNotificacao2, AssuntoNotificacao2)
                        VALUES (@{3}, 0, @CodigoAcaoWf, '{6}', NULL, 'E', '{5}', NULL, NULL, {2},{4}, {7}, {8})

                        ", dbBanco, dbOwner, TextoNotificacao, nomeSubWorkflow, AssuntoNotificacao, tipoAcao, textoAcao, TextoNotificacao2, AssuntoNotificacao2);

                        //------[ filho: acoes/gruposNotificados
                        comandoSQLAcoes += string.Format(@"
                        ---- --------[salvando REGRAS NOTIFICACOES das Acoes  de Etapas .

                        ");

                        foreach (XmlElement grupo in gruposNotificacoes)
                        {
                            string NomeTipoGrupo = grupo.GetAttribute("id").ToString();
                            string caixaNotifica = (grupo.GetAttribute("msgBox").ToString() == Resources.traducao.adm_edicaoWorkflows_acompanhamento ? "S" : "E");

                            comandoSQLAcoes += string.Format(@"
                            INSERT INTO {0}.{1}.RegrasNotificacoesRecursosWf (CodigoWorkflow, CodigoEtapaWf, CodigoAcaoWf, CodigoPerfilWf, CaixaNotificacao)
                            VALUES (@{5}, {2}, @CodigoAcaoWf, {3}, '{4}')

                            ", dbBanco, dbOwner, idEtapaPai, NomeTipoGrupo, caixaNotifica, nomeSubWorkflow);
                        }

                        //------[ filho: acoes/acoesAutomaticas
                        comandoSQLAcoes += string.Format(@"
                        ---- --------[salvando ACOES AUTOMATICAS das Acoes  de Etapas .

                        ");


                        // XmlNodeList listaFilhoSetsAcoesAutomaticas = obtemListaFilhosSetFromXmlTela(idEtapaPai, string.Format("acoes/acao[@id='{0}']/acoesAutomaticas/acaoAutomatica", textoAcao));
                        foreach (XmlElement acaoAutomatica in acoesAutomaticas)
                        {
                            string acaoAutomaticaId = acaoAutomatica.GetAttribute("id").ToString();

                            comandoSQLAcoes += string.Format(@"
                            INSERT INTO {0}.{1}.AcoesAutomaticasEtapasWf (CodigoWorkflow, CodigoEtapaWf, CodigoAcaoWf, CodigoAcaoAutomaticaWf)
                            VALUES (@{4}, {2}, @CodigoAcaoWf, {3})
                        
                            ", dbBanco, dbOwner, idEtapaPai, acaoAutomaticaId, nomeSubWorkflow);
                        } //-[Fim foreach automaticas
                    } //  if ((null != TextoNotificacao && "" != TextoNotificacao) && ...
                } // if (acoes.Count > 0)
            }
        }
        comandoSQL += comandoSQLEtapa + comandoSQLAcoes;
        return comandoSQL;
    }

    private string formataParaCampoAlfabetico(string valor)
    {
        string retorno;
        if (0 == valor.Length)
            retorno = "NULL";
        else
            retorno = string.Format("'{0}'", valor.Replace("'", "''"));

        return retorno;
    }

    #region manipulação XML

    private XmlNodeList obtemListaTipoElementoXMLTela(string tipoElemento)
    {
        return obtemListaTipoElementoXMLTela(tipoElemento, false);
    }

    /// <summary>
    /// Função para retornar uma lista de elementos contidos no xml da tela.
    /// </summary>
    /// <remarks>
    /// Se o parâmetro <paramref name="bExcluiTipo"/> for true, será devolvida a lista dos 
    /// tipos diferentes do indicado no parâmetro.
    /// </remarks>
    /// <param name="tipoElemento">tipo do elemento a comparar</param>
    /// <param name="bExcluiTipo">indica se será devolvida a lista excluindo o tipo ou incluindo</param>
    /// <returns></returns>
    private XmlNodeList obtemListaTipoElementoXMLTela(string tipoElemento, bool bExcluiTipo)
    {
        if (bExcluiTipo)
            return obtemListaElementosXmlTelaPorAtributoDiferente("tipoElemento", tipoElemento);
        else
            return obtemListaElementosXmlTelaPorAtributo("tipoElemento", tipoElemento);
    }

    private XmlNodeList obtemListaElementosXmlTelaPorId(string id)
    {
        return obtemListaElementosXmlTelaPorAtributo("id", id);
    }

    private XmlNodeList obtemListaElementosXmlTelaPorAtributo(string atributo, string valorAtributo)
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList lista = xmlDoc.SelectNodes(string.Format("/chart/dataSet/set[@{0}='{1}']", atributo, valorAtributo));
        return lista;
    }

    private XmlNodeList obtemListaElementosXmlTelaPorAtributoDiferente(string atributo, string valorAtributo)
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList lista = xmlDoc.SelectNodes(string.Format("/chart/dataSet/set[@{0}!='{1}']", atributo, valorAtributo));
        return lista;
    }

    /// <summary>
    /// Obtem a lista de ações de uma junção dado um determinado elemento que inicia o subfluxo que a junção encerra
    /// </summary>
    /// <<param name="idElementoIniciofluxo">id do elemento SET que inicia o subfluxo que a junção encerra</param>
    /// <returns></returns>
    private XmlNodeList obtemAcoesJuncao(string idElementoIniciofluxo)
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList dataFilhos = xmlDoc.SelectNodes(string.Format("/chart/dataSet/set[@idElementoInicioFluxo={0} and @tipoElemento=5]/acoes/acao", idElementoIniciofluxo));
        return dataFilhos;
    }

    private XmlNodeList obtemDisjuncaoWorkflowFromXmlTela(string idDisjuncao)
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList dataFilhos = xmlDoc.SelectNodes(string.Format("/chart/dataset/set[@tipoElemento=5 and @id={0}]/toolText", idDisjuncao));
        return dataFilhos;
    }

    /// <summary>
    /// Obtem o codigo de grupo o qual pertenece a etapa.
    /// </summary>
    /// <param name="etapaId">Id da etapa ao pesquiçar o grupo.</param>
    /// <returns>Retorna string : conteudo de o ID do grupo || caso que etapaId nao exista retorna vacio ("")</returns>
    private string obtemGrupoWorkflowEtapaFromXmlTela(string etapaId)
    {
        string grupoWorkflow = string.Empty;
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList list = xmlDoc.SelectNodes(string.Format("/chart/dataSet/set[@id='{0}']", etapaId));

        if (list.Count > 0)
        {
            XmlNode elemento = list[0];
            grupoWorkflow = elemento.Attributes["grupoWorkflow"].Value.ToString();
        }
        return grupoWorkflow;
    }

    private XmlNodeList obtemListaConnectorsFromXmlTela()
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList dataSets = xmlDoc.GetElementsByTagName("connector");
        return dataSets;
    }

    private XmlNodeList obtemListaSubWorkflowsFromXmlTela()
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList workflows = xmlDoc.GetElementsByTagName("subworkflow");
        return workflows;
    }

    private string getVersaoFormatoXmlFromXmlTela()
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList lista = xmlDoc.GetElementsByTagName("workflows");
        string versao = string.Empty;

        foreach (XmlElement elemento in lista)
        {
            versao = elemento.GetAttribute("xmlVersion");
        }
        return versao;
    }

    private void setVersaoFormatoXmlToXmlTela(XmlDocument xmlDoc, string versaoFormatoXml)
    {
        XmlNodeList lista = xmlDoc.GetElementsByTagName("workflows");

        foreach (XmlElement elemento in lista)
        {
            elemento.SetAttribute("xmlVersion", versaoFormatoXml);
        }
    }

    private void setWidhtHeightXmlToXmlTela(string widhtXml, string heightXml)
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList lista = xmlDoc.GetElementsByTagName("workflows");

        foreach (XmlElement elemento in lista)
        {
            elemento.SetAttribute("width", widhtXml);
            elemento.SetAttribute("height", heightXml);
        }

        hfValoresTela.Set("AlturaDivFlash", heightXml);
        hfValoresTela.Set("LarguraDivFlash", widhtXml);

        __strXmlWorkflow = getXmlAsStringFromXmlDoc(xmlDoc);
        atualizaXmlHiddenField(__strXmlWorkflow);
    }

    private string obtemDescricaoEtapaFromXmlTela(string idEtapa)
    {

        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList descricoes = obtemSetSubNodes(idEtapa, "descricao");
        string descricao = string.Empty;

        if ((descricoes.Item(0) != null) && (descricoes.Item(0).FirstChild != null))
        {
            descricao = descricoes.Item(0).FirstChild.Value.ToString();
        }
        return descricao;
    }

    private XmlNodeList obtemPrazoPrevistoEtapaFromXmlTela(string idEtapa)
    {
        return obtemSetSubNodes(idEtapa, "prazoPrevisto");
    }

    private XmlNodeList obtemFormulariosEtapaFromXmlTela(string idEtapa)
    {
        /*
                int ordemForm;
                int minOrdem;
                XmlDocument xmlForm;
                XmlNode nodeForm;
                XmlNodeList listFormsOrdenados;

                XmlDocument xmlDoc = obtemXmlDocFromXmlTela();


                XPathExpression expr = "/chart/dataSet/set[@id='{0}']/{1}";

                    expr.AddSort("ordemFormularioEtapa", XmlSortOrder.Ascending,
                                   XmlCaseOrder.None, "", XmlDataType.Number);



                XmlNodeList lista = xmlDoc.SelectNodes(string.Format("/chart/dataSet/set[@id='{0}']/{1}", setID, subNodesPath));


        //        IEnumerator ienumlistFormOrdenados = listFormsOrdenados.Item.
                XmlNodeList lista = obtemSetSubNodes(idEtapa, "formularios/formulario");
                IEnumerator ienumlista = lista.GetEnumerator();
                while (ienumlista.MoveNext())
                {
                    minOrdem = 999999;
                    while (ienumlista.MoveNext())
                    {
                        XmlNode form = (XmlNode)ienumlista.Current;
                        if (!int.TryParse(form.Attributes.GetNamedItem("OrdemFormularioEtapa").Value, out ordemForm))
                            ordemForm = 0;
                        if (ordemForm < minOrdem)
                        {
                            nodeForm = form;
                            minOrdem = ordemForm;
                        }
                    }
                }
        */

        XmlNodeList lista = obtemSetSubNodes(idEtapa, "formularios/formulario");
        return lista;
    }

    private XmlNodeList obtemGruposDeAcessosEtapaFromXmlTela(string idEtapa)
    {
        return obtemSetSubNodes(idEtapa, "gruposComAcesso/grupo");
    }

    private XmlNode obtemAcaoEtapaFromXmlTela(string idEtapa, string textoAcao)
    {
        XmlNodeList lst = obtemSetSubNodes(idEtapa, string.Format("acoes/acao[@id='{0}']", textoAcao));
        if ((null != lst) && (lst.Count > 0))
            return lst.Item(0);
        else
            return null;
    }

    /// <summary>
    /// Devolve uma lista de 'subnós' de um elemento do tipo SET do xml de workflow.
    /// </summary>
    /// <remarks>
    /// Esta função devolve uma lista de 'subnós' de um elemento do tipo SET do xml de workflow de acordo 
    /// com o caminho do subnó informado.
    /// </remarks>
    /// <param name="setID">Id do elemento SET do qual se quer os 'subnós'</param>
    /// <param name="subNodesPath">Caminho, dentro do nó da etapa, identificando os subnós a serem devolvidos</param>
    /// <returns></returns>
    private XmlNodeList obtemSetSubNodes(string setID, string subNodesPath)
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList lista = xmlDoc.SelectNodes(string.Format("/chart/dataSet/set[@id='{0}']/{1}", setID, subNodesPath));
        return lista;
    }

    private string obtemNomeEtapaFromXmlTela(string etapaId)
    {
        XmlElement etapa;
        etapa = (XmlElement)obtemListaElementosXmlTelaPorId(etapaId).Item(0);
        return obtemNomeEtapaFromXmlTela(etapa);
    }

    private string obtemNomeEtapaFromXmlTela(XmlElement etapa)
    {
        string nomeEtapa = string.Empty;

        if (null != etapa)
        {

            string tipo = etapa.GetAttribute("tipoElemento");

            if (tipo.Equals("1"))  // se for do tipo Etapa, pega o atributo name
                nomeEtapa = etapa.GetAttribute("name").ToString();
            else if (!tipo.Equals("3")) // se não for etapa, nem timer, pega o toolText. (timer fica sem nome)
                nomeEtapa = etapa.GetAttribute("toolText").ToString();
        }

        return nomeEtapa;
    }

    private XmlNodeList obtemListaDataSetFromXmlTela()
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList dataSets = xmlDoc.SelectNodes("/chart/dataSet/set");//xmlDoc.GetElementsByTagName("set");
        return dataSets;
    }

    private XmlNodeList obtemAcaoEtapaFromXmlTela(string etapaId)
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList dataAcoes = xmlDoc.SelectNodes(string.Format("/chart/dataSet/set[@id='{0}']/acoes/acao", etapaId));
        return dataAcoes;
    }

    private XmlNodeList obtemAcoesEtapaFromXmlTela(string etapaId)
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList dataAcoes = xmlDoc.SelectNodes(string.Format("/chart/dataSet/set[@id='{0}']/acoes", etapaId));
        return dataAcoes;
    }

    private XmlNodeList obtemListaTimerFromXmlTela(string etapaId)
    {
        XmlDocument xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList dataAcoes = xmlDoc.SelectNodes(string.Format("/chart/dataSet/set[@id='{0}']/acoes/acao[@actionType='T']", etapaId));
        return dataAcoes;
    }

    //todo: problemas com o firefox nesta linea, aonde fala que 'A chave fornecida não estava presente no dicionário'.
    private XmlDocument obtemXmlDocFromXmlTela()
    {
        string strXml = "";

        if (hfValoresTela.Contains("XMLWF"))
        {
            strXml = hfValoresTela.Get("XMLWF").ToString();
        }

        strXml = strXml.Replace("CondiÃ§Ã£o", "Condição");

        if (!strXml.Equals(__strXmlWorkflow, StringComparison.Ordinal))
        {
            XmlReader xmlR = XmlReader.Create(new StringReader(strXml));
            __xmlDoc.Load(xmlR);
            __strXmlWorkflow = strXml;
        }

        return __xmlDoc;
    }

    private string obtemIdFromTimer(string idTimer)
    {
        string idFrom = string.Empty;
        XmlNodeList listaConnector = obtemListaConnectorsFromXmlTela();

        foreach (XmlElement elemento in listaConnector)
        {
            string from = elemento.Attributes["from"].Value.ToString();
            string timer = elemento.Attributes["to"].Value.ToString();

            if (timer == idTimer)
                idFrom = from;
        }
        return idFrom;
    }

    private string obtemIdToTimer(string idTimer)
    {
        string idTo = string.Empty;
        XmlNodeList listaConnector = obtemListaConnectorsFromXmlTela();

        foreach (XmlElement elemento in listaConnector)
        {
            string to = elemento.Attributes["to"].Value.ToString();
            string timer = elemento.Attributes["from"].Value.ToString();

            if (timer == idTimer)
                idTo = to;
        }
        return idTo;
    }

    #endregion

    #endregion

    protected void gv_ParametrosAcionamentos_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "sendNull")
        {
            ASPxCheckBox c = (ASPxCheckBox)e.Editor;
            c.ClientSideEvents.CheckedChanged = @"function(s, e) {
	            memoValor.SetReadOnly(s.GetChecked());
                if(s.GetChecked() == true)
                {
                     memoValor.SetText('');
                }
            }";
        }

        if (e.Column.FieldName == "value")
        {
            object sendNull = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "sendNull");
            if( (sendNull != null) && (sendNull.ToString().ToLower() == "true"))
            {
                ASPxMemo memo = (ASPxMemo)e.Editor;
                memo.ClientSideEvents.Init = @"function(s, e) {s.SetReadOnly(true);}";                
            }
        }
    }

    protected void gv_ParametrosAcionamentos_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        DataSet dsParametrosAcionamentosAPI = ((DataSet)Session["dsParametrosAcionamentosAPI"]).Copy();

        foreach (DataRow dr1 in dsParametrosAcionamentosAPI.Tables[0].Rows)
        {
            if (dr1["IdKey"] + "" == e.Keys[0] + "")
            {
                dr1.Delete();
                dsParametrosAcionamentosAPI.Tables[0].AcceptChanges();
                break;
            }
        }
        ((ASPxGridView)(sender)).DataSource = dsParametrosAcionamentosAPI;
        ((ASPxGridView)(sender)).DataBind();
        ((ASPxGridView)(sender)).CancelEdit();
        Session["dsParametrosAcionamentosAPI"] = dsParametrosAcionamentosAPI;
        e.Cancel = true;       
    }

    protected void gv_ParametrosAcionamentos_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataSet dsParametrosAcionamentosAPI = ((DataSet)Session["dsParametrosAcionamentosAPI"]).Copy();

        DataRow dr = dsParametrosAcionamentosAPI.Tables[0].NewRow();   //dsAcionamentos.Tables[0].Compute("max(Id)", null).ToString()

        object auxId = gv_Acionamentos.GetRowValues(gv_Acionamentos.FocusedRowIndex, gv_Acionamentos.KeyFieldName);
        dr["Id"] = auxId;

        Guid novaKey = Guid.NewGuid();

        dr["IdKey"] = novaKey.ToString();
        dr["parameter"] = e.NewValues["parameter"];
        dr["dataType"] = e.NewValues["dataType"];
        dr["httpPart"] = e.NewValues["httpPart"];
        dr["sendNull"] = e.NewValues["sendNull"] == null ? false : e.NewValues["sendNull"];
        dr["value"] = e.NewValues["value"];
        dsParametrosAcionamentosAPI.Tables[0].Rows.Add(dr);
        dsParametrosAcionamentosAPI.Tables[0].AcceptChanges();
        ((ASPxGridView)(sender)).DataSource = dsParametrosAcionamentosAPI.Tables[0];
        ((ASPxGridView)(sender)).DataBind();
        Session["dsParametrosAcionamentosAPI"] = dsParametrosAcionamentosAPI;
        //atualizaGridDeParametrosAcionamentosPeloIdDoAcionamento(auxId.ToString());
        ((ASPxGridView)(sender)).CancelEdit();
        
        e.Cancel = true;
    }

    protected void gv_ParametrosAcionamentos_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataSet dsParametrosAcionamentosAPI = ((DataSet)Session["dsParametrosAcionamentosAPI"]).Copy();

        foreach (DataRow dr1 in dsParametrosAcionamentosAPI.Tables[0].Rows)
        {
            if (dr1["IdKey"] + "" == e.Keys[0] + "")
            {
                dr1["parameter"] = e.NewValues["parameter"];
                dr1["dataType"] = e.NewValues["dataType"];
                dr1["httpPart"] = e.NewValues["httpPart"];
                dr1["sendNull"] = e.NewValues["sendNull"];
                dr1["value"] = e.NewValues["value"];


                dsParametrosAcionamentosAPI.Tables[0].AcceptChanges();
                break;
            }
        }
        //object auxId = gv_Acionamentos.GetRowValues(gv_Acionamentos.FocusedRowIndex, gv_Acionamentos.KeyFieldName);
        //atualizaGridDeParametrosAcionamentosPeloIdDoAcionamento(auxId.ToString());
        ((ASPxGridView)(sender)).DataSource = dsParametrosAcionamentosAPI;
        ((ASPxGridView)(sender)).DataBind();
        ((ASPxGridView)(sender)).CancelEdit();
        e.Cancel = true;
        Session["dsParametrosAcionamentosAPI"] = dsParametrosAcionamentosAPI;

    }

    protected void gv_Acionamentos_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        if (e.CallbackName == "CUSTOMCALLBACK")
        {
            limpaGrids(((ASPxGridView)sender));
            AtribuiDatasetGridAcionamentosAPI();
        }
    }

    protected void gv_Acionamentos_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "enabled")
        {
            ASPxCheckBox c = (ASPxCheckBox)e.Editor;
            c.Checked = true;
        }
        else if (e.Column.FieldName == "activationSequence")
        {
            ASPxSpinEdit seq = (ASPxSpinEdit)e.Editor;
            seq.Value = 1;
        }
    }

    protected void gv_Acionamentos_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        var targetDelete = e.Values["Id"];
        DataSet dsAcionamentos = (DataSet)Session["dsAcionamentosAPI"];
        foreach (DataRow dr1 in dsAcionamentos.Tables[0].Rows)
        {
            if (dr1["Id"] + "" == e.Values["Id"] + "")
            {
                dr1.Delete();
                dsAcionamentos.Tables[0].AcceptChanges();
                break;
            }
        }
        ((ASPxGridView)(sender)).DataSource = dsAcionamentos.Tables[0];
        ((ASPxGridView)(sender)).DataBind();
        Session["dsAcionamentosAPI"] = dsAcionamentos;
        e.Cancel = true;
    }

    protected void gv_Acionamentos_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataSet dsAcionamentos = ((DataSet)Session["dsAcionamentosAPI"]).Copy();

        object auxId = dsAcionamentos.Tables[0].Compute("MIN(Id)", null);
        int auxId1 = 0;
        bool retorno = int.TryParse(auxId.ToString(), out auxId1);
        auxId1 -= 1;

        DataRow dr = dsAcionamentos.Tables[0].NewRow();   //dsAcionamentos.Tables[0].Compute("max(Id)", null).ToString()
        dr["Id"] = auxId1;
        dr["webServiceURL"] = e.NewValues["webServiceURL"];
        dr["activationSequence"] = e.NewValues["activationSequence"];
        dr["enabled"] = e.NewValues["enabled"] == null ? false : e.NewValues["enabled"];

        dsAcionamentos.Tables[0].Rows.Add(dr);

        ((ASPxGridView)(sender)).DataSource = dsAcionamentos.Tables[0];
        ((ASPxGridView)(sender)).DataBind();
        Session["dsAcionamentosAPI"] = dsAcionamentos;
        ((ASPxGridView)(sender)).CancelEdit();
        e.Cancel = true;
    }

    protected void gv_Acionamentos_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataSet dsAcionamentos = (DataSet)Session["dsAcionamentosAPI"];

        foreach (DataRow dr1 in dsAcionamentos.Tables[0].Rows)
        {
            if (dr1["Id"] + "" == e.Keys[0] + "")
            {
                dr1["activationSequence"] = e.NewValues["activationSequence"];
                dr1["webServiceURL"] = e.NewValues["webServiceURL"];
                dr1["enabled"] = e.NewValues["enabled"];

                dsAcionamentos.Tables[0].AcceptChanges();
                break;
            }
        }
        ((ASPxGridView)(sender)).DataSource = dsAcionamentos.Tables[0];
        ((ASPxGridView)(sender)).DataBind();
        ((ASPxGridView)(sender)).CancelEdit();
        e.Cancel = true;
        Session["dsAcionamentosAPI"] = dsAcionamentos;
    }

    protected void pnCallbackParametrosAcionamentos_Callback(object sender, CallbackEventArgsBase e)
    {

        atualizaGridDeParametrosAcionamentosPeloIdDoAcionamento(e.Parameter);

    }

    protected void atualizaGridDeParametrosAcionamentosPeloIdDoAcionamento(string Id)
    {
        DataSet dsRespostaFiltro = new DataSet();

        DataTable tabelas = new DataTable();
        dsRespostaFiltro.Tables.Add(tabelas);

        DataColumn NewColumn = null;

        NewColumn = new DataColumn("IdKey", Type.GetType("System.String"));
        NewColumn.Caption = "IdKey";
        NewColumn.ReadOnly = true;
        dsRespostaFiltro.Tables[0].Columns.Add(NewColumn);

        NewColumn = new DataColumn("Id", Type.GetType("System.Int32"));
        NewColumn.Caption = "Id";
        NewColumn.ReadOnly = false;
        dsRespostaFiltro.Tables[0].Columns.Add(NewColumn);

        NewColumn = new DataColumn("parameter", Type.GetType("System.String"));
        NewColumn.Caption = "#";
        NewColumn.ReadOnly = false;
        dsRespostaFiltro.Tables[0].Columns.Add(NewColumn);

        NewColumn = new DataColumn("dataType", Type.GetType("System.String"));
        NewColumn.Caption = "Tipo de dados";
        NewColumn.ReadOnly = false;
        dsRespostaFiltro.Tables[0].Columns.Add(NewColumn);

        NewColumn = new DataColumn("httpPart", Type.GetType("System.String"));
        NewColumn.Caption = "Seção";
        NewColumn.ReadOnly = false;
        dsRespostaFiltro.Tables[0].Columns.Add(NewColumn);

        NewColumn = new DataColumn("sendNull", Type.GetType("System.Boolean"));
        NewColumn.Caption = "Nulo";
        NewColumn.ReadOnly = false;
        dsRespostaFiltro.Tables[0].Columns.Add(NewColumn);

        NewColumn = new DataColumn("value", Type.GetType("System.String"));
        NewColumn.Caption = "Valor";
        NewColumn.ReadOnly = false;
        dsRespostaFiltro.Tables[0].Columns.Add(NewColumn);


        gv_ParametrosAcionamentos.ClientVisible = true;

        DataSet dsParametrosAcionamentosAPI = ((DataSet)Session["dsParametrosAcionamentosAPI"]).Copy();
        dsParametrosAcionamentosAPI.EnforceConstraints = false;

        DataRow[] dr = dsParametrosAcionamentosAPI.Tables[0].Select("Id='" + Id + "'");

        //if (dr.Length > 0)
        //{
        foreach (DataRow dr1 in dr)
        {

            DataRow dr2 = dsRespostaFiltro.Tables[0].NewRow();

            dr2["IdKey"] = dr1.ItemArray[0];
            dr2["Id"] = dr1.ItemArray[1];
            dr2["parameter"] = dr1.ItemArray[2];
            dr2["dataType"] = dr1.ItemArray[3];
            dr2["httpPart"] = dr1.ItemArray[4];
            dr2["sendNull"] = dr1.ItemArray[5];
            dr2["value"] = dr1.ItemArray[6];

            dsRespostaFiltro.Tables[0].Rows.Add(dr2);
        }
        //}

        //gv_ParametrosAcionamentos.DataSource = dsRespostaFiltro.Tables[0];
        //gv_ParametrosAcionamentos.DataBind();
    }

    protected void gv_ParametrosAcionamentos_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        if (e.CallbackName == "CUSTOMCALLBACK")
        {
            limpaGrids(((ASPxGridView)sender));
            AtribuiDatasetGridParametrosAcionamentosAPI();
        }

    }
}

