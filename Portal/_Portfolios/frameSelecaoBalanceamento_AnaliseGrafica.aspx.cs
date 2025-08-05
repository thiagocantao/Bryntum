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
using System.Web.Hosting;
using DevExpress.Web;

public partial class _Portfolios_frameSelecaoBalanceamento_AnaliseGrafica : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidadeUsuarioResponsavel;

    public string msgErro = "?LoadDataErrorText=Sem Informações a Apresentar";
    public string msgNoData = "&ChartNoDataText=Sem Informações a Apresentar";
    public string msgInvalid = "&InvalidXMLText=Sem Informações a Apresentar";
    public string desenhando = "&PBarLoadingText=Gerando imagem...";
    public string msgLoading = "&XMLLoadingText=Carregando...";

    public string alturaGrafico = "450";
    public string larguraGrafico = "435";

    //Cria as variáveis para carregar o gráfico (Desempenho Geral)
    public string grafico_titulo = "Análise Gráfica";
    public string grafico_swf = "../Flashs/Bubble.swf";
    public string grafico_xml = "";
    public string grafico_xmlzoom = "";

    string where = "";
    char opcao = 'C';

    //pega a data e hora atual do sistema
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";

    DataTable dtCombos = new DataTable();

    public string controlaRH = "none";

    int codigoPortfolio = 0;

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

        if (Request.QueryString["CodigoPortfolio"] != null && Request.QueryString["CodigoPortfolio"].ToString() != "")
            codigoPortfolio = int.Parse(Request.QueryString["CodigoPortfolio"].ToString());
        else if (cDados.getInfoSistema("CodigoPortfolio") != null)
            codigoPortfolio = int.Parse(cDados.getInfoSistema("CodigoPortfolio").ToString());

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        carregaComboCategorias();
        carregaCombosEixos();
        carregaComboAnos();

        this.TH(this.TS("FusionCharts"));


        DataSet dsParametro = cDados.getParametrosSistema("labelDespesa");
        string lblDespesa = "";

        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            ddlTamanhoBolha.Items.RemoveAt(0);
            lblDespesa = dsParametro.Tables[0].Rows[0]["labelDespesa"].ToString();
            ListEditItem li = new ListEditItem(string.Format("{0} Total", lblDespesa), "Despesa");
            ddlTamanhoBolha.Items.Insert(0, li);
            ddlTamanhoBolha.DataBind();
        }

        geraGrafico();

        defineTamanhoGrafico();

        DataSet ds = cDados.getParametrosSistema("controlaRHPortfolio");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            controlaRH = ds.Tables[0].Rows[0]["controlaRHPortfolio"] + "" == "S" ? "block" : "none";

        carregaGrids();

        gvDespesa.Settings.ShowFilterRow = false;
        gvDespesa.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvReceita.Settings.ShowFilterRow = false;
        gvReceita.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
    }

    private void geraGrafico()
    {
        if (ddlEixoX.Items.Count > 0 && ddlEixoY.Items.Count > 0 && ddlEixoX.SelectedIndex != -1 && ddlEixoY.SelectedIndex != -1)
        {
            DataRow[] drsTipoEscolhaX = dtCombos.Select("CodigoCriterioSelecao = " + ddlEixoX.Value.ToString());
            DataRow[] drsTipoEscolhaY = dtCombos.Select("CodigoCriterioSelecao = " + ddlEixoY.Value.ToString());



            if (ddlAnalise.SelectedIndex != 0)
            {
                switch (ddlAnalise.SelectedIndex)
                {
                    case 1: where = " AND IndicaCenario1 = 'S' ";                            
                        break;
                    case 2: where = " AND IndicaCenario2 = 'S' ";
                        break;           
                    case 3: where = " AND IndicaCenario3 = 'S' ";
                        break;           
                    case 4: where = " AND IndicaCenario4 = 'S' ";
                        break;           
                    case 5: where = " AND IndicaCenario5 = 'S' ";
                        break;          
                    case 6: where = " AND IndicaCenario6 = 'S' ";
                        break;           
                    case 7: where = " AND IndicaCenario7 = 'S' ";
                        break;           
                    case 8: where = " AND IndicaCenario8 = 'S' ";
                        break;            
                    case 9: where = " AND IndicaCenario9 = 'S' ";
                        break;
                    default: where = "";
                        break;
                }

                opcao = 'P';
            }

            DataSet dsGrid = cDados.getProjetosPorCriterio(int.Parse(ddlAno.Value.ToString()), codigoPortfolio, codigoEntidadeUsuarioResponsavel, where);



            DataSet dsPortfolioSuperior = cDados.getPortfoliosFiltroPorCenario(codigoEntidadeUsuarioResponsavel, where, codigoPortfolio);






            DataTable dt = cDados.getAnaliseGraficaPorCarteira(codigoEntidadeUsuarioResponsavel, int.Parse(ddlEixoX.Value.ToString()), int.Parse(ddlEixoY.Value.ToString())
                , drsTipoEscolhaX[0]["TipoEscolha"].ToString(), drsTipoEscolhaY[0]["TipoEscolha"].ToString(), opcao
                , int.Parse(ddlCategoria.Value.ToString()), int.Parse(ddlAno.Value.ToString()), false, codigoPortfolio,  where).Tables[0];



            //cria  a variável para armazenar o XML
            string xml = "";

            string nomeGrafico = @"/ArquivosTemporarios/graficoBolhas_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

            if (ddlAnalise.SelectedIndex != 0)
            {
                xml = cDados.geraAnaliseGraficaPortfolio(dt, ddlTamanhoBolha.Value.ToString(), ddlEixoX.SelectedItem.Text, ddlEixoY.SelectedItem.Text, ddlTamanhoBolha.SelectedItem.Text, HostingEnvironment.ApplicationPhysicalPath + "ExportHandlers\\ASP_Net", 10);
            }
            else
            {
                xml = cDados.geraAnaliseGraficaCriterios(dt, ddlTamanhoBolha.Value.ToString(), ddlEixoX.SelectedItem.Text, ddlEixoY.SelectedItem.Text, ddlTamanhoBolha.SelectedItem.Text, HostingEnvironment.ApplicationPhysicalPath + "ExportHandlers\\ASP_Net", 10);
            }

            cDados.escreveXML(xml, nomeGrafico);

            //atribui o valor do caminho do XML a ser carregado
            grafico_xml = ".." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

            nomeGrafico = "\\ArquivosTemporarios\\graficoBolhasZoom_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

            if (ddlAnalise.SelectedIndex != 0)
            {
                xml = cDados.geraAnaliseGraficaPortfolio(dt, ddlTamanhoBolha.Value.ToString(), ddlEixoX.SelectedItem.Text, ddlEixoY.SelectedItem.Text, ddlTamanhoBolha.SelectedItem.Text, HostingEnvironment.ApplicationPhysicalPath + "ExportHandlers\\ASP_Net", 16);
            }
            else
            {
                xml = cDados.geraAnaliseGraficaCriterios(dt, ddlTamanhoBolha.Value.ToString(), ddlEixoX.SelectedItem.Text, ddlEixoY.SelectedItem.Text, ddlTamanhoBolha.SelectedItem.Text, HostingEnvironment.ApplicationPhysicalPath + "ExportHandlers\\ASP_Net", 16);
            }

            cDados.escreveXML(xml, nomeGrafico);

            //atribui o valor do caminho do XML a ser carregado (ZOOM)
            grafico_xmlzoom = nomeGrafico;
            grafico_xmlzoom = grafico_xmlzoom.Replace("\\", "/");
        }
    }

    private void defineTamanhoGrafico()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraGrafico = (largura - 565).ToString();

        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaGrafico = (altura - 275).ToString();

        gvReceita.Settings.VerticalScrollableHeight = altura - 448;
    }

    private void carregaCombosEixos()
    {
        dtCombos = cDados.getOpcoesCombosEixosAnaliseGrafica(codigoEntidadeUsuarioResponsavel, "").Tables[0];

        ddlEixoX.DataSource = dtCombos;

        ddlEixoX.TextField = "DescricaoCriterioSelecao";

        ddlEixoX.ValueField = "CodigoCriterioSelecao";

        ddlEixoX.DataBind();

        ddlEixoY.DataSource = dtCombos;

        ddlEixoY.TextField = "DescricaoCriterioSelecao";

        ddlEixoY.ValueField = "CodigoCriterioSelecao";

        ddlEixoY.DataBind();

        if (dtCombos.Rows.Count > 0 && !IsPostBack)
        {
            ddlEixoX.SelectedIndex = 0;
            if (dtCombos.Rows.Count > 1)
            {
                ddlEixoY.SelectedIndex = 1;
            }
            else
            {
                ddlEixoY.SelectedIndex = 0;
            }
        }
    }
   
    private void carregaComboAnos()
    {
        string where = " AND IndicaAnoPeriodoEditavel = 'S'";
        DataSet dsAnos = cDados.getPeriodoAnalisePortfolio(codigoEntidadeUsuarioResponsavel, where);

        if (cDados.DataSetOk(dsAnos))
        {
            ddlAno.DataSource = dsAnos;

            ddlAno.TextField = "Ano";

            ddlAno.ValueField = "Ano";

            ddlAno.DataBind();

            ListEditItem lei = new ListEditItem(Resources.traducao.todos, "-1");

            ddlAno.Items.Insert(0, lei);

            if (!IsPostBack)
                ddlAno.SelectedIndex = 0;
        }
    }

   private void carregaComboCategorias()
    {
        DataSet ds = cDados.getCategoria(" AND CodigoEntidade = " + codigoEntidadeUsuarioResponsavel);

        ddlCategoria.DataSource = ds;

        ddlCategoria.TextField = "SiglaCategoria";

        ddlCategoria.ValueField = "CodigoCategoria";

        DataRow dr = ds.Tables[0].NewRow();

        dr["SiglaCategoria"] = Resources.traducao.todas;
        dr["DescricaoCategoria"] = Resources.traducao.todas;
        dr["CodigoCategoria"] = "-1";
        ds.Tables[0].Rows.InsertAt(dr, 0);

        ddlCategoria.DataBind();

        if (!IsPostBack)
            ddlCategoria.SelectedIndex = 0;
    }

    private void carregaGrids()
    {
        DataRow[] drsTipoEscolhaX = dtCombos.Select("CodigoCriterioSelecao = " + ddlEixoX.Value.ToString());
        DataRow[] drsTipoEscolhaY = dtCombos.Select("CodigoCriterioSelecao = " + ddlEixoY.Value.ToString());

        //DataTable dt = cDados.getAnaliseGraficaPorPortifolioEporCenario(codigoEntidadeUsuarioResponsavel, int.Parse(ddlEixoX.Value.ToString()), int.Parse(ddlEixoY.Value.ToString())
        //        , drsTipoEscolhaX[0]["TipoEscolha"].ToString(), drsTipoEscolhaY[0]["TipoEscolha"].ToString(), 'C', int.Parse(ddlCategoria.Value.ToString()), -1, false, "", codigoPortfolio, "-1").Tables[0];


        DataTable dt = cDados.getAnaliseGraficaPorCarteira(codigoEntidadeUsuarioResponsavel, int.Parse(ddlEixoX.Value.ToString()), int.Parse(ddlEixoY.Value.ToString())
    , drsTipoEscolhaX[0]["TipoEscolha"].ToString(), drsTipoEscolhaY[0]["TipoEscolha"].ToString(), opcao
    , int.Parse(ddlCategoria.Value.ToString()), int.Parse(ddlAno.Value.ToString()), false, codigoPortfolio, where).Tables[0];


        carregaGridDespesaReceita(dt, 0);
    }

    private void carregaGridDespesaReceita(DataTable dt, double recursosDisponiveis)
    {
        try
        {

            double despesaTotalCenario1 = double.Parse(dt.Rows[0]["Despesa"].ToString());
            double despesaTotalCenario2 = double.Parse(dt.Rows[1]["Despesa"].ToString());
            double despesaTotalCenario3 = double.Parse(dt.Rows[2]["Despesa"].ToString());
            double despesaTotalCenario4 = double.Parse(dt.Rows[3]["Despesa"].ToString());
            double despesaTotalCenario5 = double.Parse(dt.Rows[4]["Despesa"].ToString());
            double despesaTotalCenario6 = double.Parse(dt.Rows[5]["Despesa"].ToString());
            double despesaTotalCenario7 = double.Parse(dt.Rows[6]["Despesa"].ToString());
            double despesaTotalCenario8 = double.Parse(dt.Rows[7]["Despesa"].ToString());
            double despesaTotalCenario9 = double.Parse(dt.Rows[8]["Despesa"].ToString());

            //gvDespesa.SettingsText.Title = string.Format("Recursos Financeiros Disponíveis: {0:n2}", recursosDisponiveis);

            DataTable dtRF = new DataTable();

            dtRF.Columns.Add("Descricao");
            dtRF.Columns.Add("Cenario1", Type.GetType("System.Double"));
            dtRF.Columns.Add("Cenario2", Type.GetType("System.Double"));
            dtRF.Columns.Add("Cenario3", Type.GetType("System.Double"));
            dtRF.Columns.Add("Cenario4", Type.GetType("System.Double"));
            dtRF.Columns.Add("Cenario5", Type.GetType("System.Double"));
            dtRF.Columns.Add("Cenario6", Type.GetType("System.Double"));
            dtRF.Columns.Add("Cenario7", Type.GetType("System.Double"));
            dtRF.Columns.Add("Cenario8", Type.GetType("System.Double"));
            dtRF.Columns.Add("Cenario9", Type.GetType("System.Double"));

            DataRow dr = dtRF.NewRow();

            dr["Descricao"] = "Despesa Total";        

            DataSet dsParametro = cDados.getParametrosSistema("labelDespesa");
            if(cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
            {
                dr["Descricao"] = string.Format("{0} Total", dsParametro.Tables[0].Rows[0]["labelDespesa"].ToString());
            }
            dr["Cenario1"] = despesaTotalCenario1;
            dr["Cenario2"] = despesaTotalCenario2;
            dr["Cenario3"] = despesaTotalCenario3;
            dr["Cenario4"] = despesaTotalCenario4;
            dr["Cenario5"] = despesaTotalCenario5;
            dr["Cenario6"] = despesaTotalCenario6;
            dr["Cenario7"] = despesaTotalCenario7;
            dr["Cenario8"] = despesaTotalCenario8;
            dr["Cenario9"] = despesaTotalCenario9;

            dtRF.Rows.Add(dr);

            dr = dtRF.NewRow();
            dr["Descricao"] = Resources.traducao.__utilizado;
            dr["Cenario1"] = (recursosDisponiveis == 0) ? 0 : (despesaTotalCenario1 / recursosDisponiveis) * 100;
            dr["Cenario2"] = (recursosDisponiveis == 0) ? 0 : (despesaTotalCenario2 / recursosDisponiveis) * 100;
            dr["Cenario3"] = (recursosDisponiveis == 0) ? 0 : (despesaTotalCenario3 / recursosDisponiveis) * 100;
            dr["Cenario4"] = (recursosDisponiveis == 0) ? 0 : (despesaTotalCenario4 / recursosDisponiveis) * 100;
            dr["Cenario5"] = (recursosDisponiveis == 0) ? 0 : (despesaTotalCenario5 / recursosDisponiveis) * 100;
            dr["Cenario6"] = (recursosDisponiveis == 0) ? 0 : (despesaTotalCenario6 / recursosDisponiveis) * 100;
            dr["Cenario7"] = (recursosDisponiveis == 0) ? 0 : (despesaTotalCenario7 / recursosDisponiveis) * 100;
            dr["Cenario8"] = (recursosDisponiveis == 0) ? 0 : (despesaTotalCenario8 / recursosDisponiveis) * 100;
            dr["Cenario9"] = (recursosDisponiveis == 0) ? 0 : (despesaTotalCenario9 / recursosDisponiveis) * 100;

            dtRF.Rows.Add(dr);

            dr = dtRF.NewRow();
            dr["Descricao"] = Resources.traducao.folga;
            dr["Cenario1"] = (recursosDisponiveis - despesaTotalCenario1);
            dr["Cenario2"] = (recursosDisponiveis - despesaTotalCenario2);
            dr["Cenario3"] = (recursosDisponiveis - despesaTotalCenario3);
            dr["Cenario4"] = (recursosDisponiveis - despesaTotalCenario4);
            dr["Cenario5"] = (recursosDisponiveis - despesaTotalCenario5);
            dr["Cenario6"] = (recursosDisponiveis - despesaTotalCenario6);
            dr["Cenario7"] = (recursosDisponiveis - despesaTotalCenario7);
            dr["Cenario8"] = (recursosDisponiveis - despesaTotalCenario8);
            dr["Cenario9"] = (recursosDisponiveis - despesaTotalCenario9);

            dtRF.Rows.Add(dr);

            dr = dtRF.NewRow();
            dr["Descricao"] = Resources.traducao.__folga;
            dr["Cenario1"] = (recursosDisponiveis == 0) ? 0 : ((recursosDisponiveis - despesaTotalCenario1) / recursosDisponiveis) * 100;
            dr["Cenario2"] = (recursosDisponiveis == 0) ? 0 : ((recursosDisponiveis - despesaTotalCenario2) / recursosDisponiveis) * 100;
            dr["Cenario3"] = (recursosDisponiveis == 0) ? 0 : ((recursosDisponiveis - despesaTotalCenario3) / recursosDisponiveis) * 100;
            dr["Cenario4"] = (recursosDisponiveis == 0) ? 0 : ((recursosDisponiveis - despesaTotalCenario4) / recursosDisponiveis) * 100;
            dr["Cenario5"] = (recursosDisponiveis == 0) ? 0 : ((recursosDisponiveis - despesaTotalCenario5) / recursosDisponiveis) * 100;
            dr["Cenario6"] = (recursosDisponiveis == 0) ? 0 : ((recursosDisponiveis - despesaTotalCenario6) / recursosDisponiveis) * 100;
            dr["Cenario7"] = (recursosDisponiveis == 0) ? 0 : ((recursosDisponiveis - despesaTotalCenario7) / recursosDisponiveis) * 100;
            dr["Cenario8"] = (recursosDisponiveis == 0) ? 0 : ((recursosDisponiveis - despesaTotalCenario8) / recursosDisponiveis) * 100;
            dr["Cenario9"] = (recursosDisponiveis == 0) ? 0 : ((recursosDisponiveis - despesaTotalCenario9) / recursosDisponiveis) * 100;

            dtRF.Rows.Add(dr);

            gvDespesa.DataSource = dtRF;

            gvDespesa.DataBind();
        }
        catch (Exception)
        {
        }

        try
        {
            DataTable dtReceita = new DataTable();

            dtReceita.Columns.Add("Descricao");
            dtReceita.Columns.Add("Cenario1", Type.GetType("System.String"));
            dtReceita.Columns.Add("Cenario2", Type.GetType("System.String"));
            dtReceita.Columns.Add("Cenario3", Type.GetType("System.String"));
            dtReceita.Columns.Add("Cenario4", Type.GetType("System.String"));
            dtReceita.Columns.Add("Cenario5", Type.GetType("System.String"));
            dtReceita.Columns.Add("Cenario6", Type.GetType("System.String"));
            dtReceita.Columns.Add("Cenario7", Type.GetType("System.String"));
            dtReceita.Columns.Add("Cenario8", Type.GetType("System.String"));
            dtReceita.Columns.Add("Cenario9", Type.GetType("System.String"));

            DataRow dr = dtReceita.NewRow();

            dr["Descricao"] = Resources.traducao.receita_total__r__;
            dr["Cenario1"] = string.Format("{0:n2}", double.Parse(dt.Rows[0]["Receita"].ToString()));
            dr["Cenario2"] = string.Format("{0:n2}", double.Parse(dt.Rows[1]["Receita"].ToString()));
            dr["Cenario3"] = string.Format("{0:n2}", double.Parse(dt.Rows[2]["Receita"].ToString()));
            dr["Cenario4"] = string.Format("{0:n2}", double.Parse(dt.Rows[3]["Receita"].ToString()));
            dr["Cenario5"] = string.Format("{0:n2}", double.Parse(dt.Rows[4]["Receita"].ToString()));
            dr["Cenario6"] = string.Format("{0:n2}", double.Parse(dt.Rows[5]["Receita"].ToString()));
            dr["Cenario7"] = string.Format("{0:n2}", double.Parse(dt.Rows[6]["Receita"].ToString()));
            dr["Cenario8"] = string.Format("{0:n2}", double.Parse(dt.Rows[7]["Receita"].ToString()));
            dr["Cenario9"] = string.Format("{0:n2}", double.Parse(dt.Rows[8]["Receita"].ToString()));

            dtReceita.Rows.Add(dr);

            dr = dtReceita.NewRow();

            dr["Descricao"] = Resources.traducao.recurso_h_;
            dr["Cenario1"] = string.Format("{0:n2}", double.Parse(dt.Rows[0]["Trabalho"].ToString()));
            dr["Cenario2"] = string.Format("{0:n2}", double.Parse(dt.Rows[1]["Trabalho"].ToString()));
            dr["Cenario3"] = string.Format("{0:n2}", double.Parse(dt.Rows[2]["Trabalho"].ToString()));
            dr["Cenario4"] = string.Format("{0:n2}", double.Parse(dt.Rows[3]["Trabalho"].ToString()));
            dr["Cenario5"] = string.Format("{0:n2}", double.Parse(dt.Rows[4]["Trabalho"].ToString()));
            dr["Cenario6"] = string.Format("{0:n2}", double.Parse(dt.Rows[5]["Trabalho"].ToString()));
            dr["Cenario7"] = string.Format("{0:n2}", double.Parse(dt.Rows[6]["Trabalho"].ToString()));
            dr["Cenario8"] = string.Format("{0:n2}", double.Parse(dt.Rows[7]["Trabalho"].ToString()));
            dr["Cenario9"] = string.Format("{0:n2}", double.Parse(dt.Rows[8]["Trabalho"].ToString()));

            dtReceita.Rows.Add(dr);

            dr = dtReceita.NewRow();

            DataSet dsParametro = cDados.getParametrosSistema("labelScore");

            string labelScore = "Pontuação";

            if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
            {
                labelScore = dsParametro.Tables[0].Rows[0]["labelScore"] + "";
            }

            dr["Descricao"] = labelScore;
            dr["Cenario1"] = string.Format("{0:n2}", double.Parse(dt.Rows[0]["ValorCriterio"].ToString()));
            dr["Cenario2"] = string.Format("{0:n2}", double.Parse(dt.Rows[1]["ValorCriterio"].ToString()));
            dr["Cenario3"] = string.Format("{0:n2}", double.Parse(dt.Rows[2]["ValorCriterio"].ToString()));
            dr["Cenario4"] = string.Format("{0:n2}", double.Parse(dt.Rows[3]["ValorCriterio"].ToString()));
            dr["Cenario5"] = string.Format("{0:n2}", double.Parse(dt.Rows[4]["ValorCriterio"].ToString()));
            dr["Cenario6"] = string.Format("{0:n2}", double.Parse(dt.Rows[5]["ValorCriterio"].ToString()));
            dr["Cenario7"] = string.Format("{0:n2}", double.Parse(dt.Rows[6]["ValorCriterio"].ToString()));
            dr["Cenario8"] = string.Format("{0:n2}", double.Parse(dt.Rows[7]["ValorCriterio"].ToString()));
            dr["Cenario9"] = string.Format("{0:n2}", double.Parse(dt.Rows[8]["ValorCriterio"].ToString()));

            dtReceita.Rows.Add(dr);

            dr = dtReceita.NewRow();

            dr["Descricao"] = Resources.traducao.riscos;
            dr["Cenario1"] = string.Format("{0:n2}", double.Parse(dt.Rows[0]["ValorRisco"].ToString()));
            dr["Cenario2"] = string.Format("{0:n2}", double.Parse(dt.Rows[1]["ValorRisco"].ToString()));
            dr["Cenario3"] = string.Format("{0:n2}", double.Parse(dt.Rows[2]["ValorRisco"].ToString()));
            dr["Cenario4"] = string.Format("{0:n2}", double.Parse(dt.Rows[3]["ValorRisco"].ToString()));
            dr["Cenario5"] = string.Format("{0:n2}", double.Parse(dt.Rows[4]["ValorRisco"].ToString()));
            dr["Cenario6"] = string.Format("{0:n2}", double.Parse(dt.Rows[5]["ValorRisco"].ToString()));
            dr["Cenario7"] = string.Format("{0:n2}", double.Parse(dt.Rows[6]["ValorRisco"].ToString()));
            dr["Cenario8"] = string.Format("{0:n2}", double.Parse(dt.Rows[7]["ValorRisco"].ToString()));
            dr["Cenario9"] = string.Format("{0:n2}", double.Parse(dt.Rows[8]["ValorRisco"].ToString()));

            dtReceita.Rows.Add(dr);

            dr = dtReceita.NewRow();

            dr["Descricao"] = Resources.traducao.projetos;
            dr["Cenario1"] = string.Format("{0:n0}", double.Parse(dt.Rows[0]["QuantidadeProjetos"].ToString()));
            dr["Cenario2"] = string.Format("{0:n0}", double.Parse(dt.Rows[1]["QuantidadeProjetos"].ToString()));
            dr["Cenario3"] = string.Format("{0:n0}", double.Parse(dt.Rows[2]["QuantidadeProjetos"].ToString()));
            dr["Cenario4"] = string.Format("{0:n0}", double.Parse(dt.Rows[3]["QuantidadeProjetos"].ToString()));
            dr["Cenario5"] = string.Format("{0:n0}", double.Parse(dt.Rows[4]["QuantidadeProjetos"].ToString()));
            dr["Cenario6"] = string.Format("{0:n0}", double.Parse(dt.Rows[5]["QuantidadeProjetos"].ToString()));
            dr["Cenario7"] = string.Format("{0:n0}", double.Parse(dt.Rows[6]["QuantidadeProjetos"].ToString()));
            dr["Cenario8"] = string.Format("{0:n0}", double.Parse(dt.Rows[7]["QuantidadeProjetos"].ToString()));
            dr["Cenario9"] = string.Format("{0:n0}", double.Parse(dt.Rows[8]["QuantidadeProjetos"].ToString()));

            dtReceita.Rows.Add(dr);
            gvReceita.DataSource = dtReceita;
            gvReceita.DataBind();

            var dataTableReceita = gvReceita.DataSource as DataTable;
            var dataTableDespesa = gvDespesa.DataSource as DataTable;

            foreach (DataRow row in dataTableReceita.Rows)
            {
                dataTableDespesa.ImportRow(row);
            }

            //gvReceita.DataSource = dtReceita;
            gvReceita.DataSource = dataTableDespesa;
            gvReceita.DataBind();

            

        }
        catch (Exception)
        {
        }
    }

   
    
    protected void gvDespesa_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        double recursosDisponiveis = (e.Parameters + "" != "") ? double.Parse(e.Parameters.ToString()) : 0;

        DataRow[] drsTipoEscolhaX = dtCombos.Select("CodigoCriterioSelecao = " + ddlEixoX.Value.ToString());
        DataRow[] drsTipoEscolhaY = dtCombos.Select("CodigoCriterioSelecao = " + ddlEixoY.Value.ToString());


        DataTable dt = cDados.getAnaliseGraficaPorCarteira(codigoEntidadeUsuarioResponsavel, int.Parse(ddlEixoX.Value.ToString()), int.Parse(ddlEixoY.Value.ToString())
            , drsTipoEscolhaX[0]["TipoEscolha"].ToString(), drsTipoEscolhaY[0]["TipoEscolha"].ToString(), opcao
            , int.Parse(ddlCategoria.Value.ToString()), int.Parse(ddlAno.Value.ToString()), false, codigoPortfolio, where).Tables[0];


        carregaGridDespesaReceita(dt, recursosDisponiveis);
    }

    protected void gvReceita_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        double recursosDisponiveis = (e.Parameters + "" != "") ? double.Parse(e.Parameters.ToString()) : 0;

        DataRow[] drsTipoEscolhaX = dtCombos.Select("CodigoCriterioSelecao = " + ddlEixoX.Value.ToString());
        DataRow[] drsTipoEscolhaY = dtCombos.Select("CodigoCriterioSelecao = " + ddlEixoY.Value.ToString());


        DataTable dt = cDados.getAnaliseGraficaPorCarteira(codigoEntidadeUsuarioResponsavel, int.Parse(ddlEixoX.Value.ToString()), int.Parse(ddlEixoY.Value.ToString())
            , drsTipoEscolhaX[0]["TipoEscolha"].ToString(), drsTipoEscolhaY[0]["TipoEscolha"].ToString(), opcao
            , int.Parse(ddlCategoria.Value.ToString()), int.Parse(ddlAno.Value.ToString()), false, codigoPortfolio, where).Tables[0];


        carregaGridDespesaReceita(dt, recursosDisponiveis);
    }
}
