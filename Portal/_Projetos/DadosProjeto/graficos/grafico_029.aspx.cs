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

    public string grafico_titulo = "Curva S Física";
    public string grafico_swf = "../../../Flashs/ScrollLine2D.swf";
    public string grafico_swfZoom = "Flashs/ScrollLine2D.swf";
    public string grafico_xml = "";
    public string grafico_xmlzoom = "";

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

        geraGraficoCurva();
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

        pDesempenhoFisico.JSProperties["cp_grafico1_swf"] = grafico_swf;
        pDesempenhoFisico.JSProperties["cp_grafico1_xml"] = grafico_xml;
    }
    
    private void defineTamanhoObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        int larguraPaineis = ((largura) / 2 - 105);
        
        larguraGrafico = larguraPaineis - 10;

        alturaGrafico = (altura - 307) / 2;
    }
}
