using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Specialized;
using DevExpress.Web;
using System.Data;

public partial class _Projetos_Administracao_LicoesAprendidas_FormCadastroLicaoAprendida : System.Web.UI.Page
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
    private bool podeExcluir;
    private dados cDados;


    public string alturaTela = "";
    public string origemTermoAbertura = "";
    private int codigoConhecimentoAdquiridoDoPopup = -1;


    #endregion

    #region Event Handlers


    protected void Page_Init(object sender, EventArgs e)
    {
        string auxStr = "";
        int auxInt =-1;

        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        OrderedDictionary listaParametrosDados = new OrderedDictionary();
        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        cDados = CdadosUtil.GetCdados(listaParametrosDados);

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

        if (Request.QueryString["PassarFluxo"] != null && Request.QueryString["PassarFluxo"].ToString() == "N")
            hfGeral.Set("PodePassarFluxo", "N");
        else
            hfGeral.Set("PodePassarFluxo", "S");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

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

        readOnly = "S".Equals(Request.QueryString["RO"]);
        podeEditar = !readOnly;
        podeExcluir = !readOnly;
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

        cDados.aplicaEstiloVisual(this.Page);
    }

    private void carregaDadosTela()
    {
        hfGeral.Set("CodigoItemConhecimento", "-1");

        if (CodigoProjeto > 0)
        {

            DataSet ds = cDados.getDataSet(string.Format(@"
            SELECT 
		          [CodigoProjeto]				= p.[CodigoProjeto]
		        , [Identificacao]				= p.[NomeProjeto]
		        , [Descricao]					= p.[DescricaoProposta]
		        , [FalhaBoaPratica]				= {0}.{1}.f_getValorVarCampoObjeto(p.[CodigoProjeto], NULL, 'PR', 0, 'LCA_FalhaBoaPratica', NULL)
		        , [MudancaAprendizado]			= {0}.{1}.f_getValorVarCampoObjeto(p.[CodigoProjeto], NULL, 'PR', 0, 'LCA_GeraMudanca', NULL)
		        , [CodigoItemConhecimento]		= {0}.{1}.f_getValorVarCampoObjeto(p.[CodigoProjeto], NULL, 'PR', 0, 'LCA_IteArvCnh', NULL)
		        , [DescricaoItemConhecimento]	= {0}.{1}.f_getValorSomenteLeituraCampoObjeto(p.[CodigoProjeto], NULL, 'PR', 0, 'LCA_IteArvCnh', NULL)
		        , [Status]						= st.[DescricaoStatus]
		        , [ProjetosLCA]					= {0}.{1}.f_lca_GetProjetosLicoesAprendidas(p.[CodigoProjeto], 'disc')
	        FROM
		        {0}.{1}.[Projeto]		AS [p]
		
		        INNER JOIN {0}.{1}.[Status]		AS [st]		ON 
			        (st.[CodigoStatus] = p.[CodigoStatusProjeto])
	        WHERE
		        p.[CodigoProjeto] = {2}"
                , cDados.getDbName()                        // 0
                , cDados.getDbOwner()
                , CodigoProjeto));

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                txtNomeProjeto.Text = ds.Tables[0].Rows[0]["Identificacao"].ToString();
                txtNomeProjeto.ReadOnly = readOnly;

                mmDescricao.Text = ds.Tables[0].Rows[0]["Descricao"].ToString();
                mmDescricao.ReadOnly = readOnly;

                rbFalhaBoaPratica.Value = ds.Tables[0].Rows[0]["FalhaBoaPratica"].ToString();
                rbFalhaBoaPratica.ReadOnly = readOnly;

                rbMudancaAprendizagem.Value = ds.Tables[0].Rows[0]["MudancaAprendizado"].ToString();
                rbMudancaAprendizagem.ReadOnly = readOnly;

                hfGeral.Set("CodigoItemConhecimento", ds.Tables[0].Rows[0]["CodigoItemConhecimento"].ToString());
                txtConhecimento.Text = ds.Tables[0].Rows[0]["DescricaoItemConhecimento"].ToString();
                imgLinkParaConhecimento.ClientVisible = !readOnly;

                txtStatusLicao.Text = ds.Tables[0].Rows[0]["Status"].ToString();
                htmlProjetosLicao.Html = ds.Tables[0].Rows[0]["ProjetosLCA"].ToString();
            }
        }
    }

    #endregion

    protected void callbackSalvar_Callback(object source, CallbackEventArgs e)
    {
        string tipoOperacao = IndicaNovoProjeto ? "I" : "U";
        string mensagemErro = "";
        string gravouNovaIni = "0";

        SalvaDadosTela(ref mensagemErro);
        e.Result = tipoOperacao + "¥" + mensagemErro;

        if (string.IsNullOrEmpty(mensagemErro) && tipoOperacao.Equals("I"))
        {
            gravouNovaIni = "1";
        }

        ((ASPxCallback)source).JSProperties["cp_gravouNovaIni"] = gravouNovaIni;
        ((ASPxCallback)source).JSProperties["cpCodigoProjeto"] = CodigoProjeto;
    }

    private void SalvaDadosTela(ref string mensagemErro)
    {
        int codigoItemConhecimento;
        if (false == int.TryParse(hfGeral.Get("CodigoItemConhecimento").ToString(), out codigoItemConhecimento))
        {
            codigoItemConhecimento = -1;
        }

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
		        , @identificacaoLCA					varchar(255)	 
		        , @descricaoLCA						varchar(max)					 
		        , @indicaFalhaBoaPratica			varchar(155)	 
		        , @indicaGeraMudanca				varchar(255)	 
		        , @codigoItemConhecimento			int						 
		        , @descricaoItemConhecimento		varchar(100)	 

		        , @l_nRet						    int

		    SET @codigoEntidadeContexto				= {2}
		    SET @codigoUsuarioSistema				= {3}
		    SET @codigoProjeto						= {4}
		    SET @identificacaoLCA					= '{5}'
		    SET @descricaoLCA						= '{6}'
		    SET @indicaFalhaBoaPratica				= '{7}'
		    SET @indicaGeraMudanca					= '{8}'
		    SET @codigoItemConhecimento				= {9}
		    SET @descricaoItemConhecimento			= '{10}'

		    EXEC  @l_nRet = {0}.{1}.p_lca_gravaLicaoAprendidaWf
				    @in_codigoEntidadeContexto			=  @codigoEntidadeContexto		
			    , @in_codigoUsuarioSistema				=  @codigoUsuarioSistema			
			    , @io_codigoProjeto						=  @codigoProjeto								OUTPUT
			    , @in_identificacaoLCA					=  @identificacaoLCA					
			    , @in_descricaoLCA						=  @descricaoLCA							
			    , @in_indicaFalhaBoaPratica				=  @indicaFalhaBoaPratica		
			    , @in_indicaGeraMudanca					=  @indicaGeraMudanca				
			    , @in_codigoItemConhecimento			=  @codigoItemConhecimento		
			    , @in_descricaoItemConhecimento			=  @descricaoItemConhecimento

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
            , mmDescricao.Text.Replace("'", "''")       // 6
            , rbFalhaBoaPratica.Value
            , rbMudancaAprendizagem.Value
            , codigoItemConhecimento     // 9
            , txtConhecimento.Text.Replace("'", "''")
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
                    mensagemErro = "Falha interna durante a gravação dos dados da lição aprendida.";
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
                    mensagemErro = "Falha durante a gravação dos dados da lição aprendida.";
                }
            }
        }
    }

    protected void callbackConhecimento_Callback(object sender, CallbackEventArgsBase e)
    {
        int codigoItemConhecimento = -1;
        if (int.TryParse(e.Parameter, out codigoItemConhecimento))
        {
            string comandoSQL = string.Format(@"SELECT [dbo].[f_lca_GetIdentificacaoEAC_inLine] ({0},0) as identificacaoElemento", codigoItemConhecimento);
            DataSet ds = cDados.getDataSet(comandoSQL);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                var descricaoItem = ds.Tables[0].Rows[0]["identificacaoElemento"];
                if (Convert.IsDBNull(descricaoItem))
                {
                    txtConhecimento.Text = "";
                }
                else
                {
                    txtConhecimento.Text = descricaoItem.ToString();
                }
            }
            else
            {
                txtConhecimento.Text = "";
            }
        }
        hfGeral.Set("CodigoItemConhecimento", codigoItemConhecimento.ToString());
    }
}