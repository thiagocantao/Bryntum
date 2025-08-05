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
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_")  + ".xml";

    //Mensagens de erro para os gráficos
    public string msgErro = "?LoadDataErrorText=Sem Informações a Apresentar";
    public string msgNoData = "&ChartNoDataText=Sem Informações a Apresentar";
    public string msgInvalid = "&InvalidXMLText=Sem Informações a Apresentar";
    public string desenhando = "&PBarLoadingText=Gerando imagem...";
    public string msgLoading = "&XMLLoadingText=Carregando...";

    //Cria as variáveis para carregar o 1º gráfico de Bullets (Custos)
    public string grafico_titulo = "Var.(%) Mensal do Nº de Cadastros(Últimos 12 meses)";
    public string grafico_swf = "../../Flashs/ScrollLine2D.swf";
    public string grafico_xml = "";
    public string grafico_xmlzoom = "";

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

            larguraGrafico = (largura - 15).ToString();
            ASPxRoundPanel1.Width = (largura - 10);
            ASPxRoundPanel1.ContentHeight = altura - 43;
            alturaGrafico = (altura - 50).ToString();
        }
        else
        {
            string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
            largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
            altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

            larguraGrafico = ((largura - 70) / 3).ToString();
            ASPxRoundPanel1.Width = ((largura - 55) / 3);
            ASPxRoundPanel1.ContentHeight = (altura - 200) / 2 - 25;
            alturaGrafico = (((altura - 230) / 2)).ToString();
        }
    }

    //Função para geração do gráfico (Quantidade de Projetos por Desempenho) - PIZZA
    private void geraGrafico()
    {
        DataSet ds = new DataSet();

        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico de PIZZA
        string nomeGrafico = @"/ArquivosTemporarios/numeroCadastros_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + dataHora;

        int idUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (cDados.getInfoSistema("DataEstagio") != null && cDados.getInfoSistema("DataEstagio").ToString() != "")
        {
            string dataEstagio = cDados.getInfoSistema("DataEstagio").ToString();
            int diasMes = DateTime.DaysInMonth(int.Parse(dataEstagio.Substring(3)), int.Parse(dataEstagio.Substring(0, 2)));

            string data = diasMes + "/" + dataEstagio;

            //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
            ds = cDados.getNumeroCadastrosEstagios(codigoEntidade, idUsuario, int.Parse(dataEstagio.Substring(3)), int.Parse(dataEstagio.Substring(0, 2)));

            //função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
            xml = cDados.getGraficoNumeroCadastros(ds.Tables[0], "", 9);
        }

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //define o caminho e o nome do XML do gráfico de ZOOM (PIZZA)
        nomeGrafico = "\\ArquivosTemporarios\\numeroCadastrosZOOM_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + dataHora;

        if (cDados.getInfoSistema("DataEstagio") != null && cDados.getInfoSistema("DataEstagio").ToString() != "")
        {
            //função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
            xml = cDados.getGraficoNumeroCadastros(ds.Tables[0], "Var.(%) Mensal do Nº de Cadastros(Últimos 12 meses)", 15);
        }

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado (ZOOM)
        grafico_xmlzoom = nomeGrafico;
        grafico_xmlzoom = grafico_xmlzoom.Replace("\\", "/");

    }
}
