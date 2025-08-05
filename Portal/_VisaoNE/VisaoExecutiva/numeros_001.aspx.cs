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

public partial class _VisaoNE_VisaoExecutiva_numeros_001 : System.Web.UI.Page
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
        getInfoObras_Tabela1();
        getInfoContratos_Tabela1();
        getInfoContratos_Tabela2();
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

    private void getInfoObras_Tabela1()
    {
        string montagemLinhas = "";

        DataSet ds = cDados.getNumerosObra_Tabela1(codigoEntidade, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string estiloLinha = "", estiloColuna = "padding-left: 8px;", indicaSum = dr["IndicaSumario"].ToString();

                if (indicaSum == "S")
                {
                    estiloLinha = "style='font-weight:bold'";
                    estiloColuna = "";
                }

                double perc =  double.Parse(dr["Percentual"].ToString());

                montagemLinhas += string.Format(@"<tr {0}>
                                                    <td style='padding-right: 8px;{1}'>{2}</td>
                                                    <td style='padding-right: 8px;' align='right'>{3:n0}</td>
                                                    <td align='right'>{4}</td>
                                                 </tr>", estiloLinha
                                                       , estiloColuna
                                                       , dr["Descricao"].ToString()
                                                       , double.Parse(dr["Quantidade"].ToString())
                                                       , indicaSum == "S" ? string.Format(@"100%") : string.Format(@"{0:n2}%", perc));
            }


            spanObras_T1.InnerHtml = string.Format(@"<table cellpadding=""0"" cellspacing=""0"" style=""width: 100%"">
                                                    {0}
                                                  </table>", montagemLinhas);
        }
    }

    private void getInfoContratos_Tabela1()
    {
        DataSet ds = cDados.getNumerosObra_Tabela2(codigoEntidade, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];

            double previstos30Dias = double.Parse(dr["TotalContratosVencer"].ToString());
            double emAprovacao = double.Parse(dr["TotalContratosEmAprovacao"].ToString());            
            double ativos = double.Parse(dr["TotalContratosAtivos"].ToString());
            double encerrados = double.Parse(dr["TotalContratosEncerrados"].ToString());
            double aditivos = double.Parse(dr["QuantidadeAditivos"].ToString());

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
                                                    <td style='padding-right: 8px;'>A Encerrar em 60 dias</td>
                                                    <td align='right'>{7:n0}</td>
                                                 </tr>
                                                 <tr style='font-weight:bold'>
                                                    <td style='padding-right: 8px;'>Total de Aditivos</td>
                                                    <td align='right'>{8:n0}</td>
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

    private void getInfoContratos_Tabela2()
    {
        DataSet ds = cDados.getNumerosObra_Tabela2(codigoEntidade, "");

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
                                                    <td style='padding-right: 8px;'>Valor Médio</td>
                                                    <td align='right'>{8:n2}</td>
                                                 </tr>
                                                 <tr style='font-weight:bold'>
                                                    <td style='padding-right: 8px;'>Total Aditado</td>
                                                    <td align='right'>{9:n2}</td>
                                                 </tr>
                                                 <tr style='font-weight:bold'>
                                                    <td style='padding-right: 8px;'>Valor Médio Aditivos</td>
                                                    <td align='right'>{10:n2}</td>
                                                 </tr>
                                               </table>", double.Parse(dr["ValorContratos"].ToString())
                                                    , double.Parse(dr["PercContrato"].ToString())
                                                    , double.Parse(dr["ValorPago"].ToString())
                                                    , double.Parse(dr["PercPago"].ToString())
                                                    , double.Parse(dr["ValorPagar"].ToString())
                                                    , double.Parse(dr["PercValorPagar"].ToString())
                                                    , double.Parse(dr["SaldoContratual"].ToString())
                                                    , double.Parse(dr["PercSaldoContratual"].ToString())
                                                    , double.Parse(dr["ValorMedioContratos"].ToString())
                                                    , double.Parse(dr["ValorAditivos"].ToString())
                                                    , double.Parse(dr["ValorMedioAditivos"].ToString()));
        }
    }
}
