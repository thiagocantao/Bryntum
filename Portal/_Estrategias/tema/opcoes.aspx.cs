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

public partial class _Estrategias_wizard_opcoes : System.Web.UI.Page
{
    dados cDados;
    public string alturaTabela;

    private int codigoTema = 0;
    private int codigoUnidadeSelecionada = 0, codigoUnidadeLogada = 0, codigoUnidadeMapa = 0, codigoMapa = 0;
    private int idUsuarioLogado = 0;
    
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

        if (Request.QueryString["CT"] != null && Request.QueryString["CT"].ToString() != "")
        {
            codigoTema = int.Parse(Request.QueryString["CT"].ToString());
            hfGeral.Set("hfCodigoTema",codigoTema);
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

        if (codigoUnidadeLogada != codigoUnidadeSelecionada)
        {
            nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Tdl").Enabled = false;
            nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Men").Enabled = false;
            nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Ane").Enabled = false;
        }

        defineTelaInicial();
        
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
        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Det").NavigateUrl = "detalhesTema.aspx?CM=" + codigoMapa + "&CT=" + codigoTema + "&UN=" + codigoUnidadeSelecionada + "&UNM=" + codigoUnidadeMapa;
        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Det").Target = "oe_desktop";
        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Tdl").NavigateUrl = "tema_ToDoList.aspx?CM=" + codigoMapa + "&CT=" + codigoTema + "&UN=" + codigoUnidadeSelecionada + "&UNM=" + codigoUnidadeMapa;
        nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Tdl").Target = "oe_desktop";

        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Ane").NavigateUrl = "../../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?TA=TM&ID=" + codigoTema + "&UN=" + codigoUnidadeSelecionada + "&UNM=" + codigoUnidadeMapa;
        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Ane").Target = "oe_desktop";
        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Men").NavigateUrl = "mensagensTema.aspx?CM=" + codigoMapa + "&CT=" + codigoTema + "&TA=OB&UN=" + codigoUnidadeSelecionada + "&UNM=" + codigoUnidadeMapa;
        nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Men").Target = "oe_desktop";

        DataSet dsParametros = cDados.getParametrosSistema("usoToDoListEstrategia");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            string usoToDoList = dsParametros.Tables[0].Rows[0]["usoToDoListEstrategia"].ToString().ToUpper();

            if ((usoToDoList == "O") || (usoToDoList == "A"))
                nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Tdl").Visible = true;
            else
                nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Tdl").Visible = false;
        }
    }

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 140).ToString();
    }

    private void getPermissoesConsulta()
    {
        ////Procurar permissão para visualizar Ações Sugeridas.
        //DataSet ds = cDados.getPermissoesDoObjetivoPelaTela(idUsuarioLogado, codigoUnidadeLogada, codigoTema, idObjetoPai, "OB", "OB_CnsAnx", "OB_EnvMsg", "OB_CnsMsg", "OB_CnsPlnAcn");
        //if (cDados.DataSetOk(ds))                                                                                                                                            
        //{
        //    podeConsultarAnx = int.Parse(ds.Tables[0].Rows[0]["OB_CnsAnx"].ToString()) > 0;
        //    podeEnviarMsg = int.Parse(ds.Tables[0].Rows[0]["OB_CnsMsg"].ToString()) > 0;
        //    podeConsultarMsg =int.Parse(ds.Tables[0].Rows[0]["OB_CnsMsg"].ToString()) > 0;
        //    podeConsultarPlnAcn = int.Parse(ds.Tables[0].Rows[0]["OB_CnsPlnAcn"].ToString()) > 0;
            
        //}

        //nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Ane").Enabled = podeConsultarAnx;
        //nvbMenuProjeto.Groups.FindByName("Com").Items.FindByName("Men").Enabled = podeConsultarMsg;
        //nvbMenuProjeto.Groups.FindByName("Moe").Items.FindByName("Tdl").Enabled = podeConsultarPlnAcn;

        //if (podeEnviarMsg)
        //{
        //    imgMensagens.ClientEnabled = true;
        //}
        //else
        //{
        //    imgMensagens.ClientEnabled = false;
        //    imgMensagens.ToolTip = "Incluir uma nova mensagem";
        //    imgMensagens.ImageUrl = "~/imagens/questaoDes.gif";
        //    imgMensagens.Cursor = "default";
        //}
    }
}