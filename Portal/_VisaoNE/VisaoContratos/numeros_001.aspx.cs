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

public partial class _VisaoNE_VisaoContratos_numeros_001 : System.Web.UI.Page
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


        DataSet ds = cDados.getNumerosContratos_Tabela1(codigoEntidade, "");

        defineLarguraTela();
        getInfoContratos_Tabela1(ds);
        getInfoContratos_Tabela2(ds);
        cDados.aplicaEstiloVisual(this.Page);
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        //ASPxRoundPanel1.Width = 250;
        //ASPxRoundPanel1.ContentHeight = (altura - 192);
    }
   
    private void getInfoContratos_Tabela1(DataSet ds)
    {
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];

            double previstos30Dias = dr["TotalContratosVencer"].ToString() != "" ? double.Parse(dr["TotalContratosVencer"].ToString()) : 0.0;
            double emAprovacao = dr["TotalContratosEmAprovacao"].ToString() != "" ? double.Parse(dr["TotalContratosEmAprovacao"].ToString()) : 0.0;
            double ativos = dr["TotalContratosAtivos"].ToString() != "" ? double.Parse(dr["TotalContratosAtivos"].ToString()) : 0.0;
            double encerrados = dr["TotalContratosEncerrados"].ToString() != "" ? double.Parse(dr["TotalContratosEncerrados"].ToString()) : 0.0;
            double aditivos = dr["QuantidadeAditivos"].ToString() != "" ? double.Parse(dr["QuantidadeAditivos"].ToString()) : 0.0;

            double totalContratos = emAprovacao + ativos + encerrados;


            spanContratos_T1.InnerHtml = string.Format(@"
                                              <table cellpadding=""0"" cellspacing=""0"" style=""width: 100%"">
                                                <tr style='font-weight:bold'>
                                                    <td style='padding-right: 8px;'>Total de Contratos</td>
                                                    <td style='padding-right: 8px;' align='right'>{0:n0}</td>
                                                    <td align='right'>100%</td>
                                                 </tr>
                                                  <tr>
                                                    <td style='padding-right: 8px;padding-left: 8px;'>Previstos</td>
                                                    <td style='padding-right: 8px;' align='right'>{1:n0}</td>
                                                    <td align='right'>{2:p2}</td>
                                                 </tr>
                                                  <tr>
                                                    <td style='padding-right: 8px;padding-left: 8px;'>Ativos</td>
                                                    <td style='padding-right: 8px;' align='right'>{3:n0}</td>
                                                    <td align='right'>{4:p2}</td>
                                                 </tr>
                                                  <tr>
                                                    <td style='padding-right: 8px;padding-left: 8px;'>Encerrados</td>
                                                    <td style='padding-right: 8px;' align='right'>{5:n0}</td>
                                                    <td align='right'>{6:p2}</td>
                                                 </tr>
                                                 </table>
                                                 <table cellpadding=""0"" cellspacing=""0"" style=""width: 100%"">
                                                  <tr style='font-weight:bold'>
                                                    <td style='padding-right: 8px;padding-top: 4px;'>A Encerrar em 60 dias</td>
                                                    <td style='padding-top: 4px;' align='right'>{7:n0}</td>
                                                 </tr>
                                                 <tr style='font-weight:bold'>
                                                    <td style='padding-right: 8px;padding-top: 4px;'>Total de Aditivos</td>
                                                    <td style='padding-top: 4px;' align='right'>{8:n0}</td>
                                                 </tr>
                                               </table>", totalContratos
                                                    , emAprovacao
                                                    , emAprovacao == 0 ? 0 : emAprovacao / totalContratos
                                                    , ativos
                                                    , ativos == 0 ? 0 : ativos / totalContratos
                                                    , encerrados
                                                    , encerrados == 0 ? 0 : encerrados / totalContratos
                                                    , previstos30Dias
                                                    , aditivos);
        }
    }

    private void getInfoContratos_Tabela2(DataSet ds)
    {        
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];

            spanContratos_T2.InnerHtml = string.Format(@"
                                              <table cellpadding=""0"" cellspacing=""0"" style=""width: 100%"">
                                                <tr style='font-weight:bold'>
                                                    <td style='padding-right: 8px;'>Contratado</td>
                                                    <td style='padding-right: 8px;' align='right'>{0:n2}</td>
                                                    <td align='right'>{1:p0}</td>
                                                 </tr>
                                                  <tr>
                                                    <td style='padding-right: 8px;padding-left: 8px;'>Pago</td>
                                                    <td style='padding-right: 8px;' align='right'>{2:n2}</td>
                                                    <td align='right'>{3:p2}</td>
                                                 </tr>
                                                  <tr>
                                                    <td style='padding-right: 8px;padding-left: 8px;'>Previsto Pagto.</td>
                                                    <td style='padding-right: 8px;' align='right'>{4:n2}</td>
                                                    <td align='right'>{5:p2}</td>
                                                 </tr>
                                                  <tr>
                                                    <td style='padding-right: 8px;padding-left: 8px;'>Saldo</td>
                                                    <td style='padding-right: 8px;' align='right'>{6:n2}</td>
                                                    <td align='right'>{7:p2}</td>
                                                 </tr>
                                                 </table>
                                                 <table cellpadding=""0"" cellspacing=""0"" style=""width: 100%"">
                                                  <tr style='font-weight:bold'>
                                                    <td style='padding-right: 8px;padding-top: 4px;'>Valor Médio</td>
                                                    <td style='padding-top: 4px;' align='right'>{8:n2}</td>
                                                 </tr>
                                                 <tr style='font-weight:bold'>
                                                    <td style='padding-right: 8px;padding-top: 4px;'>Total Aditado</td>
                                                    <td style='padding-top: 4px;' align='right'>{9:n2}</td>
                                                 </tr>
                                                 <tr style='font-weight:bold'>
                                                    <td style='padding-right: 8px;padding-top: 4px;'>Valor Médio Aditivos</td>
                                                    <td style='padding-top: 4px;' align='right'>{10:n2}</td>
                                                 </tr>
                                               </table>", dr["ValorContratos"].ToString() != "" ? double.Parse(dr["ValorContratos"].ToString()) : 0.0
                                                    , dr["PercContrato"].ToString() != "" ? double.Parse(dr["PercContrato"].ToString()) : 0.0
                                                    , dr["ValorPago"].ToString() != "" ? double.Parse(dr["ValorPago"].ToString()) : 0.0
                                                    , dr["PercPago"].ToString() != "" ? double.Parse(dr["PercPago"].ToString()) : 0.0
                                                    , dr["ValorPagar"].ToString() != "" ? double.Parse(dr["ValorPagar"].ToString()) : 0.0
                                                    , dr["PercValorPagar"].ToString() != "" ? double.Parse(dr["PercValorPagar"].ToString()) : 0.0
                                                    , dr["SaldoContratual"].ToString() != "" ? double.Parse(dr["SaldoContratual"].ToString()) : 0.0
                                                    , dr["PercSaldoContratual"].ToString() != "" ? double.Parse(dr["PercSaldoContratual"].ToString()) : 0.0
                                                    , dr["ValorMedioContratos"].ToString() != "" ? double.Parse(dr["ValorMedioContratos"].ToString()) : 0.0
                                                    , dr["ValorAditivos"].ToString() != "" ? double.Parse(dr["ValorAditivos"].ToString()) : 0.0
                                                    , dr["ValorMedioAditivos"].ToString() != "" ? double.Parse(dr["ValorMedioAditivos"].ToString()) : 0.0);
        }
    }
}
