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
using System.Globalization;

public partial class _Orcamentos_graficos_vc_001 : System.Web.UI.Page
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
    public string grafico1_titulo = "Desempenho Geral";
    public string grafico1_swf = "../../Flashs/HBullet.swf";
    public string grafico1_xml = "";
    public string grafico1_xmlzoom = "";

    //Cria as variáveis para carregar o 2º gráfico de Bullets (Esforço)
    public string grafico2_swf = "../../Flashs/HBullet.swf";
    public string grafico2_xml = "";
    public string grafico2_xmlzoom = "";

    //Cria as variáveis para carregar o 3º gráfico de Bullets (Receitas)
    public string grafico3_swf = "../../Flashs/HBullet.swf";
    public string grafico3_xml = "";
    public string grafico3_xmlzoom = "";

    public char mostrarReceita = 'S';

    public string alturaGrafico = "";
    public string larguraGrafico = "";

    string valorLimite;

    string urlLink = "";

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

        string where = "";

        int mes = 0, orcamento = 0;

        if (cDados.getInfoSistema("Mes") != null)
            mes = int.Parse(cDados.getInfoSistema("Mes").ToString());

        if (cDados.getInfoSistema("CodigoOrcamento") != null)
            orcamento = int.Parse(cDados.getInfoSistema("CodigoOrcamento").ToString());

        int codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        permissaoLink = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidade, "ACSSISORC");

        DataSet dsParametros = cDados.getParametrosSistema("UrlOrcamento");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["UrlOrcamento"] + "" != "" && permissaoLink == true)
        {
            urlLink = " clickURL=\"n-" + dsParametros.Tables[0].Rows[0]["UrlOrcamento"].ToString() + "\" ";
        }

        //Data Set contendo a tabela com os dados a serem carregados no gráfico 
        DataSet ds = cDados.getDesempenhoOrcamentoProjetos(codigoUsuarioLogado, mes, orcamento, codigoEntidade, where);

        DataTable dt = ds.Tables[0];

        DataSet dsBullets = cDados.getCoresBulletsDesempenhoOrcamentoProjetos(mes, orcamento, where, dt);

        DataTable dtBullets = dsBullets.Tables[0];

        valorLimite = getValorLimiteGrafico(dt).ToString();

        //Função que gera o gráfico 1
        geraGrafico1(dt, dtBullets);

        //Função que gera o gráfico 2
        geraGrafico2(dt, dtBullets);
                
        defineLarguraTela();

        
    }

    private void defineLarguraTela()
    {
        int largura, altura;

        if (Request.QueryString["Altura"] != null && Request.QueryString["Largura"] != null)
        {
            largura = int.Parse(Request.QueryString["Largura"].ToString());
            altura = int.Parse(Request.QueryString["Altura"].ToString());

            larguraGrafico = (largura - 10).ToString();
            ASPxRoundPanel1.Width = (largura);
            ASPxRoundPanel1.ContentHeight = altura - 38;

            alturaGrafico = ((altura - 49) / 2).ToString();
        }
        else
        {
            string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
            largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
            altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

            larguraGrafico = ((largura - 290) / 3).ToString();

            ASPxRoundPanel1.Width = ((largura - 264) / 3);
            ASPxRoundPanel1.ContentHeight = (altura - 186) / 2 - 20;

            alturaGrafico = (((altura - 215) / 2) / 2).ToString();

        }
    }

    //Função para geração do gráfico 1
    public void geraGrafico1(DataTable dt, DataTable dtBullets)
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico Bullet
        string nomeGrafico;

        //*****************
        //Criação do Bullet de Esforço
        //cria uma variável com o nome e o caminho do XML do gráfico BULLET de esforço dos Projetos
        nomeGrafico = @"/ArquivosTemporarios/bulletCustoOrcamento_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função que cria a string XML que irá carregar o gráfico de Bullets de Esforço
        xml = cDados.getGraficoBulletsSinalizadores(dt, dtBullets, "Despesa", "DespesaPrevistaData", "DespesaPrevistaTotal", "DespesaReal", valorLimite, "../../ImageSaving/FusionChartsSave.aspx", "(Em Horas)", 9, urlLink);

        //Cria o arquivo XML
        cDados.escreveXML(xml, nomeGrafico);

        //Atribui o nome e o caminho do arquivo que irá carregar o gráfico
        grafico1_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //cria uma variável com o nome e o caminho do XML do gráfico BULLET de esforço dos Projetos
        nomeGrafico = "ArquivosTemporarios/bulletCustoOrcamentoZoom_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;


        //função que cria a string XML que irá carregar o gráfico de Bullets de Esforço de ZOOM
        xml = cDados.getGraficoBulletsSinalizadores(dt, dtBullets, "Despesa", "DespesaPrevistaData", "DespesaPrevistaTotal", "DespesaReal", valorLimite, "../../ImageSaving/FusionChartsSave.aspx", "(Em Horas)", 15, "");

        //Cria o arquivo XML
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado (ZOOM)
        grafico1_xmlzoom = nomeGrafico;

    }

    //Função para geração do gráfico 2
    public void geraGrafico2(DataTable dt, DataTable dtBullets)
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico 2
        string nomeGrafico;

        //cria uma variável com o nome e o caminho do XML do gráfico bullet de custo dos Projetos 
        nomeGrafico = "/ArquivosTemporarios/bulletOrcamentoReceita_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função que cria a string XML que irá carregar o gráfico de Bullets de Custos
        xml = cDados.getGraficoBulletsSinalizadores(dt, dtBullets, "Receita", "ReceitaPrevistaData", "ReceitaPrevistaTotal", "ReceitaReal", valorLimite, "../../ImageSaving/FusionChartsSave.aspx", "(R$)", 9, urlLink);

        //Cria o arquivo XML
        cDados.escreveXML(xml, nomeGrafico);

        //Atribui o nome e o caminho do arquivo XML que carregará o gráfico
        grafico2_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //cria uma variável com o nome e o caminho do XML do gráfico bullet de custo dos Projetos 
        nomeGrafico = "ArquivosTemporarios/bulletReceitaOrcamentoZoom_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função que cria a string XML que irá carregar o gráfico de Bullets de Custos de ZOOM
        xml = cDados.getGraficoBulletsSinalizadores(dt, dtBullets, "Receita", "ReceitaPrevistaData", "ReceitaPrevistaTotal", "ReceitaReal", valorLimite, "../../ImageSaving/FusionChartsSave.aspx", "(R$)", 15, "");

        //Cria o arquivo XML
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado (ZOOM)
        grafico2_xmlzoom = nomeGrafico;

    }

    private int getValorLimiteGrafico(DataTable dt)
    {
        int i;
        int valorLimite = 0;
        float custoPrevisto = 0;
        float receitaPrevista = 0;
        float custoReal = 0;
        float receitaReal = 0;

        for (i = 0; i < dt.Rows.Count; i++)
        {
            custoPrevisto = custoPrevisto + float.Parse(dt.Rows[i]["DespesaPrevistaTotal"].ToString());
            custoReal = custoReal + float.Parse(dt.Rows[i]["DespesaReal"].ToString());
            receitaPrevista = receitaPrevista + float.Parse(dt.Rows[i]["ReceitaPrevistaTotal"].ToString());
            receitaReal = receitaReal + float.Parse(dt.Rows[i]["ReceitaReal"].ToString());
        }

        if (custoPrevisto < custoReal)
        {
            custoPrevisto = custoReal;
        }
        if (receitaPrevista < receitaReal)
        {
            receitaPrevista = receitaReal;
        }

        if (custoPrevisto > receitaPrevista)
        {
            valorLimite = (int)custoPrevisto + ((5 * (int)custoPrevisto) / 100);
        }
        else
        {
            valorLimite = (int)receitaPrevista + ((5 * (int)receitaPrevista) / 100); ;
        }

        return valorLimite;
    }
}
