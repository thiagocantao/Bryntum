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

public partial class _Portfolios_VisaoCorporativa_vc_003 : System.Web.UI.Page
{
    dados cDados;
    public bool permissaoLink = false;

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

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        
        defineLarguraTela();

        int codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        permissaoLink = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidade, "EN_AcsPnlPtf");

        carregaGrid();        
    }

    private void defineLarguraTela()
    { 
        int largura, altura;

        if (Request.QueryString["Altura"] != null && Request.QueryString["Largura"] != null)
        {
            largura = int.Parse(Request.QueryString["Largura"].ToString());
            altura = int.Parse(Request.QueryString["Altura"].ToString());

            ASPxRoundPanel1.ContentHeight = (altura - 38);

            gvDados.Width = largura - 15;

            gvDados.Settings.VerticalScrollableHeight = altura - 43;
        }
        else
        {
            string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
            largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
            altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

            ASPxRoundPanel1.Width = ((largura - 200) / 3 - 60);
            ASPxRoundPanel1.ContentHeight = (altura - 195) / 2 - 20;

            gvDados.Width = ((largura - 200) / 3 - 75);

            gvDados.Settings.VerticalScrollableHeight = (altura - 195) / 2 - 25;
        }        
    }

    private void carregaGrid()
    {
        if (cDados.getInfoSistema("CodigoPortfolio") == null)
            cDados.setInfoSistema("CodigoPortfolio", "-1");

        DataSet ds = cDados.getTabelaDesempenhoCategoria(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), int.Parse(cDados.getInfoSistema("CodigoPortfolio").ToString()), "");

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }
}
