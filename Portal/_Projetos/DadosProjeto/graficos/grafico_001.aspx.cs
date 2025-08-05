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
using CDIS;
using System.Collections.Generic;
using System.Text;
using FusionCharts.Charts;
using Newtonsoft.Json;
using System.Dynamic;

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

    public string grafico1_titulo = "Desempenho Físico - Última Linha de Base Salva";
    public string grafico1_xml = "";
    public string grafico1_xmlzoom = "";

    public int alturaGrafico = 220;
    public int larguraGrafico = 350;
    public string grafico1_jsonzoom = "";
    int codigoUsuario;
    int codigoEntidade;
    private string utilizaCronoInstalado = "N";

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

        //Função que gera o gráfico
        carregaSpeedometro();

        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        DataSet dsParam = cDados.getParametrosSistema(codigoEntidade, "utilizaCronoInstalado");

        if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]) && dsParam.Tables[0].Rows[0]["utilizaCronoInstalado"].ToString() != "")
        {
            utilizaCronoInstalado = dsParam.Tables[0].Rows[0]["utilizaCronoInstalado"].ToString();
        }

        bool podeEditarCronograma = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidade, codigoProjeto, "null", "PR", 0, "null", "PR_CadCrn");

        btnEditarCronograma2.ClientVisible = podeEditarCronograma;
        verificaBloqueioCronograma();
        cDados.aplicaEstiloVisual(this);

        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;
    }

    //Carrega o gráfico speedometro
    private void carregaSpeedometro()
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
        grafico1_xml = "../../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        nomeGrafico = "/ArquivosTemporarios/PercentualFisicoZoom_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //gera o xml do gráfico Gauge do percentual concluido ZOOM
        xml = cDados.getGraficoDesempenhoFisico(dtGrafico, dtIndicadores, 15, "./ImageSaving/FusionChartsSave.aspx", "PercentualPrevisto", "PercentualReal", "Percentual Concluído", true);

        //escreve o arquivo XML do gráfico Gauge do percentual concluido ZOOM
        cDados.escreveXML(xml, nomeGrafico);

        grafico1_xmlzoom = nomeGrafico;

        cDados.defineAlturaFrames(this, alturaGrafico + 31);

        var jsonChart = ConverterFormatUtil.convertXMLToJSON(xml);
        jsonChart = jsonChart.Replace("\"", "'");
        dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(jsonChart);
       
        var newObject = (IDictionary<string, object>)obj;
        var chart = (IDictionary<string, object>)newObject["chart"];
        var dial = new List<KeyValuePair<string,
            string>>();
        var dialsOrigin = (IDictionary<string, object>)chart["dials"];       
        var dialOrigin = (IDictionary<string, object>)dialsOrigin["dial"];
        foreach (var d in dialOrigin)
        {
            dial.Add(new KeyValuePair<string, string>("value", d.Value.ToString()));
        }
        var point = new List<KeyValuePair<string,
            string>>();
        var trendpoints = (IDictionary<string, object>)chart["trendpoints"];
        var pointOrigin = (IDictionary<string, object>)trendpoints["point"];
        foreach (var p in pointOrigin)
        {
            point.Add(new KeyValuePair<string, string>(p.Key, p.Value.ToString()));
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

        chart.Remove("colorRange");
        chart.Remove("dials");
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

        StringBuilder points = new StringBuilder();
        points.Append("{\"point\":[{");
        foreach (var pointCnf in point)
        {
            points.AppendFormat("\"{0}\":\"{1}\",", pointCnf.Key, pointCnf.Value);
        }
        points.Replace(",", "}]}", points.Length - 1, 1);
        var objpoints = JsonConvert.DeserializeObject<ExpandoObject>(points.ToString());
        obj.trendpoints = objpoints;

        Session["grafico1_jsonzoom"] = JsonConvert.SerializeObject(obj);
        grafico1_jsonzoom = JsonConvert.SerializeObject(obj);
    }

    private void defineTamanhoObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        int larguraPaineis = ((largura) / 2 - 105);
        int alturaPaineis = altura - 238;
        larguraGrafico = larguraPaineis - 50;

        alturaGrafico = (altura - 309) / 2;
    }

    public string getIconeAlertaCronograma()
    {
        cDados = CdadosUtil.GetCdados(null);
        string retorno = "";
        string corIcone = "";
        string mensagem = "";

        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }

        bool mostrarIcone = cDados.possuiAlertaCronograma(codigoProjeto, ref corIcone, ref mensagem);

        if (mostrarIcone)
        {
            lblAlerta.Text = mensagem;

            retorno = string.Format(@"<td title=""Alerta! Clique aqui para mais informações..."" align=""left"" style=""width:23px"">
                                         <img id=""imgAlerta"" onclick=""popUpAlerta.Show();"" style=""cursor:pointer"" src=""../../../imagens/Alert{0}.png"" />
                                      </td>", corIcone.Trim());
        }

        return retorno;
    }

    private void abreCronograma()
    {
        string msgErro = "";
        int tamanhoCodigoProjeto = codigoProjeto.ToString().Length, regAf = 0;
        string minuto = string.Format("{0:D2}", DateTime.Now.Minute);
        string guid = System.Guid.NewGuid().ToString("D").Replace("-", "");
        string dia = string.Format("{0:D2}", DateTime.Now.Day);
        string hora = string.Format("{0:D2}", DateTime.Now.Hour);

        string stringCrono = tamanhoCodigoProjeto.ToString() + minuto + guid + dia + hora + codigoProjeto;

        bool retorno = cDados.atualizaCronogramaAcessoUsuario(codigoUsuario, stringCrono, ref regAf, ref msgErro);

        if (retorno)
        {
            string corpoTexto = "";
            string extensao = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuario.ToString() + ".tsq";
            string nomeArquivoGravacao = "/ArquivosTemporarios/Crono_" + extensao;
            string urlApp = cDados.getPathSistema();
            string chaveApp = System.Configuration.ConfigurationManager.AppSettings["IDProduto"].ToString();

            chaveApp = chaveApp.Replace("-", "").Replace("{", "").Replace("}", "");

            string identificacaoProduto = string.Format("{0} - {1:dd/MM/yyyy}",
                System.Configuration.ConfigurationManager.AppSettings["nomeSistema"].ToString()
               , DateTime.Now);

            string chaveCriptografada = Cripto.criptografar(stringCrono + urlApp, chaveApp);

            corpoTexto = identificacaoProduto + Environment.NewLine +
                chaveApp + Environment.NewLine +
                chaveCriptografada;

            cDados.escreveTexto(corpoTexto, nomeArquivoGravacao);

            Response.Clear();
            Response.Buffer = false;
            Response.AppendHeader("Content-Type", "application/unknown");
            Response.AppendHeader("Content-Transfer-Encoding", "binary");
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"Crono_" + extensao + "\"");
            Response.WriteFile(urlApp + nomeArquivoGravacao.Substring(1));
            Response.End();

            //callbackEditarCronograma.JSProperties["cp_Crono"] = urlApp + nomeArquivoGravacao.Substring(1);            
        }
    }

    private void verificaBloqueioCronograma()
    {
        DataSet ds = cDados.getCronogramasComCheckout(codigoEntidade, " AND cp.CodigoProjeto = " + codigoProjeto);

        string eventoBotaoEditarCrono = "", processOnServer = "";

        if (utilizaCronoInstalado == "S")
        {
            eventoBotaoEditarCrono = "pcDownload.Show();";
        }
        else
        {
            string linkOpcao = cDados.getLinkPortalDesktop(Request.Url, codigoEntidade, codigoUsuario, codigoProjeto, "./../../../");
            eventoBotaoEditarCrono = "window.open('" + linkOpcao + "', 'framePrincipal');";
            processOnServer = "e.processOnServer = false;";
        }

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string mensagem = Resources.traducao.grafico_001_o_cronograma_est__bloqueado_com_o_usu_rio_ + ds.Tables[0].Rows[0]["NomeUsuarioCheckoutCronograma"] + "."
                + Environment.NewLine + Resources.traducao.grafico_001_deseja_abrir_o_cronograma_somente_para_leitura_;

            lblInformacao.Text = mensagem;

            btnEditarCronograma2.ClientSideEvents.Click = "function(s, e) {pcInformacao.Show();e.processOnServer = false;}";
            btnAbrirCronoBloqueado.ClientSideEvents.Click = "function(s, e) {pcInformacao.Hide();" + eventoBotaoEditarCrono + processOnServer + "}";

            string mensagemDesbloqueio = Resources.traducao.grafico_001_o_cronograma_est__bloqueado_com_o_usu_rio_ + ds.Tables[0].Rows[0]["NomeUsuarioCheckoutCronograma"] + "."
                + Environment.NewLine + Resources.traducao.grafico_001_ao_fazer_o_desbloqueio_as_atualiza__es_pendentes_ser_o_perdidas__deseja_realmente_desbloquear_o_projeto_;

            lblDesbloqueio.Text = mensagemDesbloqueio;
        }
        else
        {
            btnEditarCronograma2.ClientSideEvents.Click = "function(s, e) {" + eventoBotaoEditarCrono + processOnServer + "}";
        }
    }

    protected void btnEditarCronograma_Click(object sender, EventArgs e)
    {
        abreCronograma();
    }

    protected void btnDesbloquearCrono_Click(object sender, EventArgs e)
    {
        // ACG: 04/10/2015 - O segundo parametro é para desbloquear cronogramas de replanejamento
        cDados.atualizaCronogramaCheckin(codigoProjeto, "");
        verificaBloqueioCronograma();
    }
}
