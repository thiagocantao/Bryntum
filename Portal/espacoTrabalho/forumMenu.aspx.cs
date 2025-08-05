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

public partial class espacoTrabalho_forumMenu : System.Web.UI.Page
{

    dados cDados;
    public string alturaTabela;
    public string tituloMenu = "";
    private string idUsuarioLogado;
    private string idEntidadeLogada;
    public bool podeConsultarForum = false;
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
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        idEntidadeLogada = cDados.getInfoSistema("CodigoEntidade").ToString();
        idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado").ToString();

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        alturaTabela = getAlturaTela() + "px";
               
        carregarMenuLateral();
        //lblTituloTela.Text = tituloMenu;
        //carregarMenuAntigo();
    }

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 135).ToString();
    }

    #region MENU LATERAL

    private void carregarMenuLateral()
    {

        if (cDados.VerificaPermissaoUsuario(int.Parse(idUsuarioLogado), int.Parse(idEntidadeLogada), "CONSFORUM"))
            nvbMenuForuns.Groups.FindByName("gpForuns").Items.Add(new NavBarItem("Fóruns", "itemForuns", "", "~/espacoTrabalho/foruns/frameEspacoTrabalho_ForumDiscussao.aspx?Tit=Forúns", "mapa_desktop"));

        if (cDados.VerificaPermissaoUsuario(int.Parse(idUsuarioLogado), int.Parse(idEntidadeLogada), "CONSAVS"))
            nvbMenuForuns.Groups.FindByName("gpForuns").Items.Add(new NavBarItem("Avisos", "itemAvisos", "", "~/espacoTrabalho/frameEspacoTrabalho_Avisos.aspx?Tit=Avisos", "mapa_desktop"));

        if (cDados.VerificaPermissaoUsuario(int.Parse(idUsuarioLogado), int.Parse(idEntidadeLogada), "CONSDOC"))
            nvbMenuForuns.Groups.FindByName("gpForuns").Items.Add(new NavBarItem("Biblioteca", "itemBiblioteca", "", "~/espacoTrabalho/frameEspacoTrabalho_Biblioteca.aspx?Tit=Biblioteca de Documento&TA=EN&ID=" + idEntidadeLogada.ToString(), "mapa_desktop"));

        nvbMenuForuns.Groups.FindByName("gpAvisos").Visible = false;
        nvbMenuForuns.Groups.FindByName("gpBiblioteca").Visible = false;
    }

    #endregion
}
