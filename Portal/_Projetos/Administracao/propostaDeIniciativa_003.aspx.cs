using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Specialized;
using DevExpress.Web;
using System.Data;

public partial class _Projetos_Administracao_propostaDeIniciativa_003 : System.Web.UI.Page
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
            sdsAcoes.ConnectionString =
                sdsDadosFomulario.ConnectionString =
                sdsEstrategico.ConnectionString =
                sdsIndicador.ConnectionString =
                sdsIndicadorEstrategico.ConnectionString =
                sdsIndicadorOperacional.ConnectionString =
                sdsLider.ConnectionString =
                sdsMarcosCriticos.ConnectionString =
                sdsObjetivoEstrategico.ConnectionString =
                sdsParceiros.ConnectionString =
                sdsResponsaveisAcao.ConnectionString =
                sdsResultados.ConnectionString =
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
            _codigoWorkflow= value;
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
    private dados cDados;

    public string alturaTela = "";

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
                alturaTela = (alturaInt - 113).ToString();
            if (!string.IsNullOrEmpty(alturaTela))
            {
                dv01.Style.Add("height", alturaTela + "px");
                dv02.Style.Add("height", alturaTela + "px");
                dv03.Style.Add("height", alturaTela + "px");
                dv04.Style.Add("height", alturaTela + "px");
                dv01.Style.Add("overflow", "auto");
                dv02.Style.Add("overflow", "auto");
                dv03.Style.Add("overflow", "auto");
                dv04.Style.Add("overflow", "auto");
            }
        }

        string cp = Request.QueryString["CP"];
        CodigoProjeto = int.Parse(string.IsNullOrEmpty(cp) ? "-1" : cp);
        readOnly = "S".Equals(Request.QueryString["RO"]);
        podeEditar = !readOnly;
        podeExcluir = !readOnly;
        podeIncluir = !readOnly && !IndicaNovoProjeto;
        CodigoEntidadeUsuarioResponsavel = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade") ?? -1);
        CodigoUsuarioResponsavel = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado") ?? -1);

        string cwf = Request.QueryString["CW"];
        string ci = Request.QueryString["CI"];

        CodigoWorkflow = int.Parse(string.IsNullOrEmpty(cwf) ? "0" : cwf);
        CodigoInstanciaWf = int.Parse(string.IsNullOrEmpty(ci) ? "0" : ci);
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
            CarregaDadosFormulario();

        cDados.aplicaEstiloVisual(pageControl);

        gvAcoes.DataBind();
        gvMarcosCriticos.DataBind();
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
                }
                else if (grid == gvAcoes)
                {
                    SqlDataSource[] sources = new SqlDataSource[] { sdsMarcosCriticos};
                    string nomeColunaCodigo = "SequenciaAcao";
                    string codigoAcao = gvAcoes.GetDataRow(e.VisibleIndex)[nomeColunaCodigo].ToString();
                    bool existeDependecia = ExisteDependenciaRegistro(codigoAcao, sources, nomeColunaCodigo);
                    if (existeDependecia)
                    {
                        e.Enabled = false;
                        e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                        e.Text = "Não é possível excluir a ação pois há marcos críticos vinculadas a ela.";
                    }
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

    #region gvAcoes

    protected void gvAcoes_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (gvAcoes.IsEditing && e.Column.FieldName == "CodigoUsuarioResponsavel")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            combo.ItemsRequestedByFilterCondition += cmbLider_ItemsRequestedByFilterCondition;
            combo.ItemRequestedByValue += cmbLider_ItemRequestedByValue;
        }
    }

    protected void gvAcoes_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string nomeColunaCodigo, nomeColunaDescricao, codigo, descricao;
        string nomeColunaNomeUsuario = "NomeUsuarioResponsavel";

        sdsLider.SelectCommand = "SELECT CodigoUsuario, NomeUsuario, EMail FROM Usuario";
        nomeColunaCodigo = "CodigoUsuario";
        nomeColunaDescricao = "NomeUsuario";
        codigo = e.NewValues["CodigoUsuarioResponsavel"].ToString();
        descricao = ObtemDescricaoPeloCodigo(
            codigo, nomeColunaCodigo, nomeColunaDescricao, sdsLider);

        if (e.NewValues.Contains(nomeColunaNomeUsuario))
            e.NewValues[nomeColunaNomeUsuario] = descricao;
        else
            e.NewValues.Add(nomeColunaNomeUsuario, descricao);
    }

    protected void gvAcoes_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string nomeColunaCodigo, nomeColunaDescricao, codigo, descricao;
        string nomeColunaNomeUsuario = "NomeUsuarioResponsavel";

        sdsLider.SelectCommand = "SELECT CodigoUsuario, NomeUsuario, EMail FROM Usuario";
        nomeColunaCodigo = "CodigoUsuario";
        nomeColunaDescricao = "NomeUsuario";
        codigo = e.NewValues["CodigoUsuarioResponsavel"].ToString();
        descricao = ObtemDescricaoPeloCodigo(
            codigo, nomeColunaCodigo, nomeColunaDescricao, sdsLider);

        if (e.NewValues.Contains(nomeColunaNomeUsuario))
            e.NewValues[nomeColunaNomeUsuario] = descricao;
        else
            e.NewValues.Add(nomeColunaNomeUsuario, descricao);
    }

    protected void gvAcoes_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        SalvaEdicaoIniciativa();
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
            ControlParameter param1 = new ControlParameter("CodigoSuperior", editor.UniqueID, "Value");
            sdsIndicadorEstrategico.FilterParameters.Clear();
            sdsIndicadorEstrategico.FilterParameters.Add(param1);
            if (gvEstrategico.IsEditing && !gvEstrategico.IsNewRowEditing)
                editor.ClientSideEvents.Init = "function(s, e) { s.SetEnabled(false);}";
            else
                editor.ClientSideEvents.Init = "function(s, e) { s.SetEnabled(true);}";

            if (gvEstrategico.IsNewRowEditing)
                sdsObjetivoEstrategico.SelectParameters["CodigoObjetivoAtual"].DefaultValue = "-1";
            else
                sdsObjetivoEstrategico.SelectParameters["CodigoObjetivoAtual"].DefaultValue = editor.Value.ToString();
        }

        if ("CodigoIndicador".Equals(e.Column.FieldName))
        {
            ASPxComboBox editor = (ASPxComboBox)e.Editor;

            if (gvEstrategico.IsNewRowEditing)
                sdsIndicadorEstrategico.SelectParameters["CodigoIndicadorAtual"].DefaultValue = "-1";
            else
                sdsIndicadorEstrategico.SelectParameters["CodigoIndicadorAtual"].DefaultValue = editor.Value.ToString();

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
            codigo, nomeColunaCodigo, nomeColunaDescricao, sdsIndicador);

        if (e.NewValues.Contains(nomeColunaNomeIndicador))
            e.NewValues[nomeColunaNomeIndicador] = descricao;
        else
            e.NewValues.Add(nomeColunaNomeIndicador, descricao);

        string nomeColunaSetencaResultado = "SetencaResultado";
        string transformacaoProduto = ObtemValorPelaChave(e.NewValues, "TransformacaoProduto", false);
        string nomeIndicador = ObtemValorPelaChave(e.NewValues, "NomeIndicador", false);
        string valorInicialTransformacao = ObtemValorPelaChave(e.NewValues, "ValorInicialTransformacao", true, "{0:n2}");
        string valorFinalTransformacao = ObtemValorPelaChave(e.NewValues, "ValorFinalTransformacao", true, "{0:n2}");
        string dataLimitePrevista = ObtemValorPelaChave(e.NewValues, "DataLimitePrevista", false, "{0:dd/MM/yyyy}");
        string setencaResultado = String.Format("{0} de {1} de {2} para {3} até {4}"
            , transformacaoProduto, nomeIndicador, valorInicialTransformacao, valorFinalTransformacao, dataLimitePrevista);

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
        string valorInicialTransformacao = ObtemValorPelaChave(e.NewValues, "ValorInicialTransformacao", true, "{0:n2}");
        string valorFinalTransformacao = ObtemValorPelaChave(e.NewValues, "ValorFinalTransformacao", true, "{0:n2}");
        string dataLimitePrevista = ObtemValorPelaChave(e.NewValues, "DataLimitePrevista", false, "{0:dd/MM/yyyy}");
        string setencaResultado = String.Format("{0} de {1} de {2} para {3} até {4}"
            , transformacaoProduto, nomeIndicador, valorInicialTransformacao, valorFinalTransformacao, dataLimitePrevista);

        if (e.NewValues.Contains(nomeColunaSetencaResultado))
            e.NewValues[nomeColunaSetencaResultado] = setencaResultado;
        else
            e.NewValues.Add(nomeColunaSetencaResultado, setencaResultado);
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
 INSERT INTO {0}.{1}.TermoAbertura03 
        (CodigoProjeto, NomeIniciativa, CodigoUnidadeNegocio, NomeUnidadeNegocio, ValorEstimado, ValorContrapartida) 
 VALUES
        ({2}, '{3}', {4}, '{5}', 0, 0)"
                , cDados.getDbName(), cDados.getDbOwner(), CodigoProjeto, dr["NomeProjeto"], dr["CodigoUnidadeNegocio"], dr["NomeUnidadeNegocio"]);

                int regAfetados = 0;
                cDados.execSQL(comandoSqlInsert, ref regAfetados);
            }
            Response.Redirect(string.Format("propostaDeIniciativa_003.aspx?CP={2}&RO={0}&Altura={1}", (readOnly ? "S" : "N"), alturaTela, codProjeto), true);
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
        //Define se a tela vai estar disponível somente pra leitura
        pnlDescricaoProjeto.Enabled = permiteEdicao;
        pnlElementosResultado.Enabled = permiteEdicao;
        pnlElementosOperacionais.Enabled = permiteEdicao;
        pnlAlinhamentoEstrategico.Enabled = permiteEdicao;

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
            deDataInicio.Value = row["DataInicio"];
            deDataTermino.Value = row["DataTermino"];
            txtValorProjeto.Value = row["ValorEstimado"];
            txtValorContrapartida.Value = row["ValorContrapartida"];
            txtFontesFinanciamento.Value = row["FontesFinanciamento"];
            txtPublicoAlvo.Value = row["PublicoAlvo"];
            txtObjetivoGeral.Value = row["ObjetivoGeral"];
            txtJustificativa.Value = row["Justificativa"];
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
        @DataInicio datetime,
        @DataTermino datetime,
        @ValorEstimado decimal(25,4),
        @ValorContrapartida decimal(25,4),
        @FontesFinanciamento varchar(500),
        @PublicoAlvo varchar(500),
        @Justificativa varchar(8000),
        @ObjetivoGeral varchar(8000),
        @Escopo varchar(8000)

    SET @CodigoProjeto = -1
    SET @NomeIniciativa = '{3}'
    SET @CodigoGerenteIniciativa = {4}
    SET @NomeGerenteIniciativa = '{5}'
    SET @CodigoUnidadeNegocio = {6}
    SET @NomeUnidadeNegocio = '{7}'
    SET @DataInicio = {8}
    SET @DataTermino = {9}
    SET @ValorEstimado = {10}
    SET @ValorContrapartida = {11}
    SET @FontesFinanciamento = '{12}'
    SET @PublicoAlvo = '{13}'
    SET @ObjetivoGeral = '{14}'
    SET @Justificativa = '{15}'
    SET @Escopo = '{16}'

DECLARE @CodigoEntidade INT,
        @CodigoUsuarioInclusao INT,
        @CodigoCategoria INT
        
    SET @CodigoEntidade = {17}
    SET @CodigoUsuarioInclusao = {18}
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

INSERT INTO {0}.{1}.TermoAbertura03
    (  [CodigoProjeto], [NomeIniciativa], [CodigoGerenteIniciativa], [NomeGerenteIniciativa], [CodigoUnidadeNegocio], [NomeUnidadeNegocio],[DataInicio]
     , [DataTermino], [ValorEstimado], [ValorContrapartida], [FontesFinanciamento], [PublicoAlvo], [ObjetivoGeral], [Justificativa], [Escopo])
  VALUES
    (  @CodigoProjeto, @NomeIniciativa, @CodigoGerenteIniciativa, @NomeGerenteIniciativa, @CodigoUnidadeNegocio, @NomeUnidadeNegocio, @DataInicio 
     , @DataTermino, @ValorEstimado, @ValorContrapartida, @FontesFinanciamento, @PublicoAlvo, @ObjetivoGeral, @Justificativa, @Escopo)
           
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
            , deDataInicio.Value == null ? "NULL" : string.Format(" CAST('{0}'     AS smalldatetime)", deDataInicio.Value)
            , deDataTermino.Value == null ? "NULL" : string.Format(" CAST('{0}'     AS smalldatetime)", deDataTermino.Value)
            , string.IsNullOrEmpty(txtValorProjeto.Text) ? "0" : txtValorProjeto.Text.Replace(",", ".")
            , string.IsNullOrEmpty(txtValorContrapartida.Text) ? "0" : txtValorContrapartida.Text.Replace(",", ".")
            , txtFontesFinanciamento.Text.Replace("'", "''")
            , txtPublicoAlvo.Text.Replace("'", "''")
            , txtObjetivoGeral.Text.Replace("'", "''")
            , txtJustificativa.Text.Replace("'", "''")
            , txtEscopoIniciativa.Text.Replace("'", "''")
            , CodigoEntidadeUsuarioResponsavel
            , CodigoUsuarioResponsavel);
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
        @DataInicio datetime,
        @DataTermino datetime,
        @ValorEstimado decimal(25,4),
        @ValorContrapartida decimal(25,4),
        @FontesFinanciamento varchar(500),
        @PublicoAlvo varchar(500),
        @Justificativa varchar(8000),
        @ObjetivoGeral varchar(8000),
        @Escopo varchar(8000)

    SET @CodigoProjeto = {2}
    SET @NomeIniciativa = '{3}'
    SET @CodigoGerenteIniciativa = {4}
    SET @NomeGerenteIniciativa = '{5}'
    SET @CodigoUnidadeNegocio = {6}
    SET @NomeUnidadeNegocio = '{7}'
    SET @DataInicio = {8}
    SET @DataTermino = {9}
    SET @ValorEstimado = {10}
    SET @ValorContrapartida = {11}
    SET @FontesFinanciamento = '{12}'
    SET @PublicoAlvo = '{13}'
    SET @ObjetivoGeral = '{14}'
    SET @Justificativa = '{15}'
    SET @Escopo = '{16}'

DECLARE @CodigoEntidade INT,
        @CodigoUsuarioInclusao INT,
        @CodigoCategoria INT
        
    SET @CodigoEntidade = {17}
    SET @CodigoUsuarioInclusao = {18}
    SET @CodigoCategoria = NULL

UPDATE {0}.{1}.TermoAbertura03
   SET [NomeIniciativa]				= @NomeIniciativa
     , [CodigoGerenteIniciativa]	= @CodigoGerenteIniciativa
     , [NomeGerenteIniciativa]		= @NomeGerenteIniciativa
     , [CodigoUnidadeNegocio]		= @CodigoUnidadeNegocio
     , [NomeUnidadeNegocio]			= @NomeUnidadeNegocio
     , [DataInicio]					= @DataInicio
     , [DataTermino]				= @DataTermino
     , [ValorEstimado]				= @ValorEstimado
     , [ValorContrapartida]			= @ValorContrapartida
     , [FontesFinanciamento]	    = @FontesFinanciamento
     , [PublicoAlvo]				= @PublicoAlvo
     , [ObjetivoGeral]              = @ObjetivoGeral
     , [Justificativa]				= @Justificativa
     , [Escopo]				        = @Escopo
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
            , CodigoProjeto
            , txtNomeProjeto.Text.Replace("'", "''")
            , cmbLider.Value ?? "NULL"
            , cmbLider.Text.Replace("'", "''")
            , cmbUnidadeGestora.Value ?? "NULL"
            , cmbUnidadeGestora.Text.Replace("'", "''")
            , deDataInicio.Value == null ? "NULL" : string.Format(" CAST('{0}'     AS smalldatetime)", deDataInicio.Value)
            , deDataTermino.Value == null ? "NULL" : string.Format(" CAST('{0}'     AS smalldatetime)", deDataTermino.Value)
            , string.IsNullOrEmpty(txtValorProjeto.Text) ? "0" : txtValorProjeto.Text.Replace(",", ".")
            , string.IsNullOrEmpty(txtValorContrapartida.Text) ? "0" : txtValorContrapartida.Text.Replace(",", ".")
            , txtFontesFinanciamento.Text.Replace("'", "''")
            , txtPublicoAlvo.Text.Replace("'", "''")
            , txtObjetivoGeral.Text.Replace("'", "''")
            , txtJustificativa.Text.Replace("'", "''")
            , txtEscopoIniciativa.Text.Replace("'", "''")
            , CodigoEntidadeUsuarioResponsavel
            , CodigoUsuarioResponsavel);

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

    #endregion
}