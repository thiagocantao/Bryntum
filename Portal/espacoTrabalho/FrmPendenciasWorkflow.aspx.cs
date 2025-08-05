using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;

public partial class espacoTrabalho_PendenciasWorkflow : System.Web.UI.Page
{
    dados cDados;

    public int codigoUsuarioResponsavel;
    public int codigoEntidadeUsuarioResponsavel;

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

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        populaGrid();

        cDados.aplicaEstiloVisual(this);
        defineAlturaTela();

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/PendenciasWorkflow.js""></script>"));
        this.TH(this.TS("PendenciasWorkflow"));
        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

    }

    private void populaGrid()
    {
         string comandoSQL = string.Format(@"
            BEGIN
                DECLARE @CodigoFluxo as int
                DECLARE @IniciaisFluxo as varchar(18)
                DECLARE @isMobile as bit

                SET @isMobile = {0}
				SET @CodigoFluxo = NULL
				SET @IniciaisFluxo = NULL

                SELECT @IniciaisFluxo = Valor FROM ParametroConfiguracaoSistema 
                WHERE Parametro = 'iniciaisFluxoTelaPendencia' AND CodigoEntidade = " + codigoEntidadeUsuarioResponsavel + @"
                IF(@isMobile = 1)
                BEGIN
                    SELECT @CodigoFluxo = CodigoFluxo 
                     FROM Fluxos WHERE IniciaisFluxo =  @IniciaisFluxo
                END
               EXEC [dbo].[p_wf_obtemListaInstanciasUsuario] 
                  @in_identificadorUsuario	= '" + codigoUsuarioResponsavel + @"'
                , @in_codigoEntidade		= " + codigoEntidadeUsuarioResponsavel + @"
                , @in_codigoFluxo			= @CodigoFluxo
                , @in_codigoProjeto 		= NULL
                ,@in_somenteInteracao       = 1
                ,@in_somentePendencia       = 1
                ,@in_palavraChave           = NULL
            END", cDados.isMobileBrowser() == true ? "1" : "0");

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
            
            //((GridViewCommandColumn)gvDados.Columns[0]).CustomButtons[0].Visibility = GridViewCustomButtonVisibility.BrowsableRow;
        }

    }

    #region VARIOS
        
    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));

        alturaPrincipal -= 460;         

        gvDados.Settings.VerticalScrollableHeight = alturaPrincipal;
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "PndFlxUsu");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, true, "PndFlxUsu", "Pendências de Workflow", this);

        if (!IsPostBack)
        {
            if(Request.QueryString["PND"] + "" == "S")
               gvDados.FilterExpression = " [PossuiNotificacao] = 'Sim'";

            if (Request.QueryString["ATR"] + "" == "S")
            {
                if (Request.QueryString["PND"] + "" == "S")
                    gvDados.FilterExpression += " AND [IndicaAtraso] = 'Sim'";
                else
                    gvDados.FilterExpression = " [IndicaAtraso] = 'Sim'";
            }
        }
    }

    #endregion

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "PossuiPendenciaInteracao" && !pnCallback.IsCallback)
        {
            string codigoWorkflow = gvDados.GetRowValues(e.VisibleIndex, "CodigoWorkflow").ToString();
            string codigoInstancia = gvDados.GetRowValues(e.VisibleIndex, "CodigoInstanciaWf").ToString();
            string codigoEtapa = gvDados.GetRowValues(e.VisibleIndex, "CodigoEtapaAtual").ToString();
            string codigoFluxo = gvDados.GetRowValues(e.VisibleIndex, "CodigoFluxo").ToString();
            string ocorrencia = gvDados.GetRowValues(e.VisibleIndex, "OcorrenciaAtual").ToString();
            string possuiPendenciaInteracao = gvDados.GetRowValues(e.VisibleIndex, "PossuiPendenciaInteracao").ToString();
                        
            ASPxComboBox ddlAcao = gvDados.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "ddlAcao") as ASPxComboBox;

            ddlAcao.ClientInstanceName = "ddl_" + codigoInstancia;

            // busca as ações a etapa especificada (apenas ações de usuários 'U'
            string comandoSQL = string.Format(
                @"SELECT * 
                        FROM AcoesEtapasWf 
                       WHERE CodigoWorkflow = {0} 
                         AND codigoEtapaWF = {1} AND [TipoAcao] = 'U'", codigoWorkflow, codigoEtapa);

            DataSet ds = cDados.getDataSet(comandoSQL);

            ddlAcao.DataSource = ds;
            ddlAcao.TextField = "TextoAcao";
            ddlAcao.ValueField = "CodigoAcaoWF";
            ddlAcao.DataBind();

            ddlAcao.JSProperties["cp_PossuiPendencias"] = possuiPendenciaInteracao.ToUpper() == "SIM" ? "S" : "N";

            ListEditItem lei = new ListEditItem(" ", "-1");
            ddlAcao.Items.Insert(0, lei);

            if (!IsPostBack)
            {
                ddlAcao.SelectedIndex = 0;
                hfGeral.Add("Cmb_" + codigoInstancia, ddlAcao.Value);
            }

            ddlAcao.ClientSideEvents.Init = @"function(s, e){s.SetValue(hfGeral.Get('Cmb_" + codigoInstancia + "'));}";

            ddlAcao.ClientSideEvents.ValueChanged = @"function(s, e){hfGeral.Set('Cmb_" + codigoInstancia + "', s.GetValue());}";

            ddlAcao.ClientSideEvents.Validation = @"
                function(s, e)
                {
                    e.isValid = s.cp_PossuiPendencias == 'N' || s.GetValue() == -1;
                }";

            ddlAcao.ValidationSettings.CausesValidation = true;
            ddlAcao.ValidationSettings.Display = Display.Dynamic;
            ddlAcao.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
            ddlAcao.ValidationSettings.ErrorText = "Existem pendências na etapa atual, clique em interagir para resolvê-las!";
            ddlAcao.ValidationSettings.ValidationGroup = "MKE";
        }
    }
    
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
        else if (e.RowType == GridViewRowType.Header)
        {
            if (e.Column.Name == "Acao")
                e.Text = "Possui Pendências?";
        }
    }

    private bool processaAcaoWorkflow(int usuario, int workflow, long instanciaWf, int seqEtapa, int etapa, int acao, out string mensagemErro)
    {
        bool bRet;
        string comandoSQL = "";
        try
        {
            comandoSQL = string.Format(
                @"
                DECLARE @RC                             int 
                DECLARE @in_codigoWorkFlow              int 
                DECLARE @in_codigoInstanciaWf           bigint 
                DECLARE @in_SequenciaOcorrenciaEtapaWf  int 
                DECLARE @in_codigoEtapaWf               int 
                DECLARE @in_codigoAcaoWf                int 
                DECLARE @in_identificadorUsuarioAcao    varchar(50)
                DECLARE @in_opcoes                      int 
                DECLARE @ou_resultado                   int 
                DECLARE @ou_codigoRetorno               int 
                DECLARE @ou_mensagemErro                nvarchar(2048) 

                SET @in_codigoWorkFlow              = {2} 
                SET @in_codigoInstanciaWf           = {3} 
                SET @in_SequenciaOcorrenciaEtapaWf  = {4} 
                SET @in_codigoEtapaWf               = {5} 
                SET @in_codigoAcaoWf                = {6} 
                SET @in_identificadorUsuarioAcao    = '{7}'
                SET @in_opcoes						= {8} 

                EXECUTE @RC = {0}.{1}.[p_processaAcaoWorkflow] 
                   @in_codigoWorkFlow               = @in_codigoWorkFlow 
                  ,@in_codigoInstanciaWf            = @in_codigoInstanciaWf 
                  ,@in_SequenciaOcorrenciaEtapaWf   = @in_SequenciaOcorrenciaEtapaWf 
                  ,@in_codigoEtapaWf                = @in_codigoEtapaWf 
                  ,@in_codigoAcaoWf                 = @in_codigoAcaoWf 
                  ,@in_identificadorUsuarioAcao     = @in_identificadorUsuarioAcao
                  ,@in_opcoes                       = @in_opcoes 
                  ,@ou_resultado                    = @ou_resultado                 OUTPUT 
                  ,@ou_codigoRetorno                = @ou_codigoRetorno             OUTPUT 
                  ,@ou_mensagemErro                 = @ou_mensagemErro              OUTPUT 

                SELECT 
                        @RC									AS RetornoProc,  
                        @ou_resultado				AS Resultado, 
                        @ou_codigoRetorno		AS CodigoRetorno, 
                        @ou_mensagemErro		AS MensagemErro "
                           , ""
                           , ""
                           , workflow, instanciaWf, seqEtapa, etapa, acao, usuario, 0);

            DataSet ds = cDados.getDataSet(comandoSQL);

            string msgWhere = "", msgWhat = "";
            int retornoProc, resultado, codigoRetorno;
            string msgProc;

            retornoProc = int.Parse(ds.Tables[0].Rows[0]["RetornoProc"].ToString());
            resultado = int.Parse(ds.Tables[0].Rows[0]["Resultado"].ToString());
            codigoRetorno = int.Parse(ds.Tables[0].Rows[0]["CodigoRetorno"].ToString());
            msgProc = ds.Tables[0].Rows[0]["MensagemErro"].ToString();

            if (0 == retornoProc)
            {
                mensagemErro = "";
                bRet = true;
            }
            else
            {
                if ((16 & resultado) > 0)
                    msgWhere = "no processamento de tempo limite da etapa";
                else if ((32 & resultado) > 0)
                    msgWhere = "na alteração da etapa do processo";
                else if ((64 & resultado) > 0)
                    msgWhere = "na definição da próxima etapa do processo";
                else if (((4 & resultado) > 0) || ((8 & resultado) > 0))
                    msgWhere = "na execução de ações automáticas da etapa do processo";
                else if (((1 & resultado) > 0) || ((2 & resultado) > 0))
                    msgWhere = "no envio de notificações";
                else if ((1024 & resultado) > 0)
                    msgWhere = "no acionamento de API's externas configuradas na etapa";

                switch (codigoRetorno)
                {
                    case 1:
                    case 2:
                        msgWhat = @"As informações na base de dados encontram-se inconsistentes.";
                        break;

                    case 3:
                    case 4:
                        msgWhat = @"A configuração dos parâmetros para execução de procedimentos no servidor ficou incorreta.";
                        break;
                    case 5:
                        msgWhat = @"A etapa do processo foi alterada por outro usuário. Acesse o processo novamente para ver as informações atualizadas";
                        break;
                    case 6:
                        msgWhat = @"A ação executada não produziu efeito e não foi possível determinar a causa.";
                        break;
                    case 7:
                        msgWhat = @"O usuário não possui permissão para executar a ação.";
                        break;
                    case 8:
                        msgWhat = @"Houve um erro durante o processamento e não foi possível determinar a causa.";
                        break;
                    default:
                        msgWhat = string.Format(@"Falha na execução do procedimento no servidor de banco de dados. (errorcode: {0}:{1})", codigoRetorno, retornoProc);
                        break;

                }

                if (msgProc.Length > 0)
                {
                    msgWhat += " Mensagem original do erro: " + msgProc;
                }

                mensagemErro = string.Format(@"Falha {0}.{1}.", msgWhere, msgWhat);
                bRet = false;
            }
        }
        catch (Exception ex)
        {
            mensagemErro = string.Format(@"Falha ao executar procedimento no servidor. Mensagem original do erro: {0}", ex.Message);
            bRet = false;
        }

        return bRet;
    }
       
    protected void pnCallback_Callback(object sender, CallbackEventArgsBase e)
    {
        string msgRetorno = "";
        for (int i = 0; i < gvDados.VisibleRowCount; i++)
        {

            if (gvDados.IsGroupRow(i) == false)
            {//CodigoWorkflow;CodigoInstanciaWf;CodigoFluxo;OcorrenciaAtual;CodigoEtapaAtual
                int codigoWorkflow = int.Parse(gvDados.GetRowValues(i, "CodigoWorkflow").ToString());
                long codigoInstanciaWf = long.Parse(gvDados.GetRowValues(i, "CodigoInstanciaWf").ToString());
                int codigoSequencia = int.Parse(gvDados.GetRowValues(i, "OcorrenciaAtual").ToString());
                int codigoEtapaAtual = int.Parse(gvDados.GetRowValues(i, "CodigoEtapaAtual").ToString());
                int codigoAcao = hfGeral.Contains("Cmb_" + codigoInstanciaWf) ? int.Parse(hfGeral.Get("Cmb_" + codigoInstanciaWf).ToString()) : -1;
                string mensagemErro = "";
                if (codigoAcao != -1)
                {
                    processaAcaoWorkflow(codigoUsuarioResponsavel, codigoWorkflow, codigoInstanciaWf, codigoSequencia, codigoEtapaAtual, codigoAcao, out mensagemErro);

                    msgRetorno += mensagemErro + Environment.NewLine;
                }

                if (mensagemErro.Trim() == "")
                    hfGeral.Set("Cmb_" + codigoInstanciaWf, "-1");
            }
        }

        if (msgRetorno.Trim() == "")
        {
            msgRetorno = "Execução realizada com sucesso!";
            pnCallback.JSProperties["cp_Status"] = "OK";
        }
        else
            pnCallback.JSProperties["cp_Status"] = "ERRO";

        pnCallback.JSProperties["cp_Msg"] = msgRetorno;
    }

    public string getRowCount()
    {
        string retorno = "";
        int quantidadeLinhas = 0;
        for (int i = 0; i < gvDados.VisibleRowCount; i++)
        {
            if (!gvDados.IsGroupRow(i))
                quantidadeLinhas++;
        }

        retorno = quantidadeLinhas + " pendências";

        return retorno;
    }
}