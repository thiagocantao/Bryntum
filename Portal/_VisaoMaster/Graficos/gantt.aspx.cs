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
using System.Web.Hosting;
using System.IO;
using System.Xml;
using CDIS;

public partial class _VisaoMaster_Graficos_gantt : System.Web.UI.Page
{
    dados cDados;

    //pega a data e hora atual do sistema
    string dataHora;

    public int codigoProjeto = 0;
    
    public string grafico_xml = "";
    public string alturaGrafico = "", larguraGrafico = "", nenhumGrafico = "";
    public int codigoEntidadeUsuarioResponsavel = -1;
    int codigoUsuario = -1;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
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

        dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";
        
        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }

        defineLarguraTela();

        //Função que gera o gráfico
        geraGrafico();

        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../../Content/styles.css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../../Content/sprite.css"" />"));
    }

    private void defineLarguraTela()
    {
        if (Request.QueryString["Altura"] != null && Request.QueryString["Largura"] != null)
        {
            larguraGrafico = Request.QueryString["Largura"].ToString();
            alturaGrafico = Request.QueryString["Altura"].ToString();
        }
        else
        {
            string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
            int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
            int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

            alturaGrafico = (altura - 230).ToString();
            larguraGrafico = (largura - 175).ToString();
        }
    }



    //Função para geração do gráfico (Quantidade de Projetos por Desempenho) - PIZZA
    private void geraGrafico()
    {        
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico de PIZZA
        string nomeGrafico = @"/ArquivosTemporarios/GanttTarefasPainel_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;
        
        string where = "";
        string sitio = Request.QueryString["NS"] != null && Request.QueryString["NS"].ToString() != "" ? Request.QueryString["NS"].ToString() : "UHE"; 
        int unidadeGeradora = Request.QueryString["NUG"] != null && Request.QueryString["NUG"].ToString() != "" ? int.Parse(Request.QueryString["NUG"].ToString()) : -1;

        string palavraChave = "NULL", apenasConcluidos = "NULL", dataTermino = "NULL";

        if(txtPesquisa.Text.Trim() != "")
            palavraChave = "'" + txtPesquisa.Text + "'";

        if (ckConcluidas.Checked)
            palavraChave = "'S'";

        if (ddlTermino.Text != "")
            dataTermino = string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlTermino.Date);



        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        DataSet ds = cDados.getTarefasGanttPainelGerenciamento(codigoEntidadeUsuarioResponsavel, sitio, unidadeGeradora, palavraChave, apenasConcluidos, dataTermino, where);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            xml = cDados.getGraficoGanttPainelPresidencia(ds.Tables[0]);            
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
