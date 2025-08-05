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

public partial class _Projetos_VisaoCorporativa_vc_002 : System.Web.UI.Page
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
    public string grafico_swf = "../../Flashs/Pie2D.swf";
    public string grafico_xml = "";
    public string grafico_xmlzoom = "";

    public string alturaGrafico = "";
    public string larguraGrafico = "";
    bool permissaoLink = false;

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
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());


        permissaoLink = true;

        //Função que gera o gráfico
        geraGrafico();
        cDados.aplicaEstiloVisual(this);
    }

    private void defineLarguraTela()
    {
        int largura, altura;

        if (Request.QueryString["Altura"] != null && Request.QueryString["Largura"] != null)
        {
            largura = int.Parse(Request.QueryString["Largura"].ToString());
            altura = int.Parse(Request.QueryString["Altura"].ToString());

            larguraGrafico = (largura - 15).ToString();
            ASPxRoundPanel1.Width = (largura - 10);
            ASPxRoundPanel1.ContentHeight = altura - 35;
            alturaGrafico = (altura - 37).ToString();
        }
        else
        {
            string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
            largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
            altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

            larguraGrafico = ((largura - 290) / 3).ToString();
            ASPxRoundPanel1.Width = ((largura - 270) / 3);
            ASPxRoundPanel1.ContentHeight = (altura - 200) / 2 - 25;
            alturaGrafico = (((altura - 230) / 2)).ToString();
        }
    }

    //Função para geração do gráfico (Quantidade de Projetos por Desempenho) - PIZZA
    private void geraGrafico()
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico de PIZZA
        string nomeGrafico = @"/ArquivosTemporarios/desempenhoEntidadeVC123_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        string where = "";

        if (cDados.getInfoSistema("CodigoStatus") != null && int.Parse(cDados.getInfoSistema("CodigoStatus").ToString()) != -1)
            where = " AND p.CodigoStatusProjeto = " + cDados.getInfoSistema("CodigoStatus").ToString();

        if (cDados.getInfoSistema("CodigosProjetosUsuario") != null)
            where += " AND p.CodigoProjeto IN (" + cDados.getInfoSistema("CodigosProjetosUsuario").ToString() + ")";
        else
            where += string.Format("AND p.CodigoProjeto in (SELECT codigoProjeto FROM {0}.{1}.f_getProjetosUsuario({2}, {3}, {4})) ", cDados.getDbName(), cDados.getDbOwner(), int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()));

        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        DataSet ds = cDados.getDesempenhoProjetosVC(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()),  where);
                
        //função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
        xml = cDados.getGraficoDesempenhoEntidade(ds.Tables[0], "", 9, permissaoLink, 1);

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //define o caminho e o nome do XML do gráfico de ZOOM (PIZZA)
        nomeGrafico = "\\ArquivosTemporarios\\desempenhoEntidadeVCZOOM_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;
                
        //função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
        xml = cDados.getGraficoDesempenhoEntidade(ds.Tables[0], "Projetos - Desempenho", 15, false, 1);

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado (ZOOM)
        grafico_xmlzoom = nomeGrafico;
        grafico_xmlzoom = grafico_xmlzoom.Replace("\\", "/");

    }
}
