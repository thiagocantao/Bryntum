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

public partial class _VisaoAnalista2_Graficos_visaoCorporativa_01 : System.Web.UI.Page
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

        grafico1 = "an_001.aspx";
        grafico2 = "an_002.aspx";
        grafico3 = "an_003.aspx";
        grafico4 = "an_004.aspx";
        grafico5 = "an_005.aspx";
        grafico6 = "an_006.aspx";

        DataSet dsParametro = cDados.getParametrosSistema("an_grafico007", "an_grafico008", "an_grafico009", "an_grafico010", "an_grafico011", "an_grafico012");

        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            string extensao = ".aspx?Altura=" + alturaGrafico + "&Largura=" + larguraGrafico;

            grafico1 = dsParametro.Tables[0].Rows[0]["an_grafico007"] + "" == "" ? grafico1 : dsParametro.Tables[0].Rows[0]["an_grafico007"] + extensao;
            grafico2 = dsParametro.Tables[0].Rows[0]["an_grafico008"] + "" == "" ? grafico2 : dsParametro.Tables[0].Rows[0]["an_grafico008"] + extensao;
            grafico3 = dsParametro.Tables[0].Rows[0]["an_grafico009"] + "" == "" ? grafico3 : dsParametro.Tables[0].Rows[0]["an_grafico009"] + extensao;
            grafico4 = dsParametro.Tables[0].Rows[0]["an_grafico010"] + "" == "" ? grafico4 : dsParametro.Tables[0].Rows[0]["an_grafico010"] + extensao;
            grafico5 = dsParametro.Tables[0].Rows[0]["an_grafico011"] + "" == "" ? grafico5 : dsParametro.Tables[0].Rows[0]["an_grafico011"] + extensao;
            grafico6 = dsParametro.Tables[0].Rows[0]["an_grafico012"] + "" == "" ? grafico6 : dsParametro.Tables[0].Rows[0]["an_grafico012"] + extensao;
        }
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = ((largura) / 3).ToString() + "px";
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 159).ToString() + "px";
        metadeAlturaTela = ((altura - 160) / 2).ToString() + "px";
               
        larguraGrafico = ((largura - 30) / 3).ToString();
        alturaGrafico = ((altura - 255) / 2).ToString(); 
    }
}
