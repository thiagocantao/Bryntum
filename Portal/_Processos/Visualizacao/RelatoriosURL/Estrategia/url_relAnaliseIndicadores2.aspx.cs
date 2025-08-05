using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Text;
using DevExpress.Web;
using DevExpress.Data.PivotGrid;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.XtraPivotGrid;
using DevExpress.Utils;
using System.Collections.Generic;
using DevExpress.XtraPrinting;
using Olap = DevExpress.Web.ASPxPivotGrid;

public partial class _Processos_Visualizacao_RelatoriosURL_Estrategia_url_relAnaliseIndicadores2 : System.Web.UI.Page
{
    #region Fields

    dados cDados;

    private string dbName;
    private string dbOwner;

    int codigoUsuarioLogado;
    int codigoEntidade;

    public string alturaTabela = "";
    public string larguraTabela = "";

    object metaAtual = new object();
    object resultadoAtual = new object();

    System.Globalization.CultureInfo wCultureInfo;
    private List<string> Meses = new List<string>();

    #region Campos para funcionalidade personalização da lista

    DsListaProcessos dsLP;
    int codigoLista;

    #endregion

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

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuarioLogado, codigoEntidade, codigoEntidade, "null", "EN", 0, "null", "EN_EstRelInd");
        }

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        #region Instruções para funcionalidade personalização da lista
        codigoLista = GetCodigoListaAnaliseIndicador2(codigoEntidade);
        dsLP = new DsListaProcessos();
        InitData(); // preenche a dsLP com as listas de campos 
        #endregion
                
        cDados.aplicaEstiloVisual(this);

        if (!(IsPostBack || IsCallback))
        {
            //DefineDimensoesTela();
            CDIS_PivotGridLocalizer.Activate();
            rbOpcao.Value = "M";
        }

        setMeses();

        if (!IsCallback)
        {
            ajustaTelaSelecaoFiltro();
        }

        // variável usada para formatar dinamicamente os valores a serem mostrados na grid.
        wCultureInfo = new System.Globalization.CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.LCID);
        carregaFiltros();

        populaGrid(false);
        defineAlturaTela();

        cDados.configuraPainelBotoesOLAP(tbBotoes);
    }

    #region funções para funcionalidade personalização da lista
    private int GetCodigoListaAnaliseIndicador2(int codigoEntidade)
    {
        string comandoSQL;
        int codLista = -1;

        comandoSQL = string.Format("SELECT l.[CodigoLista] FROM {0}.{1}.[Lista] AS [l] WHERE l.[CodigoEntidade] = {2} AND l.[IniciaisListaControladaSistema] = 'AnlIndic2' ", dbName, dbOwner, codigoEntidade);

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
		IndicaOpcaoDisponivel, 
		TipoLista, 
		URL, 
		CodigoEntidade,
        CodigoModuloMenu,
        IndicaListaZebrada
   FROM Lista l left join ListaUsuario lu ON (lu.CodigoLista = l.CodigoLista AND lu.CodigoUsuario = {0}) 
  WHERE (l.CodigoLista = @CodigoLista)", codigoUsuarioLogado);

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
        ISNULL(lcu.OrdemCampo, lc.OrdemCampo)", codigoUsuarioLogado);

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
        if (pvgDadosIndicador.Fields.Count == 0)
            foreach (var campo in item.GetListaCampoRows())
                AddPivotGridFileds(campo);

        if (!(IsPostBack || IsCallback))
        {
            bool possuiPaginacao = VerificaVerdadeiroOuFalso(item.IndicaPaginacao);
            pvgDadosIndicador.OptionsPager.Visible = possuiPaginacao;
            if (possuiPaginacao)
                pvgDadosIndicador.OptionsPager.RowsPerPage = item.QuantidadeItensPaginacao;
        }

        //dataSource.ConnectionString = ConnectionString;                                // retirados os comandos que preenchem os dados
        //dataSource.SelectCommand = ReplaceParameters(item.ComandoSelect);
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
                field.Name = string.Format("fieldBLT_{0}", campo.NomeCampo); // alterando a nomenclatura por conta dos eventos da tela.
                field.SummaryType = PivotSummaryType.Custom;
            }
            else
            {
                field.Name = string.Format("field_{0}", campo.NomeCampo); // alterando para usar o nome como identificador do campo por conta dos eventos de tratamento.
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
            }
            field.Visible = VerificaVerdadeiroOuFalso(campo.IndicaCampoVisivel);

            #region Personalizações da tela Análise de Indicadores

            if (field.FieldName.Equals("Unidade") || field.FieldName.Equals("Ano") || field.FieldName.Equals("Indicador") || field.FieldName.Equals("MapaEstrategico"))
            {
                field.Options.AllowDrag = DefaultBoolean.False;
            }
            if (field.FieldName.Equals("MesPorExtenso"))
            {
                field.SortMode = PivotSortMode.Custom;
            }
            if (field.FieldName.Equals("Meta") || field.FieldName.Equals("Resultado") || field.FieldName.Equals("DesempNum"))
            {
                field.SummaryType = PivotSummaryType.Custom;
                field.Options.ShowCustomTotals = false;
                field.Options.ShowGrandTotal = false;
            }

            #endregion
        }
        pvgDadosIndicador.Fields.Add(field);
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
                throw new ArgumentException("A área informada não é válida.");
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

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string parameter = (e.Parameter ?? string.Empty).ToLower();
        switch (parameter)
        {
            case "export":
                MemoryStream stream = new MemoryStream();

                XlsxExportOptionsEx options = new XlsxExportOptionsEx();

                options.TextExportMode = TextExportMode.Value;
                options.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                exporter.ExportToXlsx(stream, options);
                Session["exportStream"] = stream;
                break;
            case "save_layout":
                SalvarCustomizacaoLayout();
                break;
            case "restore_layout":
                RestaurarLayout();
                break;
        }
    }

    private void SalvarCustomizacaoLayout()
    {
        string filter = getFilter();

        StringBuilder comandoSql = new StringBuilder(@"
DECLARE @CodigoLista Int,
        @CodigoUsuario INT,
        @CodigoCampo INT,
        @NomeCampo Varchar(255),
        @OrdemCampo SMALLINT,
        @OrdemAgrupamentoCampo SMALLINT,
        @AreaDefault CHAR(1),
        @IndicaCampoVisivel CHAR(1),
        @FiltroAplicado VARCHAR(max),
        @CodigoListaUsuario BIGINT");
        comandoSql.AppendLine();
        comandoSql.AppendFormat(@"
	        SET @CodigoLista = {0}
	        SET @CodigoUsuario = {1}
	        SET @FiltroAplicado = '{2}'
            
         SELECT TOP 1 @CodigoListaUsuario = CodigoListaUsuario 
           FROM ListaUsuario AS lu 
          WHERE lu.CodigoLista = @CodigoLista 
            AND lu.CodigoUsuario = @CodigoUsuario

        IF @CodigoListaUsuario IS NOT NULL
        BEGIN
             UPDATE ListaUsuario
                SET FiltroAplicado = @FiltroAplicado
              WHERE CodigoListaUsuario = @CodigoListaUsuario
        END
        ELSE
        BEGIN
             INSERT INTO ListaUsuario(CodigoUsuario, CodigoLista, FiltroAplicado, IndicaListaPadrao) VALUES (@CodigoUsuario, @CodigoLista, @FiltroAplicado, 'S')
   
                SET @CodigoListaUsuario = SCOPE_IDENTITY()
        END"
     , codigoLista, codigoUsuarioLogado, filter.Replace("'", "''"));

        foreach (Olap.PivotGridField field in pvgDadosIndicador.Fields.OfType<Olap.PivotGridField>().Where(f => f.CanShowInCustomizationForm))
        {
            int ordemCampo = field.AreaIndex;
            int ordemAgrupamentoCampo = -1;
            string areaDefault = ObtemInicialArea(field.Area);
            string indicaCampoVisivel = field.Visible ? "S" : "N";
            string nomeCampo = field.FieldName;

            #region Comando SQL

            comandoSql.AppendFormat(@"
    SET @CodigoUsuario = {0}
    SET @NomeCampo = '{1}'
    SELECT @CodigoCampo = lc.[CodigoCampo] FROM ListaCampo lc WHERE lc.[CodigoLista] = @CodigoLista AND lc.[NomeCampo] = @NomeCampo;
    SET @OrdemCampo = {2}
    SET @OrdemAgrupamentoCampo = {3}
    SET @AreaDefault = '{4}'
    SET @IndicaCampoVisivel = '{5}'

 IF EXISTS(SELECT 1 FROM ListaCampoUsuario AS lcu WHERE lcu.CodigoCampo = @CodigoCampo AND lcu.CodigoListaUsuario = @CodigoListaUsuario)
BEGIN
     UPDATE ListaCampoUsuario
        SET OrdemCampo = @OrdemCampo,
            OrdemAgrupamentoCampo = @OrdemAgrupamentoCampo,
            AreaDefault = @AreaDefault,
            IndicaCampoVisivel = @IndicaCampoVisivel
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
            IndicaCampoVisivel)
     VALUES(
            @CodigoCampo,
            @CodigoListaUsuario,
            @OrdemCampo,
            @OrdemAgrupamentoCampo,
            @AreaDefault,
            @IndicaCampoVisivel)
END",
 codigoUsuarioLogado,
 nomeCampo.Replace("'", "''"),
 ordemCampo,
 ordemAgrupamentoCampo,
 areaDefault,
 indicaCampoVisivel);
            comandoSql.AppendLine();

            #endregion
        }
        int registrosAfetados = 0;
        cDados.execSQL(comandoSql.ToString(), ref registrosAfetados);
    }

    private string getFilter()
    {
        string filterExp = "";

        foreach (DevExpress.Web.ASPxPivotGrid.PivotGridField field in pvgDadosIndicador.Fields)
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

    private void RestaurarLayout()
    {
        StringBuilder comandoSql = new StringBuilder(@"
DECLARE @CodigoUsuario INT,
        @NomeCampo Varchar(255),
        @CodigoCampo INT,
        @CodigoLista Int");
        comandoSql.AppendLine();
        foreach (Olap.PivotGridField field in pvgDadosIndicador.Fields.OfType<Olap.PivotGridField>().Where(f => f.CanShowInCustomizationForm))
        {
            string nomeCampo = field.FieldName;

            #region Comando SQL

            comandoSql.AppendFormat(@"
    SET @CodigoUsuario = {0}
    SET @NomeCampo = '{1}'
    SET @CodigoLista = {2}
    SELECT @CodigoCampo = lc.[CodigoCampo] FROM ListaCampo lc WHERE lc.[CodigoLista] = @CodigoLista AND lc.[NomeCampo] = @NomeCampo;
 DELETE 
   FROM ListaCampoUsuario
  WHERE CodigoListaUsuario IN(SELECT lu.CodigoListaUsuario FROM ListaUsuario lu WHERE lu.CodigoUsuario = @CodigoUsuario AND lu.CodigoLista = @CodigoLista)
    AND CodigoCampo = @CodigoCampo",
                   codigoUsuarioLogado,
                   nomeCampo.Replace("'", "''"),
                   codigoLista);
            comandoSql.AppendLine();

            #endregion
        }

        comandoSql.AppendFormat(@"   
     DELETE FROM ListaUsuario
       WHERE CodigoLista = {0}
         AND CodigoUsuario = @CodigoUsuario", codigoLista);
        comandoSql.AppendLine();

        int registrosAfetados = 0;
        cDados.execSQL(comandoSql.ToString(), ref registrosAfetados);

    }

    #endregion

    private void setMeses()
    {
        Meses.Clear();
        Meses.Add("Jan");
        Meses.Add("Fev");
        Meses.Add("Mar");
        Meses.Add("Abr");
        Meses.Add("Mai");
        Meses.Add("Jun");
        Meses.Add("Jul");
        Meses.Add("Ago");
        Meses.Add("Set");
        Meses.Add("Out");
        Meses.Add("Nov");
        Meses.Add("Dez");
    }

    private void ajustaTelaSelecaoFiltro()
    {
        int nextIndex = 0;
        Olap.PivotGridField fldPerspectiva = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_Perspectiva");
        Olap.PivotGridField fldObjetivo = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_Objetivo");
        Olap.PivotGridField fldUnidade = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_Unidade");
        Olap.PivotGridField fldIndicador = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_Indicador");
        Olap.PivotGridField fldMapa = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_MapaEstrategico");

        if (fldPerspectiva.Visible)
            nextIndex++;
        if (fldObjetivo.Visible)
            nextIndex++;

        if (rbOpcao.Value.ToString() == "M")
        {
            ddlMapa.ClientVisible = true;
            ddlIndicador.ClientVisible = false;
            ddlUnidades.ClientVisible = false;
            fldUnidade.AreaIndex = nextIndex+2;
            fldIndicador.AreaIndex = nextIndex+1;
            fldMapa.AreaIndex = 0;
        }
        else if (rbOpcao.Value.ToString() == "U")
        {
            ddlMapa.ClientVisible = false;
            ddlIndicador.ClientVisible = false;
            ddlUnidades.ClientVisible = true;
            fldIndicador.AreaIndex = nextIndex+2;
            fldMapa.AreaIndex = 1;
            fldUnidade.AreaIndex = 0;
        }
        else if (rbOpcao.Value.ToString() == "I")
        {
            ddlMapa.ClientVisible = false;
            ddlIndicador.ClientVisible = true;
            ddlUnidades.ClientVisible = false;
            fldMapa.AreaIndex = 2;
            fldUnidade.AreaIndex = 1;
            fldIndicador.AreaIndex = 0;
        }

        if (fldPerspectiva.Visible)
            fldPerspectiva.AreaIndex = fldMapa.AreaIndex + 1;

        if (fldObjetivo.Visible)
            fldObjetivo.AreaIndex = fldPerspectiva.AreaIndex + 1;
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        Div2.Style.Add("height", (alturaPrincipal - 250) + "px");//a div vai ficar com essa altura
        Div2.Style.Add("width", "100%");

    }
        
    private void populaGrid(bool lerXML)
    {
        DataSet ds = new DataSet();

        //fldMeta.Options.ShowValues = !fldMes.Visible;
        //fldDesempNum.Options.ShowValues = !fldMes.Visible;

        if (!lerXML)//se não for postback então escreve xml
        {
            string nomeArquivo = "OLAP_IND2_" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioLogado + ".xml";

            int? codIndic = null;
            int? codUnid = null;
            int? codMapa = null;

            if (ddlIndicador.SelectedIndex != -1 && rbOpcao.Value.ToString() == "I")
            {
                 codIndic = int.Parse(ddlIndicador.Value.ToString());
            }

            if (ddlUnidades.SelectedIndex != -1 && rbOpcao.Value.ToString() == "U")
            {
                codUnid = int.Parse(ddlUnidades.Value.ToString());                
            }

            if (ddlMapa.SelectedIndex != -1 && rbOpcao.Value.ToString() == "M")
            {
                codMapa = int.Parse(ddlMapa.Value.ToString());
            }

            ds = cDados.getDadosOLAP_AnaliseIndicador2(codigoUsuarioLogado, codIndic, codUnid, codMapa, codigoEntidade);

            StreamWriter strWriter;

            hfGeral.Set("NomeArquivoXML", nomeArquivo);

            strWriter = new StreamWriter(HostingEnvironment.ApplicationPhysicalPath + "ArquivosTemporarios\\" + nomeArquivo, false, System.Text.Encoding.UTF8);
            strWriter.Close();

            ds.WriteXml(HostingEnvironment.ApplicationPhysicalPath + "ArquivosTemporarios\\" + hfGeral.Get("NomeArquivoXML"), XmlWriteMode.WriteSchema);
           
        }
        else
        {
            ds.ReadXml(HostingEnvironment.ApplicationPhysicalPath + "ArquivosTemporarios\\" + hfGeral.Get("NomeArquivoXML"));

        }

        if (cDados.DataSetOk(ds))
        {
            Olap.PivotGridField fldMeta = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_Meta");
            Olap.PivotGridField fldResultado = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_Resultado");
            Olap.PivotGridField fldStatus = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("fieldBLT_Desempenho");
            Olap.PivotGridField fldDesempNum = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_DesempNum");
            if (checkAcumulado.Checked)
            {
                fldMeta.FieldName = "MetaAcumulada";
                fldResultado.FieldName = "ResultadoAcumulado";
                fldStatus.FieldName = "DesempenhoAcumulado";
                fldDesempNum.FieldName = "DesempNumAcumulado";
                fldMeta.Caption = "Meta Acumulada";
                fldResultado.Caption = "Resultado Acumulado";
                fldStatus.Caption = "Status";
                fldDesempNum.Caption = "Desempenho";
                //fldMeta.Options.ShowValues = true;
            }
            else
            {
                fldMeta.FieldName = "Meta";
                fldResultado.FieldName = "Resultado";
                fldStatus.FieldName = "Desempenho";
                fldDesempNum.FieldName = "DesempNum";
                fldMeta.Caption = "Meta";
                fldResultado.Caption = "Resultado";
                fldStatus.Caption = "Status";
                fldDesempNum.Caption = "Desempenho";
            }
        }
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            pvgDadosIndicador.DataSource = ds.Tables[0];
            pvgDadosIndicador.DataBind();
        }
        else
        {
            pvgDadosIndicador.DataSource = null;
            pvgDadosIndicador.DataBind();
        }
        
    }

    #region --- [Pivot Grid]

    protected void pvgDadosIndicador_CustomFieldSort(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomFieldSortEventArgs e)
    {
        Olap.PivotGridField fldMes = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_MesPorExtenso");
        // se estiver ordenando a coluna 'Mes' 
        if (e.Field == fldMes)
        {
            object valor1 = Meses.IndexOf(e.Value1.ToString());
            object valor2 = Meses.IndexOf(e.Value2.ToString());
            e.Result = System.Collections.Comparer.Default.Compare(valor1, valor2);
            e.Handled = true;
        } // if (e.Field == fldMes)
    }

    protected void pvgDadosIndicador_CustomCellDisplayText(object sender, Olap.PivotCellDisplayTextEventArgs e)
    {
        Olap.PivotGridField fldMeta = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_Meta");
        Olap.PivotGridField fldResultado = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_Resultado");
        Olap.PivotGridField fldStatus = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("fieldBLT_Desempenho");
        Olap.PivotGridField fldDesempNum = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_DesempNum");
        double valor = 0;
        if (e.DataField == fldDesempNum)
        {
            if (null == e.Value)
                e.DisplayText = "";
            else
            {
                wCultureInfo.NumberFormat.PercentDecimalDigits = 0;
                double.TryParse(e.Value.ToString(), out valor);
                valor /= 100.00;
                e.DisplayText = valor.ToString("P", wCultureInfo);
            }
        }
        else if ((e.DataField == fldResultado) || (e.DataField == fldMeta))
        {
            DevExpress.XtraPivotGrid.PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
            ASPxPivotGrid grid = (ASPxPivotGrid)sender;
            DataTable dt = (DataTable)grid.DataSource;
            int Decimais = 0;

            if ((dt != null) && (ds != null))
            {
                foreach (DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow in ds)
                {
                    if ((summaryRow.ListSourceRowIndex >= 0) && (dt.Rows.Count >= summaryRow.ListSourceRowIndex))
                    {
                        DataRow dataRow = dt.Rows[summaryRow.ListSourceRowIndex];

                        int.TryParse(dataRow["Decimais"].ToString(), out Decimais);

                        if (null == e.Value)
                            e.DisplayText = "";
                        else
                        {
                            double.TryParse(e.Value.ToString(), out valor);
                            e.DisplayText = formataValorGrid(valor, dataRow["Medida"].ToString(), Decimais);
                        }
                    }
                    break;
                }
            }
        }
        else if (e.DataField == fldStatus)
        {
            if (e.Value != null)
                e.DisplayText = "<img alt='' src='../../../../imagens/" + e.Value.ToString() + "Menor.gif' />";
        }
    }

    private string formataValorGrid(double valor, string medida, int casasDecimais)
    {
        string numeroFormatado = "";
        wCultureInfo.NumberFormat.CurrencyDecimalDigits = casasDecimais;
        wCultureInfo.NumberFormat.NumberDecimalDigits = casasDecimais;
        wCultureInfo.NumberFormat.PercentDecimalDigits = casasDecimais;

        if (medida.Equals("%"))
        {
            valor /= 100.00;
            numeroFormatado = valor.ToString("P", wCultureInfo);
        }
        else if (medida.Equals("Nº"))
            numeroFormatado = valor.ToString("N", wCultureInfo);
        else if (medida.Equals("R$"))
            numeroFormatado = valor.ToString("C", wCultureInfo);
        else
        {
            numeroFormatado = valor.ToString("N", wCultureInfo);
            numeroFormatado = medida + " " + numeroFormatado;
        }

        return numeroFormatado;
    }

    protected void pvgDadosIndicador_CustomSummary(object sender, Olap.PivotGridCustomSummaryEventArgs e)
    {
        // como no relatório não há "grand totais" para colunas, retorna caso esteja tentando calcular algum desses
        if (e.ColumnField == null)
            return;

        Olap.PivotGridField fldMes = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_MesPorExtenso");
        Olap.PivotGridField fldAno = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_Ano");
        Olap.PivotGridField fldMeta = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_Meta");
        Olap.PivotGridField fldStatus = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("fieldBLT_Desempenho");
        Olap.PivotGridField fldDesempNum = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_DesempNum");
        string campoAObter;
        DataRow dataRow;

        ASPxPivotGrid grid = (ASPxPivotGrid)sender;
        List<Olap.PivotGridField> rowFields = grid.GetFieldsByArea(DevExpress.XtraPivotGrid.PivotArea.RowArea);

        DevExpress.XtraPivotGrid.PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
        DataTable dt = (DataTable)grid.DataSource;

        // se o cálculo for para o campo de desempenho numérico, calcula-se a média de 'atingimento das metas?????????' (exigência FNDE!!!!)
        if (e.DataField == fldDesempNum)
        {
            Hashtable combinacoes = new Hashtable();
            string chave;
            object conteudoCampo;
            if (e.ColumnField == fldAno)
                campoAObter = "DesempNumRefAno";
            else if (e.ColumnField == fldMes)
            {
                if (checkAcumulado.Checked)
                    campoAObter = "DesempNumAcumulado";
                else
                    campoAObter = "DesempNum";
            }
            else
                campoAObter = "";

            if (campoAObter != "")
            {
                for (int i = 0; i < ds.RowCount; i++)
                {
                    DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow = ds[i];
                    if ((summaryRow.ListSourceRowIndex >= 0) && (dt.Rows.Count >= summaryRow.ListSourceRowIndex))
                    {
                        chave = string.Empty;
                        foreach (Olap.PivotGridField pvField in rowFields)
                        {

                            conteudoCampo = summaryRow[pvField];
                            if ((conteudoCampo == null) || (conteudoCampo == DBNull.Value))
                                chave += "-1;";
                            else
                                chave += conteudoCampo.ToString() + ";";
                        }

                        if ((chave != string.Empty) && (!combinacoes.ContainsKey(chave)))
                        {
                            dataRow = dt.Rows[summaryRow.ListSourceRowIndex];
                            conteudoCampo = dataRow[campoAObter];
                            if ((conteudoCampo != null) && (conteudoCampo != DBNull.Value))
                            {
                                combinacoes[chave] = double.Parse(conteudoCampo.ToString().Replace('.', ','));
                            }
                        }
                    } // if ((summaryRow.ListSourceRowIndex >= 0) && ...
                    if (combinacoes.Count > 0)
                    {
                        double media = Enumerable.Average(combinacoes.Values.Cast<double>());
                        e.CustomValue = media;
                    }
                } // for (int i = 0; i < ds.RowCount; i++)
            }
        }// if (e.DataField == fldDesempNum)
        else if (e.RowField == rowFields[rowFields.Count - 1])
        {
            // se estivermos calculando os demais campos (meta, resultado, status), só são calculados para o último campo 
            // da seção "RowArea"

            // se a soma que estiver sendo feita for para a COLUNA [Ano], então o valor a mostrar 
            // como total será o campo já calculado para o ano
            if (e.ColumnField == fldAno)
            {
                if (e.DataField == fldMeta)
                    campoAObter = "MetaRefAno";
                else if (e.DataField == fldStatus)
                    campoAObter = "DesempenhoRefAno";
                else if (e.DataField == fldDesempNum)
                    campoAObter = "DesempNumRefAno";
                else
                    campoAObter = "ResultadoRefAno";

                for (int i = 0; i < ds.RowCount; i++)
                {
                    DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow = ds[i];
                    if ((summaryRow.ListSourceRowIndex >= 0) && (dt.Rows.Count >= summaryRow.ListSourceRowIndex))
                    {
                        dataRow = dt.Rows[summaryRow.ListSourceRowIndex];
                        if (campoAObter == "DesempenhoRefAno")
                        {
                            e.CustomValue = dataRow[campoAObter];
                        }
                        else
                        {
                            try
                            {
                                if (dataRow[campoAObter] + "" != "")
                                    e.CustomValue = dataRow[campoAObter].ToString().Replace('.', ',');
                            }
                            catch { }
                        }
                        break; // busca uma linha já que será igual para todas as linhas, uma vez que unidade está presente
                    } // if ((summaryRow.ListSourceRowIndex >= 0) && ...
                } // foreach summaryRow in ds

            }// if (e.ColumnField == fldAno)
            else if (e.ColumnField == fldMes)
            {   // quando se tratar do campo mês, pega-se o maior valor, já que por definição teremos um único valor, já que não se pode retirar 
                // o campo indicador da seção "RowArea"
                if (e.SummaryValue.Max != null)
                    e.CustomValue = e.SummaryValue.Max.ToString().Replace('.', ',');
            }
        } // else if (e.RowField == rowFields[rowFields.Count - 1])

    }

    protected void pvgDadosIndicador_FieldVisibleChanged(object sender, Olap.PivotFieldEventArgs e)
    {
        Olap.PivotGridField fldAno = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_Ano");
        Olap.PivotGridField fldUnidade = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_Unidade");
        Olap.PivotGridField fldIndicador = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_Indicador");
        Olap.PivotGridField fldMapa = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_MapaEstrategico");

        if (e.Field == fldAno)
        {
            e.Field.Visible = true;
            e.Field.AreaIndex = 0;
        }
        else if ((e.Field == fldUnidade) || (e.Field == fldIndicador) || (e.Field == fldMapa))
            e.Field.Visible = true;
        //else if (e.Field == fldMes) // quando 'incluindo' o campo Período na tela, esconde o campo Meta para o período.
        //{
        //    fldMeta.Options.ShowValues = !e.Field.Visible;
        //    fldDesempNum.Options.ShowValues = !e.Field.Visible;
        //}
    }

    #endregion

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
    }

    private void carregaFiltros()
    {
        DataSet dsFiltro = new DataSet();

        dsFiltro = cDados.getUnidadesIndicadoresAcessoUsuario(codigoUsuarioLogado, codigoEntidade, "");
        ddlUnidades.DataSource = dsFiltro;
        ddlUnidades.TextField = "NomeUnidadeNegocio";
        ddlUnidades.ValueField = "CodigoUnidadeNegocio";
        ddlUnidades.DataBind();

        dsFiltro = cDados.getIndicadoresAcessoUsuario(codigoUsuarioLogado, codigoEntidade, "");
        ddlIndicador.DataSource = dsFiltro;
        ddlIndicador.TextField = "NomeIndicador";
        ddlIndicador.ValueField = "CodigoIndicador";
        ddlIndicador.DataBind();

        populaMapaEstrategico();

        if (!IsPostBack)
        {
            if (ddlIndicador.Items.Count > 0)
                ddlIndicador.SelectedIndex = 0;

            if (ddlUnidades.Items.Count > 0)
            {
                if (ddlUnidades.Items.FindByValue(codigoEntidade.ToString()) != null)
                    ddlUnidades.Value = codigoEntidade.ToString();
                else
                    ddlUnidades.SelectedIndex = 0;
            }

        }
    }
    
    private void populaMapaEstrategico()
    {
        //DataSet dsMapas = cDados.getMapasUsuarioEntidade(cDados.getInfoSistema("CodigoEntidade").ToString(), idUsuarioLogado, "");
        string where = " AND un.CodigoEntidade = " + codigoEntidade.ToString();
        DataSet dsMapas = cDados.getMapasUsuarioEntidade(codigoEntidade.ToString(), codigoUsuarioLogado, where);

        if (cDados.DataSetOk(dsMapas) && cDados.DataSetOk(dsMapas))
        {
            ddlMapa.DataSource = dsMapas;
            ddlMapa.TextField = "TituloMapaEstrategico";
            ddlMapa.ValueField = "CodigoMapaEstrategico";
            ddlMapa.DataBind();

            ddlMapa.Items.Insert(0, new ListEditItem("", -1));

            if (!IsPostBack)
            {
                //todo: Testar a função de verificar mapa estrategico padrão.
                DataSet ds = cDados.getMapaDefaultUsuario(codigoUsuarioLogado, "");
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    int codMapa = int.Parse(ds.Tables[0].Rows[0]["CodigoMapaEstrategicoPadrao"].ToString());
                    string nomeMapa = ds.Tables[0].Rows[0]["TituloMapaEstrategico"].ToString();

                    int codigoMapaPadraoBanco = 0;
                    cDados.getAcessoPadraoMapaEstrategico(codigoUsuarioLogado, codigoEntidade, codMapa, ref codigoMapaPadraoBanco);

                    if (codMapa == codigoMapaPadraoBanco)
                    {
                        //ddlMapa.Value = codigoMapaPadraoBanco.ToString();
                        ListEditItem li = ddlMapa.Items.FindByValue(codigoMapaPadraoBanco.ToString());
                        if (li != null)
                            ddlMapa.SelectedIndex = li.Index;

                        hfGeral.Set("CodigoMapaSelecionado", codMapa);
                        hfGeral.Set("NomeMapaSelecionado", nomeMapa);
                    }
                    else
                    {
                        ddlMapa.SelectedIndex = 0;
                        //ddlMapa.SelectedIndex = 0;
                    }
                }
            }

            //ddlMapa.Value = hfGeral.Contains("CodigoMapaSelecionado") ? int.Parse(hfGeral.Get("CodigoMapaSelecionado").ToString()) : -1;
            //if (hfGeral.Contains("CodigoMapaSelecionado"))
            //{
            //    ListEditItem li = ddlMapa.Items.FindByValue((hfGeral.Get("CodigoMapaSelecionado").ToString()));
            //    if (li != null)
            //    {
            //        ddlMapa.SelectedIndex = li.Index;
            //    }
            //}
            //else
            //{
            //    ddlMapa.Items.Insert(0, new ListEditItem("Nenhum Mapa Selecionado", -1));
            //    ddlMapa.SelectedIndex = 0;
            //}
        }
    }

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
            cDados.exportaOlap(exporter, parameter, this);
        }
    }

    protected void menu_Init(object sender, EventArgs e)
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

        ASPxMenu menu = (sender as ASPxMenu);

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
            btnExportar.ToolTip = "Exportar para XLSX";
        }

        #endregion

        #region JS

        menu.ClientSideEvents.ItemClick =
        @"function(s, e){ 

            e.processOnServer = false;
            
            if(e.item.name == 'btnIncluir')
            {
                ShowMenu(s, e);
            }else  if(e.item.name == 'btnSalvarLayout')
            {
                SalvarConfiguracoesLayout();
            }else  if(e.item.name == 'btnRestaurarLayout')
            {
                RestaurarConfiguracoesLayout();
            }else if(e.item.name != 'btnLayout')
	        {
                e.processOnServer = true;		                                        
	        }	
        }";

        #endregion

        DevExpress.Web.MenuItem btnIncluir = menu.Items.FindByName("btnIncluir");
        btnIncluir.ClientEnabled = false;

        btnIncluir.ClientVisible = false;

        #region LAYOUT

        DevExpress.Web.MenuItem btnLayout = menu.Items.FindByName("btnLayout");

        btnLayout.ClientVisible = true;

        #endregion
    }

    #endregion

    protected void pvgDadosIndicador_CustomCallback(object sender, PivotGridCustomCallbackEventArgs e)
    {
        if (e.Parameters == "A")
        {
            Olap.PivotGridField fldAno = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_Ano");
            Olap.PivotGridField fldMapa = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_MapaEstrategico");
            Olap.PivotGridField fldIndicador = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_Indicador");
            Olap.PivotGridField fldUnidade = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_Unidade");
            Olap.PivotGridField fldMes = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_MesPorExtenso");
            Olap.PivotGridField fldObjetivo = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_Objetivo");
            Olap.PivotGridField fldPerspectiva = (Olap.PivotGridField)pvgDadosIndicador.Fields.GetFieldByName("field_Perspectiva");

            fldAno.FilterValues.Clear();
            fldMapa.FilterValues.Clear();
            fldIndicador.FilterValues.Clear();
            fldUnidade.FilterValues.Clear();
            fldMes.FilterValues.Clear();
            fldObjetivo.FilterValues.Clear();
            fldPerspectiva.FilterValues.Clear();
            ajustaTelaSelecaoFiltro();

            populaGrid(false);
        }
    }
}
