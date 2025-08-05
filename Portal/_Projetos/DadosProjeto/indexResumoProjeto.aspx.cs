/*
 OBSERVAÇÕES
 
 06/01/2011 - MUDANÇA by ALEJANDRO.
 Control do menú:   Tempo Escopo / Editar EAP
                    Control de acceso, segundo si tem o não permiso para editar o cronograma.
                    Alteração no método-> private void carregaMenuTempoEscopo(DataSet ds){...}
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

public partial class _Projetos_DadosProjeto_indexResumoProjeto : System.Web.UI.Page
{
    dados cDados;
    private string dbName;
    private string dbOwner;
    private string nomeProjeto = "";
    private int codigoEntidadeLogada;
    private int codigoUsuarioResponsavel;
    public string telaInicial = "";
    public string alturaTabela;
    public int idProjeto = 0;
    string nomeTelaInicial = "";
    string nomeTipoProjeto = "";
    bool podeIncluir = true, podeEditar = true, podeExcluir = true;
    bool mostrarFiltroFinanceiro = false;
    private int nivel;

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
        dbOwner = cDados.getDbOwner();
        dbName = cDados.getDbName();
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        imgAjudaGlossarioTipoProjeto.JSProperties["cp_CodigoGlossario"] = "-1";           
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.TH(this.TS("indexResumoProjeto"));
        imgAtualizar.Style.Add("cursor", "pointer");
        imgRedirecionar.Style.Add("cursor", "pointer");
        hfGeral.Set("hfCodigoEntidade", codigoEntidadeLogada);
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/indexResumoProjeto.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"">var textoItem;</script>"));
        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")

        {
            idProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
            hfGeral.Set("hfCodigoProjeto", idProjeto);
            Session["CodigoProjeto"] = idProjeto;
        }

            //Caso o projeto esteje na interação de quarto nível altera o Limpar BreadCrumps
        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["Tipo"] == null && Request.QueryString["TT"] == null && Request.QueryString["NomeProjeto"] == null)
            {
                nivel = 2;
            }
            else
            {
                nivel = 3;
            }

        DataSet dsParam = cDados.getParametrosSistema(codigoEntidadeLogada, "mostrarFiltroFinanceiroProjetos");
                
        if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
        {            
            mostrarFiltroFinanceiro = dsParam.Tables[0].Rows[0]["mostrarFiltroFinanceiroProjetos"].ToString() == "S";
        }

        if (mostrarFiltroFinanceiro)
        {
            ddlFinanceiro.ClientVisible = true;
            lblFinanceiro.ClientVisible = true;
            btnSelecionar.ClientVisible = true;
        }
        else
        {
            ddlFinanceiro.ClientVisible = false;
            lblFinanceiro.ClientVisible = false;
            btnSelecionar.ClientVisible = false;
        }

        cDados.verificaPermissaoProjetoInativo(idProjeto, ref podeIncluir, ref podeEditar, ref podeExcluir);

        if (!IsPostBack)
        {
            string msg = "";
            cDados.sincronizaStatusProjeto(idProjeto, ref msg);
        }

        nomeTipoProjeto = cDados.getNomeTipoProjeto(idProjeto);


        nomeProjeto = cDados.getNomeProjeto(idProjeto.ToString(), "");
        hfGeral.Set("hfNomeProjeto", nomeProjeto);


        alturaTabela = defineAlturaTela() + "px";

        //ASPxPanel1.Height = new Unit(alturaTabela);
        //callbackMenu.Height = new Unit(alturaTabela);
        //divOpcoes.Style.Add("height", alturaTabela);
        //frmDadosProjeto.Style.Add("height", alturaTabela);

        if (!IsPostBack)
        {
            carregaMenuLateral();
        }

        definePermissoesIncluiMensagem();

        string labelTelaResumoProjeto = "Status do " + nomeTipoProjeto;

        if (!IsPostBack)
        {
            defineTelaInicial();

            if (Request.QueryString["NivelNavegacao"] != null && Request.QueryString["NivelNavegacao"].ToString() != "")
                nivel = int.Parse(Request.QueryString["NivelNavegacao"].ToString());

            Master.verificaAcaoFavoritos(true, nomeProjeto, "ST_PRJ", "PROJ", idProjeto, "Adicionar Projeto aos Favoritos");
        }
        /*Ao realizar merge desta branch com a MAGIL, considerar a lógica abaixo que faz com que o usuário retorne para o projeto ágil depois de acessar o sprint.*/
        string tipoProjeto = "";
        string comandoSQL = string.Format(
                        @"SELECT IndicaTipoProjeto
                          FROM {0}.{1}.TipoProjeto
                         WHERE CodigoTipoProjeto = (SELECT CodigoTipoProjeto 
                                                      FROM Projeto 
                                                     WHERE CodigoProjeto = {2}) ", cDados.getDbName(), cDados.getDbOwner(), idProjeto);
        DataSet dsTipoProjeto = cDados.getDataSet(comandoSQL);
        if(cDados.DataSetOk(dsTipoProjeto) && cDados.DataTableOk(dsTipoProjeto.Tables[0]))
        {
            tipoProjeto = dsTipoProjeto.Tables[0].Rows[0]["IndicaTipoProjeto"].ToString();
        }
        if(tipoProjeto == "SPT")
        {
            imgRedirecionar.ClientVisible = true;
        }

        sdsConsultas.ConnectionString = cDados.classeDados.getStringConexao();

        this.Title = cDados.getNomeSistema();
        cDados.aplicaEstiloVisual(sp_Tela);
        cDados.aplicaEstiloVisual(nvbMenuProjeto);
        cDados.aplicaEstiloVisual(this);
        cDados.excluiNiveisAbaixo(nivel);
        cDados.insereNivel(nivel, this);
        Master.geraRastroSite();
    }

    private void definePermissoesIncluiMensagem()
    {
        bool IncluiMsg = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeLogada, idProjeto, "null", "PR", 0, "null", "PR_IncMsg") && podeEditar;
        imgMensagens.ClientEnabled = IncluiMsg;
        if (imgMensagens.ClientEnabled)
            imgMensagens.Style.Add("cursor", "pointer");
        else
        {
            imgMensagens.ImageUrl = "../../imagens/questaoDes.gif";
        }
    }

    private void defineTelaInicial()
    {
        telaInicial = "";

        if (nvbMenuProjeto.Groups.Count > 0)
        {
            string nomeTela = "";
            nomeTelaInicial = "";


            if (Request.QueryString["TI"] != null && Request.QueryString["TI"].ToString() != "" && Request.QueryString["MostrarTelaInicial"] == null)
            {
                string param = Request.QueryString["TI"].ToString();

                if (param == "RIS")
                {
                    nomeTela = "riscos.aspx?TT=R&";
                    nomeTelaInicial = Resources.traducao.indexResumoProjeto_riscos_do_projeto;
                } if (param == "QUE")
                {
                    nomeTela = "riscos.aspx?TT=Q&";
                    nomeTelaInicial = "";
                }
                else if (param == "FIS")
                {
                    nomeTela = "Cronograma_gantt.aspx?";
                    DataSet ds = cDados.getParametrosSistema(codigoEntidadeLogada, "linkCronograma");
                    if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                    {
                        nomeTela = VirtualPathUtility.ToAbsolute("~/") + ds.Tables[0].Rows[0]["linkCronograma"].ToString() + "?";
                    }
                    nomeTelaInicial = Resources.traducao.indexResumoProjeto_visualizar_cronograma_do_projeto;
                }
                else if (param == "CUS")
                {
                    nomeTela = "FinanceiroGraficos.aspx?Tipo=D&";
                    nomeTelaInicial = Resources.traducao.indexResumoProjeto_financeiro_do_projeto;
                }
                else if (param == "REC")
                {
                    nomeTela = "FinanceiroGraficos.aspx?Tipo=R&";
                    nomeTelaInicial = Resources.traducao.indexResumoProjeto_recursos_do_projeto;
                }
            }

            if (nomeTela != "")
                telaInicial = cDados.getPathSistema() + "_Projetos/DadosProjeto/" + nomeTela + "IDProjeto=" + idProjeto;
            else
            {
                for (int i = 0; i < nvbMenuProjeto.Groups.Count; i++)
                {
                    if (nvbMenuProjeto.Groups[i].Items.Count > 0 && nvbMenuProjeto.Groups[i].ClientVisible == true)
                    {
                        telaInicial = cDados.getPathSistema() + nvbMenuProjeto.Groups[i].Items[0].NavigateUrl.ToString().Replace("~/", "");
                        nomeTelaInicial = nvbMenuProjeto.Groups[i].Items[0].Text;
                        break;
                    }
                }
            }           

            sp_Tela.Panes[1].ContentUrl = telaInicial;

            lblTituloTela.Text = nomeProjeto + " - " + nomeTelaInicial;
        }

        if (telaInicial == "" && nomeTelaInicial == "")
            cDados.RedirecionaParaTelaSemAcesso(this);
    }

    private string defineAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        //sp_Tela.Height = alturaTela - 190;
        return (alturaTela - 140).ToString();
    }

    private int getLarguraTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        sp_Tela.Panes[1].Size = new Unit(largura - 165);
        return largura - 210;
    }

    protected void nvbMenuProjeto_ItemClick(object source, NavBarItemEventArgs e)
    {
        if (Request.QueryString["NomeProjeto"] != null && Request.QueryString["NomeProjeto"].ToString() != "")
        {
            lblTituloTela.Text = nomeTipoProjeto + ": " + Request.QueryString["NomeProjeto"].ToString() + " - Resumo Projeto";
        }

    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string msg = "";

        callback.JSProperties["cp_result"] = "";
        callback.JSProperties["cp_Erro"] = "";

        bool result = false;
        if (e.Parameter == "A")
        {
            result = cDados.atualizaStatusProjeto(idProjeto, codigoUsuarioResponsavel, ref msg);

            if (result)
            {
                callback.JSProperties["cp_result"] = Resources.traducao.indexResumoProjeto_projeto_atualizado_com_sucesso_;
            }
            else
            {
                callback.JSProperties["cp_Erro"] = Resources.traducao.indexResumoProjeto_erro_ao_atualizar_o_projeto__ + msg.Replace(Environment.NewLine.ToString(), " ");
            }

        }
        else if(e.Parameter == "R")
        {
            //Response.Redirect(string.Format(@"~/_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto={0}", Request.QueryString["IDProjetoOrigem"]));
            int idProjetoARedirecionar = 0;
            bool retorno = false;
            string comandosql = string.Format(@"SELECT CodigoProjetoPai
                                                  FROM linkprojeto WHERE CodigoProjetoFilho = {0}", idProjeto);
            DataSet dsCodigoProjetoPai = cDados.getDataSet(comandosql);
            if (cDados.DataSetOk(dsCodigoProjetoPai) && cDados.DataTableOk(dsCodigoProjetoPai.Tables[0]))
            {
                retorno = int.TryParse(dsCodigoProjetoPai.Tables[0].Rows[0]["CodigoProjetoPai"].ToString(), out idProjetoARedirecionar);
            }
            Response.RedirectLocation = string.Format(@"indexResumoProjeto.aspx?IDProjeto={0}", idProjetoARedirecionar);
        }
    }

    #region Menu Lateral

    private void carregaMenuLateral()
    {
        string codigoProjeto = hfGeral.Get("hfCodigoProjeto").ToString();

        DataSet ds = cDados.getMenuProjetoUsuario(codigoUsuarioResponsavel.ToString(), codigoProjeto, cDados.getInfoSistema("CodigoEntidade").ToString());

        //bool indicaProjetoObra = cDados.getIndicaProjetoObra(codigoProjeto);

        nvbMenuProjeto.Groups.Clear();

        if ((cDados.DataSetOk(ds)) && (cDados.DataTableOk(ds.Tables[0])))
        {
            DataRow[] drGrupos = ds.Tables[0].Select("NivelObjetoMenu = 1", "OrdemObjeto");

            foreach (DataRow drG in drGrupos)
            {
                NavBarGroup nbg = new NavBarGroup(drG["NomeMenu"].ToString(), drG["Iniciais"].ToString());
                nvbMenuProjeto.Groups.Add(nbg);                

                DataRow[] drItens = ds.Tables[0].Select("NivelObjetoMenu > 1 AND CodigoObjetoMenuPai = " + drG["CodigoObjetoMenu"], "OrdemObjeto");

                foreach (DataRow drI in drItens)
                {
                    string alturaFrameForms = defineAlturaTela();
                    int largura = getLarguraTela();
                    string larguraFrameForms = largura.ToString();
                    string idItem = "op_" + drI["CodigoObjetoMenuPai"] + "_" + drI["CodigoObjetoMenu"];
                    string urlItem = drI["URLObjetoMenu"].ToString();

                    //if (indicaProjetoObra && drG["CodigoObjetoMenu"].ToString() == "1" && drI["CodigoObjetoMenu"].ToString() == "1")
                    //{
                    //    urlItem = "~/_Projetos/DadosProjeto/ResumoObra.aspx?IDProjeto=@CodigoProjeto&tp=@TituloMenu&NP=@NomeProjeto";
                    //}

                    urlItem = urlItem.Replace("@CodigoProjeto", idProjeto.ToString()).Replace("@NomeProjeto", "").Replace("@TituloMenu", "");

                    urlItem = urlItem.Replace("@Altura", alturaFrameForms.ToString()).Replace("@Largura", larguraFrameForms.ToString());

                    if(!podeEditar)
                        urlItem = urlItem.Replace("RO=N", "RO=S");                    

                    if (urlItem.Contains("/ResumoProjeto.aspx"))
                    {
                        string urlResumo = "./ResumoProjeto.aspx" + urlItem.Substring(urlItem.IndexOf("?"));
                        btnSelecionar.JSProperties["cp_URL"] = urlResumo;

                        if (mostrarFiltroFinanceiro)
                        {
                            if (urlItem.IndexOf("?") == -1)
                                urlItem = urlItem + "?Financeiro=" + (ddlFinanceiro.SelectedIndex == -1 ? "A" : ddlFinanceiro.Value.ToString());
                            else
                                urlItem = urlItem + "&Financeiro=" + (ddlFinanceiro.SelectedIndex == -1 ? "A" : ddlFinanceiro.Value.ToString());
                        }
                    }

                    nbg.Items.Add(drI["NomeMenu"].ToString(), idItem, "", urlItem, "framePrincipal");
                }

                if (drG["Iniciais"].ToString().Equals("FORMULARIO"))
                {
                    populaItensFormulario(nbg);
                }
                else if (drG["Iniciais"].ToString().Equals("FLUXOS"))
                {
                    populaOpcoesDeFluxos(nbg);
                }

                if (nbg.Items.Count == 0)
                    nbg.ClientVisible = false;
            }
        }

        nvbMenuProjeto.AutoCollapse = true;
    }

    private bool populaItensFormulario(NavBarGroup nbg)
    {
        string alturaFrameForms = (int.Parse(defineAlturaTela()) - 60).ToString();

        int largura = getLarguraTela();
        string larguraFrameForms = largura.ToString();
        string comandoSQL = string.Format(
            @"
SELECT DISTINCT 
        mf.[CodigoModeloFormulario]
		, mftp.[TextoOpcaoFormulario]					AS [Opcao]
    , mftp.[TipoOcorrenciaFormulario]			AS [Tipo]
    , mftp.[SomenteLeitura]
	FROM
		{0}.{1}.Projeto											AS [p]
		
			INNER JOIN {0}.{1}.[ModeloFormularioStatusTipoProjeto]	        AS [mfstp]
  ON (	mfstp.[CodigoTipoProjeto]	= p.[CodigoTipoProjeto]
	    AND	mfstp.[CodigoStatus]			= p.[CodigoStatusProjeto] )
	    
                INNER JOIN {0}.{1}.[ModeloFormularioTipoProjeto]	        AS [mftp]
	ON (	mftp.[CodigoModeloFormulario]	= mfstp.[CodigoModeloFormulario]
		AND mftp.[CodigoTipoProjeto]	= mfstp.[CodigoTipoProjeto] )
		
			    INNER JOIN {0}.{1}.[ModeloFormulario]					    AS [mf]
	ON (	mf.[CodigoModeloFormulario]				= mfstp.[CodigoModeloFormulario] )
	
	WHERE
		    p.[CodigoProjeto]				= {2}
    AND  mftp.[StatusRelacionamento]		= 'A'
    AND mfstp.[StatusRelacionamento]		= 'A'
    AND	   mf.[IndicaModeloPublicado]		= 'S'
    AND    mf.[DataExclusao]                IS NULL
    AND    mf.[CodigoEntidade]              = {3} 
    ORDER BY 
			mftp.[TextoOpcaoFormulario]", dbName, dbOwner, idProjeto, codigoEntidadeLogada);

        DataTable dt = cDados.getDataSet(comandoSQL).Tables[0];        

        foreach (DataRow dr in dt.Rows)
        {
            string readOnly = podeEditar == false ? "S" : dr["SomenteLeitura"] + "";
            NavBarItem item = new NavBarItem(dr["Opcao"].ToString(), "ID_" + dr["CodigoModeloFormulario"], "", "~/wfRenderizaFormulario.aspx?CPWF=" + idProjeto + "&CMF=" + dr["CodigoModeloFormulario"] + "&AT=" + alturaFrameForms + "&WSCR=" + larguraFrameForms + "&RO=" + readOnly + "&INIPERM=PR_AltCnuFrm", "framePrincipal");
            nbg.Items.Add(item);
        }
        return dt.Rows.Count > 0;
    }

    private bool populaOpcoesDeFluxos(NavBarGroup nbg)
    {
        int nivelAcessoEtapaInicial;
        int codigoFluxo, codigoWorkflow, codigoEtapaInicial;
        string link;

        //bool mostraReformulacao = mostraReformulacaoTAI();
        //string codigoFluxoNovaProposta = "-1";

        //DataSet dsParam = cDados.getParametrosSistema(codigoEntidadeLogada, "codigoFluxoNovaPropostaIniciativa");

        //if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]) && dsParam.Tables[0].Rows[0]["codigoFluxoNovaPropostaIniciativa"].ToString().Trim() != "")
        //    codigoFluxoNovaProposta = dsParam.Tables[0].Rows[0]["codigoFluxoNovaPropostaIniciativa"].ToString();

        string comandoSQL = string.Format(@" 
            EXECUTE {0}.{1}.[p_wf_obtemListaFluxosProjeto] {2}, {3} 
        ", dbName, dbOwner, codigoUsuarioResponsavel.ToString(), idProjeto.ToString());

        DataTable dt = cDados.getDataSet(comandoSQL).Tables[0];

        string whereReformulacao = "";

        //if (!mostraReformulacao)
        //    whereReformulacao = " CodigoFluxo <> " + codigoFluxoNovaProposta;

        DataRow[] drs = dt.Select(whereReformulacao);

        foreach (DataRow dr in drs)
        {
            if (int.TryParse(dr["CodigoFluxo"].ToString(), out codigoFluxo) &&
                int.TryParse(dr["CodigoWorkflow"].ToString(), out codigoWorkflow) &&
                int.TryParse(dr["CodigoEtapaInicial"].ToString(), out codigoEtapaInicial))
            {
                // se o nivel de acesso devolvido para a etapa em questão contiver o número 2, 
                // é sinal que o usuário tem acesso de ação na etapa
                nivelAcessoEtapaInicial = cDados.obtemNivelAcessoEtapaWfNaoInstanciada(codigoWorkflow, idProjeto.ToString(), codigoEtapaInicial, codigoUsuarioResponsavel.ToString());
                string readOnly = (!podeEditar) ? "S" : "N";
                string acessoPrimeiraInstancia = ((nivelAcessoEtapaInicial & 2) == 0) ? "N" : "S";
                // se o fluxo for do tipo "U" -> único por projeto e ainda não existir instância,
                // já instancia o fluxo diretamente
                //if ((dr["Tipo"].ToString().ToUpper().Equals("U")) && (0 == dr["CodigoWorkflow2"].ToString().Length))
                //{
                //    link = "~/wfEngineInterno.aspx?CF=" + dr["CodigoFluxo"] + "&CP=" + idProjeto + "&CW=" + dr["CodigoWorkflow"] + "&RO=" + readOnly;
                //}
                //else
                //{
                link = "~/_Portfolios/listaProcessosInterno.aspx?CF=" + dr["CodigoFluxo"] + "&CP=" + idProjeto + "&CW=" + dr["CodigoWorkflow"] + "&AEI=" + nivelAcessoEtapaInicial.ToString() + "&RO=" + readOnly + "&TF=" + dr["Tipo"].ToString().ToUpper() + "&API=" + acessoPrimeiraInstancia;
                //}
                NavBarItem item = new NavBarItem(dr["Opcao"].ToString(), "ID_" + dr["CodigoWorkflow"], "", link, "framePrincipal");
                nbg.Items.Add(item);
                //}
            }
        }
        return dt.Rows.Count > 0;
    }

    private bool mostraReformulacaoTAI()
    {
        try
        {
            string comandoSQL = string.Format(@" 
            SELECT ISNULL(EtapaOrcamento, 1) AS EtapaOrcamento FROM {0}.{1}.TermoAbertura02 WHERE CodigoProjeto = {2}
        ", dbName, dbOwner, idProjeto.ToString());

            DataSet ds = cDados.getDataSet(comandoSQL);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["EtapaOrcamento"].ToString() == "2")
                return true;
        }
        catch {
            return false;
        }

        return false;
    }

    #endregion

    protected void callbackMenu_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        carregaMenuLateral();
        
        try
        {
            if (nvbMenuProjeto.Groups.FindByName("FLUXOS") != null)
            {
                nvbMenuProjeto.Groups.FindByName("FLUXOS").Expanded = true;
            }
        }
        catch { }
    }

    protected void cbImagemAjuda_Callback(object sender, CallbackEventArgsBase e)
    {
        string urlTratada1 = "";
        string retornoCodigoGlossario = "";
        string url = e.Parameter;
        if(url.IndexOf('?') != -1){
            string[] urlTratada = url.Substring(0, url.IndexOf('?')).Split('/');
            
            for (int i = 3; i < urlTratada.Length; i++)
            {
                urlTratada1 += "\\" + urlTratada[i];
            }
        }
        else
        {
            urlTratada1 = url;
        }
        
        retornoCodigoGlossario = cDados.getCodigoGlossarioTela(urlTratada1);

        //retorno
        imgAjudaGlossarioTipoProjeto.JSProperties["cp_CodigoGlossario"] = retornoCodigoGlossario;
    }

    protected void imgAjudaGlossarioTipoProjeto_Init(object sender, EventArgs e)
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
        ((ASPxImage)sender).ClientVisible = false;
        DataSet ds = cDados.getParametrosSistema("utilizaGlossario");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()) && !string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0][0].ToString()))
            {
                if(ds.Tables[0].Rows[0][0].ToString().ToLower().Trim() == "s")
                {
                    ((ASPxImage)sender).ClientVisible = true;
                }
            }                
        }
    }
    protected void gvConsultas_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "IndicaListaPadrao")
            e.Editor.ReadOnly = (e.Value as string) == "S";
    }

    protected void gvConsultas_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (gvConsultas.VisibleRowCount <= 1)
            return;

        if (e.ButtonType == ColumnCommandButtonType.Delete)
        {
            string indicaListaPadrao = gvConsultas.GetRowValues(
                e.VisibleIndex, "IndicaListaPadrao") as string;
            if (indicaListaPadrao == "S")
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.PNG";
                e.Image.ToolTip = "<%# Resources.traducao.index_s____poss_vel_excluir_uma_consulta_definida_como_padr_o_quando_n_o_houver_outras_consultas_ %>";
            }
        }
    }

    protected void popup_WindowCallback(object source, PopupWindowCallbackArgs e)
    {
        if (e.Window.Name == "winGerenciarConsultas")
        {
            string[] parametros = e.Parameter.Split(';');
            int codigoUsuario = int.Parse(parametros[0]);
            int codigoLista = int.Parse(parametros[1]);
            Session["codUsuario"] = codigoUsuario;
            Session["codLista"] = codigoLista;
            gvConsultas.CancelEdit();
            gvConsultas.DataBind();
            SelecionaLinhaPadrao();
        }
    }

    private void SelecionaLinhaPadrao()
    {
        for (int i = 0; i < gvConsultas.VisibleRowCount; i++)
        {
            string indicaListaPadrao = gvConsultas.GetRowValues(i, "IndicaListaPadrao") as string;
            if (indicaListaPadrao == "S")
            {
                gvConsultas.Selection.UnselectAll();
                gvConsultas.Selection.SelectRow(i);
                break;
            }
        }
    }

}

