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
using Newtonsoft.Json;
using System.Dynamic;
using System.Collections.Generic;
using System.Text;
using FusionCharts.Charts;

public partial class grafico_001 : System.Web.UI.Page
{
    dados cDados;

    int codigoProjeto = 0;

    //pega a data e hora atual do sistema
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";

    //Mensagens de erro para os gráficos
    public string msgErro = "?LoadDataErrorText=Sem Informações a Apresentar";
    public string msgNoData = "&ChartNoDataText=Sem Informações a Apresentar";
    public string msgInvalid = "&InvalidXMLText=Sem Informações a Apresentar";
    public string desenhando = "&PBarLoadingText=Gerando imagem...";
    public string msgLoading = "&XMLLoadingText=Carregando...";

    public string grafico_titulo = "Curva S Física";
    public string grafico_xml = "";
    public string grafico_xmlzoom = "";
    public string tipoGrafico = "";
    public string grafico18_jsonzoom = "";
    public int alturaGrafico = 220;
    public int larguraGrafico = 350;

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

        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }

        defineTamanhoObjetos();
        
        if (!IsPostBack)
        {
            //Função que gera o gráfico
            geraGraficoSpeedometro();
            tipoGrafico = "angulargauge";
        }

        cDados.aplicaEstiloVisual(this);
    }

    //Função para geração do gráfico
    private void geraGraficoCurva()
    {

        //cria  a variável para armazenar o XML
        string xml = "";
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        //cria uma variável com o nome e o caminho do XML do gráfico
        string nomeGrafico = @"/ArquivosTemporarios/CurvaS_Projeto_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        DataTable dt = cDados.getDesempenhoCurvaSObjeto(codigoEntidade, codigoUsuario, codigoProjeto.ToString(), "NULL", "PROJ").Tables[0];

        //função para gerar a estrutura xml para criação do gráfico
        xml = cDados.getGraficoCurvaSRelatorio(dt, "", 9, 0, 9, 4, 25);

        //escreve o arquivo xml
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = "../../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        nomeGrafico = "/ArquivosTemporarios/CurvaS_Projeto_ZOOM" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //gera o xml do gráfico Gauge do percentual concluido ZOOM
        xml = cDados.getGraficoCurvaSRelatorio(dt, "Curva S Física", 15, 0, 12, 3, 12);

        //escreve o arquivo XML do gráfico Gauge do percentual concluido ZOOM
        cDados.escreveXML(xml, nomeGrafico);

        grafico_xmlzoom = nomeGrafico;

        var jsonChart = ConverterFormatUtil.convertXMLToJSON(xml);
        jsonChart = jsonChart.Replace("\"", "'");
        jsonChart = jsonChart.Replace(@"'categories':{", @"'categories':[{");
        jsonChart = jsonChart.Replace(@"},'dataset'", @"}],'dataset'");

        jsonChart = jsonChart.Replace("'set'", "'data'");
        dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(jsonChart);
        obj.chart.theme = "fusion";
        obj.chart.showHoverEffect = "1";
        obj.chart.flatScrollBars = "1";
        obj.chart.linethickness = "3";
        var dataset = new List<object>();
        var datasetNovo = new List<object>();

        var newObject = (IDictionary<string, object>)obj;
        var chart = (IDictionary<string, object>)newObject["chart"];
        if (chart.ContainsKey("categories") && chart["categories"] != null && chart["categories"].ToString() != "")
        {
            var categories = (List<object>)chart["categories"];
            chart.Remove("categories");
            obj.categories = categories;
        }
        else if (chart["categories"].ToString() == "")
        {
            chart.Remove("categories");
            dynamic objCategory = JsonConvert.DeserializeObject<ExpandoObject>("{'category' : [{label:\"\"}]}");
            var objList = new List<object>();
            objList.Add(objCategory);
            obj.categories = objList;

            dataset = (List<object>)chart["dataset"];
            if (dataset.Count > 0)
            {
                foreach (var item in dataset)
                {
                    var objListData = new List<object>();
                    var objJsonItem = JsonConvert.SerializeObject(item);
                    dynamic objItem = JsonConvert.DeserializeObject<ExpandoObject>(objJsonItem);
                    dynamic objData = JsonConvert.DeserializeObject<ExpandoObject>("{value : \"0\"}");
                    objListData.Add(objData);
                    objItem.data = objListData;
                    datasetNovo.Add(objItem);
                }
            }
        }
        dataset = datasetNovo.Count > 0 ? datasetNovo : (List<object>)chart["dataset"];
        chart.Remove("dataset");
        chart.Remove("baseFontSize");
        chart.Remove("caption");

        obj.chart.baseFontSize = "9";
        obj.dataset = dataset;
        Session["grafico18_jsonzoom"] = JsonConvert.SerializeObject(obj);
        grafico18_jsonzoom = JsonConvert.SerializeObject(obj);
    }

    //Carrega o gráfico speedometro
    private void geraGraficoSpeedometro()
    {

        string xml = "";

        string nomeGrafico = @"/ArquivosTemporarios/Speedometro_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        DataSet dsGrafico = cDados.getDesempenhoFisicoProjeto(codigoProjeto, "");

        DataTable dtGrafico = dsGrafico.Tables[0];

        dsGrafico = cDados.getIndicadorDesempenhoFisico("");

        DataTable dtIndicadores = dsGrafico.Tables[0];

        //gera o xml do gráfico Gauge do percentual concluido
        xml = cDados.getGraficoDesempenhoFisico(dtGrafico, dtIndicadores, 9, "./ImageSaving/FusionChartsSave.aspx", "PercentualPrevisto", "PercentualReal", "", true);

        //escreve o arquivo XML do gráfico Gauge do percentual concluido
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = "../../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        nomeGrafico = "/ArquivosTemporarios/PercentualFisicoZoom_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //gera o xml do gráfico Gauge do percentual concluido ZOOM
        xml = cDados.getGraficoDesempenhoFisico(dtGrafico, dtIndicadores, 15, "./ImageSaving/FusionChartsSave.aspx", "PercentualPrevisto", "PercentualReal", "Percentual Concluído", true);

        //escreve o arquivo XML do gráfico Gauge do percentual concluido ZOOM
        cDados.escreveXML(xml, nomeGrafico);

        grafico_xmlzoom = nomeGrafico;

        cDados.defineAlturaFrames(this, alturaGrafico + 31);

        var jsonChart = ConverterFormatUtil.convertXMLToJSON(xml);
        jsonChart = jsonChart.Replace("\"", "'");
        dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(jsonChart);

        var newObject = (IDictionary<string, object>)obj;
        var chart = (IDictionary<string, object>)newObject["chart"];
        chart.Remove("imageSave");
        chart.Remove("imageSaveURL");
        chart.Remove("exportFileName");
        chart.Remove("exportDialogMessage");
        chart.Remove("exportAtClient");
        chart.Remove("exportFormats");
        chart.Remove("exportHandler");
        chart.Remove("bgColor");
        chart.Remove("gaugeFillMix");
        chart.Remove("gaugeFillRatio");
        
        //Processar deals
        var dial = new List<KeyValuePair<string,
            string>>();
        var dialsOrigin = (IDictionary<string, object>)chart["dials"];
        var dialOrigin = (IDictionary<string, object>)dialsOrigin["dial"];
        foreach (var d in dialOrigin)
        {
            dial.Add(new KeyValuePair<string, string>("value", d.Value.ToString()));
        }
        StringBuilder range = new StringBuilder();
        range.Append("{\"color\":[");
        var colorsRange = (IDictionary<string, object>)chart["colorRange"];
        var colorsOrigin = (List<dynamic>)colorsRange["color"];
        foreach (dynamic clr in colorsOrigin)
        {
            range.AppendFormat("{{\"minValue\":\"{0}\",\"maxValue\":\"{1}\",\"code\":\"{2}\",\"name\":\"{3}\"}},", clr.minValue, clr.maxValue, clr.code, clr.name);
        }
        range.Replace(",", "]}", range.Length - 1, 1);
        var objColor = JsonConvert.DeserializeObject<ExpandoObject>(range.ToString());
        obj.colorRange = objColor;

        chart.Remove("dials");
        chart.Remove("colorRange");
        chart.Remove("trendpoints");
        chart.Remove("baseFontSize");        

        obj.chart.baseFontSize = "9";
        obj.chart.theme = "fusion";
        obj.chart.showToolTip = "0";
        obj.chart.exportEnabled = "0";

        StringBuilder dials = new StringBuilder();
        dials.Append("{\"dial\":[");
        foreach (var dialCnf in dial)
        {
            dials.AppendFormat("{{\"{0}\":\"{1}\"}},", dialCnf.Key, dialCnf.Value);
        }
        dials.Replace(",", "]}", dials.Length - 1, 1);
        var objDials = JsonConvert.DeserializeObject<ExpandoObject>(dials.ToString());
        obj.dials = objDials;

        Session["grafico18_jsonzoom"] = JsonConvert.SerializeObject(obj);
        grafico18_jsonzoom= JsonConvert.SerializeObject(obj);
    }

    private void defineTamanhoObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        int larguraPaineis = ((largura) / 2 - 105);
        int alturaPaineis = altura - 238;
        larguraGrafico = larguraPaineis - 10;

        alturaGrafico = (altura - 309) / 2;
    }

    protected void rbGrafico_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (((ASPxRadioButtonList)sender).Value.ToString() == "S")
        {
            geraGraficoSpeedometro();
            tipoGrafico = "angulargauge";
        }
        else
        {
            //Função que gera o gráfico
            geraGraficoCurva();
            tipoGrafico = "scrollline2d";
        }
    }
}
