using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Specialized;
using DevExpress.Web;
using System.Data;

public partial class _Projetos_Administracao_propostaDeIniciativa_001 : System.Web.UI.Page
{
    #region Properties

    private string _connectionString;
    /// <summary>
    /// String de conexão para acesso a dados da tela.
    /// </summary>
    public string ConnectionString
    {
        get { return _connectionString; }
        private set
        {
            sdsDadosFomulario.ConnectionString =
                sdsEntidadesAcao.ConnectionString =
                sdsEquipeApoio.ConnectionString =
                sdsEstrategico.ConnectionString =
                sdsIndicadorEstrategico.ConnectionString =
                sdsIndicadorDesempenho.ConnectionString =
                sdsLider.ConnectionString =
                sdsLinhaAcao.ConnectionString =
                sdsMarcosCriticos.ConnectionString =
                sdsObjetivoEstrategico.ConnectionString =
                sdsObjetivoEstrategico_LinhaAtuacao.ConnectionString =
                sdsOpcoesLinhaAtuacao.ConnectionString =
                sdsResponsaveisAcao.ConnectionString =
                sdsResultados.ConnectionString =
                sdsResultadosEsperados.ConnectionString =
                sdsIndicadorOperacional.ConnectionString =
                sdsUnidadeGestora.ConnectionString = value;
            _connectionString = value;
        }
    }

    /// <summary>
    /// Indica se está sendo criado um novo projeto (codigoProjeto == -1).
    /// </summary>
    public bool IndicaNovoProjeto
    {
        get
        {
            return _codigoProjeto == -1;
        }
    }

    private int _codigoProjeto;
    /// <summary>
    /// Código do projeto vinculado a proposta de iniciativa. Caso seja igual a '-1' trata-se de uma nova proposta de iniciativa
    /// </summary>
    public int CodigoProjeto
    {
        get { return _codigoProjeto; }
        set
        {
            cDados.setInfoSistema("CodigoProjeto", value); // variável de sessão usada no fluxo
            Session["codigoProjeto"] = value;
            _codigoProjeto = value;
        }
    }

    private int _codigoEntidadeUsuarioResponsavel;
    /// <summary>
    /// Código da entidade do usuário logado
    /// </summary>
    public int CodigoEntidadeUsuarioResponsavel
    {
        get { return _codigoEntidadeUsuarioResponsavel; }
        set
        {
            cDados.setInfoSistema("CodigoEntidade", value);
            Session["codigoEntidade"] = value;
            _codigoEntidadeUsuarioResponsavel = value;
        }
    }

    private int _codigoUsuarioResponsavel;
    /// <summary>
    /// Código do usuário logado
    /// </summary>
    public int CodigoUsuarioResponsavel
    {
        get { return _codigoUsuarioResponsavel; }
        set
        {
            cDados.setInfoSistema("IDUsuarioLogado", value);
            Session["codigoUsuario"] = value;
            _codigoUsuarioResponsavel = value;
        }
    }

    private int _codigoWorkflow;

    public int CodigoWorkflow
    {
        get { return _codigoWorkflow; }
        set { _codigoWorkflow = value; }
    }

    private int _codigoEtapaWf;

    public int CodigoEtapaWf
    {
        get { return _codigoEtapaWf; }
        set { _codigoEtapaWf = value; }
    }

    private int _CodigoInstanciaWf;

    public int CodigoInstanciaWf
    {
        get { return _CodigoInstanciaWf; }
        set { _CodigoInstanciaWf = value; }
    }


    #endregion

    #region Private Fields
    private bool readOnly;
    private bool podeEditar;
    private bool podeIncluir;
    private bool podeExcluir;
    private bool podeEditarAnoOrcamento;
    private dados cDados;

    DataSet dsMetas = new DataSet();
    DataSet dsParcerias = new DataSet();
    DataSet dsMarcos = new DataSet();

    public string alturaTela = "";
    public int AcaoSemMeta = 0;
    public int AcaoSemAtividade = 0;
    public string origemTermoAbertura = "";

    public bool podeIncluirAcao;
    private bool podeEditarAcao;
    private bool podeExcluirAcao;
    private bool podeIncluirAtividade;
    private bool podeEditarAtividade;
    private bool podeExcluirAtividade;

    public string faseTAI = "";

    #endregion

    #region Event Handlers

    #region Page

    protected void Page_Init(object sender, EventArgs e)
    {
        //TERMO DE ABERTURA UTILIZADO PELO SESCOOP
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        OrderedDictionary listaParametrosDados = new OrderedDictionary();
        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.aplicaEstiloVisual(pageControl);
        //TODO: Descomentar esse código
        /*
         * try
         * {
         * if (cDados.getInfoSistema("IDUsuarioLogado") == null)
         * Response.Redirect("~/erros/erroInatividade.aspx");
         * }
         * catch
         * {
         * Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
         * Response.End();
         * }
         */
        if (!IsPostBack)
        {
            int activeTabIndex;
            int alturaInt;
            if (int.TryParse(Request.QueryString["tab"], out activeTabIndex))
                pageControl.ActiveTabIndex = activeTabIndex;
            if (int.TryParse(Request.QueryString["Altura"], out alturaInt))
                alturaTela = (alturaInt - 113).ToString();
            if (!string.IsNullOrEmpty(alturaTela))
            {
                dv01.Style.Add("height", alturaTela + "px");
                dv02.Style.Add("height", alturaTela + "px");
                dv03.Style.Add("height", alturaTela + "px");

                gvDados.Settings.VerticalScrollableHeight = alturaInt;


                dv01.Style.Add("overflow", "auto");
                dv02.Style.Add("overflow", "auto");
                dv03.Style.Add("overflow", "auto");
            }

            hfGeral.Set("PodeSalvarResultados", "S");
            hfGeral.Set("PodeSalvarAtividade", "S");
            hfGeral.Set("PodeSalvarMetas", "S");
            hfGeral.Set("PodeSalvarAcao", "S");
            AcaoSemMeta = 0;
            AcaoSemAtividade = 0;
        }


        faseTAI = Request.QueryString["FaseTAI"] != null ? Request.QueryString["FaseTAI"].ToString() : "";
        origemTermoAbertura = Request.QueryString["OrigemTermoAbertura"] != null ? Request.QueryString["OrigemTermoAbertura"].ToString() : "";

        string cp = Request.QueryString["CP"];
        CodigoProjeto = int.Parse(string.IsNullOrEmpty(cp) ? "-1" : cp);

        string comandoSQLFase = string.Format(@" 
            SELECT ISNULL(EtapaOrcamento, 1) AS EtapaOrcamento FROM {0}.{1}.TermoAbertura02 WHERE CodigoProjeto = {2}
        ", cDados.getDbName(), cDados.getDbOwner(), CodigoProjeto);

        DataSet dsFase = cDados.getDataSet(comandoSQLFase);

        if (cDados.DataSetOk(dsFase) && cDados.DataTableOk(dsFase.Tables[0]) && dsFase.Tables[0].Rows[0]["EtapaOrcamento"].ToString().Trim() != "" && int.Parse(dsFase.Tables[0].Rows[0]["EtapaOrcamento"].ToString()) > 1)
            faseTAI = "R";

        CodigoWorkflow = int.Parse(string.IsNullOrEmpty(Request.QueryString["CWF"]) ? "-1" : Request.QueryString["CWF"]);
        CodigoEtapaWf = int.Parse(string.IsNullOrEmpty(Request.QueryString["CEWF"]) ? "-1" : Request.QueryString["CEWF"]);
        CodigoInstanciaWf = int.Parse(string.IsNullOrEmpty(Request.QueryString["CIWF"]) ? "-1" : Request.QueryString["CIWF"]);
        readOnly = "S".Equals(Request.QueryString["RO"]);
        podeEditar = !readOnly;
        podeEditarAnoOrcamento = !readOnly && cDados.verificaAcessoAnoOrcamento(CodigoProjeto);
        podeExcluir = !readOnly;
        podeIncluir = !readOnly && !IndicaNovoProjeto;
        CodigoEntidadeUsuarioResponsavel = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade") ?? -1);
        CodigoUsuarioResponsavel = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado") ?? -1);

        btnImprimir.ClientEnabled = !IndicaNovoProjeto;
        btnImprimir0.ClientEnabled = !IndicaNovoProjeto;
        DefineVariaveisHiddenField();
        DefineAcoesVerificacaoConflitosAgenda();

        if (Request.QueryString["PassarFluxo"] != null && Request.QueryString["PassarFluxo"].ToString() == "N")
            hfGeral.Set("PodePassarFluxo", "N");
        else
            hfGeral.Set("PodePassarFluxo", "S");

        if (readOnly == false && faseTAI == "R" && CodigoProjeto != -1 && !IsPostBack)
        {
            string comandoSQL = string.Format(@"SELECT codigoUsuarioBloqueio FROM {0}.{1}.TermoAbertura02 WHERE CodigoProjeto = {2}", cDados.getDbName(), cDados.getDbOwner(), CodigoProjeto);

            DataSet dsBloqueio = cDados.getDataSet(comandoSQL);
            
            if (cDados.DataSetOk(dsBloqueio) && cDados.DataTableOk(dsBloqueio.Tables[0]))
            {
                if (dsBloqueio.Tables[0].Rows[0]["codigoUsuarioBloqueio"].ToString() == "")
                {
                    btnNao.JSProperties["cp_URLDirecionamento"] = "propostaDeIniciativa_001.aspx?RO=S&PassarFluxo=N&" + Request.QueryString.ToString().Replace("&RO=N", "").Replace("?RO=N", "");
                    pcIniciarFluxo.ShowOnPageLoad = true;
                }
                else if (dsBloqueio.Tables[0].Rows[0]["codigoUsuarioBloqueio"].ToString() != CodigoUsuarioResponsavel.ToString())
                {
                    Response.Redirect("~/_Projetos/Administracao/" + "propostaDeIniciativa_001.aspx?RO=S&PassarFluxo=N&" + Request.QueryString.ToString().Replace("&RO=N", "").Replace("?RO=N", ""));
                }
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //Obtem a string de conexao e seta a propriedade 'ConnectionString' dos SQL Data Sources da página.
        ConnectionString = cDados.classeDados.getStringConexao();
        DefineVisibilidadeCampos();
        DefineCamposSomenteLeitura();
        carregaComboAnoOrcamento();
        //Se foi informado um código do projeto carrega dados formulário
        if (!IsPostBack && !IndicaNovoProjeto)
            CarregaDadosFormulario();

        podeIncluirAcao = podeIncluir;
        podeEditarAcao = podeEditar;
        podeExcluirAcao = podeExcluir;
        podeIncluirAtividade = podeIncluir;
        podeEditarAtividade = podeEditar;
        podeExcluirAtividade = podeExcluir;

        gvDados.JSProperties["cp_CodigoProjeto"] = CodigoProjeto;

        carregaGridPlanoTrabalho();
        carregaAbaCronograma();


        callbackCronogramaOrcamentario.JSProperties["cp_PodeSalvar"] = podeSalvarCronogramaOrcamentario();
        callbackCronogramaOrcamentario.JSProperties["cp_PodeEditarCronograma"] = podeEditarAnoOrcamento;
        cmbAnoOrcamento.ClientEnabled = podeEditarAnoOrcamento;
      

    }

    #endregion

    #region Todas as grids

    protected void grid_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        switch (e.ButtonType)
        {
            case ColumnCommandButtonType.Delete:
                if (!podeExcluir || faseTAI == "R")
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
                else if (grid == gvEstrategico)
                {
                    SqlDataSource[] sources = new SqlDataSource[] { sdsLinhaAcao };
                    string nomeColunaCodigo = "SequenciaObjetivo";
                    string codigoObjetivoEstrategico = gvEstrategico.GetDataRow(e.VisibleIndex)[nomeColunaCodigo].ToString();
                    bool existeDependecia = VerificaExisteDependeciaAcao(codigoObjetivoEstrategico, sources, nomeColunaCodigo);
                    if (existeDependecia)
                    {
                        e.Enabled = false;
                        e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                        e.Text = "Não é possível excluir o objetivo estratégico pois há uma ou mais linhas de ação vinculadas a ele.";
                    }
                }
                break;
            case ColumnCommandButtonType.Edit:
                if (!podeEditar || faseTAI == "R")
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
                break;
            default:
                break;
        }
    }

    protected void grid_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        if (e.Exception == null)
            SalvaEdicaoIniciativa();
    }

    protected void grid_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        if (e.Exception == null)
            SalvaEdicaoIniciativa();
    }

    protected void grid_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        e.Properties["cpMyRowCount"] = ((ASPxGridView)sender).VisibleRowCount;
    }

    #endregion

    #region gvEstrategico

    protected void gvEstrategico_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //Define a descrição do objetivo estratégico
        SetTextFieldValue(e.NewValues, "CodigoObjetoEstrategia", "DescricaoObjetoEstrategia", sdsObjetivoEstrategico, "Codigo", "Descricao");
        //Caso tenha sido informado o indicador, define a descrição do indicador
        if (e.NewValues["CodigoIndicador"] != null)
            SetTextFieldValue(e.NewValues, "CodigoIndicador", "Meta", sdsIndicadorEstrategico, "CodigoIndicador", "Meta");
    }

    protected void gvEstrategico_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //Define a descrição do objetivo estratégico
        SetTextFieldValue(e.NewValues, "CodigoObjetoEstrategia", "DescricaoObjetoEstrategia", sdsObjetivoEstrategico, "Codigo", "Descricao");
        //Caso tenha sido informado o indicador, define a descrição do indicador
        if (e.NewValues["CodigoIndicador"] != null)
            SetTextFieldValue(e.NewValues, "CodigoIndicador", "Meta", sdsIndicadorEstrategico, "CodigoIndicador", "Meta");
    }

    protected void gvEstrategico_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if ("CodigoObjetoEstrategia".Equals(e.Column.FieldName))
        {
            ASPxComboBox editor = (ASPxComboBox)e.Editor;
            ControlParameter param = new ControlParameter("CodigoSuperior", editor.UniqueID, "Value");
            sdsIndicadorEstrategico.FilterParameters.Clear();
            sdsIndicadorEstrategico.FilterParameters.Add(param);
            if (gvEstrategico.IsEditing && !gvEstrategico.IsNewRowEditing)
                editor.ClientSideEvents.Init = "function(s, e) { s.SetEnabled(false);}";
            else
                editor.ClientSideEvents.Init = "function(s, e) { s.SetEnabled(true);}";
        }
        //if ("CodigoIndicador".Equals(e.Column.FieldName))
        //{
        //    ASPxComboBox editor = (ASPxComboBox)e.Editor;
        //    ControlParameter param = new ControlParameter("CodigoSuperior", editor.UniqueID, "Value");
        //    sdsIndicadorEstrategico.FilterParameters.Clear();
        //    sdsIndicadorEstrategico.FilterParameters.Add(param);
        //    if (gvEstrategico.IsEditing && !gvEstrategico.IsNewRowEditing)
        //        editor.ClientSideEvents.Init = "function(s, e) { s.SetEnabled(false);}";
        //    else
        //        editor.ClientSideEvents.Init = "function(s, e) { s.SetEnabled(true);}";
        //}
    }

    #endregion

    #region gvLinhaAcao

    protected void gvLinhaAcao_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //Define a descrição da linha de ação
        SetTextFieldValue(e.NewValues, "CodigoAcaoSugerida", "DescricaoAcao", sdsOpcoesLinhaAtuacao, "CodigoAcaoSugerida", "DescricaoAcao");
    }

    protected void gvLinhaAcao_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //Define a descrição da linha de ação
        SetTextFieldValue(e.NewValues, "CodigoAcaoSugerida", "DescricaoAcao", sdsOpcoesLinhaAtuacao, "CodigoAcaoSugerida", "DescricaoAcao");
    }

    protected void gvLinhaAcao_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if ("CodigoObjetoEstrategia".Equals(e.Column.FieldName))
        {
            ASPxEditBase editor = e.Editor;
            ControlParameter param = new ControlParameter("CodigoSuperior", editor.UniqueID, "Value");
            sdsOpcoesLinhaAtuacao.FilterParameters.Clear();
            sdsOpcoesLinhaAtuacao.FilterParameters.Add(param);
        }
    }

    #endregion

    #region gvResultados

    protected void gvResultados_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string nomeColunaCodigo = "CodigoIndicador";
        string nomeColunaDescricao = "NomeIndicador";
        string nomeColunaNomeIndicador = "NomeIndicador";
        string codigo = e.NewValues["CodigoIndicador"].ToString();
        string descricao = ObtemDescricaoPeloCodigo(
            codigo, nomeColunaCodigo, nomeColunaDescricao, sdsIndicadorDesempenho);

        if (e.NewValues.Contains(nomeColunaNomeIndicador))
            e.NewValues[nomeColunaNomeIndicador] = descricao;
        else
            e.NewValues.Add(nomeColunaNomeIndicador, descricao);

        string nomeColunaSetencaResultado = "SetencaResultado";
        if (e.NewValues.Contains(nomeColunaSetencaResultado))
            e.NewValues[nomeColunaSetencaResultado] = descricao;
        else
            e.NewValues.Add(nomeColunaSetencaResultado, descricao);
    }

    protected void gvResultados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string nomeColunaCodigo = "CodigoIndicador";
        string nomeColunaDescricao = "NomeIndicador";
        string nomeColunaNomeIndicador = "NomeIndicador";
        string codigo = e.NewValues["CodigoIndicador"].ToString();
        string descricao = ObtemDescricaoPeloCodigo(
            codigo, nomeColunaCodigo, nomeColunaDescricao, sdsIndicadorDesempenho);

        if (e.NewValues.Contains(nomeColunaNomeIndicador))
            e.NewValues[nomeColunaNomeIndicador] = descricao;
        else
            e.NewValues.Add(nomeColunaNomeIndicador, descricao);

        string nomeColunaSetencaResultado = "SetencaResultado";
        if (e.NewValues.Contains(nomeColunaSetencaResultado))
            e.NewValues[nomeColunaSetencaResultado] = descricao;
        else
            e.NewValues.Add(nomeColunaSetencaResultado, descricao);
    }

    #endregion

    #region cmbLider

    protected void cmbLider_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        bool indicaComboLider = comboBox.ID.Contains("cmbLider");
        SqlDataSource dataSource = indicaComboLider ?
            sdsLider : sdsResponsaveisAcao;
        string clausula = indicaComboLider ?
            string.Empty :
            string.Format("AND EXISTS( SELECT 1 FROM RecursoCorporativo AS rc WHERE rc.[CodigoEntidade] = {0} and rc.[CodigoUsuario] = us.CodigoUsuario)", CodigoEntidadeUsuarioResponsavel);

        string comandoSQL = cDados.getSQLComboUsuarios(CodigoEntidadeUsuarioResponsavel, e.Filter, clausula);

        cDados.populaComboVirtual(dataSource, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);

    }

    protected void cmbLider_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
    {
        if (string.IsNullOrEmpty(ConnectionString))
            ConnectionString = cDados.classeDados.getStringConexao();
        if (e.Value != null)
        {
            long value;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            bool indicaComboLider = comboBox.ID.Contains("cmbLider");
            SqlDataSource dataSource = indicaComboLider ?
                sdsLider : sdsResponsaveisAcao;

            dataSource.SelectCommand = cDados.getSQLComboUsuariosPorID(CodigoEntidadeUsuarioResponsavel);

            dataSource.SelectParameters.Clear();
            dataSource.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());

            comboBox.DataBind();
        }
    }

    #endregion

    #region Dados

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string tipoOperacao;
        if (IndicaNovoProjeto)
        {
            SalvaNovaIniciativa();
            tipoOperacao = "I";
            callback.JSProperties["cp_gravouNovaIni"] = 1;
        }
        else
        {
            SalvaEdicaoIniciativa();
            tipoOperacao = "U";
            callback.JSProperties["cp_gravouNovaIni"] = 0;
        }
        e.Result = tipoOperacao + CodigoProjeto.ToString();
    }

    protected void sqlDataSource_Filtering(object sender, SqlDataSourceFilteringEventArgs e)
    {
        if (e.ParameterValues.Contains("CodigoSuperior"))
            e.Cancel = e.ParameterValues["CodigoSuperior"] == null;
    }

    protected void sdsDadosFomulario_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows == 0)
        {
            object codProjeto = -1;
            string comandoSqlSelect = string.Format(@"
 SELECT p.CodigoProjeto, 
        p.NomeProjeto, 
        p.CodigoUnidadeNegocio,
        u.NomeUnidadeNegocio
   FROM {0}.{1}.Projeto p INNER JOIN
        {0}.{1}.UnidadeNegocio u ON p.CodigoUnidadeNegocio = u.CodigoUnidadeNegocio
  WHERE CodigoProjeto = {2}"
                , cDados.getDbName(), cDados.getDbOwner(), CodigoProjeto);
            DataSet dsTemp = cDados.getDataSet(comandoSqlSelect);
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                codProjeto = CodigoProjeto;
                DataRow dr = dsTemp.Tables[0].Rows[0];
                string comandoSqlInsert = string.Format(@"
 INSERT INTO {0}.{1}.TermoAbertura02 
        (CodigoProjeto, NomeIniciativa, CodigoUnidadeNegocio, NomeUnidadeNegocio, ValorEstimado, IndicaTipoProjeto, FonteRecurso, IndicaClassificacaoProjeto, IndicaAreaAtuacao) 
 VALUES
        ({2}, '{3}', {4}, '{5}', 0, 'FI', 'NO', 'NO', 'FO')"
                , cDados.getDbName(), cDados.getDbOwner(), CodigoProjeto, dr["NomeProjeto"], dr["CodigoUnidadeNegocio"], dr["NomeUnidadeNegocio"]);

                int regAfetados = 0;
                cDados.execSQL(comandoSqlInsert, ref regAfetados);
            }
            string url = string.Format(
                "propostaDeIniciativa_001.aspx?CP={2}&RO={0}&Altura={1}&CWF={3}&CEWF={4}&CIWF={5}"
                , (readOnly ? "S" : "N"), alturaTela, codProjeto, CodigoWorkflow, CodigoEtapaWf, CodigoInstanciaWf);
            Response.Redirect(url, true);
        }
    }

    protected void pageControl_CustomJSProperties(object sender, DevExpress.Web.CustomJSPropertiesEventArgs e)
    {
        e.Properties["cpReadOnly"] = readOnly;
    }

    #endregion

    #endregion

    #region Methods

    private void DefineVariaveisHiddenField()
    {
        hfGeral.Set("CodigoEntidade", CodigoEntidadeUsuarioResponsavel);
        hfGeral.Set("CodigoProjeto", CodigoProjeto);
        hfGeral.Set("CodigoWorkflow", CodigoWorkflow);
        hfGeral.Set("CodigoEtapaWf", CodigoEtapaWf);
        hfGeral.Set("CodigoInstanciaWf", CodigoInstanciaWf);

    }

    private void DefineAcoesVerificacaoConflitosAgenda()
    {
        int[] acoes;
        string comandoSql = string.Format(@"
DECLARE @CodigoWorkflow Int, 
        @CodigoEtapaWf Int
    SET @CodigoWorkflow = {0}
    SET @CodigoEtapaWf = {1}

 SELECT [aae].[CodigoAcaoWf]
   FROM [dbo].[AcoesAutomaticasEtapasWf] AS [aae] INNER JOIN 
        [dbo].[AcoesAutomaticasWf] AS [aa] ON (aa.[CodigoAcaoAutomaticaWf] = aae.[CodigoAcaoAutomaticaWf])
  WHERE [aae].[CodigoWorkflow] = @CodigoWorkflow
    AND [aae].[CodigoEtapaWf] = @CodigoEtapaWf
    AND [aa].[Nome] = 'Verificar Conflitos Agenda'"
            , CodigoWorkflow, CodigoEtapaWf);
        DataSet ds = cDados.getDataSet(comandoSql);
        acoes = new int[ds.Tables[0].Rows.Count];
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            acoes[i] = (int)ds.Tables[0].Rows[i]["CodigoAcaoWf"];
        hfGeral.Set("Acoes", acoes);
    }

    private void DefineCamposSomenteLeitura()
    {
        bool permiteEdicao = !readOnly;
        //Define se a tela vai estar disponível somente pra leitura
        defineEstadoEdicaoPainel(pnlDescricaoProjeto, permiteEdicao && faseTAI != "R");
        defineEstadoEdicaoPainel(pnlAlinhamentoEstrategico, permiteEdicao && faseTAI != "R");
        defineEstadoEdicaoPainel(pnlQuadroLogico, permiteEdicao && faseTAI != "R");
        defineEstadoEdicaoPainel(pnlPlanoTrabalho, permiteEdicao);

        btnSalvar.ClientVisible = permiteEdicao && faseTAI != "R";
        btnSalvar0.ClientVisible = permiteEdicao && faseTAI != "R";
        
    }

    private void defineEstadoEdicaoPainel(ASPxPanel painel, bool statusHabilitado)
    {
        if (!statusHabilitado)        
            painel.ClientSideEvents.Init = "function(s, e){ s.SetEnabled(false); }";
    }

    private void DefineVisibilidadeCampos()
    {
        //Alguns campos estarão visíveis apenas quando a edição do formulário estiver habilitada...
        if (readOnly)
            Header.Controls.Add(cDados.getLiteral("<style type=\"text/css\"> .AvailableOnlyEditing {display: none;}</style>"));
        //...enquanto outros estarão visíveis apenas quando o formulário estiver diponível apenas para leitura
        else
            Header.Controls.Add(cDados.getLiteral("<style type=\"text/css\"> .AvailableReadOnly {display: none;}</style>"));
    }

    /// <summary>
    /// Define o campo descrição (keyTextField) para um determinado campo chave (keyValueField), 
    /// que é obtido, na fonte de dados (source), pela coluna descrição (sourceTextField).
    /// Este método é util para obter a descrição na grid de uma coluna do tipo 'ComboBox'
    /// </summary>
    /// <param name="fields">Dicionário com os campos.</param>
    /// <param name="keyValueField">Nome do campo, no dicionário, correspondente a coluna chave.</param>
    /// <param name="keyTextField">Nome do campo, no dicionário, correspondente a coluna de descrição.</param>
    /// <param name="source">SqlDataSource que serve de fonte de dados.</param>
    /// <param name="sourceValueField">Nome da coluna chave na fonte de dados.</param>
    /// <param name="sourceTextField">Nome da coluna descrição na fonte de dados.</param>
    private static void SetTextFieldValue(OrderedDictionary fields, string keyValueField, string keyTextField, SqlDataSource source, string sourceValueField, string sourceTextField)
    {
        string codigo = fields[keyValueField].ToString();
        string descricao = ObtemDescricaoPeloCodigo(
            codigo, sourceValueField, sourceTextField, source);

        if (fields.Contains(keyTextField))
            fields[keyTextField] = descricao;
        else
            fields.Add(keyTextField, descricao);
    }

    private void CarregaDadosFormulario()
    {
        DataView dv = (DataView)sdsDadosFomulario.Select(new DataSourceSelectArguments());

        if (dv.Count > 0)
        {
            DataRowView row = dv[0];

            string fonteRecurso = row["FonteRecurso"].ToString();
            switch (fonteRecurso)
            {
                case "FU"://FUNDECOOP
                    cbFundecoop.Checked = true;
                    cbRecursoProprio.Checked = false;
                    break;
                case "RP"://Recurso Próprio
                    cbFundecoop.Checked = false;
                    cbRecursoProprio.Checked = true;
                    break;
                case "AO"://Ambas opções
                    cbFundecoop.Checked = true;
                    cbRecursoProprio.Checked = true;
                    break;
                case "NO"://Nenhuma das opções
                    cbFundecoop.Checked = false;
                    cbRecursoProprio.Checked = false;
                    break;
            }
            txtNomeProjeto.Value = row["NomeIniciativa"];
            rblTipoProjeto.Value = row["IndicaTipoProjeto"];
            txtLider.Value = row["NomeGerenteIniciativa"];
            txtUnidadeGestora.Value = row["NomeUnidadeNegocio"];
            cmbLider.Value = row["CodigoGerenteIniciativa"];
            cmbUnidadeGestora.Value = row["CodigoUnidadeNegocio"];
            cmbAnoOrcamento.Value = row["codigoMovimentoOrcamento"];
            deDataInicio.Value = row["DataInicio"];
            deDataTermino.Value = row["DataTermino"];
            txtPublicoAlvo.Value = row["PublicoAlvo"];
            txtBeneficiarios.Value = row["Beneficiarios"];
            rblSituacaoProjeto.Value = row["IndicaClassificacaoProjeto"];
            txtResultadosContinuidade.Value = row["UltimosResultados"];
            txtJustificativa.Value = row["Justificativa"];
            rblAreaAtuacao.Value = row["IndicaAreaAtuacao"];
            txtObjetivoGeral.Value = row["ObjetivoGeral"];
        }
    }

    protected string ObtemBotaoInclusaoRegistro(string nomeGrid, string assuntoGrid)
    {
        string tituloBotaoDesabilitado = IndicaNovoProjeto ? string.Format("Clique em 'Salvar' para poder registrar as informações de [{0}]", assuntoGrid) : "Incluir " + assuntoGrid;
        string htmlBotaoDesabilitado = string.Format(@"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""{0}"" style=""cursor: default;""/>", tituloBotaoDesabilitado);
        string htmlBotaoHabilitado = string.Format(@"<img src=""../../imagens/botoes/incluirReg02.png"" alt=""{1}"" onclick=""{0}.AddNewRow();"" title=""{1}"" style=""cursor: pointer;""/>", nomeGrid, "Incluir " + assuntoGrid);

        string strRetorno = string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>"
            , (podeIncluir && faseTAI != "R") ? htmlBotaoHabilitado : htmlBotaoDesabilitado);

        return strRetorno;
    }

    private void SalvaNovaIniciativa()
    {
        string fonteRecurso;
        fonteRecurso = ObtemSiglaFonteRecurso();

        #region Comando SQL

        string comandoSql = string.Format(@"
DECLARE @Erro INT
DECLARE @MensagemErro nvarchar(2048)
    SET @Erro = 0

BEGIN TRAN

BEGIN TRY

DECLARE @CodigoEntidade INT,
        @CodigoUsuarioInclusao INT,
        @CodigoCategoria INT
        
    SET @CodigoEntidade = {2}
    SET @CodigoUsuarioInclusao = {3}
    SET @CodigoCategoria = NULL

DECLARE @CodigoProjeto int,
        @NomeIniciativa varchar(255),
        @CodigoGerenteIniciativa int,
        @NomeGerenteIniciativa varchar(60),
        @CodigoUnidadeNegocio int,
        @NomeUnidadeNegocio varchar(100),
        @DataInicio datetime,
        @DataTermino datetime,
        @ValorEstimado decimal(25,4),
        @PublicoAlvo varchar(500),
        @Justificativa varchar(8000),
        @IndicaTipoProjeto CHAR(2),
        @IndicaClassificacaoProjeto CHAR(2),
        @IndicaAreaAtuacao CHAR(2),
        @FonteRecurso CHAR(2),
        @UltimosResultados varchar(8000),
        @Beneficiarios varchar(500),
        @ObjetivoGeral varchar(8000),
        @CodigoMovimentoOrcamento int 

    SET @ValorEstimado = 0
    SET @CodigoProjeto = -1
    SET @NomeIniciativa = '{4}'
    SET @CodigoGerenteIniciativa = {5}
    SET @NomeGerenteIniciativa = '{6}'
    SET @CodigoUnidadeNegocio = {7}
    SET @NomeUnidadeNegocio = '{8}'
    SET @DataInicio = {9}
    SET @DataTermino = {10}
    SET @PublicoAlvo = '{11}'
    SET @Justificativa = '{12}'
    SET @IndicaTipoProjeto = '{13}'
    SET @IndicaClassificacaoProjeto = '{14}'
    SET @IndicaAreaAtuacao = '{15}'
    SET @FonteRecurso = '{16}'
    SET @UltimosResultados = '{17}'
    SET @Beneficiarios = '{18}'
    SET @ObjetivoGeral = '{19}'
    SET @CodigoMovimentoOrcamento = {20}

exec {0}.{1}.p_InserePropostaTAI 
        @NomeIniciativa, 
        @CodigoEntidade, 
        @CodigoUsuarioInclusao, 
        @CodigoCategoria, 
        @CodigoUnidadeNegocio, 
        @ObjetivoGeral , 
        @CodigoGerenteIniciativa, 
        @CodigoProjeto OUTPUT
        
    SET @Erro = @Erro + @@ERROR

INSERT INTO {0}.{1}.TermoAbertura02
           (CodigoProjeto
           ,NomeIniciativa
           ,CodigoGerenteIniciativa
           ,NomeGerenteIniciativa 
           ,CodigoUnidadeNegocio 
           ,NomeUnidadeNegocio 
           ,DataInicio 
           ,DataTermino
           ,ValorEstimado 
           ,PublicoAlvo 
           ,Justificativa 
           ,IndicaTipoProjeto 
           ,IndicaClassificacaoProjeto 
           ,IndicaAreaAtuacao 
           ,FonteRecurso 
           ,UltimosResultados 
           ,Beneficiarios
           ,ObjetivoGeral
           ,CodigoMovimentoOrcamento)
     VALUES
           (@CodigoProjeto
           ,@NomeIniciativa
           ,@CodigoGerenteIniciativa
           ,@NomeGerenteIniciativa 
           ,@CodigoUnidadeNegocio 
           ,@NomeUnidadeNegocio 
           ,@DataInicio 
           ,@DataTermino
           ,@ValorEstimado 
           ,@PublicoAlvo 
           ,@Justificativa 
           ,@IndicaTipoProjeto 
           ,@IndicaClassificacaoProjeto 
           ,@IndicaAreaAtuacao 
           ,@FonteRecurso 
           ,@UltimosResultados 
           ,@Beneficiarios
           ,@ObjetivoGeral
           ,@CodigoMovimentoOrcamento)
           
    SET @Erro = @Erro + @@ERROR

END TRY
BEGIN CATCH
    SET @Erro = ERROR_NUMBER()
    SET @MensagemErro = ERROR_MESSAGE()
END CATCH

IF @Erro = 0
BEGIN
    COMMIT
END
ELSE
BEGIN
    ROLLBACK
END


 SELECT @CodigoProjeto AS CodigoProjeto,
        @Erro AS CodigoErro, 
        @MensagemErro AS MensagemErro"
            , cDados.getDbName()
            , cDados.getDbOwner()
            , CodigoEntidadeUsuarioResponsavel
            , CodigoUsuarioResponsavel
            , txtNomeProjeto.Text.Replace("'", "''")
            , cmbLider.Value ?? "NULL"
            , cmbLider.Text.Replace("'", "''")
            , cmbUnidadeGestora.Value ?? "NULL"
            , cmbUnidadeGestora.Text.Replace("'", "''")
            , deDataInicio.Value == null ? "NULL" : string.Format(" CAST('{0}'     AS smalldatetime)", deDataInicio.Value)
            , deDataTermino.Value == null ? "NULL" : string.Format(" CAST('{0}'     AS smalldatetime)", deDataTermino.Value)
            , txtPublicoAlvo.Text.Replace("'", "''")
            , txtJustificativa.Text.Replace("'", "''")
            , rblTipoProjeto.Value ?? "NULL"
            , rblSituacaoProjeto.Value ?? "NULL"
            , rblAreaAtuacao.Value ?? "NULL"
            , fonteRecurso
            , txtResultadosContinuidade.Text.Replace("'", "''")
            , txtBeneficiarios.Text.Replace("'", "''")
            , txtObjetivoGeral.Text.Replace("'", "''")
            , cmbAnoOrcamento.Value ?? "NULL"
            , cmbAnoOrcamento.Text.Replace("'", "''")            
            );

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            object codigoErro = ds.Tables[0].Rows[0]["CodigoErro"];
            string mensagemErro = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
            if (!(Convert.IsDBNull(codigoErro) || codigoErro.Equals(0)))
            {
                if (mensagemErro.Contains("UQ_Projeto_NomeProjeto"))
                    throw new Exception("Nome de projeto já existente.");
                else
                    throw new Exception(mensagemErro.ToString());
            }
            CodigoProjeto = Convert.ToInt32(ds.Tables[0].Rows[0]["CodigoProjeto"]);
        }
    }

    private void SalvaEdicaoIniciativa()
    {
        string fonteRecurso;
        fonteRecurso = ObtemSiglaFonteRecurso();

        #region Comando SQL

        string comandoSql = string.Format(@"
DECLARE @Erro INT
DECLARE @MensagemErro nvarchar(2048)
    SET @Erro = 0

BEGIN TRAN

BEGIN TRY

DECLARE @CodigoEntidade INT,
        @CodigoUsuarioInclusao INT,
        @CodigoCategoria INT
        
    SET @CodigoEntidade = {2}
    SET @CodigoUsuarioInclusao = {3}
    SET @CodigoCategoria = NULL

DECLARE @CodigoProjeto int,
        @NomeIniciativa varchar(255),
        @CodigoGerenteIniciativa int,
        @NomeGerenteIniciativa varchar(60),
        @CodigoUnidadeNegocio int,
        @NomeUnidadeNegocio varchar(100),
        @DataInicio datetime,
        @DataTermino datetime,
        @ValorEstimado decimal(25,4),
        @PublicoAlvo varchar(500),
        @Justificativa varchar(8000),
        @IndicaTipoProjeto CHAR(2),
        @IndicaClassificacaoProjeto CHAR(2),
        @IndicaAreaAtuacao CHAR(2),
        @FonteRecurso CHAR(2),
        @UltimosResultados varchar(8000),
        @Beneficiarios varchar(500),
        @ObjetivoGeral varchar(8000),
        @CodigoMovimentoOrcamento int

    SET @ValorEstimado = 0
    SET @CodigoProjeto = {20}
    SET @NomeIniciativa = '{4}'
    SET @CodigoGerenteIniciativa = {5}
    SET @NomeGerenteIniciativa = '{6}'
    SET @CodigoUnidadeNegocio = {7}
    SET @NomeUnidadeNegocio = '{8}'
    SET @DataInicio = {9}
    SET @DataTermino = {10}
    SET @PublicoAlvo = '{11}'
    SET @Justificativa = '{12}'
    SET @IndicaTipoProjeto = '{13}'
    SET @IndicaClassificacaoProjeto = '{14}'
    SET @IndicaAreaAtuacao = '{15}'
    SET @FonteRecurso = '{16}'
    SET @UltimosResultados = '{17}'
    SET @Beneficiarios = '{18}'
    SET @ObjetivoGeral = '{19}'
    SET @CodigoMovimentoOrcamento = {21}

UPDATE {0}.{1}.TermoAbertura02
   SET [IndicaTipoProjeto]			= @IndicaTipoProjeto
     , [FonteRecurso]				= @FonteRecurso
     , [NomeIniciativa]				= @NomeIniciativa
     , [CodigoGerenteIniciativa]	= @CodigoGerenteIniciativa
     , [NomeGerenteIniciativa]		= @NomeGerenteIniciativa
     , [CodigoUnidadeNegocio]		= @CodigoUnidadeNegocio
     , [NomeUnidadeNegocio]			= @NomeUnidadeNegocio
     , [DataInicio]					= @DataInicio
     , [DataTermino]				= @DataTermino
     , [ValorEstimado]				= @ValorEstimado
     , [PublicoAlvo]				= @PublicoAlvo
     , [Justificativa]				= @Justificativa
     , [IndicaClassificacaoProjeto]	= @IndicaClassificacaoProjeto
     , [UltimosResultados]			= @UltimosResultados
     , [IndicaAreaAtuacao]			= @IndicaAreaAtuacao
     , [Beneficiarios]				= @Beneficiarios
     , [ObjetivoGeral]              = @ObjetivoGeral
     , [CodigoMovimentoOrcamento]   = @CodigoMovimentoOrcamento
 WHERE [CodigoProjeto] = @CodigoProjeto

    SET @Erro = @Erro + @@ERROR

UPDATE {0}.{1}.Projeto
   SET [NomeProjeto] = @NomeIniciativa
      ,[DataUltimaAlteracao] = GETDATE()
      ,[CodigoUsuarioUltimaAlteracao] = @CodigoUsuarioInclusao
      ,[CodigoGerenteProjeto] = @CodigoGerenteIniciativa
      ,[CodigoUnidadeNegocio] = @CodigoUnidadeNegocio
      ,[DescricaoProposta] = @ObjetivoGeral
 WHERE [CodigoProjeto] = @CodigoProjeto

    SET @Erro = @Erro + @@ERROR

END TRY
BEGIN CATCH
    SET @Erro = ERROR_NUMBER()
    SET @MensagemErro = ERROR_MESSAGE()
END CATCH

IF @Erro = 0
BEGIN
    COMMIT
END
ELSE
BEGIN
    ROLLBACK
END


 SELECT @Erro AS CodigoErro, 
        @MensagemErro AS MensagemErro"
            , cDados.getDbName()
            , cDados.getDbOwner()
            , CodigoEntidadeUsuarioResponsavel
            , CodigoUsuarioResponsavel
            , txtNomeProjeto.Text.Replace("'", "''")
            , cmbLider.Value ?? "NULL"
            , cmbLider.Text.Replace("'", "''")
            , cmbUnidadeGestora.Value ?? "NULL"
            , cmbUnidadeGestora.Text.Replace("'", "''")
            , deDataInicio.Value == null ? "NULL" : string.Format(" CAST('{0}'     AS smalldatetime)", deDataInicio.Value)
            , deDataTermino.Value == null ? "NULL" : string.Format(" CAST('{0}'     AS smalldatetime)", deDataTermino.Value)
            , txtPublicoAlvo.Text.Replace("'", "''")
            , txtJustificativa.Text.Replace("'", "''")
            , rblTipoProjeto.Value ?? "NULL"
            , rblSituacaoProjeto.Value ?? "NULL"
            , rblAreaAtuacao.Value ?? "NULL"
            , fonteRecurso
            , txtResultadosContinuidade.Text.Replace("'", "''")
            , txtBeneficiarios.Text.Replace("'", "''")
            , txtObjetivoGeral.Text.Replace("'", "''")
            , CodigoProjeto
            , cmbAnoOrcamento.Value ?? "NULL"
            , cmbAnoOrcamento.Text.Replace("'", "''") 
            );

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            object codigoErro = ds.Tables[0].Rows[0]["CodigoErro"];
            string mensagemErro = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
            if (!(Convert.IsDBNull(codigoErro) || codigoErro.Equals(0)))
            {
                if (mensagemErro.Contains("UQ_Projeto_NomeProjeto"))
                    throw new Exception("Nome de projeto já existente.");
                else
                    throw new Exception(mensagemErro.ToString());
            }
        }
    }

    private string ObtemSiglaFonteRecurso()
    {
        if (cbFundecoop.Checked && cbRecursoProprio.Checked)
            return "AO";//Ambas opções
        if (cbFundecoop.Checked)
            return "FU";//FUNDECOOP
        if (cbRecursoProprio.Checked)
            return "RP";//Recurso Próprio

        return "NO";//Nenhuma das opções
    }

    private static string ObtemDescricaoPeloCodigo(string codigo, string nomeColunaCodigo, string nomeColunaDescricao, SqlDataSource sqlDataSource)
    {
        DataRowView row;
        ObtemRegistroUnicoPeloCodigo(codigo, nomeColunaCodigo, sqlDataSource, out row);

        string descricao = string.Empty;
        if (row != null)
            descricao = row[nomeColunaDescricao].ToString();

        return descricao;
    }

    private static bool ObtemRegistroUnicoPeloCodigo(string codigo, string nomeColunaCodigo, SqlDataSource sqlDataSource, out DataRowView row)
    {
        bool existeRegistros = false;
        row = null;
        //Armazena as definições iniciais de filtro
        string filterExpression = sqlDataSource.FilterExpression;
        Parameter[] parameters = new Parameter[sqlDataSource.FilterParameters.Count];
        sqlDataSource.FilterParameters.CopyTo(parameters, 0);

        //Define o filtro pelo código informado
        sqlDataSource.FilterExpression = string.Format("{0} = {1}", nomeColunaCodigo, codigo);
        sqlDataSource.FilterParameters.Clear();
        sqlDataSource.FilterParameters.Add(nomeColunaCodigo, codigo);

        //Busca o registro
        DataView itens = (DataView)sqlDataSource.Select(new DataSourceSelectArguments());

        //Redefine as definições iniciais de filtro previamente armazenadas
        sqlDataSource.FilterExpression = filterExpression;
        sqlDataSource.FilterParameters.Clear();
        foreach (Parameter param in parameters)
            sqlDataSource.FilterParameters.Add(param);

        if (itens.Count > 0)
        {
            row = itens[0];
            existeRegistros = true;
        }
        return existeRegistros;
    }

    private bool VerificaExisteDependeciaAcao(string codigo, SqlDataSource[] sources, string nomeColunaCodigo)
    {
        foreach (SqlDataSource sqlDataSource in sources)
        {
            DataRowView row;
            bool existeRegistros = ObtemRegistroUnicoPeloCodigo(codigo, nomeColunaCodigo, sqlDataSource, out row);

            if (existeRegistros)
                return true;
        }
        return false;
    }

    private string ObtemValorPelaChave(OrderedDictionary dic, string key, bool indicaValorNumerico)
    {
        string format = null;
        return ObtemValorPelaChave(dic, key, indicaValorNumerico, format);
    }

    private string ObtemValorPelaChave(OrderedDictionary dic, string key, bool indicaValorNumerico, string format)
    {
        if (string.IsNullOrEmpty(format))
            format = "{0}";
        if (indicaValorNumerico)
            return string.Format(format, dic[key]);
        return string.Format(format, dic[key]);
    }

    #endregion

    protected void gvResultadosEsperados_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

        string nomeColunaCodigo = "CodigoIndicador";
        string nomeColunaDescricao = "NomeIndicador";
        string Meta = e.NewValues["DescricaoResultado"].ToString();
        string codigo = null; //e.NewValues["CodigoIndicador"].ToString();
        string descricao = null;
        string nomeColunaNomeIndicador = "DescricaoIndicadorOperacional";
        if (!string.IsNullOrEmpty(e.NewValues["CodigoIndicador"] + ""))
        {
            codigo = e.NewValues["CodigoIndicador"].ToString();
            descricao = ObtemDescricaoPeloCodigo(
               codigo, nomeColunaCodigo, nomeColunaDescricao, sdsIndicadorOperacional);
        }
        if (e.NewValues.Contains(nomeColunaNomeIndicador))
            e.NewValues[nomeColunaNomeIndicador] = descricao;
        else
            e.NewValues.Add(nomeColunaNomeIndicador, descricao);

        //// testar duplicidade
        //if (cDados.verificaDuplicidadeResultadoEsperado(codigo, _codigoProjeto, Meta ) )
        //{
        //throw new Exception("Já existe um Resultado com este indicador, cadastrado para este projeto,\nNão é permitido inclusão em duplicidade! ");
        //}

    }
    protected void gvResultadosEsperados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string nomeColunaCodigo = "CodigoIndicador";
        string nomeColunaDescricao = "NomeIndicador";
        string Meta = e.NewValues["DescricaoResultado"].ToString();
        string codigo = null; //e.NewValues["CodigoIndicador"].ToString();
        string descricao = null;
        string nomeColunaNomeIndicador = "DescricaoIndicadorOperacional";
        if (!string.IsNullOrEmpty(e.NewValues["CodigoIndicador"] + ""))
        {
            codigo = e.NewValues["CodigoIndicador"].ToString();
            descricao = ObtemDescricaoPeloCodigo(
               codigo, nomeColunaCodigo, nomeColunaDescricao, sdsIndicadorOperacional);
        }
        if (e.NewValues.Contains(nomeColunaNomeIndicador))
            e.NewValues[nomeColunaNomeIndicador] = descricao;
        else
            e.NewValues.Add(nomeColunaNomeIndicador, descricao);

        //// testar duplicidade
        //if (cDados.verificaDuplicidadeResultadoEsperado(codigo, _codigoProjeto, Meta))
        //{
        //    throw new Exception("Já existe um Resultado com este indicador, cadastrado para este projeto,\nNão é permitido inclusão em duplicidade! ");
        //}
    }

    protected void gvResultadosEsperados_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.VisibleIndex == 0)
            hfGeral.Set("PodeSalvarResultados", "S");
        string codigoIndicador = "";
        if (e.RowType == GridViewRowType.Data)
        {
            codigoIndicador = gvResultadosEsperados.GetRowValues(e.VisibleIndex, "CodigoIndicador") + "";

        }

        if (codigoIndicador == "")
            hfGeral.Set("PodeSalvarResultados", "N");
        gvResultadosEsperados.JSProperties["cp_PodeSalvar"] = hfGeral.Get("PodeSalvarResultados").ToString();

    }

    private void carregaGridPlanoTrabalho()
    {
        DataSet ds = cDados.getPlanoTrabalhoProjeto(CodigoProjeto, "");

        gvDados.Columns["Parcerias"].Caption = "<table  border='0' cellpadding='5' style='height:35px;width:100%' cellspacing='0'><tr><td style='width:50%;border-right:solid 1px #808080;'>Área Parceira</td><td >Elementos da Parceria</td></tr></table>";

        gvDados.Columns["Parcerias"].HeaderStyle.Paddings.Padding = 0;

        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        gvDados.Settings.VerticalScrollableHeight = altura - 405;


        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();

            gvDados.JSProperties["cp_Sequencia"] = ds.Tables[0].Select("Codigo = CodigoPai").Length + 1;
        }

        dsMetas = cDados.getMetasPlanoTrabalhoProjeto(CodigoProjeto, "");
        dsParcerias = cDados.getParceriasPlanoTrabalhoProjeto(CodigoProjeto, "");
        dsMarcos = cDados.getMarcosPlanoTrabalhoProjeto(CodigoProjeto, "");
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Name == "Metas")
        {
            if (e.VisibleIndex == 0)
            {
                hfGeral.Set("PodeSalvarAtividade", "S");
                hfGeral.Set("PodeSalvarAcao", "S");
                hfGeral.Set("PodeSalvarMetas", "S");
                
            }
            string codigo = gvDados.GetRowValues(e.VisibleIndex, "Codigo").ToString();
            string codigoPai = gvDados.GetRowValues(e.VisibleIndex, "CodigoPai").ToString();
            //e.Cell.Style.Add("background-position", "center");
            //e.Cell.Style.Add("background-image", "url('../../imagens/pixelCinza.png')");
            //e.Cell.Style.Add("background-repeat", "repeat-y");
            string strMeta = getTabelaMetas(e.CellValue + "");
            //e.Cell.Style.Add("Padding", "0px");
            string Inicio = gvDados.GetRowValues(e.VisibleIndex, "Inicio").ToString();
            e.Cell.Text = strMeta == "" ? "&nbsp;" : strMeta;

            if (codigo == codigoPai )
            {
                if(strMeta == "")
                   AcaoSemMeta += 1;
            }

            if (Inicio == "")
                AcaoSemAtividade += 1;

            if (codigo != codigoPai && strMeta == "")
            {
                hfGeral.Set("PodeSalvarAtividade", "N");
            }
            
            hfGeral.Set("PodeSalvarMetas", AcaoSemMeta == 0 ? "S" : "N");
            hfGeral.Set("PodeSalvarAcao", AcaoSemAtividade == 0 ? "S" : "N");
            gvDados.JSProperties["cp_PodeSalvar"] = hfGeral.Get("PodeSalvarAtividade").ToString(); // == ) ? hfGeral.Get("PodeSalvarAtividade").ToString() : "N";
            gvDados.JSProperties["cp_PodeSalvarMetas"] = hfGeral.Get("PodeSalvarMetas").ToString();
            gvDados.JSProperties["cp_PodeSalvarAcao"] = hfGeral.Get("PodeSalvarAcao").ToString();
            e.Cell.Style.Add("Padding", "0px");
        }
        else if (e.DataColumn.Name == "Parcerias")
        {
            e.Cell.Style.Add("background-position", "center");
            e.Cell.Style.Add("background-image", "url('../../imagens/pixelCinza.png')");
            e.Cell.Style.Add("background-repeat", "repeat-y");
            e.Cell.Text = getTabelaParcerias(e.CellValue + "");
            e.Cell.Style.Add("Padding", "0px");
        }
        else if (e.DataColumn.Name == "Marcos")
        {
            e.Cell.Text = getTabelaMarcos(e.CellValue + "");
            e.Cell.Style.Add("Padding", "0px");
        }
        else if (e.DataColumn.Name == "Institucional")
        {
            e.Cell.Text = e.CellValue + "" == "S" ? "Sim" : e.CellValue + "" == "N" ? "Não" : e.CellValue.ToString();
        }
    }

    private string getTabelaMetas(string codigoObjeto)
    {
        codigoObjeto = codigoObjeto == "" ? "-1" : codigoObjeto;

        DataRow[] drs = dsMetas.Tables[0].Select("Codigo = " + codigoObjeto);

        string linhas = "";

        string estiloBorda = drs.Length > 1 ? "border-top:solid 1px #808080" : "";

        int index = 0;

        foreach (DataRow dr in drs)
        {
            linhas += string.Format(@"<tr><td align='left' style='{1}'>{0}</td></tr>", dr["Meta"], index == 0 ? "border-top:0px" : estiloBorda);
            index++;
        }

        if (linhas == "")
            return "";
        else
            return string.Format(@"<table border='0' cellpadding='5' cellspacing='0' style='width:100%;'>{0}</table>", linhas);
    }

    private string getTabelaParcerias(string codigoObjeto)
    {
        codigoObjeto = codigoObjeto == "" ? "-1" : codigoObjeto;

        DataRow[] drs = dsParcerias.Tables[0].Select("Codigo = " + codigoObjeto + " OR CodigoObjetoPai = " + codigoObjeto);

        string linhas = "";

        string estiloBorda = drs.Length > 1 ? "border-top:solid 1px #808080;" : "";

        int index = 0;

        foreach (DataRow dr in drs)
        {
            linhas += string.Format(@"<tr><td align='left' style='height:100%;width:50%;border-right:solid 1px #808080;{2}'>{0}</td><td align='left' style='{2}'>{1}</td></tr>", dr["Area"], dr["Elemento"], index == 0 ? "border-top:0px" : estiloBorda);
            index++;
        }

        return string.Format(@"<table border='0' cellpadding='5' cellspacing='0' style='width:100%'>{0}</table>", linhas);
    }

    private string getTabelaMarcos(string codigoObjeto)
    {
        codigoObjeto = codigoObjeto == "" ? "-1" : codigoObjeto;

        DataRow[] drs = dsMarcos.Tables[0].Select("Codigo = " + codigoObjeto + " OR CodigoObjetoPai = " + codigoObjeto);

        string linhas = "";

        string estiloBorda = drs.Length > 1 ? "border-top:solid 1px #808080;" : "";

        int index = 0;

        foreach (DataRow dr in drs)
        {
            linhas += string.Format(@"<tr><td align='left' style='{1}'>{0}</td></tr>", dr["MarcoComData"], index == 0 ? "border-top:0px" : estiloBorda);
            index++;
        }

        return string.Format(@"<table border='0' cellpadding='5' cellspacing='0' style='width:100%'>{0}</table>", linhas);
    }

    protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (gvDados.GetRowValues(e.VisibleIndex, "Codigo") + "" == gvDados.GetRowValues(e.VisibleIndex, "CodigoPai") + "")
            {
                e.Row.Style.Add("background-color", "#CCCCCC");
            }
        }
    }

    public string getBotoesLinha()
    {
        string imgEdicao = "", imgExclusao = "", imgNovaAtividade = "";

        string faseAcaoAtividade = Eval("EtapaAcaoAtividade") + "";

        if (Eval("Codigo").ToString() == Eval("CodigoPai").ToString())
        {
            if (podeIncluirAtividade && (faseTAI != "R" || faseAcaoAtividade == "R"))
                imgNovaAtividade = string.Format(@"<img alt='Incluir Atividade na Ação' onclick='incluiAtividade({0}, {1}, ""{2}"")' src='../../imagens/botoes/NovaAtividade.png' style='cursor:pointer' />", Eval("Codigo"), Eval("Numero"), faseTAI);
            else
                imgNovaAtividade = string.Format(@"<img src='../../imagens/botoes/NovaAtividadeDes.png' />");

            if (podeEditarAcao && (faseTAI != "R" || faseAcaoAtividade == "R"))
                imgEdicao = string.Format(@"<img alt='Editar Ação' onclick='editaAcao({0})' src='../../imagens/botoes/editarReg02.PNG' style='cursor:pointer' />", Eval("Codigo"));
            else
                imgEdicao = string.Format(@"<img src='../../imagens/botoes/editarRegDes.png' />");

            if (podeExcluirAcao && (faseTAI != "R" || faseAcaoAtividade == "R"))
                imgExclusao = string.Format(@"<img alt='Excluir Ação' onclick='excluiAcao({0})' src='../../imagens/botoes/excluirReg02.PNG' style='cursor:pointer' />", Eval("Codigo"));
            else
                imgExclusao = string.Format(@"<img src='../../imagens/botoes/excluirRegDes.png' />");

            return string.Format("<table><tr><td>{0}</td><td>{1}</td><td>{2}</td></tr></table>", imgNovaAtividade, imgEdicao, imgExclusao);
        }
        else
        {
            if (podeEditarAtividade && (faseTAI != "R" || faseAcaoAtividade == "R"))
                imgEdicao = string.Format(@"<img alt='Editar Atividade' onclick='editaAtividade({0}, {1})' src='../../imagens/botoes/editarReg02.PNG' style='cursor:pointer' />", Eval("Codigo"), Eval("CodigoPai"));
            else
                imgEdicao = string.Format(@"<img src='../../imagens/botoes/editarRegDes.png' />");

            if (podeExcluirAtividade && (faseTAI != "R" || faseAcaoAtividade == "R"))
                imgExclusao = string.Format(@"<img alt='Excluir Atividade' onclick='excluiAtividade({0})' src='../../imagens/botoes/excluirReg02.PNG' style='cursor:pointer' />", Eval("Codigo"));
            else
                imgExclusao = string.Format(@"<img src='../../imagens/botoes/excluirRegDes.png' />");

            return string.Format("<table><tr><td>{0}</td><td>{1}</td></tr></table>", imgEdicao, imgExclusao);
        }
    }

    protected void callbackPlanoTrabalho_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callbackPlanoTrabalho.JSProperties["cp_Msg"] = "";
        callbackPlanoTrabalho.JSProperties["cp_Erro"] = "";
        callbackPlanoTrabalho.JSProperties["cp_Sucesso"] = "";


        if (e.Parameter != "")
        {
            int codigoAcao = int.Parse(e.Parameter);

            bool podeExcluirAcao = true; // cDados.verificaExclusaoPlanoTrabalho(codigoAcao);

            if (podeExcluirAcao)
            {

                bool retorno = cDados.excluiAllAtividadeAcaoIniciativa(CodigoProjeto, codigoAcao);

                //if (retorno)
                //{
                //    retorno = cDados.excluiAcaoIniciativa(CodigoProjeto, codigoAcao);
                //}
                //else
                //{
                //    callbackAtividade.JSProperties["cp_Msg"] = "Erro ao excluir a(s) atividade(s) relacionada(s)!";
                //    callbackAtividade.JSProperties["cp_Status"] = "0";
                //}


                if (retorno)
                {
                    callbackPlanoTrabalho.JSProperties["cp_Sucesso"] = "Ação excluída com sucesso!";
                    callbackPlanoTrabalho.JSProperties["cp_Status"] = "1";
                }
                else
                {
                    callbackPlanoTrabalho.JSProperties["cp_Erro"] = "Erro ao excluir a ação!";
                    callbackPlanoTrabalho.JSProperties["cp_Status"] = "0";
                }
            }
            else
            {
                callbackPlanoTrabalho.JSProperties["cp_Msg"] = "A ação não pode ser excluída! Existem metas e/ou atividades associadas.";
                callbackPlanoTrabalho.JSProperties["cp_Status"] = "0";
            }
        }
        else
            callbackPlanoTrabalho.JSProperties["cp_Status"] = "0";
    }

    protected void callbackAtividade_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callbackAtividade.JSProperties["cp_Msg"] = "";
        callbackAtividade.JSProperties["cp_Erro"] = "";
        callbackAtividade.JSProperties["cp_Sucesso"] = "";

        if (e.Parameter != "")
        {
            int codigoAcao = int.Parse(e.Parameter);

            bool podeExcluirAcao = true; //cDados.verificaExclusaoAtividadeAcaoIniciativa(codigoAcao);

            if (podeExcluirAcao)
            {
                bool retorno = cDados.excluiAtividadeAcaoIniciativa(CodigoProjeto, codigoAcao);

                if (retorno)
                {
                    callbackAtividade.JSProperties["cp_Sucesso"] = "Atividade excluída com sucesso!";
                    callbackAtividade.JSProperties["cp_Status"] = "1";
                }
                else
                {
                    callbackAtividade.JSProperties["cp_Erro"] = "Erro ao excluir a atividade!";
                    callbackAtividade.JSProperties["cp_Status"] = "0";
                }
            }
            else
            {
                callbackAtividade.JSProperties["cp_Msg"] = "A atividade não pode ser excluída! Existem parceiros, produtos e/ou marcos associados.";
                callbackAtividade.JSProperties["cp_Status"] = "0";
            }
        }
        else
            callbackAtividade.JSProperties["cp_Status"] = "0";
    }

    protected void callbackConflitos_CustomJSProperties(object sender, DevExpress.Web.CustomJSPropertiesEventArgs e)
    {
        if (!e.Properties.ContainsKey("cpExecutaVerificacao"))
            e.Properties.Add("cpExecutaVerificacao", true);

        if (!e.Properties.ContainsKey("cpMudaStatusProposta"))
            e.Properties.Add("cpMudaStatusProposta", true);

        if (!e.Properties.ContainsKey("cpPossuiConflitos"))
            e.Properties.Add("cpPossuiConflitos", false);
    }

    protected void callbackConflitos_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string comandoSql = "";
        if (e.Parameter == "S")
        {
            comandoSql = string.Format("EXEC [p_GetConflitosAgenda] {0},{1}"
               , CodigoProjeto, CodigoEntidadeUsuarioResponsavel);
            DataSet ds = cDados.getDataSet(comandoSql);
            callbackConflitos.JSProperties["cpPossuiConflitos"] =
                ds.Tables[0].Rows.Count > 0;
        }
        else
        {
            try
            {
                int registrosAfetados = 0;

                comandoSql = string.Format(@"BEGIN
                                                    DECLARE @l_NovoStatus                    Varchar(10)
                                                    SET @l_NovoStatus = 'Em análise'
                                                  if exists (select 1 from  dbo.TermoAbertura02 
                                                           where CodigoProjeto = {0}
                                                             and status = 'Criação'    )
                                                  Begin             
                                                     update dbo.TermoAbertura02 set Status = @l_NovoStatus
                                                      where CodigoProjeto = {0}
      
                                                     UPDATE EventoInstitucional 
	                                                   SET Rotulo = 12
	                                                   where CodigoProjeto = {0}
      
                                                  End
                                              END", CodigoProjeto);
                //DataSet ds = cDados.getDataSet(comandoSql);
                cDados.execSQL(comandoSql, ref registrosAfetados);
                callbackConflitos.JSProperties["cpMudaStatusProposta"] = false;
            }

            catch (Exception)
            {

            }
        }

    }

    private void carregaAbaCronograma()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        string urlCronogramaOrcamentario = "CronogramaOrcamentarioNovo.aspx?FaseTAI=" + faseTAI;

        string frm = string.Format(@"<iframe id=""frmCronograma"" name=""frmCronograma"" frameborder=""0"" height=""{2}"" scrolling=""no"" src=""{4}&RO={0}&CP={1}&ALT={3}""
                                    width=""100%""></iframe>", podeIncluir ? "N" : "S"
                                                             , CodigoProjeto
                                                             , altura - 380
                                                             , altura - 470
                                                             , urlCronogramaOrcamentario);

        pageControl.TabPages.FindByName("tabCronograma").Controls.Add(cDados.getLiteral(frm));
    }

    protected void sdsIndicadorEstrategico_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        if (gvEstrategico.IsEditing && !gvEstrategico.IsNewRowEditing)
        {
            object codigoIndicador = gvEstrategico.GetRowValues(gvEstrategico.FocusedRowIndex, "CodigoIndicador");
            if (codigoIndicador == null || Convert.IsDBNull(codigoIndicador))
                codigoIndicador = -1;
            e.Command.CommandText = string.Format(@"
 SELECT i.[CodigoIndicador]
      , i.[NomeIndicador]  as Meta
      , ioe.[CodigoObjetivoEstrategico] AS CodigoSuperior
   FROM [dbo].[IndicadorUnidade] AS [iu] INNER JOIN 
        [dbo].[Indicador] AS [i] ON (i.[CodigoIndicador]     = iu.[CodigoIndicador]
                                 AND i.[DataExclusao]         IS NULL ) INNER JOIN 
        [dbo].[IndicadorObjetivoEstrategico]  AS [ioe]    ON (ioe.CodigoIndicador    = i.[CodigoIndicador]   )
  WHERE iu.[DataExclusao] IS NULL
    AND iu.[CodigoUnidadeNegocio]   = @CodigoEntidade
    AND NOT EXISTS (SELECT 1 
                      FROM tai02_ObjetivosEstrategicos t 
                     WHERE t.CodigoProjeto = @CodigoProjeto 
                       AND t.CodigoIndicador <> {0}
                       AND t.CodigoIndicador = i.CodigoIndicador 
                       AND t.CodigoObjetoEstrategia = ioe.CodigoObjetivoEstrategico)
  ORDER BY 2", codigoIndicador);
        }
    }
    protected void sdsOpcoesLinhaAtuacao_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        if (gvLinhaAcao.IsEditing && !gvLinhaAcao.IsNewRowEditing)
        {
            object codigoAcaoSugerida = gvLinhaAcao.GetRowValues(gvLinhaAcao.FocusedRowIndex, "CodigoAcaoSugerida");

            e.Command.CommandText = string.Format(@"
 SELECT a.[CodigoAcaoSugerida], 
        a.[DescricaoAcao],
        a.[CodigoObjetoAssociado] AS CodigoSuperior
   FROM [AcoesSugeridas]       AS [a]
  WHERE a.[CodigoTipoAssociacao] = dbo.f_GetCodigoTipoAssociacao('OB')
    AND a.[DataDesativacao]                  IS NULL
    AND a.CodigoEntidade = @CodigoEntidade
    AND NOT EXISTS(SELECT 1 
                     FROM tai02_LinhaDeAcao l 
                    WHERE l.CodigoAcaoSugerida <> {0}
                      AND l.CodigoAcaoSugerida = a.CodigoAcaoSugerida 
                      AND l.CodigoProjeto = @CodigoProjeto)
  ORDER BY
        a.[DescricaoAcao]", codigoAcaoSugerida);
        }
    }

    private string podeSalvarCronogramaOrcamentario()
    {
        string podeSalvar = "S";
      
        string select1 = string.Format(@"SELECT 1 as existe
                                        FROM {0}.{1}.tai02_AcoesIniciativa ai 
                                    WHERE CodigoAcao = CodigoAcaoSuperior
                                        AND fonteRecurso <> 'SR'
                                        AND NOT EXISTS (SELECT 1 FROM {0}.{1}.CronogramaOrcamentarioAcao co WHERE co.CodigoAcao = ai.codigoAcao)
                                        AND ai.CodigoProjeto = {2} ", cDados.getDbName(), cDados.getDbOwner(), CodigoProjeto);

        string select2 = string.Format(@"union all  SELECT 1 as existe
                                        FROM {0}.{1}.CronogramaOrcamentarioAcao co 
                                    WHERE co.CodigoProjeto = {2} 
                                    and co.ValorProposto = 0   ", cDados.getDbName(), cDados.getDbOwner(), CodigoProjeto);

        string select3 = string.Format(@"union all  SELECT 1 as existe
                                        FROM {0}.{1}.CronogramaOrcamentarioAcao co 
                                    WHERE co.CodigoProjeto = {2} 
                                        and isnull(co.memoriacalculo,'') = '' ", cDados.getDbName(), cDados.getDbOwner(), CodigoProjeto);

        string select4 = string.Format(@"union all  SELECT 1 as existe
										FROM {0}.{1}.CronogramaOrcamentarioAcao co inner join
											{0}.{1}.tai02_AcoesIniciativa ta on ta.CodigoProjeto = co.CodigoProjeto
																		and ta.CodigoAcao = co.CodigoAcao    
									WHERE co.CodigoProjeto = {2} 
										and (ABS(co.ValorSuplemento)+ABS(co.valorTransposto)) = 0 
										and ta.EtapaAcaoAtividade = 'R' ", cDados.getDbName(), cDados.getDbOwner(), CodigoProjeto);

        string comandoSql = "";
        if (faseTAI == "R")
        {
            select2 = "";
        }
        else
        {
            select4 = "";
        }

        comandoSql = string.Format(@"select existe from (                                              
                                                        {3}                                         
                                                                                                   
                                                        {4}
                                                       
                                                        {5} 
                                                       
                                                        {6}                                                       
                                                        ) conta 
                                                        where conta.existe > 0 
                                            ", cDados.getDbName(), cDados.getDbOwner(), CodigoProjeto, select1, select2, select3, select4);
        DataSet ds = cDados.getDataSet(comandoSql);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            podeSalvar = "N";

        return podeSalvar;
    }

    private void carregaComboAnoOrcamento()
    {
        string swhere = "";
        if (CodigoProjeto == -1)
        {
           swhere = " and ano >= YEAR(GETDATE()) ";
        }

        DataSet ds = cDados.getAnoOrcamento(CodigoEntidadeUsuarioResponsavel, swhere);

        if (cDados.DataSetOk(ds))
        {
            cmbAnoOrcamento.DataSource = ds;
            cmbAnoOrcamento.ValueField = "codigoMovimentoOrcamento";
            cmbAnoOrcamento.DataBind();
        }

        cmbAnoOrcamento.ClientEnabled = CodigoProjeto == -1;
        
    }

    protected void callbackIniciarFluxo_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string comandoSQL = string.Format(@"SELECT codigoUsuarioBloqueio FROM {0}.{1}.TermoAbertura02 WHERE CodigoProjeto = {2}", cDados.getDbName(), cDados.getDbOwner(), CodigoProjeto);

            DataSet dsBloqueio = cDados.getDataSet(comandoSQL);

            if (cDados.DataSetOk(dsBloqueio) && cDados.DataTableOk(dsBloqueio.Tables[0]) && dsBloqueio.Tables[0].Rows[0]["codigoUsuarioBloqueio"].ToString() == "")
            {
                comandoSQL = string.Format(@"UPDATE {0}.{1}.TermoAbertura02 SET InBloqueioReformulacao = 'S'
                                                                              ,codigoUsuarioBloqueio = {3} 
                                                                              ,DataUltimoBloqueio = GetDate()
                                                                              ,EtapaOrcamento = 3
                                             WHERE CodigoProjeto = {2}", cDados.getDbName(), cDados.getDbOwner(), CodigoProjeto, CodigoUsuarioResponsavel);

                int regAf = 0;

                cDados.execSQL(comandoSQL, ref regAf);
            }
            else
            {
                callback.ClientSideEvents.EndCallback = @"function(s, e)
                                                          {
                                                            window.top.mostraMensagem('Outro usuário já iniciou o fluxo!', 'atencao', true, false, null);
                                                            window.open(btnNao.cp_URLDirecionamento, '_self');
                                                          }";
            }            
    }
}
