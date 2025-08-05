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

public partial class _VisaoEPC_Graficos_numeros_001 : System.Web.UI.Page
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
        getInfoContrato();
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        //ASPxRoundPanel1.Width = 250;
        //ASPxRoundPanel1.ContentHeight = (altura - 192);
    }

    private void getInfoContrato()
    {
        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidade, "calculaSaldoContratualPorValorContrato");
        string utilizaCalculoSaldoPorValorPago = "N";
        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            if (dsParametros.Tables[0].Rows[0]["calculaSaldoContratualPorValorContrato"].ToString() != "")
            {
                utilizaCalculoSaldoPorValorPago = dsParametros.Tables[0].Rows[0]["calculaSaldoContratualPorValorContrato"].ToString();
            }
        }

        DataSet ds = cDados.getNumerosContratoMaster(1);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string strHTMLContratado = "", strHTMLPago = "", strHTMLSaldo = "";
            double valorContratado = 0, valorPago = 0, valorSaldo = 0;
            int index = 0;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (index == 0)
                {
                    valorContratado = double.Parse(dr["ValorContratado"].ToString());
                    valorPago = double.Parse(dr["ValorPago"].ToString());
                    valorSaldo = valorContratado - valorPago;

                    strHTMLContratado += string.Format(@"<tr style='font-weight:bold'>
                                                    <td style='padding-right: 8px;'>Contratado</td>
                                                    <td style='padding-right: 8px;' align='right'>{0:n2}</td>
                                                    <td align='right'>{1:p0}</td>
                                                   </tr>", valorContratado, 1);

                    strHTMLPago += string.Format(@"<tr style='font-weight:bold'>
                                                    <td style='padding-right: 8px; padding-top: 7px;'>Pago</td>
                                                    <td style='padding-right: 8px; padding-top: 7px;' align='right'>{0:n2}</td>
                                                    <td style='padding-top: 7px;' align='right'>{1:p0}</td>
                                                   </tr>", valorPago, valorContratado == 0 ? 0 : valorPago / valorContratado);

                    strHTMLSaldo += string.Format(@"<tr style='font-weight:bold'>
                                                    <td style='padding-right: 8px; padding-top: 7px;'>Saldo</td>
                                                    <td style='padding-right: 8px; padding-top: 7px;' align='right'>{0:n2}</td>
                                                    <td style='padding-top: 7px;' align='right'>{1:p0}</td>
                                                   </tr>", valorSaldo, valorContratado == 0 ? 0 : valorSaldo / valorContratado);
                }
                else
                {
                    double valorContratadoFilho = double.Parse(dr["ValorContratado"].ToString());
                    double valorPagoFilho = double.Parse(dr["ValorPago"].ToString());
                    double valorSaldoFilho = valorContratadoFilho - valorPagoFilho;

                    string estiloSombreado = "background-color:#EBEBEB";

                    if (index % 2 == 0)
                        estiloSombreado = "";

                    strHTMLContratado += string.Format(@"<tr style='{3}'>
                                                    <td style='padding-top: 4px; padding-bottom: 4px;padding-right: 8px;padding-left: 8px;'>{2}</td>
                                                    <td style='padding-top: 4px; padding-bottom: 4px;padding-right: 8px;padding-left: 8px;' align='right'>{0:n2}</td>
                                                    <td style='padding-top: 4px; padding-bottom: 4px;padding-left: 8px;' align='right'>{1:p2}</td>
                                                   </tr>", valorContratadoFilho
                                                         , valorContratado == 0 ? 0 : valorContratadoFilho / valorContratado
                                                         , dr["DescricaoContrato"].ToString()
                                                         , estiloSombreado);

                    strHTMLPago += string.Format(@"<tr style='{3}'>
                                                    <td style='padding-top: 4px;padding-bottom: 4px;padding-right: 8px;padding-left: 8px;'>{2}</td>
                                                    <td style='padding-top: 4px;padding-bottom: 4px;padding-right: 8px;padding-left: 8px;' align='right'>{0:n2}</td>
                                                    <td style='padding-top: 4px;padding-bottom: 4px;padding-left: 8px;' align='right'>{1:p2}</td>
                                                   </tr>", valorPagoFilho
                                                         , valorContratado == 0 ? 0 : valorPagoFilho / valorContratado
                                                         , dr["DescricaoContrato"].ToString()
                                                         , estiloSombreado);

                    strHTMLSaldo += string.Format(@"<tr style='{3}'>
                                                    <td style='padding-top: 4px;padding-bottom: 4px;padding-right: 8px;padding-left: 8px;'>{2}</td>
                                                    <td style='padding-top: 4px;padding-bottom: 4px;padding-right: 8px;padding-left: 8px;' align='right'>{0:n2}</td>
                                                    <td style='padding-top: 4px;padding-bottom: 4px;padding-left: 8px;' align='right'>{1:p2}</td>
                                                   </tr>", valorSaldoFilho
                                                         , valorContratado == 0 ? 0 : valorSaldoFilho / valorContratado
                                                         , dr["DescricaoContrato"].ToString()
                                                         , estiloSombreado);
                }
                index++;
            }

            spanContratos_T2.InnerHtml = string.Format(@"
                                              <table cellpadding=""0"" cellspacing=""0"" style=""width: 100%"">
                                                {0}
                                                {1}
                                                {2}
                                               </table>", strHTMLContratado, strHTMLPago, strHTMLSaldo);
        }
    }
}
