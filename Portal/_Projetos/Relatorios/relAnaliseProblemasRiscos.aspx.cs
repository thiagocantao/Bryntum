using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DevExpress.XtraReports.UI;
using DevExpress.Web;
using System.Data;
using DevExpress.XtraPrinting;

public partial class _Projetos_Relatorios_relAnaliseProblemasRiscos : System.Web.UI.Page
{
    dados cDados;
    int idEntidadeLogada;
    int idUsuarioLogado;
    public int CodigoFoco;


    public int CodigoDirecionador;
    public int CodigoGrandeDesafio;

    protected bool origemMenuProjeto;



    protected void Page_Init(object sender, EventArgs e)
    {
        //DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        verificaRedirecionamentoUnigest();
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

        idEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        /*
         - Disponibilizar uma nova opção que será apresentada no mesmo menu de relatórios dinâmicos com a denominação "Riscos e Problemas", 
         * devendo ser gerado um arquivo PDF. Este relatório deve ser exclusivamente disponibilizado para as entidades 1,82,83,84 do Sistema Indústria.
- Disponibilizar uma tela onde o usuário poderá escolher os filtros a serem aplicados na geração do PDF:
. Check Box para indicar se traz riscos, problemas ou ambos
. Tipo do Risco/problema com opção para todos
. Foco
. Direcionador
. Grande Desafio
. Unidade de Negócio
. Projeto/Programa- se selecionar Programa sai todos os projetos do programa
. Permitir selecionar se somente problemas/riscos ativos, ou se todos os problemas/riscos
. Apresentar planos de ação (sim/não)
. Status dos planos de ação (em execução, concluído, cancelado)
- A implementação deve considerar o template em anexo.

         */
        if (!IsPostBack)
        {
            populaddlProjetos();
            populaddUnidade();
            populaddlstatusplanoacao();

            populaDDLDirecionador();
            populaDDLFoco();
            populaDDLGrandeDesafio();

            string labelQuestoes = "Problemas";
            DataSet ds = cDados.getParametrosSistema("labelQuestoes");
            if (cDados.DataSetOk(ds))
            {
                labelQuestoes = ds.Tables[0].Rows[0]["labelQuestoes"].ToString();
            }
            rblTipoRelatorio.Items[1].Text = labelQuestoes;
        }
    }

    private void verificaRedirecionamentoUnigest()
    {
        string IndicaTelaRiscoUNIGEST = "N";
        DataSet ds = cDados.getParametrosSistema("IndicaTelaRiscoUNIGEST");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            IndicaTelaRiscoUNIGEST = ds.Tables[0].Rows[0]["IndicaTelaRiscoUNIGEST"].ToString().Trim();
        }
        if (IndicaTelaRiscoUNIGEST == "S")
        {
            Response.Redirect("~/_Projetos/Relatorios/relAnaliseProblemasRiscos_unigest.aspx?" + Request.QueryString.ToString());
        }
    }

    private void populaddlstatusplanoacao()
    {

        string comandosql = string.Format(@"SELECT CodigoStatusTarefa, DescricaoStatusTarefa FROM {0}.{1}.StatusTarefa", cDados.getDbName(), cDados.getDbOwner());
        DataSet ds = cDados.getDataSet(comandosql);
        if (cDados.DataSetOk(ds))
        {
            ddStatusPlanoAcao.TextField = "DescricaoStatusTarefa";
            ddStatusPlanoAcao.ValueField = "CodigoStatusTarefa";
            ddStatusPlanoAcao.DataSource = ds.Tables[0];
            ddStatusPlanoAcao.DataBind();
        }
        ListEditItem semPlanoAcao = new ListEditItem(Resources.traducao.todos, "-1");
        ddStatusPlanoAcao.Items.Insert(0, semPlanoAcao);

        if (!IsPostBack && ddStatusPlanoAcao.Items.Count > 0)
            ddStatusPlanoAcao.SelectedIndex = 0;
    }

    private void populaddUnidade()
    {
        string where = string.Format(@" AND CodigoEntidade = {0}", idEntidadeLogada);

        DataSet ds = cDados.getUnidade(where);
        if (cDados.DataSetOk(ds))
        {
            ddlUnidade.TextField = "NomeUnidadeNegocio";
            ddlUnidade.ValueField = "CodigoUnidadeNegocio";
            ddlUnidade.DataSource = ds.Tables[0];
            ddlUnidade.DataBind();
        }

        ListEditItem semUnidade = new ListEditItem(Resources.traducao.todos, "-1");
        ddlUnidade.Items.Insert(0, semUnidade);

        if (!IsPostBack && ddlUnidade.Items.Count > 0)
            ddlUnidade.SelectedIndex = 0;

    }

    private void populaddlProjetos()
    {
        string where = string.Format(@" AND P.CodigoEntidade = {0}  AND P.CodigoStatusProjeto = 3 ", idEntidadeLogada);

        DataSet ds = cDados.getProjetos(where);
        if (cDados.DataSetOk(ds))
        {
            ddlProjetoPrograma.TextField = "NomeProjeto";
            ddlProjetoPrograma.ValueField = "CodigoProjeto";
            ddlProjetoPrograma.DataSource = ds.Tables[0];
            ddlProjetoPrograma.DataBind();

        }

        ListEditItem semProjeto = new ListEditItem(Resources.traducao.todos, "-1");
        ddlProjetoPrograma.Items.Insert(0, semProjeto);

        if (!IsPostBack && ddlProjetoPrograma.Items.Count > 0)
            ddlProjetoPrograma.SelectedIndex = 0;

    }

    private void populaDDLTipo(char indRiscoQuestao)
    {
        DataSet ds = getTipoRiscoQuestao(indRiscoQuestao);
        ddlTipo.DataSource = ds.Tables[0];
        ddlTipo.ValueField = "CodigoTipoRiscoQuestao";
        ddlTipo.TextField = "DescricaoTipoRiscoQuestao";
        ddlTipo.DataBind();

        ListEditItem semTipo = new ListEditItem(Resources.traducao.todos, "-1");
        ddlTipo.Items.Insert(0, semTipo);

        if (!IsPostBack && ddlTipo.Items.Count > 0)
            ddlTipo.SelectedIndex = 0;
    }

    private void populaDDLFoco()
    {


        string comandoSQL = string.Format(@"
SELECT oes.CodigoobjetoEstrategia AS Codigo, 
        oes.TituloObjetoEstrategia AS Descricao
   FROM ObjetoEstrategia oes INNER JOIN 
        MapaEstrategico me on me.CodigoMapaEstrategico = oes.CodigoMapaEstrategico INNER JOIN 
        UnidadeNegocio un on un.CodigoUnidadeNegocio = me.CodigoUnidadeNegocio
  WHERE me.IndicaMapaEstrategicoAtivo = 'S'
    AND un.CodigoEntidade = un.CodigoUnidadeNegocio
    AND me.CodigoUnidadeNegocio = {0}
    AND oes.CodigoTipoObjetoEstrategia = 11
    AND oes.DataExclusao IS NULL
ORDER BY 2", idEntidadeLogada);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            ddlFoco.TextField = "Descricao";
            ddlFoco.ValueField = "Codigo";
            ddlFoco.DataSource = ds.Tables[0];
            ddlFoco.DataBind();
        }

        ListEditItem todosOsFocos = new ListEditItem(Resources.traducao.todos, "-1");
        ddlFoco.Items.Insert(0, todosOsFocos);

        if (!IsPostBack && ddlFoco.Items.Count > 0)
            ddlFoco.SelectedIndex = 0;
    }

    private void populaDDLDirecionador()
    {
        string comandoSQL = string.Format(@" SELECT oe.CodigoobjetoEstrategia AS Codigo, 
        oes.TituloObjetoEstrategia +  ' - ' + oe.DescricaoObjetoEstrategia AS Descricao,
        oe.CodigoObjetoEstrategiaSuperior AS CodigoSuperior
   FROM objetoestrategia oe INNER JOIN 
        ObjetoEstrategia oes on oes.CodigoObjetoEstrategia = oe.CodigoObjetoEstrategiaSuperior INNER JOIN 
        MapaEstrategico me on me.CodigoMapaEstrategico = oe.CodigoMapaEstrategico INNER JOIN 
        UnidadeNegocio un on un.CodigoUnidadeNegocio = me.CodigoUnidadeNegocio
  WHERE me.IndicaMapaEstrategicoAtivo = 'S'
    AND un.CodigoEntidade = un.CodigoUnidadeNegocio
    AND me.CodigoUnidadeNegocio = {0}
    AND oe.CodigoTipoObjetoEstrategia = 12
    AND oe.DataExclusao IS NULL
ORDER BY 2", idEntidadeLogada);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            ddlDirecionador.TextField = "Descricao";
            ddlDirecionador.ValueField = "Codigo";
            ddlDirecionador.DataSource = ds.Tables[0];
            ddlDirecionador.DataBind();
        }

        ListEditItem todosOsDirecionadores = new ListEditItem(Resources.traducao.todos, "-1");
        ddlDirecionador.Items.Insert(0, todosOsDirecionadores);

        if (!IsPostBack && ddlDirecionador.Items.Count > 0)
            ddlDirecionador.SelectedIndex = 0;
    }

    private void populaDDLGrandeDesafio()
    {
        string comandoSQL = string.Format(@"
        SELECT di.CodigoEntidade,
               di.CodigoGrandeDesafio,
               di.DescricaoGrandeDesafio
          FROM [tai_GrandeDesafio] AS [di]  
         WHERE di.CodigoEntidade = {0}
           and di.IndicaGrandeDesafioAtivo = 'S'
      order by di.DescricaoGrandeDesafio", idEntidadeLogada);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            ddlGrandeDesafio.TextField = "DescricaoGrandeDesafio";
            ddlGrandeDesafio.ValueField = "CodigoGrandeDesafio";
            ddlGrandeDesafio.DataSource = ds.Tables[0];
            ddlGrandeDesafio.DataBind();
        }

        ListEditItem todosOsGrandesDesafios = new ListEditItem(Resources.traducao.todos, "-1");
        ddlGrandeDesafio.Items.Insert(0, todosOsGrandesDesafios);

        if (!IsPostBack && ddlGrandeDesafio.Items.Count > 0)
            ddlGrandeDesafio.SelectedIndex = 0;
    }

    public DataSet getTipoRiscoQuestao(char indicaRiscoQuestao)
    {
        string where = "";
        if (indicaRiscoQuestao != 'A')
        {
            where = string.Format("WHERE IndicaRiscoQuestao = '{0}'", indicaRiscoQuestao);
        }
        string comandoSQL = string.Format(
            @"SELECT CodigoTipoRiscoQuestao
                   , DescricaoTipoRiscoQuestao
                   , IndicaControladoSistema
                   , IndicaRiscoQuestao
               FROM {0}.{1}.[TipoRiscoQuestao]
              {2}
              ORDER by DescricaoTipoRiscoQuestao", cDados.getDbName(), cDados.getDbOwner(), where);

        return cDados.getDataSet(comandoSQL);
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        
    }

    public void ExportReport(XtraReport report, string fileName, string fileType, bool inline)
    {
        MemoryStream stream = new MemoryStream();

        Response.Clear();

        if (fileType == "xls")
        {
            XlsExportOptionsEx x = new XlsExportOptionsEx();
            x.ExportType = DevExpress.Export.ExportType.WYSIWYG;
            report.ExportToXls(stream, x);
        }
            
        if (fileType == "pdf")
            report.ExportToPdf(stream);
        if (fileType == "rtf")
            report.ExportToRtf(stream);
        if (fileType == "csv")
            report.ExportToCsv(stream);

        Response.ContentType = "application/" + fileType;
        Response.AddHeader("Accept-Header", stream.Length.ToString());
        Response.AddHeader("Content-Disposition", string.Format("{0}; filename={1}.{2}",
            (inline ? "Inline" : "Attachment"), fileName, fileType));
        Response.AddHeader("Content-Length", stream.Length.ToString());
        //Response.ContentEncoding = System.Text.Encoding.Default;
        Response.BinaryWrite(stream.ToArray());
        Response.End();

    }
    protected void ddlTipo_Callback(object sender, CallbackEventArgsBase e)
    {
        char tipo = e.Parameter.ToCharArray()[0];
        populaDDLTipo(tipo);
    }
    protected void sqlDataSource_Filtering(object sender, SqlDataSourceFilteringEventArgs e)
    {

    }
    protected void btnImprimir_Click(object sender, EventArgs e)
    {

        string parametro_SIGLA_ENTIDADE = "";
        string parametro_NOME_ENTIDADE = "";
        DataSet dsSiglaNomeEntidade = cDados.getUnidadeNegocio(string.Format(@" and un.CodigoUnidadeNegocio = {0} ", idEntidadeLogada));
        if (cDados.DataSetOk(dsSiglaNomeEntidade))
        {
            parametro_SIGLA_ENTIDADE = dsSiglaNomeEntidade.Tables[0].Rows[0]["SiglaUnidadeNegocio"].ToString();
            parametro_NOME_ENTIDADE = dsSiglaNomeEntidade.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString(); ;
        }



        rel_AnaliseProblemasRiscosProjetos relatorio = new rel_AnaliseProblemasRiscosProjetos(int.Parse(ddlFoco.Value == null ? "-1" : ddlFoco.Value.ToString()),
            int.Parse(ddlDirecionador.Value == null ? "-1" : ddlDirecionador.Value.ToString()),
            int.Parse(ddlGrandeDesafio.Value == null ? "-1" : ddlGrandeDesafio.Value.ToString()), int.Parse(ddlUnidade.Value == null ? "-1":ddlUnidade.Value.ToString()), parametro_SIGLA_ENTIDADE, parametro_NOME_ENTIDADE);


        relatorio.Parameters["pfiltro_rblTipoRelatorio"].Value = rblTipoRelatorio.Value;
        relatorio.Parameters["pfiltro_ddlTipo"].Value = ddlTipo.Value;
        relatorio.Parameters["pfiltro_ddlUnidade"].Value = ddlUnidade.Value;
        relatorio.Parameters["pfiltro_ddlProjetoPrograma"].Value = ddlProjetoPrograma.Value;
        relatorio.Parameters["pfiltro_ddStatusPlanoAcao"].Value = ddStatusPlanoAcao.Value;
        relatorio.Parameters["pfiltro_ckbAtivo"].Value = ckbAtivo.Checked ? "S" : "N";
        relatorio.Parameters["pfiltro_txtFoco"].Value = ddlFoco.Text;
        relatorio.Parameters["pfiltro_txtDirecionador"].Value = ddlDirecionador.Text;
        relatorio.Parameters["pfiltro_txtGrandeDesafio"].Value = ddlGrandeDesafio.Text;
        relatorio.Parameters["pfiltro_ckbPlanosAcao"].Value = ckbPlanosAcao.Checked ? "S" : "N";

        ExportReport(relatorio, "relatorio", "pdf", false);
    }
}