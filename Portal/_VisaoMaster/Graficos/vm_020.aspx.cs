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
    public string grafico_swf = "../../Flashs/LogMSLine.swf";
    public string grafico_xml = "";

    public string alturaGrafico = "";
    public string larguraGrafico = "";
    
    int codigoEntidade;
    int codigoUsuarioResponsavel;
    private int codigoIndicador;
    private int codigoProjeto;
    private int codigoMeta;
    private int casasDecimais = 0;
    private string unidadeMedida = "";
    bool possuiValorNegativo = false;

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

        codigoIndicador = int.Parse(Request.QueryString["CI"].ToString());
        codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
        codigoMeta = int.Parse(Request.QueryString["CM"].ToString()); 

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        defineLarguraTela();

        carregaDadosIndicador();

        //Função que gera o gráfico
        geraGrafico();
        if (possuiValorNegativo)
        {
            Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/FusionCharts.js?v=1""></script>"));
            grafico_swf = "../../Flashs/MSLine.swf";
        }
        else
        {
            Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/PowerChartsJS/FusionCharts.js""></script>"));
            grafico_swf = "../../Flashs/LogMSLine.swf";
        }
        this.TH(this.TS("FusionCharts"));
    }

    private void carregaDadosIndicador()
    {
        DataSet dsIndicador = cDados.getIndicadoresOperacional(codigoEntidade, "AND ind.CodigoIndicador = " + codigoIndicador);

        if (cDados.DataSetOk(dsIndicador) && cDados.DataTableOk(dsIndicador.Tables[0]))
        {
            txtIndicador.Text = dsIndicador.Tables[0].Rows[0]["NomeIndicador"].ToString();
            casasDecimais = dsIndicador.Tables[0].Rows[0]["CasasDecimais"].ToString() == "" ? 0 : int.Parse(dsIndicador.Tables[0].Rows[0]["CasasDecimais"].ToString());
            unidadeMedida = dsIndicador.Tables[0].Rows[0]["SiglaUnidadeMedida"].ToString();
            txtSitio.Text = cDados.getNomeProjeto(codigoProjeto, "");
        }
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
        string nomeGrafico = @"/ArquivosTemporarios/ResultadoIndicador_002_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        DataTable dt = cDados.getResultadosMeta(codigoMeta, "").Tables[0];

        DataRow[] drs = dt.Select("", "Ano, Mes");

        for (int i = 0; i < drs.Length; i++)
        {
            string valorAtual = drs[i]["ResultadoMes"].ToString();

            if (valorAtual != "" && double.Parse(valorAtual) < 0)
            {
                possuiValorNegativo = true;
                break;
            }


        }

        if (possuiValorNegativo)
            xml = cDados.getGraficoPeriodicidadeHistoricoAnalises(dt, "", 9, casasDecimais, 9, 2, 12, unidadeMedida);
        else
            xml = cDados.getGraficoPeriodicidadeHistoricoAnalisesLog(dt, "", 9, casasDecimais, 9, 2, 12, unidadeMedida);

        //escreve o arquivo xml
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;  

    }
}
