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

public partial class _Portfolios_visaoCorporativa_01 : System.Web.UI.Page
{
    dados cDados;
    
    public string larguraTela = "", alturaTela = "", larguraTabela = "", metadeAlturaTela = "", larguraGraficoBarras = "", alturaNumeros = "";

    public string grafico1 = "", grafico2 = "", grafico3 = "", grafico4 = "", grafico5 = "", grafico6 = "", numeros1 = "";
    
    public string corExcelente = "#1648DC", corSatisfatorio = "#008844", corAtencao = "#FFFF44",
                  corCritico = "#DD0100", corPrevisto = "#B1B3AC", corReal = "#486A9D";

    string larguraGraficoPequeno = "", larguraGraficoGrande = "", alturaGrafico = "";

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

        imgMostrarNumeros.JSProperties["cp_mostraDefault"] = "S";

        DataSet dsParametro = cDados.getParametrosSistema("vc_grafico001", "vc_grafico002", "vc_grafico003", "vc_grafico004", "vc_grafico005", "vc_grafico006",
                                                          "corExcelente", "corSatisfatorio", "corAtencao", "corCritico", "corPrevisto", "corReal", "mostraNumerosProjetoPorDefault");

        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            grafico1 = dsParametro.Tables[0].Rows[0]["vc_grafico001"] + "" == "" ? grafico1 : dsParametro.Tables[0].Rows[0]["vc_grafico001"] + ".aspx?Largura=" + larguraGraficoPequeno + "&Altura=" + alturaGrafico + "&" + Request.QueryString;
            grafico2 = dsParametro.Tables[0].Rows[0]["vc_grafico002"] + "" == "" ? grafico2 : dsParametro.Tables[0].Rows[0]["vc_grafico002"] + ".aspx?Largura=" + larguraGraficoPequeno + "&Altura=" + alturaGrafico + "&" + Request.QueryString;
            grafico3 = dsParametro.Tables[0].Rows[0]["vc_grafico003"] + "" == "" ? grafico3 : dsParametro.Tables[0].Rows[0]["vc_grafico003"] + ".aspx?Largura=" + larguraGraficoPequeno + "&Altura=" + alturaGrafico + "&" + Request.QueryString;
            grafico4 = dsParametro.Tables[0].Rows[0]["vc_grafico004"] + "" == "" ? grafico4 : dsParametro.Tables[0].Rows[0]["vc_grafico004"] + ".aspx?Largura=" + larguraGraficoPequeno + "&Altura=" + alturaGrafico + "&" + Request.QueryString;
            grafico5 = dsParametro.Tables[0].Rows[0]["vc_grafico005"] + "" == "" ? grafico5 : dsParametro.Tables[0].Rows[0]["vc_grafico005"] + ".aspx?Largura=" + larguraGraficoPequeno + "&Altura=" + alturaGrafico + "&" + Request.QueryString;
            grafico6 = dsParametro.Tables[0].Rows[0]["vc_grafico006"] + "" == "" ? grafico6 : dsParametro.Tables[0].Rows[0]["vc_grafico006"] + ".aspx?Largura=" + larguraGraficoPequeno + "&Altura=" + alturaGrafico + "&" + Request.QueryString;

            numeros1 = "numeros_001.aspx?" + Request.QueryString;

            corExcelente = dsParametro.Tables[0].Rows[0]["corExcelente"] + "" == "" ? corExcelente : dsParametro.Tables[0].Rows[0]["corExcelente"].ToString();
            corSatisfatorio = dsParametro.Tables[0].Rows[0]["corSatisfatorio"] + "" == "" ? corSatisfatorio : dsParametro.Tables[0].Rows[0]["corSatisfatorio"].ToString();
            corAtencao = dsParametro.Tables[0].Rows[0]["corAtencao"] + "" == "" ? corAtencao : dsParametro.Tables[0].Rows[0]["corAtencao"].ToString();
            corCritico = dsParametro.Tables[0].Rows[0]["corCritico"] + "" == "" ? corCritico : dsParametro.Tables[0].Rows[0]["corCritico"].ToString();
            corPrevisto = dsParametro.Tables[0].Rows[0]["corPrevisto"] + "" == "" ? corPrevisto : dsParametro.Tables[0].Rows[0]["corPrevisto"].ToString();
            corReal = dsParametro.Tables[0].Rows[0]["corReal"] + "" == "" ? corReal : dsParametro.Tables[0].Rows[0]["corReal"].ToString();

            imgMostrarNumeros.JSProperties["cp_mostraDefault"] = dsParametro.Tables[0].Rows[0]["mostraNumerosProjetoPorDefault"] + "" == "" ? "S" : dsParametro.Tables[0].Rows[0]["mostraNumerosProjetoPorDefault"].ToString();
        }
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = ((largura - 250) / 3).ToString() + "px";
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 210).ToString() + "px";
        metadeAlturaTela = ((altura - 210) / 2).ToString() + "px";
        alturaNumeros = (altura - 217).ToString() + "px";

        alturaGrafico = ((altura - 210) / 2).ToString();
        larguraGraficoPequeno = ((largura - 260) / 3).ToString();
        larguraGraficoGrande = ((largura - 255) / 3 * 2).ToString();
    }
}
