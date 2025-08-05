using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class _Dashboard_VisualizadorDashboardComMaster : System.Web.UI.Page
{
    private dados cDados;
    private int codigoCarteira;
    private int codigoEntidade;
    private int codigoUsuarioLogado;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();

        cDados = CdadosUtil.GetCdados(null);

        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = String.Format(
                "{0}erros/erroInatividade.aspx", cDados.getPathSistema());
            Response.End();
        }

        codigoUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        codigoCarteira = Convert.ToInt32(cDados.getInfoSistema("CodigoCarteira"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var frame = frameConteudo as HtmlControl;
        string url = "VisualizadorDashboard.aspx" + Request.Url.Query;
        url += "&CodUsuario=" + codigoUsuarioLogado;
        url += "&CodEntidade=" + codigoEntidade;
        url += "&CodCarteira=" + codigoCarteira;
        url += "&origem=EN";
        frame.Attributes["src"] = url;
    }
}