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

public partial class _Projetos_Administracao_propostaDeIniciativa_007 : System.Web.UI.Page
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
            sdsIndicador.ConnectionString =
            sdsIndicadorEstrategico.ConnectionString =
            sdsIndicadorOperacional.ConnectionString =
            sdsLider.ConnectionString =
                //sdsObjetivoEstrategico.ConnectionString =
            sdsParceiros.ConnectionString =
            sdsResponsaveisAcao.ConnectionString =
            sdsUsuariosParceiros.ConnectionString =
            sdsResultados.ConnectionString =
            dsEquipe.ConnectionString =
            sdsUnidadeGestora.ConnectionString = value;
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
    private bool trimestre01;
    private bool trimestre02;
    private bool trimestre03;
    private bool trimestre04;
    private dados cDados;

    public string alturaTela = "", readOnlyInf = "";

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

        if (!IsPostBack)
        {
            int activeTabIndex;
            int alturaInt;
            if (int.TryParse(Request.QueryString["tab"], out activeTabIndex))
                pageControl.ActiveTabIndex = activeTabIndex;
            if (int.TryParse(Request.QueryString["Altura"], out alturaInt))
                alturaTela = (alturaInt - 150).ToString();
            if (!string.IsNullOrEmpty(alturaTela))
            {
                dv01.Style.Add("height", alturaTela + "px");
                dv3.Style.Add("height", alturaTela + "px");
                dv01.Style.Add("overflow", "auto");
                dv3.Style.Add("overflow", "auto");
            }
        }

        CodigoEntidadeUsuarioResponsavel = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade") ?? -1);
        CodigoUsuarioResponsavel = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado") ?? -1);

        string cp = Request.QueryString["CP"];
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

        pageControl.TabPages[2].ClientVisible = exibirAbaCaracterizacaoProjeto;

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
        hfGeral.Set("OrigemMenuProjeto", Request.QueryString["OrigemMenuProjeto"] +"");

        btnImprimir.ClientEnabled = !IndicaNovoProjeto;
        btnImprimir0.ClientEnabled = !IndicaNovoProjeto;

        btnSalvar0.JSProperties["cp_ReadOnly"] = readOnlyInf;
        cDados.aplicaEstiloVisual(this);
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

        if(cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
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
        DataSet ds = cDados.getTrimestreLiberado(CodigoEntidadeUsuarioResponsavel);
        //bool existeRegistros = false;
        //temOrcamentoAtivo = false;
        foreach (DataRow row in ds.Tables[0].Rows)
        {
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
        }

        trimestre01 = true;
        trimestre02 = true;
        trimestre03 = true;
        trimestre04 = true;

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
            gvResultados.Columns["colComandos"].Visible = true;
            constroiAbaElementosEstrategia();
        }
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

    #region gvResultados

    protected void gvResultados_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string nomeColunaCodigo = "CodigoIndicador";
        string nomeColunaDescricao = "NomeIndicador";
        string nomeColunaNomeIndicador = "NomeIndicador";
        string codigo = e.NewValues["CodigoIndicador"].ToString();
        string descricao = ObtemDescricaoPeloCodigo(
            codigo, nomeColunaCodigo, nomeColunaDescricao, sdsIndicador);

        if (e.NewValues.Contains(nomeColunaNomeIndicador))
            e.NewValues[nomeColunaNomeIndicador] = descricao;
        else
            e.NewValues.Add(nomeColunaNomeIndicador, descricao);

        string nomeColunaSetencaResultado = "SetencaResultado";
        string transformacaoProduto = ObtemValorPelaChave(e.NewValues, "TransformacaoProduto", false);
        string nomeIndicador = ObtemValorPelaChave(e.NewValues, "NomeIndicador", false);
        //string valorInicialTransformacao = ObtemValorPelaChave(e.NewValues, "ValorInicialTransformacao", true, "{0:n2}");
        string valorFinalTransformacao = ObtemValorPelaChave(e.NewValues, "ValorFinalTransformacao", true, "{0:n2}");
        string dataLimitePrevista = ObtemValorPelaChave(e.NewValues, "DataLimitePrevista", false, "{0:dd/MM/yyyy}");
        string setencaResultado = String.Format("{0} {1} {2}",
             valorFinalTransformacao, nomeIndicador, transformacaoProduto);

        if (e.NewValues.Contains(nomeColunaSetencaResultado))
            e.NewValues[nomeColunaSetencaResultado] = setencaResultado;
        else
            e.NewValues.Add(nomeColunaSetencaResultado, setencaResultado);
    }

    protected void gvResultados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string nomeColunaCodigo = "CodigoIndicador";
        string nomeColunaDescricao = "NomeIndicador";
        string nomeColunaNomeIndicador = "NomeIndicador";
        string codigo = e.NewValues["CodigoIndicador"].ToString();
        string descricao = ObtemDescricaoPeloCodigo(
            codigo, nomeColunaCodigo, nomeColunaDescricao, sdsIndicador);

        if (e.NewValues.Contains(nomeColunaNomeIndicador))
            e.NewValues[nomeColunaNomeIndicador] = descricao;
        else
            e.NewValues.Add(nomeColunaNomeIndicador, descricao);

        string nomeColunaSetencaResultado = "SetencaResultado";
        string transformacaoProduto = ObtemValorPelaChave(e.NewValues, "TransformacaoProduto", false);
        string nomeIndicador = ObtemValorPelaChave(e.NewValues, "NomeIndicador", false);
        //string valorInicialTransformacao = ObtemValorPelaChave(e.NewValues, "ValorInicialTransformacao", true, "{0:n2}");
        string valorFinalTransformacao = ObtemValorPelaChave(e.NewValues, "ValorFinalTransformacao", true, "{0:n2}");
        string dataLimitePrevista = ObtemValorPelaChave(e.NewValues, "DataLimitePrevista", false, "{0:dd/MM/yyyy}");
        string setencaResultado = String.Format("{0} {1} {2}",
             valorFinalTransformacao, nomeIndicador, transformacaoProduto);

        if (e.NewValues.Contains(nomeColunaSetencaResultado))
            e.NewValues[nomeColunaSetencaResultado] = setencaResultado;
        else
            e.NewValues.Add(nomeColunaSetencaResultado, setencaResultado);
    }

    protected void gvResultados_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        ASPxSpinEdit spin;
        if (e.Column.FieldName == "ValorFinalTransformacao" && e.VisibleIndex >= 0)
            spin = (ASPxSpinEdit)e.Editor;
        else if (e.Column.FieldName == "ValorTrimestre1" && trimestre01 == false)
            desabilitaCamposGridResultados((ASPxSpinEdit)e.Editor);
        else if (e.Column.FieldName == "ValorTrimestre2" && trimestre02 == false)
            desabilitaCamposGridResultados((ASPxSpinEdit)e.Editor);
        else if (e.Column.FieldName == "ValorTrimestre3" && trimestre03 == false)
            desabilitaCamposGridResultados((ASPxSpinEdit)e.Editor);
        else if (e.Column.FieldName == "ValorTrimestre4" && trimestre04 == false)
            desabilitaCamposGridResultados((ASPxSpinEdit)e.Editor);

    }

    protected void gvResultados_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        SalvaExclusaoIniciativa();
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

        string comandoSQL = cDados.getSQLComboUsuarios(CodigoEntidadeUsuarioResponsavel, e.Filter, "");

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
        }
        else
        {
            SalvaEdicaoIniciativa();
            tipoOperacao = "U";
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
 INSERT INTO {0}.{1}.TermoAbertura04 
        (CodigoProjeto, NomeIniciativa, CodigoUnidadeNegocio, NomeUnidadeNegocio, ValorEstimado) 
 VALUES
        ({2}, '{3}', {4}, '{5}', 0)"
                , cDados.getDbName(), cDados.getDbOwner(), CodigoProjeto, dr["NomeProjeto"], dr["CodigoUnidadeNegocio"], dr["NomeUnidadeNegocio"]);

                int regAfetados = 0;
                cDados.execSQL(comandoSqlInsert, ref regAfetados);
            }
            Response.Redirect(string.Format("propostaDeIniciativa_007.aspx?CP={2}&RO={0}&Altura={1}", (readOnly ? "S" : "N"), alturaTela, codProjeto), true);
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
        btnSalvar0.ClientVisible = permiteEdicao;
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
            txtValorProjeto.Value = row["ValorEstimado"];
            txtObjetivoGeral.Value = row["ObjetivoGeral"];
            txtJustificativa.Value = row["Justificativa"];
            txtServicosCompartilhados.Value = row["ServicosCompartilhados"];
            txtGerenteUnidade.Value = row["nomeGerenteUnidade"];
            seValorEstimado.Value = row["ValorEstimadoOrcamento"];
            txtCronogramaBasico.Value = row["CronogramaBasico"];
            txtEscopoIniciativa.Value = row["Escopo"];
        }
    }

    protected string ObtemBotaoInclusaoRegistro(string nomeGrid, string assuntoGrid)
    {
        string tituloBotaoDesabilitado = IndicaNovoProjeto ? string.Format("Clique em 'Salvar' para poder registrar as informações de [{0}]", assuntoGrid) : "Novo";
        string htmlBotaoDesabilitado = string.Format(@"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""{0}"" style=""cursor: default;""/>", tituloBotaoDesabilitado);
        string htmlBotaoHabilitado = string.Format(@"<img src=""../../imagens/botoes/incluirReg02.png"" alt=""Novo"" onclick=""{0}.AddNewRow();"" style=""cursor: pointer;""/>", nomeGrid);

        string strRetorno = string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>"
            , (podeIncluir) ? htmlBotaoHabilitado : htmlBotaoDesabilitado);
        return strRetorno;
    }

    private void SalvaNovaIniciativa()
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
        @Escopo varchar(8000)

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
       [ValorEstimado], [ObjetivoGeral], [Justificativa], [ServicosCompartilhados], [ValorEstimadoOrcamento], [CronogramaBasico], [Escopo], [IndicaModeloTAI007])
  VALUES
    (  @CodigoProjeto, @NomeIniciativa, @CodigoGerenteIniciativa, @NomeGerenteIniciativa, @CodigoUnidadeNegocio, @NomeUnidadeNegocio, 
       @ValorEstimado, @ObjetivoGeral, @Justificativa, @ServicosCompartilhados, @ValorEstimadoOrcamento, @CronogramaBasico, @Escopo, 'S')
           
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
            , txtValorProjeto.Text.Replace("'", "''")
            , txtObjetivoGeral.Text.Replace("'", "''")
            , txtJustificativa.Text.Replace("'", "''")
            , CodigoEntidadeUsuarioResponsavel
            , CodigoUsuarioResponsavel
            , txtServicosCompartilhados.Text.Replace("'", "''")
            , string.Format(CultureInfo.InvariantCulture, "{0:f2}", (seValorEstimado.Value ?? "NULL"))
            , txtCronogramaBasico.Text.Replace("'", "''")
            , txtEscopoIniciativa.Text.Replace("'", "''"));
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
    
    private void SalvaEdicaoIniciativa()
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
        @Escopo varchar(8000)

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

DECLARE @CodigoEntidade INT,
        @CodigoUsuarioInclusao INT,
        @CodigoCategoria INT
        
    SET @CodigoEntidade = {11}
    SET @CodigoUsuarioInclusao = {12}
    SET @CodigoCategoria = NULL

UPDATE {0}.{1}.TermoAbertura04
   SET [NomeIniciativa]				= @NomeIniciativa
     , [CodigoGerenteIniciativa]	= @CodigoGerenteIniciativa
     , [NomeGerenteIniciativa]		= @NomeGerenteIniciativa
     , [CodigoUnidadeNegocio]		= @CodigoUnidadeNegocio
     , [NomeUnidadeNegocio]			= @NomeUnidadeNegocio
     , [ValorEstimado]				= @ValorEstimado
     , [ObjetivoGeral]              = @ObjetivoGeral
     , [Justificativa]				= @Justificativa
     , [ServicosCompartilhados]     = @ServicosCompartilhados
     , [ValorEstimadoOrcamento]     = @ValorEstimadoOrcamento
     , [CronogramaBasico]           = @CronogramaBasico
     , [Escopo]                     = @Escopo
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
            , txtValorProjeto.Text.Replace("'", "''")
            , txtObjetivoGeral.Text.Replace("'", "''")
            , txtJustificativa.Text.Replace("'", "''")
            , CodigoEntidadeUsuarioResponsavel
            , CodigoUsuarioResponsavel
            , txtServicosCompartilhados.Text.Replace("'", "''")
            , sOrigemMenuProjeto
            , string.Format(CultureInfo.InvariantCulture, "{0:f2}", (seValorEstimado.Value ?? "NULL"))
            , txtCronogramaBasico.Text.Replace("'", "''")
            , txtEscopoIniciativa.Text.Replace("'", "''"));

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

    protected void gvParceiros_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CodigoUsuario")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;

            combo.ItemRequestedByValue += ddlResponsavel_ItemRequestedByValue;
            combo.ItemsRequestedByFilterCondition += ddlResponsavel_ItemsRequestedByFilterCondition;

            //combo.DataSource; 
            //combo.DataBind();
        }
    }

    protected void gvParceiros_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataSet ds = cDados.getUsuarios(" AND u.CodigoUsuario = " + e.NewValues["CodigoUsuario"]);

        e.NewValues["NomeUsuario"] = ds.Tables[0].Rows[0]["NomeUsuario"];
        e.NewValues["Email"] = ds.Tables[0].Rows[0]["EMail"];
        e.NewValues["NumeroTelefone"] = ds.Tables[0].Rows[0]["TelefoneContato1"];
    }

    private void desabilitaCamposGridResultados(ASPxSpinEdit spin1)
    {
        spin1.ClientEnabled = false;
        spin1.DisabledStyle.ForeColor = Color.Black;
        spin1.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
    }

    protected void ddlResponsavel_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string where =
               string.Format(@"AND us.CodigoUsuario NOT IN 
                              (SELECT tai04PI.CodigoUsuario FROM tai04_ParceirosIniciativa tai04PI WHERE tai04PI.CodigoProjeto = {2})", CodigoEntidadeUsuarioResponsavel
                                                           , e.Filter, CodigoProjeto);//not in [tai04_ParceirosIniciativa]

        string comandoSQL = cDados.getSQLComboUsuarios(CodigoEntidadeUsuarioResponsavel, e.Filter, where);

        cDados.populaComboVirtual(dsEquipe, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);
    }

    protected void ddlResponsavel_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        if (e.Value != null)
        {
            long value = 0;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsEquipe.SelectCommand = cDados.getSQLComboUsuariosPorID(CodigoEntidadeUsuarioResponsavel);

            dsEquipe.SelectParameters.Clear();
            dsEquipe.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsEquipe;
            comboBox.DataBind();
        }
    }

    protected void callbackHTMLIndicador_Callback(object source, DevExpress.Web.CallbackEventArgs e)
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



    protected void callbackNomeGerente_Callback(object sender, CallbackEventArgsBase e)
    {
        string codigo = e.Parameter;
        var result = sdsUnidadeGestora.Select(new DataSourceSelectArguments());
        string nomeUsuario = string.Empty;
        foreach (DataRowView row in result)
        {
            if (row["CodigoUnidadeNegocio"].ToString() == codigo)
                nomeUsuario = row["NomeUsuario"].ToString();
        }
        string nomeGerente = nomeUsuario;
        txtGerenteUnidade.Text = nomeGerente;

    }
}