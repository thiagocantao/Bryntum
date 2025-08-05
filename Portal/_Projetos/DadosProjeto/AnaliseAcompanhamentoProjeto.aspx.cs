using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Text.RegularExpressions;
using System.Data;
using DevExpress.Web.ASPxHtmlEditor;

public partial class _Projetos_DadosProjeto_AnaliseAcompanhamentoProjeto : System.Web.UI.Page
{
	dados cDados;

	private int codigoUsuarioResponsavel;
	private int codigoEntidadeUsuarioResponsavel;
	private int codigoProjeto;


	public bool podeEditar = false;
	public bool podeIncluir = false;
	public bool podeExcluir = false;
	private bool exibeBotaoEdicao = true;

    public Unit alturaMemoEdit;

	protected void Page_Init(object sender, EventArgs e)
	{
		DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
		cDados = CdadosUtil.GetCdados(null);

		if (!IsPostBack && !IsCallback)
		{
			try
			{
				hfDadosSessao.Set("CodigoEntidade", cDados.getInfoSistema("CodigoEntidade").ToString());
				hfDadosSessao.Set("Resolucao", cDados.getInfoSistema("ResolucaoCliente").ToString());
				hfDadosSessao.Set("IDUsuarioLogado", cDados.getInfoSistema("IDUsuarioLogado").ToString());
				hfDadosSessao.Set("IDEstiloVisual", cDados.getInfoSistema("IDEstiloVisual").ToString());
				hfDadosSessao.Set("NomeUsuarioLogado", cDados.getInfoSistema("NomeUsuarioLogado").ToString());
				hfDadosSessao.Set("CodigoCarteira", cDados.getInfoSistema("CodigoCarteira").ToString());
			}
			catch
			{
				Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
				Response.End();
			}
		}

		cDados.setInfoSistema("CodigoEntidade", hfDadosSessao.Get("CodigoEntidade").ToString());
		cDados.setInfoSistema("ResolucaoCliente", hfDadosSessao.Get("Resolucao").ToString());
		cDados.setInfoSistema("IDUsuarioLogado", hfDadosSessao.Get("IDUsuarioLogado").ToString());
		cDados.setInfoSistema("IDEstiloVisual", hfDadosSessao.Get("IDEstiloVisual").ToString());
		cDados.setInfoSistema("NomeUsuarioLogado", hfDadosSessao.Get("NomeUsuarioLogado").ToString());
		cDados.setInfoSistema("CodigoCarteira", hfDadosSessao.Get("CodigoCarteira").ToString());

		codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
		codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
		codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());

		Session["cul"] = codigoUsuarioResponsavel;

		this.Title = cDados.getNomeSistema();

        exibeBotaoEdicao = VerificaPeriodoEdicao();

		ITemplate template = gvDados.Templates.EditForm;
        cDados.aplicaEstiloVisual(this);
        gvDados.Templates.EditForm = template;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
		// a pagina não pode ser armazenada no cache.
		Response.Cache.SetCacheability(HttpCacheability.NoCache);
		DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);

		if (cDados.getInfoSistema("ResolucaoCliente") == null)
			Response.Redirect("~/index.aspx");

		if (cDados.verificaAcessoStatusProjeto(codigoProjeto))
		{
            podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_IncAnl");
			podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_AltAnl");
            podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_ExcAnl");

            cDados.verificaPermissaoProjetoInativo(codigoProjeto, ref podeIncluir, ref podeEditar, ref podeExcluir);
		}

		if (!IsPostBack)
		{
			cDados.VerificaAcessoTelaSemMaster(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_CnsAnl");
		}

		gvDados.JSProperties["cp_Salvar"] = "N";
		gvDados.JSProperties["cp_Msg"] = "";
        defineAlturaTela();
        dataSource.ConnectionString = cDados.classeDados.getStringConexao();
    }

    private void defineAlturaTela() //Ok
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));

        //alturaPrincipal = 

        int altura = (alturaPrincipal - 190);

        if (altura > 0)
        {
            gvDados.Settings.VerticalScrollableHeight = altura - 130;
        }

        if (alturaPrincipal <= 768)
        {
            alturaMemoEdit = new Unit("300px");
        }
        else if ((alturaPrincipal >= 769) && (alturaPrincipal <= 800))
        {
            alturaMemoEdit = new Unit("350px");
        }
        else if ((alturaPrincipal >= 801) && (alturaPrincipal <= 960))
        {
            alturaMemoEdit = new Unit("400px");
        }
        else if (alturaPrincipal >= 961)
        {
            alturaMemoEdit = new Unit("500px");
        }
}

private bool VerificaPeriodoEdicao()
	{
		bool permiteEdicao = true;
		DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel
			, "diaInicioEdicaoAnaliseCritica", "diaTerminoEdicaoAnaliseCritica");
		DataRow row = dsParametros.Tables[0].AsEnumerable().First();
		int diaInicio = 0;
		int diaTermino = 0;
		int diaHoje = DateTime.Today.Day;
		try
		{
			diaInicio = int.Parse(row.Field<string>("diaInicioEdicaoAnaliseCritica"));
			diaTermino = int.Parse(row.Field<string>("diaTerminoEdicaoAnaliseCritica"));
			permiteEdicao = (diaInicio <= diaHoje && diaHoje <= diaTermino) ||
				(diaInicio > diaTermino && (diaInicio < diaHoje || diaHoje < diaTermino));
		}
		catch (Exception) { }

		return permiteEdicao;
	}
	
	protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
	{
		if (gvDados.VisibleRowCount == 0) return;
		switch (e.ButtonID)
		{
			case "btnEditar":
				bool permiteEditarRegistro = "S".Equals(
					gvDados.GetRowValues(e.VisibleIndex, "IndicaRegistroEditavel"));
				e.Enabled = podeEditar && permiteEditarRegistro;
				e.Visible = exibeBotaoEdicao ?
					DevExpress.Utils.DefaultBoolean.Default :
					DevExpress.Utils.DefaultBoolean.False;
				if (!e.Enabled)
					e.Image.Url = "~/imagens/botoes/editarRegDes.png";
				break;
			case "btnExcluir":
				bool permiteExcluirRegistro = "N".Equals(
					gvDados.GetRowValues(e.VisibleIndex, "ExisteVinculoStatusReport"));
				e.Enabled = podeExcluir && permiteExcluirRegistro;
				if (!e.Enabled)
					e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
				break;
		}
	}

	protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
	{
		GridViewDataColumn coluna = e.DataColumn;
		if (coluna.FieldName == "PrincipaisResultados" || coluna.FieldName == "PontosAtencao")
		{
            string conteudo = e.CellValue.ToString().Replace("'", "''");

            string textoSemTagsHtml = Regex.Replace(conteudo, @"<.*?>", string.Empty);
            e.Cell.Text = textoSemTagsHtml;

			if (e.CellValue != null && textoSemTagsHtml.Length > 200)
			{
				e.Cell.ToolTip = textoSemTagsHtml;
                textoSemTagsHtml = textoSemTagsHtml.Substring(0, 200) + "...";
			}
			e.Cell.Text = textoSemTagsHtml;
		}        
    }

	#region Eventos Menu Botões Inserção e Exportação

	protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
	{
		string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

		cDados = CdadosUtil.GetCdados(null);
		cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "AnalisePrj");
	}

	protected void menu_Init(object sender, EventArgs e)
	{
		cDados = CdadosUtil.GetCdados(null);
		cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir && exibeBotaoEdicao, "AddNewRow();", true, true, false, "AnalisePrj", "Análises do Projeto", this);
	}

	#endregion
    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Group)
        {
            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }
    }

    protected void dataSource_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {
        e.Command.Parameters["@PontosAtencao"].Value = ((DevExpress.Web.ASPxHtmlEditor.ASPxHtmlEditor)gvDados.FindEditFormTemplateControl("htmlExecucaoFinanceiraProjeto")).Html;
        e.Command.Parameters["@PrincipaisResultados"].Value = ((DevExpress.Web.ASPxHtmlEditor.ASPxHtmlEditor)gvDados.FindEditFormTemplateControl("htmlExecucaoFisicaProjeto")).Html;
    }
}