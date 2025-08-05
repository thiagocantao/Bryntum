using DevExpress.DashboardWeb;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class _Processos_Visualizacao_VisualizacaoDashboard : System.Web.UI.Page
{
    private dados cDados;
    private int codigoCarteira;
    private int codigoEntidade;
    private int codigoLista;
    private int codigoListaUsuario;
    private string codigoModuloMenu;
    private int codigoUsuarioLogado;
    private string dbName;
    private string dbOwner;
    private bool indicaDadosInicializados;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        Regex regex = new Regex(@"(cmm)(=)(?<value>\w{3})");
        Match match = regex.Match(Request.Url.Query);
        codigoModuloMenu = match.Success ? match.Groups["value"].Value : "todos";

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

        indicaDadosInicializados = false;
        string ir = Request.QueryString["ir"];
        string cl = Request.QueryString["cl"];
        if (cl == null) return;

        codigoLista = int.Parse(Request.QueryString["cl"]);
        if (!int.TryParse(Request.QueryString["clu"], out codigoListaUsuario))
            codigoListaUsuario = -1;
        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        codigoUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        codigoCarteira = Convert.ToInt32(cDados.getInfoSistema("CodigoCarteira"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string url = ObtemUrlVisualizadorDashboard();
        Response.Redirect(url);
    }

    private string ObtemUrlVisualizadorDashboard()
    {
        string comandoSql = string.Format("SELECT IDDashboard FROM Lista WHERE CodigoLista = {0}", codigoLista);
        string id = cDados.getDataSet(comandoSql).Tables[0].Rows[0]["IDDashboard"].ToString();
        string queryString = string.Format("?id={0}&CodEntidade={1}&CodUsuario={2}&CodCarteira={3}&origem=RD",
            id, codigoEntidade, codigoUsuarioLogado, codigoCarteira);
        string url = string.Format("../../_dashboard/VisualizadorDashboard.aspx{0}", queryString);
        return url;
    }
}