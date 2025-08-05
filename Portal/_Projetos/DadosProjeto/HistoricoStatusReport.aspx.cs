using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Web;
using DevExpress.Web;
using DevExpress.XtraReports.UI;
using System.Linq;
using DevExpress.XtraPrinting;
using System.Collections;
using System.Web.UI.WebControls;

public partial class _Projetos_DadosProjeto_HistoricoStatusReport : System.Web.UI.Page
{
    dados cDados;
    private string dbName;
    private string dbOwner;

    private int codigoUsuarioResponsavel = 0;
    private int codigoEntidadeUsuarioResponsavel = 0;
    private int idObjeto;
    private string iniciaisTipoObjeto;

    private string resolucaoCliente = "";

    public string estiloDiv = "style='OVERFLOW: auto; WIDTH: 730px; max-height: 320px'";

    public bool podeIncluir = true;
    public bool podeEditar = false;
    public bool podeExcluir = false;

    public bool podeEditarComentarios = false;
    public bool podeEnviarDestinatarios = false;
    public bool podePublicar = false;
    public bool podeExcluirRelatorio = false;
    public bool existeAnaliseOrcamento = false;

    private bool exibeBotaoEdicao = true;

    public string alturaRelatorio = "500px";

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
            Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            Response.End();
        }



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

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        // datos do usuario logado e da entidad logada.
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); // Ok
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()); // Ok

        idObjeto = int.Parse(Request.QueryString["idObjeto"]);
        iniciaisTipoObjeto = Request.QueryString["tp"];

        string iniciaisPermissaoEditarComentario = string.Format("{0}_CmtSttRpt", iniciaisTipoObjeto);
        string iniciaisPermissaoEnviarRelatorio = string.Format("{0}_EnvSttRpt", iniciaisTipoObjeto);
        string iniciaisPermissaoPublicar = string.Format("{0}_PubSttRpt", iniciaisTipoObjeto);
        string iniciaisPermissaoIncluir = string.Format("{0}_CriSttRpt", iniciaisTipoObjeto);
        string iniciaisPermissaoExcluir = "EN_ExcSttRpt";

        podeEditarComentarios = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            idObjeto, "null", iniciaisTipoObjeto, 0, "null", iniciaisPermissaoEditarComentario);
        podeEnviarDestinatarios = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            idObjeto, "null", iniciaisTipoObjeto, 0, "null", iniciaisPermissaoEnviarRelatorio);
        podePublicar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            idObjeto, "null", iniciaisTipoObjeto, 0, "null", iniciaisPermissaoPublicar);
        podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            idObjeto, "null", iniciaisTipoObjeto, 0, "null", iniciaisPermissaoIncluir);
        podeExcluirRelatorio = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", iniciaisPermissaoExcluir);//Ao contrário das demais a permissão de exclusão está a nível de entidade

        if (iniciaisTipoObjeto == "PR")
        {
            cDados.verificaPermissaoProjetoInativo(idObjeto, ref podeIncluir, ref podeEditar, ref podeExcluir);
            cDados.verificaPermissaoProjetoInativo(idObjeto, ref podeEditarComentarios, ref podeEnviarDestinatarios, ref podePublicar);
            cDados.verificaPermissaoProjetoInativo(idObjeto, ref podeExcluirRelatorio, ref podeExcluirRelatorio, ref podeExcluirRelatorio);

        }

        cDados.setaTamanhoMaximoMemo(txtComentarioPlanoAcao, 2000, lbl_txtComentarioPlanoAcao);
        cDados.setaTamanhoMaximoMemo(txtAnaliseDesempenhoFisico, 2000, lbl_txtAnaliseDesempenhoFisico);
        cDados.setaTamanhoMaximoMemo(txtAnaliseDesempenhoFinanceiro, 2000, lbl_txtAnaliseDesempenhoFinanceiro);
        cDados.setaTamanhoMaximoMemo(txtAnaliseRiscos, 2000, lbl_txtAnaliseRiscos);
        cDados.setaTamanhoMaximoMemo(txtAnaliseQuestoes, 2000, lbl_txtAnaliseQuestoes);
        cDados.setaTamanhoMaximoMemo(txtAnaliseMetas, 2000, lbl_txtAnaliseMetas);
    }

    private void DefineBotoesVisiveis()
    {
        GridViewCommandColumn commandColumn = (GridViewCommandColumn)gvDados.Columns["Acao"];
        if (!podeEditarComentarios)
            commandColumn.CustomButtons[1].Visibility = GridViewCustomButtonVisibility.Invisible;
        if (!podePublicar)
            commandColumn.CustomButtons[2].Visibility = GridViewCustomButtonVisibility.Invisible;
        if (!podeEnviarDestinatarios)
            commandColumn.CustomButtons[3].Visibility = GridViewCustomButtonVisibility.Invisible;


    }

    protected void Page_Load(object sender, EventArgs e)
    {
        hfGeral.Set("existeAnaliseOrcamento", "N");
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok


        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        //dsDado.SelectParameters[0].DefaultValue = codigoEntidadeUsuarioResponsavel.ToString();
        cDados.aplicaEstiloVisual(Page);//Ok

        DefineBotoesVisiveis();
        carregaGvDados();               //Ok
        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "tituloPaginasWEB", "labelQuestao", "labelQuestoes", "lblGeneroLabelQuestao", "diaInicioEdicaoAnaliseCritica", "diaTerminoEdicaoAnaliseCritica");
        if (dsParametros.Tables[0].Rows.Count > 0)
        {
            DataRow row = dsParametros.Tables[0].Rows[0];
            object diaInicio = row["diaInicioEdicaoAnaliseCritica"];
            object diaTermino = row["diaTerminoEdicaoAnaliseCritica"];
            if (!(Convert.IsDBNull(diaInicio) || Convert.IsDBNull(diaTermino)))
            {
                int diaAtual = DateTime.Today.Day;
                exibeBotaoEdicao =
                    diaAtual >= Convert.ToInt32(diaInicio) &&
                    diaAtual <= Convert.ToInt32(diaTermino);
                htmlComentariosGerais.ClientEnabled = exibeBotaoEdicao;
            }

            string label = row["labelQuestoes"].ToString();
            string genero = row["lblGeneroLabelQuestao"].ToString();

            lblAnaliseQuestoes.Text = string.Format("Análise d{1}s {0}", label, genero == "M" ? "o" : "a");
        }

        btnProximo.JSProperties["cp_PodeEditar"] = podePublicar ? "S" : "N";
        populaCombos();

        gvAnalises.Settings.ShowFilterRow = false;
        gvAnalises.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvAnalises.SettingsBehavior.AllowSort = false;
    }

    #region GRID's

    private void carregaGvAnalise()
    {
        DataSet dsParametros = cDados.getParametrosSistema("MostraAnaliseOrcamento"); //UtilizaOrcamentoERP
        existeAnaliseOrcamento = false;

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["MostraAnaliseOrcamento"] + "" == "S")
        {
            // verifica se existem crs associados ao projeto
            DataSet dsAnalise = cDados.getAnalisesCriticas(idObjeto.ToString());
            if ((cDados.DataSetOk(dsAnalise) && dsAnalise.Tables[0].Rows.Count > 0))
            {

                gvAnalises.DataSource = dsAnalise.Tables[1];
                gvAnalises.DataBind();

                existeAnaliseOrcamento = dsAnalise.Tables[1].Rows.Count > 0;

            }
        }

        lblAnalise.ClientVisible = existeAnaliseOrcamento;
        gvAnalises.ClientVisible = existeAnaliseOrcamento;
        htmlComentariosGerais.Width = existeAnaliseOrcamento ? new Unit("600px") : new Unit("790px");
    }

    #region GRID gvDADOS

    private void carregaGvDados()
    {
        DataSet ds = cDados.getHistoricoStatusReport(codigoEntidadeUsuarioResponsavel, idObjeto, iniciaisTipoObjeto);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }

        if (iniciaisTipoObjeto == "PR")
        {
            carregaGvAnalise();
        }

    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        carregaGvDados();
    }

    #endregion

    #endregion

    #region VARIOS

    private void defineAlturaTela(string resolucaoCliente)
    {
        if (Request.QueryString["Altura"] == null)
        {
            // Calcula a altura da tela
            btnFechar3.ClientVisible = false;
        }
        else
        {
            btnFechar3.ClientVisible = true;
            int altura = int.Parse(Request.QueryString["Altura"].ToString());
            gvDados.Settings.VerticalScrollableHeight = altura - 110;

            alturaRelatorio = (altura - 250).ToString() + "px";
        }
    }

    private void HeaderOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/HistoricoStatusReport.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ASPxListbox.js""></script>"));

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));

        Header.Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/vendor/jquery/v3.3.1/jquery-3.3.1.min.js""></script>", Session["baseUrl"].ToString())));


         this.TH(this.TS("barraNavegacao", "HistoricoStatusReport", "ASPxListbox"));
    }

    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        string codigoDado = "";
        if (gvDados.FocusedRowIndex != -1)
            codigoDado = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoStatusReport").ToString();
        return codigoDado;
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_LastOperation"] = e.Parameter;
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        string mensagemErro_Persistencia = "";
        switch (e.Parameter)
        {
            case "Enviar":
                mensagemErro_Persistencia = persisteEnvioRelatorioDestinatarios();
                break;
            case "Publicar":
                mensagemErro_Persistencia = persistePublicacaoRelatorio();
                break;
            case "Editar":
                mensagemErro_Persistencia = persisteEdicaoComentarios();
                break;
            case "Proximo":
                mensagemErro_Persistencia = persisteEdicaoComentarios();
                break;
            case "GerarRelatorio":
                mensagemErro_Persistencia = persisteGeracaoRelatorio();
                break;
            case "Excluir":
                mensagemErro_Persistencia = persisteExclusaoRelatorio();
                break;
            case "SalvarDestaque":
                mensagemErro_Persistencia = persisteEdicaoDestaque();
                break;
        }
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

    }

    private string persisteExclusaoRelatorio()
    {
        string msg = string.Empty;
        try
        {
            int codigoStatusReport = int.Parse(getChavePrimaria());
            cDados.ExcluiRelatorioStatusReport(codigoStatusReport, codigoUsuarioResponsavel);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }

    private string persisteGeracaoRelatorio()
    {
        string msg = string.Empty;
        try
        {
            int codigoModeloStatusReport = (int)ddlModeloStatusReport.Value;
            cDados.GeraRelatoriosStatusReport(codigoModeloStatusReport, iniciaisTipoObjeto, idObjeto, codigoUsuarioResponsavel, out msg);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }

    private void Download()
    {
        int codigoStatusReport = int.Parse(getChavePrimaria());
        string nomeArquivo = "StatusReport.pdf";
        string arquivo = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "ArquivosTemporarios\\" + nomeArquivo;
        string arquivo2 = "~/ArquivosTemporarios/" + nomeArquivo;
        FileStream fs = new FileStream(arquivo, FileMode.Create, FileAccess.Write);
        byte[] imagem = cDados.getConteudoStatusReport(codigoStatusReport);
        if (imagem == null)
        {
            persistePublicacaoRelatorio();
            imagem = cDados.getConteudoStatusReport(codigoStatusReport);
        }
        fs.Write(imagem, 0, imagem.Length);
        fs.Close();

        ForceDownloadFile(arquivo2, true);
    }

    private void ForceDownloadFile(string fname, bool forceDownload)
    {
        //ASPxWebControl.RedirectOnCallback(fname);
        string path = MapPath(fname);
        string name = Path.GetFileName(path);
        string ext = Path.GetExtension(path);
        string type = "application/octet-stream";
        if (forceDownload)
        {
            Response.AppendHeader("content-disposition",
                "attachment; filename=" + name);
        }
        Response.ContentType = type;
        Response.WriteFile(path);
        Response.Flush();
        Response.End();
    }

    private string persisteEdicaoComentarios()
    {
        string msg = "";
        int codigoStatusReport;
        try
        {
            codigoStatusReport = int.Parse(getChavePrimaria());
            cDados.atualizaComentariosStatusReport(codigoStatusReport, codigoUsuarioResponsavel, htmlComentariosGerais.Html.Replace("'", "''"),
                txtAnaliseDesempenhoFisico.Text.Replace("'", "''"), txtAnaliseDesempenhoFinanceiro.Text.Replace("'", "''"),
                txtAnaliseRiscos.Text.Replace("'", "''"), txtAnaliseQuestoes.Text.Replace("'", "''"), txtAnaliseMetas.Text.Replace("'", "''"), txtComentarioPlanoAcao.Text.Replace("'", "''"));
            carregaGvDados();
            gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoStatusReport);
            gvDados.ClientVisible = false;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }


    private string persisteEdicaoDestaque()
    {
        string msg = "";
        int codigoStatusReport;
        try
        {
            codigoStatusReport = int.Parse(getChavePrimaria());
            cDados.atualizaDestaqueStatusReport(codigoStatusReport, codigoUsuarioResponsavel, htmlDestaqueEdit.Html.Replace("'", "''"));
            carregaGvDados();
            gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoStatusReport);
            gvDados.ClientVisible = false;
        }
        catch (Exception ex)
        {
            msg = Resources.traducao.HistoricoStatusReport_erro_ao_salvar_destaque_do_m_s__n_ + ex.Message;
        }
        return msg;
    }
    //private string persistePublicacaoRelatorio()
    //{
    //    string msg = "";
    //    int codigoStatusReport;
    //    try
    //    {
    //        codigoStatusReport = int.Parse(getChavePrimaria());
    //        cDados.publicaStatusReport(codigoStatusReport, null);
    //        carregaGvDados();
    //        gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoStatusReport);
    //        gvDados.ClientVisible = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        msg = ex.Message;
    //    }
    //    return msg;
    //}

    private string persistePublicacaoRelatorio()
    {
        string msg = "";
        int codigoStatusReport = int.Parse(getChavePrimaria());
        try
        {
            using (MemoryStream stream = new MemoryStream())
            {
                #region Comentario
                /*XtraReport rel;
                string iniciaisModeloControladoSistema = gvDados.GetRowValuesByKeyValue(
                    codigoStatusReport, "IniciaisModeloControladoSistema") as string;

                if (iniciaisModeloControladoSistema == "BLTQ")
                    rel = new rel_BoletimStatus(codigoStatusReport);
                else
                    rel = new rel_StatusReport();

                #region Comando SQL
                string comandoSql = string.Format(@"
DECLARE @CodigoStatusReport		Int
    SET @CodigoStatusReport = {0}

DECLARE @Alta Int,
        @Media Int,
        @Baixa Int
    SET @Alta = 3
    SET @Media = 2
    SET @Baixa = 1

SELECT
			sr.[CodigoStatusReport]
		, p.[CodigoProjeto]	
		, p.[NomeProjeto]
		, un.[NomeUnidadeNegocio]		AS [UnidadeProjeto]
		, u.[NomeUsuario]						AS [GerenteProjeto]
        , sr.[DataInicioPeriodoRelatorio]
        , sr.[DataTerminoPeriodoRelatorio]
        , sr.[DataGeracao]
		, sr.[ComentarioGeral]
		, sr.[ComentarioFisico]
		, sr.[ComentarioFinanceiro]
		, sr.[ComentarioRisco]
		, sr.[ComentarioQuestao]
		, sr.[ComentarioMeta]
		, sr.[ValorAgregadoProjeto]
		, sr.[ValorPlanejadoProjeto]
		, sr.[CustoRealizado]
        , sr.[ComentarioPlanoAcao]
		, dbo.f_GetIDPAtual(p.[CodigoProjeto])		AS [IDP]
		, dbo.f_GetIDCAtual(p.[CodigoProjeto])		AS [ICC]
		, sr.[EstimativaConcluir] 
		, msr.[ComentarioGeral] AS [ListaComentarioGeral]
		, msr.[ListaTarefasConcluidas]
		, msr.[ListaTarefasAtrasadas]
		, msr.[ListaTarefasFuturas]
		, msr.[ListaMarcosConcluidos]
		, msr.[ListaMarcosAtrasados]
		, msr.[ListaMarcosFuturos]
		, msr.[ComentarioFisico]  AS [ListaComentarioFisico]
		, msr.[ListaContasCusto]
		, msr.[ListaContasReceita]
		, msr.[ComentarioFinanceiro] AS [ListaComentarioFinanceiro]
		, msr.[ListaRiscosAtivos]
		, msr.[ListaRiscosEliminados]
		, msr.[ComentarioRisco] AS [ListaComentarioRisco]
		, msr.[ListaQuestoesAtivas]
		, msr.ListaQuestoesResolvidas
		, msr.[ComentarioQuestao] AS [ListaComentarioQuestao]
		, msr.[ListaMetasResultados]
		, msr.[ComentarioMeta] AS [ListaComentarioMeta]
		, msr.[ListaPendenciasToDoList]
		, msr.ListaContratos
		, msr.AnaliseValorAgregado
        , msr.[ComentarioPlanoAcao] AS [ListaComentarioPlanoAcao]
        , sr.[PercentualFisicoPrevisto]
        , sr.[PercentualFisicoReal]
        , sr.[DesvioFisico]
        , sr.[ResultadoFisico]
        , sr.[CorDesempenhoFisico]
        , sr.[ValorCustoPrevisto]
        , sr.[ValorCustoRealizado]
        , sr.[ResultadoCusto]
        , sr.[CorDesempenhoCusto]
		, ent.[LogoUnidadeNegocio]							AS [LogoEntidade]
	FROM
		[dbo].[StatusReport]										AS [sr]		
		
			INNER JOIN [dbo].[ModeloStatusReport]	AS [msr]	ON 
				( msr.[CodigoModeloStatusReport] = sr.[CodigoModeloStatusReport] )
			
			INNER JOIN [dbo].[Projeto]						AS [p]		ON 
				( p.[CodigoProjeto]						= sr.[CodigoObjeto])
				
			INNER JOIN [dbo].[UnidadeNegocio]			AS [un]		ON 
				( un.[CodigoUnidadeNegocio]		= p.[CodigoUnidadeNegocio])
				
			INNER JOIN [dbo].[UnidadeNegocio]			AS [ent]	ON 
				( ent.[CodigoUnidadeNegocio]	= un.[CodigoEntidade] )
				
			LEFT JOIN [dbo].[Usuario]							AS [u]		ON 
				( u.[CodigoUsuario]					= p.[CodigoGerenteProjeto])
	WHERE
		sr.[CodigoStatusReport] = @CodigoStatusReport


SELECT 
			t.[CodigoStatusReport]
		, t.[NomeTarefa]
		, t.[InicioPrevisto]
		, t.[TerminoPrevisto]
		, t.[InicioReal]
		, t.[TerminoReal]
		, t.[PercentualReal]
		, t.[StringAlocacaoRecursoTarefa] 
		, t.[StatusTarefa]
	FROM
		[dbo].[StatusReportTarefas]		AS [t]
	WHERE
				t.[CodigoStatusReport]		= @CodigoStatusReport
		AND t.[IndicaTipoTarefa]			= 'C'

SELECT 
			t.[CodigoStatusReport]
		, t.[NomeTarefa]
		, t.[InicioPrevisto]
		, t.[TerminoPrevisto]
		, t.[InicioReal]
		, t.[TerminoReal]
		, t.[PercentualReal]
		, t.[StringAlocacaoRecursoTarefa] 
		, t.[StatusTarefa]
	FROM
		[dbo].[StatusReportTarefas]		AS [t]
	WHERE
				t.[CodigoStatusReport]		= @CodigoStatusReport
		AND t.[IndicaTipoTarefa]			= 'A'		

SELECT 
			t.[CodigoStatusReport]
		, t.[NomeTarefa]
		, t.[InicioPrevisto]
		, t.[TerminoPrevisto]
		, t.[InicioReal]
		, t.[TerminoReal]
		, t.[PercentualReal]
		, t.[StringAlocacaoRecursoTarefa] 
		, t.[StatusTarefa]
	FROM
		[dbo].[StatusReportTarefas]		AS [t]
	WHERE
				t.[CodigoStatusReport]		= @CodigoStatusReport
		AND t.[IndicaTipoTarefa]			= 'P'				
		
SELECT 
			t.[CodigoStatusReport]
		, t.[DescricaoConta]													AS [ItemCusto]
		, t.[ValorPrevisto]														AS [CustoPrevisto]
		, t.[ValorTendencia]													AS [Custo]
		, t.[ValorTendencia] - t.[ValorPrevisto]			AS [VariacaoCusto]
		, t.[ValorReal]																AS [CustoReal]
		, t.[ValorTendencia] - t.[ValorReal]					AS [CustoRestante]
	FROM
		[dbo].[StatusReportFinanceiro]	AS [t]
	WHERE
				t.[CodigoStatusReport]		= @CodigoStatusReport
		AND t.[DespesaReceita]				= 'D'

SELECT 
			t.[CodigoStatusReport]
		, t.[DescricaoConta]													AS [ItemReceita]
		, t.[ValorPrevisto]														AS [ReceitaPrevista]
		, t.[ValorTendencia]													AS [Receita]
		, t.[ValorTendencia] - t.[ValorPrevisto]			AS [VariacaoReceita]
		, t.[ValorReal]																AS [ReceitaReal]
		, t.[ValorTendencia] - t.[ValorReal]					AS [ReceitaRestante]
	FROM
		[dbo].[StatusReportFinanceiro]	AS [t]
	WHERE
				t.[CodigoStatusReport]		= @CodigoStatusReport
		AND t.[DespesaReceita]				= 'R'
		
SELECT
			t.[CodigoStatusReport]
		, t.[NomeTarefa]																	AS [Marco]
		, t.[InicioPrevisto]
		, t.[TerminoPrevisto]
		, t.[InicioReal]
		, t.[TerminoReal]
		, t.[PercentualReal]
		, t.[StringAlocacaoRecursoTarefa] 
		, t.[StatusTarefa]
		, t.[Termino]																			AS [TerminoReprogramado]
		, DATEDIFF(DD, t.[TerminoPrevisto], t.[Termino])	AS [Desvio]
	FROM
		[dbo].[StatusReportTarefas]		AS [t]
	WHERE
				t.[CodigoStatusReport]		= @CodigoStatusReport
		AND t.[IndicaTipoTarefa]			= 'M'					

SELECT 
			t.[CodigoStatusReport]
		, srq.[DescricaoStatusRiscoQuestao]					AS [Status]
		, t.[DescricaoRiscoQuestao]							AS [Risco]
		, case t.[ProbabilidadePrioridade]		
				when @Alta then 'Alta'
				when @Media then 'Média'
				when @Baixa then 'Baixa'
          end												AS [Probabilidade]
		, case t.[ImpactoUrgencia]		
				when @Alta then 'Alto'
				when @Media then 'Médio'
				when @Baixa then 'Baixo'
			end												AS [Impacto]
		, t.[DataLimiteResolucao]							AS [LimiteEliminacao]
		, u.[NomeUsuario]									AS [Responsavel]
	FROM
		[dbo].[StatusReportRiscoQuestao]	AS [t]
			INNER JOIN [dbo].[Usuario]			AS [u] ON (u.[CodigoUsuario] = t.[CodigoUsuarioResponsavel])
            INNER JOIN [dbo].[StatusRiscoQuestao] AS [srq] ON (srq.[CodigoStatusRiscoQuestao] = t.[CodigoStatusRiscoQuestao])
	WHERE
				t.[CodigoStatusReport]		= @CodigoStatusReport
		AND t.[IndicaRiscoQuestao]		= 'R'

SELECT 
			t.[CodigoStatusReport]
		, srq.[DescricaoStatusRiscoQuestao]					AS [Status]
		, t.[DescricaoRiscoQuestao]							AS [Questao]
		, case t.[ProbabilidadePrioridade]		
				when @Alta then 'Alta'
				when @Media then 'Média'
				when @Baixa then 'Baixa'
			end												AS [Prioridade]
		, case t.[ImpactoUrgencia]		
				when @Alta then 'Alta'
				when @Media then 'Média'
				when @Baixa then 'Baixa'
			end												AS [Urgencia]
		, t.[DataLimiteResolucao]							AS [LimiteEliminacao]
		, u.[NomeUsuario]									AS [Responsavel]
	FROM
		[dbo].[StatusReportRiscoQuestao]	AS [t]
			INNER JOIN [dbo].[Usuario]			AS [u] ON (u.[CodigoUsuario] = t.[CodigoUsuarioResponsavel])
            INNER JOIN [dbo].[StatusRiscoQuestao] AS [srq] ON (srq.[CodigoStatusRiscoQuestao] = t.[CodigoStatusRiscoQuestao])
    WHERE
				t.[CodigoStatusReport]		= @CodigoStatusReport
		AND t.[IndicaRiscoQuestao]		= 'Q'

SELECT 
			t.[CodigoStatusReport]
		, t.[NomeTarefa]												AS [Tarefa]
		, t.[EstagioTarefa]											AS [DescricaoStatusTarefa]
		, t.[InicioPrevisto]										AS [InicioPrevisto]
		, t.[TerminoPrevisto]										AS [TerminoPrevisto]
		, t.[NomeResponsavel]										AS [Responsavel]
	FROM
		[dbo].StatusReportTarefasToDoList	AS [t]
	WHERE
				t.[CodigoStatusReport]		= @CodigoStatusReport

SELECT 
			t.[CodigoStatusReport]
		, t.[DescricaoIndicador]								AS [NomeIndicador]
		, t.[StatusMeta]												AS [Status]
		, t.[DescricaoMeta]											AS [Meta]
		, t.[Periodo]														AS [Mes]
		, t.[ValorMeta]													AS [MetaAcumuladaAno]
		, t.[ValorResultado]										AS [ResultadoAcumuladoAno]
	FROM
		[dbo].[StatusReportMetas]					AS [t]
	WHERE
				t.[CodigoStatusReport]		= @CodigoStatusReport
    ORDER BY
                t.[DescricaoIndicador],
                t.[DescricaoMeta],
                t.[AnoMes]

SELECT
			t.[CodigoStatusReport]
		, t.[CodigoContrato]
		, t.[NumeroContrato]
		, t.[Fornecedor]
		, t.[TerminoVigencia]
		, t.[ValorTotal]												AS [ValorContrato]
		, t.[ValorPago]													AS [ValorPago]
		, t.[ValorRestante]											AS [ValorRestante]
	FROM
		[dbo].[StatusReportContratos]			AS [t]
	WHERE
		t.[CodigoStatusReport]				= @CodigoStatusReport
		", codigoStatusReport);
                #endregion

                DataSet ds = cDados.getDataSet(comandoSql);
                DataSet dsRel = (DataSet)rel.DataSource;
                string[] tableNames = new string[] { "StatusReport", "TarefasConcluidas", "TarefasAtrasadas", "TarefasProximoPeriodo", "ItensCusto", "ItensReceita", "Marcos", "Riscos", "Questoes", "TarefasToDoList", "Metas", "Contratos" };
                try
                {
                    dsRel.Load(ds.CreateDataReader(), LoadOption.OverwriteChanges, tableNames);

                    rel.DataSource = dsRel;
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
                //DevExpress.XtraPrinting.PdfExportOptions op =
                //    new DevExpress.XtraPrinting.PdfExportOptions()
                //    {
                //        Compressed = true,
                //        ConvertImagesToJpeg = true,
                //        ImageQuality = DevExpress.XtraPrinting.PdfJpegImageQuality.Lowest
                //    };
                rel.CreateDocument();

                rel.PrintingSystem.ExportToPdf(stream);
                byte[] arquivo = stream.GetBuffer();

                cDados.publicaStatusReport(codigoStatusReport, arquivo);*/

                #endregion

                string iniciaisModeloControladoSistema = gvDados.GetRowValuesByKeyValue(
                    codigoStatusReport, "IniciaisModeloControladoSistema") as string;
                XtraReport rel;
                DataTable dtParam = cDados.getParametrosSistema("modeloBoletimStatus", "urlCapaBAE").Tables[0];
                object parametro = null;
                string urlCapaBae = string.Empty;
                if (dtParam.Rows.Count > 0)
                {
                    DataRow dr = dtParam.AsEnumerable().First();
                    parametro = dr["modeloBoletimStatus"];
                    urlCapaBae = dr["urlCapaBAE"] as string;
                }

                if (parametro == null || Convert.IsDBNull(parametro))
                    parametro = 1;

                rel = ObtemInstanciaBoletim(codigoStatusReport,
                    iniciaisModeloControladoSistema, Convert.ToInt32(parametro));
                rel.CreateDocument();
                if (rel is rel_BoletimAcoesEstrategicasVisao && !string.IsNullOrWhiteSpace(urlCapaBae))
                    InserCapaRelatorio((rel_BoletimAcoesEstrategicasVisao)rel, Server.MapPath(urlCapaBae));

                PdfExportOptions op = new PdfExportOptions();
                rel.PrintingSystem.ExportToPdf(stream, op);
                byte[] arquivo = stream.GetBuffer();

                cDados.publicaStatusReport(codigoStatusReport, arquivo);
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }

    private void InserCapaRelatorio(rel_BoletimAcoesEstrategicasVisao rel, string urlCapaBae)
    {
        rel_CapaBAE capa = new rel_CapaBAE();
        capa.ParamDataInicioPeriodoRelatorio.Value = rel.ParamDataInicioPeriodoRelatorio.Value;
        capa.ParamUrlImagemCapaBAE.Value = urlCapaBae;
        capa.CreateDocument();
        rel.Pages.Insert(0, capa.Pages[0]);
    }

    private XtraReport ObtemInstanciaBoletim(int codigoStatusReport, string iniciais, int parametroModeloBoletimStatus)
    {
        XtraReport report = null;
        if (iniciais == "BLTQ")
        {
            if (parametroModeloBoletimStatus == 1)
                report = new rel_BoletimStatusNacional(codigoStatusReport);
            else if (parametroModeloBoletimStatus == 2)
                report = new rel_BoletimStatusBahia(codigoStatusReport);
            else
                report = new rel_BoletimStatus(codigoStatusReport);
        }
        else if (iniciais == "BLT_AE_UN")
            report = new rel_BoletimAcoesEstrategicasUnidade(codigoStatusReport);
        else if (iniciais == "BLT_AE_VI")
            report = new rel_BoletimAcoesEstrategicasVisao(codigoStatusReport);
        else if (iniciais == "GRF_BOLHAS")
        {
            RelGraficoBolha relGRF_BOLHAS = new RelGraficoBolha();
            relGRF_BOLHAS.ParamCodigoStatusReport.Value = codigoStatusReport;
            report = relGRF_BOLHAS;
        }
        else if (iniciais == "BLT_RAPU")
        {
            rel_AcompanhamentoProjetosUnidade rel_RAPU = new rel_AcompanhamentoProjetosUnidade(codigoUsuarioResponsavel);
            rel_RAPU.pCodigoStatusReport.Value = codigoStatusReport;
            report = rel_RAPU;
        }
        else if (iniciais == "SR_MDL0007")
        {
            rel_StatusReport0007 rel0007 = new rel_StatusReport0007();
            rel0007.pCodigoStatusReport.Value = codigoStatusReport;
            report = rel0007;
        }
        else if (iniciais == "SR_PPJ01")
        {
            RelPlanoProjeto_001 rel_ppj = new RelPlanoProjeto_001();
            rel_ppj.pCodigoStatusReport.Value = codigoStatusReport;
            report = rel_ppj;
        }
        else if (iniciais == "PADRAONOVO")
        {
            rel_StatusReportNovoPadrao rel_novo = new rel_StatusReportNovoPadrao();
            rel_novo.pCodigoStatusReport.Value = codigoStatusReport;
            report = rel_novo;
        }
        else
        {
            report = new rel_StatusReport();
            string comandoSql = string.Format(@" exec [dbo].[p_getDadosStatusReportPadrao] {0} ", codigoStatusReport);
            DataSet ds = cDados.getDataSet(comandoSql);
            DataSet dsRel = (DataSet)report.DataSource;
            string[] tableNames = new string[] { "StatusReport", "TarefasConcluidas", "TarefasAtrasadas", "TarefasProximoPeriodo", "ItensCusto", "ItensReceita", "Marcos", "Riscos", "Questoes", "TarefasToDoList", "Metas", "Contratos" };
            dsRel.Load(ds.CreateDataReader(), LoadOption.OverwriteChanges, tableNames);
            report.DataSource = dsRel;
        }
        return report;
    }

    private string persisteEnvioRelatorioDestinatarios()
    {
        string msgErro = string.Empty;
        int codigoStatusReport = int.Parse(getChavePrimaria());

        try
        {
            DataSet ds = cDados.enviaStatusReportAosDestinatarios(codigoEntidadeUsuarioResponsavel, codigoStatusReport, codigoUsuarioResponsavel);

            if (ds.Tables[0].Rows[0]["Resultado"].Equals("Sucesso"))
            {
                DataRow row = ds.Tables[1].Rows[0];
                string modeloStatusReport = string.Format("{0} - {1}"
                    , row["DescricaoModeloStatusReport"]
                    , row["DescricaoObjeto"]);
                string assunto = modeloStatusReport;

                string[] Destinatarios = row["Destinatarios"].ToString().Split(';');
                string destinatarios = string.Empty;
                string copia = string.Empty;
                if (!string.IsNullOrWhiteSpace(Destinatarios[0]) )
                {
                    destinatarios = Destinatarios[0];
                    int contDestinatariosCopia = 0;
                    foreach (var item in Destinatarios)
                    {
                        if (contDestinatariosCopia != 0)
                        {
                            copia += item + (Destinatarios.Length == contDestinatariosCopia+1 ? "" : ";");
                        }
                        contDestinatariosCopia++;
                    }
                    
                }
                else
                {
                    msgErro = "Favor informar detinatários para o relatório";
                }


                string nomeCompletoAnexo = "";

                string dataGeracao = row["DataGeracao"].ToString();
                try
                {
                    nomeCompletoAnexo = getNomeCompletoArquivoTemporario(row["ConteudoStatusReport"] as byte[]);
                }
                catch (Exception)
                {
                    nomeCompletoAnexo = "";
                }



                string nomeSistema = "";// cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "tituloPaginasWEB").Tables[0].Rows[0]["tituloPaginasWEB"].ToString();
                //bool possuiApenasUmDestinatario = copia.Split('@').Length <= 2;
                //if (possuiApenasUmDestinatario)
                //    destinatarios = copia;

                DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "tituloPaginasWEB", "labelQuestao", "labelQuestoes");
                if (dsParametros.Tables[0].Rows.Count > 0)
                {
                    nomeSistema = dsParametros.Tables[0].Rows[0]["tituloPaginasWEB"].ToString();
                }
                //                string mensagem = string.Format(@"Prezado(a), <br><br>Segue em anexo {4} '{0}' para o período de {1:dd/MM/yyyy} a {2:dd/MM/yyyy}. <br><br>Atenciosamente, <br> {3}
                //                                  ", nomeSistema, dataGeracao,  modeloStatusReport);

                // Segue em anexo {0} emitido em {1}.A ata de reunião encontra-se no {1} para consulta.
                
                string mensagem = string.Format(Resources.traducao.HistoricoStatusReport_prezado_a____br__br_segue_em_anexo__0__emitido_em__1___a_ata_de_reuni_o_encontra_se_no__2__para_consulta_______br_, modeloStatusReport, dataGeracao, nomeSistema);
                
                int retornoStatus = 0;

    
                    string msgEmail = cDados.enviarEmailSistemaNetSend(assunto, destinatarios, copia, mensagem, nomeCompletoAnexo, "", ref retornoStatus);
          

                

                if (retornoStatus == 0)
                    msgErro = msgEmail;
            }
            else
            {
                msgErro = Resources.traducao.HistoricoStatusReport_n_o_foi_poss_vel_enviar_o_relat_rio_de_status_aos_destinat_rios_;
            }
        }
        catch (Exception ex)
        {
            msgErro = ex.Message;
        }
        return msgErro;
    }

    private string getNomeCompletoArquivoTemporario(byte[] imagem)
    {
        string nomeArquivo = "StatusReport.pdf";
        string arquivo = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "ArquivosTemporarios\\" + nomeArquivo;
        string arquivo2 = "~/ArquivosTemporarios/" + nomeArquivo;
        FileStream fs = new FileStream(arquivo, FileMode.Create, FileAccess.Write);
        fs.Write(imagem, 0, imagem.Length);
        fs.Close();

        return arquivo;
    }

    protected void pnCallbackMensagem_Callback(object sender, CallbackEventArgsBase e)
    {
        pnCallbackMensagem.JSProperties["cp_LastOperation"] = e.Parameter;
        pnCallbackMensagem.JSProperties["cp_OperacaoOk"] = "";
        string mensagemErro_Persistencia = "";

        switch (e.Parameter)
        {
            case "Enviar":
                mensagemErro_Persistencia = persisteEnvioRelatorioDestinatarios();
                break;
            case "Publicar":
                mensagemErro_Persistencia = persistePublicacaoRelatorio();
                break;
            case "Editar":
                mensagemErro_Persistencia = persisteEdicaoComentarios();
                break;
            case "GerarRelatorio":
                //mensagemErro_Persistencia = persisteEdicaoComentarios();
                break;
        }
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallbackMensagem.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }

    private ListDictionary getDadosFormulario()
    {
        // Lê as informações disponíveis no formulário
        ListDictionary oDadosFormulario = new ListDictionary();
        //  oDadosFormulario.Add("DescricaoRiscoPadrao", txtRisco.Text);
        oDadosFormulario.Add("CodigoUsuarioInclusao", codigoUsuarioResponsavel);
        oDadosFormulario.Add("CodigoEntidade", 1);
        oDadosFormulario.Add("DataInclusao", DateTime.Now.Date.ToString());
        return oDadosFormulario;
    }

    #endregion

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        Download();
    }

    protected void Button_Load(object sender, EventArgs e)
    {
        ASPxButton btn = (ASPxButton)sender;
        GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)btn.NamingContainer;
        //cDados.traduzControles(container, new System.Web.UI.Control[] { btn });
        int indexLinha = container.VisibleIndex;
        bool publicado = !Convert.IsDBNull(gvDados.GetRowValues(indexLinha, "DataPublicacao"));
        bool enviado = !Convert.IsDBNull(gvDados.GetRowValues(indexLinha, "DataEnvioDestinatarios"));
        bool possuiDestinatarios = gvDados.GetRowValues(indexLinha, "PossuiDestinatarios").Equals("S");
        bool mostraBotaoDestaque = gvDados.GetRowValues(indexLinha, "IniciaisModeloControladoSistema").Equals("BLT_AE_VI");

        switch (btn.ID)
        {
            case "btnExcluir":
                btn.Visible = podeExcluirRelatorio;
                if (btn.Visible && btn.Enabled)
                    btn.ClientSideEvents.Click = @"function(s, e) {e.processOnServer = false;btnExcluir_Click(" + indexLinha + ");}";
                break;
            case "btnDownLoad":
                if (!publicado)
                {
                    btn.ClientEnabled = false;
                    btn.Image.Url = "~/imagens/botoes/btnPDFDes.png";
                    btn.ToolTip = Resources.traducao.HistoricoStatusReport_o_relat_rio_ainda_n_o_foi_publicado;
                }
                break;
            case "btnEditar":
                if (!podeEditarComentarios)
                {
                    btn.ClientEnabled = false;
                    btn.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
                else if (publicado)
                {
                    btn.ClientEnabled = false;
                    btn.Image.Url = "~/imagens/botoes/editarRegDes.png";
                    btn.ToolTip = Resources.traducao.HistoricoStatusReport_n_o___poss_vel_editar_um_relat_rio_j__publicado;
                }
                else
                {
                    btn.ClientSideEvents.Click = @"function(s, e) {e.processOnServer = false;btnEditar_Click(" + indexLinha + ");}";
                }
                break;
            case "btnDestaquesMes":

                btn.ClientVisible = mostraBotaoDestaque;

                if (!podeEditarComentarios)
                {
                    btn.ClientEnabled = false;
                    btn.Image.Url = "~/imagens/botoes/btnEstrelaDes.png";
                }
                else if (publicado)
                {
                    btn.ClientEnabled = false;
                    btn.Image.Url = "~/imagens/botoes/btnEstrelaDes.png";
                    btn.ToolTip = Resources.traducao.HistoricoStatusReport_n_o___poss_vel_inserir_destaques_em_um_relat_rio_j__publicado;
                }

                else
                {
                    btn.ClientSideEvents.Click = @"function(s, e) {e.processOnServer = false;btnDestaque_Click(" + indexLinha + ");}";
                }

                break;


            case "btnEnviar":
                if ("CP".Equals(iniciaisTipoObjeto))
                {
                    btn.ClientVisible = false;
                }
                else
                {
                    if (!podeEnviarDestinatarios)
                    {
                        btn.ClientEnabled = false;
                        btn.Image.Url = "~/imagens/botoes/btnEncaminharDes.png";
                    }
                    //if (enviado)
                    //{
                    //    btn.ClientEnabled = false;
                    //    btn.Image.Url = "~/imagens/enviarDes.png";
                    //    btn.ToolTip = "Relatório já enviado aos destinatários";
                    //}
                    //else
                    if (!publicado)
                    {
                        btn.ClientEnabled = false;
                        btn.Image.Url = "~/imagens/botoes/btnEncaminharDes.png";
                        btn.ToolTip = Resources.traducao.HistoricoStatusReport_o_relat_rio_ainda_n_o_foi_publicado;
                    }
                    else if (!possuiDestinatarios)
                    {
                        btn.ClientEnabled = false;
                        btn.Image.Url = "~/imagens/botoes/btnEncaminharDes.png";
                        btn.ToolTip = Resources.traducao.HistoricoStatusReport_n_o___poss_vel_enviar_o_relat_rio_pois_o_mesmo_n_o_possui_destinat_rios;
                    }
                    if (btn.ClientVisible && btn.ClientEnabled)
                    {
                        btn.ClientSideEvents.Click = @"function(s, e) {
    e.processOnServer = false;
    gvDados.SetFocusedRowIndex(" + indexLinha + @");
    btnEnviar_Click();
}";
                    }
                }
                break;
        }
    }

    private void populaCombos()
    {
        DataSet ds = cDados.ObtemModelosStatusReport(codigoEntidadeUsuarioResponsavel, idObjeto, iniciaisTipoObjeto);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            cDados.PopulaDropDownASPx(this, ds.Tables[0], "CodigoModeloStatusReport", "DescricaoModeloStatusReport", "", ref ddlModeloStatusReport);
        }
    }

    protected void gvDados_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
    {
        if (e.Column.FieldName == "DescricaoModeloStatusReport")
        {
            DataTable dt = ((DataTable)gvDados.DataSource).Copy();
            string s1 = dt.Rows[e.ListSourceRowIndex1]
                ["IniciaisModeloControladoSistema"].ToString();
            string s2 = dt.Rows[e.ListSourceRowIndex2]
                ["IniciaisModeloControladoSistema"].ToString();
            e.Result = Comparer.Default.Compare(s1, s2);
            e.Handled = true;
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "HisSttRepPrj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcSelecaoModeloStatusReport);TipoOperacao = 'Incluir';", true, true, false, "HisSttRepPrj", "Histórico Status Report", this);
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
