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

public partial class _VisaoNE_VisaoCorporativa_vne_gantt : System.Web.UI.Page
{
    dados cDados;

    //pega a data e hora atual do sistema
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";

    public int codigoProjeto = 0;

    public string grafico_xml = "";

    public string alturaGrafico = "";
    public string larguraGrafico = "", nenhumGrafico = "";

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
        carregaComboMunicipio();
        carregaComboStatus();

        //Função que gera o gráfico
        geraGrafico();

        DataSet dsParametro = cDados.getParametrosSistema("corSatisfatorio", "corAtencao", "corCritico");

        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            string corSatisfatorio = "#008844", corAtencao = "#FFFF44", corCritico = "#DD0100";

            corSatisfatorio = dsParametro.Tables[0].Rows[0]["corSatisfatorio"] + "" == "" ? corSatisfatorio : dsParametro.Tables[0].Rows[0]["corSatisfatorio"].ToString();
            corAtencao = dsParametro.Tables[0].Rows[0]["corAtencao"] + "" == "" ? corAtencao : dsParametro.Tables[0].Rows[0]["corAtencao"].ToString();
            corCritico = dsParametro.Tables[0].Rows[0]["corCritico"] + "" == "" ? corCritico : dsParametro.Tables[0].Rows[0]["corCritico"].ToString();

            legVerde.Style.Add("background-color", corSatisfatorio);
            legAmarelo.Style.Add("background-color", corAtencao);
            legVermelho.Style.Add("background-color", corCritico);
        }
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));

        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaGrafico = (altura - 190).ToString();
        larguraGrafico = (largura - 15).ToString();
    }

    private void carregaComboMunicipio()
    {
        DataSet ds = cDados.getMunicipiosObra("");

        ddlMunicipio.TextField = "NomeMunicipio";
        ddlMunicipio.ValueField = "CodigoMunicipio";
        ddlMunicipio.DataSource = ds;
        ddlMunicipio.DataBind();

        ListEditItem lei = new ListEditItem(Resources.traducao.todos, "-1");

        ddlMunicipio.Items.Insert(0, lei);

        if (!IsPostBack && !IsCallback)
            ddlMunicipio.SelectedIndex = 0;
    }

    private void carregaComboStatus()
    {
        DataSet ds = cDados.getStatusObra("");

        ddlStatus.TextField = "DescricaoStatus";
        ddlStatus.ValueField = "CodigoStatus";
        ddlStatus.DataSource = ds;
        ddlStatus.DataBind();

        ListEditItem lei = new ListEditItem(Resources.traducao.todos, "-1");

        ddlStatus.Items.Insert(0, lei);

        if (!IsPostBack && !IsCallback)
            ddlStatus.SelectedIndex = 0;
    }

    //Função para geração do gráfico (Quantidade de Projetos por Desempenho) - PIZZA
    private void geraGrafico()
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico de PIZZA
        string nomeGrafico = @"/ArquivosTemporarios/GanttObras_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        string where = "";

        if (ddlMunicipio.Value.ToString() != "-1")
            where += " AND o.CodigoMunicipioObra = " + ddlMunicipio.Value;

        if (ddlStatus.Value.ToString() != "-1")
            where += " AND f.CodigoStatus = " + ddlStatus.Value;

        int anoParam = cDados.getInfoSistema("AnoContrato") == null || cDados.getInfoSistema("AnoContrato").ToString() == "-1" ? DateTime.Now.Year : int.Parse(cDados.getInfoSistema("AnoContrato").ToString());

        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        DataSet ds = cDados.getDadosGanttObras(codigoUsuario, codigoEntidade, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()), anoParam, where);

        //DataSet dsDatasGantt = cDados.getDatasGanttProjetos(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoPortfolio").ToString()), "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            //função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
            xml = cDados.getGraficoGanttObras(ds.Tables[0]);
        }
        else
        {
            nenhumGrafico = cDados.getGanttVazio(alturaGrafico);
            alturaGrafico = "0";
        }

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = "../.." + nomeGrafico;
    }
}
