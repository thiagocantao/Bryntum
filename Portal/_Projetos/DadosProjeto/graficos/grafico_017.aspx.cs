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
using System.Drawing;

public partial class grafico_009 : System.Web.UI.Page
{
    dados cDados;

    int codigoProjeto = 0;
    public string styleDisplayReceita = "";

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

        cDados.aplicaEstiloVisual(this);
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }

        DataSet dsParametros = cDados.getParametrosSistema("mostrarValoresReceita");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["mostrarValoresReceita"].ToString() == "N")
        {
            styleDisplayReceita = "display:none";
        }

        defineTamanhoObjetos();

        getNumeros();

        cDados.defineAlturaFrames(this, 55);
    }
  
    private void defineTamanhoObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        
        int larguraPaineis = ((largura) / 2 - 105);
    }

    private void getNumeros()
    {
        DataSet dsDados = cDados.getCusteioInvestimentoProjeto(codigoProjeto, DateTime.Now.Year, "");

        string html = @"<table cellpadding=""0"" cellspacing=""0"" style=""border - style: solid; border - width: 1px; border - color: #CCCCCC; width:100%; height: 65px;"">";
        string cabecalhoTB = "<tr>";
        string corpoTB = "<tr>";



        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            foreach (DataRow dr in dsDados.Tables[0].Rows)
            {
                cabecalhoTB += string.Format(@"<td style=""background-color: #E8E8E8; width:{0}%; height: 20px;"" >
                                                   {1} (R$)
                                                </td>", (int)(100 / dsDados.Tables[0].Rows.Count)
                                                      , dr["DescricaoGrupoConta"]);

                corpoTB += string.Format(@"<td>
                                                    <table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width: 100%; "">
                                                        <tr>
                                                            <td align=""left"" style=""border-left-style: solid; border-left-width: 1px; border-left-color: #E8E8E8; height: 20px;"">
                                                                Previsto:
                                                            </td>
                                                            <td align=""left"" style=""border-right-style: solid; border-right-width: 1px; border-right-color: #E8E8E8; height: 20px;"">
                                                                {0:n2}
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align=""left""  style=""border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #E8E8E8; border-left-style: solid; border-left-width: 1px; border-left-color: #E8E8E8; height: 20px;"">
                                                                Real:
                                                            </td>
                                                            <td align=""left"" style=""border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #E8E8E8; border-right-style: solid; border-right-width: 1px; border-right-color: #E8E8E8; height: 20px;"">
                                                                {1:n2}
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>"
                    , dr["ValorPrevisto"]
                    , dr["ValorReal"]);

            }
        }
        else
        {
            corpoTB = "<td></td>";
            corpoTB = "<td>Nenhuma Informação a Apresentar</td>";
        }


        cabecalhoTB += "</tr>";
        corpoTB += "</tr>";

        html += cabecalhoTB + corpoTB + "</table>";

        dvNumeros.Controls.Add(cDados.getLiteral(html));
    }
}
