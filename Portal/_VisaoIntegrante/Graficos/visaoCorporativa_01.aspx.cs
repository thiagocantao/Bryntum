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

public partial class _VisaoIntegrante_Graficos_visaoCorporativa_01 : System.Web.UI.Page
{
    dados cDados;

    public string larguraTela = "", larguraGrid = "", alturaTela = "", larguraTabela = "", metadeAlturaTela = "";

    public string grafico1 = "", grafico2 = "", grafico3 = "";

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

        grafico1 = "ie_003.aspx";
        grafico2 = "ie_001.aspx";
        grafico3 = "ie_002.aspx";

        DataSet dsParametro = cDados.getParametrosSistema("ie_grafico001", "ie_grafico002", "ie_grafico003");

        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            grafico1 = dsParametro.Tables[0].Rows[0]["ie_grafico001"] + "" == "" ? grafico1 : dsParametro.Tables[0].Rows[0]["ie_grafico001"].ToString();
            grafico2 = dsParametro.Tables[0].Rows[0]["ie_grafico002"] + "" == "" ? grafico2 : dsParametro.Tables[0].Rows[0]["ie_grafico002"].ToString();
            grafico3 = dsParametro.Tables[0].Rows[0]["ie_grafico003"] + "" == "" ? grafico3 : dsParametro.Tables[0].Rows[0]["ie_grafico003"].ToString();
        }
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = ((largura) / 3).ToString() + "px";
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;
        larguraGrid = ((largura - (largura) / 3) + 5).ToString() + "px";
        alturaTela = (altura - 185).ToString() + "px";
        metadeAlturaTela = ((altura - 195) / 2).ToString() + "px";
    }
}
