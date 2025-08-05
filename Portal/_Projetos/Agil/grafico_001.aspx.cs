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

public partial class _VisaoMaster_Graficos_vm_010 : System.Web.UI.Page
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

    public string alturaGrafico = "";
    public string larguraGrafico = "";
    
    int codigoEntidade;
    int codigoUsuarioResponsavel;
    private int codigoProjeto;

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
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        defineLarguraTela();

        cDados.aplicaEstiloVisual(Page);
        
        //Função que gera o gráfico
        geraGrafico();       
    }

    private void defineLarguraTela()
    {
        larguraGrafico = "830";
        alturaGrafico = "410";
    }

    //Função para geração do gráfico
    private void geraGrafico()
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico
        string nomeGrafico = @"/ArquivosTemporarios/Burndown_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        string comandoSQL = string.Format(@"
        SELECT * FROM f_Agil_GraficoBurn(null, {0}, 'D', 'N')
        ", codigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        xml = cDados.getGraficoMetodosAgeis(ds.Tables[0]);

        //escreve o arquivo xml
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;  

    }
}
