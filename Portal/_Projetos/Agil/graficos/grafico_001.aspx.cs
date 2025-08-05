using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Projetos_Agil_graficos_grafico_001 : System.Web.UI.Page
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
    public string grafico_titulo = "Desempenho";
    public string grafico_swf = "../../../Flashs/Pie2D.swf";
    public string grafico_xml = "";
    public string grafico_xmlzoom = "";

    public static string alturaGrafico = "";
    public static string larguraGrafico = "";
    bool permissaoLink = false;
    int codigoUsuarioLogado = -1;
    int codigoEntidade = -1;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        permissaoLink = true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        defineLarguraTela();
        //}
        geraGrafico();
    }

    private void defineLarguraTela()
    {
        int alturaAux = int.Parse((Request.QueryString["alt"] != null) ? Request.QueryString["alt"] + "" : "0");
        int larguraAux = int.Parse((Request.QueryString["larg"] != null) ? Request.QueryString["larg"] + "" : "0");

        alturaGrafico = (alturaAux - 35).ToString();
        larguraGrafico = (larguraAux - 110).ToString();
    }


    //Função para geração do gráfico (Quantidade de Projetos por Desempenho) - PIZZA
    private void geraGrafico()
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico de PIZZA
        string nomeGrafico = @"/ArquivosTemporarios/agilStatus_Grafico1" + codigoUsuarioLogado.ToString() + "_" + dataHora;

        string where = "";

        where = " AND p.CodigoGerenteProjeto = " + codigoUsuarioLogado.ToString();

        if (cDados.getInfoSistema("CodigosProjetosUsuario") != null)
            where += " AND p.CodigoProjeto IN (" + cDados.getInfoSistema("CodigosProjetosUsuario").ToString() + ")";
        else
            where += string.Format("AND p.CodigoProjeto in (SELECT codigoProjeto FROM {0}.{1}.f_getProjetosUsuario({2}, {3}, {4})) ", cDados.getDbName(), cDados.getDbOwner(), codigoUsuarioLogado, codigoEntidade, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()));

        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        DataSet ds = cDados.getDesempenhoProjetosVC(codigoUsuarioLogado, codigoEntidade, where);

        //função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
        xml = cDados.getGraficoDesempenhoProjetosGerente(ds.Tables[0], "", 9, permissaoLink, 1, codigoUsuarioLogado);

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = "../../.." + nomeGrafico;

        //define o caminho e o nome do XML do gráfico de ZOOM (PIZZA)
        nomeGrafico = "\\ArquivosTemporarios\\agilStatus_Grafico1ZOOM_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
        xml = cDados.getGraficoDesempenhoProjetosGerente(ds.Tables[0], "Meus Projetos", 15, false, 1, codigoUsuarioLogado);

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado (ZOOM)
        grafico_xmlzoom = nomeGrafico;
        grafico_xmlzoom = grafico_xmlzoom.Replace("\\", "/");

    }
}