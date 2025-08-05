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

public partial class _VisaoMaster_Graficos_numeros_003 : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidade;

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

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
                
        defineLarguraTela();
        getInfoContratos_Tabela1();
        getInfoContratos_Tabela2();
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        //ASPxRoundPanel1.Width = 250;
        //ASPxRoundPanel1.ContentHeight = (altura - 192);
    }

    private void getInfoContratos_Tabela1()
    {
        string codigoArea = Request.QueryString["CA"] == null ? "" : Request.QueryString["CA"].ToString();

        DataSet ds = cDados.getValoresContratosPainelCustosNE("");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string conteudoTable = "";

            int index = 0;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {               
                conteudoTable += string.Format(@" <tr style='padding-left:8px;'>
                                                    <td style='padding-bottom: 3px;font-weight:bold;'>{0}</td><td>&nbsp;</td>
                                                  </tr>
                                                  <tr>
                                                    <td valign:'top' style='padding-bottom: 4px;padding-left: 10px;'>- Contratado (I0)</td><td valign='top' align='right'>{1:n0}</td>
                                                  </tr>                                                  
                                                  <tr>
                                                    <td valign:'top' style='padding-bottom: 4px;padding-left: 10px;'>- Realizado (I0)</td><td valign='top' align='right'>{3:n0}</td>
                                                  </tr>"
                    //<tr>
                                                  //  <td valign:'top' style='padding-bottom: 10px;padding-left: 10px;'>- Realizado Reajustado</td><td valign='top' align='right'>{4:n0}</td>
                                                  //</tr>"
                                                       , dr["Descricao"]
                                                       , dr["ValorContratado"]
                                                       , dr["ValorContratadoReajustado"]
                                                       , dr["ValorRealizado"]
                                                       , dr["ValorRealizadoReajustado"]);

                index++;
            }

            spanContratos_T1.InnerHtml = string.Format(@"
                                              <table cellpadding=""0"" cellspacing=""0"" style=""width: 100%"">
                                                {0}
                                                 </table>", conteudoTable);
        }
    }

    private void getInfoContratos_Tabela2()
    {
        string codigoArea = Request.QueryString["CA"] == null ? "" : Request.QueryString["CA"].ToString();

        DataSet ds = cDados.getValoresSitiosPainelCustosNE(codigoArea, int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), DateTime.Now.Year, DateTime.Now.Month, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string conteudoTable = "";

            int index = 0;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                conteudoTable += string.Format(@" <tr style='padding-left:8px'>
                                                    <td valign:'top' style='padding-bottom: 4px;padding-left: 10px;'>{0}</td><td valign='top' align='right'>{1:n0}</td>
                                                  </tr>", dr["Descricao"]
                                                        , dr["ValorRealizadoReajustado"]);

                index++;
            }

            spanContratos_T2.InnerHtml = string.Format(@"
                                              <table cellpadding=""0"" cellspacing=""0"" style=""width: 100%"">
                                                {0}
                                                 </table>", conteudoTable);
        }
    }
}
