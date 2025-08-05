using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.Web.Internal;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Processos_Visualizacao_VisualizacaoGrid_prev : System.Web.UI.Page
{

    #region Constants

    const string CONST_CodigoFluxo = "CF";
    const string CONST_CodigoProjeto = "CP";
    const string CONST_CodigoWorkflow = "CW";
    const string CONST_CodigoInstanciaWF = "CI";
    const string CONST_CodigoEtapaInicial = "EI";
    const string CONST_CodigoEtapaAtual = "CE";
    const string CONST_OcorrenciaAtual = "CS";
    const string CONST_CodigoStatus = "ST";
    const string CONST_Etapa = "ET";
    const Int16 CONST_LarguraMinimaColuna = 100;

    #endregion

    #region Fields

    dados cDados;
    DsListaProcessos ds;
    public int acessoEtapaInicial = 0;
    int codigoLista;
    int codigoListaUsuario;
    string dbName;
    string dbOwner;
    int codigoUsuarioLogado;
    int codigoEntidade;
    int codigoCarteira;
    int? codigoProjeto;
    int? codigoFluxo;
    int? codigoWorkflow;
    int? codigoInstancia;
    int? codigoEtapaInicial;
    int? codigoEtapaAtual;
    int? ocorrenciaAtual;
    int? codigoStatus;
    string etapa;
    string codigoModuloMenu;
    protected bool indicaRelatorio;
    protected bool indicaPodeIncluirNovoRegistro;
    protected bool indicaDadosInicializados;
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
        Regex regex = new Regex(@"(cmm)(=)(?<value>\w{3})");
        Match match = regex.Match(Request.Url.Query);
        codigoModuloMenu = match.Success ? match.Groups["value"].Value : "todos";

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

        indicaDadosInicializados = false;
        string ir = Request.QueryString["ir"];
        string cl = Request.QueryString["cl"];
        if (cl == null) return;

        indicaRelatorio = (ir == "S" || ir == "s");
        indicaPodeIncluirNovoRegistro = true;
        codigoLista = int.Parse(Request.QueryString["cl"]);
        if (!int.TryParse(Request.QueryString["clu"], out codigoListaUsuario))
            codigoListaUsuario = -1;
        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        codigoUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        codigoCarteira = Convert.ToInt32(cDados.getInfoSistema("CodigoCarteira"));
        ds = new DsListaProcessos();

        cDados.aplicaEstiloVisual(this);

        InitData();

        this.TH(this.TS("VisualizacaoGrid_prev"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CDIS_GridLocalizer.Activate();
        DefinePermiteNovoRegistro();

        DevExpress.Web.MenuItem btnIncluir = menu.Items.FindByName("btnIncluir");
        btnIncluir.ClientEnabled = true;

        btnIncluir.ClientVisible = indicaPodeIncluirNovoRegistro;

        if (!(IsPostBack || IsPostBack))
        {
            DefineCamposControlados();
            DefineDimensoesTela();
            DefineDimensoesGrid();
            DefinirConfiguracaoToolbar();
        }

        if (null != Request.QueryString["AEI"])
            acessoEtapaInicial = int.Parse(Request.QueryString["AEI"].ToString());

        string estiloFooter = "dxgvControl dxgvGroupPanel";

        string cssPostfix = "", cssPath = "";

        cDados.getVisual(cDados.getInfoSistema("IDEstiloVisual").ToString(), ref cssPath, ref cssPostfix);

        if (cssPostfix != "")
            estiloFooter = "dxgvControl_" + cssPostfix + " dxgvGroupPanel_" + cssPostfix;

        tbBotoes.Attributes.Add("class", estiloFooter);

        tbBotoes.Style.Add("padding", "3px");

        tbBotoes.Style.Add("border-collapse", "collapse");

        tbBotoes.Style.Add("border-bottom", "none");
        gvDados.SettingsResizing.ColumnResizeMode = ColumnResizeMode.Control;
        gvDados.SettingsBehavior.AllowGroup = true;
        gvDados.Settings.ShowGroupPanel = true;
        this.TH(this.TS("VisualizacaoGrid"));
    }

    private void DefinirConfiguracaoToolbar()
    {
        var lista = ds.Lista.Single(l => l.CodigoLista == codigoLista);
        var iMenu = this.menu.Items.FindByName("btnFilterEditor");

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
            else {
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

    protected void exporter_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data && e.Value != null)
        {
            if (e.Column.Name.StartsWith("fieldBLT_"))
            {
                e.BrickStyle.Font = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
                e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;
                string value = e.Value.ToString().ToLower();
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
                        e.BrickStyle.ForeColor = Color.WhiteSmoke;
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
            {
                if (e.Column is GridViewDataTextColumn)
                {
                    string strValue = Regex.Replace(e.Text, @"<[^>]*>", " ");
                    e.TextValue = strValue;
                    e.Text = strValue;
                }
            }
        }
        else if (e.RowType == GridViewRowType.Group)
        {
            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }
    }

    protected void dataSource_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        e.Command.CommandTimeout = 0;
        DsListaProcessos.ListaRow item = ds.Lista.Single(l => l.CodigoLista == codigoLista);
        ((SqlDataSourceView)sender).FilterExpression = item.InstrucaoPreFiltroDados;
        

    }

    #endregion

    #region Methods

    private void 
        InitData()
    {
        var comandoSql = new StringBuilder();
        var dicionarioParametros = new Dictionary<string, object>();
        dicionarioParametros.Add("CodigoLista", codigoLista);
        dicionarioParametros.Add("CodigoUsuario", codigoUsuarioLogado);
        dicionarioParametros.Add("CodigoListaUsuario", codigoListaUsuario);

        #region Comandos SQL

        if (codigoListaUsuario == -1)
        {
            #region Carrega lista padrao

            comandoSql.AppendLine(@"
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
        pflt.[IndicaPreFiltroCustomizavel],
        lu.CodigoListaUsuario
   FROM Lista AS l LEFT JOIN 
		ListaUsuario lu ON (lu.CodigoLista = l.CodigoLista AND lu.CodigoUsuario = @CodigoUsuario AND lu.IndicaListaPadrao = 'S') 
		OUTER APPLY dbo.f_GetPreFiltroLista(@CodigoUsuario, @CodigoLista, @CodigoListaUsuario) AS pflt
  WHERE (l.CodigoLista = @CodigoLista)");

            #endregion

            #region Carrega campos padrao

            comandoSql.AppendLine(@"
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
                              lu.CodigoUsuario = @CodigoUsuario AND
							  lu.IndicaListaPadrao = 'S' LEFT JOIN
        ListaCampoUsuario lcu ON lcu.CodigoListaUsuario = lu.CodigoListaUsuario AND
                                 lcu.CodigoCampo = lc.CodigoCampo
  WHERE (lc.CodigoLista = @CodigoLista)
  ORDER BY
        ISNULL(lcu.OrdemCampo, lc.OrdemCampo)");

            #endregion
        }
        else
        {
            #region Carrega lista selecionada

            comandoSql.AppendLine(@"
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
		ListaUsuario lu ON (lu.CodigoLista = l.CodigoLista AND lu.CodigoUsuario = @CodigoUsuario AND lu.CodigoListaUsuario = @CodigoListaUsuario) 
		OUTER APPLY dbo.f_GetPreFiltroLista(@CodigoUsuario, @CodigoLista, @CodigoListaUsuario) AS pflt
  WHERE (l.CodigoLista = @CodigoLista)");

            #endregion

            #region Carrega campos lista selecionada

            comandoSql.AppendLine(@"
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
                              lu.CodigoUsuario = @CodigoUsuario AND
							  lu.CodigoListaUsuario = @CodigoListaUsuario LEFT JOIN
        ListaCampoUsuario lcu ON lcu.CodigoListaUsuario = lu.CodigoListaUsuario AND
                                 lcu.CodigoCampo = lc.CodigoCampo
  WHERE (lc.CodigoLista = @CodigoLista)
  ORDER BY
        ISNULL(lcu.OrdemCampo, lc.OrdemCampo)");

            #endregion
        }

        #region Carrega fluxos

        comandoSql.AppendLine(@"
 SELECT CodigoLista, 
		CodigoFluxo, 
		TituloMenu
   FROM ListaFluxo
  WHERE (CodigoLista = @CodigoLista)");

        #endregion

        #region Carrega ListaCampoLink

        comandoSql.AppendLine(@"
 SELECT CodigoCampoLista,
        CodigoCampoSubLista
   FROM ListaCampoLink AS lcl INNER JOIN
        ListaCampo AS lc ON lc.CodigoCampo = lcl.CodigoCampoLista
  WHERE lc.CodigoLista = @CodigoLista");

        #endregion

        #endregion

        FillData(ds, ConnectionString, comandoSql.ToString(), dicionarioParametros, "Lista", "ListaCampo", "ListaFluxo", "ListaCampoLink");

        SetPopupMenuItems();

        DsListaProcessos.ListaRow item = ds.Lista.Single(l => l.CodigoLista == codigoLista);

        SetGridSettings(gvDados, item);
        SetDataSourceSettings(dataSource, item.ComandoSelect);
        if (gvDados.SettingsDetail.ShowDetailRow)
        {
            ConfiguraSubLista(item);
        }
        indicaDadosInicializados = true;

        
        List<String> lst = new List<String>();
        lst.AddRange(gvDados.GetFieldNames());
       
        if (!RelatorioDinamico.validaFilterExpression(item.InstrucaoPreFiltroDados, lst))
        {
            item.InstrucaoPreFiltroDados = String.Empty;
        }

    }

    private void ConfiguraSubLista(DsListaProcessos.ListaRow item)
    {
        DsListaProcessos dsDetalhe = ObtemMetaDadosSubLista(item.CodigoSubLista);
        DsListaProcessos.ListaRow itemDetalhe = dsDetalhe.Lista.Single();
        SetDataSourceSettings(dataSourceDetalhe, itemDetalhe.ComandoSelect);
        List<string> filtros = new List<string>();
        foreach (var lcl in ds.ListaCampoLink)
        {
            var campoDetalhe = dsDetalhe.ListaCampo.FindByCodigoCampo(lcl.CodigoCampoSubLista);
            var parameter = new SessionParameter(campoDetalhe.NomeCampo, campoDetalhe.NomeCampo);
            dataSourceDetalhe.FilterParameters.Add(parameter);
            filtros.Add(string.Format("{0}='{{0}}'", campoDetalhe.NomeCampo));
        }
        dataSourceDetalhe.FilterExpression = string.Join(" AND ", filtros);
        gvDados.Templates.DetailRow = new DetailRowTemplate(dataSourceDetalhe, itemDetalhe);
    }

    private void SetDataSourceSettings(SqlDataSource dataSource, string selectCommand)
    {
        var applicationName = string.Format("app=portal,ce={0},cul={1}", codigoEntidade, codigoUsuarioLogado);
        var conexao = new SqlConnectionStringBuilder(ConnectionString);
        conexao.ApplicationName = applicationName;

        SetDefaultSessionInfo();
        dataSource.SelectCommand = ReplaceParameters(selectCommand);
        dataSource.ConnectionString = conexao.ConnectionString;
        dataSource.SetSelectParameters();
    }

    private void SetDefaultSessionInfo()
    {
        var data = new Dictionary<string, object>();
        data.Add("pa_CodigoEntidadeContexto", codigoEntidade);
        data.Add("pa_CodigoUsuarioSistema", codigoUsuarioLogado);
        DataExtensions.SetSessionInfo(data);
    }

    private void DefineDimensoesGrid()
    {
        int qtdeColunasAgrupadas = gvDados.VisibleColumns.Where(c => c is GridViewEditDataColumn)
            .Cast<GridViewEditDataColumn>().Where(c => c.GroupIndex >= 0).Count();
        var col_acoes = gvDados.Columns["col_acoes"];
        col_acoes.Width = new Unit(indicaRelatorio ? 70 : 95, UnitType.Pixel);
        gvDados.Settings.HorizontalScrollBarMode = DevExpress.Web.ScrollBarMode.Auto;
        //gvDados.Columns[col_acoes.Index + 1].Width = new Unit("100%");
    }

    private void DefinePermiteNovoRegistro()
    {
        indicaPodeIncluirNovoRegistro = !(indicaRelatorio || ds.ListaFluxo.Count == 0);
    }

    protected string ObtemNomeFluxo()
    {
        if (ds != null && ds.ListaFluxo.Count == 1)
        {
            string comandoSql;

            #region Comando SQL

            comandoSql = string.Format(@"
DECLARE @CodigoEntidade INT
DECLARE @CodigoFluxo INT

		SET @CodigoEntidade = {0}
		SET @CodigoFluxo = {1}

 SELECT f.[NomeFluxo]
   FROM [Fluxos] AS f
  WHERE f.[CodigoFluxo]		= @CodigoFluxo
	AND f.[CodigoEntidade]  = @CodigoEntidade"
                , codigoEntidade
                , ds.ListaFluxo.Single().CodigoFluxo);

            #endregion

            DataSet dsTemp = cDados.getDataSet(comandoSql);
            DataRow dr = dsTemp.Tables[0].AsEnumerable().SingleOrDefault();
            if (dr == null) return null;

            return dr.Field<string>("NomeFluxo");
        }
        return null;
    }

    private void SetPopupMenuItems()
    {
        foreach (var item in ds.ListaFluxo)
        {
            var menuItem = popupMenu.Items.Add(item.TituloMenu);
            //menuItem.NavigateUrl = GetNavigateUrl(item.CodigoFluxo);
            menuItem.Name = item.CodigoFluxo.ToString();
        }
    }

    private string GetNavigateUrl(int codigoFluxo)
    {
        string url;
        int codigoWorkflow = ObtemCodigoWorkflowUltimaVersao(codigoFluxo);

        if (codigoWorkflow == -1)
            return null;

        url = "CW=" + codigoWorkflow;
        url += "&CF=" + codigoFluxo;
        url += "&CP=" + (-1);
        url += "&AEI=" + acessoEtapaInicial;


        return url;
    }

    private int ObtemCodigoWorkflowUltimaVersao(int codigoFluxo)
    {
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
DECLARE @CodigoEntidade INT
DECLARE @CodigoFluxo INT

		SET @CodigoEntidade = {0}
		SET @CodigoFluxo = {1}

 SELECT f.[CodigoFluxo],
		f.[IniciaisFluxo],
		MAX(wf.[CodigoWorkflow]) AS CodigoWorkflow
   FROM [Fluxos]    AS [f] INNER JOIN  
		[Workflows]	AS [wf] ON (wf.[CodigoFluxo] = f.[CodigoFluxo])
  WHERE f.[CodigoFluxo]		= @CodigoFluxo
	AND f.[CodigoEntidade]  = @CodigoEntidade
	AND wf.[DataRevogacao]  IS NULL 
	AND wf.[DataPublicacao] IS NOT NULL
  GROUP BY 
		f.[CodigoFluxo], 
		f.[IniciaisFluxo]"
            , codigoEntidade
            , codigoFluxo);

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        DataRow dr = ds.Tables[0].AsEnumerable().SingleOrDefault();
        int codigoWorkflow;
        codigoWorkflow = dr == null ? -1 : (int)dr["CodigoWorkflow"];
        return codigoWorkflow;
    }

    private static void SetGridSettings(ASPxGridView grid, DsListaProcessos.ListaRow item)
    {
        var campos = item.GetListaCampoRows();
        var camposChaves = campos.Where(c => c.IndicaCampoHierarquia == "P");
        if (camposChaves.Any())
        {
            grid.SettingsBehavior.AllowFocusedRow = true;
            grid.KeyFieldName = string.Join(";", camposChaves.Select(c => c.NomeCampo));
            if (!item.IsCodigoSubListaNull() && camposChaves.All(c => c.GetListaCampoLinkRows().Any()))
            {
                grid.SettingsDetail.ShowDetailRow = true;
            }
        }

        #region IndicaPaginacao

        bool possuiPaginacao = VerificaVerdadeiroOuFalso(item.IndicaPaginacao);
        if (possuiPaginacao)
        {
            grid.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            grid.SettingsPager.PageSize = item.QuantidadeItensPaginacao;
        }
        else
            grid.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

        #endregion

        #region IndicaListaZebrada

        bool listaZebrada = VerificaVerdadeiroOuFalso(item.IndicaListaZebrada);
        if (listaZebrada)
        {
            grid.Styles.AlternatingRow.BackColor = Color.FromArgb(0xEB, 0xEB, 0xEB);
            grid.Styles.AlternatingRow.Enabled = DefaultBoolean.True;
        }

        #endregion

        #region FiltroAplicado

        if (item.FiltroAplicado != null && item.FiltroAplicado.Contains("¥¥"))
            grid.FilterExpression = string.Empty;
        else
            grid.FilterExpression = item.FiltroAplicado;

        #endregion

        #region IndicaBuscaPalavraChave

        if (!item.IsIndicaBuscaPalavraChaveNull())
            grid.SettingsSearchPanel.Visible = item.IndicaBuscaPalavraChave == "S";

        #endregion

        #region Define Colunas

        if (grid.Columns.Count == 1)
        {
            foreach (var campo in campos)
            {
                if (VerificaVerdadeiroOuFalso(campo.IndicaLink))
                {
                    var nomeCampoCodigoProjeto = campos.Single(r => r.IniciaisCampoControlado == "CP").NomeCampo;
                    AddGridColumn(grid, campo, nomeCampoCodigoProjeto);
                }
                else
                {
                    AddGridColumn(grid, campo);
                }
            }
        }

        #endregion
    }

    private DsListaProcessos ObtemMetaDadosSubLista(int codigoSubLista)
    {
        var dsDetalhe = new DsListaProcessos();
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
 SELECT * FROM Lista WHERE CodigoLista = @CodigoLista
 SELECT * FROM ListaCampo WHERE CodigoLista = @CodigoLista");

        #endregion

        var dicionarioParametros = new Dictionary<string, object>();
        dicionarioParametros.Add("CodigoLista", codigoSubLista);

        FillData(dsDetalhe, ConnectionString, comandoSql, dicionarioParametros, "Lista", "ListaCampo");

        return dsDetalhe;
    }



    private static void AddGridColumn(ASPxGridView grid, DsListaProcessos.ListaCampoRow campo, string nomeCampoCodigoProjeto = null)
    {

        if (campo.IsIndicaColunaFixaNull())
            campo.SetField<string>("IndicaColunaFixa", "N");
        if (campo.TipoCampo.ToUpper().Equals("BLT"))
        {
            #region Bullet

            GridViewDataImageColumn colImg = new GridViewDataImageColumn();
            colImg.Caption = campo.TituloCampo;
            colImg.FieldName = campo.NomeCampo;
            colImg.GroupIndex = campo.OrdemAgrupamentoCampo;
            colImg.Name = string.Format("fieldBLT_{0}#{1}", campo.NomeCampo, campo.CodigoCampo);
            colImg.PropertiesImage.ImageUrlFormatString = "~/imagens/{0}.gif";
            colImg.Visible = VerificaVerdadeiroOuFalso(campo.IndicaCampoVisivel);
            colImg.VisibleIndex = campo.OrdemCampo;
            colImg.PropertiesImage.DisplayFormatString = "<img src='../../imagens/{0}.gif'/>";

            if (!campo.IsLarguraColunaNull())
                colImg.Width = new Unit(campo.LarguraColuna, UnitType.Pixel);
            if (!VerificaVerdadeiroOuFalso(campo.IndicaAgrupamento))
                colImg.Settings.AllowGroup = DefaultBoolean.False;

            colImg.FixedStyle = campo.IndicaColunaFixa == "S" ?
                GridViewColumnFixedStyle.Left :
                GridViewColumnFixedStyle.None;

            #region Tipo filtro

            colImg.Settings.ShowFilterRowMenu = DefaultBoolean.True;
            colImg.Settings.AllowAutoFilter = campo.TipoFiltro.ToUpper() == "E" ?
                DefaultBoolean.True : DefaultBoolean.False;
            colImg.Settings.AllowHeaderFilter = campo.TipoFiltro.ToUpper() == "C" ?
                DefaultBoolean.True : DefaultBoolean.False;
            colImg.SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList;

            #endregion

            #region Alinhamento

            HorizontalAlign alinhamento = HorizontalAlign.Center;
            switch (campo.AlinhamentoCampo.ToUpper())
            {
                case "E":
                    alinhamento = HorizontalAlign.Left;
                    break;
                case "D":
                    alinhamento = HorizontalAlign.Right;
                    break;
                case "C":
                    alinhamento = HorizontalAlign.Center;
                    break;
            }
            colImg.CellStyle.HorizontalAlign = alinhamento;
            colImg.HeaderStyle.HorizontalAlign = alinhamento;
            colImg.HeaderStyle.Wrap = DefaultBoolean.True;

            #endregion

            string tituloColunaAgrupadora = campo.TituloColunaAgrupadora.Trim();
            if (string.IsNullOrWhiteSpace(tituloColunaAgrupadora))
                grid.Columns.Add(colImg);
            else
            {
                GridViewBandColumn colunaAgrupadora = (GridViewBandColumn)
                    grid.VisibleColumns.SingleOrDefault(c => c is GridViewBandColumn && c.Caption == tituloColunaAgrupadora);
                if (colunaAgrupadora == null)
                {
                    colunaAgrupadora = new GridViewBandColumn(tituloColunaAgrupadora);
                    colunaAgrupadora.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    colunaAgrupadora.HeaderStyle.Wrap = DefaultBoolean.True;
                    colunaAgrupadora.HeaderStyle.Font.Bold = true;
                    grid.Columns.Add(colunaAgrupadora);
                }
                colunaAgrupadora.Columns.Add(colImg);
            }

            #endregion
        }
        else if (VerificaVerdadeiroOuFalso(campo.IndicaLink))
        {
            #region Link

            GridViewDataHyperLinkColumn colLnk = new GridViewDataHyperLinkColumn();
            colLnk.CellStyle.HorizontalAlign = HorizontalAlign.Left;
            colLnk.FieldName = nomeCampoCodigoProjeto;
            colLnk.Caption = campo.TituloCampo;
            colLnk.GroupIndex = campo.OrdemAgrupamentoCampo;
            colLnk.Name = string.Format("col#{0}", campo.CodigoCampo);
            colLnk.PropertiesHyperLinkEdit.NavigateUrlFormatString =
                "~/_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto={0}";
            colLnk.PropertiesHyperLinkEdit.Target = "_top";
            colLnk.PropertiesHyperLinkEdit.TextFormatString = campo.Formato;
            colLnk.PropertiesHyperLinkEdit.TextField = campo.NomeCampo;
            colLnk.Visible = VerificaVerdadeiroOuFalso(campo.IndicaCampoVisivel);
            colLnk.VisibleIndex = campo.OrdemCampo;
            if (!campo.IsLarguraColunaNull())
                colLnk.Width = new Unit(campo.LarguraColuna, UnitType.Pixel);
            if (!VerificaVerdadeiroOuFalso(campo.IndicaAgrupamento))
                colLnk.Settings.AllowGroup = DefaultBoolean.False;

            colLnk.FixedStyle = campo.IndicaColunaFixa == "S" ?
                GridViewColumnFixedStyle.Left :
                GridViewColumnFixedStyle.None;

            #region Tipo campo

            switch (campo.TipoCampo.ToUpper())
            {
                default:
                    break;
            }

            #endregion

            #region Tipo filtro

            colLnk.Settings.ShowFilterRowMenu = DefaultBoolean.True;
            colLnk.Settings.AllowAutoFilter = campo.TipoFiltro.ToUpper() == "E" ?
                DefaultBoolean.True : DefaultBoolean.False;
            colLnk.Settings.AllowHeaderFilter = campo.TipoFiltro.ToUpper() == "C" ?
                DefaultBoolean.True : DefaultBoolean.False;
            colLnk.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
            colLnk.Settings.FilterMode = ColumnFilterMode.DisplayText;
            colLnk.SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList;

            #endregion

            #region Tipo totalizados

            string nomeCampo = campo.NomeCampo;
            string tipoTotalizador = campo.TipoTotalizador.ToUpper();
            SummaryItemType summaryType = GetSummaryType(tipoTotalizador);
            ASPxSummaryItem summaryItem = new ASPxSummaryItem(nomeCampo, summaryType);
            summaryItem.DisplayFormat = campo.Formato;
            grid.GroupSummary.Add(summaryItem);
            grid.TotalSummary.Add(summaryItem);

            #endregion

            #region Alinhamento

            HorizontalAlign alinhamento = HorizontalAlign.Center;
            switch (campo.AlinhamentoCampo.ToUpper())
            {
                case "E":
                    alinhamento = HorizontalAlign.Left;
                    break;
                case "D":
                    alinhamento = HorizontalAlign.Right;
                    break;
                case "C":
                    alinhamento = HorizontalAlign.Center;
                    break;
            }
            colLnk.CellStyle.HorizontalAlign = alinhamento;
            colLnk.HeaderStyle.HorizontalAlign = alinhamento;
            colLnk.HeaderStyle.Wrap = DefaultBoolean.True;

            #endregion

            string tituloColunaAgrupadora = campo.TituloColunaAgrupadora.Trim();
            if (string.IsNullOrWhiteSpace(tituloColunaAgrupadora))
                grid.Columns.Add(colLnk);
            else
            {
                GridViewBandColumn colunaAgrupadora = (GridViewBandColumn)
                    grid.VisibleColumns.SingleOrDefault(c => c is GridViewBandColumn && c.Caption == tituloColunaAgrupadora);
                if (colunaAgrupadora == null)
                {
                    colunaAgrupadora = new GridViewBandColumn(tituloColunaAgrupadora);
                    colunaAgrupadora.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    colunaAgrupadora.HeaderStyle.Wrap = DefaultBoolean.True;
                    colunaAgrupadora.HeaderStyle.Font.Bold = true;
                    grid.Columns.Add(colunaAgrupadora);
                }
                colunaAgrupadora.Columns.Add(colLnk);
            }

            #endregion
        }
        else if (campo.TipoCampo == "DAT")
        {
            #region Data

            GridViewDataDateColumn colTxt = new GridViewDataDateColumn();
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
                colTxt.GroupIndex = campo.OrdemAgrupamentoCampo;
                colTxt.Name = string.Format("col#{0}", campo.CodigoCampo);
                colTxt.PropertiesDateEdit.DisplayFormatString = campo.Formato;
                colTxt.Visible = VerificaVerdadeiroOuFalso(campo.IndicaCampoVisivel);
                colTxt.VisibleIndex = campo.OrdemCampo;
                if (!campo.IsLarguraColunaNull())
                    colTxt.Width = new Unit(campo.LarguraColuna, UnitType.Pixel);
                if (!VerificaVerdadeiroOuFalso(campo.IndicaAgrupamento))
                    colTxt.Settings.AllowGroup = DefaultBoolean.False;

                colTxt.FixedStyle = campo.IndicaColunaFixa == "S" ?
                    GridViewColumnFixedStyle.Left :
                    GridViewColumnFixedStyle.None;

                #region Tipo campo

                switch (campo.TipoCampo.ToUpper())
                {
                    default:
                        break;
                }

                #endregion

                #region Tipo filtro

                colTxt.Settings.ShowFilterRowMenu = DefaultBoolean.True;
                colTxt.Settings.AllowAutoFilter = campo.TipoFiltro.ToUpper() == "E" ?
                    DefaultBoolean.True : DefaultBoolean.False;
                colTxt.Settings.AllowHeaderFilter = campo.TipoFiltro.ToUpper() == "C" ?
                    DefaultBoolean.True : DefaultBoolean.False;
                colTxt.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                //colTxt.SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList;

                #endregion

                #region Tipo totalizados

                string nomeCampo = campo.NomeCampo;
                string tipoTotalizador = campo.TipoTotalizador.ToUpper();
                SummaryItemType summaryType = GetSummaryType(tipoTotalizador);
                ASPxSummaryItem summaryItem = new ASPxSummaryItem(nomeCampo, summaryType);
                summaryItem.DisplayFormat = campo.Formato;
                grid.GroupSummary.Add(summaryItem);
                grid.TotalSummary.Add(summaryItem);

                #endregion

                #region Alinhamento

                HorizontalAlign alinhamento = HorizontalAlign.Center;
                switch (campo.AlinhamentoCampo.ToUpper())
                {
                    case "E":
                        alinhamento = HorizontalAlign.Left;
                        break;
                    case "D":
                        alinhamento = HorizontalAlign.Right;
                        break;
                    case "C":
                        alinhamento = HorizontalAlign.Center;
                        break;
                }
                colTxt.CellStyle.HorizontalAlign = alinhamento;
                colTxt.HeaderStyle.HorizontalAlign = alinhamento;
                colTxt.HeaderStyle.Wrap = DefaultBoolean.True;

                #endregion
            }
            string tituloColunaAgrupadora = campo.TituloColunaAgrupadora.Trim();
            if (string.IsNullOrWhiteSpace(tituloColunaAgrupadora))
                grid.Columns.Add(colTxt);
            else
            {
                GridViewBandColumn colunaAgrupadora = (GridViewBandColumn)
                    grid.VisibleColumns.SingleOrDefault(c => c is GridViewBandColumn && c.Caption == tituloColunaAgrupadora);
                if (colunaAgrupadora == null)
                {
                    colunaAgrupadora = new GridViewBandColumn(tituloColunaAgrupadora);
                    colunaAgrupadora.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    colunaAgrupadora.HeaderStyle.Wrap = DefaultBoolean.True;
                    colunaAgrupadora.HeaderStyle.Font.Bold = true;
                    grid.Columns.Add(colunaAgrupadora);
                }
                colunaAgrupadora.Columns.Add(colTxt);
            }

            #endregion
        }
        else
        {
            #region Outros

            GridViewDataTextColumn colTxt = new GridViewDataTextColumn();
            colTxt.PropertiesTextEdit.EncodeHtml = false;
            colTxt.FieldName = campo.NomeCampo;
            if (VerificaVerdadeiroOuFalso(campo.IndicaCampoControle))
            {
                colTxt.Visible = false;
                colTxt.ShowInCustomizationForm = false;
                string iniciais = campo.IniciaisCampoControlado;
                if (!string.IsNullOrEmpty(iniciais))
                    colTxt.Name = string.Format("colCC_{0}", iniciais);
                //grid.Columns.Insert(0, colTxt);
                //return;
            }
            else
            {
                colTxt.Caption = campo.TituloCampo;
                colTxt.GroupIndex = campo.OrdemAgrupamentoCampo;
                colTxt.Name = string.Format("col#{0}", campo.CodigoCampo);
                colTxt.PropertiesTextEdit.DisplayFormatString = campo.Formato;
                colTxt.Visible = VerificaVerdadeiroOuFalso(campo.IndicaCampoVisivel);
                colTxt.VisibleIndex = campo.OrdemCampo;
                if (!campo.IsLarguraColunaNull())
                    colTxt.Width = new Unit(campo.LarguraColuna, UnitType.Pixel);
                if (!VerificaVerdadeiroOuFalso(campo.IndicaAgrupamento))
                    colTxt.Settings.AllowGroup = DefaultBoolean.False;

                colTxt.FixedStyle = campo.IndicaColunaFixa == "S" ?
                    GridViewColumnFixedStyle.Left :
                    GridViewColumnFixedStyle.None;

                #region Tipo campo

                switch (campo.TipoCampo.ToUpper())
                {
                    default:
                        break;
                }

                #endregion

                #region Tipo filtro

                colTxt.Settings.ShowFilterRowMenu = DefaultBoolean.True;
                colTxt.Settings.AllowAutoFilter = campo.TipoFiltro.ToUpper() == "E" ?
                    DefaultBoolean.True : DefaultBoolean.False;
                colTxt.Settings.AllowHeaderFilter = campo.TipoFiltro.ToUpper() == "C" ?
                    DefaultBoolean.True : DefaultBoolean.False;
                colTxt.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                colTxt.SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList;

                #endregion

                #region Tipo totalizados

                string nomeCampo = campo.NomeCampo;
                string tipoTotalizador = campo.TipoTotalizador.ToUpper();
                SummaryItemType summaryType = GetSummaryType(tipoTotalizador);
                ASPxSummaryItem summaryItem = new ASPxSummaryItem(nomeCampo, summaryType);
                summaryItem.DisplayFormat = campo.Formato;
                grid.GroupSummary.Add(summaryItem);
                grid.TotalSummary.Add(summaryItem);

                #endregion

                #region Alinhamento

                HorizontalAlign alinhamento = HorizontalAlign.Center;
                switch (campo.AlinhamentoCampo.ToUpper())
                {
                    case "E":
                        alinhamento = HorizontalAlign.Left;
                        break;
                    case "D":
                        alinhamento = HorizontalAlign.Right;
                        break;
                    case "C":
                        alinhamento = HorizontalAlign.Center;
                        break;
                }
                colTxt.CellStyle.HorizontalAlign = alinhamento;
                colTxt.HeaderStyle.HorizontalAlign = alinhamento;
                colTxt.HeaderStyle.Wrap = DefaultBoolean.True;

                #endregion
            }

            string tituloColunaAgrupadora = campo.TituloColunaAgrupadora.Trim();
            if (string.IsNullOrWhiteSpace(tituloColunaAgrupadora))
                grid.Columns.Add(colTxt);
            else
            {
                GridViewBandColumn colunaAgrupadora = (GridViewBandColumn)
                    grid.VisibleColumns.SingleOrDefault(c => c is GridViewBandColumn && c.Caption == tituloColunaAgrupadora);
                if (colunaAgrupadora == null)
                {
                    colunaAgrupadora = new GridViewBandColumn(tituloColunaAgrupadora);
                    colunaAgrupadora.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    colunaAgrupadora.HeaderStyle.Wrap = DefaultBoolean.True;
                    colunaAgrupadora.HeaderStyle.Font.Bold = true;
                    grid.Columns.Add(colunaAgrupadora);
                }
                colunaAgrupadora.Columns.Add(colTxt);
            }

            #endregion
        }



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
            case "MEDIA":
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

    private static bool VerificaVerdadeiroOuFalso(string valor)
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
		 * {5} - Código Fluxo
		 * {6} - Código Workflow
		 * {7} - Código Instância
		 * {8} - Código Etapa
		 * {9} - Ocorrência Atual
		 * {10} - Código Carteira
		 */
        const string valorNaoDefinido = "-1";
        comandoPreencheLista = comandoPreencheLista
            .Replace("{0}", dbName)
            .Replace("{1}", dbOwner)
            .Replace("{2}", codigoProjeto.HasValue ? codigoProjeto.ToString() : valorNaoDefinido)
            .Replace("{3}", codigoEntidade.ToString())
            .Replace("{4}", codigoUsuarioLogado.ToString())
            .Replace("{5}", codigoFluxo.HasValue ? codigoFluxo.ToString() : valorNaoDefinido)
            .Replace("{6}", codigoWorkflow.HasValue ? codigoWorkflow.ToString() : valorNaoDefinido)
            .Replace("{7}", codigoInstancia.HasValue ? codigoInstancia.ToString() : valorNaoDefinido)
            .Replace("{8}", codigoEtapaAtual.HasValue ? codigoEtapaAtual.ToString() : valorNaoDefinido)
            .Replace("{9}", ocorrenciaAtual.HasValue ? ocorrenciaAtual.ToString() : valorNaoDefinido)
            .Replace("{10}", codigoCarteira.ToString());

        return comandoPreencheLista;
    }

    private static void FillData(DataSet ds, string connectionString, string comandoSql, Dictionary<string, object> parametros, params string[] nomesTabelas)
    {
        if (parametros == null)
            throw new ArgumentException("'parametros' não pode ser nulo.");
        if (nomesTabelas == null || nomesTabelas.Length == 0)
            throw new ArgumentException("'nomesTabelas' deve conter ao menos um nome de tabela");

        using (var conn = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(comandoSql, conn);
            command.CommandTimeout = CDIS.ClasseDados.TimeOutSqlCommand;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            foreach (var parametro in parametros)
            {
                command.Parameters.AddWithValue(parametro.Key, parametro.Value);
            }
            for (int i = 0; i < nomesTabelas.Length; i++)
            {
                string sourceTable = "Table";
                string dataSetTable = nomesTabelas[i];
                if (i > 0)
                    sourceTable = string.Format("{0}{1}", sourceTable, i);
                dataAdapter.TableMappings.Add(sourceTable, dataSetTable);
            }
            dataAdapter.Fill(ds);
        }
    }

    private void DefineCamposControlados()
    {
        hfGeral.Clear();
        var campos = ds.ListaCampo
            .Where(lc => lc.CodigoLista == codigoLista)
            .Where(lc => !string.IsNullOrWhiteSpace(lc.IniciaisCampoControlado))
            .Select(lc => new { lc.IniciaisCampoControlado, lc.NomeCampo }).ToList();
        campos.ForEach(c => hfGeral.Add(c.IniciaisCampoControlado, c.NomeCampo));
    }

    private string ObtemBotaoEVTFluxo()
    {
        //string codigoProjeto = Eval("CodigoDemanda").ToString();
        //string podeIniciarFluxo = Eval("PodeIniciarFluxoAbrirProjeto").ToString();

        //string strBotao = "";

        ////if (podeIniciarFluxo == "S")
        ////    strBotao = string.Format(@"<img title='Fazer abertura do projeto' onclick='abreNovoFluxoEVT({0}, ""PRJ_TIC_PS"");' style='cursor:pointer' src='../imagens/botoes/btnEngrenagem.png' />"
        ////            , codigoProjeto);
        ////else
        ////    strBotao = string.Format(@"<img alt='' src='../imagens/botoes/btnEngrenagemDes.png' />"); 

        //return strBotao;

        //return string.Format(ObtemHtmlBotaoDesabilitadoGrid("../../imagens/botoes/btnEngrenagemDes.png"));

        return string.Empty;
    }

    private string ObtemBotaoGraficoFluxo(int? codigoWorkflow, int? codigoInstanciaWF, int? codigoFluxo, int? codigoProjeto, string nomeProcessoWf)
    {
        string strBotao;
        if (codigoFluxo.HasValue && codigoInstanciaWF.HasValue && codigoProjeto.HasValue && codigoWorkflow.HasValue)
        {
            /*string urlDirecionamento = string.Format("top.location.href = \"../../_Portfolios/GraficoProcesso.aspx?CW={0}&CI={1}&CF={2}&CP={3}&ModuloMenuLDP={4}&NivelNavegacao=3\""
				, codigoWorkflow, codigoInstanciaWF, codigoFluxo, codigoProjeto, codigoModuloMenu);*/
            string urlDirecionamento = string.Format("mostraPopupGraficoProcesso(\"{0}\", \"{1}\", \"{2}\", \"{3}\")", codigoInstancia, codigoWorkflow, codigoFluxo, codigoProjeto);
            strBotao = ObtemHtmlBotaoGrid(Resources.traducao.VisualizacaoGrid_visualizar_fluxo_graficamente, urlDirecionamento, "../../imagens/botoes/fluxos.PNG");
        }
        else
            strBotao = ObtemHtmlBotaoDesabilitadoGrid("../../imagens/botoes/fluxosDes.PNG");
        return strBotao;
    }

    private string ObtemBotaoInteragirFluxo(string etapa, int? codigoWorkflow, int? codigoInstanciaWf, int? codigoFluxo, int? codigoProjeto, int? codigoEtapaInicial, int? codigoEtapaAtual, int? ocorrenciaAtual, int? codigoStatus)
    {
        string strBotao = string.Empty;
        int acessoFluxo = 0;
        int retornoFluxo;
        int retornoCodigoWorkflow;
        int retornoCodigoEtapaInicial;
        cDados.getCodigoWfAtualPorIniciaisFluxo(codigoEntidade, out retornoFluxo
            , out retornoCodigoWorkflow, out retornoCodigoEtapaInicial, string.Empty);
        if (codigoEtapaAtual.HasValue && codigoFluxo.HasValue)
        {
            acessoFluxo = cDados.obtemNivelAcessoEtapaWf(codigoWorkflow.Value
                , codigoInstanciaWf.Value, ocorrenciaAtual.Value
                , codigoEtapaAtual.Value, codigoUsuarioLogado.ToString());
        }
        else if (!codigoFluxo.HasValue && (codigoStatus == 1 || codigoStatus == 14))
        {
            codigoFluxo = retornoFluxo;
            codigoWorkflow = retornoCodigoWorkflow;
            codigoEtapaInicial = retornoCodigoEtapaInicial;

            acessoFluxo = cDados.obtemNivelAcessoEtapaWfNaoInstanciada(codigoWorkflow
                , codigoProjeto.ToString(), codigoEtapaInicial, codigoUsuarioLogado.ToString());
        }

        if (acessoFluxo == 0 || codigoStatus == 3 || codigoStatus == 16)
        {
            strBotao = "<img alt='' style='cursor:default' src='../../imagens/botoes/interagirDes.png' />";
        }
        else if (acessoFluxo == 1)
        {
            if (codigoFluxo.HasValue && codigoProjeto.HasValue && codigoWorkflow.HasValue && codigoInstanciaWf.HasValue && codigoEtapaAtual.HasValue && ocorrenciaAtual.HasValue)
            {
                /*string urlDirecionamento = string.Format("../../wfEngine.aspx?NivelNavegacao=3&RO=S&ModuloMenuLDP={6}&CF={0}&CP={1}&CW={2}&CI={3}&CE={4}&CS={5}",
					codigoFluxo, codigoProjeto, codigoWorkflow, codigoInstanciaWf, codigoEtapaAtual, ocorrenciaAtual, codigoModuloMenu);
                strBotao = string.Format(@"<img title='Visulizar Etapa Atual' onclick='window.top.location.href = ""{0}""';' style='cursor:pointer' src='../../imagens/botoes/pFormulario.png' />"
					, urlDirecionamento);*/
                string urlDirecionamento = string.Format("mostraPopupFormulario(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\");"
                    , codigoInstancia, codigoWorkflow, codigoFluxo, codigoProjeto, ocorrenciaAtual, codigoEtapaAtual);
                strBotao = string.Format(@"<img title='Visulizar Etapa Atual' onclick='{0}' style='cursor:pointer' src='../../imagens/botoes/pFormulario.png' />"
                    , urlDirecionamento);
            }
            else
            {
                strBotao = "<img alt='' style='cursor:default' src='../../imagens/botoes/interagirDes.png' />";
            }
        }
        else
        {
            /*string parametros = string.Empty;

			if (codigoInstanciaWf.HasValue && codigoEtapaAtual.HasValue && ocorrenciaAtual.HasValue)
				parametros = string.Format("&CI={0}&CE={1}&CS={2}", codigoInstanciaWf, codigoEtapaAtual, ocorrenciaAtual);

			string urlDirecionamento = string.Format("../../wfEngine.aspx?NivelNavegacao=3&ModuloMenuLDP={4}&CF={0}&CP={1}&CW={2}{3}"
				, codigoFluxo, codigoProjeto, codigoWorkflow, parametros, codigoModuloMenu);
			strBotao = string.Format(@"<img title='Interagir' onclick='window.top.location.href = ""{0}"";' style='cursor:pointer' src='../../imagens/botoes/interagir.PNG' />"
				, urlDirecionamento);*/
            string urlDirecionamento = string.Format("mostraPopupFormulario(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\");"
                  , codigoInstancia, codigoWorkflow, codigoFluxo, codigoProjeto, ocorrenciaAtual, codigoEtapaAtual);

            strBotao = string.Format(@"<img title='Interagir' onclick='{0}' style='cursor:pointer' src='../../imagens/botoes/interagir.PNG' />"
                , urlDirecionamento);
        }

        return strBotao;
    }

    public string ObtemHtmlBotoes()
    {
        if (ds == null) return string.Empty;

        StringBuilder htmlBotoes = new StringBuilder();
        if (!indicaRelatorio)
        {
            DefineValorCamposControlados();

            string comandoSql = string.Format(@"
DECLARE @CodigoWorkflow int
DECLARE @CodigoInstanciaWf bigint

    SET @CodigoWorkflow = {0}
    SET @CodigoInstanciaWf = {1}
    
 SELECT TOP 1 NomeInstancia 
   FROM InstanciasWorkflows 
  WHERE CodigoWorkflow = @CodigoWorkflow 
    AND CodigoInstanciaWf = @CodigoInstanciaWf",
    codigoWorkflow.HasValue ? codigoWorkflow.Value : int.MinValue,
    codigoInstancia.HasValue ? codigoInstancia.Value : int.MinValue);
            DataSet dsTemp = cDados.getDataSet(comandoSql);
            string nomeProcessoWf = dsTemp.Tables[0].Rows.Count > 0 ?
                dsTemp.Tables[0].Rows[0]["NomeInstancia"] as string : string.Empty;
            nomeProcessoWf = nomeProcessoWf.Replace("\"", "\\\"");

            #region Visualizar Fluxo

            string botaoGraficoFluxo = ObtemBotaoGraficoFluxo(
                codigoWorkflow, codigoInstancia, codigoFluxo, codigoProjeto, nomeProcessoWf);
            htmlBotoes.AppendFormat("<span style=\"padding-right: 5px\">{0}</span>", botaoGraficoFluxo);

            #endregion

            #region Atualizar Fluxo

            string botaoInteragirFluxo = ObtemBotaoInteragirFluxo(etapa
                , codigoWorkflow, codigoInstancia, codigoFluxo, codigoProjeto
                , codigoEtapaInicial, codigoEtapaAtual, ocorrenciaAtual, codigoStatus);
            htmlBotoes.AppendFormat("<span style=\"padding-right: 5px\">{0}</span>", botaoInteragirFluxo);

            #endregion

            #region Fazer abertura do projeto

            string botaoFazerAberturaProjeto = ObtemBotaoEVTFluxo();
            htmlBotoes.AppendFormat("<span>{0}</span>", botaoFazerAberturaProjeto);

            #endregion
        }

        return htmlBotoes.ToString();
    }

    private void DefineValorCamposControlados()
    {
        DefineValorCampoCodigo(CONST_CodigoProjeto, out codigoProjeto);
        DefineValorCampoCodigo(CONST_CodigoFluxo, out codigoFluxo);
        DefineValorCampoCodigo(CONST_CodigoWorkflow, out codigoWorkflow);
        DefineValorCampoCodigo(CONST_CodigoInstanciaWF, out codigoInstancia);
        DefineValorCampoCodigo(CONST_CodigoEtapaInicial, out codigoEtapaInicial);
        DefineValorCampoCodigo(CONST_CodigoEtapaAtual, out codigoEtapaAtual);
        DefineValorCampoCodigo(CONST_OcorrenciaAtual, out ocorrenciaAtual);
        DefineValorCampoCodigo(CONST_CodigoStatus, out codigoStatus);
        etapa = (string)ObtemValorAtualCampoControlado(CONST_Etapa);
    }

    private void DefineValorCampoCodigo(string iniciaisCampo, out int? valorCampoCodigo)
    {
        int valor = Convert.ToInt32(ObtemValorAtualCampoControlado(iniciaisCampo));
        if (valor == 0)
            valorCampoCodigo = null;
        else
            valorCampoCodigo = valor;
    }

    private object ObtemValorAtualCampoControlado(string iniciaisCampo)
    {
        if (hfGeral.Contains(iniciaisCampo))
        {
            string nomeCampo = (string)hfGeral[iniciaisCampo];
            object valor = Eval(nomeCampo);
            if (!VerificarValorNulo(valor))
                return valor;
        }
        return null;
    }

    private static string ObtemHtmlBotaoGrid(string title, string onClick, string imagem)
    {
        string strBotao = string.Format(
            "<img title='{0}' onclick='{1}' style='cursor:pointer' src='{2}' />"
            , title, onClick, imagem);

        return strBotao;
    }

    private static string ObtemHtmlBotaoDesabilitadoGrid(string imagem)
    {
        string strBotao = string.Format("<img alt='' style='cursor:default' src='{0}' />", imagem);

        return strBotao;
    }

    private static bool VerificarValorNulo(object valor)
    {
        return valor == null || Convert.IsDBNull(valor);
    }

    private void DefineDimensoesTela()
    {
        string res = cDados.getInfoSistema("ResolucaoCliente").ToString();
        larguraTela = int.Parse(res.Split('x')[0]);
        alturaTela = int.Parse(res.Split('x')[1]);
    }

    private bool SalvarConsulta()
    {
        int qtdeItensPagina = gvDados.SettingsPager.PageSize;
        string filter = gvDados.FilterExpression;
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
    SET @FiltroAplicado = '{3}'
	SET @CodigoListaUsuario = {4}
	
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
    , codigoLista, codigoUsuarioLogado, qtdeItensPagina, filter.Replace("'", "''"), codigoListaUsuario);

        #endregion

        foreach (GridViewEditDataColumn col in gvDados.AllColumns.Where(c => !(c is GridViewBandColumn) && c.ShowInCustomizationForm))
        {
            int ordemCampo = col.VisibleIndex;
            int ordemAgrupamentoCampo = col.GroupIndex;
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
        int qtdeItensPagina = gvDados.SettingsPager.PageSize;
        string filter = gvDados.FilterExpression;
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
            SET @FiltroAplicado = '{3}'
			SET @NomeListaUsuario = '{4}'
	        SET @InstrucaoPreFiltroDados = '{5}'
	        SET @InstrucaoPreFiltroDadosOriginal = '{6}'
			SET @IndicaListaPadrao = (SELECT CASE WHEN EXISTS(SELECT 1 FROM ListaUsuario WHERE CodigoUsuario = @CodigoUsuario AND CodigoLista = @CodigoLista) THEN 'N' ELSE 'S' END)

		 INSERT INTO ListaUsuario(CodigoUsuario, CodigoLista, QuantidadeItensPaginacao, FiltroAplicado, NomeListaUsuario, IndicaListaPadrao, InstrucaoPreFiltroDados, InstrucaoPreFiltroDadosOriginal) 
		 VALUES (@CodigoUsuario, @CodigoLista, @QuantidadeItensPaginacao, @FiltroAplicado, @NomeListaUsuario, @IndicaListaPadrao, @InstrucaoPreFiltroDados, @InstrucaoPreFiltroDadosOriginal)

		SET @CodigoListaUsuario = SCOPE_IDENTITY()"
    , codigoLista, codigoUsuarioLogado, qtdeItensPagina, filter.Replace("'", "''"), nomeListaUsuario
    , item.InstrucaoPreFiltroDados.Replace("'", "''"), item.InstrucaoPreFiltroOriginal.Replace("'", "''"));

        #endregion

        foreach (GridViewEditDataColumn col in gvDados.AllColumns.Where(c => !(c is GridViewBandColumn) && c.ShowInCustomizationForm))
        {
            int ordemCampo = col.VisibleIndex;
            int ordemAgrupamentoCampo = col.GroupIndex;
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

    private int ObtemCodigoCampo(GridViewColumn col)
    {
        int codigoCampo = -1;
        string columnName = col.Name;
        int indiceCodigoCampo = columnName.LastIndexOf('#') + 1;
        if (indiceCodigoCampo > 0)
            codigoCampo = int.Parse(columnName.Substring(indiceCodigoCampo));

        return codigoCampo;
    }

    private string RemoveFormatacaoHtml(string input)
    {
        string strValue = Regex.Replace(input ?? string.Empty, @"<[^>]*>", " ");
        return strValue;
    }

    #endregion

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLSX" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        if (e.Item.Image.Url != "~/imagens/botoes/btnDownload.png")
        {
            cDados.eventoClickMenu((source as ASPxMenu), parameter, exporter, "");
        }
    }

    protected void menu_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        ASPxMenu menu = (sender as ASPxMenu);

        DsListaProcessos.ListaRow item = ds.Lista.Single(l => l.CodigoLista == codigoLista);
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

        #region INCLUIR

        DevExpress.Web.MenuItem btnIncluir = menu.Items.FindByName("btnIncluir");
        btnIncluir.ToolTip = Resources.traducao.incluir;

        #endregion

        #region JS

        string funcaoJS = string.Empty;
        if (ds.ListaFluxo.Count == 1)
        {
            funcaoJS = @"abreFluxo('" + GetNavigateUrl(ds.ListaFluxo.Single().CodigoFluxo) + "')";

            defineInclusaoNovaInstancia(ref funcaoJS);
        }




        menu.ClientSideEvents.ItemClick =
    String.Format(@"function(s, e){{ 
	var indicaPossuiListaUsuario = '{2}';
	e.processOnServer = false;

	if(e.item.name == 'btnIncluir'){{
        if (popupMenu.GetItemCount() > 1)
                setTimeout(function () {{ popupMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent)); }}, 100);
        else {{
                {3}
            }}
    }}
	else if(e.item.name == 'btnAbrirConsultas'){{parent.ExibeConsultaSalvas({0}, {1});}}
	else if(e.item.name == 'btnSalvarConsultas'){{if(indicaPossuiListaUsuario == 'S'){{SalvarConsulta();}}else{{parent.ExibirJanelaSalvarComo();}}}}
	else if(e.item.name == 'btnSalvarComoConsultas'){{parent.ExibirJanelaSalvarComo();}}
	else if(e.item.name == 'btnCerregarOriginalConsultas'){{CarregarConsulta(0);}}
    else if(e.item.name == 'btnFilterEditor'){{window.parent.showModalComFooter('FilterEditorPopup.aspx?cl={0}&clu={4}', 'Filtro dos dados', 300, 400, CarregarConsulta, null);}}
	else if(e.item.name != 'btnLayout'){{e.processOnServer = true;}}	
}}", codigoLista, codigoUsuarioLogado, item.IndicaPossuiListaUsuario, funcaoJS, codigoListaUsuario);

        #endregion

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

    public class DetailRowTemplate : ITemplate
    {
        private SqlDataSource _dataSource;
        private DsListaProcessos.ListaRow _item;

        public DetailRowTemplate(SqlDataSource dataSource, DsListaProcessos.ListaRow item)
        {
            _dataSource = dataSource;
            _item = item;
        }

        public void InstantiateIn(Control container)
        {
            var templateContainer = (GridViewDetailRowTemplateContainer)container;
            var detailGridView = new ASPxGridView();
            var col = new GridViewDataTextColumn();
            col.Width = new Unit(20, UnitType.Pixel);
            detailGridView.Columns.Add(col);
            detailGridView.DataSource = _dataSource;
            detailGridView.Font.Name = "Verdana";
            detailGridView.Font.Size = new FontUnit(8, UnitType.Point);
            detailGridView.BeforePerformDataSelect += new EventHandler(detailGridView_BeforePerformDataSelect);
            detailGridView.Settings.HorizontalScrollBarMode = ScrollBarMode.Visible;
            detailGridView.Settings.ShowFilterRow = true;
            detailGridView.Settings.ShowGroupPanel = true;
            detailGridView.Settings.ShowHeaderFilterBlankItems = false;
            detailGridView.Settings.ShowHeaderFilterButton = true;
            detailGridView.SettingsBehavior.AutoExpandAllGroups = true;
            detailGridView.SettingsResizing.ColumnResizeMode = ColumnResizeMode.Control;
            detailGridView.Width = new Unit(100, UnitType.Percentage);
            SetGridSettings(detailGridView, _item);
            templateContainer.Controls.Add(detailGridView);
        }

        void detailGridView_BeforePerformDataSelect(object sender, EventArgs e)
        {
            var detailGridView = (ASPxGridView)sender;
            var container = (GridViewDetailRowTemplateContainer)detailGridView.NamingContainer;
            var valoresChaveMaster = detailGridView.GetMasterRowKeyValue().ToString().Split(';');
            var colunasChaveMaster = container.Grid.KeyFieldName.Split(';');
            for (int i = 0; i < colunasChaveMaster.Length; i++)
                HttpContext.Current.Session[colunasChaveMaster[i]] = valoresChaveMaster[i];
        }
    }

    protected void callbackCriaInstancia_Callback(object source, CallbackEventArgs e)
    {
        if (e.Parameter != "")
        {
            codigoFluxo = int.Parse(e.Parameter);
            bool confirmaInstancia = false;

            string comandoSQL2 = string.Format(@"
        SELECT ISNULL(CriaInstanciaAutomatica, 'N') AS CriaInstanciaAutomatico
          FROM Fluxos
         WHERE CodigoFluxo = {0}", codigoFluxo);

            DataSet dsInst = cDados.getDataSet(comandoSQL2);

            if (cDados.DataSetOk(dsInst) && cDados.DataTableOk(dsInst.Tables[0]))
                confirmaInstancia = dsInst.Tables[0].Rows[0]["CriaInstanciaAutomatico"].ToString().ToUpper().Trim() == "S";

            if (!confirmaInstancia)
            {
                callbackCriaInstancia.JSProperties["cp_Parametros"] = GetNavigateUrl(codigoFluxo.Value);
                return;
            }
        }
        var codigoWorkflowUltimaVersao = ObtemCodigoWorkflowUltimaVersao(codigoFluxo.Value);
        var tituloInstanciaWf = montaTituloInstancia(codigoProjeto, codigoWorkflowUltimaVersao);
        string comandoSQL = string.Format(@"
        BEGIN
                    INSERT INTO [InstanciasWorkflows]
                               ([CodigoWorkflow]
                               ,[NomeInstancia]
                               ,[DataInicioInstancia]
                               ,[DataTerminoInstancia]
                               ,[OcorrenciaAtual]
                               ,[EtapaAtual]
                               ,[IdentificadorUsuarioCriadorInstancia]
                               ,[IdentificadorProjetoRelacionado])
                         VALUES
                               ({0}
                               ,LEFT('{1}',250)
                               , GETDATE()
                               , NULL
                               , NULL
                               , NULL
                               , '{2}'
                               , {3} )

                    DECLARE @CodigoInstanciaWf int 
                    DECLARE @CodigoEtapaInicial int
                    SELECT @CodigoInstanciaWf = scope_identity(), 
                           @CodigoEtapaInicial = [CodigoEtapaInicial]  
                   FROM Workflows 
                     WHERE CodigoWorkflow = {0}

                    INSERT INTO [EtapasInstanciasWf]
                               ([CodigoWorkflow]
                               ,[CodigoInstanciaWf]
                               ,[SequenciaOcorrenciaEtapaWf]
                               ,[CodigoEtapaWf]
                               ,[DataInicioEtapa]
                               ,[IdentificadorUsuarioIniciador]
                               ,[DataTerminoEtapa]
                               ,IdentificadorUsuarioFinalizador)
                         VALUES
                               ({0}
                               ,@CodigoInstanciaWf
                               ,1
                               ,@CodigoEtapaInicial
                               , GETDATE()
                               , '{2}'
                               , NULL
                               , NULL)

                        UPDATE InstanciasWorkflows
                           SET [OcorrenciaAtual] = 1
                             , [EtapaAtual] = @CodigoEtapaInicial
                        WHERE [CodigoWorkflow] = {0}
                          AND [CodigoInstanciaWf] = @CodigoInstanciaWf                        

                        SELECT @CodigoInstanciaWf as CodigoInstanciaWf, @CodigoEtapaInicial as CodigoEtapaInicial

                END", codigoWorkflowUltimaVersao, tituloInstanciaWf.Replace("'", "''"), codigoUsuarioLogado, codigoProjeto.HasValue ? codigoProjeto.Value.ToString() : "null");

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataTable dt = ds.Tables[0];
            int auxCodigoInstanciaWf = int.Parse(dt.Rows[0]["CodigoInstanciaWf"].ToString());
            string codigoEtapaWf = dt.Rows[0]["CodigoEtapaInicial"].ToString();
            string chamaTelaMenu = (Request.QueryString["TL"] + "") != "" ? "&TL=CHI" : "";


            callbackCriaInstancia.JSProperties["cp_Parametros"] = "CW=" + codigoWorkflowUltimaVersao + "&CI=" + auxCodigoInstanciaWf + "&CE=" + codigoEtapaWf + "&CS=1" + chamaTelaMenu;
        }
    }

    private string montaTituloInstancia(int? codigoProjeto, int codigoWorkflow)
    {
        DataSet ds;
        DataRow dr;
        string titulo = "";
        string comandoSQL = string.Format(@"
            SELECT f.[NomeFluxo]
	            FROM
		            [dbo].[Workflows]				AS [wf]
				        INNER JOIN [dbo].[Fluxos]	AS [f]	ON 
					        ( f.[CodigoFluxo] = wf.[CodigoFluxo] )
	            WHERE
				        wf.[CodigoWorkflow]	= {0} ", codigoWorkflow);
        try
        {
            ds = cDados.getDataSet(comandoSQL);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                dr = ds.Tables[0].Rows[0];
                titulo = dr["NomeFluxo"].ToString();
            }
        }
        catch
        {
            titulo = "";
        }
        if (codigoProjeto != null)
        {
            comandoSQL = string.Format(@"
            SELECT 
		            p.[NomeProjeto]
	            FROM
		            [dbo].[Projeto]					AS [p]
	            WHERE
				        p.CodigoProjeto		= {0}", codigoProjeto);
            try
            {
                ds = cDados.getDataSet(comandoSQL);
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    dr = ds.Tables[0].Rows[0];
                    titulo = titulo + " - " + dr["NomeProjeto"].ToString();
                }
            }
            catch
            {
            }
        }
        return titulo;
    }

    private void defineInclusaoNovaInstancia(ref string funcaoJS)
    {
        if (codigoFluxo.HasValue)
        {
            bool confirmaInstancia = false;

            string comandoSQL = string.Format(@"
        SELECT ISNULL(CriaInstanciaAutomatica, 'N') AS CriaInstanciaAutomatico
          FROM Fluxos
         WHERE CodigoFluxo = {0}", codigoFluxo);

            DataSet dsInst = cDados.getDataSet(comandoSQL);

            if (cDados.DataSetOk(dsInst) && cDados.DataTableOk(dsInst.Tables[0]))
                confirmaInstancia = dsInst.Tables[0].Rows[0]["CriaInstanciaAutomatico"].ToString().ToUpper().Trim() == "S";

            if (confirmaInstancia)
            {
                funcaoJS = string.Format(@"
            if(confirm(Resources.traducao.VisualizacaoGrid_ao_confirmar_esta_op__o__ser__automaticamente_criado_um_novo_processo_para_este_fluxo__confirma_))
                callbackCriaInstancia.PerformCallback();");
            }
        }
    }

    protected void popupMenu_Init(object sender, EventArgs e)
    {
        string funcaoJS = @" e.processOnServer = false;
                            lpLoading.Show();
                            callbackCriaInstancia.PerformCallback(e.item.name);";

        popupMenu.ClientSideEvents.ItemClick = String.Format(@"function(s, e){{ 
	
                {0}
        }}", funcaoJS);
    }



}