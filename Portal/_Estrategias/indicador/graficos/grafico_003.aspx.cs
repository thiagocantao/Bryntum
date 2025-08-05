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
using FusionCharts.Charts;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Dynamic;

public partial class _Estrategias_indicador_graficos_grafico_003 : System.Web.UI.Page
{
    dados cDados;

    int codigoIndicador = 0, codigoUnidade = 0, mes = 0, ano = 0, casasDecimais = 0;
    string polaridade = "";

    char tipoDesempenho = 'A';

    //pega a data e hora atual do sistema
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";

    //Mensagens de erro para os gráficos
    public string msgErro = "?LoadDataErrorText=" + Resources.traducao.grafico_003_sem_informa__es_a_apresentar;
    public string msgNoData = "&ChartNoDataText=" + Resources.traducao.grafico_003_sem_informa__es_a_apresentar;
    public string msgInvalid = "&InvalidXMLText=" + Resources.traducao.grafico_003_sem_informa__es_a_apresentar;
    public string desenhando = "&PBarLoadingText=" + Resources.traducao.grafico_003_gerando_imagem___;
    public string msgLoading = "&XMLLoadingText=" + Resources.traducao.grafico_003_carregando___;

    public string grafico1_titulo = Resources.traducao.grafico_003_desempenho_do_indicador;

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

        codigoIndicador = int.Parse(cDados.getInfoSistema("COIN").ToString());

        if (Request.QueryString["NumeroCasas"] != null && Request.QueryString["NumeroCasas"].ToString() != "")
        {
            casasDecimais = int.Parse(Request.QueryString["NumeroCasas"].ToString());
        }

        if (Request.QueryString["Polaridade"] != null && Request.QueryString["Polaridade"].ToString() != "")
        {
            polaridade = Request.QueryString["Polaridade"].ToString();
        }

        if (cDados.getInfoSistema("TipoDesempenho") != null)
            tipoDesempenho = char.Parse(cDados.getInfoSistema("TipoDesempenho").ToString());

        if (cDados.getInfoSistema("CodigoUnidade") != null)
            codigoUnidade = int.Parse(cDados.getInfoSistema("CodigoUnidade").ToString());

        if (cDados.getInfoSistema("AnoIndicador") != null)
            ano = int.Parse(cDados.getInfoSistema("AnoIndicador").ToString());

        if (cDados.getInfoSistema("MesIndicador") != null)
            mes = int.Parse(cDados.getInfoSistema("MesIndicador").ToString());

        hfGeral.Set("urlAtual", Request.QueryString.ToString());

        carregaGraficoDesempenho();

        cDados.aplicaEstiloVisual(this);

    }

    //Carrega o gráfico 
    private void carregaGraficoDesempenho()
    {
        string json1 = "";

        string nomeGrafico = @"/ArquivosTemporarios/desempenhoIndicador_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        DataSet dsGrafico = cDados.getDesempenhoIndicadorUnidade(codigoIndicador, mes, ano, tipoDesempenho, codigoUnidade, int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), "");

        if (cDados.DataSetOk(dsGrafico))
        {
            DataTable dt = dsGrafico.Tables[0];

            //gera o JSON do gráfico Gauge
            json1 = getGraficoDesempenhoIndicador(dt, 10, "", casasDecimais, polaridade, codigoIndicador);
            
            string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
            int altura = 0;
            int largura = 0;

            cDados.getLarguraAlturaTela(ResolucaoCliente, out largura, out altura);
            Chart sales = new Chart();
            sales.SetChartParameter(Chart.ChartParameter.chartId, "myChart");
            sales.SetChartParameter(Chart.ChartParameter.chartType, "angulargauge");

            if (!string.IsNullOrEmpty(Request.QueryString["w"] + "") && !string.IsNullOrEmpty(Request.QueryString["h"] + ""))
            {
                sales.SetChartParameter(Chart.ChartParameter.chartWidth, largura - 200);
                sales.SetChartParameter(Chart.ChartParameter.chartHeight, altura - 270);

            }
            else
            {
                alturaGrafico = (180);
                pGrafico1.ContentHeight = 190;
                sales.SetChartParameter(Chart.ChartParameter.chartHeight, pGrafico1.ContentHeight);
            }

            sales.SetData(json1);
            Literal1.Text = sales.Render();
        }
    }

    public string getGraficoDesempenhoIndicador(DataTable dt, int fonte, string titulo, int casaDecimais, string polaridade, int codigoIndicador)
    {
        StringBuilder json = new StringBuilder();

        double resultado = 0;
        float meta = 0;

        string strResultado = "";
        string strMeta = "";

        try
        {

            strResultado = dt.Rows[0]["Realizado"].ToString();
            strMeta = dt.Rows[0]["Meta"].ToString();
            if (!string.IsNullOrWhiteSpace(strResultado))
                resultado = double.Parse(strResultado);
            if (!string.IsNullOrWhiteSpace(strMeta))
                meta = float.Parse(strMeta);


        double limiteCritico, limiteAtencao, limiteSatisfatorio, limiteExcelente, limiteInferiorGrafico, limiteSuperiorGrafico;
        int retProc;

        retProc = cDados.getValoresGraficoGaugeIndicador(codigoIndicador, ref polaridade, meta, resultado, casaDecimais, out limiteInferiorGrafico, out limiteSuperiorGrafico, out limiteCritico, out limiteAtencao, out limiteSatisfatorio, out limiteExcelente);

        string strLimiteSuperior = limiteSuperiorGrafico.ToString().Replace(",", ".");
        string strLimiteInferior = limiteInferiorGrafico.ToString().Replace(",", ".");

            json.Clear();

        json.AppendFormat(@"{{
    ""chart"": {{
        ""plotToolText"" : ""Current Score: $value"",
        ""theme"" : ""fusion"",
        ""upperLimit"" : """ + strLimiteSuperior + @""",
        ""showValue"" : ""1"", 
        ""decimals"" : """ + casaDecimais + @""", 
        ""decimalSeparator"" : ""."", 
        ""inDecimalSeparator"" : "","",
        ""baseFontSize"" : """ + fonte + @""", 
        ""bgColor"" : ""FFFFFF"",
        ""showBorder"" : ""0"", 
        ""chartTopMargin"" : ""5"",
        ""chartBottomMargin"" : ""20"",
        ""lowerLimit"" : """ + strLimiteInferior + @""", 
        ""gaugeFillRatio"" : """",
        ""inThousandSeparator"" : ""."",
        ""thousandSeparator"" : "".""
    }},");
        json.AppendFormat(@"""colorRange"": {{");

        if (polaridade == "NEG")
        {
            json.Append(@"""color"": [");
            json.AppendFormat(@"{{ ""minValue"" : ""{0}"", ""maxValue"" : ""{1}"", ""code"" : ""{2}"" }},", limiteSuperiorGrafico, limiteCritico, cDados.corCritico);
            json.AppendFormat(@"{{ ""minValue"" : ""{0}"", ""maxValue"" : ""{1}"", ""code"" : ""{2}"" }},", limiteCritico, limiteAtencao, cDados.corAtencao);
            json.AppendFormat(@"{{ ""minValue"" : ""{0}"", ""maxValue"" : ""{1}"", ""code"" : ""{2}"" }},", limiteAtencao, limiteSatisfatorio, cDados.corSatisfatorio);
            json.AppendFormat(@"{{ ""minValue"" : ""{0}"", ""maxValue"" : ""{1}"", ""code"" : ""{2}"" }}", limiteSatisfatorio, limiteExcelente, cDados.corExcelente);
            json.Append("]");
            json.AppendFormat("}},");
        }
        else
        {
            json.Append(@"""color"": [");
            json.AppendFormat(@"{{ ""minValue"" : ""{0}"", ""maxValue"" : ""{1}"", ""name"":""Ruim"", ""code"" : ""{2}"" }},", limiteInferiorGrafico, limiteCritico, cDados.corCritico);
            json.AppendFormat(@"{{ ""minValue"" : ""{0}"", ""maxValue"" : ""{1}"", ""name"":""Regular"", ""code"" : ""{2}"" }},", limiteCritico, limiteAtencao, cDados.corAtencao);
            json.AppendFormat(@"{{ ""minValue"" : ""{0}"", ""maxValue"" : ""{1}"", ""name"":""Bom"", ""code"" : ""{2}"" }},", limiteAtencao, limiteSatisfatorio, cDados.corSatisfatorio);
            json.AppendFormat(@"{{ ""minValue"" : ""{0}"", ""maxValue"" : ""{1}"", ""name"":""Excelente"", ""code"" : ""{2}"" }}", limiteSatisfatorio, limiteExcelente, cDados.corExcelente);
            json.Append("]");
            json.AppendFormat("}},");
        }

        json.AppendFormat(@" ""dials"": {{
        ""dial"": [
            {{
                ""value"": """ + (resultado) + @"""
            }}
        ]
    }},");

        json.AppendFormat(@"""trendPoints"": {{
           ""point"": [
            {{
                ""startValue"" : """ + meta + @""",
                ""color"" : ""#0075c2"",
                ""dashed"" : ""1"",
                ""markerTooltext"" : ""Meta = " + meta + @""",
                ""useMarker"" : ""1"",
                ""dashLen"" : ""2"", 
                ""dashGap"":""2"",
                ""valueInside"" : ""1""
            }}
        ]
    }}");

        if (titulo != "")
        {
            json.AppendFormat(@" , ""annotations"": {{
            ""origW"" : ""890"",
            ""origH"" : ""120"",
            ""constrainedScale"" : ""0"",
            ""groups"": [
                   {{
                        ""id"": ""Grp1"",
                        ""autoscale"" : ""1"",
                        ""items"": [
                         {{
                               ""type"" : ""text"",
                               ""font"" : ""Verdana"",
                               ""bold"" : ""1"",
                               ""fontSize"" : ""15"",
                               ""fontColor"" : ""000000"",
                               ""align"" : ""center"", 
                               ""x"" : ""435"",
                               ""y"" : ""20"",
                               ""label"" : ""{0}""
                         }}
                      ]
                 }}
            ]
        }}", titulo);
        }
        json.AppendFormat(@"}}");
        return json.ToString();
        }
        catch
        {
            return null;
        }
    }
}
