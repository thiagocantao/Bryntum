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

public partial class _Portfolios_Administracao_opcoes : System.Web.UI.Page
{
    dados cDados;
    private string idUsuarioLogado;
    private string idEntidadeLogada;
    public string alturaTabela;

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

        idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado").ToString();
        idEntidadeLogada = cDados.getInfoSistema("CodigoEntidade").ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        alturaTabela = getAlturaTela() + "px";
        carregarMenuLateral();
    }

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 135).ToString();
    }

    private void carregarMenuLateral()
    {
        nvbMenuPortfolio.Groups.FindByName("gpPortfolio").Items.Add(new NavBarItem("Manutenção de Portfólio", "opManutencaoPortfolio", "", "~/_Portfolios/Administracao/portfolios.aspx?Tit=" + "Manutenção de Portfólio", "portfolio_desktop"));
        nvbMenuPortfolio.Groups.FindByName("gpPortfolio").Items.Add(new NavBarItem("Critérios de Seleção", "opCriterioSelecao", "", "~/_Portfolios/Administracao/criteriosSelecaoNova.aspx?Tit=" + "Critérios de Seleção", "portfolio_desktop"));
        nvbMenuPortfolio.Groups.FindByName("gpPortfolio").Items.Add(new NavBarItem("Riscos Padrões", "opRiscoPadroes", "", "~/_Portfolios/Administracao/riscosPadroes.aspx?Tit=" + "Riscos Padrões", "portfolio_desktop"));
        nvbMenuPortfolio.Groups.FindByName("gpPortfolio").Items.Add(new NavBarItem("Categorias", "opCategorias", "", "~/_Portfolios/Administracao/categoriasNova.aspx?Tit=" + "Categorias", "portfolio_desktop"));
        nvbMenuPortfolio.Groups.FindByName("gpPortfolio").Items.Add(new NavBarItem("Configuração", "opConfiguracao", "", "~/_Portfolios/Administracao/Configuracoes.aspx?Tit=" + "Configuração", "portfolio_desktop"));
    }
}
