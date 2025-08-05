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

public partial class _Portfolios_VisaoCorporativa_visaoCorporativa_01 : System.Web.UI.Page
{
    dados cDados;
    public string larguraTela = "", alturaTela = "", larguraTabela = "", metadeAlturaTela = "", larguraGraficoBarras = "", alturaNumeros = "";

    public string corExcelente = "#1648DC", corSatisfatorio = "#008844", corAtencao = "#FFFF44",
                  corCritico = "#DD0100", corDespesa = "#AFD7F8", corReceita = "#F6BD0F";

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

        DataSet dsParametro = cDados.getParametrosSistema("corSatisfatorio", "corAtencao", "corCritico");

        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            corSatisfatorio = dsParametro.Tables[0].Rows[0]["corSatisfatorio"] + "" == "" ? corSatisfatorio : dsParametro.Tables[0].Rows[0]["corSatisfatorio"].ToString();
            corAtencao = dsParametro.Tables[0].Rows[0]["corAtencao"] + "" == "" ? corAtencao : dsParametro.Tables[0].Rows[0]["corAtencao"].ToString();
            corCritico = dsParametro.Tables[0].Rows[0]["corCritico"] + "" == "" ? corCritico : dsParametro.Tables[0].Rows[0]["corCritico"].ToString();            
        }

        defineLarguraTela();
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = ((largura - 200) / 3).ToString() + "px";
        larguraTabela = (((largura - 200) / 3) - 55).ToString() + "px";
        larguraGraficoBarras = (((largura - 200) / 3 - 60) + ((largura - 200) / 3)).ToString() + "px";
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 155).ToString() + "px";
        metadeAlturaTela = ((altura - 155) / 2).ToString() + "px";
        alturaNumeros = (altura - 155).ToString() + "px";
    }
}
