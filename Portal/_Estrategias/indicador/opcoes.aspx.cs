/*OBSERVAÇÕES
 * 
 * MUDANÇÃS
 * 
 * 22/03/2011 :: Alejandro : Adaptação para permissões de envio de mensagem [IN_EnvMsg].
 * 30/03/2011 :: Alejandro : Atualização de permissões [IN_IncMsg]
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

public partial class _Estrategias_indicador_opcoes : System.Web.UI.Page
{
    dados cDados;
    public string alturaTabela;
    
    private int codigoIndicador = 0;
    private int codigoUnidadeSelecionada = 0;
    private int idUsuarioLogado = 0;
    private int codigoUnidadeMapa = 0;
    private int codigoUnidadeLogada = 0;
    private int codigoObjetoEstrategia = 0;
    private int codigoMapa = 0;
    private int idObjetoPai = 0;
    public bool podeEnviarMsg = false;
    public bool podeConsultarMsg = false;
    public bool podeCnsDtl = false;
    public bool podeCnsAnl = false;
    public bool podeCnsPlnAcn = false;
    public bool podeCnsAtlMet = false;
    public bool podeCnsAtlRes = false;
    public bool podeCnsAnx = false;
    public bool podeAnlDad = false;
    public bool podeAnlUnd = false;
    private int alturaFrameAnexos = 650;

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
            cDados.aplicaEstiloVisual(Page);
        }
        if (cDados.getInfoSistema("COE") != null)
            codigoObjetoEstrategia = int.Parse(cDados.getInfoSistema("COE").ToString());

        if (Request.QueryString["UNM"] != null && Request.QueryString["UNM"].ToString() != "")
            codigoUnidadeMapa = int.Parse(Request.QueryString["UNM"].ToString());

        if (Request.QueryString["CM"] != null && Request.QueryString["CM"].ToString() != "")
            codigoMapa = int.Parse(Request.QueryString["CM"].ToString());

        codigoIndicador = int.Parse(cDados.getInfoSistema("COIN").ToString());
        hfGeral.Set("CodigoIndicador", codigoIndicador);

        codigoUnidadeSelecionada = int.Parse(cDados.getInfoSistema("CodigoUnidade").ToString());
        codigoUnidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        if(cDados.getInfoSistema("CodigoMapa") != null)
        {
            codigoMapa = int.Parse(cDados.getInfoSistema("CodigoMapa").ToString());
        }
        if (cDados.getInfoSistema("UNM") != null)
        {
            codigoUnidadeMapa = int.Parse(cDados.getInfoSistema("UNM").ToString());
        }

        if (codigoMapa == 0)
        {
            idObjetoPai = codigoUnidadeSelecionada;
        }
        else
        {
            idObjetoPai = codigoMapa * (-1);
        }

        alturaTabela = getAlturaTela() + "px";

        DataSet dsParametros = cDados.getParametrosSistema(codigoUnidadeLogada, "usaUnidadesFederacao", "expandirTodoMenu", "usoToDoListEstrategia");

        string telaInicial = "";

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["usaUnidadesFederacao"].ToString() == "N")
        {
            telaInicial = "./resumoIndicador2.aspx";
        }
        else
        {
            telaInicial = "./resumoIndicador.aspx";
        }
        
        nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Res").NavigateUrl = telaInicial;
        nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Res").Target = "indicador_desktop";

        nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Ben").NavigateUrl = "benchmarkingIndicador.aspx";
        nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Ben").Target = "indicador_desktop";

        nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Ana").NavigateUrl = "analises.aspx?COIN=" + codigoIndicador + "&CM=" + codigoMapa + "&COE=" + codigoObjetoEstrategia + "&UNM=" + codigoUnidadeMapa;
        nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Ana").Target = "indicador_desktop";

        nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Tdl").NavigateUrl = "IndicadorEstrategia_ToDoList.aspx?COIN=" + codigoIndicador;
        nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Tdl").Target = "indicador_desktop";

        nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Atm").NavigateUrl = "editaMetas.aspx?COIN=" + codigoIndicador;
        nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Atm").Target = "indicador_desktop";

        nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Atr").NavigateUrl = "editaResultados.aspx?COIN=" + codigoIndicador;
        nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Atr").Target = "indicador_desktop";

        nvbMenuProjeto.Groups.FindByName("Ind").Expanded = true;

        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Anex").NavigateUrl = "../../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?TA=IN&ID=" + codigoIndicador + "&IDOP=" + codigoMapa * (-1) + "&ALT=" + alturaFrameAnexos + "&Frame=S";
        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Anex").Target = "indicador_desktop";

        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Men").NavigateUrl = "~/Mensagens/MensagensPainelBordo.aspx?CO=" + codigoIndicador + "&TA=IN&MostrarTitulo=S";
        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Men").Target = "indicador_desktop";

        nvbMenuProjeto.Groups.FindByName("Con").Items.FindByName("Ddi").NavigateUrl = string.Format("composicaoIndicador.aspx?COIN={0}&UNM={1}", codigoIndicador, codigoUnidadeMapa);
        nvbMenuProjeto.Groups.FindByName("Con").Items.FindByName("Ddi").Target = "indicador_desktop";

        nvbMenuProjeto.Groups.FindByName("Con").Items.FindByName("Udi").NavigateUrl = string.Format("OLAP_unidadesIndicador.aspx?COIN={0}&UNM={1}", codigoIndicador, codigoUnidadeMapa);
        nvbMenuProjeto.Groups.FindByName("Con").Items.FindByName("Udi").Target = "indicador_desktop";


        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            nvbMenuProjeto.AutoCollapse = dsParametros.Tables[0].Rows[0]["expandirTodoMenu"].ToString() == "N";
            string usoToDoList = dsParametros.Tables[0].Rows[0]["usoToDoListEstrategia"].ToString().ToUpper();

            if ((usoToDoList == "I") || (usoToDoList == "A"))
                nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Tdl").Visible = true;
            else
                nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Tdl").Visible = false;
        }
        getPermissoesConsulta();
        defineTelaInicial();

        string sql = string.Format(@"SELECT CodigoUnidadeNegocio FROM IndicadorUnidade WHERE CodigoIndicador = {0} AND IndicaUnidadeCriadoraIndicador = 'S'", codigoIndicador);

        DataSet dsUnidade = cDados.getDataSet(sql);

        int codigoUnidadeCriadora = 0;

        if (cDados.DataTableOk(dsUnidade.Tables[0]))
            codigoUnidadeCriadora = int.Parse(dsUnidade.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString());


        if (codigoUnidadeCriadora != codigoUnidadeSelecionada)
        {
            nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Ana").ClientEnabled = false;
            nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Tdl").ClientEnabled = false;
            nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Atm").ClientEnabled = false;
            nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Atr").ClientEnabled = false;
            nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Anex").ClientEnabled = false;
            nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Men").ClientEnabled = false;
        }
        else
        {
            nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Ana").ClientEnabled = podeCnsAnl;
            nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Tdl").ClientEnabled = podeCnsPlnAcn;
            nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Atm").ClientEnabled = podeCnsAtlMet;
            nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Atr").ClientEnabled = podeCnsAtlRes;
            nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Anex").ClientEnabled = podeCnsAnx;
            nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Men").ClientEnabled = podeConsultarMsg;
        }
    }

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        alturaFrameAnexos = alturaTela - 195;
        return (alturaTela - 150).ToString();
    }

    private void getPermissoesConsulta()
    {
        DataSet ds = cDados.getPermissoesDoObjetivoPelaTela(idUsuarioLogado, codigoUnidadeLogada, codigoIndicador, idObjetoPai, "IN", "IN_CnsMsg", "IN_IncMsg", "IN_CnsDtl", "IN_CnsAnl", "IN_CnsPlnAcn", "IN_CnsAnx", "IN_AnlDad", "IN_AnlUnd", "IN_CnsMta", "IN_VerRes");
        if (cDados.DataSetOk(ds))
        {

            podeConsultarMsg = int.Parse(ds.Tables[0].Rows[0]["IN_CnsMsg"].ToString()) > 0;
            podeEnviarMsg = int.Parse(ds.Tables[0].Rows[0]["IN_IncMsg"].ToString()) > 0;
            podeCnsDtl = int.Parse(ds.Tables[0].Rows[0]["IN_CnsDtl"].ToString()) > 0;
            podeCnsAnl = int.Parse(ds.Tables[0].Rows[0]["IN_CnsAnl"].ToString()) > 0;
            podeCnsPlnAcn = int.Parse(ds.Tables[0].Rows[0]["IN_CnsPlnAcn"].ToString()) > 0;
            podeCnsAtlMet = int.Parse(ds.Tables[0].Rows[0]["IN_CnsMta"].ToString()) > 0;
            podeCnsAtlRes = int.Parse(ds.Tables[0].Rows[0]["IN_VerRes"].ToString()) > 0;
            podeCnsAnx = int.Parse(ds.Tables[0].Rows[0]["IN_CnsAnx"].ToString()) > 0;
            podeAnlDad = int.Parse(ds.Tables[0].Rows[0]["IN_AnlDad"].ToString()) > 0;
            podeAnlUnd = int.Parse(ds.Tables[0].Rows[0]["IN_AnlUnd"].ToString()) > 0;
        }

        nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Res").ClientEnabled = podeCnsDtl;
        nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Ana").ClientEnabled = podeCnsAnl;
        nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Tdl").ClientEnabled = podeCnsPlnAcn;
        nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Atm").ClientEnabled = podeCnsAtlMet;
        nvbMenuProjeto.Groups.FindByName("Ind").Items.FindByName("Atr").ClientEnabled = podeCnsAtlRes;
        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Anex").ClientEnabled = podeCnsAnx;
        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Men").ClientEnabled = podeConsultarMsg;
        nvbMenuProjeto.Groups.FindByName("Con").Items.FindByName("Ddi").ClientEnabled = podeAnlDad;
        nvbMenuProjeto.Groups.FindByName("Con").Items.FindByName("Udi").ClientEnabled = podeAnlUnd;

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
}
