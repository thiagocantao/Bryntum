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

public partial class _VisaoMaster_Graficos_vm_011 : System.Web.UI.Page
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
    public string grafico_swf = "../../Flashs/ScrollLine2D.swf";
    public string grafico_xml = "";
    public string grafico_xmlzoom = "", toolTip = "";

    public string alturaGrafico = "";
    public string larguraGrafico = "";

    public int anoSelecionado;

    int codigoEntidade;
    string nomeArea = "";

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

        nomeArea = Request.QueryString["NA"] == null ? "" : " - " + Request.QueryString["NA"].ToString();

        //Função que gera o gráfico
        geraGrafico();

        ASPxLabel1.Text = "<strong style=\"font-family:verdana; font-size:7pt; font-weight:normal;\">Fonte: </strong><strong style=\"font-family:verdana; font-size:7pt; font-weight:bold;\">Orçamento e Realizado Reajustado: </strong>Gerência de Informações Gerenciais&nbsp;&nbsp;&nbsp;<strong style=\"font-family:verdana; font-size:7pt; font-weight:bold;\">Contrato I0 e Realizado I0: </strong>Contratos e Medições dos Contratos";
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

            larguraGrafico = (largura - 17).ToString();
            ASPxRoundPanel1.Width = (largura - 12);
            ASPxRoundPanel1.ContentHeight = altura - 35;
            alturaGrafico = (altura - 35).ToString();
        }
        else
        {
            larguraGrafico = ((largura - 30) / 3).ToString();
            ASPxRoundPanel1.Width = ((largura - 20) / 3);
            ASPxRoundPanel1.ContentHeight = (altura - 240) / 2;
            alturaGrafico = ((altura - 250) / 2).ToString();
        }
    }

    //Função para geração do gráfico
    private void geraGrafico()
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        toolTip = string.Format(@"Objetivo/descrição - Comparar o Orçado e o Realizado Reajustado do ano vigente. Essa comparação contempla apenas as informações das áreas de Obra Civil e de Fornecimento e Montagem (CAPEX). Compara também as curvas previstas e realizadas dos contratos (i0) das referidas áreas.{0}Data Base Orçamento: Maio/2012"
            , Environment.NewLine);

        //cria uma variável com o nome e o caminho do XML do gráfico
        string nomeGrafico = @"/ArquivosTemporarios/CurvaSCusto_001_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        string codigoArea = Request.QueryString["CA"] == null ? "" : Request.QueryString["CA"].ToString();

        DataTable dt = cDados.getCurvaSPainelPercentualCusto(codigoArea, codigoEntidade, DateTime.Now.Year, "").Tables[0];

        //função para gerar a estrutura xml para criação do gráfico
        xml = cDados.getGraficoCurvaSCustoGerenciamento(dt, "", 9, 2, 9, 2, 25);

        //escreve o arquivo xml
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //define o caminho e o nome do XML do gráfico de ZOOM 
        nomeGrafico = "\\ArquivosTemporarios\\CurvaSCusto_001ZOOM_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função para gerar a estrutura xml para criação do gráfico
        xml = cDados.getGraficoCurvaSCustoGerenciamento(dt, "Acompanhamento Econômico Orçado x Realizado" + nomeArea + " (R$ Mil)", 15, 2, 9, 2, 25);

        //escreve o arquivo xml
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado (ZOOM)
        grafico_xmlzoom = nomeGrafico;
        grafico_xmlzoom = grafico_xmlzoom.Replace("\\", "/");

        string codigoReservado = "UHE_Principal";
        string nomeArquivoASPX = Request.FilePath.Substring(Request.FilePath.LastIndexOf("/") + 1).Replace(".aspx", "");
        ASPxLabel1.ClientSideEvents.Click = "function(s, e) {if(window.parent.parent.callbackMetrica) window.parent.parent.callbackMetrica.PerformCallback('" + nomeArquivoASPX + ";" + codigoReservado + "'); } ";
        ASPxLabel1.Cursor = "Pointer";
        ASPxLabel1.ToolTip = "Clique aqui para visualizar a métrica e última atualização";
    }

    public string getTitulo()
    {
        nomeArea = (Request.QueryString["NA"] != null && Request.QueryString["NA"].ToString() != "") ? " - " + Request.QueryString["NA"].ToString() : "";
        return "Acompanhamento Econômico - Orçado x Realizado (CAPEX)" + nomeArea + " (R$ Mil)";
    }
}
