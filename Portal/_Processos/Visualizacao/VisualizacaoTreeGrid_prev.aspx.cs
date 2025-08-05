using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using DevExpress.XtraPrinting;

public partial class _Processos_Visualizacao_VisualizacaoTreeGrid_prev : System.Web.UI.Page
{
    #region Constants

    const Int16 CONST_LarguraMinimaColuna = 100;

    #endregion

    #region Fields

    dados cDados;
    DsListaProcessos ds;
	int codigoLista;
	int codigoListaUsuario;
    string dbName;
    string dbOwner;
    int codigoUsuarioLogado;
    int codigoEntidade;
    int codigoCarteira;
    int codigoProjeto = -1;
    /*int? codigoFluxo;
    int? codigoWorkflow;
    int? codigoInstancia;
    int? codigoEtapaInicial;
    int? codigoEtapaAtual;
    int? ocorrenciaAtual;
    int? codigoStatus;
    string etapa;*/
    protected int alturaTela;
    protected int larguraTela;

    #endregion

    #region Properties

    private string _ConnectionString;
    public string ConnectionString
    {
        get
        {
            if (string.IsNullOrEmpty(_ConnectionString))
                _ConnectionString = cDados.classeDados.getStringConexao();
            return _ConnectionString;
        }
        set { _ConnectionString = value; }
    }

  

    #endregion

    #region Event Handlers

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();

		cDados = CdadosUtil.GetCdados(null);

		try
		{
			if (cDados.getInfoSistema("IDUsuarioLogado") == null)
				Response.Redirect("~/erros/erroInatividade.aspx");
		}
		catch
		{
			Response.RedirectLocation = String.Format(
				"{0}erros/erroInatividade.aspx", cDados.getPathSistema());
			Response.End();
		}
		string cl = Request.QueryString["cl"];
		if (string.IsNullOrWhiteSpace(cl)) return;

		codigoLista = int.Parse(cl);
		if (!int.TryParse(Request.QueryString["clu"], out codigoListaUsuario))
			codigoListaUsuario = -1;
		dbName = cDados.getDbName();
		dbOwner = cDados.getDbOwner();
		codigoUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
		codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
		codigoCarteira = Convert.ToInt32(cDados.getInfoSistema("CodigoCarteira"));
		ds = new DsListaProcessos();

        this.TH(this.TS("VisualizacaoTreeGrid_prev"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        InitData();

        if (!(IsPostBack || IsPostBack))
        {
            DefineDimensoesTela();
            DefineDimensoesTreeGrid();
            cDados.aplicaEstiloVisual(this);
            DefinirConfiguracaoToolbar();
        }

        tlDados.DataBind();

        cDados.configuraPainelBotoesTREELIST(tbBotoesEdicao);
    }

    private void DefinirConfiguracaoToolbar()
    {
        var lista = ds.Lista.Single(l => l.CodigoLista == codigoLista);
        var iMenu = this.menu.Items.FindByName("preFiltro");

        if (lista.IndicaPreFiltroCustomizavel.Equals("N") && String.IsNullOrEmpty(lista.InstrucaoPreFiltroDados))
            iMenu.Visible = false;
        else
        {
            iMenu.Visible = true;

            if (lista.IndicaPreFiltroCustomizavel.Equals("S"))
            {
                iMenu.Enabled = true;
                iMenu.ToolTip = "Filtro master";
                iMenu.Image.Url = lista.InstrucaoPreFiltroDados.Equals("") ? "~/imagens/filtroDisponivel.png" : "~/imagens/FiltroAplicado.png";
            }
            else
            {
                iMenu.Enabled = false;
                iMenu.ToolTip = "Personalização do filtro desabilitada";
                iMenu.Image.Url = "~/imagens/filtroDesabilitado.png";
            }
        }

        
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
		string[] parameters = (e.Parameter ?? string.Empty).ToLower().Split(';');
		string acao = parameters[0];
		string valor = parameters.Length > 1 ? parameters[1] : string.Empty;
		switch (acao)
		{
			case "salvar":
				e.Result = SalvarConsulta().ToString();
				break;
			case "salvar_como":
				e.Result = SalvarConsultaComo(valor);
				break;
			default:
				break;
		}
    }

    protected void exporter_RenderBrick(object sender, DevExpress.Web.ASPxTreeList.ASPxTreeListExportRenderBrickEventArgs e)
    {
        if (e.RowKind == TreeListRowKind.Data && e.Column.Name.StartsWith("fieldBLT_") && (e.TextValue != null))
        {
            e.BrickStyle.Font =
                new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
            e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;
            string value = e.TextValue.ToString().ToLower();
            e.Text = "l";
            e.TextValue = "l";
            switch (value)
            {
                case "vermelho":
                    e.BrickStyle.ForeColor = Color.Red;
                    break;
                case "amarelo":
                    e.BrickStyle.ForeColor = Color.Yellow;
                    break;
                case "verde":
                    e.BrickStyle.ForeColor = Color.Green;
                    break;
                case "azul":
                    e.BrickStyle.ForeColor = Color.Blue;
                    break;
                case "branco":
                    e.BrickStyle.ForeColor = Color.White;
                    break;
                case "laranja":
                    e.BrickStyle.ForeColor = Color.Orange;
                    break;
                default:
                    e.Text = " ";
                    e.TextValue = " ";
                    break;
            }
        }
        else
            e.BrickStyle.Font = new Font("Verdana", 8, FontStyle.Regular, GraphicsUnit.Point);
    }

    protected void tlDados_HtmlDataCellPrepared(object sender, TreeListHtmlDataCellEventArgs e)
    {
        if (e.Column is TreeListTextColumn && e.CellValue is string && !e.Column.Name.StartsWith("fieldBLT_"))
            e.Cell.Text = RemoveFormatacaoHtml(e.CellValue as string);
    }

	protected void dataSource_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
	{
		e.Command.CommandTimeout = 0;
        
            DsListaProcessos.ListaRow item = ds.Lista.Single(l => l.CodigoLista == codigoLista);
            ((SqlDataSourceView)sender).FilterExpression = item.InstrucaoPreFiltroDados;
        

    }

    #endregion

    #region Methods
   
    private void DefineDimensoesTela()
    {
        string res = cDados.getInfoSistema("ResolucaoCliente").ToString();
        larguraTela = int.Parse(res.Split('x')[0]);
        alturaTela = int.Parse(res.Split('x')[1]);
    }

    private void DefineDimensoesTreeGrid()
    {
        //a linha de comando abaixo já está sendo resolvida no metodo
        //oninit do tlDados no cliente.
        //tlDados.Settings.ScrollableHeight = alturaTela - 250;
        int qtdeColunasVisiveis = tlDados.VisibleColumns.Count;
        int larguraColunas =
            (larguraTela - 330) / (qtdeColunasVisiveis);
        //for (int i = 0; i < qtdeColunasVisiveis; i++)
        //    tlDados.VisibleColumns[i].Width = new Unit(larguraColunas, UnitType.Pixel);
    }

    private string RemoveFormatacaoHtml(string input)
    {
        string strValue = Regex.Replace(input ?? string.Empty, @"<[^>]*>", " ");
        return strValue;
    }

    private void InitData()
    {
        string comandoPreencheLista;
        string comandoPreencheListaCampo;
        string comandoPreencheListaFluxo;

		#region Comandos SQL

		if (codigoListaUsuario == -1)
		{
			#region Carrega lista padrao

			comandoPreencheLista = string.Format(@"
 SELECT l.CodigoLista, 
		NomeLista, 
		GrupoMenu, 
		ItemMenu, 
		GrupoPermissao, 
		ItemPermissao, 
		IniciaisPermissao, 
		TituloLista, 
		ComandoSelect, 
		IndicaPaginacao, 
		ISNULL(lu.QuantidadeItensPaginacao, l.QuantidadeItensPaginacao) AS QuantidadeItensPaginacao, 
		IndicaOpcaoDisponivel, 
		TipoLista, 
		URL, 
		CodigoEntidade,
        CodigoModuloMenu,
        IndicaListaZebrada,
        lu.FiltroAplicado,
		l.IndicaBuscaPalavraChave,
		(SELECT CASE WHEN lu.CodigoListaUsuario IS NULL THEN 'N' ELSE 'S' END ) AS IndicaPossuiListaUsuario,
		(lu.NomeListaUsuario + ' (Padrão)') AS NomeListaUsuario,
        l.CodigoSubLista, 
        pflt.[InstrucaoPreFiltroDados], 
		pflt.[InstrucaoPreFiltroOriginal], 
        pflt.[IndicaPreFiltroCustomizavel]
   FROM Lista AS l LEFT JOIN 
		ListaUsuario lu ON (lu.CodigoLista = l.CodigoLista AND lu.CodigoUsuario = {0} AND lu.IndicaListaPadrao = 'S') 
        OUTER APPLY dbo.f_GetPreFiltroLista({0}, @CodigoLista, {1}) AS pflt
  WHERE (l.CodigoLista = @CodigoLista)", codigoUsuarioLogado, codigoListaUsuario);

			#endregion

			#region Carrega campos padrao

			comandoPreencheListaCampo = string.Format(@"
 SELECT lc.CodigoCampo,
        lc.CodigoLista, 
        lc.NomeCampo, 
        lc.TituloCampo, 
        ISNULL(lcu.OrdemCampo, lc.OrdemCampo) AS OrdemCampo, 
        ISNULL(lcu.OrdemAgrupamentoCampo, lc.OrdemAgrupamentoCampo) AS OrdemAgrupamentoCampo,
        lc.TipoCampo, 
        lc.Formato, 
        lc.IndicaAreaFiltro, 
        lc.TipoFiltro, 
        lc.IndicaAgrupamento, 
        lc.TipoTotalizador, 
        lc.IndicaAreaDado, 
        lc.IndicaAreaColuna, 
        lc.IndicaAreaLinha, 
        ISNULL(lcu.AreaDefault, lc.AreaDefault) AS AreaDefault, 
        ISNULL(lcu.IndicaCampoVisivel, lc.IndicaCampoVisivel) AS IndicaCampoVisivel, 
        lc.IndicaCampoControle,
        lc.IniciaisCampoControlado,
        lc.IndicaLink,
        (CASE WHEN lcu.CodigoCampo IS NOT NULL THEN 'S' ELSE 'N' END) AS IndicaCampoCustumizado,
        lc.AlinhamentoCampo,
        lc.IndicaCampoHierarquia,
        ISNULL(lcu.LarguraColuna, lc.LarguraColuna) AS LarguraColuna,
        lc.TituloColunaAgrupadora,
		lc.IndicaColunaFixa,
        lu.CodigoListaUsuario
   FROM ListaCampo AS lc LEFT JOIN
		ListaUsuario AS lu ON lc.CodigoLista = lu.CodigoLista AND 
                              lu.CodigoUsuario = {0} AND
							  lu.IndicaListaPadrao = 'S' LEFT JOIN
        ListaCampoUsuario lcu ON lcu.CodigoListaUsuario = lu.CodigoListaUsuario AND
                                 lcu.CodigoCampo = lc.CodigoCampo
  WHERE (lc.CodigoLista = @CodigoLista)
  ORDER BY
        ISNULL(lcu.OrdemCampo, lc.OrdemCampo)", codigoUsuarioLogado);

			#endregion
		}
		else
		{
			#region Carrega lista selecionada

			comandoPreencheLista = string.Format(@"
 SELECT l.CodigoLista, 
		NomeLista, 
		GrupoMenu, 
		ItemMenu, 
		GrupoPermissao, 
		ItemPermissao, 
		IniciaisPermissao, 
		TituloLista, 
		ComandoSelect, 
		IndicaPaginacao, 
		ISNULL(lu.QuantidadeItensPaginacao, l.QuantidadeItensPaginacao) AS QuantidadeItensPaginacao, 
		IndicaOpcaoDisponivel, 
		TipoLista, 
		URL, 
		CodigoEntidade,
        CodigoModuloMenu,
        IndicaListaZebrada,
        lu.FiltroAplicado,
		l.IndicaBuscaPalavraChave,
		'S' AS IndicaPossuiListaUsuario,
		lu.NomeListaUsuario,
        l.CodigoSubLista, 
        pflt.[InstrucaoPreFiltroDados], 
		pflt.[InstrucaoPreFiltroOriginal], 
        pflt.[IndicaPreFiltroCustomizavel]
   FROM Lista AS l LEFT JOIN 
		ListaUsuario lu ON (lu.CodigoLista = l.CodigoLista AND lu.CodigoUsuario = {0} AND lu.codigoListaUsuario = {1})
        OUTER APPLY dbo.f_GetPreFiltroLista({0}, @CodigoLista, {1}) AS pflt
  WHERE (l.CodigoLista = @CodigoLista)", codigoUsuarioLogado, codigoListaUsuario);

			#endregion

			#region Carrega campos lista selecionada

			comandoPreencheListaCampo = string.Format(@"
 SELECT lc.CodigoCampo,
        lc.CodigoLista, 
        lc.NomeCampo, 
        lc.TituloCampo, 
        ISNULL(lcu.OrdemCampo, lc.OrdemCampo) AS OrdemCampo, 
        ISNULL(lcu.OrdemAgrupamentoCampo, lc.OrdemAgrupamentoCampo) AS OrdemAgrupamentoCampo,
        lc.TipoCampo, 
        lc.Formato, 
        lc.IndicaAreaFiltro, 
        lc.TipoFiltro, 
        lc.IndicaAgrupamento, 
        lc.TipoTotalizador, 
        lc.IndicaAreaDado, 
        lc.IndicaAreaColuna, 
        lc.IndicaAreaLinha, 
        ISNULL(lcu.AreaDefault, lc.AreaDefault) AS AreaDefault, 
        ISNULL(lcu.IndicaCampoVisivel, lc.IndicaCampoVisivel) AS IndicaCampoVisivel, 
        lc.IndicaCampoControle,
        lc.IniciaisCampoControlado,
        lc.IndicaLink,
        (CASE WHEN lcu.CodigoCampo IS NOT NULL THEN 'S' ELSE 'N' END) AS IndicaCampoCustumizado,
        lc.AlinhamentoCampo,
        lc.IndicaCampoHierarquia,
        ISNULL(lcu.LarguraColuna, lc.LarguraColuna) AS LarguraColuna,
        lc.TituloColunaAgrupadora,
		lc.IndicaColunaFixa
   FROM ListaCampo AS lc LEFT JOIN
		ListaUsuario AS lu ON lc.CodigoLista = lu.CodigoLista AND 
                              lu.CodigoUsuario = {0} AND
							  lu.codigoListaUsuario = {1} LEFT JOIN
        ListaCampoUsuario lcu ON lcu.CodigoListaUsuario = lu.CodigoListaUsuario AND
                                 lcu.CodigoCampo = lc.CodigoCampo
  WHERE (lc.CodigoLista = @CodigoLista)
  ORDER BY
        ISNULL(lcu.OrdemCampo, lc.OrdemCampo)", codigoUsuarioLogado, codigoListaUsuario);

			#endregion
		}

		#region Carrega fluxos

		comandoPreencheListaFluxo = @"
 SELECT CodigoLista, 
		CodigoFluxo, 
		TituloMenu
   FROM ListaFluxo
  WHERE (CodigoLista = @CodigoLista)";

		#endregion

		#endregion

        FillData(ds.Lista, comandoPreencheLista);
        FillData(ds.ListaCampo, comandoPreencheListaCampo);
        FillData(ds.ListaFluxo, comandoPreencheListaFluxo);

        //SetPopupMenuItems();

        DsListaProcessos.ListaRow item = ds.Lista.Single();

        SetTreeListSettings(item);
       
        List<String> lst = new List<String>();
        foreach (var col in tlDados.DataColumns)
        {
            lst.Add(col.FieldName);
        }
        if (!RelatorioDinamico.validaFilterExpression(item.InstrucaoPreFiltroDados, lst))
        {
            item.InstrucaoPreFiltroDados = String.Empty;
        }
    }

    private void SetTreeListSettings(DsListaProcessos.ListaRow lista)
    {
        string keyFieldName = GetKeyFieldName(lista);
        string parentFieldName = GetParentFieldName(lista);
        bool possuiPaginacao = VerificaVerdadeiroOuFalso(lista.IndicaPaginacao);
        bool listaZebrada = VerificaVerdadeiroOuFalso(lista.IndicaListaZebrada);
        tlDados.KeyFieldName = keyFieldName;
        tlDados.ParentFieldName = parentFieldName;
        if (possuiPaginacao)
        {
            tlDados.SettingsPager.Mode = TreeListPagerMode.ShowPager;
            tlDados.SettingsPager.PageSize = lista.QuantidadeItensPaginacao;
        }
        else
            tlDados.SettingsPager.Mode = TreeListPagerMode.ShowAllNodes;
        if (listaZebrada)
        {
            tlDados.Styles.AlternatingNode.BackColor = Color.FromArgb(0xEB, 0xEB, 0xEB);
            tlDados.Styles.AlternatingNode.Enabled = DevExpress.Utils.DefaultBoolean.True;
        }
        if (tlDados.Columns.Count == 0)
            foreach (var campo in lista.GetListaCampoRows())
                AddTreeListColumn(campo);

        bool retorno = int.TryParse(Request.QueryString["IDProjeto"] + "", out codigoProjeto);

        dataSource.SelectCommand = ReplaceParameters(lista.ComandoSelect);
        dataSource.ConnectionString = ConnectionString;
        dataSource.SetSelectParameters();
    }

    private void SetDefaultSessionInfo()
    {
        var data = new Dictionary<string, object>();
        data.Add("pa_CodigoEntidadeContexto", codigoEntidade);
        data.Add("pa_CodigoUsuarioSistema", codigoUsuarioLogado);
        DataExtensions.SetSessionInfo(data);
    }

    private static string GetKeyFieldName(DsListaProcessos.ListaRow lista)
    {
        DsListaProcessos.ListaCampoRow[] campos = lista.GetListaCampoRows();
        DsListaProcessos.ListaCampoRow campoChavePrimaria =
            campos.SingleOrDefault(c => c.IndicaCampoHierarquia.Equals("P"));

        if (campoChavePrimaria == null) return string.Empty;

        return campoChavePrimaria.NomeCampo;
    }

    private static string GetParentFieldName(DsListaProcessos.ListaRow lista)
    {
        DsListaProcessos.ListaCampoRow[] campos = lista.GetListaCampoRows();
        DsListaProcessos.ListaCampoRow campoSuperiorEstruturaHierarquica =
            campos.SingleOrDefault(c => c.IndicaCampoHierarquia.Equals("S"));

        if (campoSuperiorEstruturaHierarquica == null) return string.Empty;

        return campoSuperiorEstruturaHierarquica.NomeCampo;
    }

    private void AddTreeListColumn(DsListaProcessos.ListaCampoRow campo)
    {
        if (campo.TipoCampo.ToUpper().Equals("BLT"))
        {
            #region Bullet

            TreeListTextColumn colImg = new TreeListTextColumn();
            colImg.Caption = campo.TituloCampo;
            colImg.FieldName = campo.NomeCampo;
            colImg.Name = string.Format("fieldBLT_{0}#{1}", campo.NomeCampo, campo.CodigoCampo);
            colImg.PropertiesTextEdit.DisplayFormatString = "<img style='border:0px' src='../../imagens/{0}.gif'/>";
            colImg.Visible = VerificaVerdadeiroOuFalso(campo.IndicaCampoVisivel);
            colImg.VisibleIndex = campo.OrdemCampo;
            if (!campo.IsLarguraColunaNull())
                colImg.Width = new Unit(campo.LarguraColuna, UnitType.Pixel);

            #region Tipo filtro

            colImg.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            colImg.AllowFilterBySearchPanel = DevExpress.Utils.DefaultBoolean.False;

            colImg.ShowFilterRowMenu = (campo.IndicaAreaFiltro.ToUpper() == "S") ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            if (colImg.ShowFilterRowMenu == DevExpress.Utils.DefaultBoolean.True)
            {
                colImg.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;
                colImg.SettingsHeaderFilter.Mode = campo.TipoFiltro.ToUpper() == "C" ? GridHeaderFilterMode.List : GridHeaderFilterMode.Default;
            }
            else
            {
                colImg.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            }

            #endregion

            #region Alinhamento

            HorizontalAlign alinhamento = HorizontalAlign.Center;
            switch (campo.AlinhamentoCampo.ToUpper())
            {
                case "E": alinhamento = HorizontalAlign.Left;
                    break;
                case "D": alinhamento = HorizontalAlign.Right;
                    break;
                case "C": alinhamento = HorizontalAlign.Center;
                    break;
            }
            colImg.CellStyle.HorizontalAlign = alinhamento;
            colImg.HeaderStyle.HorizontalAlign = alinhamento;

            #endregion

            tlDados.Columns.Add(colImg);

            #endregion
        }
        else if (campo.TipoCampo.ToUpper().Equals("DAT"))
        {
            TreeListDateTimeColumn colData = new TreeListDateTimeColumn();
            colData.Caption = campo.TituloCampo;
            colData.FieldName = campo.NomeCampo;
            colData.Name = string.Format("col#{0}",campo.CodigoCampo);
            colData.PropertiesDateEdit.DisplayFormatString = campo.Formato;

            colData.AllowFilterBySearchPanel = DevExpress.Utils.DefaultBoolean.False;

            colData.ShowFilterRowMenu = (campo.IndicaAreaFiltro.ToUpper() == "S") ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            if (colData.ShowFilterRowMenu == DevExpress.Utils.DefaultBoolean.True)
            {
                colData.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;
                colData.SettingsHeaderFilter.Mode = campo.TipoFiltro.ToUpper() == "C" ? GridHeaderFilterMode.List : GridHeaderFilterMode.Default;
            }
            else
            {
                colData.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            }

            tlDados.Columns.Add(colData);
        }
        else if (campo.TipoCampo.ToUpper().Equals("NUM"))
        {
            TreeListSpinEditColumn colNum = new TreeListSpinEditColumn();
            colNum.Caption = campo.TituloCampo;
            colNum.FieldName = campo.NomeCampo;
            colNum.Name = string.Format("col#{0}", campo.CodigoCampo);
            colNum.PropertiesSpinEdit.DisplayFormatString = campo.Formato;

            colNum.AllowFilterBySearchPanel = DevExpress.Utils.DefaultBoolean.False;


            colNum.ShowFilterRowMenu = (campo.IndicaAreaFiltro.ToUpper() == "S") ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            if (colNum.ShowFilterRowMenu == DevExpress.Utils.DefaultBoolean.True)
            {
                colNum.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;
                colNum.SettingsHeaderFilter.Mode = campo.TipoFiltro.ToUpper() == "C" ? GridHeaderFilterMode.List : GridHeaderFilterMode.Default;
            }
            else
            {
                colNum.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            }

            tlDados.Columns.Add(colNum);
        }
        else if (VerificaVerdadeiroOuFalso(campo.IndicaLink))
        {
            #region Link

            TreeListHyperLinkColumn colLnk = new TreeListHyperLinkColumn();
            colLnk.CellStyle.HorizontalAlign = HorizontalAlign.Left;
            colLnk.FieldName = ds.ListaCampo.Single(r => r.IniciaisCampoControlado == "CP").NomeCampo;
            colLnk.Caption = campo.TituloCampo;
            colLnk.Name = string.Format("col#{0}", campo.CodigoCampo);
            colLnk.PropertiesHyperLink.NavigateUrlFormatString =
                "~/_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto={0}";
            colLnk.PropertiesHyperLink.Target = "_top";
            colLnk.PropertiesHyperLink.TextFormatString = campo.Formato;
            colLnk.PropertiesHyperLink.TextField = campo.NomeCampo;
            colLnk.Visible = VerificaVerdadeiroOuFalso(campo.IndicaCampoVisivel);
            colLnk.VisibleIndex = campo.OrdemCampo;
            if (!campo.IsLarguraColunaNull())
                colLnk.Width = new Unit(campo.LarguraColuna, UnitType.Pixel);

            #region Tipo campo

            switch (campo.TipoCampo.ToUpper())
            {
                default:
                    break;
            }

            #endregion

            #region Tipo filtro
            colLnk.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            colLnk.AllowFilterBySearchPanel = DevExpress.Utils.DefaultBoolean.True;


            colLnk.ShowFilterRowMenu = (campo.IndicaAreaFiltro.ToUpper() == "S") ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            if (colLnk.ShowFilterRowMenu == DevExpress.Utils.DefaultBoolean.True)
            {
                colLnk.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;
                colLnk.SettingsHeaderFilter.Mode = campo.TipoFiltro.ToUpper() == "C" ? GridHeaderFilterMode.List : GridHeaderFilterMode.Default;
            }
            else
            {
                colLnk.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            }


            #endregion

            #region Tipo totalizados

            string tipoTotalizador = campo.TipoTotalizador.ToUpper();
            SummaryItemType summaryType = GetSummaryType(tipoTotalizador);
            TreeListSummaryItem summaryItem = new TreeListSummaryItem();
            summaryItem.DisplayFormat = campo.Formato;
            summaryItem.FieldName = campo.NomeCampo;
            summaryItem.ShowInColumn = campo.NomeCampo;
            summaryItem.SummaryType = summaryType;
            tlDados.Summary.Add(summaryItem);

            #endregion

            #region Alinhamento

            HorizontalAlign alinhamento = HorizontalAlign.Center;
            switch (campo.AlinhamentoCampo.ToUpper())
            {
                case "E": alinhamento = HorizontalAlign.Left;
                    break;
                case "D": alinhamento = HorizontalAlign.Right;
                    break;
                case "C": alinhamento = HorizontalAlign.Center;
                    break;
            }
            colLnk.CellStyle.HorizontalAlign = alinhamento;
            colLnk.HeaderStyle.HorizontalAlign = alinhamento;

            #endregion

            tlDados.Columns.Add(colLnk);

            #endregion
        }
        else
        {
            #region Outros

            TreeListTextColumn colTxt = new TreeListTextColumn();
            colTxt.FieldName = campo.NomeCampo;
            if (VerificaVerdadeiroOuFalso(campo.IndicaCampoControle))
            {
                colTxt.Visible = false;
                colTxt.ShowInCustomizationForm = false;
                string iniciais = campo.IniciaisCampoControlado;
                if (!string.IsNullOrEmpty(iniciais))
                    colTxt.Name = string.Format("colCC_{0}", iniciais);
            }
            else
            {
                colTxt.Caption = campo.TituloCampo;
                colTxt.Name = string.Format("col#{0}", campo.CodigoCampo);
                colTxt.Visible = VerificaVerdadeiroOuFalso(campo.IndicaCampoVisivel);
                colTxt.PropertiesTextEdit.DisplayFormatString = campo.Formato;
                colTxt.VisibleIndex = campo.OrdemCampo;
                if (!campo.IsLarguraColunaNull())
                    colTxt.Width = new Unit(campo.LarguraColuna, UnitType.Pixel);

                #region Tipo campo

                switch (campo.TipoCampo.ToUpper())
                {
                    default:
                        break;
                }

                #endregion

                #region Tipo filtro

                colTxt.AllowFilterBySearchPanel = DevExpress.Utils.DefaultBoolean.True;

                colTxt.ShowFilterRowMenu = (campo.IndicaAreaFiltro.ToUpper() == "S") ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                if (colTxt.ShowFilterRowMenu == DevExpress.Utils.DefaultBoolean.True)
                {
                    colTxt.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;
                    colTxt.SettingsHeaderFilter.Mode = campo.TipoFiltro.ToUpper() == "C" ? GridHeaderFilterMode.List : GridHeaderFilterMode.Default;
                }
                else
                {
                    colTxt.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
                }

                #endregion

                #region Tipo totalizados

                string tipoTotalizador = campo.TipoTotalizador.ToUpper();
                SummaryItemType summaryType = GetSummaryType(tipoTotalizador);
                TreeListSummaryItem summaryItem = new TreeListSummaryItem();
                summaryItem.DisplayFormat = campo.Formato;
                summaryItem.FieldName = campo.NomeCampo;
                summaryItem.ShowInColumn = campo.NomeCampo;
                summaryItem.SummaryType = summaryType;
                tlDados.Summary.Add(summaryItem);

                #endregion

                #region Alinhamento

                HorizontalAlign alinhamento = HorizontalAlign.Center;
                switch (campo.AlinhamentoCampo.ToUpper())
                {
                    case "E": alinhamento = HorizontalAlign.Left;
                        break;
                    case "D": alinhamento = HorizontalAlign.Right;
                        break;
                    case "C": alinhamento = HorizontalAlign.Center;
                        break;
                }
                colTxt.CellStyle.HorizontalAlign = alinhamento;
                colTxt.HeaderStyle.HorizontalAlign = alinhamento;

                #endregion
            }

            tlDados.Columns.Add(colTxt);

            #endregion
        }
    }

    private void FillData(DataTable dt, string comandoSql)
    {
        SqlDataAdapter da = new SqlDataAdapter(comandoSql, ConnectionString);
        SqlParameter p = da.SelectCommand.Parameters.Add("@CodigoLista", SqlDbType.Int);
        p.Value = codigoLista;
        da.Fill(dt);
    }

    private bool VerificaVerdadeiroOuFalso(string valor)
    {
        if (valor == null)
            return false;
        return valor.ToLower().Equals("s");
    }

    private string ReplaceParameters(string comandoPreencheLista)
    {
        /*
         * {0} - BD
         * {1} - Owner
         * {2} - Código do Projeto
         * {3} - Código da Entidade
         * {4} - Usuário Logado 
         * {5} – Código Fluxo
         * {6} - Código Workflow
         * {7} - Código Instância
         * {8} - Código Etapa
         * {9} – Ocorrência Atual
         * {10} - Código Carteira
         */
        const string valorNaoDefinido = "-1";
        comandoPreencheLista = comandoPreencheLista
            .Replace("{0}", dbName)
            .Replace("{1}", dbOwner)
            .Replace("{2}", (codigoProjeto != -1  && codigoProjeto != 0)? codigoProjeto.ToString() : valorNaoDefinido)
            .Replace("{3}", codigoEntidade.ToString())
            .Replace("{4}", codigoUsuarioLogado.ToString())
            .Replace("{5}", /*codigoFluxo.HasValue ? codigoFluxo.ToString() :*/ valorNaoDefinido)
            .Replace("{6}", /*codigoWorkflow.HasValue ? codigoWorkflow.ToString() :*/ valorNaoDefinido)
            .Replace("{7}", /*codigoInstancia.HasValue ? codigoInstancia.ToString() :*/ valorNaoDefinido)
            .Replace("{8}", /*codigoEtapaAtual.HasValue ? codigoEtapaAtual.ToString() :*/ valorNaoDefinido)
            .Replace("{9}", /*ocorrenciaAtual.HasValue ? ocorrenciaAtual.ToString() :*/ valorNaoDefinido)
            .Replace("{10}", codigoCarteira.ToString());

        return comandoPreencheLista;
    }

    private static SummaryItemType GetSummaryType(string tipoTotalizador)
    {
        SummaryItemType summaryType;
        switch (tipoTotalizador)
        {
            case "CONTAR":
                summaryType = SummaryItemType.Count;
                break;
            case "MÉDIA":
                summaryType = SummaryItemType.Average;
                break;
            case "SOMA":
                summaryType = SummaryItemType.Sum;
                break;
            default:
                summaryType = SummaryItemType.None;
                break;
        }
        return summaryType;
    }

    private int ObtemCodigoCampo(TreeListEditDataColumn col)
    {
        int codigoCampo = -1;
        string columnName = col.Name;
        int indiceCodigoCampo = columnName.LastIndexOf('#') + 1;
        if (indiceCodigoCampo > 0)
            codigoCampo = int.Parse(columnName.Substring(indiceCodigoCampo));

        return codigoCampo;
    }

	private bool SalvarConsulta()
	{
		int qtdeItensPagina = tlDados.SettingsPager.PageSize;
		StringBuilder comandoSql = new StringBuilder();

		#region Comando SQL

		comandoSql.AppendLine(@"
DECLARE @CodigoLista Int,
        @CodigoUsuario INT,
        @CodigoCampo INT,
        @OrdemCampo SMALLINT,
        @OrdemAgrupamentoCampo SMALLINT,
        @AreaDefault CHAR(1),
        @IndicaCampoVisivel CHAR(1),
        @LarguraColuna SMALLINT,
        @QuantidadeItensPaginacao INT,
        @FiltroAplicado VARCHAR(4000),
		@CodigoListaUsuario BIGINT");

		comandoSql.AppendFormat(@"
    SET @CodigoLista = {0}
    SET @CodigoUsuario = {1}
    SET @QuantidadeItensPaginacao = {2}
	SET @CodigoListaUsuario = {3}
	
IF @CodigoListaUsuario = -1
 SELECT @CodigoListaUsuario = [CodigoListaUsuario] 
   FROM [ListaUsuario] 
  WHERE [CodigoLista] = @CodigoLista 
	AND [CodigoUsuario] = @CodigoUsuario 
	AND [IndicaListaPadrao] = 'S'

 UPDATE [ListaUsuario]
	SET [QuantidadeItensPaginacao] = @QuantidadeItensPaginacao
	   ,[FiltroAplicado] = @FiltroAplicado
  WHERE [CodigoListaUsuario] = @CodigoListaUsuario"
	, codigoLista, codigoUsuarioLogado, qtdeItensPagina, codigoListaUsuario);

		#endregion

		foreach (TreeListEditDataColumn col in tlDados.Columns.OfType<TreeListEditDataColumn>().Where(c => c.ShowInCustomizationForm))
		{
			int ordemCampo = col.VisibleIndex;
			int ordemAgrupamentoCampo = 1;
			string areaDefault = "L";
			string indicaCampoVisivel = col.Visible ? "S" : "N";
			int codigoCampo = ObtemCodigoCampo(col);
			double larguraColuna = col.Width.IsEmpty ? CONST_LarguraMinimaColuna : col.Width.Value;

			#region Comando SQL

			comandoSql.AppendFormat(@"
    SET @CodigoCampo = {0}
    SET @OrdemCampo = {1}
    SET @OrdemAgrupamentoCampo = {2}
    SET @AreaDefault = '{3}'
    SET @IndicaCampoVisivel = '{4}'
    SET @LarguraColuna = {5}

IF EXISTS(SELECT 1 FROM [ListaCampoUsuario] WHERE [CodigoListaUsuario] = @CodigoListaUsuario AND [CodigoCampo] = @CodigoCampo)
 UPDATE [ListaCampoUsuario]
	SET [OrdemCampo] = @OrdemCampo
       ,[OrdemAgrupamentoCampo] = @OrdemAgrupamentoCampo
       ,[AreaDefault] = @AreaDefault
       ,[IndicaCampoVisivel] = @IndicaCampoVisivel
       ,[LarguraColuna] = @LarguraColuna
  WHERE [CodigoListaUsuario] = @CodigoListaUsuario
    AND [CodigoCampo] = @CodigoCampo
ELSE IF EXISTS(SELECT 1 FROM [ListaUsuario] WHERE [CodigoListaUsuario] = @CodigoListaUsuario)
 INSERT INTO [ListaCampoUsuario](
        [CodigoCampo],
        [CodigoListaUsuario],
        [OrdemCampo],
        [OrdemAgrupamentoCampo],
        [AreaDefault],
        [IndicaCampoVisivel],
        [LarguraColuna])
  VALUES(
        @CodigoCampo,
        @CodigoListaUsuario,
        @OrdemCampo,
        @OrdemAgrupamentoCampo,
        @AreaDefault,
        @IndicaCampoVisivel,
        @LarguraColuna)",
	 codigoCampo,
	 ordemCampo,
	 ordemAgrupamentoCampo,
	 areaDefault,
	 indicaCampoVisivel,
	 larguraColuna);
			comandoSql.AppendLine();

			#endregion
		}
		int registrosAfetados = 0;
		cDados.execSQL(comandoSql.ToString(), ref registrosAfetados);

		bool salvoComSucesso = registrosAfetados == 0;
		return salvoComSucesso;
	}

	private string SalvarConsultaComo(string nomeListaUsuario)
	{
		int qtdeItensPagina = tlDados.SettingsPager.PageSize;
		StringBuilder comandoSql = new StringBuilder();
        DsListaProcessos.ListaRow item = ds.Lista.Single();

        #region Comando SQL

        comandoSql.AppendLine(@"
DECLARE @CodigoLista Int,
        @CodigoUsuario INT,
        @CodigoCampo INT,
        @OrdemCampo SMALLINT,
        @OrdemAgrupamentoCampo SMALLINT,
        @AreaDefault CHAR(1),
        @IndicaCampoVisivel CHAR(1),
        @LarguraColuna SMALLINT,
        @QuantidadeItensPaginacao INT,
        @FiltroAplicado VARCHAR(4000),
		@NomeListaUsuario VARCHAR(255),
		@IndicaListaPadrao CHAR(1),
		@CodigoListaUsuario BIGINT,
		@InstrucaoPreFiltroDados varchar(max),
		@InstrucaoPreFiltroDadosOriginal varchar(max)");

        comandoSql.AppendFormat(@"
            SET @CodigoLista = {0}
            SET @CodigoUsuario = {1}
            SET @QuantidadeItensPaginacao = {2}
			SET @NomeListaUsuario = '{3}'
	        SET @InstrucaoPreFiltroDados = '{4}'
	        SET @InstrucaoPreFiltroDadosOriginal = '{5}'
			SET @IndicaListaPadrao = (SELECT CASE WHEN EXISTS(SELECT 1 FROM ListaUsuario WHERE CodigoUsuario = @CodigoUsuario AND CodigoLista = @CodigoLista) THEN 'N' ELSE 'S' END)

		 INSERT INTO ListaUsuario(CodigoUsuario, CodigoLista, QuantidadeItensPaginacao, FiltroAplicado, NomeListaUsuario, IndicaListaPadrao, InstrucaoPreFiltroDados, InstrucaoPreFiltroDadosOriginal) 
		 VALUES (@CodigoUsuario, @CodigoLista, @QuantidadeItensPaginacao, @FiltroAplicado, @NomeListaUsuario, @IndicaListaPadrao, @InstrucaoPreFiltroDados, @InstrucaoPreFiltroDadosOriginal)

		SET @CodigoListaUsuario = SCOPE_IDENTITY()"
    , codigoLista, codigoUsuarioLogado, qtdeItensPagina, nomeListaUsuario
    , item.InstrucaoPreFiltroDados.Replace("'", "''"), item.InstrucaoPreFiltroOriginal.Replace("'", "''"));

        #endregion

        foreach (TreeListEditDataColumn col in tlDados.Columns.OfType<TreeListEditDataColumn>().Where(c => c.ShowInCustomizationForm))
		{
			int ordemCampo = col.VisibleIndex;
			int ordemAgrupamentoCampo = 1;
			string areaDefault = "L";
			string indicaCampoVisivel = col.Visible ? "S" : "N";
			int codigoCampo = ObtemCodigoCampo(col);
			double larguraColuna = col.Width.IsEmpty ? CONST_LarguraMinimaColuna : col.Width.Value;

			#region Comando SQL

			comandoSql.AppendFormat(@"
    SET @CodigoCampo = {0}
    SET @OrdemCampo = {1}
    SET @OrdemAgrupamentoCampo = {2}
    SET @AreaDefault = '{3}'
    SET @IndicaCampoVisivel = '{4}'
    SET @LarguraColuna = {5}

    INSERT INTO ListaCampoUsuario(
           CodigoCampo,
           CodigoListaUsuario,
           OrdemCampo,
           OrdemAgrupamentoCampo,
           AreaDefault,
           IndicaCampoVisivel,
           LarguraColuna)
     VALUES(
           @CodigoCampo,
           @CodigoListaUsuario,
           @OrdemCampo,
           @OrdemAgrupamentoCampo,
           @AreaDefault,
           @IndicaCampoVisivel,
           @LarguraColuna)",
	 codigoCampo,
	 ordemCampo,
	 ordemAgrupamentoCampo,
	 areaDefault,
	 indicaCampoVisivel,
	 larguraColuna);
			comandoSql.AppendLine();

			#endregion
		}
		comandoSql.AppendLine();
		comandoSql.AppendLine("SELECT @CodigoListaUsuario AS CodigoListaUsuario");
		DataSet ds2 = cDados.getDataSet(comandoSql.ToString());
		return ds2.Tables[0].Rows[0]["CodigoListaUsuario"].ToString();
	}

    #endregion

    #region Classes Auxiliares

    public class HeaderTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            TreeListHeaderTemplateContainer c =
                (TreeListHeaderTemplateContainer)container;
            if (c.TreeList.VisibleColumns.Count > 0)
            {
                int firstIndex = c.TreeList.VisibleColumns.Select(col => col.VisibleIndex).Min();
                if (c.Column.VisibleIndex == firstIndex)
                {
                    c.Controls.Add(new ImagemExportacao());
                    c.Controls.Add(new ImagemSalvarLayout());
                    c.Controls.Add(new ImagemRestaurarLayout());
                }
                ASPxLabel lblHeader = new ASPxLabel();
                lblHeader.Text = c.Column.Caption;
                c.Controls.Add(lblHeader);
            }
        }
    }

    public class ImagemExportacao : ASPxImage
    {
        public ImagemExportacao()
            : base()
        {
            ID = "imgExportacao";
            ImageUrl = "~/imagens/menuExportacao/iconoExcel.png";
            Cursor = "pointer";
            Height = new Unit(20, UnitType.Pixel);
            Width = new Unit(20, UnitType.Pixel);
            ClientSideEvents.Click = "function(s, e) {callback.PerformCallback('export');}";
        }
    }

    public class ImagemSalvarLayout : ASPxImage
    {
        public ImagemSalvarLayout()
            : base()
        {
            ID = "imgSalvarLayout";
            ImageUrl = "~/imagens/salvar.png";
            Cursor = "pointer";
            Height = new Unit(20, UnitType.Pixel);
            Width = new Unit(20, UnitType.Pixel);
            ClientSideEvents.Click = "function(s, e) {SalvarConfiguracoesLayout();}";
        }
    }

    public class ImagemRestaurarLayout : ASPxImage
    {
        public ImagemRestaurarLayout()
            : base()
        {
            ID = "imgRestaurarLayout";
            ImageUrl = "~/imagens/restaurar.png";
            Cursor = "pointer";
            Height = new Unit(20, UnitType.Pixel);
            Width = new Unit(20, UnitType.Pixel);
            ClientSideEvents.Click = "function(s, e) {RestaurarConfiguracoesLayout();}";
        }
    }

    #endregion

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        if (e.Item.Image.Url != "~/imagens/botoes/btnDownload.png")
        {
            cDados.exportaTreeList(exporter, parameter);
        }
    }

    protected void menu_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

		ASPxMenu menu = (sender as ASPxMenu);

		DsListaProcessos.ListaRow item = ds.Lista.Single();
		lblNomeConsulta.Text = item.NomeListaUsuario;

        if (codigoListaUsuario == -1 && !String.IsNullOrEmpty(item.CodigoListaUsuario))
        {
            codigoListaUsuario = int.Parse(item.CodigoListaUsuario);
        }

        #region EXPORTAÇÃO

        DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");

        bool exportaOLAPTodosFormatos = false;

        if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
        {
            exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
        }

        DevExpress.Web.MenuItem btnExportar = menu.Items.FindByName("btnExportar");

        btnExportar.ClientVisible = true;

        if (!exportaOLAPTodosFormatos)
        {
            btnExportar.Items.Clear();
            btnExportar.Image.Url = "~/imagens/botoes/btnExcel.png";
            btnExportar.ToolTip = Resources.traducao.VisualizacaoGrid_exportar_para_xlsx;
        }
        else
        {
            btnExportar.ToolTip = Resources.traducao.exportar;
            btnExportar.Items.FindByName("btnXLS").ToolTip = Resources.traducao.exportar_para_xls;
            btnExportar.Items.FindByName("btnPDF").ToolTip = Resources.traducao.exportar_para_pdf;
            btnExportar.Items.FindByName("btnRTF").ToolTip = Resources.traducao.exportar_para_rtf;
            btnExportar.Items.FindByName("btnHTML").ToolTip = Resources.traducao.exportar_para_html;
        }

        #endregion

        #region JS

        menu.ClientSideEvents.ItemClick =
			String.Format(@"function(s, e){{ 
	var indicaPossuiListaUsuario = '{2}';
	e.processOnServer = false;

	if(e.item.name == 'btnIncluir'){{ShowMenu(s, e);}}
	else if(e.item.name == 'btnAbrirConsultas'){{parent.ExibeConsultaSalvas({0}, {1});}}
	else if(e.item.name == 'btnSalvarConsultas'){{if(indicaPossuiListaUsuario == 'S'){{SalvarConsulta();}}else{{parent.ExibirJanelaSalvarComo();}}}}
	else if(e.item.name == 'btnSalvarComoConsultas'){{parent.ExibirJanelaSalvarComo();}}
	else if(e.item.name == 'btnCerregarOriginalConsultas'){{CarregarConsulta(0);}}
    else if(e.item.name == 'preFiltro'){{window.parent.showModalComFooter('FilterEditorPopup.aspx?cl={0}&clu={3}', 'Filtro dos dados', 300, 400, CarregarConsulta, null);}}
	else if(e.item.name != 'btnLayout'){{e.processOnServer = true;}}	
}}", codigoLista, codigoUsuarioLogado, item.IndicaPossuiListaUsuario, codigoListaUsuario);

        #endregion

        DevExpress.Web.MenuItem btnIncluir = menu.Items.FindByName("btnIncluir");
        btnIncluir.ToolTip = Resources.traducao.incluir;
        btnIncluir.ClientEnabled = false;

        btnIncluir.ClientVisible = false;

        #region LAYOUT

        DevExpress.Web.MenuItem btnLayout = menu.Items.FindByName("btnLayout");
        btnLayout.Items.FindByName("btnAbrirConsultas").Text = Resources.traducao.abrir;
        btnLayout.Items.FindByName("btnAbrirConsultas").ToolTip = Resources.traducao.abrir_consultas_salvas;
        btnLayout.Items.FindByName("btnSalvarConsultas").Text = Resources.traducao.salvar;
        btnLayout.Items.FindByName("btnSalvarConsultas").ToolTip = Resources.traducao.salvar_consulta;
        btnLayout.Items.FindByName("btnSalvarComoConsultas").Text = Resources.traducao.salvar_como;
        btnLayout.Items.FindByName("btnSalvarComoConsultas").ToolTip = Resources.traducao.salvar_consulta_como;
        btnLayout.Items.FindByName("btnCerregarOriginalConsultas").Text = Resources.traducao.carregar_configura__es_originais;
        btnLayout.Items.FindByName("btnCerregarOriginalConsultas").ToolTip = Resources.traducao.carregar_configura__es_originais_da_consulta;

        btnLayout.ClientVisible = true;

        #endregion
    }

    #endregion

  
}