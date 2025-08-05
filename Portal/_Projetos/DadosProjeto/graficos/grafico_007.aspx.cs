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

public partial class grafico_007 : System.Web.UI.Page
{
    dados cDados;

    int codigoProjeto = 0;

    //pega a data e hora atual do sistema
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";

    //Mensagens de erro para os gráficos
    public string msgErro = "?LoadDataErrorText=Sem Informações a Apresentar";
    public string msgNoData = "&ChartNoDataText=Sem Informações a Apresentar";
    public string msgInvalid = "&InvalidXMLText=Sem Informações a Apresentar";
    public string desenhando = "&PBarLoadingText=Gerando imagem...";
    public string msgLoading = "&XMLLoadingText=Carregando...";

    public string tituloQuestoes = "";
    public string grafico1_swf = "../../../Flashs/StackedColumn2D.swf";
    public string grafico1_xml = "";
    public string grafico1_xmlzoom = "";

    public int alturaGrafico = 220;
    public int larguraGrafico = 350;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        DataSet ds = cDados.getParametrosSistema("labelQuestoes");

        if ((cDados.DataSetOk(ds)) && (cDados.DataTableOk(ds.Tables[0])))
        {
            tituloQuestoes = ds.Tables[0].Rows[0]["labelQuestoes"].ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }        
        
        defineTamanhoObjetos();

        //Função que gera o gráfico
        carregaGrafico();

        cDados.defineAlturaFrames(this, alturaGrafico + 31);
        cDados.aplicaEstiloVisual(this);
    }

    //Carrega o gráfico speedometro
    private void carregaGrafico()
    {
        string xml = "";

       

        string nomeGrafico = @"/ArquivosTemporarios/RiscosQuestoes_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        DataSet dsGrafico = cDados.getDesempenhoRiscosQuestoesProjeto(codigoProjeto, "");

        DataTable dtGrafico = dsGrafico.Tables[0];

        bool linkQuestoes = false, linkRiscos = false;

        linkQuestoes = cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsRQ2");
        linkRiscos = cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsRQ1");
        

        //gera o xml do gráfico Gauge do percentual concluido
        xml = cDados.getGraficoDesempenhoRiscosQuestoes(dtGrafico, "",  9, linkQuestoes, linkRiscos, codigoProjeto);

        //escreve o arquivo XML do gráfico Gauge do percentual concluido
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico1_xml = "../../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        nomeGrafico = "/ArquivosTemporarios/RiscosQuestoesZoom_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //gera o xml do gráfico Gauge do percentual concluido ZOOM
        xml = cDados.getGraficoDesempenhoRiscosQuestoes(dtGrafico, "Desempenho dos Riscos e " + tituloQuestoes, 15, false, false, codigoProjeto);

        //escreve o arquivo XML do gráfico Gauge do percentual concluido ZOOM
        cDados.escreveXML(xml, nomeGrafico);

        grafico1_xmlzoom = nomeGrafico;

        ASPxRoundPanel1.ClientInstanceName = "pc";
        ASPxRoundPanel1.ClientSideEvents.Init = "OnInit";
        ASPxRoundPanel1.JSProperties["cp_grafico1_swf"] = grafico1_swf;
        ASPxRoundPanel1.JSProperties["cp_grafico1_xml"] = grafico1_xml;
    }

    private void defineTamanhoObjetos()
    {

        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        int larguraPaineis = ((largura) / 2 - 105);
        int alturaPaineis = altura - 238;
        larguraGrafico = larguraPaineis - 10;

        alturaGrafico = (altura - 309) / 2;
    }
}
