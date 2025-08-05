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

public partial class _VisaoMaster_Graficos_vm_019 : System.Web.UI.Page
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

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        defineLarguraTela();

        //Função que gera o gráfico
        geraGrafico();

        ASPxLabel1.Text = "<strong style=\"font-family:verdana; font-size:7pt; font-weight:normal;\">Fonte: </strong><strong style=\"font-family:verdana; font-size:7pt; font-weight:bold;\">Previsto: </strong>Orçamento e Contratos de Fornecimento e Montagem&nbsp;&nbsp;&nbsp;<strong style=\"font-family:verdana; font-size:7pt; font-weight:bold;\">Realizado: </strong>Medições de Contratos";
    }

    private void defineLarguraTela()
    {
        int largura, altura;


        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

        if (Request.QueryString["Altura"] != null && Request.QueryString["Largura"] != null)
        {
            largura = int.Parse(Request.QueryString["Largura"].ToString());
            altura = int.Parse(Request.QueryString["Altura"].ToString());

            larguraGrafico = (largura);            
            alturaGrafico = (altura);
        }
        else
        {
            larguraGrafico = ((largura - 30) / 3);            
            alturaGrafico = ((altura - 250) / 2);
        }
    }

    //Função para geração do gráfico
    private void geraGrafico()
    {
        string xml = "";

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

        string nomeArquivoASPX = Request.FilePath.Substring(Request.FilePath.LastIndexOf("/") + 1).Replace(".aspx", "");
        ASPxLabel1.ClientSideEvents.Click = "function(s, e) {if(window.parent.parent.callbackMetrica) window.parent.parent.callbackMetrica.PerformCallback('" + nomeArquivoASPX + ";" + codigoReservado + "'); } ";
        ASPxLabel1.Cursor = "Pointer";
        ASPxLabel1.ToolTip = "Clique aqui para visualizar a métrica e última atualização";
    }
}
