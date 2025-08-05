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

public partial class _VisaoMaster_Graficos_vm_009 : System.Web.UI.Page
{
    dados cDados;

    //pega a data e hora atual do sistema
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";

    //Mensagens de erro para os gráficos
    public string msgErro = "?LoadDataErrorText=Sem Informações a Apresentar";
    public string msgNoData = "&ChartNoDataText=Sem Informações a Apresentar";
    public string msgInvalid = "&InvalidXMLText=Sem Informações a Apresentar";
    public string desenhando = "&PBarLoadingText=Gerando imagem...";
    public string msgLoading = "&XMLLoadingText=Carregando...";

    //Cria as variáveis para carregar o 1º gráfico de Bullets (Custos)
    //Cria as variáveis para carregar o 1º gráfico de Bullets (Custos)
    public string grafico1_titulo = "Desempenho Geral do Sítio";
    public string grafico1_swf = "../../Flashs/HBullet.swf";
    public string grafico1_xml = "";
    public string grafico1_xmlzoom = "";

    //Cria as variáveis para carregar o 2º gráfico de Bullets (Esforço)
    public string grafico2_swf = "../../Flashs/HBullet.swf";
    public string grafico2_xml = "";
    public string grafico2_xmlzoom = "";

    public string alturaGrafico = "";
    public string larguraGrafico = "";

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

        defineLarguraTela();

        string where = "";

        string codigoReservado = Request.QueryString["CR"] == null ? "" : Request.QueryString["CR"].ToString();
        string codigoArea = Request.QueryString["CA"] == null ? "" : Request.QueryString["CA"].ToString();

        //Data Set contendo a tabela com os dados a serem carregados no gráfico 
        DataSet ds = cDados.getDesempenhoFisicoFinanceiroPainelGerenciamento(codigoReservado, int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), int.Parse(codigoArea), where);

        DataTable dt = ds.Tables[0];

        //Função que gera o gráfico 1
        geraGrafico1(dt);

        //Função que gera o gráfico 2
        geraGrafico2(dt);
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

            larguraGrafico = (largura - 15).ToString();
            ASPxRoundPanel1.Width = (largura - 10);
            ASPxRoundPanel1.ContentHeight = altura - 35;
            alturaGrafico = ((altura - 70) / 2).ToString();
        }
        else
        {
            larguraGrafico = ((largura - 30) / 3).ToString();
            ASPxRoundPanel1.Width = ((largura - 20) / 3);
            ASPxRoundPanel1.ContentHeight = (altura - 240) / 2;
            alturaGrafico = ((altura - 250) / 4).ToString();
        }
    }

    //Função para geração do gráfico 1
    public void geraGrafico1(DataTable dt)
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico Bullet
        string nomeGrafico;

        //*****************
        //Criação do Bullet de Esforço
        //cria uma variável com o nome e o caminho do XML do gráfico BULLET de esforço dos Projetos
        nomeGrafico = @"/ArquivosTemporarios/bulletFisico_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função que cria a string XML que irá carregar o gráfico de Bullets de Esforço
        xml = cDados.getGraficoBulletsSitio(dt, "Físico", "PercFisicoPrevisto", "PercFisicoReal", "CorFisico", "../../ImageSaving/FusionChartsSave.aspx", "%", 9);

        //Cria o arquivo XML
        cDados.escreveXML(xml, nomeGrafico);

        //Atribui o nome e o caminho do arquivo que irá carregar o gráfico
        grafico1_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //cria uma variável com o nome e o caminho do XML do gráfico BULLET de esforço dos Projetos
        nomeGrafico = "ArquivosTemporarios/bulletFisicoZoom_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;


        //função que cria a string XML que irá carregar o gráfico de Bullets de Esforço de ZOOM
        xml = cDados.getGraficoBulletsSitio(dt, "Físico", "PercFisicoPrevisto", "PercFisicoReal", "CorFisico", "../../ImageSaving/FusionChartsSave.aspx", "%", 15);

        //Cria o arquivo XML
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado (ZOOM)
        grafico1_xmlzoom = nomeGrafico;

    }

    //Função para geração do gráfico 2
    public void geraGrafico2(DataTable dt)
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico 2
        string nomeGrafico;

        //cria uma variável com o nome e o caminho do XML do gráfico bullet de custo dos Projetos 
        nomeGrafico = "/ArquivosTemporarios/bulletFinanceiro_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função que cria a string XML que irá carregar o gráfico de Bullets de Custos
        xml = cDados.getGraficoBulletsSitio(dt, "Financeiro", "PercCustoPrevisto", "PercCustoReal", "CorCusto", "../../ImageSaving/FusionChartsSave.aspx", "%", 9);

        //Cria o arquivo XML
        cDados.escreveXML(xml, nomeGrafico);

        //Atribui o nome e o caminho do arquivo XML que carregará o gráfico
        grafico2_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //cria uma variável com o nome e o caminho do XML do gráfico bullet de custo dos Projetos 
        nomeGrafico = "ArquivosTemporarios/bulletFinanceiroZoom_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função que cria a string XML que irá carregar o gráfico de Bullets de Custos de ZOOM
        xml = cDados.getGraficoBulletsSitio(dt, "Financeiro", "PercCustoPrevisto", "PercCustoReal", "CorCusto", "../../ImageSaving/FusionChartsSave.aspx", "%", 15);

        //Cria o arquivo XML
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado (ZOOM)
        grafico2_xmlzoom = nomeGrafico;

    }
}