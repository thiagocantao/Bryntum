using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using DevExpress.Utils;
using System.Data.SqlClient;
using DevExpress.XtraPrinting;
using System.Text;
using DevExpress.Data;

public partial class _Estrategias_mapa_FatorChave : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";

    #region Campos para funcionalidade personalização da lista

    DsListaProcessos dsLP;
    int codigoLista;

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
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        defineAlturaTela(resolucaoCliente);

        #region Instruções para funcionalidade personalização da lista
        codigoLista = getCodigoListaProjetosMapa();
        dsLP = new DsListaProcessos();

        if (!IsPostBack)
            InitData(); // preenche a dsLP com as listas de campos 
        #endregion

        //cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "EN", "EN_AcsLstPrjME"); 

        if (!IsPostBack)
        {
            bool bPodeIncluirProjetos;

            /// se houver algum unidade em que o usuário possa incluir projetos
            bPodeIncluirProjetos = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "UN", "UN_IncPrj");

            imgIncluirProjeto.ClientVisible = bPodeIncluirProjetos;
        }

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ListaProjetosMapa.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../../Content/styles.css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../../Content/sprite.css"" />"));

        carregaComboMapas();
        carregaListaProjetos();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "LPMP", "EST", -1, Resources.traducao.adicionar_aos_favoritos);
        }
    }
    
    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 166);

        gvDados.Settings.VerticalScrollableHeight = alturaPrincipal - 140;
    }

    private void carregaComboMapas()
    {
        DataSet dsMapas = cDados.getMapasUsuarioEntidade(codigoEntidadeUsuarioResponsavel.ToString(), codigoUsuarioResponsavel, "");

        if (cDados.DataSetOk(dsMapas))
        {
            ddlMapa.DataSource = dsMapas;
            ddlMapa.TextField = "TituloMapaEstrategico";
            ddlMapa.ValueField = "CodigoMapaEstrategico";
            ddlMapa.DataBind();

            if (!IsPostBack && ddlMapa.Items.Count > 0)
            {
                int codigoMapaSelecionado = -1;

                if (Request.QueryString["CM"] != null && Request.QueryString["CM"].ToString() != "")
                    codigoMapaSelecionado = int.Parse(Request.QueryString["CM"].ToString());

                if (codigoMapaSelecionado != -1 && ddlMapa.Items.FindByValue(codigoMapaSelecionado) != null)
                    ddlMapa.Value = codigoMapaSelecionado;
                else
                {
                    DataSet dsParametro = cDados.getMapaDefaultUsuario(codigoUsuarioResponsavel, "");

                    if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
                    {
                        string codigoMapa = dsParametro.Tables[0].Rows[0]["CodigoMapaEstrategicoPadrao"] + "";

                        if (codigoMapa != "" && ddlMapa.Items.FindByValue(codigoMapa) != null)
                            ddlMapa.Value = codigoMapa;
                        else
                            ddlMapa.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlMapa.SelectedIndex = 0;
                    }
                }
            }
        }
    }

    private void carregaListaProjetos()
    {
        int codigoMapa = ddlMapa.SelectedIndex == -1 ? -1 : int.Parse(ddlMapa.Value.ToString());

        string where = "";

        if (txtPesquisa.Text.Trim() != "")
            where += string.Format(@"AND (NomeProjeto LIKE '%{0}%' OR FatorChave LIKE '%{0}%' OR Tema LIKE '%{0}%' OR ObjetivoEstrategico LIKE '%{0}%' OR AcaoTransformadora LIKE '%{0}%' OR Unidade LIKE '%{0}%' OR GerenteUnidade LIKE '%{0}%' OR GerenteProjeto LIKE '%{0}%' OR StatusProjeto LIKE '%{0}%')", txtPesquisa.Text);

        DataSet dsProjetos = cDados.getListaProjetosMapa(codigoMapa, codigoUsuarioResponsavel, where);

        if (cDados.DataSetOk(dsProjetos))
        {
            gvDados.DataSource = dsProjetos;
            gvDados.DataBind();

            if (!IsPostBack)
                gvDados.ExpandAll();
        }
    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "R")
        {
            InitData();
        }
        gvDados.ExpandAll();
    }

    #region GRAVAÇÃO e RECUPERAÇÃO DE LAYOUT

    private int getCodigoListaProjetosMapa()
    {
        string comandoSQL;
        int codLista = -1;

        comandoSQL = string.Format("SELECT l.[CodigoLista] FROM {0}.{1}.[Lista] AS [l] WHERE l.[CodigoEntidade] = {2} AND l.[IniciaisListaControladaSistema] = 'LstPrjMap' ", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if ((cDados.DataSetOk(ds)) && (cDados.DataTableOk(ds.Tables[0])))
            codLista = int.Parse(ds.Tables[0].Rows[0]["CodigoLista"].ToString());

        return codLista;
    }

    private void InitData()
    {
        string comandoPreencheLista;
        string comandoPreencheListaCampo;
        string comandoPreencheListaFluxo;

        #region Comandos SQL

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
        lu.FiltroAplicado,
		IndicaOpcaoDisponivel, 
		TipoLista, 
		URL, 
		CodigoEntidade,
        CodigoModuloMenu,
        IndicaListaZebrada
   FROM Lista l left join ListaUsuario lu ON (lu.CodigoLista = l.CodigoLista AND lu.CodigoUsuario = {0}) 
  WHERE (l.CodigoLista = @CodigoLista)", codigoUsuarioResponsavel);

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
        ISNULL(lcu.LarguraColuna, lc.LarguraColuna) AS LarguraColuna
   FROM ListaCampo AS lc LEFT JOIN
		ListaUsuario lu ON lu.CodigoLista = lc.CodigoLista AND 
						   lu.CodigoUsuario = {0} LEFT JOIN
        ListaCampoUsuario lcu ON lc.CodigoCampo = lcu.CodigoCampo AND 
                                 lcu.CodigoListaUsuario = lu.CodigoListaUsuario
  WHERE (lc.CodigoLista = @CodigoLista)
  ORDER BY
        ISNULL(lcu.OrdemCampo, lc.OrdemCampo)", codigoUsuarioResponsavel);

        comandoPreencheListaFluxo = @"
 SELECT CodigoLista, 
		CodigoFluxo, 
		TituloMenu
   FROM ListaFluxo
  WHERE (CodigoLista = @CodigoLista)";

        #endregion

        FillData(dsLP.Lista, comandoPreencheLista);
        FillData(dsLP.ListaCampo, comandoPreencheListaCampo);
        FillData(dsLP.ListaFluxo, comandoPreencheListaFluxo);

        DsListaProcessos.ListaRow item = dsLP.Lista.Single();

        SetGridSettings(item);
    }

    private void FillData(DataTable dt, string comandoSql)
    {
        SqlDataAdapter da = new SqlDataAdapter(comandoSql, ConnectionString);
        SqlParameter p = da.SelectCommand.Parameters.Add("@CodigoLista", SqlDbType.Int);
        p.Value = codigoLista;
        da.Fill(dt);
    }

    private void SetGridSettings(DsListaProcessos.ListaRow item)
    {
        foreach (var campo in item.GetListaCampoRows())
            AddGridColumn(campo);

        bool possuiPaginacao = VerificaVerdadeiroOuFalso(item.IndicaPaginacao);
        gvDados.SettingsPager.Visible = possuiPaginacao;

        if (possuiPaginacao)
            gvDados.SettingsPager.PageSize = item.QuantidadeItensPaginacao;
        
       //gvDados.FilterExpression = item.FiltroAplicado;
    }

    private void AddGridColumn(DsListaProcessos.ListaCampoRow campo)
    {
        GridViewDataTextColumn colTxt = (GridViewDataTextColumn)gvDados.Columns[campo.NomeCampo];

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
            colTxt.GroupIndex = campo.OrdemAgrupamentoCampo;
            colTxt.Name = string.Format("col#{0}", campo.CodigoCampo);
            colTxt.Caption = campo.TituloCampo;
            colTxt.VisibleIndex = campo.OrdemCampo;
            colTxt.Visible = VerificaVerdadeiroOuFalso(campo.IndicaCampoVisivel);
            colTxt.Settings.AllowGroup = campo.IndicaAgrupamento == "S" ? DefaultBoolean.True : DefaultBoolean.False;
            colTxt.PropertiesTextEdit.DisplayFormatString = campo.Formato;
            //if (!campo.IsLarguraColunaNull())
            //    colTxt.Width = new Unit(campo.LarguraColuna, UnitType.Pixel);

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

            #endregion

            #region Tipo totalizados

            string nomeCampo = campo.NomeCampo;
            string tipoTotalizador = campo.TipoTotalizador.ToUpper();
            SummaryItemType summaryType = GetSummaryType(tipoTotalizador);
            ASPxSummaryItem summaryItem = new ASPxSummaryItem(nomeCampo, summaryType);
            summaryItem.DisplayFormat = campo.Formato;
            gvDados.GroupSummary.Add(summaryItem);
            gvDados.TotalSummary.Add(summaryItem);

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

    private bool VerificaVerdadeiroOuFalso(string valor)
    {
        if (valor == null)
            return false;
        return valor.ToLower().Equals("s");
    }

    private void SalvarCustomizacaoLayout()
    {
        StringBuilder comandoSql = new StringBuilder();
        int registrosAfetados = 0;

        #region Atualizando a tabela ListaUsuario

        int qtdeItensPagina;
        string filtroAplicado;
        qtdeItensPagina = gvDados.SettingsPager.PageSize;
        filtroAplicado = gvDados.FilterExpression;

        comandoSql.Append(@"
            DECLARE @CodigoLista Int,
                    @CodigoUsuario Int,
                    @QuantidadeItensPaginacao SmallInt,
                    @FiltroAplicado VARCHAR(MAX),
                    @CodigoListaUsuario BIGINT");

        comandoSql.AppendLine();

        comandoSql.AppendFormat(@"
            SET @CodigoLista = {0}
            SET @CodigoUsuario = {1}
            SET @QuantidadeItensPaginacao = {2}
            SET @FiltroAplicado = '{3}'

            SELECT TOP 1 @CodigoListaUsuario = CodigoListaUsuario 
              FROM ListaUsuario AS lu 
             WHERE lu.CodigoLista = @CodigoLista 
               AND lu.CodigoUsuario = @CodigoUsuario

             IF @CodigoListaUsuario IS NOT NULL
                BEGIN
                     UPDATE ListaUsuario
                        SET QuantidadeItensPaginacao = @QuantidadeItensPaginacao,
			                FiltroAplicado = @FiltroAplicado
                      WHERE CodigoListaUsuario = @CodigoListaUsuario
                END
                ELSE
                BEGIN
                     INSERT INTO ListaUsuario(CodigoUsuario, CodigoLista, QuantidadeItensPaginacao, FiltroAplicado, IndicaListaPadrao) VALUES (@CodigoUsuario, @CodigoLista, @QuantidadeItensPaginacao, @FiltroAplicado, 'S')
   
                        SET @CodigoListaUsuario = SCOPE_IDENTITY()
                END"
            , codigoLista, codigoUsuarioResponsavel, qtdeItensPagina, filtroAplicado.Replace("'", "''"));

        comandoSql.AppendLine();

        cDados.execSQL(comandoSql.ToString(), ref registrosAfetados);
        #endregion

        comandoSql.Clear();

        comandoSql = new StringBuilder(@"
DECLARE @CodigoUsuario INT,
        @CodigoCampo INT,
        @OrdemCampo SMALLINT,
        @OrdemAgrupamentoCampo SMALLINT,
        @AreaDefault CHAR(1),
        @IndicaCampoVisivel CHAR(1),
        @LarguraColuna SMALLINT");
        comandoSql.AppendLine();
        foreach (GridViewEditDataColumn col in gvDados.AllColumns.Where(c => c.ShowInCustomizationForm))
        {
            int ordemCampo = col.VisibleIndex;
            int ordemAgrupamentoCampo = col.GroupIndex;
            string areaDefault = "L";
            string indicaCampoVisivel = col.Visible ? "S" : "N";
            int codigoCampo = ObtemCodigoCampo(col);
            double larguraColuna = col.Width.IsEmpty ? 100 : col.Width.Value;

            #region Comando SQL

            comandoSql.AppendFormat(@"
    SET @CodigoUsuario = {0}
    SET @CodigoCampo = {1}
    SET @OrdemCampo = {2}
    SET @OrdemAgrupamentoCampo = {3}
    SET @AreaDefault = '{4}'
    SET @IndicaCampoVisivel = '{5}'
    SET @LarguraColuna = {6}

 IF EXISTS(SELECT 1 FROM ListaCampoUsuario AS lcu WHERE lcu.CodigoCampo = @CodigoCampo AND lcu.CodigoListaUsuario = @CodigoListaUsuario)
BEGIN
     UPDATE ListaCampoUsuario
        SET OrdemCampo = @OrdemCampo,
            OrdemAgrupamentoCampo = @OrdemAgrupamentoCampo,
            AreaDefault = @AreaDefault,
            IndicaCampoVisivel = @IndicaCampoVisivel,
            LarguraColuna = @LarguraColuna
      WHERE CodigoCampo = @CodigoCampo
        AND CodigoListaUsuario = @CodigoListaUsuario
END
ELSE
BEGIN
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
            @LarguraColuna)
END",
     codigoUsuarioResponsavel,
     codigoCampo,
     ordemCampo,
     ordemAgrupamentoCampo,
     areaDefault,
     indicaCampoVisivel,
     larguraColuna);
            comandoSql.AppendLine();

            #endregion
        }

        cDados.execSQL(comandoSql.ToString(), ref registrosAfetados);
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

    private void RestaurarLayout()
    {
        StringBuilder comandoSql = new StringBuilder(@"
DECLARE @CodigoUsuario INT,
        @CodigoCampo INT");
        comandoSql.AppendLine();
        foreach (GridViewEditDataColumn col in gvDados.AllColumns.Where(c => c.ShowInCustomizationForm))
        {
            int codigoCampo = ObtemCodigoCampo(col);

            #region Comando SQL

            comandoSql.AppendFormat(@"
    SET @CodigoUsuario = {0}
    SET @CodigoCampo = {1}

 DELETE 
   FROM ListaCampoUsuario
  WHERE CodigoListaUsuario IN(SELECT lu.CodigoListaUsuario FROM ListaUsuario lu WHERE lu.CodigoUsuario = @CodigoUsuario AND lu.CodigoLista = {2})
    AND CodigoCampo = @CodigoCampo",
                   codigoUsuarioResponsavel,
                   codigoCampo,
                   codigoLista);
            comandoSql.AppendLine();

            #endregion
        }

        comandoSql.AppendFormat(@"   
     DELETE FROM ListaUsuario
       WHERE CodigoLista = @CodigoLista
         AND CodigoUsuario = @CodigoUsuario");
        comandoSql.AppendLine();

        int registrosAfetados = 0;
        cDados.execSQL(comandoSql.ToString(), ref registrosAfetados);        
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string parameter = (e.Parameter ?? string.Empty).ToLower();
        switch (parameter)
        {
            case "save_layout":
                SalvarCustomizacaoLayout();
                break;
            case "restore_layout":
                RestaurarLayout();
                break;
        }
    }

    #endregion

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
    }

    protected void ASPxGridViewExporter1_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
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
        if (e.Column == gvDados.Columns["CorDesempenho"]  && (e.Value != null))
        {
            e.BrickStyle.Font =
                new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
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
    }

    public string getDescricao()
    {
        string descricaoProjeto = Eval("NomeProjeto").ToString();

        if (Eval("PodeAcessarProjeto").ToString() == "S")
        {
            string linkProjeto = Eval("IndicaProjetoCarteiraPrioritaria").ToString() == "S" ? "indexResumoProjeto" : "cni_ResumoProjeto";

            descricaoProjeto = string.Format(@"<a class='LinkGrid' target='_top' href='../../_Projetos/DadosProjeto/" + linkProjeto + ".aspx?IDProjeto=" + Eval("CodigoProjeto") + "&NomeProjeto=" + Eval("NomeProjeto") + "&NivelNavegacao=2'>" + Eval("NomeProjeto") + "</a>");
        }

        string table = string.Format(@"<table>
                                            <tr>                                                
                                                <td>{0}</td>
                                            </tr>
                                       </table>", descricaoProjeto);

        return table;
    }
}
