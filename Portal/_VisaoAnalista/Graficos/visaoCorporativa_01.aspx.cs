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

public partial class _VisaoAnalista_Graficos_visaoCorporativa_01 : System.Web.UI.Page
{
    dados cDados;
    
    public string larguraTela = "", alturaTela = "", larguraTabela = "", metadeAlturaTela = "", alturaGrafico = "", larguraGrafico = "";

    public string grafico1 = "", grafico2 = "", grafico3 = "", grafico4 = "", grafico5 = "", grafico6 = "";

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

        grafico1 = "an_002.aspx";
        grafico2 = "an_003.aspx";
        grafico3 = "an_004.aspx";
        grafico4 = "an_001.aspx";
        grafico5 = "an_005.aspx";
        grafico6 = "an_006.aspx";

        DataSet dsParametro = cDados.getParametrosSistema("an_grafico001", "an_grafico002", "an_grafico003", "an_grafico004", "an_grafico005", "an_grafico006");

        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            string extensao = ".aspx?Altura=" + alturaGrafico + "&Largura=" + larguraGrafico;

            grafico1 = dsParametro.Tables[0].Rows[0]["an_grafico001"] + "" == "" ? grafico1 : dsParametro.Tables[0].Rows[0]["an_grafico001"] + extensao;
            grafico2 = dsParametro.Tables[0].Rows[0]["an_grafico002"] + "" == "" ? grafico2 : dsParametro.Tables[0].Rows[0]["an_grafico002"] + extensao;
            grafico3 = dsParametro.Tables[0].Rows[0]["an_grafico003"] + "" == "" ? grafico3 : dsParametro.Tables[0].Rows[0]["an_grafico003"] + extensao;
            grafico4 = dsParametro.Tables[0].Rows[0]["an_grafico004"] + "" == "" ? grafico4 : dsParametro.Tables[0].Rows[0]["an_grafico004"] + extensao;
            grafico5 = dsParametro.Tables[0].Rows[0]["an_grafico005"] + "" == "" ? grafico5 : dsParametro.Tables[0].Rows[0]["an_grafico005"] + extensao;
            grafico6 = dsParametro.Tables[0].Rows[0]["an_grafico006"] + "" == "" ? grafico6 : dsParametro.Tables[0].Rows[0]["an_grafico006"] + extensao;
        }
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = ((largura) / 3).ToString() + "px";
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 210).ToString() + "px";
        metadeAlturaTela = ((altura - 210) / 2).ToString() + "px";
               
        alturaGrafico = ((altura - 210) / 2).ToString();
        larguraGrafico = ((largura) / 3).ToString();
    }
}
