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

public partial class _VisaoExecutiva_Graficos_visaoCorporativa_01 : System.Web.UI.Page
{
    dados cDados;
    
    public string larguraTela = "", alturaTela = "", larguraTabela = "", metadeAlturaTela = "", larguraGraficoBarras = "", alturaNumeros = "";

    public string grafico1 = "", grafico2 = "", grafico3 = "", grafico4 = "";
    
    public string corExcelente = "#1648DC", corSatisfatorio = "#008844", corAtencao = "#FFFF44",
                  corCritico = "#DD0100";


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

        DataSet dsParametro = cDados.getParametrosSistema("ex_grafico001", "ex_grafico002", "ex_grafico003", "ex_grafico004",
                                                          "corExcelente", "corSatisfatorio", "corAtencao", "corCritico");

        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            grafico1 = dsParametro.Tables[0].Rows[0]["ex_grafico001"] + "" == "" ? grafico1 : dsParametro.Tables[0].Rows[0]["ex_grafico001"] + ".aspx";
            grafico2 = dsParametro.Tables[0].Rows[0]["ex_grafico002"] + "" == "" ? grafico2 : dsParametro.Tables[0].Rows[0]["ex_grafico002"] + ".aspx";
            grafico3 = dsParametro.Tables[0].Rows[0]["ex_grafico003"] + "" == "" ? grafico3 : dsParametro.Tables[0].Rows[0]["ex_grafico003"] + ".aspx";
            grafico4 = dsParametro.Tables[0].Rows[0]["ex_grafico004"] + "" == "" ? grafico4 : dsParametro.Tables[0].Rows[0]["ex_grafico004"] + ".aspx";
            
            corExcelente = dsParametro.Tables[0].Rows[0]["corExcelente"] + "" == "" ? corExcelente : dsParametro.Tables[0].Rows[0]["corExcelente"].ToString();
            corSatisfatorio = dsParametro.Tables[0].Rows[0]["corSatisfatorio"] + "" == "" ? corSatisfatorio : dsParametro.Tables[0].Rows[0]["corSatisfatorio"].ToString();
            corAtencao = dsParametro.Tables[0].Rows[0]["corAtencao"] + "" == "" ? corAtencao : dsParametro.Tables[0].Rows[0]["corAtencao"].ToString();
            corCritico = dsParametro.Tables[0].Rows[0]["corCritico"] + "" == "" ? corCritico : dsParametro.Tables[0].Rows[0]["corCritico"].ToString();            
        }
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = ((largura) / 3).ToString() + "px";
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 180).ToString() + "px";
        metadeAlturaTela = ((altura - 180) / 2).ToString() + "px";
    }
}
