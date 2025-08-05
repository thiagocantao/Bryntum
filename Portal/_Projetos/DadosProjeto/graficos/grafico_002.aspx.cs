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
using Newtonsoft.Json;
using System.Dynamic;
using System.Collections.Generic;
using System.Text;

public partial class grafico_002 : System.Web.UI.Page
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

    public string grafico2_titulo = "Desempenho Geral do Projeto";
    public string grafico2_xml = "";

    public string grafico3_xml = "";
    public string grafico3_xmlzoom = "";

    public string grafico4_xml = "";
    public string grafico4_xmlzoom = "";

    public string grafico002_1_jsonzoom = "";
    public string grafico002_2_jsonzoom = "";
    public string grafico002_3_jsonzoom = "";

    string valorLimite;

    public int alturaGrafico = 229;
    public int larguraGrafico = 350;

    public string mostrarReceita = "S";
    public string mostrarDespesa = "S";
    public string mostrarEsforco = "S";

    int numeroGraficos = 3;

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

        DataSet dsParametros = cDados.getParametrosSistema("mostrarValoresReceita", "mostrarValoresDespesa", "mostrarValoresEsforco");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            if (dsParametros.Tables[0].Rows[0]["mostrarValoresReceita"].ToString() == "N")
            {
                mostrarReceita = "N";
                numeroGraficos--;
            }

            if (dsParametros.Tables[0].Rows[0]["mostrarValoresDespesa"].ToString() == "N")
            {
                mostrarDespesa= "N";
                numeroGraficos--;
            }

            if (dsParametros.Tables[0].Rows[0]["mostrarValoresEsforco"].ToString() == "N")
            {
                mostrarEsforco = "N";
                numeroGraficos--;
            }
        }

        defineTamanhoObjetos();

        //Função que gera o gráfico
        geraGraficosBullets();

        cDados.defineAlturaFrames(this, (alturaGrafico * numeroGraficos) + 30);

        cDados.aplicaEstiloVisual(this);
    }

    private void geraGraficosBullets()
    {
        string where = "";

        int anoFinanceiro = Request.QueryString["Financeiro"] != null && Request.QueryString["Financeiro"].ToString() != "" ? int.Parse(Request.QueryString["Financeiro"].ToString()) : -1;

        //Data Set contendo a tabela com os dados a serem carregados no gráfico 
        DataSet ds = cDados.getNumerosDesempenhoProjeto(codigoProjeto, anoFinanceiro, where);

        DataTable dt = ds.Tables[0];

        DataSet dsBullets = cDados.getCoresBulletsProjeto(codigoProjeto, anoFinanceiro, "");

        DataTable dtBullets = dsBullets.Tables[0];

        valorLimite = getValorLimiteGrafico(dt).ToString();

        //Função que gera o gráfico 1
        
        geraGrafico1(dt, dtBullets);

        //Função que gera o gráfico 2
        geraGrafico2(dt, dtBullets);

        //Função que gera o gráfico 3
        geraGrafico3(dt, dtBullets);

    }

    //Função para geração do gráfico 1
    public void geraGrafico1(DataTable dt, DataTable dtBullets)
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico Bullet
        string nomeGrafico;

        //*****************
        //Criação do Bullet de Esforço
        //cria uma variável com o nome e o caminho do XML do gráfico BULLET de esforço dos Projetos
        nomeGrafico = @"/ArquivosTemporarios/bulletEsforco_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função que cria a string XML que irá carregar o gráfico de Bullets de Esforço
        xml = cDados.getGraficoBulletsSinalizadores(dt, dtBullets, "Esforço", "TotalTrabalhoPrevisto", "TotalTrabalhoPrevistoGeral", "TotalTrabalhoReal", valorLimite, "../../ImageSaving/FusionChartsSave.aspx", "(Em Horas)", 9, "");

        //Cria o arquivo XML
        cDados.escreveXML(xml, nomeGrafico);

        //Atribui o nome e o caminho do arquivo que irá carregar o gráfico
        grafico2_xml = "../../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //cria uma variável com o nome e o caminho do XML do gráfico BULLET de esforço dos Projetos
        nomeGrafico = "ArquivosTemporarios/bulletEsforcoZoom_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função que cria a string XML que irá carregar o gráfico de Bullets de Esforço de ZOOM
        xml = cDados.getGraficoBulletsSinalizadores(dt, dtBullets, "Esforço", "TotalTrabalhoPrevisto", "TotalTrabalhoPrevistoGeral", "TotalTrabalhoReal", valorLimite, "../../ImageSaving/FusionChartsSave.aspx", "(Em Horas)", 15, "");

        dynamic obj = buildDynamicObjectByXML(xml);
        Session["grafico002_1_jsonzoom"] = JsonConvert.SerializeObject(obj);
        grafico002_1_jsonzoom = JsonConvert.SerializeObject(obj);

    }

    //Função para geração do gráfico 2
    public void geraGrafico2(DataTable dt, DataTable dtBullets)
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico 2
        string nomeGrafico;

        //cria uma variável com o nome e o caminho do XML do gráfico bullet de custo dos Projetos 
        nomeGrafico = "/ArquivosTemporarios/bulletCustos_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função que cria a string XML que irá carregar o gráfico de Bullets de Custos
        xml = cDados.getGraficoBulletsSinalizadores(dt, dtBullets, "Despesa", "TotalCustoOrcado", "TotalCustoOrcadoGeral", "TotalCustoReal", valorLimite, "../../ImageSaving/FusionChartsSave.aspx", "(R$)", 9, "");

        //Cria o arquivo XML
        cDados.escreveXML(xml, nomeGrafico);

        //Atribui o nome e o caminho do arquivo XML que carregará o gráfico
        grafico3_xml = "../../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //cria uma variável com o nome e o caminho do XML do gráfico bullet de custo dos Projetos 
        nomeGrafico = "ArquivosTemporarios/bulletCustosZoom_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função que cria a string XML que irá carregar o gráfico de Bullets de Custos de ZOOM
        xml = cDados.getGraficoBulletsSinalizadores(dt, dtBullets, "Despesa", "TotalCustoOrcado", "TotalCustoOrcadoGeral", "TotalCustoReal", valorLimite, "../../ImageSaving/FusionChartsSave.aspx", "(R$)", 15, "");

        //Cria o arquivo XML
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado (ZOOM)
        grafico3_xmlzoom = nomeGrafico;

        dynamic obj = buildDynamicObjectByXML(xml);
        Session["grafico002_2_jsonzoom"] = JsonConvert.SerializeObject(obj);
        grafico002_2_jsonzoom = JsonConvert.SerializeObject(obj);
    }

    //Função para geração do gráfico 1
    public void geraGrafico3(DataTable dt, DataTable dtBullets)
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico 3
        string nomeGrafico;

        //cria uma variável com o nome e o caminho do XML do gráfico bullet de custo dos Projetos 
        nomeGrafico = "/ArquivosTemporarios/bulletReceitas_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função que cria a string XML que irá carregar o gráfico de Bullets de Receitas
        xml = cDados.getGraficoBulletsSinalizadores(dt, dtBullets, "Receita", "TotalReceitaOrcada", "TotalReceitaOrcadaGeral", "TotalReceitaReal", valorLimite, "../../ImageSaving/FusionChartsSave.aspx", "(R$)", 9, "");

        //Cria o arquivo XML
        cDados.escreveXML(xml, nomeGrafico);

        //Atribui o nome e o caminho do arquivo XML que irá carregar o gráfico
        grafico4_xml = "../../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //cria uma variável com o nome e o caminho do XML do gráfico bullet de custo dos Projetos 
        nomeGrafico = "ArquivosTemporarios/bulletReceitasZoom_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função que cria a string XML que irá carregar o gráfico de Bullets de Receitas de ZOOM
        xml = cDados.getGraficoBulletsSinalizadores(dt, dtBullets, "Receita", "TotalReceitaOrcada", "TotalReceitaOrcadaGeral", "TotalReceitaReal", valorLimite, "../../ImageSaving/FusionChartsSave.aspx", "(R$)", 15, "");

        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado (ZOOM)
        grafico4_xmlzoom = nomeGrafico;
        dynamic obj = buildDynamicObjectByXML(xml);
        Session["grafico002_3_jsonzoom"] = JsonConvert.SerializeObject(obj);
        grafico002_3_jsonzoom = JsonConvert.SerializeObject(obj);
    }
    private dynamic buildDynamicObjectByXML(string xml)
    {
        var jsonChart = ConverterFormatUtil.convertXMLToJSON(xml);
        jsonChart = jsonChart.Replace("\"", "'");
        dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(jsonChart);

        var newObject = (IDictionary<string, object>)obj;
        var chart = (IDictionary<string, object>)newObject["chart"];
        if (chart.ContainsKey("colorRange") && chart["colorRange"] != null && chart["colorRange"].ToString() != "")
        {
            StringBuilder range = new StringBuilder();
            range.Append("{\"color\":[");
            var colorRange = (IDictionary<string, object>)chart["colorRange"];
            foreach(var color in colorRange)
            {
                var objJson = JsonConvert.SerializeObject(color.Value);
                dynamic col = JsonConvert.DeserializeObject<ExpandoObject>(objJson);
                range.AppendFormat("{{\"minValue\":\"{0}\",\"maxValue\":\"{1}\",\"code\":\"{2}\"}},", col.minValue, col.maxValue, col.code);
            }
            range.Replace(",", "]}", range.Length - 1, 1);
            chart.Remove("colorRange");

            var objColor = JsonConvert.DeserializeObject<ExpandoObject>(range.ToString());
            obj.colorRange = objColor;
        }
        chart.Remove("baseFontSize");
        obj.chart.baseFontSize = "9";
        return obj;
    }
    private int getValorLimiteGrafico(DataTable dt)
    {
        int i;
        int valorLimite = 0;
        float custoPrevisto = 0;
        float receitaPrevista = 0;
        float custoReal = 0;
        float receitaReal = 0;

        for (i = 0; i < dt.Rows.Count; i++)
        {
            custoPrevisto = custoPrevisto + float.Parse(dt.Rows[i]["TotalCustoOrcadoGeral"].ToString());
            custoReal = custoReal + float.Parse(dt.Rows[i]["TotalCustoReal"].ToString());
            receitaPrevista = receitaPrevista + float.Parse(dt.Rows[i]["TotalReceitaOrcadaGeral"].ToString());
            receitaReal = receitaReal + float.Parse(dt.Rows[i]["TotalReceitaReal"].ToString());
        }

        if (custoPrevisto < custoReal)
        {
            custoPrevisto = custoReal;
        }
        if (receitaPrevista < receitaReal)
        {
            receitaPrevista = receitaReal;
        }

        if (custoPrevisto > receitaPrevista)
        {
            valorLimite = (int)custoPrevisto + ((5 * (int)custoPrevisto) / 100);
        }
        else
        {
            valorLimite = (int)receitaPrevista + ((5 * (int)receitaPrevista) / 100); ;
        }

        return valorLimite;
    }

    private void defineTamanhoObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        int larguraPaineis = ((largura) / 2 - 105);
        int alturaPaineis = altura - 235;
        larguraGrafico = larguraPaineis - 10;

        alturaGrafico = (((altura - 308) / 2) / numeroGraficos);
    }
}
