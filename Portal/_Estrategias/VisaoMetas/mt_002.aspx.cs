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
using DevExpress.Web;

public partial class mt_002 : System.Web.UI.Page
{
    dados cDados;
    int codigoIndicador = 0;
    string unidadeMedida = "";
    int codigoUnidadeNegocio = 0;
    int casasDecimais = 0;

    //pega a data e hora atual do sistema
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";

    //Mensagens de erro para os gráficos
    public string msgErro = "?LoadDataErrorText=Sem Informações a Apresentar";
    public string msgNoData = "&ChartNoDataText=Sem Informações a Apresentar";
    public string msgInvalid = "&InvalidXMLText=Sem Informações a Apresentar";
    public string desenhando = "&PBarLoadingText=Gerando imagem...";
    public string msgLoading = "&XMLLoadingText=Carregando...";

    //Cria as variáveis para carregar o 1º gráfico de Bullets (Custos)
    public string grafico_titulo = "";
    public string grafico_swf = "../../Flashs/MSCombi2D.swf";
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

        if (Request.QueryString["UN"] != null && Request.QueryString["UN"].ToString() != "")
            codigoUnidadeNegocio = int.Parse(Request.QueryString["UN"].ToString());
        else
            codigoUnidadeNegocio = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (Request.QueryString["CI"] != null && Request.QueryString["CI"].ToString() != "")
            codigoIndicador = int.Parse(Request.QueryString["CI"].ToString());

        if (Request.QueryString["UM"] != null && Request.QueryString["UM"].ToString() != "")
            unidadeMedida = Request.QueryString["UM"].ToString();

        if (Request.QueryString["CD"] != null && Request.QueryString["CD"].ToString() != "")
            casasDecimais = int.Parse(Request.QueryString["CD"].ToString());

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        defineLarguraTela();

        //Função que gera o gráfico
        geraGrafico();

        carregaGridProjetos();

        montaCamposAnalise();

        cDados.aplicaEstiloVisual(this);

        gridProjetos.Settings.ShowFilterRow = false;
        gridProjetos.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        larguraGrafico = ((largura - 180) / 2).ToString();
        alturaGrafico = "220";
        //gridProjetos.Width = (int)((largura - 120) / 2);
    }

    //Função para geração do gráfico (Quantidade de Projetos por Desempenho) - PIZZA
    private void geraGrafico()
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico de PIZZA
        string nomeGrafico = @"/ArquivosTemporarios/metas_" + codigoIndicador + "_" + dataHora;
       
        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        DataSet dsGrafico = cDados.getPeriodicidadeIndicador(codigoUnidadeNegocio, codigoIndicador, "");

        if (cDados.DataSetOk(dsGrafico))
        {
            DataTable dt = dsGrafico.Tables[0];

            //gera o xml do gráfico Gauge do percentual concluido
            xml = cDados.getGraficoPeriodosIndicadorMetas2(dt, "", 9, casasDecimais, 9, 2, 12, codigoIndicador.ToString(),codigoUnidadeNegocio.ToString());

            //escreve o arquivo xml de quantidade de projetos por entidade
            cDados.escreveXML(xml, nomeGrafico);

            //atribui o valor do caminho do XML a ser carregado
            grafico_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;
        }

    }

    private void carregaGridProjetos()
    {
        int codigoEntidadeUnidade = cDados.getEntidadUnidadeNegocio(codigoUnidadeNegocio);
        DataSet dsProjetos = cDados.getProjetosIndicador(codigoIndicador, codigoEntidadeUnidade, "");

        if (cDados.DataSetOk(dsProjetos))
        {
            gridProjetos.DataSource = dsProjetos;

            gridProjetos.DataBind();
        }
    }

    private void montaCamposAnalise()
    {
        string where = " AND ap.CodigoUnidadeNegocio = " + codigoUnidadeNegocio;

        DataSet dsAnalise = cDados.getUltimaAnaliseIndicador(codigoIndicador, 'E', where);

        if (cDados.DataSetOk(dsAnalise) && cDados.DataTableOk(dsAnalise.Tables[0]))
        {
            string nomeResponsavel = dsAnalise.Tables[0].Rows[0]["Responsavel"].ToString();

            if (nomeResponsavel.IndexOf(' ') != -1)
            {
                nomeResponsavel = nomeResponsavel.Substring(0, nomeResponsavel.IndexOf(' ')) + " " + nomeResponsavel.Substring(nomeResponsavel.LastIndexOf(' '));
            }


            txtAnalise.Text = dsAnalise.Tables[0].Rows[0]["Analise"].ToString();
            lblAnalise.Text = string.Format("Análise feita por {0} em {1}", nomeResponsavel
                                                                          , dsAnalise.Tables[0].Rows[0]["DataAnalise"].ToString());
        }
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "ListaMetas");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "ListaMetas", "Painel de Metas", this);
    }

    #endregion
    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Group)
        {
            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }
    }

    protected void txtAnalise_TextChanged(object sender, EventArgs e)
    {

    }
}
