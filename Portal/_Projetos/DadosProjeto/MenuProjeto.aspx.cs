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

public partial class _Projetos_DadosProjeto_menu : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        dbOwner = cDados.getDbOwner();
        dbName = cDados.getDbName();

        imgAtualizar.Style.Add("cursor", "pointer");
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

        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        hfGeral.Set("hfCodigoEntidade", codigoEntidadeLogada);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/indexResumoProjeto.js""></script>"));
        this.TH(this.TS("indexResumoProjeto"));
        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            idProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
            hfGeral.Set("hfCodigoProjeto", idProjeto);
        }
        
        if (!IsPostBack)
        {
            string msg = "";
            cDados.sincronizaStatusProjeto(idProjeto, ref msg);
        }

        nomeTipoProjeto = cDados.getNomeTipoProjeto(idProjeto);


        nomeProjeto = cDados.getNomeProjeto(idProjeto.ToString(), "");
            hfGeral.Set("hfNomeProjeto", nomeProjeto);
       

        alturaTabela = getAlturaTela() + "px";

        if (!IsPostBack)
        {
            carregaMenuLateral();
        }

        definePermissoesIncluiMensagem();

        string labelTelaResumoProjeto = "Status do " + nomeTipoProjeto;

        if (!IsPostBack)
        {
            defineTelaInicial();

            int nivel = 2;

            if (Request.QueryString["NivelNavegacao"] != null && Request.QueryString["NivelNavegacao"].ToString() != "")
                nivel = int.Parse(Request.QueryString["NivelNavegacao"].ToString());

            cDados.aplicaEstiloVisual(Page);
            cDados.excluiNiveisAbaixo(nivel);
            cDados.insereNivel(nivel, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, nomeProjeto, "ST_PRJ", "PROJ", idProjeto, "Adicionar Projeto aos Favoritos");
        }
        this.Title = cDados.getNomeSistema();
       
    }

    private void definePermissoesIncluiMensagem()
    {
        bool IncluiMsg = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeLogada, idProjeto, "null", "PR", 0, "null", "PR_IncMsg");
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
                    nomeTelaInicial = "Riscos do Projeto";
                }
                else if (param == "FIS")
                {
                    nomeTela = "Cronograma_gantt.aspx?";
                    DataSet ds = cDados.getParametrosSistema(codigoEntidadeLogada, "linkCronograma");
                    if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                    {
                        nomeTela = VirtualPathUtility.ToAbsolute("~/") + ds.Tables[0].Rows[0]["linkCronograma"].ToString() + "?";
                    }
                    nomeTelaInicial = "Visualizar Cronograma do Projeto";
                }
                else if (param == "CUS")
                {
                    nomeTela = "FinanceiroGraficos.aspx?Tipo=D&";
                    nomeTelaInicial = "Financeiro do Projeto";
                }
                else if (param == "REC")
                {
                    nomeTela = "FinanceiroGraficos.aspx?Tipo=R&";
                    nomeTelaInicial = "Recursos do Projeto";
                }
            }

            if (nomeTela != "")
                telaInicial = "./" + nomeTela + "IDProjeto=" + idProjeto + "&NMP=" + hfGeral.Get("hfNomeProjeto").ToString();
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

            lblTituloTela.Text = nomeProjeto + " - " + nomeTelaInicial;
        }

        if (telaInicial == "" && nomeTelaInicial == "")
            cDados.RedirecionaParaTelaSemAcesso(this);
    }

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 150).ToString();
    }

    private int getLarguraTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        return largura - 140;
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
        bool result = cDados.atualizaStatusProjeto(idProjeto, codigoUsuarioResponsavel, ref msg);

        if (result)
        {
            callback.JSProperties["cp_result"] = "Projeto Atualizado com Sucesso!";
            callback.JSProperties["cp_status"] = "ok";
        }
        else
        {
            callback.JSProperties["cp_result"] = "Erro ao Atualizar o Projeto: " + msg.Replace(Environment.NewLine.ToString(), " ");
            callback.JSProperties["cp_status"] = "erro";
        }
    }

    #region Menu Lateral

    private void carregaMenuLateral()
    {
        string codigoProjeto = hfGeral.Get("hfCodigoProjeto").ToString();

        DataSet ds = cDados.getMenuProjetoUsuario(codigoUsuarioResponsavel.ToString(), codigoProjeto, cDados.getInfoSistema("CodigoEntidade").ToString());

        nvbMenuProjeto.Groups.Clear();

        if ((cDados.DataSetOk(ds)) && (cDados.DataTableOk(ds.Tables[0])))
        {
            DataRow[] drGrupos = ds.Tables[0].Select("NivelObjetoMenu = 1", "OrdemObjeto");

            foreach (DataRow drG in drGrupos)
            {
                NavBarGroup nbg = new NavBarGroup(drG["NomeMenu"].ToString(), drG["Iniciais"].ToString());
                nvbMenuProjeto.Groups.Add(nbg);

                if (drG["Iniciais"].ToString().Equals("FORMULARIO"))
                {
                    populaItensFormulario(nbg);
                }
                else if (drG["Iniciais"].ToString().Equals("FLUXOS"))
                {
                    populaOpcoesDeFluxos(nbg);
                }

                DataRow[] drItens = ds.Tables[0].Select("NivelObjetoMenu > 1 AND CodigoObjetoMenuPai = " + drG["CodigoObjetoMenu"], "OrdemObjeto");

                foreach (DataRow drI in drItens)
                {
                    string alturaFrameForms = getAlturaTela();
                    int largura = getLarguraTela();
                    string larguraFrameForms = largura.ToString();
                    string idItem = "op_" + drI["CodigoObjetoMenuPai"] + "_" + drI["CodigoObjetoMenu"];
                    string urlItem = drI["URLObjetoMenu"].ToString();
                    
                    urlItem = urlItem.Replace("@CodigoProjeto", idProjeto.ToString()).Replace("@NomeProjeto", nomeProjeto).Replace("@TituloMenu", drI["NomeMenu"].ToString());

                    urlItem = urlItem.Replace("@Altura", alturaFrameForms.ToString()).Replace("@Largura", larguraFrameForms);

                    nbg.Items.Add(drI["NomeMenu"].ToString(), idItem, "", urlItem, "framePrincipal");
                }

                if (nbg.Items.Count == 0)
                    nbg.ClientVisible = false;
            }
        }

        nvbMenuProjeto.AutoCollapse = true;
    }       

    private bool populaItensFormulario(NavBarGroup nbg)
    {
        string alturaFrameForms = getAlturaTela();

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
            NavBarItem item = new NavBarItem(dr["Opcao"].ToString(), "ID_" + dr["CodigoModeloFormulario"], "", "~/wfRenderizaFormulario.aspx?CPWF=" + idProjeto + "&CMF=" + dr["CodigoModeloFormulario"] + "&AT=" + alturaFrameForms + "&WSCR=" + larguraFrameForms + "&RO=" + dr["SomenteLeitura"] + "&INIPERM=PR_AltCnuFrm", "framePrincipal");
            nbg.Items.Add(item);
        }
        return dt.Rows.Count > 0;
    }

    private bool populaOpcoesDeFluxos(NavBarGroup nbg)
    {
        int nivelAcessoEtapaInicial;
        int codigoWorkflow, codigoEtapaInicial;
        string link;

        string comandoSQL = string.Format(@" 
            EXECUTE {0}.{1}.[p_wf_obtemListaFluxosProjeto] {2}, {3} 
        ", dbName, dbOwner, codigoUsuarioResponsavel.ToString(), idProjeto.ToString());

        DataTable dt = cDados.getDataSet(comandoSQL).Tables[0];
        foreach (DataRow dr in dt.Rows)
        {
            if (int.TryParse(dr["CodigoWorkflow"].ToString(), out codigoWorkflow) &&
                int.TryParse(dr["CodigoEtapaInicial"].ToString(), out codigoEtapaInicial))
            {
                // se o nivel de acesso devolvido para a etapa em questão contiver o número 2, 
                // é sinal que o usuário tem acesso de ação na etapa
                nivelAcessoEtapaInicial = cDados.obtemNivelAcessoEtapaWfNaoInstanciada(codigoWorkflow, idProjeto.ToString(), codigoEtapaInicial, codigoUsuarioResponsavel.ToString());
                string readOnly = ((nivelAcessoEtapaInicial & 2) == 0) ? "S" : "N";
                // se o fluxo for do tipo "U" -> único por projeto e ainda não existir instância,
                // já instancia o fluxo diretamente
                if ((dr["Tipo"].ToString().ToUpper().Equals("U")) && (0 == dr["CodigoWorkflow2"].ToString().Length))
                {
                    link = "~/wfEngineInterno.aspx?CF=" + dr["CodigoFluxo"] + "&CP=" + idProjeto + "&CW=" + dr["CodigoWorkflow"] + "&RO=" + readOnly;
                }
                else
                {
                    link = "~/_Portfolios/listaProcessosInterno.aspx?CF=" + dr["CodigoFluxo"] + "&CP=" + idProjeto + "&CW=" + dr["CodigoWorkflow"] + "&AEI=" + nivelAcessoEtapaInicial.ToString();
                }
                NavBarItem item = new NavBarItem(dr["Opcao"].ToString(), "ID_" + dr["CodigoWorkflow"], "", link, "framePrincipal");
                nbg.Items.Add(item);
                //}
            }
        }
        return dt.Rows.Count > 0;
    }

    #endregion
}

