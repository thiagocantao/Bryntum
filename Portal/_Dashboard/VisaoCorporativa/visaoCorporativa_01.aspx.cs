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

public partial class _Dashboard_VisaoCorporativa_visaoCorporativa_01 : System.Web.UI.Page
{
    dados cDados;
    
    public string larguraTela = "", alturaTela = "", larguraTabela = "", metadeAlturaTela = "", larguraGraficoBarras = "", alturaNumeros = "";

    public string grafico1 = "", grafico2 = "", grafico3 = "", grafico4 = "", grafico5 = "", grafico6 = "", numeros1 = "";
    
    public string corExcelente = "#1648DC", corSatisfatorio = "#008844", corAtencao = "#FFFF44",
                  corCritico = "#DD0100", corPrevisto = "#B1B3AC", corReal = "#486A9D";

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

        defineLarguraTela();

        DataSet dsParametro = cDados.getParametrosSistema("grafico_MeuDash_01", "grafico_MeuDash_02", "grafico_MeuDash_03", "grafico_MeuDash_04", "grafico_MeuDash_05", "grafico_MeuDash_06",
                                                          "corExcelente", "corSatisfatorio", "corAtencao", "corCritico", "corPrevisto", "corReal");

        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            grafico1 = dsParametro.Tables[0].Rows[0]["grafico_MeuDash_01"] + "" == "" ? grafico1 : dsParametro.Tables[0].Rows[0]["grafico_MeuDash_01"] + ".aspx";
            grafico2 = dsParametro.Tables[0].Rows[0]["grafico_MeuDash_02"] + "" == "" ? grafico2 : dsParametro.Tables[0].Rows[0]["grafico_MeuDash_02"] + ".aspx";
            grafico3 = dsParametro.Tables[0].Rows[0]["grafico_MeuDash_03"] + "" == "" ? grafico3 : dsParametro.Tables[0].Rows[0]["grafico_MeuDash_03"] + ".aspx";
            grafico4 = dsParametro.Tables[0].Rows[0]["grafico_MeuDash_04"] + "" == "" ? grafico4 : dsParametro.Tables[0].Rows[0]["grafico_MeuDash_04"] + ".aspx";
            grafico5 = dsParametro.Tables[0].Rows[0]["grafico_MeuDash_05"] + "" == "" ? grafico5 : dsParametro.Tables[0].Rows[0]["grafico_MeuDash_05"] + ".aspx";
            grafico6 = dsParametro.Tables[0].Rows[0]["grafico_MeuDash_06"] + "" == "" ? grafico6 : dsParametro.Tables[0].Rows[0]["grafico_MeuDash_06"] + ".aspx";

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
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = ((largura - 30) / 3).ToString() + "px";
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 159).ToString() + "px";
        metadeAlturaTela = ((altura - 159) / 2).ToString() + "px";
    }
}
