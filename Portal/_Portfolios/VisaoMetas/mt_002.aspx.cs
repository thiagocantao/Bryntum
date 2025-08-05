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

public partial class _Portfolios_VisaoMetas_mt_002 : System.Web.UI.Page
{
    dados cDados;
    int codigoIndicador = 0, codigoMeta = 0, codigoProjeto = 0;
    string unidadeMedida = "";

    //pega a data e hora atual do sistema
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";

    //Mensagens de erro para os gráficos
    public string msgErro = "?LoadDataErrorText=Sem Informações a Apresentar";
    public string msgNoData = "&ChartNoDataText=Sem Informações a Apresentar";
    public string msgInvalid = "&InvalidXMLText=Sem Informações a Apresentar";
    public string desenhando = "&PBarLoadingText=Gerando imagem...";
    public string msgLoading = "&XMLLoadingText=Carregando...";

    //Cria as variáveis para carregar o 1º gráfico de Bullets (Custos)
    public string grafico_titulo = "";
    public string grafico_swf = "../../Flashs/MSLine.swf";
    public string grafico_xml = "";
    public string grafico_xmlzoom = "";

    public string alturaGrafico = "";
    public string larguraGrafico = "";
    
    protected void Page_Init(object sender, EventArgs e)
    {
        ASPxWebControl.SetIECompatibilityModeEdge();
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

        if (Request.QueryString["CM"] != null && Request.QueryString["CM"].ToString() != "")
            codigoMeta = int.Parse(Request.QueryString["CM"].ToString());

        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());

        if (Request.QueryString["CI"] != null && Request.QueryString["CI"].ToString() != "")
            codigoIndicador = int.Parse(Request.QueryString["CI"].ToString());

        if (Request.QueryString["UM"] != null && Request.QueryString["UM"].ToString() != "")
            unidadeMedida = Request.QueryString["UM"].ToString();

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        defineLarguraTela();

        //Função que gera o gráfico
        geraGrafico();
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        larguraGrafico = ((largura - 260) / 2).ToString();
        alturaGrafico = "145";
    }

    //Função para geração do gráfico (Quantidade de Projetos por Desempenho) - PIZZA
    private void geraGrafico()
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico de PIZZA
        string nomeGrafico = @"/ArquivosTemporarios/metas_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + codigoMeta + "_" + dataHora;
       
        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        DataSet ds = cDados.getMetaProjetoIndicadorVisaoCorporativa(codigoMeta, "");

        //função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
        xml = cDados.getGraficoMetaIndicadorProjeto(ds.Tables[0], "", 9, unidadeMedida);

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        ////define o caminho e o nome do XML do gráfico de ZOOM (PIZZA)
        //nomeGrafico = "\\ArquivosTemporarios\\metasZoom_" + codigoIndicador + "_" + codigoProjeto + "_" + dataHora;
                
        ////função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
        //xml = cDados.getGraficoMetaIndicadorProjeto(ds.Tables[0], "Meta/Resultado", 15);

        ////escreve o arquivo xml de quantidade de projetos por entidade
        //cDados.escreveXML(xml, nomeGrafico);

        ////atribui o valor do caminho do XML a ser carregado (ZOOM)
        //grafico_xmlzoom = nomeGrafico;
        //grafico_xmlzoom = grafico_xmlzoom.Replace("\\", "/");

    }
}
