using DevExpress.Web;
using System;
using System.Data;
using System.Web;
using FusionCharts.Charts;
using System.Text;

public partial class _Estrategias_indicador_graficos_periodo_002 : System.Web.UI.Page
{
    dados cDados;

    int codigoIndicador = 0, codigoUnidade = 0, casasDecimais = 0;
    int codigoEntidadeLogada = 0;
    int codigoUsuarioLogado = 0;

    //pega a data e hora atual do sistema
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";

    //Mensagens de erro para os gráficos
    public string msgErro = "?LoadDataErrorText=" + Resources.traducao.periodo_002_sem_informa__es_a_apresentar;
    public string msgNoData = "&ChartNoDataText=" + Resources.traducao.periodo_002_sem_informa__es_a_apresentar;
    public string msgInvalid = "&InvalidXMLText=" + Resources.traducao.periodo_002_sem_informa__es_a_apresentar;
    public string desenhando = "&PBarLoadingText=" + Resources.traducao.periodo_002_gerando_imagem___;
    public string msgLoading = "&XMLLoadingText=" + Resources.traducao.periodo_002_carregando___;

    public string grafico1_titulo = Resources.traducao.periodo_002_meta_x_resultado;
    public string grafico1_xml = "";
    public string grafico1_xmlzoom = "";
    public string periodo2_jsonzoom = "";

    public int alturaGrafico = 215;
    public int larguraGrafico = 350;
    string tipo = "A";

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

        if (Request.QueryString["NumeroCasas"] != null && Request.QueryString["NumeroCasas"].ToString() != "")
        {
            casasDecimais = int.Parse(Request.QueryString["NumeroCasas"].ToString());
        }

        if (cDados.getInfoSistema("CodigoUnidade") != null && cDados.getInfoSistema("CodigoUnidade").ToString() != "")
            codigoUnidade = int.Parse(cDados.getInfoSistema("CodigoUnidade").ToString());

        codigoIndicador = int.Parse(cDados.getInfoSistema("COIN").ToString());

        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        tipo = configurarTipoDeGrafico();
        //Função que gera o gráfico

      
        this.TH(this.TS("FusionCharts"));

        
        hfGeral.Set("urlAtual", Request.QueryString.ToString());
        
        carregaGrafico();
        cDados.aplicaEstiloVisual(this);

    }

    private string configurarTipoDeGrafico()
    {
        ASPxComboBox combo = ((ASPxComboBox)pGrafico1.FindControl("ddlTipo"));
        DataSet ds = cDados.getParametrosSistema("opcaoPadraoApresentacaoGraficoIndicadores");
        bool indicaOpcaoAcumuladoSelecionado = true;
        indicaOpcaoAcumuladoSelecionado = (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0])) &&
            !(ds.Tables[0].Rows[0]["opcaoPadraoApresentacaoGraficoIndicadores"].ToString() == "Status");

        if (!IsPostBack)
        {
            combo.Value = (indicaOpcaoAcumuladoSelecionado == true) ? "A" : "S";
        }            
        tipo = combo.Value.ToString();
        return tipo;
    }

    //Carrega o gráfico 
    private void carregaGrafico()
    {
        string json1 = "";

        DataSet dsGrafico = cDados.getPeriodicidadeIndicadorGrafico(codigoUnidade, codigoIndicador, tipo);

        if (cDados.DataSetOk(dsGrafico))
        {
            DataTable dt = dsGrafico.Tables[0];
            json1 = getGraficoPeriodosIndicador(dt, Resources.traducao.meta + " X " + Resources.traducao.resultado, 9, casasDecimais, 9, 2, 13);

            string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
            int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
            int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

 

    
            Chart sales = new Chart();
            sales.SetChartParameter(Chart.ChartParameter.chartId, "myChart");
            //Código comentado reflete o modelo anterior scrollcombidy2d
            //sales.SetChartParameter(Chart.ChartParameter.chartType, "scrollcombidy2d"); 
            sales.SetChartParameter(Chart.ChartParameter.chartType, "MSColumnLine3D");

            if (!string.IsNullOrEmpty(Request.QueryString["w"] + "") && !string.IsNullOrEmpty(Request.QueryString["h"] + ""))
            {
                largura = int.Parse(Request.QueryString["w"] + "");
                altura = int.Parse(Request.QueryString["h"] + "");
                sales.SetChartParameter(Chart.ChartParameter.chartWidth, largura + 100);
                sales.SetChartParameter(Chart.ChartParameter.chartHeight, altura + 100);

            }
            else
            {
                sales.SetChartParameter(Chart.ChartParameter.chartWidth, largura - 350);
                sales.SetChartParameter(Chart.ChartParameter.chartHeight, altura - 480);
            }

            sales.SetData(json1);
            Literal1.Text = sales.Render();
        }
    }


    public string getGraficoPeriodosIndicador(DataTable dt, string titulo, int fonte, int casasDecimais, int scrollHeight, int numDivLines, int numVisiblePlot)
    {
        string polaridade = "";
        DataSet dsIndicador = cDados.getIndicadores(codigoEntidadeLogada, codigoUsuarioLogado, "S", "and tb.CodigoIndicador = " + codigoIndicador);
        if (cDados.DataSetOk(dsIndicador) && cDados.DataTableOk(dsIndicador.Tables[0]))
        {
            polaridade = dsIndicador.Tables[0].Rows[0]["Polaridade"].ToString();
        }
        DataSet ds = cDados.getParametrosSistema("ordemApresentacaoGraficoIndicador");
        bool indicaGraficoOrdemAscendente = true;
        indicaGraficoOrdemAscendente = (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0])) &&
            !(ds.Tables[0].Rows[0]["ordemApresentacaoGraficoIndicador"].ToString() == "Descendente");
        int i = 0;


        StringBuilder json = new StringBuilder();
        json.AppendFormat(@"{{""chart"": {{
                ""baseFontSize"" : ""{1}"",
                ""chartBottomMargin"" : ""0"",
                ""chartLeftMargin"" : ""1"",
                ""chartRightMargin"" : ""1"",
                ""chartTopMargin"" : ""3"",
                ""canvasBgColor"" : ""F7F7F7"",
                ""BgColor"" : ""F7F7F7"",
                ""canvasLeftMargin"" : ""0"",
                ""canvasRightMargin"" : ""20"",
                ""canvasBottomMargin"" : ""0"",
                ""numDivLines"" : ""{2}"",
                ""showValues"" : ""1"",
                ""showBorder"" : ""0"",
                ""slantLabels"" : ""1"",
                ""subCaption"": """",
                ""labelDisplay"" : ""NONE"",
                ""yAxisName"": """",
                ""numVisiblePlot"": ""{3}"",
                ""scrollheight"": ""10"",
                ""scrollToEnd"" : ""0"",
                ""flatScrollBars"": ""1"",
                ""scrollShowButtons"": ""0"",
                ""scrollColor"": ""#cccccc"",
                ""rotateLabels"" : ""1"",
                ""labelPadding"" : ""0"",
                ""inDecimalSeparator"" : "","",
                ""inThousandSeparator"" : ""."",
                ""thousandSeparator"" : ""."",
                ""scrollHeight"" : ""{4}"",
                ""showLegend"": ""0"",
                ""theme"": ""fusion"",
                ""decimals"" : ""{5}"",
                ""decimalSeparator"" : "","",
                ""palette"" : ""2"",
                ""yAxisValueFontSize"" : ""6"",
                ""placeValuesInside"" : ""1"",
                ""valueFontBold"" : ""1""
              }},", titulo, fonte, numDivLines, numVisiblePlot, scrollHeight, casasDecimais);

        /*
  b. Regras para DataLabel do RESULTADO:
      1. Colocar DataLabel dentro da barra, 'quase' no topo da barra (JÁ FOI FEITO COM A PROPRIEDADE: ""placeValuesInside"" : ""1"")
 */

        json.AppendFormat(@"
            ""categories"": [
                {{
                    ""category"": [");

        for (i = (indicaGraficoOrdemAscendente == true) ? 0 : dt.Rows.Count - 1; (indicaGraficoOrdemAscendente == true) ? i < dt.Rows.Count : i >= 0; i = (indicaGraficoOrdemAscendente == true) ? i + 1 : i - 1)
        {
            json.AppendFormat("{{");

            string valorMeta = dt.Rows[i]["ValorMeta"].ToString();
            string valorResultado = dt.Rows[i]["ValorRealizado"].ToString();

            string posicao = "";

            if (valorMeta != "" && valorResultado != "")
            {
                if (double.Parse(valorMeta) < double.Parse(valorResultado))
                    posicao = @", ""valuePosition"" : ""BELOW"" ";
                else
                    posicao = @", ""valuePosition"" : ""ABOVE"" ";
            }

            string displayMeta = valorMeta == "" ? "-" : string.Format("{0:n" + casasDecimais + "}", double.Parse(valorMeta));
            string traduzMeta = Resources.traducao.meta;

            json.AppendFormat(@" ""label"" : ""{0}"" , ""fontSize"":""10""  {1}  , ""toolText"" : "" Meta: {2} """, dt.Rows[i]["Periodo"].ToString(), posicao, displayMeta);
            if (indicaGraficoOrdemAscendente == true)
            {
                if (i + 1 == dt.Rows.Count)
                {
                    json.AppendFormat("}}");
                }
                else
                {
                    json.AppendFormat("}},");
                }
            }
            else
            {
                if (i - 1 == 0)
                {
                    json.AppendFormat("}}");
                }
                else
                {
                    json.AppendFormat("}},");
                }
            }
        }

        json.AppendFormat(@"]
                }}
            ],
            ""dataset"": [
                {{
                    ""seriesName"": ""Meta"",
                    ""renderAs"": ""line"",
                    ""showValues"": ""1"",
                    ""anchorRadius"" : ""8"",
                    ""data"": [");


        for (i = (indicaGraficoOrdemAscendente == true) ? 0 : dt.Rows.Count - 1; (indicaGraficoOrdemAscendente == true) ? i < dt.Rows.Count : i >= 0; i = (indicaGraficoOrdemAscendente == true) ? i + 1 : i - 1)
        {
            string valorMeta = dt.Rows[i]["ValorMeta"].ToString();
            string valorResultado = dt.Rows[i]["ValorRealizado"].ToString();

            string posicao = "";
            /*
             a. Regras para DataLabel da META:
                 1. Se polaridade 'POS', mostrar DataLabel 'acima' da linha;
                 2. Se polaridade 'NEG', mostrar DataLabel 'abaixo';
             */
            if (polaridade == "POS")
                posicao = @" , ""valuePosition"" : ""ABOVE""";
            else
                posicao = @" , ""valuePosition"" : ""BELOW""";

            string displayMeta = valorMeta == "" ? "-" : string.Format("{0:n" + casasDecimais + "}", double.Parse(valorMeta));
            string traduzMeta = Resources.traducao.meta;
            json.AppendFormat("{{");
            json.AppendFormat(@" ""value"" : ""{0}"",  ""toolText"" : ""{2}: {1}"" {3}", valorMeta == "" ? "0" : valorMeta, displayMeta, traduzMeta, posicao);

            if (indicaGraficoOrdemAscendente == true)
            {
                if (i + 1 == dt.Rows.Count)
                {
                    json.AppendFormat("}}");
                }
                else
                {
                    json.AppendFormat("}},");
                }
            }
            else
            {
                if (i - 1 == 0)
                {
                    json.AppendFormat("}}");
                }
                else
                {
                    json.AppendFormat("}},");
                }
            }


        }

        json.AppendFormat("]");
        json.AppendFormat("}}");

        json.AppendFormat(@"
                ,{{
                    ""seriesName"": ""Realizado"",
                    ""renderAs"": ""bar"",
                    ""showValues"": ""1"",
                    ""data"": [");

        for (i = (indicaGraficoOrdemAscendente == true) ? 0 : dt.Rows.Count - 1; (indicaGraficoOrdemAscendente == true) ? i < dt.Rows.Count : i >= 0; i = (indicaGraficoOrdemAscendente == true) ? i + 1 : i - 1)
        {
            string corBarra;

            switch (dt.Rows[i]["CorIndicador"].ToString().ToLower())
            {
                case "verde":
                    corBarra = cDados.corSatisfatorio;
                    break;
                case "amarelo":
                    corBarra = cDados.corAtencao;
                    break;
                case "vermelho":
                    corBarra = cDados.corCritico;
                    break;
                case "azul":
                    corBarra = cDados.corExcelente;
                    break;
                default:
                    corBarra = "FFFFFF";
                    break;
            }

            string valorMeta = dt.Rows[i]["ValorMeta"].ToString();
            string valorResultado = dt.Rows[i]["ValorRealizado"].ToString();

            string posicao = "";

            if (valorMeta != "" && valorResultado != "")
            {
                if (double.Parse(valorMeta) >= double.Parse(valorResultado))
                    posicao = @" , ""valuePosition"" : ""BELOW""";
                else
                    posicao = @" , ""valuePosition"" : ""ABOVE""";
            }

            posicao = @" , ""valuePosition"" : ""AUTO""";
            string displayResultado = valorResultado == "" ? "-" : string.Format("{0:n" + casasDecimais + "}", double.Parse(valorResultado));
            string traduzResultado = Resources.traducao.resultado;
            json.AppendFormat(@"{{");
            //json.AppendFormat(@" ""value"" : ""{0}"", ""toolText"" : ""{4}: {1}"" , ""color"" : ""{2}"" {3} ", valorResultado == "" ? "0" : valorResultado.Replace(',', '.'), displayResultado.Replace(',', '.'), corBarra, posicao, traduzResultado);
            json.AppendFormat(@" ""value"" : ""{0}"" ,  ""toolText"" : ""{1}: {0}"" , ""color"" : ""{2}"" {3}", valorResultado == "" ? "0" : displayResultado, traduzResultado, corBarra, posicao);
            if (indicaGraficoOrdemAscendente == true)
            {
                if (i + 1 == dt.Rows.Count)
                {
                    json.AppendFormat("}}");
                }
                else
                {
                    json.AppendFormat("}},");
                }
            }
            else
            {
                if (i - 1 == 0)
                {
                    json.AppendFormat("}}");
                }
                else
                {
                    json.AppendFormat("}},");
                }
            }
        }
        json.AppendFormat("]\n");
        json.AppendFormat("}}\n");
        json.AppendFormat("]\n");
        json.AppendFormat("}}");
        return json.ToString();
    }

    private void defineTamanhoObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int diffLargura = 0;

        if (Request.QueryString["DiffLargura"] != null && Request.QueryString["DiffLargura"].ToString() != "")
        {
            diffLargura = int.Parse(Request.QueryString["DiffLargura"].ToString());
        }

        if (Request.QueryString["AlturaTela"] != null && Request.QueryString["AlturaTela"].ToString() != "")
        {
            alturaGrafico = int.Parse(Request.QueryString["AlturaTela"].ToString()) - 60;
        }
        else
            alturaGrafico = altura - 445;


        larguraGrafico = largura - 600 + diffLargura;
        pGrafico1.ContentHeight = altura - 545 - 10;
    }
}
