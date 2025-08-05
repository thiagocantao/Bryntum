using DevExpress.Web;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class _Dashboard_VisualizacaoDashboardPainelDinamico : System.Web.UI.Page
{
    private string dashboardId;
    private dados cDados;
    private ASPxLabel lblTitulo;

    protected void Page_Init(object sender, EventArgs e)
    {
        ASPxWebControl.SetIECompatibilityModeEdge();
        OrderedDictionary listaParametrosDados = new OrderedDictionary();
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
        dashboardId = Request.QueryString["id"];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        defineLarguraTela();
        (frameDashboard as HtmlControl).Attributes["src"] =
            string.Format("VisualizadorDashboard.aspx?id={0}", dashboardId);
        DefiniTituloLabel();
    }

    private void DefiniTituloLabel()
    {
        string camandoSql = string.Format("SELECT TituloPortlet FROM Portlet WHERE IDDashboard = '{0}'", dashboardId);
        DataSet ds = cDados.getDataSet(camandoSql);
        string tituloPortlet = ds.Tables[0].Rows[0]["TituloPortlet"] as string;
        lblTitulo.Text = tituloPortlet;
    }

    private void defineLarguraTela()
    {
        int largura, altura;

        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

        if (Request.QueryString["Altura"] != null && Request.QueryString["Largura"] != null)
        {
            largura = int.Parse(Request.QueryString["Largura"]);
            altura = int.Parse(Request.QueryString["Altura"]);

            roundPanel.Width = (largura - 10);
            roundPanel.ContentHeight = altura - 35;
        }
        else
        {
            roundPanel.Width = ((largura - 30) / 3);
            roundPanel.ContentHeight = (altura - 250) / 2;
        }

    }

    protected void lblTitulo_Init(object sender, EventArgs e)
    {
        lblTitulo = (ASPxLabel)sender;
    }
}