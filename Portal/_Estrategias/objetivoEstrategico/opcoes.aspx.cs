/*OBSERVAÇÕES
 * 
 * MUDANÇÃS
 * 
 * 24/03/2011 :: Alejandro : Adaptação para permissões de envio de mensagem 
 *                           [OB_CnsAcnSug], [OB_CnsAnl], [OB_CnsAnx], [OB_CnsEtg], [OB_EnvMsg], [OB_EnvMsg]
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

public partial class _Estrategias_objetivoEstrategico_opcoes : System.Web.UI.Page
{
    //menu lateral de opções do objetivo estrategico
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
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
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
            hfGeral.Set("hfCodigoObjetivo",codigoObjetivoEstrategico);
        }

        if (Request.QueryString["UN"] != null && Request.QueryString["UN"].ToString() != "")
            codigoUnidadeSelecionada = int.Parse(Request.QueryString["UN"].ToString());
        if (Request.QueryString["CM"] != null && Request.QueryString["CM"].ToString() != "")
            codigoMapa = int.Parse(Request.QueryString["CM"].ToString());
        if (Request.QueryString["UNM"] != null && Request.QueryString["UNM"].ToString() != "")
            codigoUnidadeMapa = int.Parse(Request.QueryString["UNM"].ToString());

        codigoUnidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        alturaTabela = getAlturaTela() + "px";

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
        }

        defineTelaInicial();

        DataTable dt = cDados.getParametrosSistema("labelAcoesSugeridasEstrategia").Tables[0];
        if (dt.Rows.Count > 0)
        {
            nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Asu").Text = dt.Rows[0]["labelAcoesSugeridasEstrategia"].ToString();
        }
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

        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Ane").NavigateUrl = "../../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?TA=OB&ID=" + codigoObjetivoEstrategico + "&UN=" + codigoUnidadeSelecionada + "&UNM=" + codigoUnidadeMapa;
        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Ane").Target = "oe_desktop";
        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Men").NavigateUrl = "~/Mensagens/MensagensPainelBordo.aspx?CO=" + codigoObjetivoEstrategico + "&TA=OB&UN=" + codigoUnidadeSelecionada + "&UNM=" + codigoUnidadeMapa;
        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Men").Target = "oe_desktop";

        nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("Mel").NavigateUrl = "analiseMelhoresPraticas.aspx?CM=" + codigoMapa + "&COE=" + codigoObjetivoEstrategico + "&TA=OB&UN=" + codigoUnidadeSelecionada + "&UNM=" + codigoUnidadeMapa;
        nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("Mel").Target = "oe_desktop";
        nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("AnaliseIni").NavigateUrl = "relAnaliseIniciativas.aspx?CM=" + codigoMapa + "&COE=" + codigoObjetivoEstrategico + "&TA=OB&UN=" + codigoUnidadeSelecionada + "&UNM=" + codigoUnidadeMapa;
        nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("AnaliseIni").Target = "oe_desktop";

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

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 140).ToString();
    }

    private void getPermissoesConsulta()
    {
        //Procurar permissão para visualizar Ações Sugeridas.
        DataSet ds = cDados.getPermissoesDoObjetivoPelaTela(idUsuarioLogado, codigoUnidadeLogada, codigoObjetivoEstrategico, idObjetoPai, "OB", "OB_CnsAcnSug", "OB_CnsAnl", "OB_CnsAnx", "OB_CnsEtg", "OB_EnvMsg", "OB_CnsMsg", "OB_CnsPlnAcn", "OB_AnlAcnSug");
        if (cDados.DataSetOk(ds))                                                                                                                                            
        {
            podeConsultarAcnSug = int.Parse(ds.Tables[0].Rows[0]["OB_CnsAcnSug"].ToString()) > 0;
            podeConsultarAnl = int.Parse(ds.Tables[0].Rows[0]["OB_CnsAnl"].ToString()) > 0;
            podeConsultarAnx = int.Parse(ds.Tables[0].Rows[0]["OB_CnsAnx"].ToString()) > 0;
            podeConsultarEtg = int.Parse(ds.Tables[0].Rows[0]["OB_CnsEtg"].ToString()) > 0;
            podeEnviarMsg = int.Parse(ds.Tables[0].Rows[0]["OB_CnsMsg"].ToString()) > 0;
            podeConsultarMsg =int.Parse(ds.Tables[0].Rows[0]["OB_CnsMsg"].ToString()) > 0;
            podeConsultarPlnAcn = int.Parse(ds.Tables[0].Rows[0]["OB_CnsPlnAcn"].ToString()) > 0;
            podeConsultarRelatorio = int.Parse(ds.Tables[0].Rows[0]["OB_AnlAcnSug"].ToString()) > 0;
        }

        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Asu").Enabled = podeConsultarAcnSug;
        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Ana").Enabled = podeConsultarAnl;
        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Ane").Enabled = podeConsultarAnx;
        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Men").Enabled = podeConsultarMsg;
        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Est").Enabled = podeConsultarEtg;
        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Tdl").Enabled = podeConsultarPlnAcn;
        nvbMenuProjeto.Groups.FindByName("Rel").Items.FindByName("Mel").Enabled = podeConsultarRelatorio;

        if (podeEnviarMsg)
        {
            imgMensagens.ClientEnabled = true;
        }
        else
        {
            imgMensagens.ClientEnabled = false;
            imgMensagens.ToolTip = "Incluir uma nova mensagem";
            imgMensagens.ImageUrl = "~/imagens/questaoDes.gif";
            imgMensagens.Cursor = "default";
        }
    }
}