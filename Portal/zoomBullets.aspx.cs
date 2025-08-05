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
    public string grafico1_swf = "";
    public string grafico1_xml = "";
    public string grafico2_swf = "";
    public string grafico2_xml = "";
    public string grafico3_swf = "";
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

        //Atribui os valores recebidos como parametro às variáveis para carregar os gráficos
        grafico1_swf = "Flashs/HBullet.swf";
        grafico1_xml = (Request.QueryString["XML1"] + "" != "" ? Request.QueryString["XML1"].ToString() + msgErro + msgNoData + msgInvalid + desenhando + msgLoading : "");
        grafico2_swf = "Flashs/HBullet.swf";
        grafico2_xml = (Request.QueryString["XML2"] + "" != "" ? Request.QueryString["XML2"].ToString() + msgErro + msgNoData + msgInvalid + desenhando + msgLoading : "");
        grafico3_swf = "Flashs/HBullet.swf";
        grafico3_xml = (Request.QueryString["XML3"] + "" != "" ? Request.QueryString["XML3"].ToString() + msgErro + msgNoData + msgInvalid + desenhando + msgLoading : "");

        DataSet dsParametros = cDados.getParametrosSistema("mostrarValoresReceita", "mostrarValoresDespesa", "mostrarValoresEsforco");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            if (dsParametros.Tables[0].Rows[0]["mostrarValoresReceita"].ToString() == "N" || grafico3_xml == "")
            {
                mostrarReceita = "N";
                numeroGraficos--;
            }

            if (dsParametros.Tables[0].Rows[0]["mostrarValoresDespesa"].ToString() == "N" || grafico2_xml == "")
            {
                mostrarDespesa = "N";
                numeroGraficos--;
            }

            if (dsParametros.Tables[0].Rows[0]["mostrarValoresEsforco"].ToString() == "N" || grafico1_xml == "")
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
        //Atribui o valor recebido como parametro ao título do gráfico
        //lbl_TituloGrafico.Text = Request.QueryString["TIT"].ToString();
    }

    private void defineLarguraTela()
    {
        int altura;

        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

        alturaGrafico = ((altura - 210)/numeroGraficos);

    }
}
