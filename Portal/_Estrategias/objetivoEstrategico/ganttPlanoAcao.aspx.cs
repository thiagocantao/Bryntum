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

public partial class _Estrategias_objetivoEstrategico_ganttPlanoAcao : System.Web.UI.Page
{
    dados cDados;

    //pega a data e hora atual do sistema
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";

    public int codigoObjetivo = 0;
    public int codigoPlanoAcao = 0;

    public string grafico_xml = "";
    public string alturaGrafico = "", nenhumGrafico = "";
    int codigoEntidade = 0;

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

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

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

        if (Request.QueryString["CPA"] != null && Request.QueryString["CPA"].ToString() != "")
        {
            codigoPlanoAcao = int.Parse(Request.QueryString["CPA"].ToString());
        }

        if (Request.QueryString["COE"] != null && Request.QueryString["COE"].ToString() != "")
        {
            codigoObjetivo = int.Parse(Request.QueryString["COE"].ToString());
        }        

        defineLarguraTela();

        //Função que gera o gráfico
        geraGrafico();
        carregaCampos();
    }

    private void carregaCampos()
    {        
        DataSet ds = cDados.getObjetivoEstrategico(null, codigoObjetivo, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {            
            txtObjetivoEstrategico.Text = ds.Tables[0].Rows[0]["DescricaoObjetoEstrategia"].ToString();
        }
        else
        {
            txtObjetivoEstrategico.Text = "";
        }
    }

    private void defineLarguraTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        alturaGrafico = (alturaPrincipal - 230).ToString();
    }

    //Função para geração do gráfico (Quantidade de Projetos por Desempenho) - PIZZA
    private void geraGrafico()
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico de PIZZA
        string nomeGrafico = @"/ArquivosTemporarios/GanttTarefasPlanoAcao_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        string where = "";

        if (codigoPlanoAcao != 0)
        {
            where += " AND CodigoPlanoAcao = " + codigoPlanoAcao;
        }

        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        DataSet ds = cDados.getCronogramaGanttPlanoAcao(codigoObjetivo, codigoEntidade, "OB", where);


        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            xml = cDados.getGraficoGanttPlanosAcao(ds.Tables[0], false);
        }
        else
        {
            nenhumGrafico = cDados.getGanttVazio((int.Parse(alturaGrafico) - 20).ToString());

            alturaGrafico = "0";
        }

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = "../.." + nomeGrafico;
    }

}
