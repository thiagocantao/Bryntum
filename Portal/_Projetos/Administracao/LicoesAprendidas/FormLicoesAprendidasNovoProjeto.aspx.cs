using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Specialized;
using DevExpress.Web;
using System.Data;

public partial class _Projetos_Administracao_LicoesAprendidas_FormLicoesAprendidasNovoProjeto : System.Web.UI.Page
{
    #region Properties

    private string _connectionString;
    /// <summary>
    /// String de conexão para acesso a dados da tela.
    /// </summary>
    private string ConnectionString
    {
        get { return _connectionString; }
        set
        {
            //sdsLicoesAprendidas.ConnectionString = value;
            _connectionString = value;
        }
    }

    /// <summary>
    /// Indica se está sendo criado um novo projeto (codigoProjeto == -1).
    /// </summary>
    private bool IndicaNovoProjeto
    {
        get
        {
            return _codigoProjeto == -1 || _codigoProjeto == 0;
        }
    }

    private int _codigoProjeto;
    /// <summary>
    /// Código do projeto vinculado a proposta de iniciativa. Caso seja igual a '-1' trata-se de uma nova proposta de iniciativa
    /// </summary>
    private int CodigoProjeto
    {
        get { return _codigoProjeto; }
        set
        {
            cDados.setInfoSistema("CodigoProjeto", value); // variável de sessão usada no fluxo
            Session["codigoProjeto"] = value;
            _codigoProjeto = value;
        }
    }

    private int _codigoEntidadeContexto;
    /// <summary>
    /// Código da entidade do usuário logado
    /// </summary>
    private int CodigoEntidadeContexto
    {
        get { return _codigoEntidadeContexto; }
        set
        {
            cDados.setInfoSistema("CodigoEntidade", value);
            Session["codigoEntidade"] = value;
            _codigoEntidadeContexto = value;
        }
    }

    private int _codigoUsuarioLogado;
    /// <summary>
    /// Código do usuário logado
    /// </summary>
    private int CodigoUsuarioLogado
    {
        get { return _codigoUsuarioLogado; }
        set
        {
            cDados.setInfoSistema("IDUsuarioLogado", value);
            Session["codigoUsuario"] = value;
            _codigoUsuarioLogado = value;
        }
    }
    #endregion

    #region Private Fields
    private bool readOnly;
    private bool podeEditar;
    private bool podeIncluir;
    private dados cDados;

    public string alturaTela = "";
    public string origemTermoAbertura = "";

    private bool indicaProjetoDefinitivo;
    #endregion

    #region Event Handlers


    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        OrderedDictionary listaParametrosDados = new OrderedDictionary();
        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        if (Request.QueryString["PassarFluxo"] != null && Request.QueryString["PassarFluxo"].ToString() == "N")
            hfGeral.Set("PodePassarFluxo", "N");
        else
            hfGeral.Set("PodePassarFluxo", "S");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string auxStr = "";
        int auxInt = -1;

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        ConnectionString = cDados.classeDados.getStringConexao();
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

        if (Request.QueryString["CP"] != null)
        {
            auxStr = Request.QueryString["CP"].ToString();
        }

        if (string.IsNullOrEmpty(auxStr) || (false == int.TryParse(auxStr, out auxInt)))
        {
            if (Request.QueryString["ID"] != null)
            {
                auxStr = Request.QueryString["ID"].ToString();
                if (false == int.TryParse(auxStr, out auxInt))
                {
                    auxInt = -1;
                }
            }
        }

        CodigoProjeto = auxInt;

        // o projeto será considerado definitivo quando o código vier na URL e não vir o parâmetro prjTemp
        indicaProjetoDefinitivo = ( (CodigoProjeto > 0) && (Request.QueryString["prjTemp"] == null));

        readOnly = "S".Equals(Request.QueryString["RO"]);
        podeEditar = !readOnly;
        podeIncluir = !readOnly;
        btnSalvar.ClientEnabled = podeEditar;

        CodigoEntidadeContexto = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade") ?? -1);
        CodigoUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado") ?? -1);

        int alturaInt;

        if (Request.QueryString["Altura"] != null)
        {
            if (int.TryParse(Request.QueryString["Altura"], out alturaInt))
                alturaTela = (alturaInt - 0).ToString();
            if (!string.IsNullOrEmpty(alturaTela))
            {
                //height:120px;overflow:scroll
                dv01.Style.Add("height", alturaTela + "px");
                dv01.Style.Add("overflow", "auto");
                //dv01.Style.Add("width", "99%");
            }
        }

        if (!Page.IsPostBack)
        {
            carregaDadosTela();
        }

        carregaGridContexto();
        carregaGridLicoesAprendidas();
        cDados.aplicaEstiloVisual(this.Page);
        gvContexto.Settings.ShowFilterRow = false;
    }

    private void carregaDadosTela()
    {
        if (CodigoProjeto > 0)
        {
            DataSet ds = cDados.getDataSet(string.Format(@"SELECT NomeProjeto FROM Projeto WHERE CodigoProjeto = {0}", CodigoProjeto));

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                txtNomeProjeto.Text = ds.Tables[0].Rows[0]["NomeProjeto"].ToString();
                txtNomeProjeto.ReadOnly = readOnly;
            }
        }
    }

    private void carregaGridContexto()
    {
        string comandoSQL = string.Format(@"
        DECLARE @RC int
        DECLARE @in_codigoEntidade int
        DECLARE @in_codigoProjeto int
        DECLARE @in_situacaoContexto varchar(15)

        SET @in_codigoEntidade = {0}
        SET @in_codigoProjeto = {1}
        SET @in_situacaoContexto =  'ASSOCIADO'

        EXECUTE @RC = [dbo].[p_lca_getContextosLicoesAprendidas] 
        @in_codigoEntidade
        ,@in_codigoProjeto
        ,@in_situacaoContexto", CodigoEntidadeContexto, CodigoProjeto);

        DataSet dstemp = cDados.getDataSet(comandoSQL);
        gvContexto.DataSource = dstemp;
        gvContexto.DataBind();
    }


    private void carregaGridLicoesAprendidas()
    {
        string comandoSQL = string.Format(@"
        DECLARE @RC int
        DECLARE @in_codigoEntidade int
        DECLARE @in_codigoProjeto int
        DECLARE @in_situacaoLicaoAprendida varchar(15)

        SET @in_codigoEntidade = {0}
        SET @in_codigoProjeto = {1}
        SET @in_situacaoLicaoAprendida = 'ASSOCIADO'

        EXECUTE @RC = [dbo].[p_lca_listaLicoesAprendidas] 
        @in_codigoEntidade
        ,@in_codigoProjeto
       ,@in_situacaoLicaoAprendida", CodigoEntidadeContexto, CodigoProjeto);


        DataSet dstemp = cDados.getDataSet(comandoSQL);
        gvLicoesAprendidas.DataSource = dstemp;
        gvLicoesAprendidas.DataBind();
    }


    #endregion
    protected void menu_gvContexto_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu),podeIncluir, "gvContexto.AddNewRow();", podeIncluir, false, false, "LicoesApre", "Lições aprendidas 2021", this);
    }

    protected void menu_gvLicoesAprendidas_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        string scriptPopupLicoesAprendidas = string.Format(@"var url1 = '../../_Projetos/Administracao/LicoesAprendidas/popupLicoesAprendidas.aspx?CP={0}';", CodigoProjeto);
        scriptPopupLicoesAprendidas += @"window.top.showModal( url1, 'Lições Aprendidas', null, null, recebeLCAEscolhida);";

        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, scriptPopupLicoesAprendidas, podeIncluir, false, false, "LicoesApre", "Lições aprendidas 2021", this);

    }

    protected void gvContexto_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string comandoSQL;
        string codigoContexto = e.NewValues[0].ToString();
        string mensagemErro = "";
        string recarregarTela = "0";
        string queryString = "";

        bool projetoJaGravado = !IndicaNovoProjeto; // registrando o valor da propriedade antes da gravação

        SalvaDadosTela(ref mensagemErro, false);

        if (string.IsNullOrEmpty(mensagemErro) && (!projetoJaGravado))
        {
            recarregarTela = "1";
            queryString = getUpdatedQueryString();
        }

        ((ASPxGridView)sender).JSProperties["cpRecarregarTela"] = recarregarTela;
        ((ASPxGridView)sender).JSProperties["cpQueryString"] = queryString;

        comandoSQL = string.Format(@"
            DECLARE @RC int
            EXECUTE @RC = [dbo].[p_lca_associaProjetoContextoLicoesAprendidas] {0}, {1}", CodigoProjeto, codigoContexto);

        int regAfetados = 0;
        try
        {
            cDados.execSQL(comandoSQL, ref regAfetados);
        }
        catch(Exception ex)
        {

        }

        carregaGridContexto();
        e.Cancel = true;
        gvContexto.CancelEdit();
    }

    protected void gvContexto_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        ASPxComboBox combo = ((ASPxComboBox)e.Editor);

        string comandoSQL = string.Format(@"
        DECLARE @RC int
        DECLARE @in_codigoEntidade int
        DECLARE @in_codigoProjeto int
        DECLARE @in_situacaoContexto varchar(15)

        SET @in_codigoEntidade = {0}
        SET @in_codigoProjeto = {1}
        SET @in_situacaoContexto =  'DISPONIVEL'

        EXECUTE @RC = [dbo].[p_lca_getContextosLicoesAprendidas] 
        @in_codigoEntidade
        ,@in_codigoProjeto
        ,@in_situacaoContexto", CodigoEntidadeContexto, CodigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);
        combo.DataSource = ds;
        combo.DataBind();
    }

    protected void callbackSalvar_Callback(object source, CallbackEventArgs e)
    {
        string tipoOperacao = IndicaNovoProjeto ? "I" : "U";
        string mensagemErro = "";
        string gravarInstanciaFluxo = "0";

        SalvaDadosTela(ref mensagemErro, true);
        e.Result = tipoOperacao + "¥" + mensagemErro;

        if (string.IsNullOrEmpty(mensagemErro) && (!indicaProjetoDefinitivo))
        {
            gravarInstanciaFluxo = "1";
        }

        ((ASPxCallback)source).JSProperties["cpCodigoProjeto"] = CodigoProjeto;
        ((ASPxCallback)source).JSProperties["cp_gravarInstanciaFluxo"] = gravarInstanciaFluxo;
    }

    private void SalvaDadosTela(ref string mensagemErro, bool chamadaViaBotaoSalvar)
    {
        string comandoSql = string.Format(@"
        DECLARE @Erro INT
        DECLARE @MensagemErro nvarchar(2048)
        SET @Erro = 0

        BEGIN TRAN
        BEGIN TRY
            DECLARE
		          @codigoEntidadeContexto			int						 
		        , @codigoUsuarioSistema				int						 
		        , @codigoProjeto					int						 
		        , @nomeProjeto  					varchar(255)	 
		        , @gravarDataExclusao			    bit

		        , @l_nRet						    int

		    SET @codigoEntidadeContexto				= {2}
		    SET @codigoUsuarioSistema				= {3}
		    SET @codigoProjeto						= {4}
		    SET @nomeProjeto    					= '{5}'
            SET @gravarDataExclusao			        = {6}

		    EXEC  @l_nRet = {0}.{1}.p_lca_gravaDadosProjetoWf
				    @in_codigoEntidadeContexto			=  @codigoEntidadeContexto		
			    , @in_codigoUsuarioSistema				=  @codigoUsuarioSistema			
			    , @io_codigoProjeto						=  @codigoProjeto								OUTPUT
			    , @in_nomeProjeto					    =  @nomeProjeto					
		        , @in_gravarDataExclusao				=  @gravarDataExclusao;

            COMMIT
        END TRY
        BEGIN CATCH
            SET @Erro = ERROR_NUMBER()
            SET @MensagemErro = ERROR_MESSAGE()
            ROLLBACK
        END CATCH
            
            
        SELECT 
              [CodigoProjeto]   = @codigoProjeto
            , [CodigoErro]      = @Erro
            , [MensagemErro]    = @MensagemErro"
            , cDados.getDbName()                        // 0
            , cDados.getDbOwner()
            , CodigoEntidadeContexto
            , CodigoUsuarioLogado                       // 3
            , CodigoProjeto
            , txtNomeProjeto.Text.Replace("'", "''")
            , (chamadaViaBotaoSalvar || indicaProjetoDefinitivo)? 0 : 1 // 6
            );

        DataSet ds = cDados.getDataSet(comandoSql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            object codigoErro = ds.Tables[0].Rows[0]["CodigoErro"];
            mensagemErro = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
            if ((Convert.IsDBNull(codigoErro) || codigoErro.Equals(0)))
            {
                CodigoProjeto = (int)ds.Tables[0].Rows[0]["CodigoProjeto"];
                if (CodigoProjeto <= 0)
                {
                    mensagemErro = "Falha interna durante a gravação dos dados do projeto.";
                }
                else
                {
                    mensagemErro = ""; // apenas garantindo que não haverá conteúdo em mensagemErro já que não temos código do erro
                }
            }
            else
            {
                if (string.IsNullOrEmpty(mensagemErro))
                {
                    mensagemErro = "Falha durante a gravação dos dados do projeto.";
                }
            }
        }
    }

    protected void gvContexto_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string comandoSQL = "";
        string codigoContexto = e.Keys[0].ToString();
        ((ASPxGridView)sender).JSProperties["cpTextoMsg"] = "";
        ((ASPxGridView)sender).JSProperties["cpNomeImagem"] = "";
        ((ASPxGridView)sender).JSProperties["cpMostraBtnOK"] = "";
        ((ASPxGridView)sender).JSProperties["cpMostraBtnCancelar"] = "";
        ((ASPxGridView)sender).JSProperties["cpTimeout"] = "";

        bool retorno = false;

        //{

        comandoSQL = string.Format(@"
            DECLARE @RC int
            EXECUTE @RC = [dbo].[p_lca_desassociaProjetoContextoLicoesAprendidas] {0}, {1}", CodigoProjeto, codigoContexto);

        int regAfetados = 0;
        try
        {
            retorno = cDados.execSQL(comandoSQL, ref regAfetados);
            if (retorno)
            {
                ((ASPxGridView)sender).JSProperties["cpTextoMsg"] = "Contexto Excluído com sucesso";
                ((ASPxGridView)sender).JSProperties["cpNomeImagem"] = "sucesso";
                ((ASPxGridView)sender).JSProperties["cpMostraBtnOK"] = "false";
                ((ASPxGridView)sender).JSProperties["cpMostraBtnCancelar"] = "false";
                ((ASPxGridView)sender).JSProperties["cpTimeout"] = "3000";
            }
        }
        catch (Exception ex)
        {
            ((ASPxGridView)sender).JSProperties["cpTextoMsg"] = "Erro:" + ex.Message;
            ((ASPxGridView)sender).JSProperties["cpNomeImagem"] = "erro";
            ((ASPxGridView)sender).JSProperties["cpMostraBtnOK"] = "true";
            ((ASPxGridView)sender).JSProperties["cpMostraBtnCancelar"] = "false";
            ((ASPxGridView)sender).JSProperties["cpTimeout"] = "null";
        }

        carregaGridContexto();
        e.Cancel = true;
        ((ASPxGridView)sender).CancelEdit();
    }

    protected void gvContexto_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.ButtonType == ColumnCommandButtonType.Delete)
        {
            e.Visible = podeEditar;
        }
    }

    protected void gvLicoesAprendidas_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.ButtonType == ColumnCommandButtonType.Delete)
        {
            e.Visible = podeEditar;
        }
    }

    protected void gvLicoesAprendidas_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string comandoSQL = "";
        string codigoLicaoAprendida = e.Keys[0].ToString();
        ((ASPxGridView)sender).JSProperties["cpTextoMsg"] = "";
        ((ASPxGridView)sender).JSProperties["cpNomeImagem"] = "";
        ((ASPxGridView)sender).JSProperties["cpMostraBtnOK"] = "";
        ((ASPxGridView)sender).JSProperties["cpMostraBtnCancelar"] = "";
        ((ASPxGridView)sender).JSProperties["cpTimeout"] = "";

        bool retorno = false;

        //{

        comandoSQL = string.Format(@"
            DECLARE @RC int
            EXECUTE @RC = [dbo].[p_lca_desassociaProjetoLicoesAprendidas] {0}, {1}", CodigoProjeto, codigoLicaoAprendida);

        int regAfetados = 0;
        try
        {
            retorno = cDados.execSQL(comandoSQL, ref regAfetados);
            if (retorno)
            {
                ((ASPxGridView)sender).JSProperties["cpTextoMsg"] = "Lição aprendida excluída com sucesso!";
                ((ASPxGridView)sender).JSProperties["cpNomeImagem"] = "sucesso";
                ((ASPxGridView)sender).JSProperties["cpMostraBtnOK"] = "false";
                ((ASPxGridView)sender).JSProperties["cpMostraBtnCancelar"] = "false";
                ((ASPxGridView)sender).JSProperties["cpTimeout"] = "3000";
            }
            else
            {
                ((ASPxGridView)sender).JSProperties["cpTextoMsg"] = "Nenhuma lição aprendida foi excluída!";
                ((ASPxGridView)sender).JSProperties["cpNomeImagem"] = "aviso";
                ((ASPxGridView)sender).JSProperties["cpMostraBtnOK"] = "true";
                ((ASPxGridView)sender).JSProperties["cpMostraBtnCancelar"] = "false";
                ((ASPxGridView)sender).JSProperties["cpTimeout"] = "3000";
            }
        }
        catch (Exception ex)
        {
            ((ASPxGridView)sender).JSProperties["cpTextoMsg"] = "Erro:" + ex.Message;
            ((ASPxGridView)sender).JSProperties["cpNomeImagem"] = "erro";
            ((ASPxGridView)sender).JSProperties["cpMostraBtnOK"] = "true";
            ((ASPxGridView)sender).JSProperties["cpMostraBtnCancelar"] = "false";
            ((ASPxGridView)sender).JSProperties["cpTimeout"] = "null";
        }

        carregaGridLicoesAprendidas();
        e.Cancel = true;
        ((ASPxGridView)sender).CancelEdit();
    }

    private string getUpdatedQueryString()
    {
        NameValueCollection auxQueryString = HttpUtility.ParseQueryString(String.Empty);
        auxQueryString.Add(Request.QueryString);

        if (auxQueryString["CP"] == null)
        {
            auxQueryString.Add("CP", CodigoProjeto.ToString());
        }
        else
        {
            auxQueryString["CP"] = CodigoProjeto.ToString();
        }
        return auxQueryString.ToString();
    }

    protected void gvLicoesAprendidas_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        var codigoLicaoAprendida = e.Parameters;

        string comandoSQL;

        string mensagemErro = "";
        string recarregarTela = "0";
        string queryString = "";

        bool projetoJaGravado = !IndicaNovoProjeto; // registrando o valor da propriedade antes da gravação

        SalvaDadosTela(ref mensagemErro, false);

        if (string.IsNullOrEmpty(mensagemErro) && (!projetoJaGravado))
        {
            recarregarTela = "1";
            queryString = getUpdatedQueryString();
        }

        ((ASPxGridView)sender).JSProperties["cpRecarregarTela"] = recarregarTela;
        ((ASPxGridView)sender).JSProperties["cpQueryString"] = queryString;

        comandoSQL = string.Format(@"
            DECLARE @RC int
            EXECUTE @RC = [dbo].[p_lca_associaProjetoLicoesAprendidas] {0}, {1}", CodigoProjeto, codigoLicaoAprendida);

        int regAfetados = 0;
        try
        {
            cDados.execSQL(comandoSQL, ref regAfetados);
        }
        catch (Exception ex)
        {

        }

        carregaGridLicoesAprendidas();
    }
}