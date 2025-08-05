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
using DevExpress.XtraPivotGrid.Localization;
using DevExpress.Utils.Localization.Internal;
using System.IO;
using DevExpress.XtraPivotGrid;

public partial class _Projetos_DadosProjeto_RecursosHumanos : System.Web.UI.Page
{
    dados cDados;

    int idProjeto = 0;

    //pega a data e hora atual do sistema
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";

    //Mensagens de erro para os gráficos
    public string msgErro = "?LoadDataErrorText=Sem Informações a Apresentar";
    public string msgNoData = "&ChartNoDataText=Sem Informações a Apresentar";
    public string msgInvalid = "&InvalidXMLText=Sem Informações a Apresentar";
    public string desenhando = "&PBarLoadingText=Gerando imagem...";
    public string msgLoading = "&XMLLoadingText=Carregando...";

    public string grafico1_titulo = "Recursos";
    public string grafico1_swf = "../../Flashs/Pie2D.swf";
    public string grafico1_xml = "";
    public string grafico1_xmlzoom = "";

    public int alturaGrafico = 220;
    public int larguraGrafico = 350;

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

        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        
        if (Request.QueryString["idProjeto"] != null)
            idProjeto = int.Parse(Request.QueryString["idProjeto"].ToString());

        if (!IsPostBack)
        {
            int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuario, codigoEntidade, idProjeto, "null", "PR", 0, "null", "PR_AcsTlaRHU");
        }

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        DataSet ds = cDados.getRecursosHumanosProjeto(idProjeto);

        if (cDados.DataSetOk(ds))
        {
            geraGraficoPizza(ds.Tables[0]);
        }
               
        defineAlturaTela();

        imgGraficos.ClientSideEvents.Click = "function(s, e) {window.location.href = 'RecursosHumanos.aspx?idProjeto=" + idProjeto + "';}";
        imgGraficos.Style.Add("cursor", "pointer");
        lblGrafico.ClientSideEvents.Click = "function(s, e) {window.location.href = 'RecursosHumanos.aspx?idProjeto=" + idProjeto + "';}";
        lblGrafico.Style.Add("cursor", "pointer");
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        
        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0,resolucaoCliente.IndexOf('x')));

        pRH.ContentHeight = alturaPrincipal - 205;
        pRH.Width = new Unit("100%");

        alturaGrafico = alturaPrincipal - 215;
        larguraGrafico = larguraPrincipal - 200;
    }

    private void geraGraficoPizza(DataTable dtGrafico)
    {
        string xml = "";

        string nomeGrafico = @"/ArquivosTemporarios/RHPizza_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;
                
        //gera o xml do gráfico Gauge do percentual concluido
        xml = cDados.getGraficoRHProjetoPizza(dtGrafico, "", 9);

        //escreve o arquivo XML do gráfico Gauge do percentual concluido
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico1_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //define o caminho e o nome do XML do gráfico de ZOOM (PIZZA)
        nomeGrafico = "\\ArquivosTemporarios\\RHPizzaZOOM_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //gera o xml do gráfico Gauge do percentual concluido ZOOM
        xml = cDados.getGraficoRHProjetoPizza(dtGrafico, "Recursos do Projeto", 15);

        //escreve o arquivo XML do gráfico Gauge do percentual concluido ZOOM
        cDados.escreveXML(xml, nomeGrafico);

        grafico1_xmlzoom = nomeGrafico;

        grafico1_xmlzoom = grafico1_xmlzoom.Replace("\\", "/");
    }
}
