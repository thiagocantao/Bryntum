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

public partial class _Projetos_DadosProjeto_indexResumoDemanda : System.Web.UI.Page
{
    public string alturaTabela;
    dados cDados;

    private string dbName;
    private string dbOwner;
    private string nomeProjeto = "";

    private int codigoEntidadeLogada;
    private int codigoUsuarioResponsavel;

    public int idProjeto = 0;
    public string telaInicial = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
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

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/indexResumoDemanda.js""></script>"));
        this.TH(this.TS("indexResumoDemanda"));
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        
        hfGeral.Set("hfCodigoEntidade", codigoEntidadeLogada);

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            idProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
            hfGeral.Set("hfCodigoProjeto", idProjeto);
        }

        if (Request.QueryString["NomeProjeto"] != null && Request.QueryString["NomeProjeto"].ToString() != "")
        {
            nomeProjeto = Request.QueryString["NomeProjeto"].ToString();
            lblTituloTela.Text = nomeProjeto + " - Status da Demanda";
            hfGeral.Set("hfNomeProjeto", nomeProjeto);
        }

        alturaTabela = getAlturaTela() + "px";

        if (!IsPostBack)
        {            
            carregaMenuLateral();
            defineTelaInicial();
        }

        definePermissoesIncluiMensagem();

        DataSet dsParametros = cDados.getParametrosSistema("expandirTodoMenu");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
            nvbMenuProjeto.AutoCollapse = dsParametros.Tables[0].Rows[0]["expandirTodoMenu"].ToString() == "N";
        
        this.Title = cDados.getNomeSistema();
    }

    private void definePermissoesIncluiMensagem()
    {
        bool IncluiMsg = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeLogada, idProjeto, "null", "DC", 0, "null", "DC_IncMsg");
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
        string nomeTelaInicial = "";

        if (nvbMenuProjeto.Groups.Count > 0)
        {
            string nomeTela = "ResumoProjeto.aspx?";

            if (Request.QueryString["TI"] != null && Request.QueryString["TI"].ToString() != "")
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
                    DataSet ds = cDados.getParametrosSistema(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), "linkCronograma");
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
                telaInicial = "./" + nomeTela + "IDProjeto=" + idProjeto + "&TipoTela=D";
            else
            {
                for (int i = 0; i < nvbMenuProjeto.Groups.Count; i++)
                {
                    if (nvbMenuProjeto.Groups[i].Items.Count > 0 && nvbMenuProjeto.Groups[i].ClientVisible == true)
                    {
                        telaInicial = nvbMenuProjeto.Groups[i].Items[0].NavigateUrl.ToString();
                        nomeTelaInicial = nvbMenuProjeto.Groups[i].Items[0].Text;
                        break;
                    }
                }
            }

            lblTituloTela.Text = nomeProjeto + " - " + nomeTelaInicial;

        }
        else
        {
            telaInicial = "../../erros/SemAcesso.aspx";
        }    
    }

    #region Menu Lateral

    private void carregaMenuLateral()
    {
        string codigoProjeto = hfGeral.Get("hfCodigoProjeto").ToString();

        DataSet ds = cDados.GetPermissoesUsuarioDemanda(codigoUsuarioResponsavel.ToString(), codigoProjeto, cDados.getInfoSistema("CodigoEntidade").ToString());

        if ((cDados.DataSetOk(ds)) && (cDados.DataTableOk(ds.Tables[0])))
        {
            carregaMenuPrincipal(ds);
            carregaMenuTempoEscopo(ds);
            carregaMenuComunicacao(ds);
            carregaMenuFormulario(ds);
            carregaMenuFluxo(ds);
        }

        bool mostrarMenuTarefas = false;
        nvbMenuProjeto.Groups.FindByName("opTarefas").ClientVisible = mostrarMenuTarefas;

        DataSet dsParametros = cDados.getParametrosSistema("expandirTodoMenu");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
            nvbMenuProjeto.AutoCollapse = dsParametros.Tables[0].Rows[0]["expandirTodoMenu"].ToString() == "N";
    }

    private void carregaMenuPrincipal(DataSet ds)
    {
        string definicaoToDoList = "To Do List";
        DataSet dsParametros = cDados.getParametrosSistema("labelToDoList");
        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["labelToDoList"] + "" != "")
            definicaoToDoList = dsParametros.Tables[0].Rows[0]["labelToDoList"] + "";

        NavBarItem nvbStatusProj = new NavBarItem("Status da Demanda", "opStatus", "", "ResumoProjeto.aspx?IDProjeto=" + idProjeto + "&TipoTela=D&tp=Status da Demanda", "framePrincipal");
        nvbMenuProjeto.Groups.FindByName("opPrincipal").Items.Add(nvbStatusProj);

        if (ds.Tables[0].Rows[0]["VisualizarCronograma"].ToString().Equals("1"))
        {
            hfGeral.Set("urlEAPvisualica", "EdicaoEAP.aspx?IDProjeto=" + idProjeto + "&AM=RO&CU=" + codigoUsuarioResponsavel + "&CE=" + codigoEntidadeLogada + "&NP=" + nomeProjeto);
            string linkCronograma = string.Empty;
            DataSet ds1 = cDados.getParametrosSistema(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), "linkCronograma");
            if (cDados.DataSetOk(ds1) && cDados.DataTableOk(ds1.Tables[0]))
            {
                linkCronograma = VirtualPathUtility.ToAbsolute("~/") + ds.Tables[0].Rows[0]["linkCronograma"].ToString() + "?IDProjeto=" + idProjeto + "&TipoTela=D&tp=Cronograma";
            }

            nvbMenuProjeto.Groups.FindByName("opPrincipal").Items.Add(new NavBarItem("Cronograma", "opVisCrono", "", linkCronograma, "framePrincipal"));
        }

        if (ds.Tables[0].Rows[0]["MenuRH"].ToString().Equals("1"))
            nvbMenuProjeto.Groups.FindByName("opPrincipal").Items.Add(new NavBarItem("Recursos", "opRecursosHumanos", "", "RecursosHumanosGraficos.aspx?IDProjeto=" + idProjeto + "&TipoTela=D&tp=Recursos", "framePrincipal"));

        if (ds.Tables[0].Rows[0]["MenuFinanceiro"].ToString().Equals("1"))
            nvbMenuProjeto.Groups.FindByName("opPrincipal").Items.Add(new NavBarItem("Financeiro", "opFinanceiro", "", "FinanceiroGraficos.aspx?IDProjeto=" + idProjeto + "&TipoTela=D&tp=Financeiro", "framePrincipal"));

        if (ds.Tables[0].Rows[0]["MenuToDoList"].ToString().Equals("1"))
            nvbMenuProjeto.Groups.FindByName("opPrincipal").Items.Add(new NavBarItem(definicaoToDoList, "opToDoList", "", "TarefasToDoList.aspx?IDProjeto=" + idProjeto + "&TipoTela=D&tp=To Do List", "framePrincipal"));
           
        if (ds.Tables[0].Rows[0]["MenuCaracterizacao"].ToString().Equals("1"))
            nvbMenuProjeto.Groups.FindByName("opPrincipal").Items.Add(new NavBarItem("Caracterização", "opCaracterizacao", "", "Caracterizacao.aspx?IDProjeto=" + idProjeto + "&TipoTela=D&tp=Caracterização", "framePrincipal"));

        if (ds.Tables[0].Rows[0]["MenuPermissao"].ToString().Equals("1"))
            nvbMenuProjeto.Groups.FindByName("opPrincipal").Items.Add(new NavBarItem("Permissões", "opPermissoes", "", "../../_Estrategias/InteressadosObjeto.aspx?TIT=N&ITO=DC&COE=" + idProjeto + "&TOE=" + nomeProjeto, "framePrincipal"));

        nvbMenuProjeto.Groups.FindByName("opPrincipal").ClientVisible = nvbMenuProjeto.Groups.FindByName("opPrincipal").Items.Count > 0;
    }

    private void carregaMenuTempoEscopo(DataSet ds)
    {
        bool podeEditarEAP = getPodeEditarEAP();
        NavBarGroup group = nvbMenuProjeto.Groups.FindByName("opTempoEscopo");

        if (null != group)
        {
            if (ds.Tables[0].Rows[0]["EditarCronograma"].ToString().Equals("1"))
            {
                string linkOpcao = cDados.getLinkPortalDesktop(Request.Url, codigoEntidadeLogada, codigoUsuarioResponsavel, idProjeto, "./../../");
                group.Items.Add(new NavBarItem("Editar Cronograma", "opEditarCronograma", "", linkOpcao, "framePrincipal"));
                hfGeral.Set("urlEAPedicao", "EdicaoEAP.aspx?IDProjeto=" + idProjeto + "&AM=RW&CU=" + codigoUsuarioResponsavel + "&CE=" + codigoEntidadeLogada + "&NP=" + nomeProjeto);

                NavBarItem itemEditarEAP = new NavBarItem("Editar EAP");
                itemEditarEAP.ClientEnabled = podeEditarEAP;

                group.Items.Add(itemEditarEAP); //, "opEditarEAP", "", "EdicaoEAP.aspx?IDProjeto=" + idProjeto + "&AM=RW&CU=" + codigoUsuarioResponsavel + "&CE=" + codigoEntidadeLogada, "framePrincipal"));

            }

            if (ds.Tables[0].Rows[0]["MenuDesbloqueio"].ToString().Equals("1"))
                group.Items.Add(new NavBarItem("Desbloqueio", "opCheckin", "", "checkinProjetos.aspx?IDProjeto=" + idProjeto, "framePrincipal"));

            group.ClientVisible = group.Items.Count > 0;
        }

    }
    
    private void carregaMenuComunicacao(DataSet ds)
    {


        if (ds.Tables[0].Rows[0]["MenuReuniao"].ToString().Equals("1"))
        {
            nvbMenuProjeto.Groups.FindByName("opComunicacao").Items.Add(new NavBarItem("Reuniões", "opReuniao", "", "../../Reunioes/reunioes.aspx?TA=PR&TipoTela=D&idProjeto=" + idProjeto + "&NMP=" + hfGeral.Get("hfNomeProjeto").ToString() + "&MOD=PRJ&IOB=PR", "framePrincipal"));

        }

        if (ds.Tables[0].Rows[0]["MenuMensage"].ToString().Equals("1"))
        {
            nvbMenuProjeto.Groups.FindByName("opComunicacao").Items.Add(new NavBarItem("Mensagens", "opMensagens", "", "editaMensagens.aspx?IDProjeto=" + idProjeto + "&TipoTela=D&NMP=" + hfGeral.Get("hfNomeProjeto").ToString(), "framePrincipal"));

        }

        if (ds.Tables[0].Rows[0]["MenuAnexo"].ToString().Equals("1"))
        {
            nvbMenuProjeto.Groups.FindByName("opComunicacao").Items.Add(new NavBarItem("Anexos", "opAnexos", "", "../../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?TA=PR&ID=" + idProjeto + "&NMP=" + hfGeral.Get("hfNomeProjeto").ToString(), "framePrincipal"));

        }

        nvbMenuProjeto.Groups.FindByName("opComunicacao").ClientVisible = nvbMenuProjeto.Groups.FindByName("opComunicacao").Items.Count > 0;
    }

    private void carregaMenuFormulario(DataSet ds)
    {

        if (ds.Tables[0].Rows[0]["MenuFormulario"].ToString().Equals("1"))
        {
            populaItensFormulario();

        }
        nvbMenuProjeto.Groups.FindByName("opFormularios").ClientVisible = nvbMenuProjeto.Groups.FindByName("opFormularios").Items.Count > 0;
    }

    private void carregaMenuFluxo(DataSet ds)
    {


        if (ds.Tables[0].Rows[0]["MenuFluxo"].ToString().Equals("1"))
        {
            populaOpcoesDeFluxos();

        }

        nvbMenuProjeto.Groups.FindByName("opFluxos").ClientVisible = nvbMenuProjeto.Groups.FindByName("opFluxos").Items.Count > 0;
    }


    private bool populaItensFormulario()
    {
        string alturaFrameForms = getAlturaTela();

        int largura = getLarguraTela();
        string larguraFrameForms = largura.ToString();
        string comandoSQL = string.Format(
            @"
            SELECT DISTINCT 
                mf.[CodigoModeloFormulario]
		       ,mftp.[TextoOpcaoFormulario]					AS [Opcao]
               ,mftp.[TipoOcorrenciaFormulario]			AS [Tipo]
               ,mftp.[SomenteLeitura]
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
            NavBarItem item = new NavBarItem(dr["Opcao"].ToString(), "ID_" + dr["CodigoModeloFormulario"], "", "~/wfRenderizaFormulario.aspx?CPWF=" + idProjeto + "&TipoTela=D&CMF=" + dr["CodigoModeloFormulario"] + "&AT=" + alturaFrameForms + "&WSCR=" + larguraFrameForms + "&RO=" + dr["SomenteLeitura"], "framePrincipal");
            nvbMenuProjeto.Groups.FindByName("opFormularios").Items.Add(item);
        }
        return dt.Rows.Count > 0;
    }

    private bool populaOpcoesDeFluxos()
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
                //if ((2 & nivelAcessoEtapaInicial) > 0)
                //{
                // se o fluxo for do tipo "U" -> único por projeto e ainda não existir instância,
                // já instancia o fluxo diretamente
                if ((dr["Tipo"].ToString().ToUpper().Equals("U")) && (0 == dr["CodigoWorkflow2"].ToString().Length))
                {
                    link = "~/wfEngineInterno.aspx?CF=" + dr["CodigoFluxo"] + "&CP=" + idProjeto + "&CW=" + dr["CodigoWorkflow"];
                }
                else
                {
                    link = "~/_Portfolios/listaProcessosInterno.aspx?CF=" + dr["CodigoFluxo"] + "&TipoTela=D&CP=" + idProjeto + "&CW=" + dr["CodigoWorkflow"] + "&AEI=" + nivelAcessoEtapaInicial.ToString();
                }
                NavBarItem item = new NavBarItem(dr["Opcao"].ToString(), "ID_" + dr["CodigoWorkflow"], "", link, "framePrincipal");
                nvbMenuProjeto.Groups.FindByName("opFluxos").Items.Add(item);
                //}
            }
        }
        return dt.Rows.Count > 0;
    }

    #endregion

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
        return largura-140;
    }

    protected void nvbMenuProjeto_ItemClick(object source, NavBarItemEventArgs e)
    {
        if (Request.QueryString["NomeProjeto"] != null && Request.QueryString["NomeProjeto"].ToString() != "")
        {
            lblTituloTela.Text = Request.QueryString["NomeProjeto"].ToString() + " - Status da Demanda";
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

    protected void cbkGeral_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string parametroEAP = e.Parameter;
        if ("" != parametroEAP)
            desbloquear(parametroEAP);
    }

    private void desbloquear(string parametroEAP)
    {
        //fazer as mudanças do desbloquei do cronograma.
        //pasando como parametro o codigoEAP
        string comandoSQL = "";
        comandoSQL = string.Format(@"
            --Desbloquear Cronograma.
            EXEC {0}.{1}.[p_crono_UndoCheckoutEdicaoEAP] @in_IdEdicaoEAP = '{2}'
            ", dbName, dbOwner, parametroEAP);
        System.Diagnostics.Debug.WriteLine(comandoSQL);
        int regAfetados = 0;
        cDados.execSQL(comandoSQL, ref regAfetados);
    }

    private bool getPodeEditarEAP()
    {
        bool retorno = true;
        string comandoSQL = string.Format(@"
                SELECT  DataUltimaGravacaoDesktop 
                FROM    {0}.{1}.CronogramaProjeto 
                WHERE   CodigoProjeto = {2}
                  AND   DataUltimaGravacaoDesktop IS NOT NULL
                ", dbName, dbOwner, idProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            retorno = false;

        return retorno;
    }
}
