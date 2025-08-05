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

public partial class _VisaoMaster_Graficos_vm_016 : System.Web.UI.Page
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
    public string grafico_titulo = "Previsão de Desembolso Financeiro", tituloPainel = "";
    public string grafico_swf = "../../Flashs/MSCombiDY2D.swf";
    public string grafico_xml = "";
    public string grafico_xmlzoom = "";

    public string alturaGrafico = "";
    public string larguraGrafico = "";
    int codigoUsuarioLogado, codigoEntidade;

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

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());


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

            larguraGrafico = (largura - 15).ToString();
            ASPxRoundPanel1.Width = (largura - 10);
            ASPxRoundPanel1.ContentHeight = altura - 32;
            alturaGrafico = (altura - 33).ToString();
        }
        else
        {
            larguraGrafico = ((largura - 30) / 3).ToString();
            ASPxRoundPanel1.Width = ((largura - 20) / 3);
            ASPxRoundPanel1.ContentHeight = (altura - 235) / 2;
            alturaGrafico = ((altura - 238) / 2).ToString();
        }
    }

   //Função para geração do gráfico (Quantidade de Projetos por Desempenho) - PIZZA
    private void geraGrafico()
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        string toolTip = string.Format(@"Objetivo/descrição - Verificar a acuracidade trimestral da previsão de desembolso.{0}Data Base: Janeiro/2013"
            , Environment.NewLine);
        
        //cria uma variável com o nome e o caminho do XML do gráfico de PIZZA
        string nomeGrafico = @"/ArquivosTemporarios/Financiamentos_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        string where = "";

        string codigoArea = Request.QueryString["CA"] == null ? "" : Request.QueryString["CA"].ToString();
        string nomeArea = Request.QueryString["NA"] == null ? "" : Request.QueryString["NA"].ToString();

        grafico_titulo = "Previsão de Desembolso Financeiro (R$ Mil)";
                
        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        DataSet ds = cDados.getValoresDesembolsoFinanceiro(codigoArea, int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), where);

        string codigoReservado = "UHE_Principal";
        string nomeArquivoASPX = Request.FilePath.Substring(Request.FilePath.LastIndexOf("/") + 1).Replace(".aspx", "");
        string funcao = "if(window.parent.parent.callbackMetrica) window.parent.parent.callbackMetrica.PerformCallback('" + nomeArquivoASPX + ";" + codigoReservado + "');";

        tituloPainel = string.Format(@"<table title=""{2}"" cellpadding='0' cellspacing='0' style='width:100%;'><tr><td style='width:18px;' align='left'><img onclick=""{3}"" style=""cursor:pointer"" alt='Desempenho' src='../../imagens/{0}Menor.gif' /></td><td align='left'><a style='color: #5d7b9d;text-decoration:underline;' href='#' onclick='abreTabela({4});'>{1}</a></td></tr></table>"
                , Request.QueryString["CorDesembolso"]
                , grafico_titulo
                , toolTip
                , funcao
                , codigoArea
                , nomeArea);

        //função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
        xml = cDados.getGraficoDesembolsoFinanceiro(ds.Tables[0], "", 9, codigoArea, true);

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //define o caminho e o nome do XML do gráfico de ZOOM (PIZZA)
        nomeGrafico = "\\ArquivosTemporarios\\FinanciamentosZOOM_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;
                
        //função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
        xml = cDados.getGraficoDesembolsoFinanceiro(ds.Tables[0], grafico_titulo, 15, codigoArea, false);

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado (ZOOM)
        grafico_xmlzoom = nomeGrafico;
        grafico_xmlzoom = grafico_xmlzoom.Replace("\\", "/");

        
        ASPxLabel1.ClientSideEvents.Click = "function(s, e) { " + funcao + " } ";
        ASPxLabel1.Cursor = "Pointer";
        ASPxLabel1.ToolTip = "Clique aqui para visualizar a métrica e última atualização";
    }
}
