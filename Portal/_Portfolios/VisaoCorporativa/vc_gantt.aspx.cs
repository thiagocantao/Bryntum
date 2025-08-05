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

public partial class _Portfolios_VisaoCorporativa_vc_gantt : System.Web.UI.Page
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

        //Função que gera o gráfico
        geraGrafico();
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaGrafico = (altura - 155).ToString();
        larguraGrafico = (largura - 15).ToString();
    }

    //Função para geração do gráfico (Quantidade de Projetos por Desempenho) - PIZZA
    private void geraGrafico()
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico de PIZZA
        string nomeGrafico = @"/ArquivosTemporarios/GanttProjeto_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        int codigoPortfolio = int.Parse(cDados.getInfoSistema("CodigoPortfolio").ToString());

        string where = "";

        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        DataSet ds = cDados.getDadosGanttProjetos(codigoUsuario, codigoEntidade, codigoPortfolio, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()), where);
                
        //DataSet dsDatasGantt = cDados.getDatasGanttProjetos(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoPortfolio").ToString()), "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            //função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
            xml = cDados.getGraficoGanttProjetos(ds.Tables[0], "Categoria");
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
