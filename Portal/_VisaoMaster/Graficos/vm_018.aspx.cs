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

public partial class _VisaoMaster_Graficos_vm_018 : System.Web.UI.Page
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

    public string grafico_titulo = "", tituloPainel = "";
    public string grafico_swf = "../../Flashs/MSLine.swf";
    public string grafico_xml = "";
    public string grafico_xmlzoom = "";

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
            ASPxRoundPanel1.Width = (largura - 3);
            ASPxRoundPanel1.ContentHeight = altura - 32;
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

        string toolTip = string.Format(@"Objetivo/descrição - Comparar o Orçado e o Realizado do ano vigente. Essa comparação abrange o CAPEX e o OPEX.{0}Data Base: Maio/2012"
            , Environment.NewLine);

        //cria uma variável com o nome e o caminho do XML do gráfico
        string nomeGrafico = @"/ArquivosTemporarios/CurvaSCusto_001_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        string codigoArea = Request.QueryString["CA"] == null ? "" : Request.QueryString["CA"].ToString();
        string nomeArea = Request.QueryString["NA"] == null ? "" : Request.QueryString["NA"].ToString();

        DataTable dt = cDados.getCurvaSPainelPercentualCapexOpex(codigoArea, codigoEntidade, DateTime.Now.Year, "").Tables[0];

        string codigoReservado = "UHE_Principal";
        string nomeArquivoASPX = Request.FilePath.Substring(Request.FilePath.LastIndexOf("/") + 1).Replace(".aspx", "");
        string funcao = "if(window.parent.parent.callbackMetrica) window.parent.parent.callbackMetrica.PerformCallback('" + nomeArquivoASPX + ";" + codigoReservado + "');";

        tituloPainel = string.Format(@"<table title=""{2}"" cellpadding='0' cellspacing='0' style='width:100%;'><tr><td style='width:18px' align='left'><img onclick=""{3}"" style=""cursor:pointer"" alt='Desempenho do Custo' src='../../imagens/{0}Menor.gif' /></td><td align='left'><a style='color: #5d7b9d;text-decoration:underline;' href='#' onclick='abreCusto({4}, ""{5}"");'>{1}</a></td></tr></table>"
                , Request.QueryString["CorCusto"]
                , "Acompanhamento Econômico Orçado x Realizado - CAPEX + OPEX (R$ Mil)"
                , toolTip
                , funcao
                , codigoArea
                , nomeArea);

        //função para gerar a estrutura xml para criação do gráfico
        xml = cDados.getGraficoCurvaSCapexOpex(dt, "", 9, 2, 9, 2, 25);

        //escreve o arquivo xml
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //define o caminho e o nome do XML do gráfico de ZOOM 
        nomeGrafico = "\\ArquivosTemporarios\\CurvaSCusto_001ZOOM_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função para gerar a estrutura xml para criação do gráfico
        xml = cDados.getGraficoCurvaSCapexOpex(dt, "Acompanhamento Econômico Orçado x Realizado - CAPEX + OPEX (R$ Mil)" + nomeArea, 15, 2, 9, 2, 25);

        //escreve o arquivo xml
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado (ZOOM)
        grafico_xmlzoom = nomeGrafico;
        grafico_xmlzoom = grafico_xmlzoom.Replace("\\", "/");

        ASPxLabel1.ClientSideEvents.Click = "function(s, e) { " + funcao + " } ";
        ASPxLabel1.Cursor = "Pointer";
        ASPxLabel1.ToolTip = "Clique aqui para visualizar a métrica e última atualização";

    }
}
