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

public partial class zoomBullets : System.Web.UI.Page
{
    public string grafico1_xml = "";
    public string grafico2_xml = "";
    public string grafico3_xml = "";

    //Mensagens de erro para os gráficos
    public string msgErro = "?LoadDataErrorText=Sem Informações a Apresentar";
    public string msgNoData = "&ChartNoDataText=Sem Informações a Apresentar";
    public string msgInvalid = "&InvalidXMLText=Sem Informações a Apresentar";
    public string desenhando = "&PBarLoadingText=Gerando imagem...";
    public string msgLoading = "&XMLLoadingText=Carregando...";

    public string mostrarReceita = "S";
    public string mostrarDespesa = "S";
    public string mostrarEsforco = "S";
    public int alturaGrafico = 145;

    public int numeroGraficos = 3;

    public string tipoGrafico = "";
    public string grafico1_jsonzoom = "";
    public string grafico2_jsonzoom = "";
    public string grafico3_jsonzoom = "";
    public string tipoGrafico1 = "";
    public string tipoGrafico2 = "";
    public string tipoGrafico3 = "";

    dados cDados;
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

        lbl_TituloGrafico.Text = "";

        DataSet dsParametros = cDados.getParametrosSistema("mostrarValoresReceita", "mostrarValoresDespesa", "mostrarValoresEsforco");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            if (dsParametros.Tables[0].Rows[0]["mostrarValoresReceita"].ToString() == "N" )
            {
                mostrarReceita = "N";
                numeroGraficos--;
            }

            if (dsParametros.Tables[0].Rows[0]["mostrarValoresDespesa"].ToString() == "N" )
            {
                mostrarDespesa = "N";
                numeroGraficos--;
            }

            if (dsParametros.Tables[0].Rows[0]["mostrarValoresEsforco"].ToString() == "N" )
            {
                mostrarEsforco = "N";
                numeroGraficos--;
            }
        }

        defineLarguraTela();

        DataSet ds1 = cDados.getParametrosSistema("tituloPaginasWEB");
        if(cDados.DataSetOk(ds1) && cDados.DataTableOk(ds1.Tables[0]))
        {
            Page.Title = ds1.Tables[0].Rows[0][0].ToString() + " - Zoom";
        }

        tipoGrafico1 = Request.QueryString["grafico002_1_jsonzoom"].ToString();
        tipoGrafico2 = Request.QueryString["grafico002_2_jsonzoom"].ToString();
        tipoGrafico3 = Request.QueryString["grafico002_3_jsonzoom"].ToString();
        if (mostrarEsforco != "N")
        {
            grafico1_jsonzoom = Session[tipoGrafico1].ToString();
        }
        if (mostrarDespesa != "N")
        {
            grafico2_jsonzoom = Session[tipoGrafico2].ToString();
        }
        if (mostrarReceita != "N")
        {
            grafico3_jsonzoom = Session[tipoGrafico3].ToString();
        }
    }

    private void defineLarguraTela()
    {
        int altura;

        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

        alturaGrafico = ((altura - 210)/numeroGraficos);

    }
}
