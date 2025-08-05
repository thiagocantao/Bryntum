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

public partial class _VisaoNE_VisaoContratos_visaoCorporativa_01 : System.Web.UI.Page
{
    dados cDados;
    
    public string larguraTela = "", larguraTela2 = "", alturaTela = "", larguraTabela = "", metadeAlturaTela = "", larguraGraficoBarras = "", alturaNumeros = "";

    public string grafico1 = "", grafico2 = "", grafico3 = "", grafico4 = "", numeros1 = "";

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

        DataSet dsParametro = cDados.getParametrosSistema("pgc_grafico001", "pgc_grafico002", "pgc_grafico003", "pgc_grafico004");

        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            grafico1 = dsParametro.Tables[0].Rows[0]["pgc_grafico001"] + "" == "" ? grafico1 : dsParametro.Tables[0].Rows[0]["pgc_grafico001"] + ".aspx";
            grafico2 = dsParametro.Tables[0].Rows[0]["pgc_grafico002"] + "" == "" ? grafico2 : dsParametro.Tables[0].Rows[0]["pgc_grafico002"] + ".aspx";
            grafico3 = dsParametro.Tables[0].Rows[0]["pgc_grafico003"] + "" == "" ? grafico3 : dsParametro.Tables[0].Rows[0]["pgc_grafico003"] + ".aspx";
            grafico4 = dsParametro.Tables[0].Rows[0]["pgc_grafico004"] + "" == "" ? grafico4 : dsParametro.Tables[0].Rows[0]["pgc_grafico004"] + ".aspx";
        }

    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x'))) - 70;
        larguraTela = ((largura - 175) / 2).ToString() + "px";
        larguraTela2 = ((largura - 295) / 2).ToString() + "px";
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 159).ToString() + "px";
        metadeAlturaTela = ((altura - 160) / 2).ToString() + "px";
        alturaNumeros = (altura - 160).ToString() + "px";
    }
}
