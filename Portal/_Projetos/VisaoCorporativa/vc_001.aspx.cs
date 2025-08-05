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

public partial class _Portfolios_VisaoCorporativa_vc_001 : System.Web.UI.Page
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
    public string grafico1_titulo = "Projetos - Desempenho Geral";
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

    public string mostrarReceita = "S";
    public string mostrarDespesa = "S";
    public string mostrarEsforco = "S";

    public string alturaGrafico = "";
    public string larguraGrafico = "";

    string valorLimite;

    int numeroGraficos = 3;

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

        //if (cDados.getInfoSistema("CodigoStatus") != null && int.Parse(cDados.getInfoSistema("CodigoStatus").ToString()) != -1)
        //    where += " AND p.CodigoStatusProjeto = " + cDados.getInfoSistema("CodigoStatus").ToString();

        if (cDados.getInfoSistema("CodigosProjetosUsuario") != null)
            where += " AND rp.CodigoProjeto IN (" + cDados.getInfoSistema("CodigosProjetosUsuario").ToString() + ")";
        else
            where += string.Format("AND p.CodigoProjeto in (SELECT codigoProjeto FROM {0}.{1}.f_getProjetosUsuario({2}, {3}, {4})) ", cDados.getDbName(), cDados.getDbOwner(), int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()));

        int anoFinanceiro = Request.QueryString["Financeiro"] != null && Request.QueryString["Financeiro"].ToString() != "" ? int.Parse(Request.QueryString["Financeiro"].ToString()) : -1;
        
        //Data Set contendo a tabela com os dados a serem carregados no gráfico 
        DataSet ds = cDados.getOrcamentoTrabalhoProjetos(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), anoFinanceiro,  where);

        DataTable dt = ds.Tables[0];

        DataSet dsBullets = cDados.getCoresBulletsVCProjetos(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()),anoFinanceiro, where);

        DataTable dtBullets = dsBullets.Tables[0];

        valorLimite = getValorLimiteGrafico(dt).ToString();

        //Função que gera o gráfico 1
        geraGrafico1(dt, dtBullets);

        //Função que gera o gráfico 2
        geraGrafico2(dt, dtBullets);

        DataSet dsParametros = cDados.getParametrosSistema("mostrarValoresReceita", "mostrarValoresDespesa", "mostrarValoresEsforco");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            if (dsParametros.Tables[0].Rows[0]["mostrarValoresReceita"].ToString() == "N")
            {
                mostrarReceita = "N";
                numeroGraficos--;
            }

            if (dsParametros.Tables[0].Rows[0]["mostrarValoresDespesa"].ToString() == "N")
            {
                mostrarDespesa = "N";
                numeroGraficos--;
            }

            if (dsParametros.Tables[0].Rows[0]["mostrarValoresEsforco"].ToString() == "N")
            {
                mostrarEsforco = "N";
                numeroGraficos--;
            }
        }

        geraGrafico3(dt, dtBullets);
               
        defineLarguraTela();

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
            ASPxRoundPanel1.ContentHeight = altura - 54;

            alturaGrafico = ((altura - 55) / numeroGraficos).ToString();
        }
        else
        {
            string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
            largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
            altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

            larguraGrafico = ((largura - 290) / 3).ToString();

            ASPxRoundPanel1.Width = ((largura - 270) / 3);

            ASPxRoundPanel1.ContentHeight = (altura - 190) / 2 - 20;

            alturaGrafico = (((altura - 305) / 2) / numeroGraficos).ToString();
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
        nomeGrafico = @"/ArquivosTemporarios/bulletEsforco_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função que cria a string XML que irá carregar o gráfico de Bullets de Esforço
        xml = cDados.getGraficoBulletsSinalizadores(dt, dtBullets, "Esforço", "TotalTrabalhoPrevisto", "TotalTrabalhoPrevistoGeral", "TotalTrabalhoReal", valorLimite, "../../ImageSaving/FusionChartsSave.aspx", "(Em Horas)", 9, "");

        //Cria o arquivo XML
        cDados.escreveXML(xml, nomeGrafico);

        //Atribui o nome e o caminho do arquivo que irá carregar o gráfico
        grafico1_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //cria uma variável com o nome e o caminho do XML do gráfico BULLET de esforço dos Projetos
        nomeGrafico = "ArquivosTemporarios/bulletEsforcoZoom_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

      
        //função que cria a string XML que irá carregar o gráfico de Bullets de Esforço de ZOOM
        xml = cDados.getGraficoBulletsSinalizadores(dt, dtBullets, "Esforço", "TotalTrabalhoPrevisto", "TotalTrabalhoPrevistoGeral", "TotalTrabalhoReal", valorLimite, "../../ImageSaving/FusionChartsSave.aspx", "(Em Horas)", 15, "");

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
        nomeGrafico = "/ArquivosTemporarios/bulletCustos_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função que cria a string XML que irá carregar o gráfico de Bullets de Custos
        xml = cDados.getGraficoBulletsSinalizadores(dt, dtBullets, "Despesa", "TotalCustoOrcado", "TotalCustoOrcadoGeral", "TotalCustoReal", valorLimite, "../../ImageSaving/FusionChartsSave.aspx", "(R$)", 9, "");

        //Cria o arquivo XML
        cDados.escreveXML(xml, nomeGrafico);

        //Atribui o nome e o caminho do arquivo XML que carregará o gráfico
        grafico2_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //cria uma variável com o nome e o caminho do XML do gráfico bullet de custo dos Projetos 
        nomeGrafico = "ArquivosTemporarios/bulletCustosZoom_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função que cria a string XML que irá carregar o gráfico de Bullets de Custos de ZOOM
        xml = cDados.getGraficoBulletsSinalizadores(dt, dtBullets, "Despesa", "TotalCustoOrcado", "TotalCustoOrcadoGeral", "TotalCustoReal", valorLimite, "../../ImageSaving/FusionChartsSave.aspx", "(R$)", 15, "");

        //Cria o arquivo XML
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado (ZOOM)
        grafico2_xmlzoom = nomeGrafico;

    }

    //Função para geração do gráfico 1
    public void geraGrafico3(DataTable dt, DataTable dtBullets)
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico 3
        string nomeGrafico;

        //cria uma variável com o nome e o caminho do XML do gráfico bullet de custo dos Projetos 
        nomeGrafico = "/ArquivosTemporarios/bulletReceitas_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função que cria a string XML que irá carregar o gráfico de Bullets de Receitas
        xml = cDados.getGraficoBulletsSinalizadores(dt, dtBullets, "Receita", "TotalReceitaOrcada", "TotalReceitaOrcadaGeral", "TotalReceitaReal", valorLimite, "../../ImageSaving/FusionChartsSave.aspx", "(R$)", 9, "");

        //Cria o arquivo XML
        cDados.escreveXML(xml, nomeGrafico);

        //Atribui o nome e o caminho do arquivo XML que irá carregar o gráfico
        grafico3_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //cria uma variável com o nome e o caminho do XML do gráfico bullet de custo dos Projetos 
        nomeGrafico = "ArquivosTemporarios/bulletReceitasZoom_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função que cria a string XML que irá carregar o gráfico de Bullets de Receitas de ZOOM
        xml = cDados.getGraficoBulletsSinalizadores(dt, dtBullets, "Receita", "TotalReceitaOrcada", "TotalReceitaOrcadaGeral", "TotalReceitaReal", valorLimite, "../../ImageSaving/FusionChartsSave.aspx", "(R$)", 15, "");

        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado (ZOOM)
        grafico3_xmlzoom = nomeGrafico;


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
            custoPrevisto = custoPrevisto + float.Parse(dt.Rows[i]["TotalCustoOrcadoGeral"].ToString());
            custoReal = custoReal + float.Parse(dt.Rows[i]["TotalCustoReal"].ToString());
            receitaPrevista = receitaPrevista + float.Parse(dt.Rows[i]["TotalReceitaOrcadaGeral"].ToString());
            receitaReal = receitaReal + float.Parse(dt.Rows[i]["TotalReceitaReal"].ToString());
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
