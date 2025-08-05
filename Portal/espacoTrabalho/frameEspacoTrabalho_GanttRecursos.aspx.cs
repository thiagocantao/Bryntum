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
using System.Web.Hosting;
using DevExpress.Web;

public partial class frameEspacoTrabalho_GanttRecursos : System.Web.UI.Page
{
    dados cDados;

    //pega a data e hora atual do sistema
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";

    public string grafico_xml = "";

    public string alturaGrafico = "";
    public string larguraGrafico = "", nenhumGrafico = "";
    private int codigoEntidade;
    int idUsuarioLogado;

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        // =========================== Verifica se a sessão existe INICIO ========================
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
        // =========================== Verifica se a sessão existe FIM ========================

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/AnyChart.js""></script>"));
        this.TH(this.TS("AnyChart"));

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(this);

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        defineLarguraTela();        

        if (!IsPostBack)
        {
            //Função que gera o gráfico
            geraGrafico();
        }

        nenhumGrafico = cDados.getGanttVazio(alturaGrafico);
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));

        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaGrafico = (altura - 200).ToString();
        larguraGrafico = (largura - 15).ToString();
    }

    //Função para geração do gráfico (Quantidade de Projetos por Desempenho) - PIZZA
    private void geraGrafico()
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico de PIZZA
        string nomeGrafico = @"/ArquivosTemporarios/GanttRecursos_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + dataHora;

        string where = getWhere();

        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        DataSet ds = cDados.getTarefasRecursoGantt(idUsuarioLogado, -1, codigoEntidade, where);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            //função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
            xml = cDados.getGraficoGanttTarefasRecurso(ds.Tables[0]);
        }
        else
        {            
            alturaGrafico = "0";
        }

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = ".." + nomeGrafico;
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        geraGrafico();
        callback.JSProperties["cp_XML"] = grafico_xml;
        callback.JSProperties["cp_Altura"] = alturaGrafico;
    }

    private string getWhere()
    {
        string where = "";

        //if (ddlTermino.Text != "")
        //{
        //    where += string.Format("AND TerminoPrevisto <= CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlTermino.Date);
        //}

        //if (ddlInicio.Text != "")
        //{
        //    where += string.Format("AND InicioPrevisto >= CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlInicio.Date);
        //}

        return where;
    }
}
