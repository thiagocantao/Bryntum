using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Specialized;
using DevExpress.Web;
using System.Data;

public partial class _Projetos_Administracao_propostaDeIniciativa : System.Web.UI.Page
{
    #region Properties

    private string _connectionString;
    protected bool origemMenuProjeto;
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
                sdsDirecionador.ConnectionString =
                sdsEntidadesAcao.ConnectionString =
                sdsFocoEstrategico.ConnectionString =
                sdsGrandesDesafios.ConnectionString =
                sdsIndicador.ConnectionString =
                sdsLider.ConnectionString =
                sdsMarcosCriticos.ConnectionString =
                sdsMarcosCriticosView.ConnectionString =
                sdsOpcoesDesafios.ConnectionString =
                sdsOpcoesDirecionador.ConnectionString =
                sdsParceiros.ConnectionString =
                sdsProdutos.ConnectionString =
                sdsProdutosView.ConnectionString =
                sdsResponsaveisAcao.ConnectionString =
                sdsTipoIniciativa.ConnectionString = 
                sdsResultados.ConnectionString =
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



    string resolucaoCliente = "";


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

    #endregion

    #region Private Fields
    private bool readOnly;
    private bool podeEditar;
    private bool podeIncluir;
    private bool podeExcluir;
    //private bool existemAlteracoesNaoSalvas;
    private dados cDados;

    public string alturaTela = "";

    #endregion

    #region Event Handlers

    #region Page

    protected void Page_Init(object sender, EventArgs e)
    {
        //TERMO DE ABERTURA UTILIZADO NA CNI = DIRECT
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        OrderedDictionary listaParametrosDados = new OrderedDictionary();
        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        //TODO: Descomentar esse código

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
           // bool isPopup = getIsPopup();

            int activeTabIndex;

            int largura = 0;
            int altura = 0;
            bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

            if (int.TryParse(Request.QueryString["tab"], out activeTabIndex))
                pageControl.ActiveTabIndex = activeTabIndex;

            //int AlturaTelaGlobal = 0;
            //if (HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["AlturaWf"] != null)
            //{
            //    AlturaTelaGlobal = int.Parse(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["AlturaWf"]);
            //    if (isPopup == true)
            //    {
            //        alturaTela = ((int)(AlturaTelaGlobal - (0.288) * AlturaTelaGlobal)).ToString();
            //    }
            //    else
            //    {
            //        alturaTela = ((int)(AlturaTelaGlobal - (0.310) * AlturaTelaGlobal)).ToString();
            //    }
            //}
            //else
            //{
            //    AlturaTelaGlobal = int.Parse(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["Altura"]);
            //    if (isPopup == true)
            //    {
            //        alturaTela = ((int)(AlturaTelaGlobal - (0.288) * AlturaTelaGlobal)).ToString();
            //    }
            //    else
            //    {
            //        alturaTela = ((int)(AlturaTelaGlobal - (0.0015) * AlturaTelaGlobal)).ToString();
            //    }
            //}

            //if (!string.IsNullOrEmpty(alturaTela))
            //{

            string menuProjeto = (Request.QueryString["OrigemMenuProjeto"] + "").Trim();
            if (menuProjeto == "S")
            {
                dv01.Style.Add("height", (altura - 330) + "px");
                dv02.Style.Add("height", (altura - 330) + "px");
                dv03.Style.Add("height", (altura - 330) + "px");
                dv04.Style.Add("height", (altura - 330) + "px");
            }
            else
            {
                dv01.Style.Add("height", (altura - 510) + "px");
                dv02.Style.Add("height", (altura - 510) + "px");
                dv03.Style.Add("height", (altura - 510) + "px");
                dv04.Style.Add("height", (altura - 510) + "px");
            }



            //dv01.Style.Add("min-height", "300px");
            //dv02.Style.Add("min-height", "300px");
            //dv03.Style.Add("min-height", "300px");
            //dv04.Style.Add("min-height", "300px");

            dv01.Style.Add("overflow", "scroll");
            dv02.Style.Add("overflow", "scroll");
            dv03.Style.Add("overflow", "scroll");
            dv04.Style.Add("overflow", "scroll");
            //}
        }
        CodigoEntidadeUsuarioResponsavel = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade") ?? -1);
        CodigoUsuarioResponsavel = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado") ?? -1);
        string cp = Request.QueryString["CP"];
        cp = cp.Replace("|", "");
        CodigoProjeto = int.Parse(string.IsNullOrEmpty(cp) ? "-1" : cp);
        readOnly = "S".Equals(Request.QueryString["RO"]);
        origemMenuProjeto = "S".Equals(Request.QueryString["OrigemMenuProjeto"]);
        if (origemMenuProjeto && readOnly)
        {
            if (cDados.VerificaPermissaoUsuario(CodigoUsuarioResponsavel, CodigoEntidadeUsuarioResponsavel, CodigoProjeto, "null", "PR", 0, "null", "PR_EditaTAI"))
                readOnly = false;
        }
        podeEditar = !readOnly;
        podeExcluir = !readOnly;
        podeIncluir = !readOnly && !IndicaNovoProjeto;
        //existemAlteracoesNaoSalvas = true;


        hfGeral.Set("CodigoProjeto", CodigoProjeto);

        btnImprimir.ClientEnabled = !IndicaNovoProjeto;

        //Obtem a string de conexao e seta a propriedade 'ConnectionString' dos SQL Data Sources da página.
        ConnectionString = cDados.classeDados.getStringConexao();
    }

    private bool getIsPopup()
    {
        bool isPopup = false;
        string[] parametros = Request.UrlReferrer.AbsoluteUri.Split('?');
        string chaves = parametros[1];
        isPopup = chaves.Contains("Popup=S");
        return isPopup;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(this);
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        DefineVisibilidadeCampos();
        DefineCamposSomenteLeitura();
        //Se foi informado um código do projeto carrega dados formulário
        if (!IsPostBack && !IndicaNovoProjeto)
            CarregaDadosFormulario();
        
        ASPxPopupMsgEdicaoTai.ShowOnPageLoad = (origemMenuProjeto && !readOnly);

        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        int largura = 0;
        int altura = 0;
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        gvGrandesDesafios.Settings.VerticalScrollableHeight = (altura -550);


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
                    object codigoAcao = gvAcoes.GetDataRow(e.VisibleIndex)["SequenciaAcao"];
                    bool existeDependecia = VerificaExisteDependeciaAcao((codigoAcao ?? string.Empty).ToString());
                    if (existeDependecia)
                    {
                        e.Enabled = false;
                        e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                        e.Text = "Não é possível excluir a ação pois há produtos e/ou marcos críticos vinculadas a ela.";
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
        string mensagemErro = "";
        if (e.Exception == null)
            SalvaEdicaoIniciativa(ref mensagemErro);
    }

    protected void grid_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
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
        string nomeColunaCodigo = "CodigoEntidade";
        string nomeColunaDescricao = "NomeEntidade";
        string nomeColunaNomeEntidade = "NomeEntidadeResponsavel";
        string codigo = e.NewValues["CodigoEntidadeResponsavel"].ToString();
        string descricao = ObtemDescricaoPeloCodigo(
            codigo, nomeColunaCodigo, nomeColunaDescricao, sdsEntidadesAcao);

        if (e.NewValues.Contains(nomeColunaNomeEntidade))
            e.NewValues[nomeColunaNomeEntidade] = descricao;
        else
            e.NewValues.Add(nomeColunaNomeEntidade, descricao);

        sdsLider.SelectCommand = "SELECT CodigoUsuario, NomeUsuario, EMail FROM Usuario";
        string nomeColunaNomeUsuario = "NomeUsuarioResponsavel";
        nomeColunaCodigo = "CodigoUsuario";
        nomeColunaDescricao = "NomeUsuario";
        codigo = e.NewValues["CodigoUsuarioResponsavel"].ToString();
        descricao = ObtemDescricaoPeloCodigo(
            codigo, nomeColunaCodigo, nomeColunaDescricao, sdsLider);

        if (e.NewValues.Contains(nomeColunaNomeEntidade))
            e.NewValues[nomeColunaNomeUsuario] = descricao;
        else
            e.NewValues.Add(nomeColunaNomeUsuario, descricao);
    }

    protected void gvAcoes_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string nomeColunaCodigo = "CodigoEntidade";
        string nomeColunaDescricao = "NomeEntidade";
        string nomeColunaNomeEntidade = "NomeEntidadeResponsavel";
        string codigo = e.NewValues["CodigoEntidadeResponsavel"].ToString();
        string descricao = ObtemDescricaoPeloCodigo(
            codigo, nomeColunaCodigo, nomeColunaDescricao, sdsEntidadesAcao);

        if (e.NewValues.Contains(nomeColunaNomeEntidade))
            e.NewValues[nomeColunaNomeEntidade] = descricao;
        else
            e.NewValues.Add(nomeColunaNomeEntidade, descricao);

        sdsLider.SelectCommand = "SELECT CodigoUsuario, NomeUsuario, EMail FROM Usuario";
        string nomeColunaNomeUsuario = "NomeUsuarioResponsavel";
        nomeColunaCodigo = "CodigoUsuario";
        nomeColunaDescricao = "NomeUsuario";
        codigo = e.NewValues["CodigoUsuarioResponsavel"].ToString();
        descricao = ObtemDescricaoPeloCodigo(
            codigo, nomeColunaCodigo, nomeColunaDescricao, sdsLider);

        if (e.NewValues.Contains(nomeColunaNomeEntidade))
            e.NewValues[nomeColunaNomeUsuario] = descricao;
        else
            e.NewValues.Add(nomeColunaNomeUsuario, descricao);
    }

    protected void gvAcoes_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        string mensagemErro = "";
        SalvaEdicaoIniciativa(ref mensagemErro);
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

    #region gvResultados

    protected void gvResultados_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //string nomeColunaCodigo = "CodigoIndicador";
        //string nomeColunaDescricao = "NomeIndicador";
        //string nomeColunaNomeIndicador = "NomeIndicador";
        //string nomeColunatransformacaoProduto = "TransformacaoProduto";
        //string codigo = e.NewValues["CodigoIndicador"].ToString();
        //string descricao = ObtemDescricaoPeloCodigo(
        //    codigo, nomeColunaCodigo, nomeColunaDescricao, sdsIndicador);

        //if (e.NewValues.Contains(nomeColunaNomeIndicador))
        //    e.NewValues[nomeColunaNomeIndicador] = descricao;
        //else
        //    e.NewValues.Add(nomeColunaNomeIndicador, descricao);

        string nomeColunaSetencaResultado = "SetencaResultado";
        //string transformacaoProduto = ObtemValorPelaChave(e.NewValues, "TransformacaoProduto", false);
        string setencaResultado = ObtemValorPelaChave(e.NewValues, "SetencaResultado", false);
        //string transformacaoProduto = setencaResultado.Substring(0, setencaResultado.Length > 255 ? 255 : setencaResultado.Length);
        //string nomeIndicador = ObtemValorPelaChave(e.NewValues, "NomeIndicador", false);
        //string valorInicialTransformacao = ObtemValorPelaChave(e.NewValues, "ValorInicialTransformacao", true, "{0:n2}");
        string valorFinalTransformacao = ObtemValorPelaChave(e.NewValues, "ValorFinalTransformacao", true, "{0:n2}");
        //string dataLimitePrevista = ObtemValorPelaChave(e.NewValues, "DataLimitePrevista", false, "{0:dd/MM/yyyy}");
        //string setencaResultado = String.Format("{0} de {1} de {2} para {3} até {4}"
        //    , transformacaoProduto, nomeIndicador, valorInicialTransformacao, valorFinalTransformacao, dataLimitePrevista);

        //setencaResultado = setencaResultado.Replace(" de de ", " de ");
        //setencaResultado = setencaResultado.Replace(" da de ", " de ");
        //setencaResultado = setencaResultado.Replace(" das de ", " de ");
        //setencaResultado = setencaResultado.Replace(" dos de ", " de ");

        if (e.NewValues.Contains(nomeColunaSetencaResultado))
            e.NewValues[nomeColunaSetencaResultado] = setencaResultado;
        else
            e.NewValues.Add(nomeColunaSetencaResultado, setencaResultado);
     
    }

    protected void gvResultados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //string nomeColunaCodigo = "CodigoIndicador";
        //string nomeColunaDescricao = "NomeIndicador";
        //string nomeColunaNomeIndicador = "NomeIndicador";
        //string codigo = e.NewValues["CodigoIndicador"].ToString();
        //string descricao = ObtemDescricaoPeloCodigo(
        //    codigo, nomeColunaCodigo, nomeColunaDescricao, sdsIndicador);

        //if (e.NewValues.Contains(nomeColunaNomeIndicador))
        //    e.NewValues[nomeColunaNomeIndicador] = descricao;
        //else
        //    e.NewValues.Add(nomeColunaNomeIndicador, descricao);

        string nomeColunaSetencaResultado = "SetencaResultado";
        string setencaResultado = ObtemValorPelaChave(e.NewValues, "SetencaResultado", false);
        //string transformacaoProduto = setencaResultado.Substring(0, setencaResultado.Length > 255 ? 255 : setencaResultado.Length);

        //string nomeIndicador = ObtemValorPelaChave(e.NewValues, "NomeIndicador", false);
        //string valorInicialTransformacao = ObtemValorPelaChave(e.NewValues, "ValorInicialTransformacao", true, "{0:n2}");
        string valorFinalTransformacao = ObtemValorPelaChave(e.NewValues, "ValorFinalTransformacao", true, "{0:n2}");
        //string dataLimitePrevista = ObtemValorPelaChave(e.NewValues, "DataLimitePrevista", false, "{0:dd/MM/yyyy}");
        //string setencaResultado = String.Format("{0} de {1} de {2} para {3} até {4}"
        //    , transformacaoProduto, nomeIndicador, valorInicialTransformacao, valorFinalTransformacao, dataLimitePrevista);

        //setencaResultado = setencaResultado.Replace(" de de ", " de ");
        //setencaResultado = setencaResultado.Replace(" da de ", " de ");
        //setencaResultado = setencaResultado.Replace(" das de ", " de ");
        //setencaResultado = setencaResultado.Replace(" dos de ", " de ");

        if (e.NewValues.Contains(nomeColunaSetencaResultado))
            e.NewValues[nomeColunaSetencaResultado] = setencaResultado;
        else
            e.NewValues.Add(nomeColunaSetencaResultado, setencaResultado);
    }

    #endregion

    #region Dados

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string mensagemErro = "";
        string tipoOperacao;
        if (IndicaNovoProjeto)
        {
            SalvaNovaIniciativa(ref mensagemErro);
            tipoOperacao = "I";
            callback.JSProperties["cp_gravouNovaIni"] = 1;
        }
        else
        {
            SalvaEdicaoIniciativa(ref mensagemErro);
            tipoOperacao = "U";
            callback.JSProperties["cp_gravouNovaIni"] = 0;
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
 INSERT INTO {0}.{1}.TermoAbertura 
        (CodigoProjeto, NomeIniciativa, CodigoUnidadeNegocio, NomeUnidadeNegocio, ValorEstimado) 
 VALUES
        ({2}, '{3}', {4}, '{5}', 0)"
                , cDados.getDbName(), cDados.getDbOwner(), CodigoProjeto, dr["NomeProjeto"], dr["CodigoUnidadeNegocio"], dr["NomeUnidadeNegocio"]);

                int regAfetados = 0;
                cDados.execSQL(comandoSqlInsert, ref regAfetados);
            }
            Response.Redirect(string.Format("propostaDeIniciativa.aspx?CP={2}&RO={0}&Altura={1}", (readOnly ? "S" : "N"), alturaTela, codProjeto), true);
        }
    }

    protected void pageControl_CustomJSProperties(object sender, DevExpress.Web.CustomJSPropertiesEventArgs e)
    {
        e.Properties["cpReadOnly"] = readOnly;
    }

    #endregion

    #region Master detail

    protected void detailGrid_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["SequenciaAcao"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void gvAcoesView_DataBound(object sender, EventArgs e)
    {
        ((ASPxGridView)sender).DetailRows.ExpandAllRows();
    } 

    #endregion

    #endregion

    #region Methods

    private void DefineCamposSomenteLeitura()
    {
        //Define se a tela vai estar disponível somente pra leitura
        pnlIdentificacao.Enabled = !readOnly;
        pnlElementosResultado.Enabled = !readOnly;
        pnlElementosOperacionais.Enabled = !readOnly;

        btnSalvar.ClientVisible = !readOnly;
    }

    private void DefineVisibilidadeCampos()
    {
        ////Alguns campos estarão visíveis apenas quando a edição do formulário estiver habilitada...
        if (readOnly)
            Header.Controls.Add(cDados.getLiteral("<style type=\"text/css\"> .AvailableOnlyEditing {display: none;}</style>"));
        ////...enquanto outros estarão visíveis apenas quando o formulário estiver diponível apenas para leitura
        else
            Header.Controls.Add(cDados.getLiteral("<style type=\"text/css\"> .AvailableReadOnly {display: none;}</style>"));
    }

    private void CarregaDadosFormulario()
    {
        DataView dv = (DataView)sdsDadosFomulario.Select(new DataSourceSelectArguments());

        if (dv.Count > 0)
        {
            DataRowView row = dv[0];

            txtNomeIniciativa.Value = row["NomeIniciativa"];
            txtLider.Value = row["NomeGerenteIniciativa"];
            txtUnidadeGestora.Value = row["NomeUnidadeNegocio"];
            cmbLider.Value = row["CodigoGerenteIniciativa"];
            cmbUnidadeGestora.Value = row["CodigoUnidadeNegocio"];

            cmbTipoIniciativa.Value = row["Codigo"];
            txtTipoIniciativa.Text = row["Descricao"].ToString();

            deDataInicio.Value = row["DataInicio"];
            deDataTermino.Value = row["DataTermino"];
            txtDataInicio.Value = row["DataInicio"];
            txtDataTermino.Value = row["DataTermino"];
            //txtCentroResponsabilidade.Value = dr["CR"];
            txtFocoEstrategico.Value = row["NomeFocoEstrategico"];
            cmbFocoEstrategico.Value = row["CodigoFocoEstrategico"];
            cmbDirecionador.Value = row["CodigoOE_Direcionador"];
            txtDirecionador.Value = row["DescricaoDirecionadorEstrategico"];
            if (!Convert.IsDBNull(row["CodigoIndicadorDesafio"]))
                gvGrandesDesafios.Selection.SelectRowByKey(row["CodigoIndicadorDesafio"]);
            txtGrandesDesafios.Value = row["DescricaoDesafio"];
            txtValorEstimado.Value = row["ValorEstimado"];

            //txtPublicoAlvo.Value = row["PublicoAlvo"];
            txtValorFomento.Value = row["ValorFomento"];
            txtJustificativa.Value = row["Justificativa"];
            txtObjetivoGeral.Value = row["ObjetivoGeral"];
            txtEscopoIniciativa.Value = row["Escopo"];
            txtLimitesEscopo.Value = row["LimiteEscopo"];
            txtPremissas.Value = row["Premissas"];
            txtRestricoes.Value = row["Restricoes"];
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
        @DataInicio datetime,
        @DataTermino datetime,
        @CR varchar(50),
        @CodigoFocoEstrategico bigint,
        @CodigoDirecionador bigint,
        @NomeFocoEstrategico varchar(500),
        @NomeDirecionador varchar(500),
        @CodigoIndicadorDesafio int,
        @DescricaoDesafio varchar(2000),
        @ValorEstimado decimal(25,4),
        @ValorFomento decimal(25,4),
        @PublicoAlvo varchar(8000),
        @Justificativa varchar(8000),
        @ObjetivoGeral varchar(8000),
        @Escopo varchar(8000),
        @LimiteEscopo varchar(8000),
        @Premissas varchar(8000),
        @Restricoes varchar(8000),
        @CodigoTipoProjeto int
        
    SET @NomeIniciativa = '{2}'
    SET @CodigoGerenteIniciativa = {3}
    SET @NomeGerenteIniciativa = '{4}'
    SET @CodigoUnidadeNegocio = {5}
    SET @NomeUnidadeNegocio = '{6}'
    SET @DataInicio = {7}
    SET @DataTermino = {8}
    SET @CR = '{9}'
    SET @CodigoFocoEstrategico = {10}
    SET @CodigoDirecionador = {23}
    SET @NomeFocoEstrategico = '{11}'
    SET @NomeDirecionador = '{24}'
    SET @CodigoIndicadorDesafio = {25}
    SET @DescricaoDesafio = '{26}'
    SET @ValorEstimado = {12}
    SET @ValorFomento = {27}
    SET @PublicoAlvo = '{13}'
    SET @Justificativa = '{14}'
    SET @ObjetivoGeral = '{15}'
    SET @Escopo = '{16}'
    SET @LimiteEscopo = '{17}'
    SET @Premissas = '{18}'
    SET @Restricoes = '{19}'
    SET @CodigoTipoProjeto = '{28}'
        
DECLARE @CodigoEntidade INT,
        @CodigoUsuarioInclusao INT,
        @CodigoCategoria INT
        
    SET @CodigoEntidade = {20}
    SET @CodigoUsuarioInclusao = {21}
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

INSERT INTO {0}.{1}.TermoAbertura
           ([CodigoProjeto]
           ,[NomeIniciativa]
           ,[CodigoGerenteIniciativa]
           ,[NomeGerenteIniciativa]
           ,[CodigoUnidadeNegocio]
           ,[NomeUnidadeNegocio]
           ,[DataInicio]
           ,[DataTermino]
           ,[CR]
           ,[CodigoFocoEstrategico]
           ,[NomeFocoEstrategico]
           ,[CodigoOE_Direcionador]
           ,[DescricaoDirecionadorEstrategico]
           ,[ValorEstimado]
           ,[PublicoAlvo]
           ,[Justificativa]
           ,[ObjetivoGeral]
           ,[Escopo]
           ,[LimiteEscopo]
           ,[Premissas]
           ,[Restricoes]
           ,[CodigoIndicadorDesafio]
           ,[DescricaoDesafio]
           ,[ValorFomento]
           ,[CodigoTipoProjeto])
     VALUES
           (@CodigoProjeto
           ,@NomeIniciativa
           ,@CodigoGerenteIniciativa
           ,@NomeGerenteIniciativa
           ,@CodigoUnidadeNegocio
           ,@NomeUnidadeNegocio
           ,@DataInicio
           ,@DataTermino
           ,@CR
           ,@CodigoFocoEstrategico
           ,@NomeFocoEstrategico
           ,@CodigoDirecionador
           ,@NomeDirecionador
           ,@ValorEstimado
           ,@PublicoAlvo
           ,@Justificativa
           ,@ObjetivoGeral
           ,@Escopo
           ,@LimiteEscopo
           ,@Premissas
           ,@Restricoes
           ,@CodigoIndicadorDesafio
           ,@DescricaoDesafio
           ,@ValorFomento
           ,@CodigoTipoProjeto)
           
    SET @Erro = @Erro + @@ERROR

    UPDATE {0}.{1}.Projeto
        SET [CodigoCategoria] = (SELECT CodigoCategoria FROM {0}.{1}.Categoria WHERE DescricaoCategoria = @NomeFocoEstrategico AND CodigoEntidade = @CodigoEntidade)
           ,[CodigoTipoProjeto] = @CodigoTipoProjeto
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


 SELECT @CodigoProjeto AS CodigoProjeto,
        @Erro AS CodigoErro, 
        @MensagemErro AS MensagemErro",
             cDados.getDbName(),
             cDados.getDbOwner(),
             txtNomeIniciativa.Text.Replace("'", "''"),
             cmbLider.Value ?? "NULL",
             cmbLider.Text.Replace("'", "''"),
             cmbUnidadeGestora.Value ?? "NULL",
             cmbUnidadeGestora.Text.Replace("'", "''"),
             deDataInicio.Value == null ? "NULL" : string.Format(" CAST('{0}'     AS smalldatetime)", deDataInicio.Value),
             deDataTermino.Value == null ? "NULL" : string.Format(" CAST('{0}'     AS smalldatetime)", deDataTermino.Value),
             string.Empty,
             cmbFocoEstrategico.Value ?? "NULL",
             cmbFocoEstrategico.Text.Replace("'", "''"),
             string.IsNullOrEmpty(txtValorEstimado.Text) ? "0" : txtValorEstimado.Text.Replace(",", "."),
             "NULL", //txtPublicoAlvo.Text.Replace("'", "''"),
             txtJustificativa.Text.Replace("'", "''"),
             txtObjetivoGeral.Text.Replace("'", "''"),
             txtEscopoIniciativa.Text.Replace("'", "''"),
             txtLimitesEscopo.Text.Replace("'", "''"),
             txtPremissas.Text.Replace("'", "''"),
             txtRestricoes.Text.Replace("'", "''"),
             CodigoEntidadeUsuarioResponsavel,
             CodigoUsuarioResponsavel,
             cmbFocoEstrategico.Value,
             cmbDirecionador.Value ?? "NULL",
             cmbDirecionador.Text.Replace("'", "''"),
             gvGrandesDesafios.Selection.Count.Equals(1) ? gvGrandesDesafios.GetSelectedFieldValues("CodigoGrandeDesafio")[0] : "NULL",
             txtGrandesDesafios.Text.Replace("'", "''"),
             string.IsNullOrEmpty(txtValorFomento.Text) ? "0" : txtValorFomento.Text.Replace(",", "."),
             cmbTipoIniciativa.Value ?? "NULL");

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            object codigoErro = ds.Tables[0].Rows[0]["CodigoErro"];
            mensagemErro = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
            if (!(Convert.IsDBNull(codigoErro) || codigoErro.Equals(0)))
            {
                if (mensagemErro.Contains("UQ_Projeto_NomeProjeto"))
                {
                    mensagemErro = "Nome de projeto já existente.";
                }
                else
                {
                    mensagemErro = mensagemErro.ToString();
                }
                    
            }
            CodigoProjeto = Convert.ToInt32(ds.Tables[0].Rows[0]["CodigoProjeto"]);
        }

        //existemAlteracoesNaoSalvas = false;
    }

    private void SalvaEdicaoIniciativa(ref string mensagemErro)
    {
        AtualizaValorEstimadoIniciativa();

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
        @CR varchar(50),
        @CodigoFocoEstrategico bigint,
        @CodigoDirecionador bigint,
        @NomeFocoEstrategico varchar(500),
        @NomeDirecionador varchar(500),
        @CodigoIndicadorDesafio int,
        @DescricaoDesafio varchar(2000),
        @ValorEstimado decimal(25,4),
        @ValorFomento decimal(25,4),
        @PublicoAlvo varchar(8000),
        @Justificativa varchar(8000),
        @ObjetivoGeral varchar(8000),
        @Escopo varchar(8000),
        @LimiteEscopo varchar(8000),
        @Premissas varchar(8000),
        @Restricoes varchar(8000),
        @CodigoTipoProjeto int
        
    SET @CodigoProjeto = {22}
    SET @NomeIniciativa = '{2}'
    SET @CodigoGerenteIniciativa = {3}
    SET @NomeGerenteIniciativa = '{4}'
    SET @CodigoUnidadeNegocio = {5}
    SET @NomeUnidadeNegocio = '{6}'
    SET @DataInicio = {7}
    SET @DataTermino = {8}
    SET @CR = '{9}'
    SET @CodigoFocoEstrategico = {10}
    SET @CodigoDirecionador = {24}
    SET @NomeFocoEstrategico = '{11}'
    SET @NomeDirecionador = '{25}'
    SET @CodigoIndicadorDesafio = {26}
    SET @DescricaoDesafio = '{27}'
    SET @ValorEstimado = {12}
    SET @ValorFomento = {28}
    SET @PublicoAlvo = '{13}'
    SET @Justificativa = '{14}'
    SET @ObjetivoGeral = '{15}'
    SET @Escopo = '{16}'
    SET @LimiteEscopo = '{17}'
    SET @Premissas = '{18}'
    SET @Restricoes = '{19}'
    SET @CodigoTipoProjeto = {29}
        
DECLARE @CodigoEntidade INT,
        @CodigoUsuarioInclusao INT,
        @CodigoCategoria INT
        
    SET @CodigoEntidade = {20}
    SET @CodigoUsuarioInclusao = {21}
    SET @CodigoCategoria = NULL

UPDATE {0}.{1}.TermoAbertura
   SET [NomeIniciativa] = @NomeIniciativa
      ,[CodigoGerenteIniciativa] = @CodigoGerenteIniciativa
      ,[NomeGerenteIniciativa] = @NomeGerenteIniciativa
      ,[CodigoUnidadeNegocio] = @CodigoUnidadeNegocio
      ,[NomeUnidadeNegocio] = @NomeUnidadeNegocio
      ,[DataInicio] = @DataInicio
      ,[DataTermino] = @DataTermino
      ,[CR] = @CR
      ,[CodigoFocoEstrategico] = @CodigoFocoEstrategico
      ,[NomeFocoEstrategico] = @NomeFocoEstrategico
      ,[CodigoOE_Direcionador] = @CodigoDirecionador
      ,[DescricaoDirecionadorEstrategico] = @NomeDirecionador
      ,[ValorEstimado] = @ValorEstimado
      ,[PublicoAlvo] = @PublicoAlvo
      ,[Justificativa] = @Justificativa
      ,[ObjetivoGeral] = @ObjetivoGeral
      ,[Escopo] = @Escopo
      ,[LimiteEscopo] = @LimiteEscopo
      ,[Premissas] = @Premissas
      ,[Restricoes] = @Restricoes
      ,[CodigoIndicadorDesafio] = @CodigoIndicadorDesafio
      ,[DescricaoDesafio] = @DescricaoDesafio
      ,[ValorFomento] = @ValorFomento
      ,[CodigoTipoProjeto] = @CodigoTipoProjeto
 WHERE CodigoProjeto = @CodigoProjeto

    SET @Erro = @Erro + @@ERROR

UPDATE {0}.{1}.Projeto
   SET [NomeProjeto] = @NomeIniciativa
      ,[DataUltimaAlteracao] = GETDATE()
      ,[CodigoUsuarioUltimaAlteracao] = @CodigoUsuarioInclusao
      ,[CodigoGerenteProjeto] = @CodigoGerenteIniciativa
      ,[CodigoUnidadeNegocio] = @CodigoUnidadeNegocio
      ,[DescricaoProposta] = @ObjetivoGeral
      ,[CodigoCategoria] = (SELECT CodigoCategoria FROM {0}.{1}.Categoria WHERE DescricaoCategoria = @NomeFocoEstrategico AND CodigoEntidade = @CodigoEntidade)
      ,[CodigoTipoProjeto] = @CodigoTipoProjeto
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
        @MensagemErro AS MensagemErro",
             cDados.getDbName(),
             cDados.getDbOwner(),
             txtNomeIniciativa.Text.Replace("'", "''"),
             cmbLider.Value ?? "NULL",
             cmbLider.Text.Replace("'", "''"),
             cmbUnidadeGestora.Value ?? "NULL",
             cmbUnidadeGestora.Text.Replace("'", "''"),
             deDataInicio.Value == null ? "NULL" : string.Format(" CAST('{0}'     AS smalldatetime)", deDataInicio.Value),
             deDataTermino.Value == null ? "NULL" : string.Format(" CAST('{0}'     AS smalldatetime)", deDataTermino.Value),
             "",
             cmbFocoEstrategico.Value ?? "NULL",
             cmbFocoEstrategico.Text.Replace("'", "''"),
             string.IsNullOrEmpty(txtValorEstimado.Text) ? "0" : txtValorEstimado.Text.Replace(",", "."),
             "NULL",//txtPublicoAlvo.Text.Replace("'", "''"),
             txtJustificativa.Text.Replace("'", "''"),
             txtObjetivoGeral.Text.Replace("'", "''"),
             txtEscopoIniciativa.Text.Replace("'", "''"),
             txtLimitesEscopo.Text.Replace("'", "''"),
             txtPremissas.Text.Replace("'", "''"),
             txtRestricoes.Text.Replace("'", "''"),
             CodigoEntidadeUsuarioResponsavel,
             CodigoUsuarioResponsavel,
             CodigoProjeto,
             cmbFocoEstrategico.Value,
             cmbDirecionador.Value ?? "NULL",
             cmbDirecionador.Text.Replace("'", "''"),
             gvGrandesDesafios.Selection.Count.Equals(1) ? gvGrandesDesafios.GetSelectedFieldValues("CodigoGrandeDesafio")[0] : "NULL",
             txtGrandesDesafios.Text.Replace("'", "''"),
             string.IsNullOrEmpty(txtValorFomento.Text) ? "0" : txtValorFomento.Text.Replace(",", "."),
             cmbTipoIniciativa.Value ?? "NULL");

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
        //existemAlteracoesNaoSalvas = false;
    }

    private void AtualizaValorEstimadoIniciativa()
    {
        decimal valorTotalEstimadoIniciativa = 0;
        DataView dv = (DataView)sdsAcoes.Select(new DataSourceSelectArguments());
        for (int i = 0; i < dv.Count; i++)
        {
            decimal valorAcao = Convert.ToDecimal(dv[i]["ValorPrevisto"]);
            valorTotalEstimadoIniciativa += valorAcao;
        }
        txtValorEstimado.Value = valorTotalEstimadoIniciativa;
        string jsPropertieName = "cp_ValorTotalEstimadoIniciativa";
        if (gvAcoes.JSProperties.ContainsKey(jsPropertieName))
            gvAcoes.JSProperties[jsPropertieName] = valorTotalEstimadoIniciativa;
        else
            gvAcoes.JSProperties.Add(jsPropertieName, valorTotalEstimadoIniciativa);
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

    private bool VerificaExisteDependeciaAcao(string codigo)
    {
        string nomeColunaCodigo = "SequenciaAcao";
        SqlDataSource[] sources = new SqlDataSource[] { sdsProdutos, sdsMarcosCriticos };
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

    protected void gvAcoesView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        gvAcoesView.DataBind();
    }

    protected void cmbDirecionador_Callback(object sender, CallbackEventArgsBase e) //andre
    {
        //cmbDirecionador.DataSource = sdsDirecionador.Select(DataSourceSelectArguments.Empty);
        cmbDirecionador.DataBind();
    }

    protected void gvGrandesDesafios_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)         // Otávio
    {
        gvGrandesDesafios.DataBind();
    }
}