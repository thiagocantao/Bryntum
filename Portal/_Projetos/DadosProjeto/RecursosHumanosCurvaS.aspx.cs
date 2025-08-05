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
using DevExpress.XtraPivotGrid.Localization;
using DevExpress.Utils.Localization.Internal;
using System.IO;
using DevExpress.XtraPivotGrid;
using Newtonsoft.Json;
using System.Dynamic;
using System.Text;
using System.Collections.Generic;
using FusionCharts.Charts;

public partial class _Projetos_DadosProjeto_RecursosHumanos : System.Web.UI.Page
{
    dados cDados;

    int idProjeto = 0;

    //Mensagens de erro para os gráficos
    public string msgErro = "?LoadDataErrorText=Sem Informações a Apresentar";
    public string msgNoData = "&ChartNoDataText=Sem Informações a Apresentar";
    public string msgInvalid = "&InvalidXMLText=Sem Informações a Apresentar";
    public string desenhando = "&PBarLoadingText=Gerando imagem...";
    public string msgLoading = "&XMLLoadingText=Carregando...";

    public string grafico1_titulo = "Curva S de Alocação de Recursos";
    public string grafico21_jsonzoom = "";

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

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));

        if (Request.QueryString["idProjeto"] != null)
            idProjeto = int.Parse(Request.QueryString["idProjeto"].ToString());


        int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuario, codigoEntidade, idProjeto, "null", "PR", 0, "null", "PR_AcsTlaRHU");
        }


        cDados.aplicaEstiloVisual(Page);

        DataSet ds = cDados.getRecursosHumanosProjetoCurvaS(idProjeto, codigoEntidade, codigoUsuario);

        if (cDados.DataSetOk(ds))
        {
            string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
            int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
            int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

            grafico21_jsonzoom = getGraficoProjetoCurvaS(ds.Tables[0], "Curva S de Alocação de Recursos", 15, 2, 9, 2, 25);
            Session["grafico21_jsonzoom"] = grafico21_jsonzoom;
            Chart sales = new Chart();
            sales.SetChartParameter(Chart.ChartParameter.chartId, "myChart");
            sales.SetChartParameter(Chart.ChartParameter.chartType, "multiaxisline");
            sales.SetChartParameter(Chart.ChartParameter.chartWidth, largura - 350);
            sales.SetChartParameter(Chart.ChartParameter.chartHeight, altura - 270);
            //sales.SetChartParameter(Chart.ChartParameter.renderAt, "divchart");
            sales.SetData(grafico21_jsonzoom);
            Literal1.Text = sales.Render();
        }

        imgGraficos.ClientSideEvents.Click = "function(s, e) {window.location.href = 'RecursosHumanos.aspx?CurvaS&idProjeto=" + idProjeto + "';}";
        imgGraficos.Style.Add("cursor", "pointer");
        lblGrafico.ClientSideEvents.Click = "function(s, e) {window.location.href = 'RecursosHumanos.aspx?CurvaS&idProjeto=" + idProjeto + "';}";
        lblGrafico.Style.Add("cursor", "pointer");
    }


    public string getGraficoProjetoCurvaS(DataTable dt, string titulo, int fonte, int casasDecimais, int scrollHeight, int numDivLines, int numVisiblePlot)
    {
        //Cria as variáveis para a formação do XML
        StringBuilder xml = new StringBuilder();
        StringBuilder json = new StringBuilder();

        int i = 0;


        json.AppendFormat(@"{{
    ""chart"": {{
        ""showvalues"": ""0"",
        ""labeldisplay"": ""NONE"",
        ""rotatelabels"": ""45"",
        ""plothighlighteffect"": ""fadeout"",
        ""plottooltext"": ""$seriesName in $label : <b>$dataValue</b>"",
        ""theme"": ""fusion""
    }},
    ""axis"": [
        {{
            ""title"": """",
            ""numberprefix"": """",
            ""divlineisdashed"": ""1"",
           ""dataset"": [
                {{
                    ""seriesname"": ""Previsto"",
                    ""linethickness"": ""3"",
                    ""data"": [");

        for (i = 0; i < dt.Rows.Count; i++)
        {
            string valor = dt.Rows[i]["PercentualPrevisto"].ToString();
            string valorPeriodo = dt.Rows[i]["PrevistoPeriodo"].ToString();
            string valorAcumulado = dt.Rows[i]["PrevistoAcumulado"].ToString();

            string displayValue = valor == "" ? "-" : string.Format("{0:n" + casasDecimais + "}", double.Parse(valor));
            string displayValuePeriodo = valorPeriodo == "" ? "-" : string.Format("{0:n" + casasDecimais + "}", double.Parse(valorPeriodo));
            string displayValueAcumulado = valorAcumulado == "" ? "-" : string.Format("{0:n" + casasDecimais + "}", double.Parse(valorAcumulado));

            json.AppendFormat(@"{{");
            json.AppendFormat(string.Format(@" ""value"": ""{0}"" ", valor));
            json.AppendFormat(@"}} {0}", (i + 1) == dt.Rows.Count ? "," : ",");
        }
        json.AppendFormat(@"
                    ]
                }}
            ]
        }},
        {{
            ""title"": """",
            ""axisonleft"": ""0"",
            ""titlepos"": ""left"",
            ""numdivlines"": ""8"",
            ""divlineisdashed"": ""1"",
            ""showYAxisValues"" : ""0"",
            ""minimiseWrappingInLegend"" : ""1"",
            ""showAxis"" : ""0"",
            ""dataset"": [
                {{
                    
                    ""seriesname"": ""Realizado"",
                    ""dashed"": ""0"",
                    ""showAxis"" : ""0"",
                    ""data"": [");

        for (i = 0; i < dt.Rows.Count; i++)
        {
            string valor = dt.Rows[i]["PercentualReal"].ToString();
            string valorPeriodo = dt.Rows[i]["RealPeriodo"].ToString();
            string valorAcumulado = dt.Rows[i]["RealAcumulado"].ToString();

            string displayValue = valor == "" ? "-" : string.Format("{0:n" + casasDecimais + "}", double.Parse(valor));
            string displayValuePeriodo = valorPeriodo == "" ? "-" : string.Format("{0:n" + casasDecimais + "}", double.Parse(valorPeriodo));
            string displayValueAcumulado = valorAcumulado == "" ? "-" : string.Format("{0:n" + casasDecimais + "}", double.Parse(valorAcumulado));

            json.AppendFormat(@"{{");
            json.AppendFormat(@" ""value"" : ""{0}"" ", valor);
            json.AppendFormat(@"}} {0}", (i + 1 == dt.Rows.Count) ? "" : ",");
        }

        json.AppendFormat(@"
                    ]
                }}
            ]
        }},
    ],
    ""categories"": [
        {{
            ""category"": [");

        for (i = 0; i < dt.Rows.Count; i++)
        {
            json.AppendFormat(@" {{ ");
            json.AppendFormat(@" ""label"" : ""{0}"" ", dt.Rows[i]["Periodo"].ToString());
            if (i + 1 == dt.Rows.Count)
            {
                json.AppendFormat(@" }} ");
            }
            else
            {
                json.AppendFormat(@" }}, ");
            }
        }

        json.AppendFormat(@" 
            ]
        }}
    ]
}}");

        return json.ToString();
    }


    protected void callbackGrafico_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {

    }
}
