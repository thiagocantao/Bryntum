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

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/basicBryNTum/Gantt.js""></script>"));
        this.TH(this.TS("geral", "Gantt"));
        //Função que gera o gráfico
        geraGrafico();       
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

            int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

            alturaGrafico = (altura - 225).ToString();
            larguraGrafico = (largura - 15).ToString();
        }
    }

    //Função para geração do gráfico (Quantidade de Projetos por Desempenho) - PIZZA
    private void geraGrafico()
    {        
        int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        string where = "";

        where = " AND prj.CodigoStatusProjeto = 3";

        if (cDados.getInfoSistema("CodigosProjetosUsuario") != null)
            where += " AND p.CodigoProjeto IN (" + cDados.getInfoSistema("CodigosProjetosUsuario").ToString() + ")";
        else
            where += string.Format("AND p.CodigoProjeto in (SELECT codigoProjeto FROM {0}.{1}.f_getProjetosUsuario({2}, {3}, {4})) ", cDados.getDbName(), cDados.getDbOwner(), int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()));

        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        DataSet ds = cDados.getDadosGanttProjetosUnidade(codigoUsuario, codigoEntidade, where);

        cdisGanttHelper = new CDISGanttListaProjetos(codigoUsuario.ToString());
        string taskStore = cdisGanttHelper.geraGraficoJsonTaskData(ds.Tables[0], codigoUsuario);
        //atribui o valor do caminho do JSON a ser carregado
        string caminhoJSON = "../../ArquivosTemporarios/" + cdisGanttHelper.PathToBryntumFile;
        string scripts = @"<script type=""text/javascript"">var urlJSON = """ + caminhoJSON + @""";
                                                                                 var urlJSONDep = '';
                                                                                 var dataInicio = " + cdisGanttHelper.DataInicio + @";
                                                                                 var dataTermino = " + cdisGanttHelper.DataTermino + @";
                                                </script>";
        Literal literal = new Literal();
        literal.Text = scripts;
        Header.Controls.Add(literal);

        if (taskStore == "")
        {
            nenhumGrafico = cDados.getGanttVazio(alturaGrafico);
            alturaGrafico = "0";
        }
    }
}
