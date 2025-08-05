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

public partial class _Estrategias_wizard_menuMapa_OpcoesMapa : System.Web.UI.Page
{
    dados cDados;

    public string alturaTabela;

    private int idUsuarioLogado;
    private int idEntidadeLogada;
    private int codigoMapaDaEmpresa = -1;

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

        idEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(this);
        }
        alturaTabela = getAlturaTela() + "px";
        carregarMenuLateral();
    }

    #region VARIOS

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 135).ToString();
    }

    #endregion

    #region MENU LATERAL

    private void carregarMenuLateral()
    {
        NavBarItem itemMenuPerspectiva = new NavBarItem(Resources.traducao.perspectivas, "itemMenuPerspectiva", "", "~/_Estrategias/wizard/MenuMapa/PerspectivasMapa.aspx?Tit="+ Resources.traducao.perspectivas + "&CM=" + codigoMapaDaEmpresa, "mapa_desktop");
        NavBarItem itemMenuTema = new NavBarItem(Resources.traducao.temas, "itemMenuTema", "", "~/_Estrategias/wizard/MenuMapa/TemasMapa.aspx?Tit="+ Resources.traducao.temas + "&CM=" + codigoMapaDaEmpresa, "mapa_desktop");
        NavBarItem itemMenuObjetivo = new NavBarItem(Resources.traducao.objetivos, "itemMenuObjetivo", "", "~/_Estrategias/wizard/MenuMapa/ObjetivosEstrategicosMapa.aspx?Tit="+ Resources.traducao.objetivos + "&CM=" + codigoMapaDaEmpresa, "mapa_desktop");

        nvbMenuMapa.Groups.FindByName("gpMenuMapa").ClientVisible = true;

        nvbMenuMapa.Groups.FindByName("gpMenuMapa").Items.Add(itemMenuPerspectiva);
        nvbMenuMapa.Groups.FindByName("gpMenuMapa").Items.Add(itemMenuTema);
        nvbMenuMapa.Groups.FindByName("gpMenuMapa").Items.Add(itemMenuObjetivo);

        nvbMenuMapa.ClientSideEvents.ItemClick = "function(s, e){ window.parent.lblTituloTela.SetText('" + Server.UrlDecode(Request.QueryString["Titulo"]) + " - ' + e.item.GetText()); }";
    }

    #endregion
}
