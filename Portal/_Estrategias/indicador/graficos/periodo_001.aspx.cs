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
using DevExpress.Web;

public partial class _Estrategias_indicador_graficos_periodo_001 : System.Web.UI.Page
{
    dados cDados;

    int codigoIndicador = 0, casasDecimais = 0; 

    int codigoUnidade = 0;

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

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        codigoIndicador = int.Parse(cDados.getInfoSistema("COIN").ToString());

        if (Request.QueryString["NumeroCasas"] != null && Request.QueryString["NumeroCasas"].ToString() != "")
        {
            casasDecimais = int.Parse(Request.QueryString["NumeroCasas"].ToString());
        }        

        if (cDados.getInfoSistema("CodigoUnidade") != null)
            codigoUnidade = int.Parse(cDados.getInfoSistema("CodigoUnidade").ToString());

        carregaGrid();

        ((GridViewDataTextColumn)grid.Columns[1]).PropertiesTextEdit.DisplayFormatString = "{0:n" + casasDecimais + "}";
        ((GridViewDataTextColumn)grid.Columns[2]).PropertiesTextEdit.DisplayFormatString = "{0:n" + casasDecimais + "}";

        defineAlturaTela();

        cDados.aplicaEstiloVisual(this);
    }

    private void carregaGrid()
    {
        DataTable dtGrid = cDados.getPeriodicidadeIndicador(codigoUnidade, codigoIndicador, "").Tables[0];

        grid.DataSource = dtGrid;

        grid.DataBind();
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        grid.Settings.VerticalScrollableHeight = (alturaPrincipal - 451);
    }
}