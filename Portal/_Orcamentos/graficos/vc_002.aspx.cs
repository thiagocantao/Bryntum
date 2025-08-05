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

public partial class _Orcamentos_graficos_vc_002 : System.Web.UI.Page
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
    public string grafico_titulo = "Resultado";
    public string grafico_swf = "../../Flashs/AngularGauge.swf";
    public string grafico_xml = "";
    public string grafico_xmlzoom = "";

    public string alturaGrafico = "";
    public string larguraGrafico = "";

    bool permissaoLink = false;

    int codigoEntidade = -1;

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

        int codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        permissaoLink = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidade, "ACSSISORC");

        //Função que gera o gráfico
        geraGrafico();
    }

    private void defineLarguraTela()
    {
        int largura, altura;

        if (Request.QueryString["Altura"] != null && Request.QueryString["Largura"] != null)
        {
            largura = int.Parse(Request.QueryString["Largura"].ToString());
            altura = int.Parse(Request.QueryString["Altura"].ToString());

            larguraGrafico = (largura - 10).ToString();
            ASPxRoundPanel1.Width = (largura - 10);
            ASPxRoundPanel1.ContentHeight = altura - 35;
            alturaGrafico = (altura - 50).ToString();
        }
        else
        {
            string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
            largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
            altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

            larguraGrafico = ((largura - 280) / 3).ToString();
            ASPxRoundPanel1.Width = ((largura - 265) / 3);
            ASPxRoundPanel1.ContentHeight = (altura - 195) / 2 - 25;
            alturaGrafico = (((altura - 235) / 2)).ToString();
        }
    }

    //Função para geração do gráfico (Quantidade de Projetos por Desempenho) - PIZZA
    private void geraGrafico()
    {
        float valorReceita = 0, valorDespesa = 0, valorMax = 0, valorMedio = 0;
        string valorResultado = "";

        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico de PIZZA
        string nomeGrafico = @"/ArquivosTemporarios/resultadoOrcamentoVC_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        string where = "";

        int mes = 0, orcamento = 0;

        if (cDados.getInfoSistema("Mes") != null)
            mes = int.Parse(cDados.getInfoSistema("Mes").ToString());

        if (cDados.getInfoSistema("CodigoOrcamento") != null)
            orcamento = int.Parse(cDados.getInfoSistema("CodigoOrcamento").ToString());

        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        DataSet ds = cDados.getDesempenhoOrcamentoProjetos(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), mes, orcamento, codigoEntidade, where);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataTable dt = ds.Tables[0];

            valorReceita = float.Parse(dt.Rows[0]["ReceitaReal"].ToString());
            valorDespesa = float.Parse(dt.Rows[0]["DespesaReal"].ToString());
            valorResultado = dt.Rows[0]["ResultadoReal"].ToString();

            if (valorDespesa > valorReceita)
            {
                valorMax = valorDespesa;
            }
            else
            {
                valorMax = valorReceita;
            }

            DataSet dsParametros = cDados.getParametrosSistema("percentualResultado");

            float valorAtencao = 0;

            if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["percentualResultado"] + "" != "")
            {
                valorAtencao = float.Parse(dsParametros.Tables[0].Rows[0]["percentualResultado"].ToString());
            }

            valorMedio = (valorMax * valorAtencao) / 100;            
        }

        DataSet dsUrl = cDados.getParametrosSistema("UrlOrcamento");

        string urlLink = "";

        if (cDados.DataSetOk(dsUrl) && cDados.DataTableOk(dsUrl.Tables[0]) && dsUrl.Tables[0].Rows[0]["UrlOrcamento"] + "" != "" && permissaoLink == true)
        {
            urlLink = " clickURL=\"n-" + dsUrl.Tables[0].Rows[0]["UrlOrcamento"].ToString() + "\" ";
        }

        int margem = int.Parse(alturaGrafico) - 185;

        //função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
        xml = cDados.getGraficoResultado(valorMedio, valorMax, valorResultado, 90, "", false, 9, margem <= 0 ? 35 : margem, urlLink);

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //define o caminho e o nome do XML do gráfico de ZOOM (PIZZA)
        nomeGrafico = "\\ArquivosTemporarios\\resultadoOrcamentoVCZOOM_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;
                
        //função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
        xml = cDados.getGraficoResultado(valorMedio, valorMax, valorResultado, 260, "", true, 15, 100, "");

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado (ZOOM)
        grafico_xmlzoom = nomeGrafico;
        grafico_xmlzoom = grafico_xmlzoom.Replace("\\", "/");

    }
}
