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

public partial class _Estrategias_indicador_graficos_grafico_002 : System.Web.UI.Page
{
    dados cDados;

    int codigoIndicador = 0, codigoUnidade = 0, mes = 0, ano = 0, casasDecimais = 0;
    string polaridade = "";

    char tipoDesempenho = 'A';

    //pega a data e hora atual do sistema
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";

    //Mensagens de erro para os gráficos
    public string msgErro = "?LoadDataErrorText=" + Resources.traducao.grafico_002_sem_informa__es_a_apresentar;
    public string msgNoData = "&ChartNoDataText=" + Resources.traducao.grafico_002_sem_informa__es_a_apresentar;
    public string msgInvalid = "&InvalidXMLText=" + Resources.traducao.grafico_002_sem_informa__es_a_apresentar;
    public string desenhando = "&PBarLoadingText=" + Resources.traducao.grafico_002_gerando_imagem___;
    public string msgLoading = "&XMLLoadingText=" + Resources.traducao.grafico_002_carregando___;

    public string grafico1_titulo = Resources.traducao.grafico_002_desempenho_do_indicador;
    public string grafico1_swf = "../../../Flashs/AngularGauge.swf";
    public string grafico1_xml = "";
    public string grafico1_xmlzoom = "";

    public string grafico2_titulo = "Comparação";
    public string grafico2_swf = "../../../Flashs/MSCombi2D.swf";
    public string grafico2_xml = "";
    public string grafico2_xmlzoom = "";

    public string grafico3_xml = "";
    public string grafico3_xmlzoom = "";

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

        defineTamanhoObjetos();

        //Função que gera o gráfico
        carregaGraficoComparacao();
        carregaGraficoDesempenho();

        cDados.aplicaEstiloVisual(this);
    }

    //Carrega o gráfico 
    private void carregaGraficoDesempenho()
    {
        string xml = "";

        string nomeGrafico = @"/ArquivosTemporarios/desempenhoIndicador_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        DataSet dsGrafico = cDados.getDesempenhoIndicadorUnidade(codigoIndicador, mes, ano, tipoDesempenho, codigoUnidade, int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), "");

        if (cDados.DataSetOk(dsGrafico))
        {
            DataTable dt = dsGrafico.Tables[0];

            //gera o xml do gráfico Gauge do percentual concluido
            xml = cDados.getGraficoDesempenhoIndicador(dt, 9, "", casasDecimais, polaridade,codigoIndicador);

            //escreve o arquivo XML do gráfico Gauge do percentual concluido
            cDados.escreveXML(xml, nomeGrafico);

            //atribui o valor do caminho do XML a ser carregado
            grafico1_xml = "../../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

            nomeGrafico = "/ArquivosTemporarios/desempenhoIndicadorZoom_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

            //gera o xml do gráfico Gauge do percentual concluido ZOOM
            xml = cDados.getGraficoDesempenhoIndicador(dt, 15, Resources.traducao.grafico_002_desempenho_do_indicador, casasDecimais, polaridade, codigoIndicador);
            //escreve o arquivo XML do gráfico Gauge do percentual concluido ZOOM
            cDados.escreveXML(xml, nomeGrafico);
            
            grafico1_xmlzoom = nomeGrafico;
        }
    }

    private void carregaGraficoComparacao()
    {
        string xml = "";

        string nomeGrafico = @"/ArquivosTemporarios/comparacaoIndicadorEntidade_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        DataSet dsGrafico = cDados.getComparacaoIndicador(codigoUnidade, codigoIndicador, mes, ano, "");

        

        if (cDados.DataSetOk(dsGrafico))
        {
            DataTable dt = dsGrafico.Tables[0];

            //gera o xml do gráfico Gauge do percentual concluido
            xml = cDados.getGraficoComparacaoIndicador(dt, "", 9, "BR", casasDecimais);

            //escreve o arquivo XML do gráfico Gauge do percentual concluido
            cDados.escreveXML(xml, nomeGrafico);

            //atribui o valor do caminho do XML a ser carregado
            grafico2_xml = "../../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

            pGrafico2.JSProperties["cp_grafico01"] = xml;

            pGrafico2.JSProperties["cp_alturaGrafico"] = alturaGrafico;

            nomeGrafico = @"/ArquivosTemporarios/comparacaoIndicadorRegiao_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

            //gera o xml do gráfico Gauge do percentual concluido
            xml = cDados.getGraficoComparacaoIndicador(dt, "", 9, "RG", casasDecimais);
            
            pGrafico2.JSProperties["cp_grafico02"] = xml;

            //escreve o arquivo XML do gráfico Gauge do percentual concluido
            cDados.escreveXML(xml, nomeGrafico);

            //atribui o valor do caminho do XML a ser carregado
            grafico3_xml = "../../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

            
            nomeGrafico = "/ArquivosTemporarios/desempenhoIndicadorEntidadeZoom_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

            //gera o xml do gráfico Gauge do percentual concluido ZOOM
            xml = cDados.getGraficoComparacaoIndicador(dt, Resources.traducao.grafico_002_compara__o_do_indicador___brasil, 15, "BR", casasDecimais);
            

            //escreve o arquivo XML do gráfico Gauge do percentual concluido ZOOM
            cDados.escreveXML(xml, nomeGrafico);

            grafico2_xmlzoom = nomeGrafico;

            pGrafico2.JSProperties["cp_graficoZoom1"] = grafico2_xmlzoom;
            pGrafico2.JSProperties["cp_graficoZoom"] = grafico2_xmlzoom;

            nomeGrafico = "/ArquivosTemporarios/desempenhoIndicadorRegiaoZoom_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

            //gera o xml do gráfico Gauge do percentual concluido ZOOM
            xml = cDados.getGraficoComparacaoIndicador(dt, Resources.traducao.grafico_002_compara__o_do_indicador___regi_o, 15, "RG", casasDecimais);

            //escreve o arquivo XML do gráfico Gauge do percentual concluido ZOOM
            cDados.escreveXML(xml, nomeGrafico);

            grafico3_xmlzoom = nomeGrafico;

            pGrafico2.JSProperties["cp_graficoZoom2"] = grafico3_xmlzoom;
        }
    }

    private void defineTamanhoObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;
        alturaGrafico = (altura - 316) / 2;
        pGrafico1.ContentHeight = (altura - 335) / 2;
        pGrafico2.ContentHeight = (altura - 350) / 2;
    }
}
