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

public partial class _VisaoMaster_Graficos_numeros_002 : System.Web.UI.Page
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
        string codigoReservado = Request.QueryString["CR"] == null ? "" : Request.QueryString["CR"].ToString();

        DataSet ds = cDados.getIndicadoresEstrategicosPainelGerenciamento(codigoReservado, codigoEntidade, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string conteudoTable = "";

            int index = 0;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string estiloSombreado = "background-color:#F6F6F6";

                if (index % 2 == 0)
                    estiloSombreado = "";

                conteudoTable += string.Format(@" <tr style='height:25px; {3}'>
                                                    <td style='padding-right: 8px;'>{0}</td>
                                                    <td style='padding-right: 8px;width:120px' align='right'>{1:n0}</td>
                                                    <td align='right' style='width:120px'>{2:n0}</td>
                                                 </tr>", dr["NomeIndicador"]
                                                       , dr["ValorPrevistoAcum"]
                                                       , dr["ValorRealizadoAcum"]
                                                       , estiloSombreado);

                index++;
            }

            spanContratos_T1.InnerHtml = string.Format(@"
                                              <table cellpadding=""0"" cellspacing=""0"" style=""width: 100%"">
                                                {0}
                                                 </table>", conteudoTable);
        }
    }
}
