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
    public string alturaTabela;

    private int codigoObjetivoEstrategico = 0;
    private int codigoUnidadeSelecionada = 0, codigoUnidadeLogada = 0, codigoUnidadeMapa = 0, codigoMapa = 0;
    private int idUsuarioLogado = 0;
    private int idObjetoPai = 0;
    private bool podeConsultarAcnSug = false;
    private bool podeConsultarAnl = false;
    private bool podeConsultarAnx = false;
    private bool podeConsultarEtg = false;
    private bool podeConsultarMsg = false;
    private bool podeConsultarRelatorio = false;
    private bool podeEnviarMsg = false;
    private bool podeConsultarPlnAcn = false;
    private bool podeConsultarAnInd = false;
    private bool podeConsultarAnFin = false;
    private int alturaFrameAnexos = 650;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.TH(this.TS("indexResumoObjetivo"));
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
        }

        if (Request.QueryString["COE"] != null && Request.QueryString["COE"].ToString() != "")
        {
            codigoObjetivoEstrategico = int.Parse(Request.QueryString["COE"].ToString());
            hfGeral.Set("hfCodigoObjetivo", codigoObjetivoEstrategico);
        }

        string nomeObjetivo = cDados.getNomeObjetivo(codigoObjetivoEstrategico);

        hfGeral.Set("hfNomeObjetivo", nomeObjetivo);

       if (Request.QueryString["CM"] != null && Request.QueryString["CM"].ToString() != "")
            codigoMapa = int.Parse(Request.QueryString["CM"].ToString());
	   if (Request.QueryString["UNM"] != null && Request.QueryString["UNM"].ToString() != "")
		   codigoUnidadeMapa = int.Parse(Request.QueryString["UNM"].ToString());
	   else
	   {
		   DataSet dsUnidadeMapa = cDados.getMapasEstrategicos(null, " AND Mapa.CodigoMapaEstrategico = " + codigoMapa);
		   codigoUnidadeMapa = int.Parse(dsUnidadeMapa.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString());
	   }

        codigoUnidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        alturaTabela = getAlturaTela() + "px";
        getLarguraTela();        

        carregaComboUnidades();

        codigoUnidadeSelecionada = ddlUnidade.SelectedIndex == -1 ? -1 : int.Parse(ddlUnidade.Value.ToString());

        int codigoEntidadeUnidadeSelecionada = cDados.getEntidadUnidadeNegocio(codigoUnidadeSelecionada);

        lblEntidadeDiferente.ClientVisible = codigoEntidadeUnidadeSelecionada != codigoUnidadeLogada;

        criaMenu();

        DataSet dsParametros = cDados.getParametrosSistema("expandirTodoMenu");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
            nvbMenuProjeto.AutoCollapse = dsParametros.Tables[0].Rows[0]["expandirTodoMenu"].ToString() == "N";

        getPermissoesConsulta();

        if (codigoUnidadeMapa != codigoUnidadeSelecionada)
        {
            nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Ana").Enabled = false;
            nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Tdl").Enabled = false;
            nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Men").Enabled = false;
            nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Ane").Enabled = false;
            nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("Mel").Enabled = false;
            nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("AnaliseIni").Enabled = false;
            nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("AnInd").Enabled = false;
            nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("AnFin").Enabled = false;
        }

        defineTelaInicial();

        DataTable dt = cDados.getParametrosSistema("labelAcoesSugeridasEstrategia").Tables[0];
        if (dt.Rows.Count > 0)
        {
            nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Asu").Text = dt.Rows[0]["labelAcoesSugeridasEstrategia"].ToString();
        }

        if (!IsPostBack)
        {            
            cDados.excluiNiveisAbaixo(3);
            cDados.insereNivel(3, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, Resources.traducao.objetivo_estrat_gico + ": " + nomeObjetivo, "OBJ_ES", "OBJ", codigoObjetivoEstrategico, Resources.traducao.adicionar_aos_favoritos);

            sp_Tela.Panes[1].ContentUrl = "detalhesObjetivoEstrategico.aspx?CM=" + codigoMapa + "&COE=" + codigoObjetivoEstrategico + "&UN=" + codigoUnidadeSelecionada + "&UNM=" + codigoUnidadeMapa;
            lblTituloTela.Text = nomeObjetivo + " - " + Resources.traducao.detalhes;
            lblTituloTela.ToolTip = nomeObjetivo + " - " + Resources.traducao.detalhes;

        }

        cDados.aplicaEstiloVisual(sp_Tela);
        cDados.aplicaEstiloVisual(nvbMenuProjeto);
    }

    private void defineTelaInicial()
    {
        int controle = 0;

        for (int i = 0; i < nvbMenuProjeto.Groups.Count; i++)
        {

            for (int j = 0; j < nvbMenuProjeto.Groups[i].Items.Count; j++)
            {
                if (nvbMenuProjeto.Groups[i].Items[j].Enabled == true)
                {
                    nvbMenuProjeto.JSProperties["cp_TelaInicial"] = nvbMenuProjeto.Groups[i].Items[j].NavigateUrl.ToString();
                    nvbMenuProjeto.JSProperties["cp_NomeTelaInicial"] = nvbMenuProjeto.Groups[i].Items[j].Text;
                    controle = 1;
                    break;
                }
            }
            if (controle == 1)
                break;
        }

    }

    private void criaMenu()
    {
        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Det").NavigateUrl = "detalhesObjetivoEstrategico.aspx?CM=" + codigoMapa + "&COE=" + codigoObjetivoEstrategico + "&UN=" + codigoUnidadeSelecionada + "&UNM=" + codigoUnidadeMapa;
        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Det").Target = "oe_desktop";
        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Ana").NavigateUrl = "ObjetivoEstrategico_Analises.aspx?CM=" + codigoMapa + "&COE=" + codigoObjetivoEstrategico + "&UN=" + codigoUnidadeSelecionada + "&UNM=" + codigoUnidadeMapa;
        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Ana").Target = "oe_desktop";
        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Asu").NavigateUrl = "ObjetivoEstrategico_AcoesSugeridas.aspx?CM=" + codigoMapa + "&COE=" + codigoObjetivoEstrategico + "&UN=" + codigoUnidadeSelecionada + "&UNM=" + codigoUnidadeMapa;
        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Asu").Target = "oe_desktop";
        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Tdl").NavigateUrl = "ObjetivoEstrategico_ToDoList.aspx?CM=" + codigoMapa + "&COE=" + codigoObjetivoEstrategico + "&UN=" + codigoUnidadeSelecionada + "&UNM=" + codigoUnidadeMapa;
        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Tdl").Target = "oe_desktop";
        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Est").NavigateUrl = "ObjetivoEstrategico_Estrategias.aspx?CM=" + codigoMapa + "&COE=" + codigoObjetivoEstrategico + "&UN=" + codigoUnidadeSelecionada + "&UNM=" + codigoUnidadeMapa;
        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Est").Target = "oe_desktop";

        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Ane").NavigateUrl = "../../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?TA=OB&ID=" + codigoObjetivoEstrategico + "&UN=" + codigoUnidadeSelecionada + "&UNM=" + codigoUnidadeMapa + "&ALT=" + alturaFrameAnexos + "&Frame=S";
        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Ane").Target = "oe_desktop";
        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Men").NavigateUrl = "~/Mensagens/MensagensPainelBordo.aspx?CO=" + codigoObjetivoEstrategico + "&TA=OB&UN=" + codigoUnidadeSelecionada + "&UNM=" + codigoUnidadeMapa;
        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Men").Target = "oe_desktop";

        nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("Mel").NavigateUrl = "analiseMelhoresPraticas.aspx?CM=" + codigoMapa + "&COE=" + codigoObjetivoEstrategico + "&TA=OB&UN=" + codigoUnidadeSelecionada + "&UNM=" + codigoUnidadeMapa;
        nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("Mel").Target = "oe_desktop";
        nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("AnaliseIni").NavigateUrl = "relAnaliseIniciativas.aspx?CM=" + codigoMapa + "&COE=" + codigoObjetivoEstrategico + "&TA=OB&UN=" + codigoUnidadeSelecionada + "&UNM=" + codigoUnidadeMapa;
        nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("AnaliseIni").Target = "oe_desktop";
        
        string url = string.Format("{0}_dashboard/VisualizadorDashboard.aspx", cDados.getPathSistema());

        nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("AnInd").NavigateUrl = url + string.Format("?id=B48BBA01-C386-492E-9C00-B6D04E2EA40D&CodEntidade={1}&CodUsuario={2}&CodObjetoEstrategia={0}&origem=RD",
             codigoObjetivoEstrategico, codigoUnidadeLogada, idUsuarioLogado); 
        nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("AnInd").Target = "oe_desktop";
        nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("AnFin").NavigateUrl = url + string.Format("?id=65569D82-D047-4B0F-B1E6-A1BE4955F657&CodEntidade={1}&CodUsuario={2}&CodObjetoEstrategia={0}&origem=RD",
             codigoObjetivoEstrategico, codigoUnidadeLogada, idUsuarioLogado); 
        nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("AnFin").Target = "oe_desktop";
        
        DataSet dsParametros = cDados.getParametrosSistema("usoToDoListEstrategia", "labelLinhasAtuacao");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            string usoToDoList = dsParametros.Tables[0].Rows[0]["usoToDoListEstrategia"].ToString().ToUpper();

            if ((usoToDoList == "O") || (usoToDoList == "A"))
                nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Tdl").Visible = true;
            else
                nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Tdl").Visible = false;

            string labelPlural = dsParametros.Tables[0].Rows[0]["labelLinhasAtuacao"].ToString();
            nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Est").Text = labelPlural;
        }

        //teste Alejandro
        hfGeral.Set("urlAux", nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Asu").NavigateUrl.ToString());
    }

    private int getLarguraTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        sp_Tela.Panes[1].Size = new Unit(largura - 165);
        return largura - 140;
    }

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        //sp_Tela.Height = alturaTela - 190;
        alturaFrameAnexos = alturaTela - 230;
        return (alturaTela - 100).ToString();
    }

    private void getPermissoesConsulta()
    {
        //Procurar permissão para visualizar Ações Sugeridas.

        DataSet ds = cDados.getPermissoesDoObjetivoPelaTela(idUsuarioLogado, codigoUnidadeLogada, codigoObjetivoEstrategico, idObjetoPai, "OB", "OB_CnsAcnSug", "OB_CnsAnl", "OB_CnsAnx", "OB_CnsEtg", "OB_EnvMsg", "OB_CnsMsg", "OB_CnsPlnAcn", "OB_AnlAcnSug", "OB_AcsPnlInd", "OB_AcsPnlFin");
        if (cDados.DataSetOk(ds))
        {
            podeConsultarAcnSug = int.Parse(ds.Tables[0].Rows[0]["OB_CnsAcnSug"].ToString()) > 0;
            podeConsultarAnl = int.Parse(ds.Tables[0].Rows[0]["OB_CnsAnl"].ToString()) > 0;
            podeConsultarAnx = int.Parse(ds.Tables[0].Rows[0]["OB_CnsAnx"].ToString()) > 0;
            podeConsultarEtg = int.Parse(ds.Tables[0].Rows[0]["OB_CnsEtg"].ToString()) > 0;
            podeEnviarMsg = int.Parse(ds.Tables[0].Rows[0]["OB_CnsMsg"].ToString()) > 0;
            podeConsultarMsg = int.Parse(ds.Tables[0].Rows[0]["OB_CnsMsg"].ToString()) > 0;
            podeConsultarPlnAcn = int.Parse(ds.Tables[0].Rows[0]["OB_CnsPlnAcn"].ToString()) > 0;
            podeConsultarRelatorio = int.Parse(ds.Tables[0].Rows[0]["OB_AnlAcnSug"].ToString()) > 0;
            podeConsultarAnInd = int.Parse(ds.Tables[0].Rows[0]["OB_AcsPnlInd"].ToString()) > 0;
            podeConsultarAnFin = int.Parse(ds.Tables[0].Rows[0]["OB_AcsPnlFin"].ToString()) > 0;
        }


        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Asu").Enabled = podeConsultarAcnSug;
        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Ana").Enabled = podeConsultarAnl;
        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Ane").Enabled = podeConsultarAnx;
        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Men").Enabled = podeConsultarMsg;
        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Est").Enabled = podeConsultarEtg;
        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Tdl").Enabled = podeConsultarPlnAcn;
        nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("Mel").Visible = false; // podeConsultarRelatorio;   (este relatório foi retirado para todos os clientes)
        nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("AnInd").Visible = podeConsultarAnInd;
        nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("AnFin").Visible = podeConsultarAnFin;


        if (!(podeConsultarAnInd || podeConsultarAnFin))
        {
            nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("Mel").Visible = false;
            nvbMenuProjeto.Groups.FindByName("Rel").Visible = false;
        }


        if (podeEnviarMsg)
        {
            imgMensagens.ClientEnabled = true;
        }
        else
        {
            imgMensagens.ClientEnabled = false;
            imgMensagens.ToolTip = Resources.traducao.indexResumoObjetivo_incluir_uma_nova_mensagem;
            imgMensagens.ImageUrl = "~/imagens/questaoDes.gif";
            imgMensagens.Cursor = "default";
        }
    }

    private void carregaComboUnidades()
    {
        string where = "";

        DataSet dsUnidades = cDados.getEntidadesMapasEstrategicos(idUsuarioLogado, codigoMapa, where);

        DataSet ds = cDados.getDefinicaoUnidade(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()));

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            lblEntidade.Text = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString() + ":";
        }


        if (cDados.DataSetOk(dsUnidades))
        {
            ddlUnidade.DataSource = dsUnidades;
            ddlUnidade.TextField = "SiglaUnidadeNegocio";
            ddlUnidade.ValueField = "CodigoUnidadeNegocio";

            ddlUnidade.Columns[0].FieldName = "SiglaUnidadeNegocio";
            ddlUnidade.Columns[0].Caption = Resources.traducao.iniciais + " " + lblEntidade.Text;

            ddlUnidade.Columns[1].FieldName = "NomeUnidadeNegocio";
            ddlUnidade.Columns[1].Caption = Resources.traducao.nome + " " + lblEntidade.Text;

            ddlUnidade.DataBind();

        }


        if (!IsPostBack && ddlUnidade.Items.Count > 0)
        {
            if (ddlUnidade.Items.FindByValue(codigoUnidadeMapa) == null)
                ddlUnidade.Value = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
            else
                ddlUnidade.Value = codigoUnidadeMapa;
        }
    }

    protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
    {
        sp_Tela.Panes[1].ContentUrl = "detalhesObjetivoEstrategico.aspx?CM=" + codigoMapa + "&COE=" + codigoObjetivoEstrategico + "&UN=" + codigoUnidadeSelecionada + "&UNM=" + codigoUnidadeMapa;
    }
}