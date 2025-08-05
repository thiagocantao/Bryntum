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

public partial class _Projetos_VisaoCorporativa_vc_003 : System.Web.UI.Page
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
    public string grafico_swf = "../../../Flashs/ScrollLine2D.swf";
    public string grafico_xml = "";
    public string grafico_xmlzoom = "";

    public int alturaGrafico;
    public int larguraGrafico;

    public int anoSelecionado;

    int codigoEntidade;
    int codigoProjeto = -1;
    
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

        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        carregaComboPrevisoes();

        defineLarguraTela();

        //Função que gera o gráfico
        geraGrafico();

        cDados.defineAlturaFrames(this, alturaGrafico + 31);
        cDados.aplicaEstiloVisual(this);
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        int larguraPaineis = ((largura) / 2 - 105);
        int alturaPaineis = altura - 238;
        larguraGrafico = larguraPaineis - 10;

        alturaGrafico = (altura - 309) / 2;
    }

    //Função para geração do gráfico
    private void geraGrafico()
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        int codigoPrevisao = -1;
        char periodicidade = 'M';

        ASPxComboBox cmbPrevisao = ASPxRoundPanel1.FindControl("ddlPrevisao") as ASPxComboBox;
        ASPxComboBox cmbPeriodicidade = ASPxRoundPanel1.FindControl("ddlPeriodicidade") as ASPxComboBox;

        if (cmbPrevisao.Items.Count > 0 && cmbPrevisao.SelectedIndex != -1)
            codigoPrevisao = int.Parse(cmbPrevisao.Value.ToString());

        if (cmbPeriodicidade.Items.Count > 0 && cmbPeriodicidade.SelectedIndex != -1)
            periodicidade = char.Parse(cmbPeriodicidade.Value.ToString());

        //cria uma variável com o nome e o caminho do XML do gráfico
        string nomeGrafico = @"/ArquivosTemporarios/CurvaS_Previsao_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        DataTable dt = cDados.getCurvaSPrevisaoProjeto(codigoProjeto, codigoPrevisao, periodicidade, "").Tables[0];
        
        //função para gerar a estrutura xml para criação do gráfico
        xml = cDados.getGraficoCurvaS_PrevisaoOrcamentaria(dt, "", 9, 2, 9, 2, 13, periodicidade.ToString());

        //escreve o arquivo xml
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = "../../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //define o caminho e o nome do XML do gráfico de ZOOM 
        nomeGrafico = "\\ArquivosTemporarios\\CurvaS_PrevisaoZOOM_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função para gerar a estrutura xml para criação do gráfico
        xml = cDados.getGraficoCurvaS_PrevisaoOrcamentaria(dt, "Curva S - " + cmbPrevisao.Text, 15, 2, 9, 2, 25, periodicidade.ToString());

        //escreve o arquivo xml
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado (ZOOM)
        grafico_xmlzoom = nomeGrafico;
        grafico_xmlzoom = grafico_xmlzoom.Replace("\\", "/");


        ASPxRoundPanel1.ClientInstanceName = "pc";
        ASPxRoundPanel1.ClientSideEvents.Init = "OnInit";
        ASPxRoundPanel1.JSProperties["cp_grafico1_swf"] = grafico_swf;
        ASPxRoundPanel1.JSProperties["cp_grafico1_xml"] = grafico_xml;
    }

    private void carregaComboPrevisoes()
    {
        ASPxComboBox cmb = ASPxRoundPanel1.FindControl("ddlPrevisao") as ASPxComboBox;

        DataSet ds = cDados.getPrevisoesOrcamentarias(codigoEntidade, "");

        if (cDados.DataSetOk(ds))
        {
            cmb.DataSource = ds;
            cmb.TextField = "DescricaoPrevisao";
            cmb.ValueField = "CodigoPrevisao";

            cmb.DataBind();

            if (!IsPostBack && cDados.DataTableOk(ds.Tables[0]))
            {
                DataRow drOficial = ds.Tables[0].Select("IndicaPrevisaoOficial = 'S'")[0];

                cmb.Value = drOficial["CodigoPrevisao"].ToString();
            }
        }
    }
}
