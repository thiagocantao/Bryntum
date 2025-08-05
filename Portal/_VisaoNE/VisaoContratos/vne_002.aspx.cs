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

public partial class _VisaoNE_VisaoContratos_vne_002 : System.Web.UI.Page
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

    public string grafico_titulo = "";
    public string grafico_swf = "../../Flashs/StackedColumn2D.swf";
    public string grafico_xml = "";
    public string grafico_xmlzoom = "";

    public string alturaGrafico = "";
    public string larguraGrafico = "";
    int codigoEntidade;

    int anoParam;

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
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x'))) - 20;
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        larguraGrafico = ((largura - 245) / 2).ToString();

        ASPxRoundPanel1.Width = ((largura - 225) / 2);
        ASPxRoundPanel1.ContentHeight = (altura - 200) / 2 - 25;
        alturaGrafico = (((altura - 230) / 2)).ToString();
    }

    private void geraGrafico()
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico
        string nomeGrafico = @"/ArquivosTemporarios/desempenhoFinanceiroContratosAnoVNE_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        anoParam = cDados.getInfoSistema("AnoPainelContrato") == null ? DateTime.Now.Year : int.Parse(cDados.getInfoSistema("AnoPainelContrato").ToString());
        int tipoContratacao = cDados.getInfoSistema("TipoContratacao") == null || cDados.getInfoSistema("TipoContratacao").ToString() == "" ? -1 : int.Parse(cDados.getInfoSistema("TipoContratacao").ToString());
        int fonteRecursos = cDados.getInfoSistema("CodigoFonte") == null || cDados.getInfoSistema("CodigoFonte").ToString() == "" ? -1 : int.Parse(cDados.getInfoSistema("CodigoFonte").ToString());

        //DataTable dt = cDados.getRealizacaoContratosPorMunicipio(codigoEntidade, "").Tables[0];
        DataTable dt = cDados.getRealizacaoContratosPorMunicipio(codigoEntidade, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()), int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), anoParam, "N", tipoContratacao, fonteRecursos, "").Tables[0];
        //função para gerar a estrutura xml para criação do gráfico
        xml = cDados.getGraficoRealizacaoContratosPorMunicipio(dt, "", 9, true, "N");

        //escreve o arquivo xml
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //define o caminho e o nome do XML do gráfico de ZOOM 
        nomeGrafico = "\\ArquivosTemporarios\\desempenhoFinanceiroAnoContratosVNEZOOM_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        xml = cDados.getGraficoRealizacaoContratosPorMunicipio(dt, "Índice de Realização Financeira dos Contratos por Município - " + anoParam, 15, false, "N");

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado (ZOOM)
        grafico_xmlzoom = nomeGrafico;
        grafico_xmlzoom = grafico_xmlzoom.Replace("\\", "/");

    }

    public string getTitulo()
    {
        return "Índice de Realização Financeira dos Contratos por Município";
    }
}
