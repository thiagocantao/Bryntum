using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Data.PivotGrid;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPrinting;
using Olap = DevExpress.Web.ASPxPivotGrid;
using DevExpress.Web.ASPxPivotGrid;
using System.Collections.Generic;
using DevExpress.Data.Filtering;

public partial class _Processos_Visualizacao_VisualizacaoOlap : System.Web.UI.Page
{
    #region Constants

    const string CONST_CodigoFluxo = "CF";
    const string CONST_CodigoProjeto = "CP";
    const string CONST_CodigoWorkflow = "CW";
    const string CONST_CodigoInstanciaWF = "CI";
    const string CONST_CodigoEtapaAtual = "CE";
    const string CONST_OcorrenciaAtual = "CS";

    #endregion

    #region Fields

    dados cDados;
    DsListaProcessos ds;
    DsListaProcessos.ListaDataTable dtLista;
    int codigoLista;
    int codigoListaUsuario;
    string dbName;
    string dbOwner;
    int codigoUsuarioLogado;
    int codigoEntidade;
    int codigoCarteira;
    /*int? codigoProjeto;
	int? codigoFluxo;
	int? codigoWorkflow;
	int? codigoInstancia;
	int? codigoEtapa;
	int? ocorrenciaAtual;*/
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

    #region Events Handlers

    protected void Page_Init(object sender, EventArgs e)
    {
        string cl = Request.QueryString["cl"];
        if (cl == null) return;
        codigoLista = int.Parse(Request.QueryString["cl"]);

        string commandSql = "select CodigoLista,  NomeLista, GrupoPermissao, ItemPermissao, IniciaisPermissao, TituloLista,IndicaOpcaoDisponivel, TipoLista, CodigoEntidade,  indicaVisibilidadeExterna from lista where codigoLista = @CodigoLista";
        FillData(dtLista = new DsListaProcessos.ListaDataTable(), commandSql);

        if (dtLista[0].indicaVisibilidadeExterna.Equals("S") && cDados.getInfoSistema("IDUsuarioLogado") == null)
        {
            menu.Visible = false;
            menu.Enabled = false;
            menu.Load -= menu_Load;
        }

        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();

        cDados = CdadosUtil.GetCdados(null);

        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null && !dtLista[0].indicaVisibilidadeExterna.Equals("S"))
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = String.Format(
                "{0}erros/erroInatividade.aspx", cDados.getPathSistema());
            Response.End();
        }

        
        if (!int.TryParse(Request.QueryString["clu"], out codigoListaUsuario))
            codigoListaUsuario = -1;
        cDados = CdadosUtil.GetCdados(null);
        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        codigoUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        codigoCarteira = Convert.ToInt32(cDados.getInfoSistema("CodigoCarteira"));
        ds = new DsListaProcessos();

        //menu.Items.FindByName("titulo").Text = Request.QueryString["titulo"];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        

        InitData();
        if (!(IsPostBack || IsPostBack))
        {
            CDIS_PivotGridLocalizer.Activate();
            DefinirConfiguracaoToolbar();

        }
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
                iMenu.ToolTip = "";// "Filtro master";
                iMenu.Text = "Filtro master";
                iMenu.Image.Url = lista.InstrucaoPreFiltroDados.Equals("") ? "~/imagens/filtroDisponivel.png" : "~/imagens/FiltroAplicado.png";
            }
            else
            {
                iMenu.Enabled = false;
                iMenu.ToolTip = "Personalização do filtro desabilitada";
                iMenu.Text = "Filtro master";
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

    protected void pgDados_CustomCellDisplayText(object sender, Olap.PivotCellDisplayTextEventArgs e)
    {
        //string prefixo = "fieldBLT_";
        //if (e.DataField != null && e.DataField.Name.Contains(prefixo))
        //{
        //    if (e.Value != null)
        //        e.DisplayText = string.Format("<img alt='' src='../../imagens/{0}Menor.gif' />", e.Value);
        //}
    }

    protected void pgDados_CustomSummary(object sender, Olap.PivotGridCustomSummaryEventArgs e)
    {
        string prefixo = "fieldBLT_";
        if (e.DataField.Name.StartsWith(prefixo))
        {
            if (e.SummaryValue.Count <= 1)
            {
                object cor = e.CreateDrillDownDataSource()[0][e.DataField];
                e.CustomValue = (cor ?? "branco").ToString().Trim();
            }
            else
            {
                e.CustomValue = "vazio";
            }
        }
    }

    protected void exporter_CustomExportCell(object sender, WebCustomExportCellEventArgs e)
    {
        if (e.DataField != null)
        {
            if (e.DataField.Name.StartsWith("fieldBLT_") && (e.Value != null))
            {
                e.Appearance.Font = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                string value = e.Value.ToString().ToLower();
                e.Brick.Text = "l";
                e.Brick.TextValue = "l";
                switch (value)
                {
                    case "vermelho":
                        e.Appearance.ForeColor = Color.Red;
                        break;
                    case "amarelo":
                        e.Appearance.ForeColor = Color.Yellow;
                        break;
                    case "verde":
                        e.Appearance.ForeColor = Color.Green;
                        break;
                    case "azul":
                        e.Appearance.ForeColor = Color.Blue;
                        break;
                    case "branco":
                        e.Appearance.ForeColor = Color.WhiteSmoke;
                        break;
                    case "laranja":
                        e.Appearance.ForeColor = Color.Orange;
                        break;
                    default:
                        e.Brick.Text = " ";
                        e.Brick.TextValue = " ";
                        break;
                }
            }
            else
                e.Appearance.Font = new Font("Verdana", 8, FontStyle.Regular, GraphicsUnit.Point);
        }
    }

    protected void exporter_CustomExportHeader(object sender, WebCustomExportHeaderEventArgs e)
    {
        e.Appearance.Font = new Font("Verdana", 8, FontStyle.Regular, GraphicsUnit.Point);
    }

    protected void exporter_CustomExportFieldValue(object sender, WebCustomExportFieldValueEventArgs e)
    {
        e.Appearance.Font = new Font("Verdana", 8, FontStyle.Regular, GraphicsUnit.Point);

        if (e.Field != null && e.Value != null)
        {
            if (e.Field.Name.StartsWith("fieldBLT_"))
            {
                if (e.Value.ToString().Equals("Vermelho"))
                {
                    e.Appearance.Font = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
                    e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    e.Brick.Text = "l";
                    e.Brick.TextValue = "l";
                    e.Appearance.ForeColor = Color.Red;
                }
                else if (e.Value.ToString().Equals("Amarelo"))
                {
                    e.Appearance.Font = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
                    e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    e.Brick.Text = "l";
                    e.Brick.TextValue = "l";
                    e.Appearance.ForeColor = Color.Yellow;
                }
                else if (e.Value.ToString().Equals("Verde"))
                {
                    e.Appearance.Font = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
                    e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    e.Brick.Text = "l";
                    e.Brick.TextValue = "l";
                    e.Appearance.ForeColor = Color.Green;
                }
                else if (e.Value.ToString().Equals("Azul"))
                {
                    e.Appearance.Font = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
                    e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    e.Brick.Text = "l";
                    e.Brick.TextValue = "l";
                    e.Appearance.ForeColor = Color.Blue;
                }
                else if (e.Value.ToString().Equals("Branco"))
                {
                    e.Appearance.Font = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
                    e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    e.Brick.Text = "l";
                    e.Brick.TextValue = "l";
                    e.Appearance.ForeColor = Color.LightGray;
                }
                else if (e.Value.ToString().Equals("Laranja"))
                {
                    e.Appearance.Font = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
                    e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    e.Brick.Text = "l";
                    e.Brick.TextValue = "l";
                    e.Appearance.ForeColor = Color.Orange;
                }
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
        pflt.[InstrucaoPreFiltroDados], 
		pflt.[InstrucaoPreFiltroOriginal], 
        pflt.[IndicaPreFiltroCustomizavel],
        l.indicaVisibilidadeExterna
   FROM Lista AS l 
	LEFT JOIN ListaUsuario lu ON (lu.CodigoLista = l.CodigoLista AND lu.CodigoUsuario = {0} AND lu.IndicaListaPadrao = 'S') 
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
        pflt.[IndicaPreFiltroCustomizavel],
        l.indicaVisibilidadeExterna
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

        DsListaProcessos.ListaRow item = ds.Lista.Single();

        SetGridSettings(item);


        List<String> lst = new List<String>();

        lst.AddRange(pgDados.GetFieldList());

        if (!RelatorioDinamico.validaFilterExpression(item.InstrucaoPreFiltroDados, lst))
        {
            item.InstrucaoPreFiltroDados = String.Empty;
        }
    }

    private void SetGridSettings(DsListaProcessos.ListaRow item)
    {
        bool possuiPaginacao = VerificaVerdadeiroOuFalso(item.IndicaPaginacao);
        pgDados.OptionsPager.Visible = possuiPaginacao;
        if (possuiPaginacao)
            pgDados.OptionsPager.RowsPerPage = item.QuantidadeItensPaginacao;
        if (pgDados.Fields.Count == 0)
            foreach (var campo in item.GetListaCampoRows())
                AddPivotGridFileds(campo);
        SetDefaultSessionInfo();
        dataSource.SelectCommand = ReplaceParameters(item.ComandoSelect);
        dataSource.ConnectionString = ConnectionString;
        dataSource.SetSelectParameters();

        if (item.FiltroAplicado != null && item.FiltroAplicado.Contains("¥¥") && !IsPostBack)
        {
            aplicaFiltroColunas(item.FiltroAplicado);
        }
    }

    private void SetDefaultSessionInfo()
    {
        var data = new Dictionary<string, object>();
        data.Add("pa_CodigoEntidadeContexto", codigoEntidade);
        data.Add("pa_CodigoUsuarioSistema", codigoUsuarioLogado);
        DataExtensions.SetSessionInfo(data);
    }

    private void aplicaFiltroColunas(string filtroExp)
    {
        string[] campos = filtroExp.Split(new string[] { "¥¥" }, StringSplitOptions.None);

        foreach (string campo in campos)
        {
            if (campo != "")
            {
                string fieldName = campo.Split(new string[] { "$$" }, StringSplitOptions.None)[0];
                string[] valores = campo.Substring(campo.IndexOf("$$") + 2).Split(new string[] { "##" }, StringSplitOptions.None);

                // Locks the control to prevent excessive updates when multiple properties are modified.
                pgDados.BeginUpdate();
                try
                {
                    pgDados.Fields[fieldName].FilterValues.Clear();

                    foreach (string valor in valores)
                    {
                        if (valor != "")
                        {
                            pgDados.Fields[fieldName].FilterValues.Add(valor);
                        }
                    }
                    pgDados.Fields[fieldName].FilterValues.FilterType = DevExpress.XtraPivotGrid.PivotFilterType.Excluded;
                }
                finally
                {
                    // Unlocks the control.
                    pgDados.EndUpdate();
                }
            }
        }
    }

    private void AddPivotGridFileds(DsListaProcessos.ListaCampoRow campo)
    {
        Olap.PivotGridField field = new Olap.PivotGridField();
        field.FieldName = campo.NomeCampo;
        if (VerificaVerdadeiroOuFalso(campo.IndicaCampoControle))
        {
            field.Visible = false;
            field.Options.ShowInCustomizationForm = false;
            string iniciais = campo.IniciaisCampoControlado;
            if (!string.IsNullOrEmpty(iniciais))
                field.Name = string.Format("fieldCC_{0}", iniciais);
        }
        else
        {
            if (campo.TipoCampo.ToUpper().Equals("BLT"))
            {
                field.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                field.ValueStyle.HorizontalAlign = HorizontalAlign.Center;
                field.Name = string.Format("fieldBLT_{0}#{1}"
                    , campo.NomeCampo, campo.CodigoCampo);
                field.SummaryType = PivotSummaryType.Custom;
            }
            else
            {
                field.Name = string.Format("field#{0}", campo.CodigoCampo);
                //field.UseNativeFormat = DefaultBoolean.True;
            }
            field.Area = GetPivotArea(campo.AreaDefault);
            field.AreaIndex = campo.OrdemCampo;
            field.AllowedAreas = GetAllowedAreas(campo);
            field.Caption = campo.TituloCampo;
            if (!string.IsNullOrEmpty(campo.Formato))
            {
                field.CellFormat.FormatType = GetFormatType(campo.TipoCampo);
                field.CellFormat.FormatString = campo.Formato;
                //Incluído por Amauri em 07/11/2013 para resolver a formatação de colunas Monetário na área de linhas
                field.ValueFormat.FormatType = GetFormatType(campo.TipoCampo);
                field.ValueFormat.FormatString = campo.Formato;
            }
            if (campo.TipoTotalizador.ToLower() == "nenhum")
            {
                field.Options.ShowCustomTotals = false;
                field.Options.ShowGrandTotal = false;
                field.Options.ShowTotals = false;
            }

            //Faz a SOMA, MÉDIA E CONTAGEM DEPENDENDO DE COMO FOI DESENHADO NAS CONFIGURAÇÕES DE RELATÓRIO
            switch (campo.TipoTotalizador.ToUpper())
            {
                case "CONTAR":
                    field.SummaryType = PivotSummaryType.Count;
                    break;
                case "MÉDIA":
                    field.SummaryType = PivotSummaryType.Average;
                    break;
                case "MEDIA":
                    field.SummaryType = PivotSummaryType.Average;
                    break;
                case "SOMA":
                    field.SummaryType = PivotSummaryType.Sum;
                    break;
                default:
                    field.Options.ShowCustomTotals = false;
                    field.Options.ShowGrandTotal = false;
                    field.Options.ShowTotals = false;
                    break;
            }

            field.Visible = VerificaVerdadeiroOuFalso(campo.IndicaCampoVisivel);
        }
        pgDados.Fields.Add(field);
    }

    private static FormatType GetFormatType(string tipoCampo)
    {
        /*
		 * - NUM
		 * - TXT
		 * - DAT
		 * - VAR
		 * - MON (Valor monetário)
		 * - PER (Valor percentual)
		 */
        switch (tipoCampo.ToUpper())
        {
            case "NUM":
            case "MON":
            case "PER":
                return FormatType.Numeric;
            case "DAT":
                return FormatType.DateTime;
            case "VAR":
                return FormatType.Custom;
            default:
                return FormatType.None;
        }
    }

    private PivotArea GetPivotArea(string strPivotArea)
    {
        switch (strPivotArea.ToUpper())
        {
            case "D":
                return PivotArea.DataArea;
            case "L":
                return PivotArea.RowArea;
            case "C":
                return PivotArea.ColumnArea;
            case "F":
                return PivotArea.FilterArea;
            default:
                throw new ArgumentException(Resources.traducao.VisualizacaoOlap_a__rea_informada_n_o___v_lida_);
        }
    }

    private PivotGridAllowedAreas GetAllowedAreas(DsListaProcessos.ListaCampoRow campo)
    {
        bool indicaAreaDados = VerificaVerdadeiroOuFalso(campo.IndicaAreaDado);
        bool indicaAreaLinha = VerificaVerdadeiroOuFalso(campo.IndicaAreaLinha);
        bool indicaAreaColuna = VerificaVerdadeiroOuFalso(campo.IndicaAreaColuna);
        bool indicaAreaFiltro = VerificaVerdadeiroOuFalso(campo.IndicaAreaFiltro);
        int valorAreaDado = (int)PivotGridAllowedAreas.DataArea;
        int valorAreaLinha = (int)PivotGridAllowedAreas.RowArea;
        int valorAreaColuna = (int)PivotGridAllowedAreas.ColumnArea;
        int valorAreaFiltro = (int)PivotGridAllowedAreas.FilterArea;
        const int CONST_Zero = 0;
        int valorAreasPermitidas =
            (indicaAreaDados ? valorAreaDado : CONST_Zero) |
            (indicaAreaLinha ? valorAreaLinha : CONST_Zero) |
            (indicaAreaColuna ? valorAreaColuna : CONST_Zero) |
            (indicaAreaFiltro ? valorAreaFiltro : CONST_Zero);
        PivotGridAllowedAreas allowedAreas = (PivotGridAllowedAreas)valorAreasPermitidas;
        return allowedAreas;
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
            .Replace("{2}", /*codigoProjeto.HasValue ? codigoProjeto.ToString() :*/ valorNaoDefinido)
            .Replace("{3}", codigoEntidade.ToString())
            .Replace("{4}", codigoUsuarioLogado.ToString())
            .Replace("{5}", /*codigoFluxo.HasValue ? codigoFluxo.ToString() :*/ valorNaoDefinido)
            .Replace("{6}", /*codigoWorkflow.HasValue ? codigoWorkflow.ToString() :*/ valorNaoDefinido)
            .Replace("{7}", /*codigoInstancia.HasValue ? codigoInstancia.ToString() :*/ valorNaoDefinido)
            .Replace("{8}", /*codigoEtapa.HasValue ? codigoEtapa.ToString() :*/ valorNaoDefinido)
            .Replace("{9}", /*ocorrenciaAtual.HasValue ? ocorrenciaAtual.ToString() :*/ valorNaoDefinido)
            .Replace("{10}", codigoCarteira.ToString());

        return comandoPreencheLista;
    }

    private void FillData(DataTable dt, string comandoSql)
    {
        SqlDataAdapter da = new SqlDataAdapter(comandoSql, ConnectionString);
        SqlParameter p = da.SelectCommand.Parameters.Add("@CodigoLista", SqlDbType.Int);

        p.Value = codigoLista;
        da.Fill(dt);
    }

    private void DefineDimensoesTela()
    {
        string res = cDados.getInfoSistema("ResolucaoCliente").ToString();
        larguraTela = int.Parse(res.Split('x')[0]);
        alturaTela = int.Parse(res.Split('x')[1]);
    }

    private bool SalvarConsulta()
    {
        int qtdeItensPagina = pgDados.OptionsPager.RowsPerPage;
        string filter = cDados.getFilter(pgDados);
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
	SET @FiltroAplicado = '{4}'
	
IF @CodigoListaUsuario = -1
 SELECT @CodigoListaUsuario = [CodigoListaUsuario] 
   FROM [ListaUsuario] 
  WHERE [CodigoLista] = @CodigoLista 
	AND [CodigoUsuario] = @CodigoUsuario 
	AND [IndicaListaPadrao] = 'S'

 UPDATE [ListaUsuario]
	SET [QuantidadeItensPaginacao] = @QuantidadeItensPaginacao,
        FiltroAplicado = @FiltroAplicado
  WHERE [CodigoListaUsuario] = @CodigoListaUsuario"
    , codigoLista, codigoUsuarioLogado, qtdeItensPagina, codigoListaUsuario, filter.Replace("'", "''"));

        #endregion

        foreach (Olap.PivotGridField field in pgDados.Fields.OfType<Olap.PivotGridField>().Where(f => f.CanShowInCustomizationForm))
        {
            int ordemCampo = field.AreaIndex;
            int ordemAgrupamentoCampo = field.GroupIndex;
            string areaDefault = ObtemInicialArea(field.Area); ;
            string indicaCampoVisivel = field.Visible ? "S" : "N";
            int codigoCampo = ObtemCodigoCampo(field);

            #region Comando SQL

            comandoSql.AppendFormat(@"
    SET @CodigoCampo = {0}
    SET @OrdemCampo = {1}
    SET @OrdemAgrupamentoCampo = {2}
    SET @AreaDefault = '{3}'
    SET @IndicaCampoVisivel = '{4}'

IF EXISTS(SELECT 1 FROM [ListaCampoUsuario] WHERE [CodigoListaUsuario] = @CodigoListaUsuario AND [CodigoCampo] = @CodigoCampo)
 UPDATE [ListaCampoUsuario]
	SET [OrdemCampo] = @OrdemCampo
       ,[OrdemAgrupamentoCampo] = @OrdemAgrupamentoCampo
       ,[AreaDefault] = @AreaDefault
       ,[IndicaCampoVisivel] = @IndicaCampoVisivel
  WHERE [CodigoListaUsuario] = @CodigoListaUsuario
    AND [CodigoCampo] = @CodigoCampo
ELSE IF EXISTS(SELECT 1 FROM [ListaUsuario] WHERE [CodigoListaUsuario] = @CodigoListaUsuario)
 INSERT INTO [ListaCampoUsuario](
        [CodigoCampo],
        [CodigoListaUsuario],
        [OrdemCampo],
        [OrdemAgrupamentoCampo],
        [AreaDefault],
        [IndicaCampoVisivel])
  VALUES(
        @CodigoCampo,
        @CodigoListaUsuario,
        @OrdemCampo,
        @OrdemAgrupamentoCampo,
        @AreaDefault,
        @IndicaCampoVisivel)",
     codigoCampo,
     ordemCampo,
     ordemAgrupamentoCampo,
     areaDefault,
     indicaCampoVisivel);
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
        int qtdeItensPagina = pgDados.OptionsPager.RowsPerPage;
        StringBuilder comandoSql = new StringBuilder();
        string filter = cDados.getFilter(pgDados);
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
	SET @FiltroAplicado = '{4}'
	SET @InstrucaoPreFiltroDados = '{5}'
	SET @InstrucaoPreFiltroDadosOriginal = '{6}'
	SET @IndicaListaPadrao = (SELECT CASE WHEN EXISTS(SELECT 1 FROM ListaUsuario WHERE CodigoUsuario = @CodigoUsuario AND CodigoLista = @CodigoLista) THEN 'N' ELSE 'S' END)

 INSERT INTO ListaUsuario(CodigoUsuario, CodigoLista, QuantidadeItensPaginacao, NomeListaUsuario, IndicaListaPadrao, FiltroAplicado, InstrucaoPreFiltroDados, InstrucaoPreFiltroDadosOriginal) 
 VALUES (@CodigoUsuario, @CodigoLista, @QuantidadeItensPaginacao, @NomeListaUsuario, @IndicaListaPadrao, @FiltroAplicado, @InstrucaoPreFiltroDados, @InstrucaoPreFiltroDadosOriginal)

	SET @CodigoListaUsuario = SCOPE_IDENTITY()"
    , codigoLista, codigoUsuarioLogado, qtdeItensPagina, nomeListaUsuario, filter.Replace("'", "''")
    , item.InstrucaoPreFiltroDados.Replace("'", "''"), item.InstrucaoPreFiltroOriginal.Replace("'", "''"));

        #endregion

        foreach (Olap.PivotGridField field in pgDados.Fields.OfType<Olap.PivotGridField>().Where(f => f.CanShowInCustomizationForm))
        {
            int ordemCampo = field.AreaIndex;
            int ordemAgrupamentoCampo = field.GroupIndex;
            string areaDefault = ObtemInicialArea(field.Area);
            string indicaCampoVisivel = field.Visible ? "S" : "N";
            int codigoCampo = ObtemCodigoCampo(field);

            #region Comando SQL

            comandoSql.AppendFormat(@"
    SET @CodigoCampo = {0}
    SET @OrdemCampo = {1}
    SET @OrdemAgrupamentoCampo = {2}
    SET @AreaDefault = '{3}'
    SET @IndicaCampoVisivel = '{4}'

    INSERT INTO ListaCampoUsuario(
           CodigoCampo,
           CodigoListaUsuario,
           OrdemCampo,
           OrdemAgrupamentoCampo,
           AreaDefault,
           IndicaCampoVisivel)
     VALUES(
           @CodigoCampo,
           @CodigoListaUsuario,
           @OrdemCampo,
           @OrdemAgrupamentoCampo,
           @AreaDefault,
           @IndicaCampoVisivel)",
     codigoCampo,
     ordemCampo,
     ordemAgrupamentoCampo,
     areaDefault,
     indicaCampoVisivel);
            comandoSql.AppendLine();

            #endregion
        }
        comandoSql.AppendLine();
        comandoSql.AppendLine("SELECT @CodigoListaUsuario AS CodigoListaUsuario");
        DataSet ds2 = cDados.getDataSet(comandoSql.ToString());
        return ds2.Tables[0].Rows[0]["CodigoListaUsuario"].ToString();
    }

    private string getFilter()
    {
        string filterExp = "";

        foreach (DevExpress.Web.ASPxPivotGrid.PivotGridField field in pgDados.Fields)
        {
            if (field.FilterValues.Count > 0)
            {
                filterExp += field.FieldName + "$$";

                foreach (object valores in field.FilterValues.ValuesExcluded)
                {
                    if (valores != null && valores.ToString() != "")
                        filterExp += valores + "##";
                }

                filterExp += "¥¥";
            }
        }

        return filterExp;
    }

    private string ObtemInicialArea(PivotArea area)
    {
        switch (area)
        {
            case PivotArea.ColumnArea:
                return "C";
            case PivotArea.DataArea:
                return "D";
            case PivotArea.FilterArea:
                return "F";
            case PivotArea.RowArea:
                return "L";
            default:
                throw new Exception();
        }
    }

    private int ObtemCodigoCampo(Olap.PivotGridField field)
    {
        int codigoCampo = -1;
        string fieldName = field.Name;
        int indiceCodigoCampo = fieldName.LastIndexOf('#') + 1;
        if (indiceCodigoCampo > 0)
            codigoCampo = int.Parse(fieldName.Substring(indiceCodigoCampo));

        return codigoCampo;
    }

    #endregion

    #region Classes Auxiliares

    public class HeaderTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            Olap.PivotGridHeaderTemplateContainer c =
                (Olap.PivotGridHeaderTemplateContainer)container;
            Olap.PivotGridHeaderHtmlTable table = c.CreateHeader();
            switch (c.Field.ID)
            {
                case "fieldImgExportacao":
                    table.Content = new ImagemExportacao();
                    table.BorderStyle = BorderStyle.None;
                    break;
                case "fieldImgSalvarLayout":
                    table.Content = new ImagemSalvarLayout();
                    table.BorderStyle = BorderStyle.None;
                    break;
                case "fieldImgRestaurarLayout":
                    table.Content = new ImagemRestaurarLayout();
                    table.BorderStyle = BorderStyle.None;
                    break;
            }
            c.Controls.Add(table);
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

    #region Eventos Menu Botões Inserção e Exportação+

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = "";

        switch (e.Item.Image.IconID)
        {
            case "export_exporttoxls_16x16gray":
                parameter = "XLS";
                break;
            case "export_exporttopdf_16x16gray":
                parameter = "PDF";
                break;
            case "export_exporttortf_16x16gray":
                parameter = "RTF";
                break;
            case "export_exporttocsv_16x16gray":
                parameter = "CSV";
                break;
            default:
                parameter = "XLSX";
                break;
        }

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        if (e.Item.Image.Url != "~/imagens/botoes/btnDownload.png")
        {
            cDados.exportaOlap(exporter, parameter, this);
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
        menu.Items.FindByName("btnLayout").Text = item.NomeListaUsuario;

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
            btnExportar.Image.IconID = "export_exporttoxls_16x16gray";
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

    if(e.item.name === 'titulo') return;

	if(e.item.name == 'btnIncluir'){{ShowMenu(s, e);}}
	else if(e.item.name == 'btnAbrirConsultas'){{parent.ExibeConsultaSalvas({0}, {1});}}
	else if(e.item.name == 'btnSalvarConsultas'){{if(indicaPossuiListaUsuario == 'S'){{SalvarConsulta();}}else{{parent.ExibirJanelaSalvarComo();}}}}
	else if(e.item.name == 'btnSalvarComoConsultas'){{parent.ExibirJanelaSalvarComo();}}
	else if(e.item.name == 'btnCerregarOriginalConsultas'){{CarregarConsulta(0);}}
	else if(e.item.name == 'btnFilterEditor'){{window.parent.showModalComFooter('FilterEditorPopup.aspx?cl={0}&clu={3}', 'Filtro dos dados', 300, 400, CarregarConsulta, null);;}}
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

    protected void pgDados_HtmlFieldValuePrepared(object sender, PivotHtmlFieldValuePreparedEventArgs e)
    {
        if (e.ValueType == PivotGridValueType.Value && e.Field != null && e.Field.ID.Contains("BLT"))
        {
            string value = e.Value as string;
            if (!(string.IsNullOrWhiteSpace(value)))
                e.Cell.Text = string.Format("<img alt='' src='../../imagens/{0}Menor.gif' />", value);
        }
    }

    protected void pgDados_HtmlCellPrepared(object sender, PivotHtmlCellPreparedEventArgs e)
    {
        if (e.DataField != null && e.DataField.ID.Contains("BLT"))
        {
            string value = e.Value as string;
            if (!(string.IsNullOrWhiteSpace(value)))
                e.Cell.Text = string.Format("<img alt='' src='../../imagens/{0}Menor.gif' />", value);
        }
    }

    protected void pgDados_Init(object sender, EventArgs e)
    {
        
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        if (cDados.getInfoSistema("IDUsuarioLogado") != null) {
            string res = cDados.getInfoSistema("ResolucaoCliente").ToString();
            larguraTela = int.Parse(res.Split('x')[0]);
            alturaTela = int.Parse(res.Split('x')[1]);
            alturaTela = alturaTela - 200;
            //((ASPxPivotGrid)sender).Width = new Unit(larguraTela.ToString() + "px");
            ((ASPxPivotGrid)sender).Height = new Unit(alturaTela.ToString() + "px");
        }else
            ((ASPxPivotGrid)sender).Height = new Unit("20" + "px");
    }

    protected void pgDados_CustomFilterPopupItems(object sender, Olap.PivotCustomFilterPopupItemsEventArgs e)
    {
        PivotGridFieldFilterValues filterValues = ((ASPxPivotGrid)sender).Fields[e.Field.FieldName].FilterValues;
        e.Items.ToList().ForEach(x =>
        {
            x.IsChecked = true;
            filterValues.ValuesExcluded.ToList().ForEach(y =>
            {
                if (y.ToString() == x.ToString())
                {
                    x.IsChecked = false;
                }
            });
        });
    }
}