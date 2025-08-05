using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Specialized;
using DevExpress.Web;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;

public partial class _Projetos_Administracao_propostaDeIniciativa_008 : System.Web.UI.Page
{
    protected int codigoModeloFormulario;
    protected int codigoFormulario;
    bool exibirAbaCaracterizacaoProjeto;
    bool origemMenuProjeto;

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
            sdsLider.ConnectionString =
            sdsEquipe.ConnectionString =
            sdsResponsaveisAcao.ConnectionString =
            sdsUnidadeGestora.ConnectionString =
            sdsProdutoIntermediario.ConnectionString =
            sdsAcoesIniciativa.ConnectionString =
            sdsPremissa.ConnectionString =
            sdsParceiro.ConnectionString =
            sdsRestricao.ConnectionString =
            sdsProdutoFinalResultadosEsperados.ConnectionString = 
            sdsGrupoServico.ConnectionString = value;
            _connectionString = value;
        }
    }

    private int _codigoProjeto;
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

    private int _codigoInstanciaWf;
    /// <summary>
    /// Código da instância de workflow em que a tela está sendo exibida.
    /// Um valor não positivo indica uma das duas possibilidades:
    /// 1. A instância ainda não foi criada;
    /// 2. A tela não está sendo exibida dentro de uma instância de fluxo. Neste caso, CodigoWorkflow será <=0.
    /// </summary>
    public int CodigoInstanciaWf
    {
        get { return _codigoInstanciaWf; }
        set
        {
            _codigoInstanciaWf = value;
        }
    }

    private int _codigoWorkflow;
    /// <summary>
    /// Código do workflow dentro do qual a tela está sendo exibida
    /// Um valor não positivo indica que a tela não está sendo exibida dentro de um workflow
    /// </summary>
    public int CodigoWorkflow
    {
        get { return _codigoWorkflow; }
        set
        {
            _codigoWorkflow = value;
        }
    }

    /// <summary>
    /// Indica se é preciso gravar nova instância no momento em que o formulário for salvo.
    /// Só será preciso gravar nova instância quando estiver dentro de um fluxo (CodigoWorkflow>0) e a 
    /// a instância ainda não estiver sido criada (CodigoInstanciaWf <= 0);
    /// </summary>
    public bool PrecisaGravarNovaInstanciaWf
    {
        get
        {
            return ((CodigoWorkflow > 0) && (CodigoInstanciaWf <= 0));
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

    #endregion

    #region Private Fields
    private bool readOnly;
    private bool podeEditar;
    private bool podeIncluir;
    private bool podeExcluir;
    //private bool temOrcamentoAtivo;
    //private bool trimestre01;
    //private bool trimestre02;
    //private bool trimestre03;
    //private bool trimestre04;
    private dados cDados;

    public string alturaTela = "", readOnlyInf = "";
    public string dvForm = "";

    #endregion

    #region Event Handlers

    #region Page

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        OrderedDictionary listaParametrosDados = new OrderedDictionary();
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


        int activeTabIndex;
        int alturaInt;
        if (!IsPostBack)
        {
            if (int.TryParse(Request.QueryString["tab"], out activeTabIndex))
                pageControl.ActiveTabIndex = activeTabIndex;

            int altura = this.getAlturaTela() - 300;

            string menuProjeto = (Request.QueryString["OrigemMenuProjeto"] + "").Trim();
            if(menuProjeto == "S")
            {
                dv01.Style.Add("max-height", (altura + 40).ToString() + "px");
                dv3.Style.Add("max-height", (altura + 40).ToString() + "px");
                dv4.Style.Add("max-height", (altura + 40).ToString() + "px");
                divpopup.Style.Add("max-height", (altura + 25).ToString() + "px");
            }
            else
            {
                dv01.Style.Add("max-height", (altura).ToString() + "px");
                dv3.Style.Add("max-height", (altura).ToString() + "px");
                dv4.Style.Add("max-height", (altura).ToString() + "px");
                divpopup.Style.Add("max-height", (altura - 25).ToString() + "px");
            }


            divpopup.Style.Add("overflow", "auto");
            dv01.Style.Add("overflow", "auto");
            dv3.Style.Add("overflow", "auto");
            dv4.Style.Add("overflow", "auto");
        }
        if (int.TryParse(Request.QueryString["Altura"], out alturaInt))
            alturaTela = (alturaInt + 30).ToString();

        if (!string.IsNullOrEmpty(alturaTela))
        {

            dv01.Style.Add("height", alturaTela + "px");
            dv3.Style.Add("height", alturaTela + "px");
            dv4.Style.Add("height", alturaTela + "px");

            dv01.Style.Add("overflow", "auto");
            dv3.Style.Add("overflow", "auto");
            gvAcoes.SettingsPopup.EditForm.Height = alturaInt - 20;
            dvForm = "style='height: " + (alturaInt - 260) + "px; overflow: auto;'";
        }
        else
        {
            string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            alturaInt = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
            gvAcoes.SettingsPopup.EditForm.Height = alturaInt - 340;
            dvForm = "style='height: " + (alturaInt - 380) + "px; overflow: auto;'";
        }


        CodigoEntidadeUsuarioResponsavel = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade") ?? -1);
        CodigoUsuarioResponsavel = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado") ?? -1);
        
        string cp = Request.QueryString["CP"];
        cp = cp.Replace("|", "");
        CodigoProjeto = int.Parse(string.IsNullOrEmpty(cp) ? "-1" : cp);
        readOnly = "S".Equals(Request.QueryString["RO"]);
        exibirAbaCaracterizacaoProjeto = "S".Equals(Request.QueryString["ExibirAbaCaracterizacaoProjeto"]);
        origemMenuProjeto = "S".Equals(Request.QueryString["OrigemMenuProjeto"]);
        readOnlyInf = "S";

        //Verifica se usuário pode editar todo o TAI
        if (origemMenuProjeto && readOnly)
        {
            if (cDados.VerificaPermissaoUsuario(CodigoUsuarioResponsavel, CodigoEntidadeUsuarioResponsavel, CodigoProjeto, "null", "PR", 0, "null", "PR_EditaTAI"))
            {
                readOnly = false;
                readOnlyInf = "N";
            }
        }


        //verifica se usuário pode editar a aba de informações complementares somente se não tem a permissão anterior
        if (readOnly)
        {
            if (cDados.VerificaPermissaoUsuario(CodigoUsuarioResponsavel, CodigoEntidadeUsuarioResponsavel, CodigoProjeto, "null", "PR", 0, "null", "PR_EditaInfComp"))
                readOnlyInf = "N";
        }

        //pageControl.TabPages[2].ClientVisible = exibirAbaCaracterizacaoProjeto;

        podeEditar = !readOnly;
        podeExcluir = !readOnly;
        podeIncluir = !readOnly && !IndicaNovoProjeto;


        string cwf = Request.QueryString["CW"];
        string ci = Request.QueryString["CI"];

        CodigoWorkflow = int.Parse(string.IsNullOrEmpty(cwf) ? "0" : cwf);
        CodigoInstanciaWf = int.Parse(string.IsNullOrEmpty(ci) ? "0" : ci);
        DefineCodigoModeloFormulario();
        DefineCodigoFormulario();
        hfGeral.Set("CodigoProjeto", CodigoProjeto);
        hfGeral.Set("CodigoFormulario", codigoFormulario);
        hfGeral.Set("OrigemMenuProjeto", Request.QueryString["OrigemMenuProjeto"] + "");

        btnImprimir.ClientEnabled = !IndicaNovoProjeto;
        
        //var template = gvAcoes.Templates.EditForm;
        cDados.aplicaEstiloVisual(this);
        //gvAcoes.Templates.EditForm = template;
        gvResponsaveis.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

    }

    private int getAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int alturaTela = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 150);
    }

    private void DefineCodigoModeloFormulario()
    {
        string comandoSql = @"
SELECT CodigoModeloFormulario 
   FROM ModeloFormulario mf 
  WHERE mf.IniciaisFormularioControladoSistema LIKE 'CaracterizacaoProjeto'";
        DataSet ds = cDados.getDataSet(comandoSql);
        DataRowCollection rows = ds.Tables[0].Rows;
        if (rows.Count > 0)
            codigoModeloFormulario = (int)rows[0]["CodigoModeloFormulario"];
        else
            codigoModeloFormulario = -1;
    }

    private void DefineCodigoFormulario()
    {
        string comandoSql = string.Format(@"
         select f.codigoformulario from formulario f inner join 
                modeloformulario mf on f.codigomodeloformulario = mf.codigomodeloformulario and mf.codigomodeloformulario = {0}  inner join
                formularioprojeto fp on fp.codigoformulario = f.codigoformulario and fp.codigoproject = {1} ", codigoModeloFormulario, CodigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSql);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            codigoFormulario = ds.Tables[0].Rows[0]["codigoformulario"] + "" != "" ? codigoFormulario = int.Parse(ds.Tables[0].Rows[0]["codigoformulario"].ToString()) : -1;
        }
        else
            codigoFormulario = -1;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //Obtem a string de conexao e seta a propriedade 'ConnectionString' dos SQL Data Sources da página.
        ConnectionString = cDados.classeDados.getStringConexao();
        DefineVisibilidadeCampos();
        DefineCamposSomenteLeitura();
        //Se foi informado um código do projeto carrega dados formulário
        if (!IsPostBack && !IndicaNovoProjeto)
        {
            CarregaDadosFormulario();

        }

        /* DESCOMENTAR ESTE CÓDIGO APÓS LIBERAÇÃO DO ERICSSON */
        /*Busca relação de trimestres liberados*/
        //DataSet ds = cDados.getTrimestreLiberado(CodigoEntidadeUsuarioResponsavel);
        //DataSet ds = cDados.getTrimestreLiberado(CodigoEntidadeUsuarioResponsavel);
        //bool existeRegistros = false;
        //temOrcamentoAtivo = false;
        //foreach (DataRow row in ds.Tables[0].Rows)
        //{
            //existeRegistros = true;
            //temOrcamentoAtivo = true;
            //if (row["Trimestre"].ToString() == "1")
            //    trimestre01 = true;

            //if (row["Trimestre"].ToString() == "2")
            //    trimestre02 = true;

            //if (row["Trimestre"].ToString() == "3")
            //    trimestre03 = true;

            //if (row["Trimestre"].ToString() == "4")
            //    trimestre04 = true;
        //}

        //trimestre01 = true;
        //trimestre02 = true;
        //trimestre03 = true;
        //trimestre04 = true;

        //if (!existeRegistros)
        //{
        //    ASPxPopupMsg.ShowOnPageLoad = (!IsPostBack && !readOnly);
        //    readOnly = true;
        //    podeEditar = !readOnly;
        //    podeExcluir = !readOnly;
        //    podeIncluir = !readOnly;
        //    DefineVisibilidadeCampos();
        //    DefineCamposSomenteLeitura();
        //}

        //ASPxPopupMsgEdicaoTai.ShowOnPageLoad = (origemMenuProjeto && !readOnly);


        int codigoTipoProjeto = cDados.getCodigoTipoProjeto(CodigoProjeto);

        if (codigoTipoProjeto == 8)
        {
            constroiAbaElementosEstrategia();
        }

        gvAcoes.Settings.ShowFilterRow = false;
        //foreach (GridViewDataColumn coluna in gvEquipe.Columns.OfType<GridViewDataColumn>())
        //{
        //    coluna.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
        //}
        gvEntregas.Settings.ShowFilterRow = false;
        gvProdutosFinais.Settings.ShowFilterRow = false;
        gvPremissa.Settings.ShowFilterRow = false;
        gvRestricao.Settings.ShowFilterRow = false; 
        gvParceiro.Settings.ShowFilterRow = false;
        gvServicosCorporativos.SettingsPager.AlwaysShowPager = false;
    }

    #endregion

    #region Todas as grids

    protected void grid_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        switch (e.ButtonType)
        {
            case ColumnCommandButtonType.Delete:
                if (!podeExcluir)
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                    e.Text = "Não é possível excluir esta Meta pois já existem resultados lançados! ";

                }
                break;
            case ColumnCommandButtonType.Edit:
                if (!podeEditar)
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
        string mensagemErro = "";
        if (e.Exception == null)
        {
            SalvaEdicaoIniciativa(ref mensagemErro);
            object codigoAcao = e.Keys[0];
            string comandoSql = string.Format(@"
DECLARE @CodigoAcaoIniciativa INT
	SET @CodigoAcaoIniciativa = {0}
DELETE FROM [tai04_ResponsavelAcao] WHERE CodigoAcaoIniciativa = @CodigoAcaoIniciativa
DELETE FROM [tai04_ServicoCompartilhadoIniciativa] WHERE CodigoAcaoIniciativa = @CodigoAcaoIniciativa
"
                    , codigoAcao);
            ASPxListBox listBox = (ASPxListBox)
                gvAcoes.FindEditFormTemplateControl("listBoxResponsaveis");
            foreach (object value in listBox.SelectedValues)
            {
                comandoSql += string.Format(@"
INSERT INTO [tai04_ResponsavelAcao]
           ([CodigoAcaoIniciativa]
           ,[CodigoUsuarioResponsavel])
     VALUES
           (@CodigoAcaoIniciativa,{0})"
                    , value);
            }

            ASPxListBox listBox2 = (ASPxListBox)
                gvAcoes.FindEditFormTemplateControl("listBoxGrupoServicos");
            foreach (object value in listBox2.SelectedValues)
            {
                comandoSql += string.Format(@"
INSERT INTO [tai04_ServicoCompartilhadoIniciativa]
           ([CodigoAcaoIniciativa]
           ,[CodigoServicoCompartilhado])
     VALUES
           (@CodigoAcaoIniciativa,{0})"
                    , value);
            }
            
            int registrosAfetados = 0;
            cDados.execSQL(comandoSql, ref registrosAfetados);
        }
    }

    protected void grid_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        string mensagemErro = "";
        if (e.Exception == null)
            SalvaEdicaoIniciativa(ref mensagemErro);
    }

    protected void grid_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        string mensagemErro = "";
        if (e.Exception == null)
            SalvaEdicaoIniciativa(ref mensagemErro);
    }

    protected void grid_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        e.Properties["cpMyRowCount"] = ((ASPxGridView)sender).VisibleRowCount;
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
            SqlDataSource dataSource = sdsLider;

            dataSource.SelectCommand = cDados.getSQLComboUsuariosPorID(CodigoEntidadeUsuarioResponsavel);

            dataSource.SelectParameters.Clear();
            dataSource.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());

            comboBox.DataBind();
        }
    }

    #endregion

    #region Dados

    protected void callback_Callback(object source, CallbackEventArgs e)
    {
        string tipoOperacao;
        string mensagemErro = "";
        if (IndicaNovoProjeto)
        {
            SalvaNovaIniciativa(ref mensagemErro);
            tipoOperacao = "I";
        }
        else
        {
            SalvaEdicaoIniciativa(ref mensagemErro);
            tipoOperacao = "U";
        }
        e.Result = tipoOperacao + CodigoProjeto.ToString() + "|" + mensagemErro;
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
 INSERT INTO {0}.{1}.TermoAbertura04 
        (CodigoProjeto, NomeIniciativa, CodigoUnidadeNegocio, NomeUnidadeNegocio, ValorEstimado) 
 VALUES
        ({2}, '{3}', {4}, '{5}', 0)"
                , cDados.getDbName(), cDados.getDbOwner(), CodigoProjeto, dr["NomeProjeto"], dr["CodigoUnidadeNegocio"], dr["NomeUnidadeNegocio"]);

                int regAfetados = 0;
                cDados.execSQL(comandoSqlInsert, ref regAfetados);
            }
            Response.Redirect(string.Format("propostaDeIniciativa_008.aspx?CP={2}&RO={0}&Altura={1}", (readOnly ? "S" : "N"), alturaTela, codProjeto), true);
        }
    }

    protected void pageControl_CustomJSProperties(object sender, DevExpress.Web.CustomJSPropertiesEventArgs e)
    {
        e.Properties["cpReadOnly"] = readOnly;
    }

    #endregion

    #endregion

    #region Methods

    private void DefineCamposSomenteLeitura()
    {
        bool permiteEdicao = !readOnly;

        pnlDescricaoProjeto.JSProperties["cp_RO"] = permiteEdicao ? "N" : "S";

        pnlCronograma.JSProperties["cp_RO"] = permiteEdicao ? "N" : "S";

        btnSalvar.ClientVisible = permiteEdicao;
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

            txtNomeProjeto.Value = row["NomeIniciativa"];
            cmbLider.Value = row["CodigoGerenteIniciativa"];
            txtLider.Value = row["NomeGerenteIniciativa"];
            cmbUnidadeGestora.Value = row["CodigoUnidadeNegocio"];
            txtUnidadeGestora.Value = row["NomeUnidadeNegocio"];
            txtObjetivoGeral.Value = row["ObjetivoGeral"];
            txtJustificativa.Value = row["Justificativa"];
            seValorEstimado.Value = row["ValorEstimadoOrcamento"];
            txtPublicoAlvo.Value = row["PublicoAlvo"];
            deDataInicioProjeto.Value = row["DataInicioProjeto"];
            deDataTerminoProjeto.Value = row["DataTerminoProjeto"];

            checkDesabilitaCampoValorEstimado.Value = row["IndicaValorOrcamentoCalculado"];
            seValorEstimado.ReadOnly = row["IndicaValorOrcamentoCalculado"].ToString() == "S";

        }
    }

    protected string ObtemBotaoInclusaoRegistro(string nomeGrid, string assuntoGrid)
    {
        if(nomeGrid == "gvAcoes")
        {
            string tituloBotaoDesabilitado = IndicaNovoProjeto ? string.Format("Clique em 'Salvar' para poder registrar as informações de [{0}]", assuntoGrid) : "Novo";
            string htmlBotaoDesabilitado = string.Format(@"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""{0}"" style=""cursor: default;""/>", tituloBotaoDesabilitado);
            string htmlBotaoHabilitado = string.Format(@"<img src=""../../imagens/botoes/incluirReg02.png"" alt=""Novo"" onclick=""LimpaCamposFormulario();"" style=""cursor: pointer;""/>");

            string strRetorno = string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>"
                , (podeIncluir) ? htmlBotaoHabilitado : htmlBotaoDesabilitado);
            return strRetorno;
        }
        else
        {
            string tituloBotaoDesabilitado = IndicaNovoProjeto ? string.Format("Clique em 'Salvar' para poder registrar as informações de [{0}]", assuntoGrid) : "Novo";
            string htmlBotaoDesabilitado = string.Format(@"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""{0}"" style=""cursor: default;""/>", tituloBotaoDesabilitado);
            string htmlBotaoHabilitado = string.Format(@"<img src=""../../imagens/botoes/incluirReg02.png"" alt=""Novo"" onclick=""{0}.AddNewRow();"" style=""cursor: pointer;""/>", nomeGrid);

            string strRetorno = string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>"
                , (podeIncluir) ? htmlBotaoHabilitado : htmlBotaoDesabilitado);
            return strRetorno;
        }


    }

    protected string ObtemBotaoInclusaoRegistroAbaInfoComplement(string nomeGrid, string assuntoGrid)
    {
        string tituloBotaoDesabilitado = IndicaNovoProjeto ? string.Format("Clique em 'Salvar' para poder registrar as informações de [{0}]", assuntoGrid) : "Novo";
        string htmlBotaoDesabilitado = string.Format(@"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""{0}"" style=""cursor: default;""/>", tituloBotaoDesabilitado);
        string htmlBotaoHabilitado = string.Format(@"<img src=""../../imagens/botoes/incluirReg02.png"" alt=""Novo"" onclick=""{0}.AddNewRow();"" style=""cursor: pointer;""/>", nomeGrid);

        bool abaHabilitada = (gvProdutosFinais.VisibleRowCount > 0)
            && gvEntregas.VisibleRowCount > 0
            && gvAcoes.VisibleRowCount > 0;
        string strRetorno = string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>"
            , abaHabilitada ? htmlBotaoHabilitado : htmlBotaoDesabilitado);
        return strRetorno;
    }

    private void SalvaNovaIniciativa(ref string mensagemErro)
    {
        #region Comando SQL

        string comandoSql = string.Format(@"
DECLARE @Erro INT
DECLARE @MensagemErro nvarchar(2048)
    SET @Erro = 0

BEGIN TRAN

BEGIN TRY

DECLARE @CodigoProjeto int,
        @NomeIniciativa varchar(255),
        @CodigoGerenteIniciativa int,
        @NomeGerenteIniciativa varchar(60),
        @CodigoUnidadeNegocio int,
        @NomeUnidadeNegocio varchar(100),
        @ValorEstimado varchar(8000),
        @Justificativa varchar(8000),
        @ObjetivoGeral varchar(8000),
        @ServicosCompartilhados varchar(8000),
        @ValorEstimadoOrcamento decimal(25, 4),
        @CronogramaBasico varchar(8000),
        @Escopo varchar(8000),
		@PublicoAlvo varchar(2000),
		@ProdutoFinal varchar(8000),
		@DataInicioProjeto datetime,
		@DataTerminoProjeto datetime,
        @IndicaValorOrcamentoCalculado char(1)

    SET @CodigoProjeto = -1
    SET @NomeIniciativa = '{3}'
    SET @CodigoGerenteIniciativa = {4}
    SET @NomeGerenteIniciativa = '{5}'
    SET @CodigoUnidadeNegocio = {6}
    SET @NomeUnidadeNegocio = '{7}'
    SET @ValorEstimado = '{8}'
    SET @ObjetivoGeral = '{9}'
    SET @Justificativa = '{10}'
    SET @ServicosCompartilhados = '{13}'
    SET @ValorEstimadoOrcamento = {14}
    SET @CronogramaBasico = '{15}'
    SET @Escopo = '{16}'
	SET @PublicoAlvo = '{17}'
	SET @DataInicioProjeto = {19}
	SET @DataTerminoProjeto = {20}
    SET @IndicaValorOrcamentoCalculado = '{21}'

DECLARE @CodigoEntidade INT,
        @CodigoUsuarioInclusao INT,
        @CodigoCategoria INT
        
    SET @CodigoEntidade = {11}
    SET @CodigoUsuarioInclusao = {12}
    SET @CodigoCategoria = NULL

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

INSERT INTO {0}.{1}.TermoAbertura04
    (  [CodigoProjeto], [NomeIniciativa], [CodigoGerenteIniciativa], [NomeGerenteIniciativa], [CodigoUnidadeNegocio], [NomeUnidadeNegocio],
       [ValorEstimado], [ObjetivoGeral], [Justificativa], [ServicosCompartilhados], [ValorEstimadoOrcamento], [CronogramaBasico], [Escopo], [IndicaModeloTAI008],
	   [PublicoAlvo], [DataInicioProjeto], [DataTerminoProjeto], [IndicaValorOrcamentoCalculado])
  VALUES
    (  @CodigoProjeto, @NomeIniciativa, @CodigoGerenteIniciativa, @NomeGerenteIniciativa, @CodigoUnidadeNegocio, @NomeUnidadeNegocio, 
       @ValorEstimado, @ObjetivoGeral, @Justificativa, @ServicosCompartilhados, @ValorEstimadoOrcamento, @CronogramaBasico, @Escopo, 'S',
	   @PublicoAlvo, @DataInicioProjeto, @DataTerminoProjeto, @IndicaValorOrcamentoCalculado)
           
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
            , 0 // o código do projeto não será usado no comando do banco
            , txtNomeProjeto.Text.Replace("'", "''")
            , cmbLider.Value ?? "NULL"
            , cmbLider.Text.Replace("'", "''")
            , cmbUnidadeGestora.Value ?? "NULL"
            , cmbUnidadeGestora.Text.Replace("'", "''")
            , string.Empty
            , txtObjetivoGeral.Text.Replace("'", "''")
            , txtJustificativa.Text.Replace("'", "''")
            , CodigoEntidadeUsuarioResponsavel
            , CodigoUsuarioResponsavel
            , string.Empty
            , string.Format(CultureInfo.InvariantCulture, "{0:f2}", (seValorEstimado.Value ?? "NULL"))
            , string.Empty
            , string.Empty
            , txtPublicoAlvo.Text.Replace("'", "''")
            , string.Empty
            , deDataInicioProjeto.Value == null ? "null" : string.Format("'{0:d}'", deDataInicioProjeto.Value)
            , deDataTerminoProjeto.Value == null ? "null" : string.Format("'{0:d}'", deDataTerminoProjeto.Value)
            , checkDesabilitaCampoValorEstimado.Value);
        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        callback.JSProperties["cp_gravouNovaIni"] = "1";
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            object codigoErro = ds.Tables[0].Rows[0]["CodigoErro"];
            mensagemErro = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
            if (!(Convert.IsDBNull(codigoErro) || codigoErro.Equals(0)))
            {
                if (mensagemErro.Contains("UQ_Projeto_NomeProjeto"))
                    mensagemErro = "Nome de projeto já existente.";
                else
                    mensagemErro = mensagemErro.ToString();
                callback.JSProperties["cp_gravouNovaIni"] = "0";
            }
            
            CodigoProjeto = Convert.ToInt32(ds.Tables[0].Rows[0]["CodigoProjeto"]);
        }
    }

    private void SalvaExclusaoIniciativa()
    {
        #region Comando SQL

        string sOrigemMenuProjeto = "S".Equals(Request.QueryString["OrigemMenuProjeto"]) ? "S" : "N";
        string comandoSql = string.Format(@"
DECLARE @Erro INT
DECLARE @MensagemErro nvarchar(2048)
    SET @Erro = 0

BEGIN TRAN

BEGIN TRY

DECLARE @CodigoProjeto int,
        @sOrigemMenuProjeto varchar(1)

    SET @CodigoProjeto = {2}

DECLARE @CodigoEntidade INT,
        @CodigoUsuarioInclusao INT,
        @CodigoCategoria INT
        
    SET @CodigoEntidade = {3}
    SET @CodigoUsuarioInclusao = {4}
    SET @sOrigemMenuProjeto = '{5}'

    SET @Erro = @Erro + @@ERROR


    if (@sOrigemMenuProjeto = 'S')
    begin
        DECLARE @RC int
        EXECUTE @RC = {0}.{1}.[p_tai04_efetivaAtualizacaoResultados] @CodigoProjeto,@CodigoUsuarioInclusao 
        SET @Erro = @Erro + @@ERROR
    end


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
            , CodigoProjeto
            , CodigoEntidadeUsuarioResponsavel
            , CodigoUsuarioResponsavel
            , sOrigemMenuProjeto);

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            object codigoErro = ds.Tables[0].Rows[0]["CodigoErro"];
            string mensagemErro = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
            if (!(Convert.IsDBNull(codigoErro) || codigoErro.Equals(0)))
            {
                throw new Exception(mensagemErro.ToString());
            }
        }
    }

    private void SalvaEdicaoIniciativa(ref string mensagemErro)
    {
        #region Comando SQL

        string sOrigemMenuProjeto = "S".Equals(Request.QueryString["OrigemMenuProjeto"]) ? "S" : "N";
        string comandoSql = string.Format(@"
DECLARE @Erro INT
DECLARE @MensagemErro nvarchar(2048)
    SET @Erro = 0

BEGIN TRAN

BEGIN TRY

DECLARE @CodigoProjeto int,
        @NomeIniciativa varchar(255),
        @CodigoGerenteIniciativa int,
        @NomeGerenteIniciativa varchar(60),
        @CodigoUnidadeNegocio int,
        @NomeUnidadeNegocio varchar(100),
        @ValorEstimado varchar(8000),
        @Justificativa varchar(8000),
        @ObjetivoGeral varchar(8000),
        @ServicosCompartilhados varchar(8000),
        @sOrigemMenuProjeto varchar(1),
        @ValorEstimadoOrcamento decimal(25, 4),
        @CronogramaBasico varchar(8000),
        @Escopo varchar(8000),
		@PublicoAlvo varchar(2000),
		@ProdutoFinal varchar(8000),
		@DataInicioProjeto datetime,
		@DataTerminoProjeto datetime,
        @IndicaValorOrcamentoCalculado char(1)

    SET @CodigoProjeto = {2}
    SET @NomeIniciativa = '{3}'
    SET @CodigoGerenteIniciativa = {4}
    SET @NomeGerenteIniciativa = '{5}'
    SET @CodigoUnidadeNegocio = {6}
    SET @NomeUnidadeNegocio = '{7}'
    SET @ValorEstimado = '{8}'
    SET @ObjetivoGeral = '{9}'
    SET @Justificativa = '{10}'
    SET @ServicosCompartilhados = '{13}'
    SET @sOrigemMenuProjeto = '{14}'
    SET @ValorEstimadoOrcamento = {15}
    SET @CronogramaBasico = '{16}'
    SET @Escopo = '{17}'
	SET @PublicoAlvo = '{18}'
	SET @DataInicioProjeto = {20}
	SET @DataTerminoProjeto = {21}
    SET @IndicaValorOrcamentoCalculado = '{22}'

DECLARE @CodigoEntidade INT,
        @CodigoUsuarioInclusao INT,
        @CodigoCategoria INT
        
    SET @CodigoEntidade = {11}
    SET @CodigoUsuarioInclusao = {12}
    SET @CodigoCategoria = NULL

UPDATE {0}.{1}.TermoAbertura04
   SET [NomeIniciativa]				   = @NomeIniciativa
     , [CodigoGerenteIniciativa]	   = @CodigoGerenteIniciativa
     , [NomeGerenteIniciativa]		   = @NomeGerenteIniciativa
     , [CodigoUnidadeNegocio]		   = @CodigoUnidadeNegocio
     , [NomeUnidadeNegocio]			   = @NomeUnidadeNegocio
     , [ValorEstimado]				   = @ValorEstimado
     , [ObjetivoGeral]                 = @ObjetivoGeral
     , [Justificativa]				   = @Justificativa
     , [ServicosCompartilhados]        = @ServicosCompartilhados
     , [ValorEstimadoOrcamento]        = @ValorEstimadoOrcamento
     , [CronogramaBasico]              = @CronogramaBasico
     , [Escopo]                        = @Escopo
	 , [PublicoAlvo]				   = @PublicoAlvo
	 , [DataInicioProjeto]			   = @DataInicioProjeto
	 , [DataTerminoProjeto]			   = @DataTerminoProjeto
     , [IndicaValorOrcamentoCalculado] = @IndicaValorOrcamentoCalculado
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


if (@sOrigemMenuProjeto = 'S')
begin
    DECLARE @RC int
    EXECUTE @RC = {0}.{1}.[p_tai04_efetivaAtualizacaoResultados] @CodigoProjeto,@CodigoUsuarioInclusao 
    SET @Erro = @Erro + @@ERROR
end


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
            , CodigoProjeto
            , txtNomeProjeto.Text.Replace("'", "''")
            , cmbLider.Value ?? "NULL"
            , cmbLider.Text.Replace("'", "''")
            , cmbUnidadeGestora.Value ?? "NULL"
            , cmbUnidadeGestora.Text.Replace("'", "''")
            , string.Empty
            , txtObjetivoGeral.Text.Replace("'", "''")
            , txtJustificativa.Text.Replace("'", "''")
            , CodigoEntidadeUsuarioResponsavel
            , CodigoUsuarioResponsavel
            , string.Empty
            , sOrigemMenuProjeto
            , string.Format(CultureInfo.InvariantCulture, "{0:f2}", (seValorEstimado.Value ?? "NULL"))
            , string.Empty
            , string.Empty
            , txtPublicoAlvo.Text.Replace("'", "''")
            , string.Empty
            , deDataInicioProjeto.Value == null ? "null" : string.Format("'{0:d}'", deDataInicioProjeto.Value)
            , deDataTerminoProjeto.Value == null ? "null" : string.Format("'{0:d}'", deDataTerminoProjeto.Value)
            , checkDesabilitaCampoValorEstimado.Value);

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            object codigoErro = ds.Tables[0].Rows[0]["CodigoErro"];
            mensagemErro = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
            if (!(Convert.IsDBNull(codigoErro) || codigoErro.Equals(0)))
            {
                if (mensagemErro.Contains("UQ_Projeto_NomeProjeto"))
                    mensagemErro = "Nome de projeto já existente.";
                else
                    mensagemErro = mensagemErro.ToString();
            }
        }
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

    private bool ExisteDependenciaRegistro(string codigo, SqlDataSource[] sources, string nomeColunaCodigo)
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

    private void constroiAbaElementosEstrategia()
    {
        if (pageControl.TabPages.FindByName("tabElementosEstrategia") == null)
        {
            TabPage tb = new TabPage("Elementos da Estratégia", "tabElementosEstrategia");

            string frameElementos = string.Format(@"<iframe frameborder=""0"" name=""frmElementos"" scrolling=""auto"" src=""./ElementosEstrategia.aspx?CP={0}&RO={1}""
                    width=""100%"" style=""height: {2}px"" id=""frmElementos""></iframe>", CodigoProjeto, readOnly ? "S" : "N", alturaTela);

            tb.Controls.Add(cDados.getLiteral(frameElementos));

            pageControl.TabPages.Insert(2, tb);
        }
    }

    #endregion

    private void desabilitaCamposGridResultados(ASPxSpinEdit spin1)
    {
        spin1.ClientEnabled = false;
        spin1.DisabledStyle.ForeColor = Color.Black;
        spin1.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
    }

    protected void callbackHTMLIndicador_Callback(object source, CallbackEventArgs e)
    {
        string codigoIndicador = e.Parameter;

        DataSet ds = cDados.getIndicadoresOperacional(CodigoEntidadeUsuarioResponsavel, " AND ind.CodigoIndicador = " + codigoIndicador);



        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string resumoDoIndicador = String.Format(@"
        Tipo de indicador:{0}
        Agrupamento da Meta:{1}
        Unidade de medida:{2}
        Polaridade:{4}
        Critério: {6}", ds.Tables[0].Rows[0]["DescTipoIndicador"].ToString() + "</br>"
              , ds.Tables[0].Rows[0]["NomeFuncaoAgrupamentoMeta"].ToString() + "</br>"
              , ds.Tables[0].Rows[0]["SiglaUnidadeMedida"].ToString() + "</br>"
              , ds.Tables[0].Rows[0]["CasasDecimais"].ToString() + "</br>"
              , (ds.Tables[0].Rows[0]["Polaridade"].ToString() == "POS") ? "Quanto maior o valor, MELHOR</br>" : "Quanto maior o valor, PIOR</br>"
              , ds.Tables[0].Rows[0]["ResponsavelObjeto"].ToString() + "</br>"
              , (ds.Tables[0].Rows[0]["IndicaCriterio"].ToString() == "A") ? "Acumulado</br>" : "Status</br>");
            resumoDoIndicador += "--------------Glossário--------------</br>";
            resumoDoIndicador += ds.Tables[0].Rows[0]["GlossarioIndicador"].ToString();
            resumoDoIndicador += "</br>-------------------------------------";
            callbackHTMLIndicador.JSProperties["cp_HTML"] = resumoDoIndicador;

            string tipoAgrupamento = ds.Tables[0].Rows[0]["NomeFuncaoBD"].ToString();
            string tipoCriterio = ds.Tables[0].Rows[0]["IndicaCriterio"].ToString();
            string casasDecimais = ds.Tables[0].Rows[0]["CasasDecimais"].ToString();

            callbackHTMLIndicador.JSProperties["cp_Criterio"] = tipoCriterio;
            callbackHTMLIndicador.JSProperties["cp_Agrupamento"] = tipoAgrupamento;
            callbackHTMLIndicador.JSProperties["cp_Decimals"] = casasDecimais;

        }
        else
        {
            callbackHTMLIndicador.JSProperties["cp_HTML"] = "";
        }
    }

    protected void gvAcoes_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CodigoUsuarioResponsavel")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;

            combo.ItemRequestedByValue += cmbLider_ItemRequestedByValue;
            combo.ItemsRequestedByFilterCondition += cmbLider_ItemsRequestedByFilterCondition;
        }
    }

    protected void panelCallbakcValorEstimado_Callback(object sender, CallbackEventArgsBase e)
    {
        //DataView dv = (DataView)sdsDadosFomulario.Select(DataSourceSelectArguments.Empty);
        //seValorEstimado.Value = dv[0]["ValorEstimadoOrcamento"];
    }

    //    protected void gvAcoes_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    //    {
    //        if (e.Expanded)
    //        {
    //            int visibleIndex = e.VisibleIndex;
    //            int codigoAcaoIniciativa = Convert.ToInt32(
    //                gvAcoes.GetRowValues(visibleIndex, "CodigoAcaoIniciativa"));
    //            ASPxDataView dataView = (ASPxDataView)
    //                gvAcoes.FindDetailRowTemplateControl(visibleIndex, "dataView");
    //            Session["codigoAcaoIniciativa"] = codigoAcaoIniciativa;
    //            string comandoSql = string.Format(@"
    //DECLARE @CodigoAcaoIniciativa INT
    //	SET @CodigoAcaoIniciativa = {0}
    //  
    // SELECT sc.CodigoServicoCompartilhado,
    //        sc.DescricaoServicoCompartilhado,
    //        sc.GrupoServicoCompartilhado,
    //        (CASE WHEN (sci.CodigoServicoCompartilhado IS NULL) THEN 'N' ELSE 'S' END) AS Selecionado
    //   FROM tai04_ServicoCompartilhado AS sc LEFT JOIN
    //        tai04_ServicoCompartilhadoIniciativa AS sci ON sci.CodigoServicoCompartilhado = sc.CodigoServicoCompartilhado
    //                                            AND sci.CodigoAcaoIniciativa = @CodigoAcaoIniciativa
    //  WHERE sc.DataDesativacao IS NULL"
    //                , codigoAcaoIniciativa);
    //            DataTable dt = cDados.getDataSet(comandoSql).Tables[0];
    //            var servCompartAgrupados = dt
    //                .AsEnumerable()
    //                .Select(r => new
    //                {
    //                    Codigo = r.Field<int>("CodigoServicoCompartilhado"),
    //                    Descricao = r.Field<string>("DescricaoServicoCompartilhado"),
    //                    Grupo = r.Field<string>("GrupoServicoCompartilhado"),
    //                    Selecionado = r.Field<string>("Selecionado").Equals("S")
    //                })
    //                .GroupBy(r => r.Grupo);

    //            int qtdeGrupos = servCompartAgrupados.Count();
    //            dataView.ColumnCount = qtdeGrupos;
    //            dataView.DataSource = servCompartAgrupados;
    //            dataView.DataBind();

    //            for (int posicao = 0; posicao < qtdeGrupos; posicao++)
    //            {
    //                DataViewItem dvItem = dataView.Items[posicao];
    //                ASPxCheckBoxList cblServicoCompartilhado = (ASPxCheckBoxList)
    //                    dataView.FindItemControl("cblServicoCompartilhado", dvItem);
    //                var servicosCompart = servCompartAgrupados.ElementAt(posicao).ToList();
    //                cblServicoCompartilhado.DataSource = servicosCompart;
    //                cblServicoCompartilhado.DataBind();

    //                var codigosSelecionados =
    //                    servicosCompart.Where(s => s.Selecionado).Select(s => s.Codigo);
    //                foreach (int codigo in codigosSelecionados)
    //                    cblServicoCompartilhado.Items.FindByValue(codigo).Selected = true;
    //            }
    //        }
    //    }

    protected void callbackServicosCompartilhados_Callback(object source, CallbackEventArgs e)
    {
        string[] parametros = e.Parameter.Split(';');
        bool selecionado = bool.Parse(parametros[1]);
        int codigoServicoCompartilhado = int.Parse(parametros[0]);
        int codigoAcaoIniciativa = (int)Session["codigoAcaoIniciativa"];

        string comandoSql = string.Format(@"
DELETE FROM [tai04_ServicoCompartilhadoIniciativa] 
      WHERE [CodigoAcaoIniciativa] = {0}
		AND [CodigoServicoCompartilhado] = {1}
"
            , codigoAcaoIniciativa
            , codigoServicoCompartilhado);
        if (selecionado)
        {
            comandoSql += string.Format(@"
INSERT INTO [tai04_ServicoCompartilhadoIniciativa]
           ([CodigoAcaoIniciativa]
           ,[CodigoServicoCompartilhado])
     VALUES
           ({0}
           ,{1})"
                , codigoAcaoIniciativa
                , codigoServicoCompartilhado);
        }
        int registrosAfetados = 0;
        cDados.execSQL(comandoSql, ref registrosAfetados);
    }

    protected void gvAcoes_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        Session["codigoAcao"] = e.EditingKeyValue;
    }

    protected void gvAcoes_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        Session["codigoAcao"] = -1;
    }

    protected void ASPxListBox1_DataBound(object sender, EventArgs e)
    {
        ASPxListBox listBox = (ASPxListBox)sender;
        foreach (ListEditItem item in listBox.Items)
        {
            bool selecionado = item.GetFieldValue("Selecionado").ToString() == "S";
            item.Selected = selecionado;
        }
    }

    protected void ASPxListBox2_DataBound(object sender, EventArgs e)
    {
        ASPxListBox listBox = (ASPxListBox)sender;
        foreach (ListEditItem item in listBox.Items)
        {
            bool selecionado = item.GetFieldValue("Selecionado").ToString() == "S";
            item.Selected = selecionado;
        }
    }

    protected void sdsAcoesIniciativa_Inserting(object sender, SqlDataSourceCommandEventArgs e)
    {

        string[] arrayResponsaveisSelecionados = new string[gvResponsaveis.GetSelectedFieldValues("CodigoUsuario").Count];
        for (int i = 0; i < arrayResponsaveisSelecionados.Length; i++)
        {
            arrayResponsaveisSelecionados[i] = gvResponsaveis.GetSelectedFieldValues("CodigoUsuario")[i].ToString();
        }
        string[] arrayServicosCorporativosSelecionados = new string[gvServicosCorporativos.GetSelectedFieldValues("CodigoServicoCompartilhado").Count];
        for (int i = 0; i < arrayServicosCorporativosSelecionados.Length; i++)
        {
            arrayServicosCorporativosSelecionados[i] = gvServicosCorporativos.GetSelectedFieldValues("CodigoServicoCompartilhado")[i].ToString();
        }

        e.Command.CommandText += @"
DECLARE @CodigoAcao INT
	SET @CodigoAcao = SCOPE_IDENTITY()";
        foreach (object value in arrayResponsaveisSelecionados)
        {
            e.Command.CommandText += string.Format(@"
INSERT INTO [tai04_ResponsavelAcao]
           ([CodigoAcaoIniciativa]
           ,[CodigoUsuarioResponsavel])
     VALUES
           (@CodigoAcao,{0})", value);
        }

        foreach (object value in arrayServicosCorporativosSelecionados)
        {
            e.Command.CommandText += string.Format(@"
INSERT INTO [tai04_ServicoCompartilhadoIniciativa]
           ([CodigoAcaoIniciativa]
           ,[CodigoServicoCompartilhado])
     VALUES
           (@CodigoAcao,{0})", value);
        }
    }

    protected void gvAcoes_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        DateTime inicio = (DateTime)e.NewValues["DataInicio"];
        DateTime termino = (DateTime)e.NewValues["DataTermino"];
        if (termino < inicio)
        {
            e.RowError = "Data Início não pode ser maior que data Término";
        }

    }


    protected void pnValorEstimado_Callback(object sender, CallbackEventArgsBase e)
    {
        var checado = e.Parameter;
        if (checado == "S")
        {

            string comandoSQL = string.Format(@"
            SELECT isnull(SUM(isnull(ValorPrevisto,0)),0) as somatoriaDoValorEstimado 
             FROM [tai04_AcoesIniciativa] 
            WHERE ([CodigoProjeto] = {0}) ", CodigoProjeto);

            DataSet ds = cDados.getDataSet(comandoSQL);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                seValorEstimado.Value = (decimal)ds.Tables[0].Rows[0]["somatoriaDoValorEstimado"];
            }
            seValorEstimado.ReadOnly = true;
        }
        else
        {
            seValorEstimado.ReadOnly = false;
        }
    }


    protected void gvResponsaveis_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {

        ((ASPxGridView)sender).ExpandAll();
        ((ASPxGridView)sender).Selection.UnselectAll();

        for (int i = 0; i < ((ASPxGridView)sender).VisibleRowCount; i++)
        {
            if (((ASPxGridView)sender).GetRowValues(i, "Selecionado").ToString() == "S")
            {
                ((ASPxGridView)sender).Selection.SelectRow(i);
            }
        }
    }

    private string getChavePrimariaGvAcoes() // retorna a primary key da tabela
    {
        //gvAcoes.GetRowValues(gvAcoes.FocusedRowIndex,"CodigoAcaoIniciativa")

        if (gvAcoes.GetRowValues(gvAcoes.FocusedRowIndex, "CodigoAcaoIniciativa") != null)
            return gvAcoes.GetRowValues(gvAcoes.FocusedRowIndex, "CodigoAcaoIniciativa").ToString();
        else
            return "-1";
    }

    protected void callbackAcoes_Callback(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cp_erro"] = "";
        ((ASPxCallback)source).JSProperties["cp_operacao"] = "";

        string parametroAcao = e.Parameter;
        ((ASPxCallback)source).JSProperties["cp_operacao"] = parametroAcao;
        if (parametroAcao == "AtualizaAcao")
        {
            string auxNomeUsuario = (Session["NomeUsuario"] != null) ? Session["NomeUsuario"].ToString() : "";



            sdsAcoesIniciativa.InsertParameters["NomeAcao"].DefaultValue = 
                sdsAcoesIniciativa.UpdateParameters["NomeAcao"].DefaultValue = txtNomeAcao.Text;

            sdsAcoesIniciativa.InsertParameters["CodigoUsuarioResponsavel"].DefaultValue = 
                sdsAcoesIniciativa.UpdateParameters["CodigoUsuarioResponsavel"].DefaultValue =  CodigoUsuarioResponsavel.ToString();

            sdsAcoesIniciativa.InsertParameters["NomeUsuarioResponsavel"].DefaultValue = 
                sdsAcoesIniciativa.UpdateParameters["NomeUsuarioResponsavel"].DefaultValue = auxNomeUsuario;

            sdsAcoesIniciativa.InsertParameters["DataInicio"].DefaultValue = 
                sdsAcoesIniciativa.UpdateParameters["DataInicio"].DefaultValue = ddlDataInicioAcao.Value.ToString();

            sdsAcoesIniciativa.InsertParameters["DataTermino"].DefaultValue = 
                sdsAcoesIniciativa.UpdateParameters["DataTermino"].DefaultValue = ddlDataTerminoAcao.Value.ToString();

            sdsAcoesIniciativa.InsertParameters["ValorPrevisto"].DefaultValue = 
                sdsAcoesIniciativa.UpdateParameters["ValorPrevisto"].DefaultValue = (spnValor.Value != null) ? spnValor.Value.ToString() : "0";

            sdsAcoesIniciativa.UpdateParameters["CodigoAcaoIniciativa"].DefaultValue = Session["codigoAcao"].ToString();

            if (Session["codigoAcao"].ToString() == "-1")
            {
                sdsAcoesIniciativa.Insert();
            }
            else
            {
                sdsAcoesIniciativa.Update();
            }
                
            gvAcoes.DataBind();
            string comandoSql = "";
            if (Session["codigoAcao"].ToString() != "-1")
            {
                comandoSql = string.Format(@"
            DECLARE @CodigoAcaoIniciativa INT
	        SET @CodigoAcaoIniciativa = {0}
            DELETE FROM [tai04_ResponsavelAcao] WHERE CodigoAcaoIniciativa = @CodigoAcaoIniciativa
            DELETE FROM [tai04_ServicoCompartilhadoIniciativa] WHERE CodigoAcaoIniciativa = @CodigoAcaoIniciativa", Session["codigoAcao"].ToString());

                string[] arrayResponsaveisSelecionados = new string[gvResponsaveis.GetSelectedFieldValues("CodigoUsuario").Count];
                for (int i = 0; i < arrayResponsaveisSelecionados.Length; i++)
                {
                    arrayResponsaveisSelecionados[i] = gvResponsaveis.GetSelectedFieldValues("CodigoUsuario")[i].ToString();
                    comandoSql += string.Format(@"
                INSERT INTO [tai04_ResponsavelAcao]
                ([CodigoAcaoIniciativa]
                ,[CodigoUsuarioResponsavel])
                VALUES
                (@CodigoAcaoIniciativa,{0})", arrayResponsaveisSelecionados[i]);
                }

                string[] arrayServicosCorporativosSelecionados = new string[gvServicosCorporativos.GetSelectedFieldValues("CodigoServicoCompartilhado").Count];
                for (int i = 0; i < arrayServicosCorporativosSelecionados.Length; i++)
                {
                    arrayServicosCorporativosSelecionados[i] = gvServicosCorporativos.GetSelectedFieldValues("CodigoServicoCompartilhado")[i].ToString();
                    comandoSql += string.Format(@"
                INSERT INTO [tai04_ServicoCompartilhadoIniciativa]
                ([CodigoAcaoIniciativa],[CodigoServicoCompartilhado])
                VALUES (@CodigoAcaoIniciativa,{0})", arrayServicosCorporativosSelecionados[i]);

                }
            } 


            bool retorno = false;
            try
            {
                int registrosAfetados = 0;
                retorno = cDados.execSQL(comandoSql, ref registrosAfetados);

            }
            catch (Exception ex)
            {
                ((ASPxCallback)source).JSProperties["cp_erro"] = ex.Message;
            }
        }
        else if (parametroAcao == "PreparaAtualizaAcao")
        {
            Session["codigoAcao"] = getChavePrimariaGvAcoes();
        }
        else if (parametroAcao == "PreparaInclusaoAcao")
        {
            Session["codigoAcao"] = -1;
        }
    }

    protected void gvAcoes_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        sdsAcoesIniciativa.DataBind();
        gvAcoes.DataBind();
    }



    protected void gvEntregas_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        ((ASPxGridView)sender).SettingsText.PopupEditFormCaption = "Editar";
    }

    protected void gvProdutosFinais_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        ((ASPxGridView)sender).SettingsText.PopupEditFormCaption = "Editar";
    }
}