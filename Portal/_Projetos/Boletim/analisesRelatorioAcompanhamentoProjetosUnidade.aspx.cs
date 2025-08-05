using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Text.RegularExpressions;
using DevExpress.Web.Data;
using System.Data;
using DevExpress.Web.ASPxHtmlEditor;

public partial class _Projetos_Boletim_analisesRelatorioAcompanhamentoProjetosUnidade : Page
{
	private int codigoUsuarioLogado;
	private int codigoEntidade;
	protected int codigoStatusReport;
	private string iniciais;
	private dados cDados;

	protected void Page_Load(object sender, EventArgs e)
	{
		cDados = CdadosUtil.GetCdados(null);
		codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
		codigoUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
		Session["ce"] = codigoEntidade;
		Session["cul"] = codigoUsuarioLogado;

		dataSource.ConnectionString = cDados.classeDados.getStringConexao();

		if (!int.TryParse(Request.QueryString["codStatusReport"], out codigoStatusReport))
			codigoStatusReport = -1;
		iniciais = Request.QueryString["iniciais"];
		if (string.IsNullOrWhiteSpace(iniciais))
			iniciais = "BLTQ";

		btnAvancar.JSProperties["cp_CodigoStatusReport"] = codigoStatusReport;
		btnAvancar.JSProperties["cp_podeEditar"] = Request.QueryString["podeEditar"];
		btnAvancar.JSProperties["cp_iniciais"] = iniciais;

		ITemplate template = gvDadosBoletim.Templates.EditForm;
		cDados.aplicaEstiloVisual(this);
		gvDadosBoletim.Templates.EditForm = template;

		if (!IsPostBack && !IsCallback)
			CDIS_GridLocalizer.Activate();
		defineDimensoesTela();
        txtBoletim.Text = ObtemDescricaoBoletim();
	}

    private string ObtemDescricaoBoletim()
	{
		string comandoSql = string.Format(@"
DECLARE @CodigoStatusReport Int
	SET @CodigoStatusReport = {2}
	
 SELECT {0}.{1}.f_GetDescricaoStatusReport(@CodigoStatusReport) NomeRelatorio"
			, cDados.getDbName()
			, cDados.getDbOwner()
			, codigoStatusReport);

		DataSet ds = cDados.getDataSet(comandoSql);

		return ds.Tables[0].Rows[0]["NomeRelatorio"] as string;
	}
    
	private void defineDimensoesTela()
	{
		string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
		int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
		int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
		gvDadosBoletim.Settings.VerticalScrollableHeight = altura - 360;
	}
    
   

    protected void htmlExecucaoFinanceiraProjeto_Init(object sender, EventArgs e)
    {
        ASPxHtmlEditor htmleditor = sender as ASPxHtmlEditor;
        GridViewDataItemTemplateContainer container = htmleditor.NamingContainer as GridViewDataItemTemplateContainer;
        if (container.VisibleIndex >= 0)
        {
            htmleditor.ClientInstanceName = "dx_" + container.VisibleIndex;
            htmleditor.Html = DataBinder.Eval(container.DataItem, "PontosAtencao").ToString();
        }
    }

    protected void htmlExecucaoFisicaProjeto_Init(object sender, EventArgs e)
    {
        ASPxHtmlEditor htmleditor = sender as ASPxHtmlEditor;
        GridViewDataItemTemplateContainer container = htmleditor.NamingContainer as GridViewDataItemTemplateContainer;
        if (container.VisibleIndex >= 0)
        {
            htmleditor.ClientInstanceName = "dx_" + container.VisibleIndex;
            htmleditor.Html = DataBinder.Eval(container.DataItem, "PrincipaisResultados").ToString();
        }
    }

    protected void dataSource_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {
        e.Command.Parameters["@PontosAtencao"].Value = ((DevExpress.Web.ASPxHtmlEditor.ASPxHtmlEditor)gvDadosBoletim.FindEditRowCellTemplateControl(gvDadosBoletim.Columns[4] as DevExpress.Web.GridViewDataColumn, "htmlExecucaoFinanceiraProjeto")).Html;
        e.Command.Parameters["@PrincipaisResultados"].Value = ((DevExpress.Web.ASPxHtmlEditor.ASPxHtmlEditor)gvDadosBoletim.FindEditRowCellTemplateControl(gvDadosBoletim.Columns[5] as DevExpress.Web.GridViewDataColumn, "htmlExecucaoFisicaProjeto")).Html;
    }


    protected void gvDadosBoletim_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {

    }

    protected void gvDadosBoletim_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "PrincipaisResultados" || e.DataColumn.FieldName == "PontosAtencao")
        {
            string conteudo = e.CellValue.ToString().Replace("'", "''");

            string textoSemTagsHtml = Regex.Replace(conteudo, @"<.*?>", string.Empty);
            e.Cell.Text = textoSemTagsHtml;
        }
    }
}