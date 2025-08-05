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

    public string grafico1_titulo = "Desempenho Geral";
    public string grafico1_swf = "../../../Flashs/Bar2D.swf";
    public string grafico1_swfzoom = "../../../Flashs/Bar2D.swf";
    public string grafico1_xml = "";
    public string grafico1_xmlzoom = "";

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
            carregaGraficoDesempenho();
        }

        cDados.aplicaEstiloVisual(this);
    }

    //Carrega o gráfico speedometro
    private void carregaGraficoDesempenho()
    {
        grafico1_swf = "../../../Flashs/Bar2D.swf";
        grafico1_swfzoom = "Flashs/Bar2D.swf";

        string xml = "";

       

        string nomeGrafico = @"/ArquivosTemporarios/DesempenhoObra_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        int codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        DataSet dsGrafico = cDados.getDesempenhoGeralObra(codigoProjeto, codigoEntidadeLogada, "N", "");

        DataTable dtGrafico = dsGrafico.Tables[0];

        //gera o xml do gráfico Gauge do percentual concluido
        xml = cDados.getGraficoDesempenhoGeralObra(dtGrafico, "", 9, false, codigoProjeto, false);

        //escreve o arquivo XML do gráfico Gauge do percentual concluido
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico1_xml = "../../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        nomeGrafico = "/ArquivosTemporarios/DesempenhoObraZoom_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //gera o xml do gráfico Gauge do percentual concluido ZOOM
        xml = cDados.getGraficoDesempenhoGeralObra(dtGrafico, "Desempenho Geral", 15, false, -1, false);

        //escreve o arquivo XML do gráfico Gauge do percentual concluido ZOOM
        cDados.escreveXML(xml, nomeGrafico);

        grafico1_xmlzoom = nomeGrafico;

        cDados.defineAlturaFrames(this, alturaGrafico + 31);

        ASPxRoundPanel1.ClientInstanceName = "pc";
        ASPxRoundPanel1.ClientSideEvents.Init = "OnInit";
        ASPxRoundPanel1.JSProperties["cp_grafico1_swf"] = grafico1_swf;
        ASPxRoundPanel1.JSProperties["cp_grafico1_xml"] = grafico1_xml;
    }

    private void geraGraficoCurva()
    {
        grafico1_swf = "../../../Flashs/ScrollLine2D.swf";
        grafico1_swfzoom = "Flashs/ScrollLine2D.swf";

        //cria  a variável para armazenar o XML
        string xml = "";
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        string nomeGrafico = @"/ArquivosTemporarios/CurvaS_002_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        DataTable dt = cDados.getDesempenhoCurvaSFinanceiraObjeto(codigoEntidade, codigoUsuario, codigoProjeto.ToString(), "NULL", "PROJ").Tables[0];

        //função para gerar a estrutura xml para criação do gráfico
        xml = cDados.getGraficoCurvaSFinanceiraRelatorio(dt, "", 9, 0, 9, 4, 25);

        //escreve o arquivo xml
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico1_xml = "../../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        nomeGrafico = "/ArquivosTemporarios/CurvaS_002_ZOOM" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //gera o xml do gráfico Gauge do percentual concluido ZOOM
        xml = cDados.getGraficoCurvaSFinanceiraRelatorio(dt, "Curva S Orçamento",15, 0, 12, 4, 25);

        //escreve o arquivo XML do gráfico Gauge do percentual concluido ZOOM
        cDados.escreveXML(xml, nomeGrafico);

        grafico1_xmlzoom = nomeGrafico;
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
        if (((ASPxRadioButtonList)sender).Value.ToString() == "D")
        {
            carregaGraficoDesempenho();
        }
        else
        {
            //Função que gera o gráfico
            geraGraficoCurva();
        }

        cDados.aplicaEstiloVisual(this);
    }
}
