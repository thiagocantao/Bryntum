/*
 * OBSERVAÇÕES
 * 
 * Telas qeu recibe via Request os siguentes parâmetros para preparar a tela:
 * 
 * "ITO" : iniciaisTO   :: Inicias do objeto alvo da permissão.
 * "COE" : codigoOE     :: Código do objeto alvo da permissão.
 * "TOE" : tituloOE     :: Título ou descrição do Objeto alvo da permissão.
 * "TME" : tituloME     :: Título do mapa estratégico ao qual pertenece o Objeto alvo da permissão.
 * "COP" : codigoOP     :: Código Objeto Pai do objeto alvo da permissão.
 * "TIT" : captionOE    :: Si posee o não header na grid, aonde fica detalhado a descrição do objeto alvo. ('S': sim, 'N': não)
 *                          Tamben e para definir a altura da tela, caso qeu seja 'N', a altura da grid vai ter que ser segundo a
 *                          altura da tela (ob. olhear metodo [private void defineAlturaTela(...)]).
 * 
 * 
 * Modificações:
 * 
 * - 28/02/2011 :: Alejandro : Adaptação da função de obtemção de dados.
 * - 02/03/2011 :: Alejandro : no método 'private string getSQLpermissoes(DataTable dt, string tipoAcao, int codigoUsuario)'
 *                              se modfico para comparar apenas o primeiros 4 bit's.
 * - 04/03/2011 :: Géter     : Correções diversas
 * - 10/03/2011 :: Alejandro : Correções diversas.
 * - 17/03/2011 :: Alejandro : 
 */
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using System.Drawing;

public partial class _Estrategias_InteressadosObjeto : System.Web.UI.Page
{
    #region   ===== Variáveis da classe ====
    dados cDados;
    public bool podeIncluir = true, podeEditar = true;
    //public string estiloFooter = "dxgvFooter";
    private string iniciaisTO;
    private string codigoOE;
    private string tituloOE;
    private string tituloME;
    private string codigoOP = "0";
    private string captionOE = "";
    private string idUsuarioLogado;
    private string idEntidadeLogada;
    private string dbName;
    private string dbOwner;
    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;
    //ChaveComposta chavePrimaria;
    private string codigoObjetoAssociadoPK = "";
    private string codigoTipoAssociacaoPK = "";
    private string codigoUsuarioPK = "";
    private string definicaoUnidade = "Unidade";
    private string definicaoUnidadePlural = "Unidades";
    private string definicaoEntidade = "Entidade";
    private string definicaoEntidadePlural = "Entidades";
    private string definicaoCarteira = "Carteira";
    private string definicaoCarteiraPlural = "Carteiras";

    public class ChaveComposta
    {
        //CodigoObjetoAssociado;CodigoTipoAssociacao;CodigoUsuario
        private string _codigoObjetoAssociado;
        private string _codigoTipoAssociacao;
        private string _codigoUsuario;

        public string CodigoObjetoAssociado
        {
            get { return _codigoObjetoAssociado; }
            set { _codigoObjetoAssociado = value; }
        }
        public string CodigoTipoAssociacao
        {
            get { return _codigoTipoAssociacao; }
            set { _codigoTipoAssociacao = value; }
        }
        public string CodigoUsuario
        {
            get { return _codigoUsuario; }
            set { _codigoUsuario = value; }
        }

        public ChaveComposta()
        {
            this._codigoObjetoAssociado = "";
            this._codigoTipoAssociacao = "";
            this._codigoUsuario = "";
        }

        //Otro constructor al que le pasamos todos los datos
        public ChaveComposta(string codigoObjetoAssociado, string codigoTipoAssociacao, string codigoUsuario)
        {
            this._codigoObjetoAssociado = codigoObjetoAssociado;
            this._codigoTipoAssociacao = codigoTipoAssociacao;
            this._codigoUsuario = codigoUsuario;
        }
    }

    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();

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

        definicaoUnidade = Resources.traducao.unidade;
        definicaoUnidadePlural = Resources.traducao.unidades;
        definicaoEntidade = Resources.traducao.entidade;
        definicaoEntidadePlural = Resources.traducao.entidades;
        definicaoCarteira = Resources.traducao.carteira;
        definicaoCarteiraPlural = Resources.traducao.carteiras;

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado").ToString();
        idEntidadeLogada = cDados.getInfoSistema("CodigoEntidade").ToString();

        DataSet ds = cDados.getDefinicaoUnidade(int.Parse(idEntidadeLogada));
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            definicaoUnidade = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
            definicaoUnidadePlural = ds.Tables[0].Rows[0]["DescricaoPluralTipoUnidade"].ToString();
        }

        ds = cDados.getDefinicaoEntidade(int.Parse(idEntidadeLogada));
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            definicaoEntidade = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
            definicaoEntidadePlural = ds.Tables[0].Rows[0]["DescricaoPluralTipoUnidade"].ToString();
        }

        nbAssociacao.AutoCollapse = true; // necssário para que o javascript funcione (GetActiveGroup);

        HeaderOnTela();
        defineLabelCarteira();
        /*obter os parâmetros desde tela. Segundo de qual tela seja chamada, se adaptara ao tipo de objeto.*/
        if (Request.QueryString["ITO"] != null) iniciaisTO = Request.QueryString["ITO"].ToString();
        if (Request.QueryString["COE"] != null && Request.QueryString["COE"].ToString() != "") codigoOE = Request.QueryString["COE"].ToString();
        if (Request.QueryString["TOE"] != null && Request.QueryString["TOE"].ToString() != "") tituloOE = Request.QueryString["TOE"].ToString();
        if (Request.QueryString["TME"] != null && Request.QueryString["TME"].ToString() != "") tituloME = Request.QueryString["TME"].ToString();
        if (Request.QueryString["COP"] != null && Request.QueryString["COP"].ToString() != "") codigoOP = Request.QueryString["COP"].ToString();
        if (Request.QueryString["TIT"] != null && Request.QueryString["TIT"].ToString() != "") captionOE = Request.QueryString["TIT"].ToString();

        if (iniciaisTO == "PR" && codigoOE != "")
        {
            cDados.verificaPermissaoProjetoInativo(int.Parse(codigoOE), ref podeIncluir, ref podeEditar, ref podeEditar);
        }

        // em função da nova opção de compartilhar indicador por unidade, quando for mostrar permissões de um indicador, e não tiver sido passado o pai do indicador, então o pai será a entidade logada.
        if (iniciaisTO == "IN" && codigoOP == "0")
        {
            codigoOP = idEntidadeLogada;
        }

        if (!IsPostBack)
        {
            cerrarSessionCheck();
            hfGeral.Set("itemMenu", iniciaisTO);
            hfGeral.Set("itemMenuIniciais", iniciaisTO);
            hfGeral.Set("TipoOperacao", "Consultar");
            hfGeral.Set("HerdaPermissoes", "S");
            hfGeral.Set("CodigoObjetoAssociado", "-1");
            hfGeral.Set("CodigoUsuarioPermissao", "-1");
            hfGeral.Set("CodigosPerfisSelecionados", "-1");

            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            cDados.aplicaEstiloVisual(Page);

            carregaMenuLateral(hfGeral.Get("itemMenuIniciais").ToString());
            carregaListaPerfis("-1", iniciaisTO, "0");
            carregaListaPerfisAssociados("-1", iniciaisTO, "0");


        }

        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();
    }

    private void carregaGVPerfisAtribuiveis()
    {
        string comandSQL = string.Format(@"DECLARE @RC int
        DECLARE @CodigoEntidadeContexto int
        DECLARE @CodigoUsuarioSistema int
        DECLARE @IniciaisTipoObjeto char(2)
        DECLARE @codigoObjeto bigint
        DECLARE @codigoObjetoPai bigint

        SET @CodigoEntidadeContexto = {0}
        SET @CodigoUsuarioSistema = {1}
        SET @IniciaisTipoObjeto = '{2}'
        SET @codigoObjeto = {3}
        SET @codigoObjetoPai = {4}

        EXECUTE @RC = [dbo].[p_perm_obtemPerfisAtribuiveisTipoObjeto] 
           @CodigoEntidadeContexto
          ,@CodigoUsuarioSistema
          ,@IniciaisTipoObjeto
          ,@codigoObjeto
          ,@codigoObjetoPai", idEntidadeLogada, idUsuarioLogado, iniciaisTO, codigoOE, codigoOP);

        DataSet ds = cDados.getDataSet(comandSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            gvPerfisAtribuiveis.DataSource = ds;
            gvPerfisAtribuiveis.DataBind();
        }


    }

    protected void Page_Load(object sender, EventArgs e)
    {
        gvDados.SettingsPager.AlwaysShowPager = true;
        string nomeSession = "dt" + hfGeral.Get("itemMenu").ToString();
        if (Session[nomeSession] != null)
        {
            DataTable dt = (DataTable)Session[nomeSession];

            if (null != dt)
            {
                gvPermissoes.DataSource = dt;
                gvPermissoes.DataBind();
            }

            string siglaAssociacao = hfGeral.Get("itemMenu").ToString();

            //Prencho o Title da grid gvPermissoes.
            getTituloGridPermissoes(siglaAssociacao);
        }
        if (!Page.IsCallback && !Page.IsPostBack)
        {
            pcAbas.ActiveTabIndex = 0;
            carregaGvDados();
            gvDados.DataBind();
            carregaGVPerfisAtribuiveis();
            
        }

        populaComboInteressado();

        gvPermissoes.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
    }

    private void defineLabelCarteira()
    {
        DataSet dsParametro = cDados.getParametrosSistema("labelCarteiras", "labelCarteirasPlural");


        if ((cDados.DataSetOk(dsParametro)) && (cDados.DataTableOk(dsParametro.Tables[0])))
        {
            definicaoCarteira = dsParametro.Tables[0].Rows[0]["labelCarteiras"].ToString();
            definicaoCarteiraPlural = dsParametro.Tables[0].Rows[0]["labelCarteirasPlural"].ToString();
        }
    }
    #region VARIOS

    private void defineAlturaTela(string resolucaoCliente)
    {
        if (captionOE.Equals("N"))
        {
            btnFechar.ClientVisible = false;
        }
        else
        {
            btnFechar.ClientVisible = true;
        }
    }

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/InteressadosObjeto.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ASPxListbox.js""></script>"));
        this.TH(this.TS("barraNavegacao", "InteressadosObjeto", "ASPxListbox"));
    }

    private string getIniciaisTipoAssociacao(string textoAssociacao)
    {
        string retorno = "";

        /* EN UN ST PR ME PP TM OB IN CT */
        if (textoAssociacao == "mnEntidade") retorno = "EN";
        else if (textoAssociacao == "mnUnidade") retorno = "UN";
        else if (textoAssociacao == "mnEstrategia") retorno = "ST";
        else if (textoAssociacao == "mnProjeto") retorno = "PR";
        else if (textoAssociacao == "mnMapa") retorno = "ME";
        else if (textoAssociacao == "mnPerspectiva") retorno = "PP";
        else if (textoAssociacao == "mnTema") retorno = "TM";
        else if (textoAssociacao == "mnObjetivo") retorno = "OB";
        else if (textoAssociacao == "mnIndicador") retorno = "IN";
        else if (textoAssociacao == "mnContrato") retorno = "CT";
        else if (textoAssociacao == "mnDemandaComplexa") retorno = "DC";
        else if (textoAssociacao == "mnDemandaSimple") retorno = "DS";
        else if (textoAssociacao == "mnProcesso") retorno = "PC";
        else if (textoAssociacao == "mnCarteira") retorno = "CP";
        else if (textoAssociacao == "mnEquipe") retorno = "EQ";


        return retorno;
    }

    private string getDescricaoAssociacaoFromIniciais(string iniciaiTo)
    {
        /* EN UN ST PR ME PP TM OB IN CT */
        string retorno = "";

        if (iniciaisTO.Equals("EN")) retorno = definicaoEntidade;
        else if (iniciaisTO.Equals("UN")) retorno = definicaoUnidade;
        else if (iniciaisTO.Equals("ST")) retorno = Resources.traducao.InteressadosObjeto_estrat_gia;
        else if (iniciaisTO.Equals("PR")) retorno = Resources.traducao.InteressadosObjeto_projeto;
        else if (iniciaisTO.Equals("ME")) retorno = Resources.traducao.InteressadosObjeto_mapa_estrat_gico;
        else if (iniciaisTO.Equals("PP")) retorno = Resources.traducao.InteressadosObjeto_perspectiva;
        else if (iniciaisTO.Equals("TM")) retorno = Resources.traducao.InteressadosObjeto_tema;
        else if (iniciaisTO.Equals("OB")) retorno = Resources.traducao.InteressadosObjeto_objetivo_estrat_gico;
        else if (iniciaisTO.Equals("IN")) retorno = Resources.traducao.InteressadosObjeto_indicador;
        else if (iniciaisTO.Equals("CT")) retorno = Resources.traducao.InteressadosObjeto_contrato;
        else if (iniciaisTO.Equals("DC")) retorno = Resources.traducao.InteressadosObjeto_demanda_complexa;
        else if (iniciaisTO.Equals("DS")) retorno = Resources.traducao.InteressadosObjeto_demanda_simples;
        else if (iniciaisTO.Equals("PC")) retorno = Resources.traducao.InteressadosObjeto_processo;
        else if (iniciaisTO.Equals("CP")) retorno = definicaoCarteira;

        return retorno;
    }

    private void carregaMenuLateral(string iniciaisTO)
    {
        /* EN UN ST PR ME PP TM OB IN CT */
        string strComando = string.Format(@"
            SELECT 
                  [CodigoTipoAssociacao]
                , [DescricaoTipoAssociacao]
                , [IniciaisTipoAssociacao] 
            FROM 
                {0}.{1}.f_GetTiposAssociacoesDescendentes('{2}', 'PERMISSAO') 
            ", dbName, dbOwner, iniciaisTO);
        DataSet ds = cDados.getDataSet(strComando);
        if (cDados.DataSetOk(ds))
        {
            string menuId, DescricaoTipoObjeto, iniciaisTipoObjeto;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DescricaoTipoObjeto = dr["DescricaoTipoAssociacao"].ToString();
                iniciaisTipoObjeto = dr["IniciaisTipoAssociacao"].ToString();

                if (iniciaisTipoObjeto.Equals("EN")) menuId = "mnEntidade";
                else if (iniciaisTipoObjeto.Equals("UN")) menuId = "mnUnidades";
                else if (iniciaisTipoObjeto.Equals("ST")) menuId = "mnEstrategia";
                else if (iniciaisTipoObjeto.Equals("ME")) menuId = "mnMapas";
                else if (iniciaisTipoObjeto.Equals("PR")) menuId = "mnProjeto";
                else if (iniciaisTipoObjeto.Equals("PP")) menuId = "mnPerspectiva";
                else if (iniciaisTipoObjeto.Equals("TM")) menuId = "mnTema";
                else if (iniciaisTipoObjeto.Equals("OB")) menuId = "mnObjetivo";
                else if (iniciaisTipoObjeto.Equals("IN")) menuId = "mnIndicador";
                else if (iniciaisTipoObjeto.Equals("CT")) menuId = "mnContrato";
                else if (iniciaisTipoObjeto.Equals("DC")) menuId = "mnDemandaComplexa";
                else if (iniciaisTipoObjeto.Equals("DS")) menuId = "mnDemandaSimple";
                else if (iniciaisTipoObjeto.Equals("PC")) menuId = "mnProcesso";
                else if (iniciaisTipoObjeto.Equals("CP")) menuId = "mnCarteira";
                else if (iniciaisTipoObjeto.Equals("EQ")) menuId = "mnEquipe";
                else
                    menuId = "";

                if (menuId != "")
                    nbAssociacao.Groups.FindByName("gpAssociacao").Items.Add(new NavBarItem(DescricaoTipoObjeto, menuId));
            }

            if (nbAssociacao.Items.Count > 0)
            {
                hfGeral.Set("itemMenu", getIniciaisTipoAssociacao(nbAssociacao.Items[0].Name));
                nbAssociacao.Items[0].Selected = true;
            }
        }
    }

    #endregion

    #region SESSIONES

    private void populaSessiones(string codigoUsuarioPermissao)
    {
        /* EN UN ST PR ME PP TM OB IN CT */
        DataSet ds;
        DataSet dsTA;
        string sqlPermissao;
        string iniciais = iniciaisTO;
        string listaPerfis = "NULL";

        string strComando = string.Format(@"
            SELECT 
                  [CodigoTipoAssociacao]
                , [DescricaoTipoAssociacao]
                , [IniciaisTipoAssociacao] 
            FROM 
                {0}.{1}.f_GetTiposAssociacoesDescendentes('{2}', 'PERMISSAO') 
            ", dbName, dbOwner, iniciaisTO);

        dsTA = cDados.getDataSet(strComando);

        if (cDados.DataSetOk(dsTA))
        {
            string DescricaoTipoObjeto, iniciaisTipoObjeto;
            foreach (DataRow dr in dsTA.Tables[0].Rows)
            {
                string nomeSession = "";
                DescricaoTipoObjeto = dr["DescricaoTipoAssociacao"].ToString();
                iniciaisTipoObjeto = dr["IniciaisTipoAssociacao"].ToString();
                nomeSession = "dt" + iniciaisTipoObjeto;

                sqlPermissao = getSQLPermissao(codigoOE, iniciaisTO, codigoUsuarioPermissao, idUsuarioLogado, codigoOP, idEntidadeLogada, listaPerfis, iniciaisTipoObjeto);
                ds = cDados.getDataSet(sqlPermissao);
                Session[nomeSession] = ds.Tables[0];
            }

            if (nbAssociacao.Items.Count > 0)
            {
                hfGeral.Set("itemMenu", getIniciaisTipoAssociacao(nbAssociacao.Items[0].Name));
                nbAssociacao.Items[0].Selected = true;
            }
        }
    }

    private void cerrarSessionCheck()
    {
        /* EN UN ST PR ME PP TM OB IN CT */

        if (Session["dtEN"] != null) Session.Remove("dtEN");
        if (Session["dtUN"] != null) Session.Remove("dtUN");
        if (Session["dtST"] != null) Session.Remove("dtST");
        if (Session["dtPR"] != null) Session.Remove("dtPR");
        if (Session["dtME"] != null) Session.Remove("dtME");
        if (Session["dtPP"] != null) Session.Remove("dtPP");
        if (Session["dtTM"] != null) Session.Remove("dtTM");
        if (Session["dtOB"] != null) Session.Remove("dtOB");
        if (Session["dtIN"] != null) Session.Remove("dtIN");
        if (Session["dtCT"] != null) Session.Remove("dtCT");
        if (Session["dtDC"] != null) Session.Remove("dtDC");
        if (Session["dtDS"] != null) Session.Remove("dtDS");
        if (Session["dtPC"] != null) Session.Remove("dtPC");
        if (Session["dtCP"] != null) Session.Remove("dtCP");
        if (Session["dtEQ"] != null) Session.Remove("dtEQ");
    }

    private string getSQLPermissao(string codigoOE, string iniciaisTO, string codigoUsuarioPermissao
                            , string idUsuarioLogado, string codigoObjetoPai, string idEntidadeLogada
                            , string listaPerfis, string iniciaisTipoAssociacao)
    {
        string sqlPermissao = "";

        sqlPermissao = string.Format(@"
            DECLARE @CodigoTipoAssociacao INT,
                    @SiglaTipoAssociacao VARCHAR(2)

            SET @SiglaTipoAssociacao = '{9}' --'EN'

            SELECT  *
                ,	CASE    WHEN [Personalizada] = 1 THEN    'Editado.png'
                            WHEN [Herdada]       = 1 THEN    'Perfil_Herdada.png' 
                            ELSE ''
                    END     AS [imagemIcono]

            FROM    {0}.{1}.f_GetPermissoesUsuario ( {2} , '{3}', {6}, {7}, {4}, @SiglaTipoAssociacao, {5}, {8})
            ", dbName, dbOwner, codigoOE, iniciaisTO
             , codigoUsuarioPermissao, idUsuarioLogado
             , codigoObjetoPai, idEntidadeLogada, listaPerfis
             , iniciaisTipoAssociacao);

        return sqlPermissao;
    }

    #endregion

    #region CALLBACK'S


    private string AssociaUsuariosNoPerfil()
    {
        int codigoPerfil = getChavePrimariaPerfil();

        string listaUsuarios = "-1";

        if (hfGeral.Contains("CodigosUsuariosSelecionados"))
            listaUsuarios = hfGeral.Get("CodigosUsuariosSelecionados").ToString();

        string mensagemErro = "";
        try
        {


            string comandoSQL = string.Format(@"
                DECLARE @RC int
                DECLARE @CodigoEntidadeContexto int
                DECLARE @CodigoUsuarioSistema int
                DECLARE @codigoObjeto bigint
                DECLARE @IniciaisTipoObjeto char(2)
                DECLARE @codigoObjetoPai bigint
                DECLARE @codigoPerfil int
                DECLARE @usuariosSelecionados varchar(max)

                SET @CodigoEntidadeContexto = {0}
                SET @CodigoUsuarioSistema = {1}
                SET @codigoObjeto = {3}
                SET @IniciaisTipoObjeto = '{4}'
                SET @codigoObjetoPai = {5}
                SET @codigoPerfil = {2}
                SET @usuariosSelecionados = '{6}'

                EXECUTE @RC = [dbo].[p_perm_atribuiPerfilInteressadosObjeto] 
                   @CodigoEntidadeContexto
                  ,@CodigoUsuarioSistema
                  ,@codigoObjeto
                  ,@IniciaisTipoObjeto
                  ,@codigoObjetoPai
                  ,@codigoPerfil
                  ,@usuariosSelecionados", idEntidadeLogada, idUsuarioLogado, codigoPerfil, codigoOE, iniciaisTO, codigoOP, listaUsuarios);

            int qtd = 0;
            bool ret = cDados.execSQL(comandoSQL, ref qtd);
        }
        catch (Exception ex)
        {
            mensagemErro = ex.Message;
        }
        return mensagemErro;
    }

    private string persisteIncluirNovoRegistro()
    {
        carregaListaPerfisAssociados("-1", iniciaisTO, codigoOP);
        carregaListaPerfis("-1", iniciaisTO, codigoOP);
        cerrarSessionCheck();
        return "";
    }

    protected void pnCallbackPermissoes_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string codigoUsuarioPermissao = e.Parameter;
        string itemMenu = hfGeral.Get("itemMenu").ToString();
        string iniciaisTipoAssociacao = hfGeral.Get("itemMenuIniciais").ToString();
        string listaPerfis = "NULL";

        string commandoSQL = string.Format(@"
                                SELECT *
                                    ,	CASE    WHEN [Personalizada] = 1 THEN    'Editado.png'
                                                WHEN [Herdada]       = 1 THEN    'Perfil_Herdada.png' 
                                                ELSE ''

                                FROM {0}.{1}.f_GetPermissoesUsuario ( {2} , '{3}', {7}, {8}, {4}, '{5}', {6}, {9} )
                                ", dbName, dbOwner, codigoOE, iniciaisTipoAssociacao
                                 , codigoUsuarioPermissao, itemMenu
                                 , idUsuarioLogado, codigoOP, idEntidadeLogada, listaPerfis);

        DataSet ds = cDados.getDataSet(commandoSQL);

        gvPermissoes.DataSource = ds.Tables[0];
        gvPermissoes.DataBind();

        populaSessiones(codigoUsuarioPermissao);
    }

    protected void ddlPapelNoProjeto_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string parametro = e.Parameter;
    }

    protected void callbackGeral_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (e.Parameter.Equals("CerrarSession"))
            cerrarSessionCheck();
    }

    protected void callbackConceder_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string nomeSession = "dt" + hfGeral.Get("itemMenu").ToString();
        DataTable dt = (DataTable)Session[nomeSession]; //(DataTable)Session["dtPermissoes"];

        string codigoPermissao = e.Parameter.ToString().Split(';')[0];
        string permissaoConceder = e.Parameter.ToString().Split(';')[1];
        string check = e.Parameter.ToString().Split(';')[2];

        if (null != dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["CodigoPermissao"].ToString() == codigoPermissao)
                {
                    if (check.Equals("C"))
                    {
                        dr["Concedido"] = permissaoConceder;
                        dr["Delegavel"] = false;
                        dr["Negado"] = false;
                    }
                    else if (check.Equals("D"))
                    {
                        dr["Concedido"] = true;
                        dr["Delegavel"] = permissaoConceder;
                        dr["Negado"] = false;
                    }
                    else if (check.Equals("N"))
                    {
                        dr["Concedido"] = false;
                        dr["Delegavel"] = false;
                        dr["Negado"] = permissaoConceder;
                    }
                    else if (check.Equals("I"))
                    {
                        dr["Incondicional"] = permissaoConceder;
                    }

                    if (!(bool)dr["Concedido"] && !(bool)dr["Delegavel"] && !(bool)dr["Negado"])
                        dr["Incondicional"] = false;

                    dt.AcceptChanges();
                    break;
                } // if (dr["CodigoTipoProjeto"].ToString() == e.Keys["CodigoTipoProjeto"].ToString())
            } // foreach (DataRow dr in dt)

            Session[nomeSession] = dt;
            gvPermissoes.DataSource = dt;
            gvPermissoes.DataBind();
        }  // if (null != dt)
    }

    protected void pnCallbackDetalhe_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string parametro = e.Parameter.ToString();

        if (parametro.Equals("IncluirNovo"))
        {
            populaComboInteressado();
        }
    }

    #endregion

    #region GRIDVIEW

    #region gvDados

    private void carregaGvDados()
    {
        string iniciaisTA = hfGeral.Get("itemMenuIniciais").ToString();
        DataSet ds = cDados.getInteressadosObjetos(iniciaisTA, int.Parse(codigoOE), int.Parse(codigoOP), int.Parse(idEntidadeLogada));
        bool acessoRestringidoPermissao = cDados.getAcessoRestringidoDaPermissao(int.Parse(codigoOE), int.Parse(idEntidadeLogada), int.Parse(codigoOP), iniciaisTA);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            if (!captionOE.Equals("N"))
            {
                gvDados.SettingsText.Title = string.Format(@"
                <table style='TEXT-ALIGN: left'>
                <tr>
                    <td vAlign='top'><b>{0} : </b></td>
                    <td><i>{1}</i></td>
                </tr>
                </table>
                <table>
                <tr>
                    <td><font color=""#80BFFF""><small>{2}</small></font></dt>
                </tr>
                </table>
                <table>
                <tr>
                    <td>
                    <i>Adotar acesso restritivo?</i><input id=""CheckRestrincao"" {3} onclick=""clicaRestrincao(this.checked)"" type=""checkbox"" />
                    </dt>
                </tr>
                </table>
                ", getDescricaoAssociacaoFromIniciais(iniciaisTO), tituloOE
                 , ((tituloME != null) ? "Mapa : " + tituloME : "")
                 , acessoRestringidoPermissao ? "CHECKED" : "");
            }
            

        }
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        GridViewDataColumn coluna = e.DataColumn;
        if (coluna.FieldName == "Perfis")
        {
            if (e.CellValue.ToString().Length > 70)
            {
                e.Cell.Text = e.CellValue.ToString().Substring(0, 70) + "...";
            }
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (gvDados.GetRowValues(e.VisibleIndex, "Perfis") != null)
        {
            string perfisAtual = gvDados.GetRowValues(e.VisibleIndex, "Perfis").ToString();
            string podeSerEditado = gvDados.GetRowValues(e.VisibleIndex, "IndicaEdicaoPermitida").ToString();

            if (e.ButtonID == "btnExcluirCustom")
            {
                if (perfisAtual.Equals("*") || podeSerEditado.Equals("N") || podeEditar == false)
                {
                    e.Enabled = false;
                    e.Text = Resources.traducao.InteressadosObjeto_excluir_n_o_dispon_vel;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
            }
            if (e.ButtonID == "btnEditarCustom")
            {
                if (podeSerEditado.Equals("N") || podeEditar == false)
                {
                    e.Enabled = false;
                    e.Text = Resources.traducao.InteressadosObjeto_edi__o_n_o_dispon_vel;
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
            }
        }
    }


    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string parametro = e.Parameters;
        string iniciaisTA = hfGeral.Get("itemMenuIniciais").ToString();

        cDados.atualizaAcessoRestringidoDaPermissao(int.Parse(codigoOE), int.Parse(idEntidadeLogada), int.Parse(codigoOP), int.Parse(idUsuarioLogado), iniciaisTA, parametro);
        gvDados.DataBind();
    }

    #endregion

    #region gvPERMISSOES

    protected void gvPermissoes_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        bool somenteLeitura = true;
        if (hfGeral.Contains("TipoOperacao") && (hfGeral.Get("TipoOperacao").ToString() == "Incluir" || hfGeral.Get("TipoOperacao").ToString() == "Editar"))
            somenteLeitura = false;

        ddlInteressado.Enabled = !somenteLeitura;

        if (e.Parameters.ToString().Equals("ATL"))
        {
            string siglaAssociacao = hfGeral.Get("itemMenu").ToString();

            //Prencho o Title da grid gvPermissoes.
            getTituloGridPermissoes(siglaAssociacao);
        }
        else if (e.Parameters.ToString().Equals("VOLTAR"))
        {
            cerrarSessionCheck();
        }
        else
        {
            string[] parametro = e.Parameters.ToString().Split(';');
            populaChecks(parametro[0], parametro[1]);
        }
    }

    private void getTituloGridPermissoes(string siglaAssociacao)
    {
        /* EN UN ST PR ME PP TM OB IN CT EQ*/
        if (siglaAssociacao.Equals("EN")) gvPermissoes.SettingsText.Title = Resources.traducao.InteressadosObjeto_permiss_es___entidades;
        else if (siglaAssociacao.Equals("UN")) gvPermissoes.SettingsText.Title = Resources.traducao.InteressadosObjeto_permiss_es___unidades;
        else if (siglaAssociacao.Equals("ST")) gvPermissoes.SettingsText.Title = Resources.traducao.InteressadosObjeto_permiss_es___estrat_gias;
        else if (siglaAssociacao.Equals("PR")) gvPermissoes.SettingsText.Title = Resources.traducao.InteressadosObjeto_permiss_es___projetos;
        else if (siglaAssociacao.Equals("ME")) gvPermissoes.SettingsText.Title = Resources.traducao.InteressadosObjeto_permiss_es___mapas;
        else if (siglaAssociacao.Equals("PP")) gvPermissoes.SettingsText.Title = Resources.traducao.InteressadosObjeto_permiss_es___perspectivas;
        else if (siglaAssociacao.Equals("TM")) gvPermissoes.SettingsText.Title = Resources.traducao.InteressadosObjeto_permiss_es___temas;
        else if (siglaAssociacao.Equals("OB")) gvPermissoes.SettingsText.Title = Resources.traducao.InteressadosObjeto_permiss_es___objetivos_estrat_gicos;
        else if (siglaAssociacao.Equals("IN")) gvPermissoes.SettingsText.Title = Resources.traducao.InteressadosObjeto_permiss_es___indicadores;
        else if (siglaAssociacao.Equals("CT")) gvPermissoes.SettingsText.Title = Resources.traducao.InteressadosObjeto_permiss_es___contrato;
        else if (siglaAssociacao.Equals("DC")) gvPermissoes.SettingsText.Title = Resources.traducao.InteressadosObjeto_permiss_es___demandas_complexas;
        else if (siglaAssociacao.Equals("DS")) gvPermissoes.SettingsText.Title = Resources.traducao.InteressadosObjeto_permiss_es___demandas_simples;
        else if (siglaAssociacao.Equals("PC")) gvPermissoes.SettingsText.Title = Resources.traducao.InteressadosObjeto_permiss_es___processos;
        else if (siglaAssociacao.Equals("CP")) gvPermissoes.SettingsText.Title = Resources.traducao.InteressadosObjeto_permiss_es___ + definicaoCarteiraPlural;
        else if (siglaAssociacao.Equals("EQ")) gvPermissoes.SettingsText.Title = Resources.traducao.InteressadosObjeto_permiss_es___equipe_de_recursos;
    }

    protected void gvPermissoes_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        Color color = new Color();

        if (e.RowType == GridViewRowType.Data)
        {
            bool readOnly = (bool)e.GetValue("ReadOnly");

            if (readOnly)
            {
                int red = Int32.Parse("DD", System.Globalization.NumberStyles.HexNumber);
                int green = Int32.Parse("FF", System.Globalization.NumberStyles.HexNumber);
                int blue = Int32.Parse("CC", System.Globalization.NumberStyles.HexNumber);

                color = Color.FromArgb(red, green, blue);
                e.Row.BackColor = color;
            }
        }
    }

    public string getCheckBox(string nomeCheck, string coluna, string inicial)
    {
        string retorno = "";
        bool readOnly = (bool)Eval("ReadOnly");
        string desabilitado = hfGeral.Get("TipoOperacao").ToString().Equals("Consultar") || readOnly ? "disabled='disabled'" : "";

        if (nomeCheck == "CheckIncondicional" && !hfGeral.Get("TipoOperacao").ToString().Equals("Consultar"))
        {
            bool indicaCheckConcedido = (bool)Eval("Concedido");
            bool indicaCheckDelegavel = (bool)Eval("Delegavel");
            bool indicaCheckNegado = (bool)Eval("Negado");
            bool incondicionalBloqueado = (bool)Eval("IncondicionalBloqueado");


            if ((indicaCheckConcedido || indicaCheckDelegavel || indicaCheckNegado) && (incondicionalBloqueado == false))
                desabilitado = "";
            else
                desabilitado = "disabled='disabled'";

        }

        retorno = "<input " + desabilitado + " id='" + nomeCheck + "' " + ((Eval(coluna).ToString() == "1" || Eval(coluna).ToString() == "True") ? "checked='CHECKED'" : "") + " onclick='clicaConceder(" + Eval("CodigoPermissao") + ", this.checked, \"" + inicial + "\")' type='checkbox' />";

        return retorno;
    }

    #endregion

    #endregion

    #region CHECK BOX

    private void populaChecks(string siglaAssociacao, string codigoUsuarioInteressado)
    {
        /* EN UN ST PR ME PP TM OB IN CT */
        if (!codigoUsuarioInteressado.Equals("-1"))
        {
            string sqlPermissao;
            string codigoTipoAssociacao = "";
            string listaPerfis = "";
            string herdaPermissoes = "S";

            //Prencho o Title da grid gvPermissoes.
            getTituloGridPermissoes(siglaAssociacao);

            //Vejo si a sessión existe. Caso que não, cargo o dados do Banco.
            string nomeSession = "dt" + siglaAssociacao;
            if (Session[nomeSession] == null)
            {
                if (hfGeral.Contains("CodigosPerfisSelecionados") && !hfGeral.Get("CodigosPerfisSelecionados").ToString().Equals("-1"))
                    listaPerfis = hfGeral.Get("CodigosPerfisSelecionados").ToString();
                if (hfGeral.Contains("HerdaPermissoes"))
                    herdaPermissoes = hfGeral.Get("HerdaPermissoes").ToString();
                if (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString().Equals("Consultar"))
                    herdaPermissoes = "NULL";


                codigoTipoAssociacao = string.Format("SET @SiglaTipoAssociacao = '" + siglaAssociacao + "' ", dbName, dbOwner);

                //SELECT * FROM desenv_Portfolio.dbo.f_GetPermissoesUsuario ( 161 , 'UN', 0, 1, 39, @SiglaTipoAssociacao, 3, '68;76;')
                sqlPermissao = string.Format(@"
                                DECLARE @CodigoTipoAssociacao INT,
                                        @SiglaTipoAssociacao VARCHAR(2)

                                {6}
                                
                                SELECT  *
                                    ,	CASE    WHEN [Herdada]       = 1 THEN    'Perfil_Herdada.png' 
                                                WHEN [Personalizada] = 1 THEN    'Editado.png'
                                                ELSE ''
                                        END     AS [imagemIcono]

                                FROM {0}.{1}.f_GetPermissoesUsuario ( {2}, '{3}', {7}, {8}, {4}, @SiglaTipoAssociacao, {5}, {9}, {10} )

                                ", dbName, dbOwner, codigoOE
                                 , iniciaisTO, codigoUsuarioInteressado, idUsuarioLogado
                                 , codigoTipoAssociacao, codigoOP, idEntidadeLogada
                                 , listaPerfis.Equals("") ? "NULL" : "'" + listaPerfis + "'"
                                 , string.IsNullOrEmpty(herdaPermissoes.Trim()) || herdaPermissoes.Equals("NULL") ? "NULL" : String.Format("'{0}'", herdaPermissoes));

                DataSet ds = cDados.getDataSet(sqlPermissao);

                gvPermissoes.DataSource = ds.Tables[0];
                gvPermissoes.DataBind();

                Session[nomeSession] = ds.Tables[0];
            }
            else
            {
                //Caso que a sessión exista, prencho a grid gvPermissoes con ela.
                DataTable dt = (DataTable)Session[nomeSession];
                gvPermissoes.DataSource = dt;
                gvPermissoes.DataBind();
            }
        }
    }

    #endregion

    #region COMBOBOX

    /// <summary>
    /// Populo o combo de usuarios ao dar permissao no objeto.
    /// O listagem será para so aparecer os usuarios que não tenha ja vinculo com o objeto.
    /// </summary>
    private void populaComboInteressado()
    {
        string where = string.Format(@"
                        AND us.DataExclusao IS NULL
                        AND us.CodigoUsuario NOT IN (
        					SELECT iobj.[CodigoInteressado]
                            FROM    {0}.{1}.f_GetInteressadosObjeto( {2}, NULL, '{3}', {4}, {5}, 'T')   AS [iobj]
                        )
                        ", dbName, dbOwner, codigoOE, iniciaisTO, codigoOP, idEntidadeLogada);

        DataSet ds = getUsuarioDaEntidadeAtiva(int.Parse(idEntidadeLogada), where,"");
        if ((cDados.DataSetOk(ds)))// && (cDados.DataTableOk(ds.Tables[0])))
        {
            ddlInteressado.DataSource = ds.Tables[0];
            ddlInteressado.TextField = "NomeUsuario";
            ddlInteressado.ValueField = "CodigoUsuario";
            ddlInteressado.DataBind();
            if (!IsPostBack)
                ddlInteressado.SelectedIndex = 0;
        }

    }

    public DataSet getUsuarioDaEntidadeAtiva(int codigoEntidade, string where, string filtro)
    {
        string whereNomeUsuario = filtro == "" ? "" : "AND NomeUsuario LIKE '%" + filtro + "%'";

        string comandoSQL =
          string.Format(@"
                SELECT  ROW_NUMBER()over(order by IndicaUsuarioAtivoUnidadeNegocio DESC, nomeUsuario) as rn , 
                                us.CodigoUsuario, 
                                us.NomeUsuario, 
                                us.EMail,
                                ISNULL(uun.IndicaUsuarioAtivoUnidadeNegocio, 'S') AS IndicaUsuarioAtivoUnidadeNegocio,
                                CASE WHEN ISNULL(uun.IndicaUsuarioAtivoUnidadeNegocio, 'S') = 'S' THEN '' ELSE '({4})' END AS StatusUsuario
                  FROM Usuario us
                 INNER JOIN UsuarioUnidadeNegocio uun on us.CodigoUsuario = uun.CodigoUsuario
                 WHERE uun.CodigoUnidadeNegocio = {0}
                   AND us.DataExclusao IS NULL
                   {3}
                   {2}
                  ", codigoEntidade, filtro, where, whereNomeUsuario, Resources.traducao.inativo);

        return cDados.getDataSet(comandoSQL);
    }

    #endregion

    #region BANCO DE DADOS

    private void getChavePrimaria() // retorna a primary key da tabela.
    {
        if (gvDados.FocusedRowIndex >= 0)
        {
            codigoObjetoAssociadoPK = "";
            codigoTipoAssociacaoPK = "";
            codigoUsuarioPK = "";

            Object[] obj = (Object[])gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoObjetoAssociado", "CodigoTipoAssociacao", "CodigoUsuario");

            codigoObjetoAssociadoPK = obj[0].ToString(); //gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoObjetoAssociado").ToString();
            codigoTipoAssociacaoPK = obj[1].ToString(); //gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoTipoAssociacao").ToString();
            codigoUsuarioPK = obj[2].ToString(); //gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoUsuario").ToString();
        }
    }
    private int getChavePrimariaPerfil() // retorna a primary key da tabela.
    {
        int codigoPerfil = -1;
        if (gvPerfisAtribuiveis.FocusedRowIndex >= 0)
        {
            codigoPerfil =  (int)gvPerfisAtribuiveis.GetRowValues(gvPerfisAtribuiveis.FocusedRowIndex, "CodigoPerfil");
        }
        return codigoPerfil;
    }

    /// <summary>
    /// Inclui o usuário como interessado no objeto.
    /// </summary>
    /// <returns></returns>
    private string persisteInclusaoInteressadoObjeto()
    {
        string listaPerfis = "-1";
        string mensagemErro = "";
        string iniciaisTipoAssociacao = string.Format("'{0}'", hfGeral.Get("itemMenuIniciais").ToString());
        int codigoObjetoAssociacao = int.Parse(codigoOE);
        int codigoUsuarioInteressado = int.Parse(hfGeral.Get("CodigoUsuarioPermissao").ToString());
        bool herdaPermissaoObjetoSuperior = checkHerdarPermissoes.Checked;

        if (hfGeral.Contains("CodigosPerfisSelecionados"))
            listaPerfis = hfGeral.Get("CodigosPerfisSelecionados").ToString();

        try
        {
            cDados.incluirInteressadoObjeto(codigoObjetoAssociacao, "NULL", iniciaisTipoAssociacao, codigoUsuarioInteressado
                                            , herdaPermissaoObjetoSuperior, int.Parse(idUsuarioLogado), int.Parse(codigoOP)
                                            , listaPerfis, int.Parse(idEntidadeLogada), ref mensagemErro);
            return mensagemErro;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string persisteEdicaoInteressadoObjeto()
    {
        getChavePrimaria();

        string listaPerfis = "-1";
        int codigoObjetoAssociacao = int.Parse(codigoObjetoAssociadoPK);
        int codigoUsuario = int.Parse(codigoUsuarioPK);
        bool herdaPermissaoObjetoSuperior = checkHerdarPermissoes.Checked;

        if (hfGeral.Contains("CodigosPerfisSelecionados"))
            listaPerfis = hfGeral.Get("CodigosPerfisSelecionados").ToString();

        string mensagemErro = "";
        try
        {
            cDados.incluirInteressadoObjeto(codigoObjetoAssociacao, codigoTipoAssociacaoPK, "NULL", codigoUsuario
                                            , herdaPermissaoObjetoSuperior, int.Parse(idUsuarioLogado), int.Parse(codigoOP)
                                            , listaPerfis, int.Parse(idEntidadeLogada), ref mensagemErro);
            return mensagemErro;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string persisteInclusaoPermissao()
    {
        DataTable dt;
        string sqlPermissoes = "";
        string listaPerfis = "-1";
        string mensagemErro = "";
        string iniciaisTipoAssociacao = string.Format("'{0}'", hfGeral.Get("itemMenuIniciais").ToString());
        int codigoObjetoAssociacao = int.Parse(codigoOE);
        int codigoUsuarioInteressado = int.Parse(hfGeral.Get("CodigoUsuarioPermissao").ToString()); //int.Parse(ddlInteressado.Value.ToString());
        bool herdaPermissaoObjetoSuperior = checkHerdarPermissoes.Checked;

        if (hfGeral.Contains("CodigosPerfisSelecionados"))
            listaPerfis = hfGeral.Get("CodigosPerfisSelecionados").ToString();

        try
        {
            cDados.incluirInteressadoObjeto(codigoObjetoAssociacao, "NULL", iniciaisTipoAssociacao, codigoUsuarioInteressado
                                        , herdaPermissaoObjetoSuperior, int.Parse(idUsuarioLogado), int.Parse(codigoOP)
                                        , listaPerfis, int.Parse(idEntidadeLogada), ref mensagemErro);

            /* EN UN ST PR ME PP TM OB IN CT */
            if (Session["dtEN"] != null)
            {
                dt = (DataTable)Session["dtEN"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtUN"] != null)
            {
                dt = (DataTable)Session["dtUN"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtST"] != null)
            {
                dt = (DataTable)Session["dtST"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtPR"] != null)
            {
                dt = (DataTable)Session["dtPR"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtME"] != null)
            {
                dt = (DataTable)Session["dtME"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtPP"] != null)
            {
                dt = (DataTable)Session["dtPP"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtTM"] != null)
            {
                dt = (DataTable)Session["dtTM"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtOB"] != null)
            {
                dt = (DataTable)Session["dtOB"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtIN"] != null)
            {
                dt = (DataTable)Session["dtIN"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtCT"] != null)
            {
                dt = (DataTable)Session["dtCT"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtDC"] != null)
            {
                dt = (DataTable)Session["dtDC"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtDS"] != null)
            {
                dt = (DataTable)Session["dtDS"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtPC"] != null)
            {
                dt = (DataTable)Session["dtPC"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtCP"] != null)
            {
                dt = (DataTable)Session["dtCP"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtEQ"] != null)
            {
                dt = (DataTable)Session["dtEQ"];
                sqlPermissoes += getSQLpermissoes(dt);
            }

            cDados.registraPermissoesUsuario(sqlPermissoes, iniciaisTO, codigoObjetoAssociacao
                                                      , codigoUsuarioInteressado, int.Parse(idUsuarioLogado), int.Parse(codigoOP)
                                                      , int.Parse(idEntidadeLogada), ref mensagemErro);

            return mensagemErro;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string persisteEdicaoPermissao()
    {
        getChavePrimaria();

        DataTable dt;
        string mensagemErro = "";
        string sqlPermissoes = "";
        string listaPerfis = "-1";
        int codigoObjetoAssociacao = int.Parse(codigoObjetoAssociadoPK);
        int codigoUsuarioInteressado = int.Parse(codigoUsuarioPK);
        bool herdaPermissaoObjetoSuperior = checkHerdarPermissoes.Checked;

        if (hfGeral.Contains("CodigosPerfisSelecionados"))
            listaPerfis = hfGeral.Get("CodigosPerfisSelecionados").ToString();

        try
        {
            cDados.incluirInteressadoObjeto(codigoObjetoAssociacao, codigoTipoAssociacaoPK, "NULL", codigoUsuarioInteressado
                                        , herdaPermissaoObjetoSuperior, int.Parse(idUsuarioLogado), int.Parse(codigoOP) //"NULL", 0
                                        , listaPerfis, int.Parse(idEntidadeLogada), ref mensagemErro);

            /* EN UN ST PR ME PP TM OB IN CT */
            if (Session["dtEN"] != null)
            {
                dt = (DataTable)Session["dtEN"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtUN"] != null)
            {
                dt = (DataTable)Session["dtUN"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtST"] != null)
            {
                dt = (DataTable)Session["dtST"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtPR"] != null)
            {
                dt = (DataTable)Session["dtPR"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtME"] != null)
            {
                dt = (DataTable)Session["dtME"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtPP"] != null)
            {
                dt = (DataTable)Session["dtPP"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtTM"] != null)
            {
                dt = (DataTable)Session["dtTM"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtOB"] != null)
            {
                dt = (DataTable)Session["dtOB"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtIN"] != null)
            {
                dt = (DataTable)Session["dtIN"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtCT"] != null)
            {
                dt = (DataTable)Session["dtCT"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtDC"] != null)
            {
                dt = (DataTable)Session["dtDC"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtDS"] != null)
            {
                dt = (DataTable)Session["dtDS"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtPC"] != null)
            {
                dt = (DataTable)Session["dtPC"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtCP"] != null)
            {
                dt = (DataTable)Session["dtCP"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtEQ"] != null)
            {
                dt = (DataTable)Session["dtEQ"];
                sqlPermissoes += getSQLpermissoes(dt);
            }

            cDados.registraPermissoesUsuario(sqlPermissoes, iniciaisTO, codigoObjetoAssociacao
                                                      , codigoUsuarioInteressado, int.Parse(idUsuarioLogado), int.Parse(codigoOP)
                                                      , int.Parse(idEntidadeLogada), ref mensagemErro);
            return mensagemErro;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string persisteIncluirUsuariosNosPerfis()
    {
        getChavePrimaria();

        string mensagemErro = "";
        string sqlPermissoes = "";
        string listaPerfis = "-1";
        int codigoObjetoAssociacao = int.Parse(codigoObjetoAssociadoPK);
        int codigoUsuarioInteressado = int.Parse(codigoUsuarioPK);
        bool herdaPermissaoObjetoSuperior = checkHerdarPermissoes.Checked;

        if (hfGeral.Contains("CodigosPerfisSelecionados"))
            listaPerfis = hfGeral.Get("CodigosPerfisSelecionados").ToString();

        try
        {
            cDados.incluirInteressadoObjeto(codigoObjetoAssociacao, codigoTipoAssociacaoPK, "NULL", codigoUsuarioInteressado
                                        , herdaPermissaoObjetoSuperior, int.Parse(idUsuarioLogado), int.Parse(codigoOP) //"NULL", 0
                                        , listaPerfis, int.Parse(idEntidadeLogada), ref mensagemErro);


            cDados.registraPermissoesUsuario(sqlPermissoes, iniciaisTO, codigoObjetoAssociacao
                                                      , codigoUsuarioInteressado, int.Parse(idUsuarioLogado), int.Parse(codigoOP)
                                                      , int.Parse(idEntidadeLogada), ref mensagemErro);
            return mensagemErro;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    private string persisteExclusaoRegistro()
    {
        string msgErro = "";
        bool herdaPermissaoObjetoSuperior = checkHerdarPermissoes.Checked;

        getChavePrimaria();

        cDados.excluiPermissaoObjetoAoUsuario(int.Parse(codigoTipoAssociacaoPK), int.Parse(codigoObjetoAssociadoPK)
                                            , int.Parse(codigoUsuarioPK), int.Parse(idUsuarioLogado), hfGeral.Get("itemMenuIniciais").ToString()
                                            , int.Parse(codigoOP), int.Parse(idEntidadeLogada), herdaPermissaoObjetoSuperior
                                            , ref msgErro);

        return "";
    }

    private string getSQLpermissoes(DataTable dt)
    {
        int valorOriginal = 0;
        string listaPermissoes = "";

        foreach (DataRow dr in dt.Rows)
        {
            int valorPermissao = 0;

            valorPermissao += dr["Concedido"].ToString().Equals("True") ? 1 : 0;
            valorPermissao += dr["Negado"].ToString().Equals("True") ? 2 : 0;
            valorPermissao += dr["Delegavel"].ToString().Equals("True") ? 4 : 0;
            valorPermissao += dr["Incondicional"].ToString().Equals("True") ? 8 : 0;

            valorOriginal = int.Parse(dr["FlagsOrigem"].ToString());
 
            // OBSERVAÇÃO Comparando apenas o primeiros 4 bit's.
            if ((valorPermissao & 15) != (valorOriginal & 15))
            {
                listaPermissoes += dr["CodigoPermissao"].ToString() + "|" + valorPermissao + ";";
            }
        }

        return listaPermissoes; // retorno;
    }

    #endregion

    #region LISTBOX

    protected void lbListaPerfisSelecionados_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string codigoInteressado = "";
        int testPerfis;
        if (e.Parameter.Length > 0)
        {
            codigoInteressado = e.Parameter.ToString();
            if (int.TryParse(codigoInteressado, out testPerfis))
            {
                carregaListaPerfisAssociados(codigoInteressado, iniciaisTO, codigoOP);
            }
        }
    }

    protected void lbListaPerfisDisponivel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string codigoUsuario = e.Parameter.ToString();
        carregaListaPerfis(codigoUsuario, iniciaisTO, codigoOP);
    }

    private void carregaListaPerfis(string codigoUsuario, string tipoAssociacao, string codigoObjetoPai)
    {
        DataSet ds = cDados.getListaPerfisDisponiveisUsuario(int.Parse(codigoOE), tipoAssociacao, int.Parse(codigoObjetoPai)
                                                            , int.Parse(idEntidadeLogada), int.Parse(codigoUsuario));//getListaProjetoDisponivel(codigoGerente, codigoUnidade, codigoPerfis);

        lbListaPerfisDisponivel.DataSource = ds.Tables[0];
        lbListaPerfisDisponivel.DataBind();

        if (lbListaPerfisDisponivel.Items.Count > 0)
        {
            lbListaPerfisDisponivel.SelectedIndex = -1;
            btnADDTodos.ClientEnabled = true;
        }
    }

    private void carregaListaPerfisAssociados(string codigoUsuario, string tipoAssociacao, string codigoObjetoPai)
    {
        DataSet ds = cDados.getListaPerfisAtribuidosUsuario(int.Parse(codigoOE), tipoAssociacao, int.Parse(codigoObjetoPai)
                                                            , int.Parse(idEntidadeLogada), int.Parse(codigoUsuario));//getListaProjetoSelecionados(codigoGerente, codigoUnidade, codigoPerfis);
        lbListaPerfisSelecionados.DataSource = ds.Tables[0];
        lbListaPerfisSelecionados.DataBind();

        if (lbListaPerfisSelecionados.Items.Count > 0)
        {
            lbListaPerfisSelecionados.SelectedIndex = -1;
            btnRMVTodos.ClientEnabled = true;
        }
    }

    #endregion

    protected void gvDados_DataBound(object sender, EventArgs e)
    {
        if (IsCallback || IsPostBack)
            carregaGvDados();
    }

    protected void ddlGerenteProjeto_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        if (e.Value != null)
        {
            long value = 0;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsResponsavel.SelectCommand = cDados.getSQLComboUsuariosPorID(int.Parse(idEntidadeLogada));

            dsResponsavel.SelectParameters.Clear();
            dsResponsavel.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsResponsavel;
            comboBox.DataBind();
        }

    }

    protected void ddlGerenteProjeto_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string filtro = "";

        filtro = e.Filter.ToString();

        string where = string.Format(@"
                AND us.DataExclusao IS NULL
                AND us.CodigoUsuario NOT IN (
					SELECT iobj.[CodigoInteressado]
                    FROM    {0}.{1}.f_GetInteressadosObjeto( {2}, NULL, '{3}', {4}, {5}, 'T')   AS [iobj]
                )
                ", dbName, dbOwner, codigoOE, iniciaisTO, codigoOP, idEntidadeLogada);

        string comandoSQL = cDados.getSQLComboUsuarios(int.Parse(idEntidadeLogada), filtro, where);

        cDados.populaComboVirtual(dsResponsavel, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "LstIntObj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "IncluirNovoInteressado();", true, true, false, "LstIntObj", "Permissão", this);
    }

    #endregion

    protected void gvPermissoes_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        if (e.CallbackName.Equals("APPLYCOLUMNFILTER"))
        {
            string siglaAssociacao = hfGeral.Get("itemMenu").ToString();

            //Prencho o Title da grid gvPermissoes.
            getTituloGridPermissoes(siglaAssociacao);
        }
    }
    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Group)
        {
            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }
    }

    protected void lbListaUsuariosDisponivel_Callback(object sender, CallbackEventArgsBase e)
    {
        int codigoPerfil = int.Parse(e.Parameter);
        ((ASPxListBox)sender).JSProperties["cpCodigoPerfil"] = codigoPerfil;

        string comandoSQL = string.Format(@"
                DECLARE @RC int
                DECLARE @CodigoEntidadeContexto int
                DECLARE @CodigoUsuarioSistema int
                DECLARE @codigoObjeto bigint
                DECLARE @IniciaisTipoObjeto char(2)
                DECLARE @codigoObjetoPai bigint
                DECLARE @codigoPerfil int

                SET @CodigoEntidadeContexto = {0}
                SET @CodigoUsuarioSistema = {1}
                SET @codigoObjeto = {3}
                SET @IniciaisTipoObjeto = '{4}'
                SET @codigoObjetoPai = {5}
                SET @codigoPerfil = {2}

                EXECUTE @RC = [dbo].[p_brk_perm_obtemUsuariosDisponiveis] 
                   @CodigoEntidadeContexto
                  ,@CodigoUsuarioSistema
                  ,@codigoObjeto
                  ,@IniciaisTipoObjeto
                  ,@codigoObjetoPai
                  ,@codigoPerfil", idEntidadeLogada, idUsuarioLogado, codigoPerfil, codigoOE, iniciaisTO, codigoOP);

        DataSet ds = cDados.getDataSet(comandoSQL);
        ((ASPxListBox)sender).DataSource = ds;
        ((ASPxListBox)sender).DataBind();

    }

    protected void lbListaUsuariosSelecionados_Callback(object sender, CallbackEventArgsBase e)
    {
        int codigoPerfil = int.Parse(e.Parameter);

        string comandoSQL = string.Format(@"
                DECLARE @RC int
                DECLARE @CodigoEntidadeContexto int
                DECLARE @CodigoUsuarioSistema int
                DECLARE @codigoObjeto bigint
                DECLARE @IniciaisTipoObjeto char(2)
                DECLARE @codigoObjetoPai bigint
                DECLARE @codigoPerfil int

                SET @CodigoEntidadeContexto = {0}
                SET @CodigoUsuarioSistema = {1}
                SET @codigoObjeto = {3}
                SET @IniciaisTipoObjeto = '{4}'
                SET @codigoObjetoPai = {5}
                SET @codigoPerfil = {2}

                EXECUTE @RC = [dbo].[p_brk_perm_obtemUsuariosPerfilObjeto] 
                   @CodigoEntidadeContexto
                  ,@CodigoUsuarioSistema
                  ,@codigoObjeto
                  ,@IniciaisTipoObjeto
                  ,@codigoObjetoPai
                  ,@codigoPerfil", idEntidadeLogada, idUsuarioLogado, codigoPerfil, codigoOE, iniciaisTO, codigoOP);

        DataSet ds = cDados.getDataSet(comandoSQL);
        ((ASPxListBox)sender).DataSource = ds;
        ((ASPxListBox)sender).DataBind();
    }

    protected void gvPerfisAtribuiveis_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGVPerfisAtribuiveis();
    }

    protected void pnCallback_Callback(object sender, CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";

        if (e.Parameter == "Incluir") mensagemErro_Persistencia = persisteInclusaoInteressadoObjeto();
        else if (e.Parameter == "Editar") mensagemErro_Persistencia = persisteEdicaoInteressadoObjeto();
        else if (e.Parameter == "Excluir") mensagemErro_Persistencia = persisteExclusaoRegistro();
        else if (e.Parameter == "IncluirPermissao") mensagemErro_Persistencia = persisteInclusaoPermissao();
        else if (e.Parameter == "EditarPermissao") mensagemErro_Persistencia = persisteEdicaoPermissao();
        else if (e.Parameter == "AssociarUsuariosNoPerfil")
            mensagemErro_Persistencia = AssociaUsuariosNoPerfil();

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            carregaGvDados();
            gvDados.DataBind();
            carregaGVPerfisAtribuiveis();
            populaComboInteressado();

            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.            

        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

       
    }

    protected void callbackAtualizaComboInteressado_Callback(object sender, CallbackEventArgsBase e)
    {
        populaComboInteressado();
    }

    protected void menu0_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "LstIntObj", "Permissão", this);
    }

    protected void menu0_ItemClick(object source, MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter2, "LstIntObj");
    }

    protected void gvDados_PageIndexChanging(object sender, EventArgs e)
    {
        carregaGvDados();
        gvDados.DataBind();
        carregaGVPerfisAtribuiveis();
    }
}
