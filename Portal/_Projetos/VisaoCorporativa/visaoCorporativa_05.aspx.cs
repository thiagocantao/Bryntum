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

    public string larguraTela = "", larguraTelaGrande = "", alturaTela = "", larguraTabela = "", metadeAlturaTela = "", larguraGraficoBarras = "", alturaNumeros = "";

    public string grafico1 = "", grafico2 = "", grafico3 = "", grafico4 = "", numeros1 = "";

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

        DataSet dsParametro = cDados.getParametrosSistema("vc_grafico001", "vc_grafico002", "vc_grafico003", "vc_grafico004", "mostraNumerosProjetoPorDefault");

        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            grafico1 = dsParametro.Tables[0].Rows[0]["vc_grafico001"] + "" == "" ? grafico1 : dsParametro.Tables[0].Rows[0]["vc_grafico001"] + ".aspx?Largura=" + larguraGraficoPequeno + "&Altura=" + alturaGrafico + "&" + Request.QueryString;
            grafico2 = dsParametro.Tables[0].Rows[0]["vc_grafico002"] + "" == "" ? grafico2 : dsParametro.Tables[0].Rows[0]["vc_grafico002"] + ".aspx?Largura=" + larguraGraficoGrande + "&Altura=" + alturaGrafico + "&" + Request.QueryString;
            grafico3 = dsParametro.Tables[0].Rows[0]["vc_grafico003"] + "" == "" ? grafico3 : dsParametro.Tables[0].Rows[0]["vc_grafico003"] + ".aspx?Largura=" + larguraGraficoGrande + "&Altura=" + alturaGrafico + "&" + Request.QueryString;
            grafico4 = dsParametro.Tables[0].Rows[0]["vc_grafico004"] + "" == "" ? grafico4 : dsParametro.Tables[0].Rows[0]["vc_grafico004"] + ".aspx?Largura=" + larguraGraficoPequeno + "&Altura=" + alturaGrafico + "&" + Request.QueryString;

            numeros1 = "numeros_001.aspx?" + Request.QueryString;
            
            imgMostrarNumeros.JSProperties["cp_mostraDefault"] = dsParametro.Tables[0].Rows[0]["mostraNumerosProjetoPorDefault"] + "" == "" ? "S" : dsParametro.Tables[0].Rows[0]["mostraNumerosProjetoPorDefault"].ToString();
        }
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = ((largura - 250) / 3).ToString() + "px";
        larguraTelaGrande = ((largura - 250) / 3 * 2).ToString() + "px";  
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 210).ToString() + "px";
        metadeAlturaTela = ((altura - 210) / 2).ToString() + "px";
        alturaNumeros = (altura - 180).ToString() + "px";

        alturaGrafico = ((altura - 210) / 2).ToString();
        larguraGraficoPequeno = ((largura - 260) / 3).ToString();
        larguraGraficoGrande = ((largura - 255) / 3 * 2).ToString();
    }
}
