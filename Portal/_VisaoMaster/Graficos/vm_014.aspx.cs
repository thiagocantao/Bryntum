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

public partial class _VisaoMaster_Graficos_vm_014 : System.Web.UI.Page
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
    public string grafico_titulo = "Avanço por Sítio";
    public string grafico_swf = "../../Flashs/StackedColumn2D.swf";//StackedColumn2D
    public string grafico_xml = "";
    public string grafico_xmlzoom = "", toolTip = "";

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

        alturaGrafico = "170";
        defineLarguraTela();

        //Função que gera o gráfico
        geraGrafico();

        ASPxLabel2.Text = "<strong style=\"font-family:verdana; font-size:7pt; font-weight:normal;\">Fonte: </strong><strong style=\"font-family:verdana; font-size:7pt; font-weight:bold;\">Prev.: </strong>Orçamento I0 e Contratos de Fornecimento e Montagem&nbsp;&nbsp;&nbsp;<strong style=\"font-family:verdana; font-size:7pt; font-weight:bold;\">Real.: </strong>Medições dos Contratos";
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
            alturaGrafico = (altura - 40).ToString();
        }
        else
        {
            larguraGrafico = ((largura - 30) / 3).ToString();
            ASPxRoundPanel1.Width = ((largura - 20) / 3);
            ASPxRoundPanel1.ContentHeight = (altura - 240) / 2;
            alturaGrafico = ((altura - 250) / 2).ToString();
        }
    }

    //Função para geração do gráfico (Quantidade de Projetos por Desempenho) - PIZZA
    private void geraGrafico()
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        toolTip = string.Format(@"Objetivo/descrição - Comparar o Previsto e o Realizado do avanço físico por Sítio, da Área selecionada (Civil / Fornecimento e Montagem).");

        //cria uma variável com o nome e o caminho do XML do gráfico de PIZZA
        string nomeGrafico = @"/ArquivosTemporarios/AvancoPorSitioVC_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        string where = "";

        string codigoArea = Request.QueryString["CA"] == null ? "" : Request.QueryString["CA"].ToString();

        int diasTolerancia = cDados.getDiasToleranciaUHE();

        DateTime dataParam = DateTime.Now.Day < diasTolerancia ? DateTime.Now.AddMonths(-2) : DateTime.Now.AddMonths(-1);
        
        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        DataSet ds = cDados.getAvancoFinanceiroPorSitio(codigoArea, int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), dataParam.Year, dataParam.Month, where);

        grafico_titulo = string.Format("Avanço por Sítio até {0:MMM/yyyy} (R$ Mil)", dataParam);

        //função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
        xml = cDados.getGraficoAvancoFinanceiroSitio(ds.Tables[0], "PercentualRealizado", "PercentualRestante", "ValorReal", "ValorRestante", "Realizado(I0)", "A Realizar(I0)", "", 9, 0);

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //define o caminho e o nome do XML do gráfico de ZOOM (PIZZA)
        nomeGrafico = "\\ArquivosTemporarios\\AvancoPorSitioVCZOOM_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
        xml = cDados.getGraficoAvancoFinanceiroSitio(ds.Tables[0], "PercentualRealizado", "PercentualRestante", "ValorReal", "ValorRestante", "Realizado(I0)", "A Realizar", "", 15, 0);

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado (ZOOM)
        grafico_xmlzoom = nomeGrafico;
        grafico_xmlzoom = grafico_xmlzoom.Replace("\\", "/");

        string codigoReservado = "UHE_Principal";
        string nomeArquivoASPX = Request.FilePath.Substring(Request.FilePath.LastIndexOf("/") + 1).Replace(".aspx", "");
        ASPxLabel2.ClientSideEvents.Click = "function(s, e) {if(window.parent.parent.callbackMetrica) window.parent.parent.callbackMetrica.PerformCallback('" + nomeArquivoASPX + ";" + codigoReservado + "'); } ";
        ASPxLabel2.Cursor = "Pointer";
        ASPxLabel2.ToolTip = "Clique aqui para visualizar a métrica e última atualização";
    }
}