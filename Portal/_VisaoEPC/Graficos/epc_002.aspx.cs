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

public partial class _VisaoEPC_Graficos_epc_002 : System.Web.UI.Page
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
    public string grafico_xmlzoom = "";

    public string alturaGrafico = "";
    public string larguraGrafico = "";

    public int anoSelecionado;

    int codigoEntidade;

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

        carregaComboContratos();

        defineLarguraTela();

        //Função que gera o gráfico
        geraGrafico();
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x'))) - 20;
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        larguraGrafico = ((largura - 415)).ToString();

        ASPxRoundPanel1.Width = ((largura - 400));
        ASPxRoundPanel1.ContentHeight = (altura - 205) / 2 - 25;
        alturaGrafico = (((altura - 235) / 2)).ToString();
    }

    //Função para geração do gráfico
    private void geraGrafico()
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        int codigoContrato = -1;

        ASPxComboBox cmb = ASPxRoundPanel1.FindControl("ddlContrato") as ASPxComboBox;

        if (cmb.Items.Count > 0 && cmb.SelectedIndex != -1)
            codigoContrato = int.Parse(cmb.Value.ToString());

        //cria uma variável com o nome e o caminho do XML do gráfico
        string nomeGrafico = @"/ArquivosTemporarios/EPC_002_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;
        char periodicidade = cDados.getInfoSistema("PeriodicidadeEPC") == null || cDados.getInfoSistema("PeriodicidadeEPC").ToString() == "" ? 'M' : char.Parse(cDados.getInfoSistema("PeriodicidadeEPC").ToString());

        DataTable dt = cDados.getContratoEPC(codigoContrato, 'N', periodicidade).Tables[0];
        
        //função para gerar a estrutura xml para criação do gráfico
        xml = cDados.getGraficoCurvaS_EPC(dt, "", 9, 2, 9, 2, 13, periodicidade.ToString());

        //escreve o arquivo xml
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //define o caminho e o nome do XML do gráfico de ZOOM 
        nomeGrafico = "\\ArquivosTemporarios\\EPC_002ZOOM_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função para gerar a estrutura xml para criação do gráfico
        xml = cDados.getGraficoCurvaS_EPC(dt, "Curva S - EPC", 15, 2, 9, 2, 25, periodicidade.ToString());

        //escreve o arquivo xml
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado (ZOOM)
        grafico_xmlzoom = nomeGrafico;
        grafico_xmlzoom = grafico_xmlzoom.Replace("\\", "/");

    }

    public string getTitulo()
    {
        return "Curva S";
    }

    private void carregaComboContratos()
    {
        ASPxComboBox cmb = ASPxRoundPanel1.FindControl("ddlContrato") as ASPxComboBox;

        DataSet ds = cDados.getContratoFilhosEPC(1);

        if (cDados.DataSetOk(ds))
        {
            cmb.DataSource = ds;
            cmb.TextField = "DescricaoContratoAgrupado";
            cmb.ValueField = "CodigoContrato";
            cmb.DataBind();

            if (!IsPostBack && cmb.Items.Count > 0)
                cmb.SelectedIndex = 0;
        }
    }
}
