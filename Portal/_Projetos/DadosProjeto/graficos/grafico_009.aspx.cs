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
            pReceita.ClientVisible = false;
        }

        defineTamanhoObjetos();

        getNumeros();

        cDados.defineAlturaFrames(this, 55);

        DataSet dsParam = cDados.getParametrosSistema(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), "labelDespesa");

        string labelDespesa = Resources.traducao.grafico_009_despesa;

        if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]) && dsParam.Tables[0].Rows[0]["labelDespesa"].ToString() != "")
            labelDespesa = dsParam.Tables[0].Rows[0]["labelDespesa"].ToString();

        pDespesa.HeaderText = labelDespesa;
        pReceita.HeaderText = Resources.traducao.receita;

        if (pDespesa.ClientVisible && pReceita.ClientVisible)
        {
            pReceita.BorderLeft.BorderStyle = BorderStyle.None;
            pReceita.HeaderStyle.BorderLeft.BorderStyle = BorderStyle.None;
            pDespesa.HeaderStyle.BorderRight.BorderStyle = BorderStyle.None;
        }
    }
  
    private void defineTamanhoObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        
        int larguraPaineis = ((largura) / 2 - 105);
    }

    private void getNumeros()
    {
        int anoFinanceiro = Request.QueryString["Financeiro"] != null && Request.QueryString["Financeiro"].ToString() != "" ? int.Parse(Request.QueryString["Financeiro"].ToString()) : -1;

        DataSet dsDados = cDados.getNumerosDesempenhoProjeto(codigoProjeto, anoFinanceiro, "");

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            DataTable dt = dsDados.Tables[0];

            lblCustoPrevisto.Text = string.Format("{0:c0}", dt.Rows[0]["TotalCustoOrcadoGeral"] + "" != "" ? double.Parse(dt.Rows[0]["TotalCustoOrcadoGeral"] + "") : 0);
            lblCustoPrevistoData.Text = string.Format("{0:c0}", dt.Rows[0]["TotalCustoOrcado"] + "" != "" ? double.Parse(dt.Rows[0]["TotalCustoOrcado"] + "") : 0);
            lblCustoReal.Text = string.Format("{0:c0}", dt.Rows[0]["TotalCustoReal"] + "" != "" ? double.Parse(dt.Rows[0]["TotalCustoReal"] + "") : 0);
            
            lblReceitaPrevista.Text = string.Format("{0:c0}", dt.Rows[0]["TotalReceitaOrcadaGeral"] + "" != "" ? double.Parse(dt.Rows[0]["TotalReceitaOrcadaGeral"] + "") : 0);
            lblReceitaPrevistaData.Text = string.Format("{0:c0}", dt.Rows[0]["TotalReceitaOrcada"] + "" != "" ? double.Parse(dt.Rows[0]["TotalReceitaOrcada"] + "") : 0);
            lblReceitaReal.Text = string.Format("{0:c0}", dt.Rows[0]["TotalReceitaReal"] + "" != "" ? double.Parse(dt.Rows[0]["TotalReceitaReal"] + "") : 0);
        }
        else
        {
            lblCustoPrevisto.Text = string.Format("{0:c0}", 0);
            lblCustoPrevistoData.Text = string.Format("{0:c0}", 0);
            lblCustoReal.Text = string.Format("{0:c0}", 0);
            
            lblReceitaPrevista.Text = string.Format("{0:c0}", 0);
            lblReceitaPrevistaData.Text = string.Format("{0:c0}", 0);
            lblReceitaReal.Text = string.Format("{0:c0}", 0);
        }
    }
}
