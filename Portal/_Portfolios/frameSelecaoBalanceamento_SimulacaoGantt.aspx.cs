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

public partial class _Portfolios_frameSelecaoBalanceamento_Simulacao : System.Web.UI.Page
{
    dados cDados;
    
    public string alturaGrafico = "";
    public string larguraGrafico = "", nenhumGrafico = "";
    private CDISGanttListaProjetos cdisGanttHelper;

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

        this.TH(this.TS("geral", "Gantt"));

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/basicBryNTum/Gantt.js""></script>"));


        if (!IsPostBack)
            cDados.aplicaEstiloVisual(this);

        imgVisao.Style.Add("cursor", "pointer");
        lblGantt.Style.Add("cursor", "pointer");

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        defineLarguraTela();        

        if (!IsPostBack)
        {
            if (cDados.getInfoSistema("Cenario") != null)
                ddlCenario.Value = cDados.getInfoSistema("Cenario").ToString();

            cDados.setInfoSistema("Cenario", ddlCenario.Value.ToString());

            //Função que gera o gráfico
            geraGrafico();
        }
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));

        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaGrafico = (altura - 225).ToString();
        larguraGrafico = (largura - 15).ToString();
    }

    //Função para geração do gráfico (Quantidade de Projetos por Desempenho) - PIZZA
    private void geraGrafico()
    {
        int codigoPortfolio = -1;

        if (Request.QueryString["CodigoPortfolio"] != null && Request.QueryString["CodigoPortfolio"].ToString() != "")
            codigoPortfolio = int.Parse(Request.QueryString["CodigoPortfolio"].ToString());
        else if (cDados.getInfoSistema("CodigoPortfolio") != null)
            codigoPortfolio = int.Parse(cDados.getInfoSistema("CodigoPortfolio").ToString());

        string comandoSQL = string.Format(
               @"SELECT f.CodigoProjeto AS Codigo, f.NomeProjeto AS Descricao, f.Inicio, f.Termino, f.Cor AS Status, rp.PercentualRealizacao * 100 AS Concluido, '0' AS Sumaria                        
                   FROM {0}.{1}.f_GetGanttProjetos({2}, -1, {3}) f INNER JOIN
                        {0}.{1}.f_GetProjetosSelecaoBalanceamento({3}, -1, {2}) p ON p._CodigoProjeto = f.CodigoProjeto INNER JOIN
                        {0}.{1}.ResumoProjeto rp ON rp.CodigoProjeto = f.CodigoProjeto
                 WHERE 1 = 1 {4} ",
               cDados.getDbName(), cDados.getDbOwner(), cDados.getInfoSistema("CodigoEntidade").ToString(), codigoPortfolio, " AND IndicaCenario" + ddlCenario.Value + " = 'S'");


        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        DataSet dsCrono = cDados.getDataSet(comandoSQL);

        cdisGanttHelper = new CDISGanttListaProjetos(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        string taskStore = cdisGanttHelper.geraGraficoJsonTaskData(dsCrono.Tables[0], int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()));
        //atribui o valor do caminho do JSON a ser carregado
        string caminhoJSON = "../ArquivosTemporarios/" + cdisGanttHelper.PathToBryntumFile;
        string scripts = @"<script type=""text/javascript"">var urlJSON = """ + caminhoJSON + @""";
                                                                                 var urlJSONDep = '';
                                                                                 var dataInicio = " + cdisGanttHelper.DataInicio + @";
                                                                                 var dataTermino = " + cdisGanttHelper.DataTermino + @";
                                                </script>";
        Literal literal = new Literal();
        literal.Text = scripts;
        Header.Controls.Add(literal);

        if(taskStore == "")
        {
            nenhumGrafico = cDados.getGanttVazio(alturaGrafico);
            alturaGrafico = "0";
        }
    }

    protected void btnSelecionar_Click(object sender, EventArgs e)
    {
        cDados.setInfoSistema("Cenario", ddlCenario.Value.ToString());
        geraGrafico();
    }   
}
