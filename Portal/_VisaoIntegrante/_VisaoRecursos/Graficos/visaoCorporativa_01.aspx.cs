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

    public string grafico1 = "", grafico2 = "", grafico3 = "", grafico4 = "";

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

        DataSet dsParametro = cDados.getParametrosSistema("rec_grafico001", "rec_grafico002", "rec_grafico003", "rec_grafico004");

        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            string extensao = ".aspx?Altura=" + alturaGrafico + "&Largura=" + larguraGrafico;

            grafico1 = dsParametro.Tables[0].Rows[0]["rec_grafico001"] + "" == "" ? grafico1 : dsParametro.Tables[0].Rows[0]["rec_grafico001"] + extensao;
            grafico2 = dsParametro.Tables[0].Rows[0]["rec_grafico002"] + "" == "" ? grafico2 : dsParametro.Tables[0].Rows[0]["rec_grafico002"] + extensao;
            grafico3 = dsParametro.Tables[0].Rows[0]["rec_grafico003"] + "" == "" ? grafico3 : dsParametro.Tables[0].Rows[0]["rec_grafico003"] + extensao;
            grafico4 = dsParametro.Tables[0].Rows[0]["rec_grafico004"] + "" == "" ? grafico3 : dsParametro.Tables[0].Rows[0]["rec_grafico004"] + extensao;
        }
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = ((largura) / 2).ToString() + "px";
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 159).ToString() + "px";
        metadeAlturaTela = ((altura - 160) / 2).ToString() + "px";
               
        alturaGrafico = ((altura - 160) / 2).ToString();
        larguraGrafico = ((largura) / 2).ToString();
    }
}
