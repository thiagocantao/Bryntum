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

public partial class _VisaoMaster_Graficos_vm_005 : System.Web.UI.Page
{
    dados cDados;

    int codigoEntidade = 0;

    //pega a data e hora atual do sistema
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";

    //Mensagens de erro para os gráficos
    public string msgErro = "?LoadDataErrorText=Sem Informações a Apresentar";
    public string msgNoData = "&ChartNoDataText=Sem Informações a Apresentar";
    public string msgInvalid = "&InvalidXMLText=Sem Informações a Apresentar";
    public string desenhando = "&PBarLoadingText=Gerando imagem...";
    public string msgLoading = "&XMLLoadingText=Carregando...";

    public string grafico1_titulo = "Desempenho Econômico";
    public string grafico1_swf = "../../Flashs/AngularGauge.swf";
    public string grafico1_xml = "";
    public string grafico1_xmlzoom = "", toolTip = "";

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

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        defineTamanhoObjetos();

        //Função que gera o gráfico
        carregaSpeedometro();


    }

    //Carrega o gráfico speedometro
    private void carregaSpeedometro()
    {
        string xml = "";

        toolTip = string.Format(@"Objetivo/descrição - Apresentar o desempenho econômico da UHE Belo Monte, referente à Área selecionada (Civil / Fornecimento e Montagem). O velocímetro sinaliza o desempenho em 3 níveis (verde, amarelo e vermelho). O ponteiro indica o realizado e a linha pontilhada o previsto.");

        string nomeGrafico = @"/ArquivosTemporarios/SpeedometroFinanceiro_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        string codigoReservado = Request.QueryString["CR"] == null ? "" : Request.QueryString["CR"].ToString();
        string codigoArea = Request.QueryString["CA"] == null ? "" : Request.QueryString["CA"].ToString();

        int diasTolerancia = cDados.getDiasToleranciaUHE();

        DateTime dataParam = DateTime.Now.Day < diasTolerancia ? DateTime.Now.AddMonths(-2) : DateTime.Now.AddMonths(-1);

        grafico1_titulo = string.Format("Desempenho Econômico até {0:MMM/yyyy}", dataParam);

        DataSet dsGrafico = cDados.getDesempenhoFinanceiroPainelGerenciamento(codigoReservado, codigoEntidade, int.Parse(codigoArea), "");

        DataTable dtGrafico = dsGrafico.Tables[0];

        dsGrafico = cDados.getIndicadorDesempenhoFinanceiro("");

        DataTable dtIndicadores = dsGrafico.Tables[0];

        //gera o xml do gráfico Gauge do percentual concluido
        xml = cDados.getGraficoDesempenhoPaineisUHE(dtGrafico, dtIndicadores, 9, "./ImageSaving/FusionChartsSave.aspx", "PercentualPrevisto", "PercentualReal", "", true);

        //escreve o arquivo XML do gráfico Gauge do percentual concluido
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico1_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        nomeGrafico = "/ArquivosTemporarios/PercentualFinanceiroZoom_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //gera o xml do gráfico Gauge do percentual concluido ZOOM
        xml = cDados.getGraficoDesempenhoPaineisUHE(dtGrafico, dtIndicadores, 15, "./ImageSaving/FusionChartsSave.aspx", "PercentualPrevisto", "PercentualReal", grafico1_titulo, true);

        //escreve o arquivo XML do gráfico Gauge do percentual concluido ZOOM
        cDados.escreveXML(xml, nomeGrafico);

        grafico1_xmlzoom = nomeGrafico;

        cDados.defineAlturaFrames(this, alturaGrafico + 30);

        string nomeArquivoASPX = Request.FilePath.Substring(Request.FilePath.LastIndexOf("/") + 1).Replace(".aspx", "");
        ASPxLabel1.ClientSideEvents.Click = "function(s, e) {if(window.parent.parent.callbackMetrica) window.parent.parent.callbackMetrica.PerformCallback('" + nomeArquivoASPX + ";" + codigoReservado + "'); } ";
        ASPxLabel1.Cursor = "Pointer";
        ASPxLabel1.ToolTip = "Clique aqui para visualizar a métrica e última atualização";
    }

    private void defineTamanhoObjetos()
    {
        int largura, altura;

        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

        if (Request.QueryString["Altura"] != null && Request.QueryString["Largura"] != null)
        {
            largura = int.Parse(Request.QueryString["Largura"].ToString());
            altura = int.Parse(Request.QueryString["Altura"].ToString());

            int larguraPaineis = ((largura - 10));
            int alturaPaineis = altura - 10;

            pDesempenhoFisico.Width = larguraPaineis;
            larguraGrafico = larguraPaineis - 10;
            pDesempenhoFisico.ContentHeight = altura - 35;
            alturaGrafico = (altura - 50);
        }
        else
        {
            int larguraPaineis = ((largura) / 2 - 105);
            int alturaPaineis = altura - 235;


            pDesempenhoFisico.Width = larguraPaineis;
            larguraGrafico = larguraPaineis - 10;

            alturaGrafico = (altura - 295) / 2;
        }
    }
}
