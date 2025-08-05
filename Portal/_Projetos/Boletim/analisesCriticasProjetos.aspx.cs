using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Collections;
using System.Text.RegularExpressions;

public partial class _Projetos_Boletim_analisesCriticasProjetos : System.Web.UI.Page
{
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoUsuarioLogado;
    private dados cDados;
    protected int codigoStatusReport;
    private string iniciais;
    protected string larguraTela;
    protected string alturaTela;
    protected string tituloModal;

    private bool exibeBotaoEdicao = true;
    private bool exibirAnaliseGeral = true;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        cDados = CdadosUtil.GetCdados(null);

        if (!IsPostBack && !IsCallback)
        {
            hfDadosSessao.Set("CodigoEntidade", cDados.getInfoSistema("CodigoEntidade").ToString());
            hfDadosSessao.Set("Resolucao", cDados.getInfoSistema("ResolucaoCliente").ToString());
            hfDadosSessao.Set("IDUsuarioLogado", cDados.getInfoSistema("IDUsuarioLogado").ToString());
            hfDadosSessao.Set("IDEstiloVisual", cDados.getInfoSistema("IDEstiloVisual").ToString());
            hfDadosSessao.Set("NomeUsuarioLogado", cDados.getInfoSistema("NomeUsuarioLogado").ToString());
            hfDadosSessao.Set("CodigoCarteira", cDados.getInfoSistema("CodigoCarteira").ToString());

        }

        cDados.setInfoSistema("CodigoEntidade", hfDadosSessao.Get("CodigoEntidade").ToString());
        cDados.setInfoSistema("ResolucaoCliente", hfDadosSessao.Get("Resolucao").ToString());
        cDados.setInfoSistema("IDUsuarioLogado", hfDadosSessao.Get("IDUsuarioLogado").ToString());
        cDados.setInfoSistema("IDEstiloVisual", hfDadosSessao.Get("IDEstiloVisual").ToString());
        cDados.setInfoSistema("NomeUsuarioLogado", hfDadosSessao.Get("NomeUsuarioLogado").ToString());
        cDados.setInfoSistema("CodigoCarteira", hfDadosSessao.Get("CodigoCarteira").ToString());

        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        
        btnEdicaoAnaliseGeral.ClientVisible = exibeBotaoEdicao;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

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
        if (!int.TryParse(Request.QueryString["codStatusReport"], out codigoStatusReport))
            codigoStatusReport = -1;
        iniciais = Request.QueryString["iniciais"];
        if (string.IsNullOrWhiteSpace(iniciais))
            iniciais = "BLTQ";
        exibirAnaliseGeral = (iniciais != "BLT_AE_UN");

        InitData(codigoStatusReport);
        defineDimensoesTela();
        btnAvancar.JSProperties["cp_CodigoStatusReport"] = codigoStatusReport;
        btnAvancar.JSProperties["cp_podeEditar"] = Request.QueryString["podeEditar"];
        btnAvancar.JSProperties["cp_iniciais"] = iniciais;
        if (!hfGeral.Contains("TipoRegistroEmEdicao"))
            hfGeral.Add("TipoRegistroEmEdicao", string.Empty);
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        cDados.aplicaEstiloVisual(this);
        gvDadosBoletim.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

        if (!IsPostBack && !IsCallback)
            CDIS_GridLocalizer.Activate();


        gvDadosBoletim.JSProperties["cp_Salvar"] = "N";
        gvDadosBoletim.JSProperties["cp_Msg"] = "";

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/analisesCriticasProjetos.js""></script>"));
        this.TH(this.TS("analisesCriticasProjetos"));
    }


    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/jquery.ultima.js""></script>"));
    }




    private void InitData(int codigoStatusReport)
    {
        #region Comando SQL

        string comandoSql = string.Format(@"
DECLARE @CodigoEntidade INT
DECLARE @CodigoStatusReport Int
DECLARE @IniciaisModelo varchar(10)
    SET @CodigoEntidade = {2}
	SET @CodigoStatusReport = {3}
    SET @IniciaisModelo = '{4}'
	
 SELECT {0}.{1}.f_GetDescricaoStatusReport(SR.CodigoStatusReport) NomeRelatorio ,
		CASE WHEN @IniciaisModelo = 'GRF_BOLHAS' THEN sr.ComentarioGeral ELSE ap.Analise END as ComentarioGeral, --sr.ComentarioGeral ,
        (SELECT {0}.{1}.f_GetDescricaoOrigemAssociacaoObjeto(@CodigoEntidade, sr.CodigoTipoAssociacaoObjeto, null, sr.CodigoObjeto,0,null) ) AS DescricaoObjeto,
        sr.DataGeracao        AS DataEmissaoRelatorio,
        ap.IndicaRegistroEditavel,
        msr.DescricaoModeloStatusReport,
        msr.IniciaisModeloControladoSistema
   FROM {0}.{1}.StatusReport AS sr INNER JOIN 
		{0}.{1}.ModeloStatusReport AS msr ON msr.CodigoModeloStatusReport = sr.CodigoModeloStatusReport LEFT JOIN
		{0}.{1}.AnalisePerformance ap ON ap.CodigoAnalisePerformance = sr.CodigoAnalisePerformance
  WHERE sr.CodigoStatusReport = @CodigoStatusReport	
 
 SELECT sr.CodigoObjeto AS CodigoProjeto, 
        sr.CodigoStatusReport,
		(SELECT {0}.{1}.f_GetDescricaoOrigemAssociacaoObjeto(@CodigoEntidade, sr.CodigoTipoAssociacaoObjeto, null, sr.CodigoObjeto,0,null) ) AS DescricaoObjeto, 
        ap.IndicaRegistroEditavel,
		CASE WHEN @IniciaisModelo = 'GRF_BOLHAS' THEN sr.ComentarioGeral ELSE ap.Analise END as ComentarioGeral--sr.ComentarioGeral
   FROM {0}.{1}.[StatusReport] AS [sr]  LEFT JOIN
		{0}.{1}.AnalisePerformance ap ON ap.CodigoAnalisePerformance = sr.CodigoAnalisePerformance
  WHERE sr.[CodigoStatusReportSuperior] = @CodigoStatusReport
  ORDER BY 2", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, codigoStatusReport, iniciais);

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);

        if (cDados.DataSetOk(ds))
        {
            if (cDados.DataTableOk(ds.Tables[0]))
            {
                DataRow row = ds.Tables[0].Rows[0];
                int quinzena = Convert.ToDateTime(row["DataEmissaoRelatorio"]).Day < 15 ? 1 : 2;
                bool indicaRegistroEditavel = row["IndicaRegistroEditavel"].Equals("S");
                //htmlAnalise.ClientEnabled = indicaRegistroEditavel;
                tituloModal = string.Format("{0} - {1}", row["DescricaoModeloStatusReport"], row["DescricaoObjeto"]);
                tituloModal = tituloModal.Replace("\"", "&quot;");
                txtBoletim.Value = row["NomeRelatorio"];
                if (!IsPostBack)
                {
                    string comentarioGeral = row["ComentarioGeral"].ToString();
                    int numberOfCaracters = comentarioGeral.Length;
                    htmlAnalise.Html = comentarioGeral;
                    //htmlAnaliseEdit.Html = comentarioGeral;
                }
            }
            if (cDados.DataTableOk(ds.Tables[1]))
            {
                gvDadosBoletim.DataSource = ds.Tables[1];
                gvDadosBoletim.DataBind();
            }
        }
    }

    private void defineDimensoesTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = (largura - 23).ToString() + "px";

        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        alturaTela = (altura - 250).ToString() + "px";

        divAnaliseGeral.Style.Clear();
        if (exibirAnaliseGeral || gvDadosBoletim.VisibleRowCount == 0)
        {
            int auxaltura = 0;
            bool retorno_altura = int.TryParse(Request.QueryString["altura"] + "", out auxaltura);
            gvDadosBoletim.Settings.VerticalScrollableHeight = auxaltura - 310;
        }

        else
        {
            divAnaliseGeral.Style.Add(HtmlTextWriterStyle.Display, "none");
            int auxaltura = 0;
            bool retorno_altura = int.TryParse(Request.QueryString["altura"] + "", out auxaltura);
            gvDadosBoletim.Settings.VerticalScrollableHeight = auxaltura - 310;
        }
    }

    protected void gvDadosBoletim_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (gvDadosBoletim.DataSource == null)
            return;

        DataTable dt = (DataTable)gvDadosBoletim.DataSource;
        foreach (DictionaryEntry key in e.Keys)
        {
            object codigoProjeto = key.Value;
            string filtro = string.Format("CodigoProjeto = {0}", codigoProjeto);
            DataRow row = dt.Select(filtro)[0];
            string analiseGeral = e.NewValues["ComentarioGeral"] as string ?? string.Empty;
            row["ComentarioGeral"] = analiseGeral;
            row.EndEdit();

            string comandoSql = string.Format(@"
DECLARE @CodigoStatusReport INT,
        @CodigoObjetoAssociado INT, 
        @CodigoTipoAssociacao INT, 
        @Analise_Original VARCHAR(MAX), 
        @Analise VARCHAR(MAX), 
        @CodigoAnalisePerformace_Original INT,
        @CodigoAnalisePerformace INT,
        @CodigoUsuario INT,
        @Data DATETIME,
        @CodigoObjeto INT
        
    SET @CodigoStatusReport = {2}
    SET @Analise = '{4}'
    SET @CodigoUsuario = {5}
    SET @Data = GETDATE()
    SET @CodigoObjeto = {3}
    
 SELECT @CodigoAnalisePerformace_Original = sr.CodigoAnalisePerformance,
        @CodigoObjetoAssociado = sr.CodigoObjeto,
        @CodigoTipoAssociacao = sr.CodigoTipoAssociacaoObjeto,
        @Analise_Original = ap.Analise
   FROM {0}.{1}.[StatusReport] sr LEFT JOIN
        {0}.{1}.[AnalisePerformance] ap ON ap.CodigoAnalisePerformance = sr.CodigoAnalisePerformance
  WHERE sr.CodigoStatusReportSuperior = @CodigoStatusReport 
    AND sr.CodigoObjeto = @CodigoObjeto

 UPDATE {0}.{1}.[AnalisePerformance]
    SET [DataAnalisePerformance] = @Data,
        [Analise] = @Analise,
        [CodigoUsuarioUltimaAlteracao] = @CodigoUsuario,
        [DataUltimaAlteracao] = @Data
  WHERE [CodigoAnalisePerformance] = @CodigoAnalisePerformace_Original
    AND [IndicaRegistroEditavel] = 'S'
    
IF @@ROWCOUNT = 0 AND (@Analise_Original <> @Analise OR @Analise_Original IS NULL)
BEGIN
INSERT INTO {0}.{1}.[AnalisePerformance]
           ([CodigoObjetoAssociado]
           ,[CodigoTipoAssociacao]
           ,[DataAnalisePerformance]
           ,[Analise]
           ,[CodigoUsuarioInclusao]
           ,[DataInclusao]           
           ,[IndicaRegistroEditavel])
     VALUES
           (@CodigoObjetoAssociado
           ,@CodigoTipoAssociacao
           ,@Data
           ,@Analise
           ,@CodigoUsuario
           ,@Data          
           ,'S')

    SET @CodigoAnalisePerformace = SCOPE_IDENTITY()

IF @CodigoAnalisePerformace_Original IS NULL
 UPDATE {0}.{1}.[StatusReport]
    SET [CodigoAnalisePerformance] = @CodigoAnalisePerformace
  WHERE [CodigoAnalisePerformance] IS NULL
    AND [CodigoObjeto] = @CodigoObjetoAssociado
    AND [CodigoTipoAssociacaoObjeto] = @CodigoTipoAssociacao
    AND [DataPublicacao] IS NULL
ELSE
 UPDATE {0}.{1}.[StatusReport]
    SET [CodigoAnalisePerformance] = @CodigoAnalisePerformace
  WHERE [CodigoAnalisePerformance] = @CodigoAnalisePerformace_Original
    AND [DataPublicacao] IS NULL

END    
    
 UPDATE {0}.{1}.[StatusReport]
    SET [DataUltimaAlteracao] = @Data
       ,[CodigoUsuarioUltimaAlteracao] = @CodigoUsuario
  WHERE [CodigoStatusReport] = @CodigoStatusReport"
                , cDados.getDbName(), cDados.getDbOwner(), codigoStatusReport, codigoProjeto, analiseGeral.Replace("'", "''"), codigoUsuarioLogado);

            int registrosAfetados = 0;
            cDados.execSQL(comandoSql, ref registrosAfetados);
        }
        gvDadosBoletim.CancelEdit();
        e.Cancel = true;
    }

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string parametro = e.Parameter;
        pnCallback.JSProperties["cp_LastOperation"] = parametro;
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        int codigoSR;
        string tipoRegistroEmEndicaio = (string)hfGeral["TipoRegistroEmEdicao"];
        if (tipoRegistroEmEndicaio == "pai" || parametro == "Avancar")
            codigoSR = codigoStatusReport;
        else
            codigoSR = (int)gvDadosBoletim.GetRowValues(
                gvDadosBoletim.FocusedRowIndex, "CodigoStatusReport");
        string mensagemErro_Persistencia;
        switch (parametro)
        {
            case "Avancar":
                mensagemErro_Persistencia = string.Empty;//mensagemErro_Persistencia = SalvarAlteracoes(codigoSR);
                break;
            case "Salvar":
                if (iniciais == "GRF_BOLHAS")
                    mensagemErro_Persistencia = SalvarAlteracoesGraficoBolhas(codigoSR);
                else
                    mensagemErro_Persistencia = SalvarAlteracoes(codigoSR);
                break;
            default:
                mensagemErro_Persistencia = string.Empty;
                break;
        }
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = parametro;
            InitData(codigoStatusReport);
            if (tipoRegistroEmEndicaio == "pai")
                htmlAnalise.Html = htmlAnaliseEdit.Html;
        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }

    private string SalvarAlteracoesGraficoBolhas(int codigoSR)
    {
        string msgErro = string.Empty;
        try
        {
            string analise = htmlAnaliseEdit.Html.Replace("'", "''");

            string analiseComTagsHtmlRemovida = Regex.Replace(analise, @"/<.*?>/g", string.Empty);

            if (analiseComTagsHtmlRemovida.Length > 8000)
                return "O conteúdo da análise é muito grande.";
            string comandoSql = string.Format(@"

DECLARE @CodigoStatusReport INT,
        @CodigoObjetoAssociado INT, 
        @CodigoTipoAssociacao INT, 
        @Analise_Original VARCHAR(MAX), 
        @Analise VARCHAR(MAX), 
        @CodigoAnalisePerformace_Original INT,
        @CodigoAnalisePerformace INT,
        @CodigoUsuario INT,
        @Data DATETIME
        
    SET @CodigoStatusReport = {2}
    SET @Analise = '{3}'
    SET @CodigoUsuario = {4}
    SET @Data = GETDATE()

 UPDATE {0}.{1}.[StatusReport]
    SET [DataUltimaAlteracao] = @Data
       ,[ComentarioGeral] = @Analise
       ,[CodigoUsuarioUltimaAlteracao] = @CodigoUsuario
  WHERE [CodigoStatusReport] = @CodigoStatusReport"
                        , cDados.getDbName()
                        , cDados.getDbOwner()
                        , codigoSR
                        , analise
                        , codigoUsuarioLogado);

            int registrosAfetados = 0;
            cDados.execSQL(comandoSql, ref registrosAfetados);
            gvDadosBoletim.JSProperties["cp_Html"] = analise;
        }
        catch(Exception e)
        {
            msgErro = e.Message;
        }
        return msgErro;
    }

    private string SalvarAlteracoes(int codigoSR)
    {
        string msgErro = string.Empty;
        try
        {
            string analise = htmlAnaliseEdit.Html.Replace("'", "''");

            string analiseComTagsHtmlRemovida = Regex.Replace(analise ?? string.Empty, @"<[^>]*>", " ");

            if (analiseComTagsHtmlRemovida.Length > 8000)
            {
                gvDadosBoletim.JSProperties["cp_status"] = "erro";
                return "O conteúdo da análise é muito grande.";
            }
            if (VerificaHouveAlteracaoAnalise(codigoSR, analise))
            {
                string comandoSql = string.Format(@"
DECLARE @CodigoStatusReport INT,
        @CodigoObjetoAssociado INT, 
        @CodigoTipoAssociacao INT, 
        @Analise_Original VARCHAR(max), 
        @Analise VARCHAR(max), 
        @CodigoAnalisePerformace_Original INT,
        @CodigoAnalisePerformace INT,
        @CodigoUsuario INT,
        @Data DATETIME
        
    SET @CodigoStatusReport = {2}
    SET @Analise = '{3}'
    SET @CodigoUsuario = {4}
    SET @Data = GETDATE()
    
 SELECT @CodigoAnalisePerformace_Original = sr.CodigoAnalisePerformance,
        @CodigoObjetoAssociado = sr.CodigoObjeto,
        @CodigoTipoAssociacao = sr.CodigoTipoAssociacaoObjeto,
        @Analise_Original = ap.Analise
   FROM {0}.{1}.[StatusReport] sr LEFT JOIN
        {0}.{1}.[AnalisePerformance] ap ON ap.CodigoAnalisePerformance = sr.CodigoAnalisePerformance
  WHERE sr.CodigoStatusReport = @CodigoStatusReport

 UPDATE {0}.{1}.[AnalisePerformance]
    SET [DataAnalisePerformance] = @Data,
        [Analise] = @Analise,
        [CodigoUsuarioUltimaAlteracao] = @CodigoUsuario,
        [DataUltimaAlteracao] = @Data
  WHERE [CodigoAnalisePerformance] = @CodigoAnalisePerformace_Original
    AND [IndicaRegistroEditavel] = 'S'
    
IF @@ROWCOUNT = 0 AND (@Analise_Original <> @Analise OR @Analise_Original IS NULL)
BEGIN
INSERT INTO {0}.{1}.[AnalisePerformance]
           ([CodigoObjetoAssociado]
           ,[CodigoTipoAssociacao]
           ,[DataAnalisePerformance]
           ,[Analise]
           ,[CodigoUsuarioInclusao]
           ,[DataInclusao]           
           ,[IndicaRegistroEditavel])
     VALUES
           (@CodigoObjetoAssociado
           ,@CodigoTipoAssociacao
           ,@Data
           ,@Analise
           ,@CodigoUsuario
           ,@Data          
           ,'S')

    SET @CodigoAnalisePerformace = SCOPE_IDENTITY()

IF @CodigoAnalisePerformace_Original IS NULL
 UPDATE {0}.{1}.[StatusReport]
    SET [CodigoAnalisePerformance] = @CodigoAnalisePerformace
  WHERE [CodigoAnalisePerformance] IS NULL
    AND [CodigoObjeto] = @CodigoObjetoAssociado
    AND [CodigoTipoAssociacaoObjeto] = @CodigoTipoAssociacao
    AND [DataPublicacao] IS NULL
ELSE
 UPDATE {0}.{1}.[StatusReport]
    SET [CodigoAnalisePerformance] = @CodigoAnalisePerformace
  WHERE [CodigoAnalisePerformance] = @CodigoAnalisePerformace_Original
    AND [DataPublicacao] IS NULL

END    
    
 UPDATE {0}.{1}.[StatusReport]
    SET [DataUltimaAlteracao] = @Data
       ,[CodigoUsuarioUltimaAlteracao] = @CodigoUsuario
  WHERE [CodigoStatusReport] = @CodigoStatusReport"
                        , cDados.getDbName(), cDados.getDbOwner(), codigoSR, analise, codigoUsuarioLogado);

                int registrosAfetados = 0;
                cDados.execSQL(comandoSql, ref registrosAfetados);
                gvDadosBoletim.JSProperties["cp_Html"] = analise;
            }
        }
        catch (Exception e)
        {
            gvDadosBoletim.JSProperties["cp_status"] = "erro";
            msgErro = e.Message;
        }
        return msgErro;
    }

    private bool VerificaHouveAlteracaoAnalise(int codigoSR, string analise)
    {
        string comandoSql = string.Format(@"
 SELECT ap.Analise
   FROM [StatusReport] sr LEFT JOIN
        [AnalisePerformance] ap ON ap.CodigoAnalisePerformance = sr.CodigoAnalisePerformance
  WHERE sr.CodigoStatusReport = {0}", codigoSR);
        DataSet dsTemp = cDados.getDataSet(comandoSql);
        if (dsTemp.Tables[0].Rows.Count == 1)
        {
            string anliseOriginal = dsTemp.Tables[0].Rows[0]["Analise"].ToString();
            return analise != anliseOriginal;
        }

        return !string.IsNullOrWhiteSpace(analise);
    }

    protected void gvDadosBoletim_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {
        /*switch (e.ButtonType)
        {
            case DevExpress.Web.ColumnCommandButtonType.Edit:
                bool permiteEditarRegistro = gvDadosBoletim.GetRowValues(
                    e.VisibleIndex, "IndicaRegistroEditavel").Equals("S");
                if (!permiteEditarRegistro)
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
                break;
        }*/
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (gvDadosBoletim.VisibleRowCount == 0) return;
        switch (e.ButtonID)
        {
            case "btnEditar":
                e.Visible = exibeBotaoEdicao ?
                    DevExpress.Utils.DefaultBoolean.Default :
                    DevExpress.Utils.DefaultBoolean.False;
                break;
        }

    }

    protected void gvDadosBoletim_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        GridViewDataColumn coluna = e.DataColumn;

        if (coluna.FieldName == "ComentarioGeral")
        {
            string strValue = e.CellValue.ToString();
            strValue = Regex.Replace(strValue, @"<[^>]*>", " ");
            if (e.CellValue != null && strValue.Length > 200)
            {
                e.Cell.ToolTip = strValue;
                strValue = strValue.Substring(0, 200) + "...";
            }
            e.Cell.Text = strValue;
        }
    }

    protected void gvDadosBoletim_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "S")
        {
            int codigoSR;
            string tipoRegistroEmEndicaio = (string)hfGeral["TipoRegistroEmEdicao"];
            if (tipoRegistroEmEndicaio == "pai")
                codigoSR = codigoStatusReport;
            else
                codigoSR = (int)gvDadosBoletim.GetRowValues(
                    gvDadosBoletim.FocusedRowIndex, "CodigoStatusReport");

            gvDadosBoletim.JSProperties["cp_Msg"] = SalvarAlteracoes(codigoSR);

            gvDadosBoletim.JSProperties["cp_Salvar"] = "S";
        }
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "AnlCritPrj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "AnlCritPrj", "Análise Crítica Projeto", this);
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
}