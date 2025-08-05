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

public partial class _VisaoNE_VisaoExecutiva_visaoCorporativa : System.Web.UI.Page
{
    dados cDados;
    
    public string larguraTela = "", larguraTela2 = "", alturaTela = "", larguraTabela = "", metadeAlturaTela = "", larguraGraficoBarras = "", alturaNumeros = "";

    public string grafico1 = "", grafico2 = "", grafico3 = "", grafico4 = "", numeros1 = "";
    
    public string corExcelente = "#1648DC", corSatisfatorio = "#008844", corAtencao = "#FFFF44",
                  corCritico = "#DD0100", corPrevisto = "#B1B3AC", corReal = "#486A9D";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        imgMostrarNumeros.Style.Add("cursor", "pointer");

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

        defineLarguraTela();

        grafico1 = "vne_001.aspx";
        grafico2 = "vne_002.aspx";
        grafico3 = "vne_003.aspx";
        grafico4 = "vne_004.aspx";

        DataSet dsParametro = cDados.getParametrosSistema("corExcelente", "corSatisfatorio", "corAtencao", "corCritico", "corPrevisto", "corReal");

        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            corExcelente = dsParametro.Tables[0].Rows[0]["corExcelente"] + "" == "" ? corExcelente : dsParametro.Tables[0].Rows[0]["corExcelente"].ToString();
            corSatisfatorio = dsParametro.Tables[0].Rows[0]["corSatisfatorio"] + "" == "" ? corSatisfatorio : dsParametro.Tables[0].Rows[0]["corSatisfatorio"].ToString();
            corAtencao = dsParametro.Tables[0].Rows[0]["corAtencao"] + "" == "" ? corAtencao : dsParametro.Tables[0].Rows[0]["corAtencao"].ToString();
            corCritico = dsParametro.Tables[0].Rows[0]["corCritico"] + "" == "" ? corCritico : dsParametro.Tables[0].Rows[0]["corCritico"].ToString();
            corPrevisto = dsParametro.Tables[0].Rows[0]["corPrevisto"] + "" == "" ? corPrevisto : dsParametro.Tables[0].Rows[0]["corPrevisto"].ToString();
            corReal = dsParametro.Tables[0].Rows[0]["corReal"] + "" == "" ? corReal : dsParametro.Tables[0].Rows[0]["corReal"].ToString();
        }

    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x'))) - 40;
        larguraTela = ((largura - 175) / 2).ToString() + "px";
        larguraTela2 = ((largura - 295) / 2).ToString() + "px";
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 190).ToString() + "px";
        metadeAlturaTela = ((altura - 180) / 2).ToString() + "px";
        alturaNumeros = (altura - 160).ToString() + "px";
    }
}
