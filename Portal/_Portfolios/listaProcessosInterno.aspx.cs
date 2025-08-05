using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using DevExpress.XtraReports.UI;
using System.IO;

public partial class _Portfolios_listaProcessosInterno : System.Web.UI.Page
{
    dados cDados;

    public int codigoUsuarioResponsavel;
    public int codigoEntidadeUsuarioResponsavel;
    public int _codigoFluxo;
    public int _codigoProjeto;
    public int _codigoWorkflow;
    public bool chamadaViaTelaDeProjetos;
    //private string telaChica = "";
    public int acessoEtapaInicial = 0;
    public string readOnly = "N", acessoPrimeiraInstancia = "S";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = new dados(listaParametrosDados);
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
        this.TH(this.TS("listaProcessosInterno"));
        string strAux;

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        _codigoFluxo = 0;
        if (null != Request.QueryString["CF"])
        {
            strAux = Request.QueryString["CF"];

            int.TryParse(strAux, out _codigoFluxo); // se não der certo, codigoFluxo continua = 0;
        }

        _codigoProjeto = 0;
        if (null != Request.QueryString["CP"])
        {
            strAux = Request.QueryString["CP"];

            int.TryParse(strAux, out _codigoProjeto); // se não der certo, codigoProjeto continua = 0;
        }

        _codigoWorkflow = 0;
        if (null != Request.QueryString["CW"])
        {
            strAux = Request.QueryString["CW"];

            int.TryParse(strAux, out _codigoWorkflow); // se não der certo, codigoWorkflow continua = 0;
        }

        if (null != Request.QueryString["AEI"])
            acessoEtapaInicial = int.Parse(Request.QueryString["AEI"].ToString());

        if (null != Request.QueryString["RO"])
            readOnly = Request.QueryString["RO"].ToString();

        if (null != Request.QueryString["API"])
            acessoPrimeiraInstancia = Request.QueryString["API"].ToString();

        chamadaViaTelaDeProjetos = (0 != _codigoFluxo) && (0 != _codigoProjeto) && (0 != _codigoWorkflow);
        gvDados.JSProperties["cp_chamadaViaTelaDeProjetos"] = chamadaViaTelaDeProjetos;

        defineAlturaTela();
        populaGrid();
        cDados.aplicaEstiloVisual(Page);

        // removendo variável de sessão caso tenha sido criada durante uma 'interação' com algum fluxo
        // instrução necessária para evitar confusão com o código do projeto do fluxo.
        cDados.setInfoSistema("CodigoProjeto", -1);

        if (hfGeral.Contains("param"))
            Response.Redirect("~/wfEngine.aspx?RO=" + readOnly + "&" + hfGeral.Get("param").ToString());
        else
            LimpaJSProperties();
    }

    #region VARIOS

    private void LimpaJSProperties()
    {
        // mensagem de erro gerada durante um processamento callback 
        // usada no endCallBack para ser mostrada ao usuário caso tenha ocorrido;
        gvDados.JSProperties["cpCallbackMessage"] = "";
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        //int larguraTela = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        if (chamadaViaTelaDeProjetos)
        {
            alturaPrincipal -= 350;
            //gvDados.Width = larguraTela - 190;
        }
        else
        {
            alturaPrincipal -= 320;
            //gvDados.Width = larguraTela - 10;
        }


        //gvDados.Settings.VerticalScrollableHeight = alturaPrincipal;
    }

    #endregion

    #region GRID's

    private void populaGrid()
    {
        string filtro = Request.QueryString["Filtro"] + "" == "" ? "NULL" : ("'" + Request.QueryString["Filtro"].ToString().Replace("'", "''") + "'");

        string codFluxo = (0 == _codigoFluxo || Request.QueryString["Filtro"] + "" != "") ? "NULL" : _codigoFluxo.ToString();
        string codProjeto = 0 == _codigoProjeto ? "NULL" : _codigoProjeto.ToString();
        string comandoSQL = string.Format(@"EXEC [dbo].[p_wf_obtemListaInstanciasUsuario] 
                  @in_identificadorUsuario	= '{0}'
                , @in_codigoEntidade		= {1}
                , @in_codigoFluxo			= {2}
                , @in_codigoProjeto 		= {3}
                , @in_palavraChave          = {4}
                ", codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codFluxo, codProjeto, filtro);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();

            //((GridViewCommandColumn)gvDados.Columns[0]).CustomButtons[0].Visibility = GridViewCustomButtonVisibility.BrowsableRow;
        }
    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {

        gvDados.JSProperties["cpCallbackMessage"] = ""; // limpa a mensagem pós callback
        if (gvDados.FocusedRowIndex > -1)
        {
            string parametro = e.Parameters;
            string codigoWorkflow = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoWorkflow").ToString();
            string codigoInstanciaWf = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoInstanciaWf").ToString();
            string codigoEtapaAtual = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoEtapaAtual").ToString();
            string ocorrenciaAtual = gvDados.GetRowValues(gvDados.FocusedRowIndex, "OcorrenciaAtual").ToString();
            string codigoUltimaEtapa = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoUltimaEtapa").ToString();
            string ultimaOcorrencia = gvDados.GetRowValues(gvDados.FocusedRowIndex, "UltimaOcorrencia").ToString();
            string indicaSubFluxo = gvDados.GetRowValues(gvDados.FocusedRowIndex, "IndicaSubFluxoAtual").ToString();
            string NomeInstanciaWf = gvDados.GetRowValues(gvDados.FocusedRowIndex, "NomeInstanciaWf").ToString();
            string errorMessage = string.Empty;

            if (ocorrenciaAtual == "")
                ocorrenciaAtual = "1";

            string chamaTelaMenu = (Request.QueryString["TL"] + "") != "" ? "&TL=CHI" : "";

            gvDados.JSProperties["cpCallbackMessage"] = ""; // limpa a mensagem pós callback
            gvDados.JSProperties["cp_status"] = "";
            //"CW=" +Eval("CodigoWorkflow") + "&CI=" + Eval("CodigoInstanciaWf")+ "&CE=" + Eval("CodigoEtapaAtual")+ "&CS=" +Eval("OcorrenciaAtual")
            if (parametro == "btnInteragir")
            {
                // se não tiver etapa atual -> instância terminada
                if (0 == codigoEtapaAtual.Length)
                    gvDados.JSProperties["cpCallbackMessage"] = @Resources.traducao.listaProcessosInterno_n_o_ser__poss_vel_interagir_com_este_processo__motivo__processo_encerrado_;
                else
                {
                    if (indicaSubFluxo == "S")
                    {
                        if (chamadaViaTelaDeProjetos)
                        {
                            ASPxWebControl.RedirectOnCallback("GraficoProcessoInterno.aspx?" + "CW=" + codigoWorkflow + "&CI=" + codigoInstanciaWf + "&TL=CHI&CF=" + _codigoFluxo + "&CP=" + _codigoProjeto);
                        }
                        else
                        {
                            //ASPxWebControl.RedirectOnCallback("GraficoProcessoInterno.aspx?" + "CW=" + codigoWorkflow + "&CI=" + codigoInstanciaWf + "&CF=" + _codigoFluxo + "&CP=" + _codigoProjeto + chamaTelaMenu);
                        }
                    }
                    else if (0 == cDados.obtemNivelAcessoEtapaWf(int.Parse(codigoWorkflow), int.Parse(codigoInstanciaWf), int.Parse(ocorrenciaAtual), int.Parse(codigoEtapaAtual), codigoUsuarioResponsavel.ToString()))
                        gvDados.JSProperties["cpCallbackMessage"] = @Resources.traducao.listaProcessosInterno_aten__o__a_etapa_atual_em_que_o_processo_se_encontra_n_o_permite_a_intera__o_por_este_usu_rio__aguarde_at__que_o_processo_mude_de_etapa_;
                    else
                    {
                        if (chamadaViaTelaDeProjetos)
                        {
                            ASPxWebControl.RedirectOnCallback("../wfEngineInterno.aspx?CF=" + _codigoFluxo + "&CP=" + _codigoProjeto + "&CW=" + codigoWorkflow + "&CI=" + codigoInstanciaWf + "&CE=" + codigoEtapaAtual + "&CS=" + ocorrenciaAtual + chamaTelaMenu);
                        }
                        else
                        {
                            //ASPxWebControl.RedirectOnCallback("../wfEngineInterno.aspx?" + "CW=" + codigoWorkflow + "&CI=" + codigoInstanciaWf + "&CE=" + codigoEtapaAtual + "&CS=" + ocorrenciaAtual + chamaTelaMenu);
                        }


                    }
                }
            }
            else if (parametro == "btnHistorico")
            {
                //ASPxWebControl.RedirectOnCallback("historicoProcessoInterno.aspx?" + "CW=" + codigoWorkflow + "&CI=" + codigoInstanciaWf);
            }

            else if (parametro == "btnGrafico")
            {
                if (chamadaViaTelaDeProjetos)
                    ASPxWebControl.RedirectOnCallback("GraficoProcessoInterno.aspx?" + "CW=" + codigoWorkflow + "&CI=" + codigoInstanciaWf + "&TL=CHI&CF=" + _codigoFluxo + "&CP=" + _codigoProjeto);
                //else
                //    ASPxWebControl.RedirectOnCallback("GraficoProcessoInterno.aspx?" + "CW=" + codigoWorkflow + "&CI=" + codigoInstanciaWf + "&CF=" + _codigoFluxo + "&CP=" + _codigoProjeto + chamaTelaMenu);
                gvDados.JSProperties["cp_CW"] = codigoWorkflow;
                gvDados.JSProperties["cp_CI"] = codigoInstanciaWf;
                gvDados.JSProperties["cp_CF"] = _codigoFluxo;
                gvDados.JSProperties["cp_CP"] = _codigoProjeto;


            }

            else if (parametro == "btnReverter")
            {
                string idUsuario = cDados.getInfoSistema("IDUsuarioLogado").ToString();
                if (true == cDados.reverteAcaoWorkflow(int.Parse(codigoWorkflow), long.Parse(codigoInstanciaWf), int.Parse(ocorrenciaAtual), codigoEtapaAtual, idUsuario, out errorMessage))
                {
                    gvDados.JSProperties["cpCallbackMessage"] = Resources.traducao.listaProcessosInterno_processo_revertido_;
                    gvDados.JSProperties["cp_status"] = "ok";
                    populaGrid();
                }
                else
                    gvDados.JSProperties["cpCallbackMessage"] = errorMessage;
            }

            else if (parametro == "btnCancelar")
            {
                string idUsuario = cDados.getInfoSistema("IDUsuarioLogado").ToString();
                if (true == cDados.cancelaInstanciaWorkflow(int.Parse(codigoWorkflow), long.Parse(codigoInstanciaWf), int.Parse(ocorrenciaAtual), int.Parse(codigoEtapaAtual), idUsuario, out errorMessage))
                {
                    gvDados.JSProperties["cpCallbackMessage"] = Resources.traducao.listaProcessosInterno_processo_cancelado_;
                    gvDados.JSProperties["cp_status"] = Resources.traducao.listaProcessosInterno_ok;
                    populaGrid();
                }
                else
                    gvDados.JSProperties["cpCallbackMessage"] = errorMessage;
            }

            else if (parametro == "btnUltimaEtapa")
            {
                if (chamadaViaTelaDeProjetos)
                    ASPxWebControl.RedirectOnCallback("../wfEngineInterno.aspx?CF=" + _codigoFluxo + "&CP=" + _codigoProjeto + "&CW=" + codigoWorkflow + "&CI=" + codigoInstanciaWf + "&CE=" + codigoUltimaEtapa + "&CS=" + ultimaOcorrencia + chamaTelaMenu);
                else
                    ASPxWebControl.RedirectOnCallback("../wfEngineInterno.aspx?" + "CW=" + codigoWorkflow + "&CI=" + codigoInstanciaWf + "&CE=" + codigoUltimaEtapa + "&CS=" + ultimaOcorrencia + chamaTelaMenu);
            }
            else if (parametro == "btnResumoProcesso")
            {
                Session["exportStream"] = null;
                relResumoProcessos rel = new relResumoProcessos(codigoEntidadeUsuarioResponsavel, int.Parse(codigoWorkflow), int.Parse(codigoInstanciaWf));
                DataSet dsTemporario = cDados.getResumoProcessosReport(int.Parse(codigoWorkflow), int.Parse(codigoInstanciaWf));
                rel.Parameters["pCodigoWorkflow"].Value = codigoWorkflow;
                rel.Parameters["pCodigoInstanciaWorkFlow"].Value = codigoInstanciaWf;
                rel.Parameters["pNomeInstanciaWf"].Value = NomeInstanciaWf;
                rel.Parameters["pDataImpressao"].Value = DateTime.Now.ToLocalTime();

                MemoryStream stream = new MemoryStream();
                rel.ExportToPdf(stream);
                Session["exportStream"] = stream;
                gvDados.JSProperties["cpCallbackMessage"] = "export";
            }
            else if (parametro != "" && parametro.Substring(0, 2) == "CW")
            {
                string[] aParametros = parametro.Split('!');
                string CodigoWorkflow = aParametros[0].Substring(2);
                string CodigoInstanciaWf = aParametros[1].Substring(2);
                string CodigoEtapaAtual = aParametros[2].Substring(2);
                string OcorrenciaAtual = aParametros[3].Substring(2);

                cDados.setInfoSistema("CodigoWorkflow", CodigoWorkflow);
                cDados.setInfoSistema("CodigoInstanciaWf", CodigoInstanciaWf);
                cDados.setInfoSistema("CodigoEtapaAtual", CodigoEtapaAtual);
                cDados.setInfoSistema("OcorrenciaAtual", OcorrenciaAtual);
            }
        }
    }

    public bool podeIniciarNovaInstancia()
    {
        bool bPode = ((chamadaViaTelaDeProjetos == true) && ((2 & acessoEtapaInicial) > 0) && (acessoPrimeiraInstancia == "S") &&
            (cDados.podeIniciarNovaInstanciaFluxo(_codigoFluxo, _codigoProjeto) == true));
        return bPode;
    }

    protected void gvDados_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
    {
        populaGrid();
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        //if (gvDados.VisibleRowCount == 0)
        //    return;

        if (e.CellType == GridViewTableCommandCellType.Filter)
        {
            e.Visible = DevExpress.Utils.DefaultBoolean.False;
            return;
        }

        if (e.ButtonID == "btnInteragir")
        {
            int nivelAcessoEtapa = int.Parse(gvDados.GetRowValues(e.VisibleIndex, "NivelAcessoEtapaAtual").ToString());
            string possoInteragir = gvDados.GetRowValues(e.VisibleIndex, "IndicaInteracaoEtapaAtual").ToString().ToUpper();

            if ((possoInteragir == "SIM" || possoInteragir == "S") && readOnly == "N")
            {
                e.Image.Url = "../imagens/botoes/interagir.png";
                e.Text = "Interagir";
                e.Visible = DevExpress.Utils.DefaultBoolean.True;
            }
            else if ((1 & nivelAcessoEtapa) > 0)
            {
                e.Image.Url = "../imagens/botoes/btnVisualizarProcesso.png";
                e.Text = "Visualizar Etapa Atual";
                e.Visible = DevExpress.Utils.DefaultBoolean.True;
            }
            else
                e.Visible = DevExpress.Utils.DefaultBoolean.False;
        }
        else if (e.ButtonID == "btnCancelar")
        {
            string indicaPermissao = gvDados.GetRowValues(e.VisibleIndex, "IndicaPermissaoCancelamento").ToString();
            if (indicaPermissao == "S")
                e.Visible = DevExpress.Utils.DefaultBoolean.True;
            else
                e.Visible = DevExpress.Utils.DefaultBoolean.False;
        }
        else if (e.ButtonID == "btnReverter")
        {
            string indicaPermissao = gvDados.GetRowValues(e.VisibleIndex, "IndicaPermissaoReversao").ToString();
            if (indicaPermissao == "S")
                e.Visible = DevExpress.Utils.DefaultBoolean.True;
            else
                e.Visible = DevExpress.Utils.DefaultBoolean.False;
        }

        else if (e.ButtonID == "btnUltimaEtapa")
        {
            int nivelAcessoEtapa = int.Parse(gvDados.GetRowValues(e.VisibleIndex, "NivelAcessoUltimaEtapa").ToString());
            if ((1 & nivelAcessoEtapa) > 0)
                e.Visible = DevExpress.Utils.DefaultBoolean.True;
            else
                e.Visible = DevExpress.Utils.DefaultBoolean.False;
        }
        else if (e.ButtonID == "btnResumoProcesso")
        {
            e.Visible = (System.Threading.Thread.CurrentThread.CurrentCulture.Name.StartsWith("pt", StringComparison.InvariantCultureIgnoreCase) == true ?
                DevExpress.Utils.DefaultBoolean.True :
                DevExpress.Utils.DefaultBoolean.False);
        }
    }

    #endregion

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        e.Editor.ToolTip = Resources.traducao.listaProcessosInterno_digite_a_palavra_a_ser_pesquisada_e_pressione__enter_;
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = new dados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "LstFlxUsu");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = new dados(listaParametrosDados);

        bool confirmaInstancia = false;

        string funcaoJS = @"abreInclusaoFluxo('" + _codigoFluxo + "', '" + _codigoProjeto + "', '" + acessoEtapaInicial + "', '" + _codigoWorkflow + "')";

        string comandoSQL = string.Format(@"
        SELECT ISNULL(CriaInstanciaAutomatica, 'N') AS CriaInstanciaAutomatico
          FROM Fluxos
         WHERE CodigoFluxo = {0}", _codigoFluxo);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            confirmaInstancia = ds.Tables[0].Rows[0]["CriaInstanciaAutomatico"].ToString().ToUpper().Trim() == "S";

        if (confirmaInstancia)
        {
            funcaoJS = string.Format(@"
            if(confirm(traducao.listaProcessosInterno_ao_confirmar_esta_op__o__ser__automaticamente_criado_um_novo_processo_para_este_fluxo__confirma_))
                callbackCriaInstancia.PerformCallback();");
        }

        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), true, funcaoJS, podeIniciarNovaInstancia(), true, false, "LstFlxUsu", "Fluxos", this);
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

    protected void callbackCriaInstancia_Callback(object source, CallbackEventArgs e)
    {
        string tituloInstanciaWf = montaTituloInstancia(_codigoProjeto, _codigoWorkflow);

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

                END", _codigoWorkflow, tituloInstanciaWf.Replace("'", "''"), codigoUsuarioResponsavel, _codigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataTable dt = ds.Tables[0];
            int auxCodigoInstanciaWf = int.Parse(dt.Rows[0]["CodigoInstanciaWf"].ToString());
            string codigoEtapaWf = dt.Rows[0]["CodigoEtapaInicial"].ToString();
            string chamaTelaMenu = (Request.QueryString["TL"] + "") != "" ? "&TL=CHI" : "";

            if (chamadaViaTelaDeProjetos)
                callbackCriaInstancia.JSProperties["cp_Parametros"] = "CF=" + _codigoFluxo + "&CP=" + _codigoProjeto + "&CW=" + _codigoWorkflow + "&CI=" + auxCodigoInstanciaWf + "&CE=" + codigoEtapaWf + "&CS=1" + chamaTelaMenu;
            else
                callbackCriaInstancia.JSProperties["cp_Parametros"] = "CW=" + _codigoWorkflow + "&CI=" + auxCodigoInstanciaWf + "&CE=" + codigoEtapaWf + "&CS=1" + chamaTelaMenu;
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

    protected void gvDados_BeforeGetCallbackResult(object sender, EventArgs e)
    {
        AjustaContadorDePagina((ASPxGridView)sender);
    }

    private static void AjustaContadorDePagina(ASPxGridView sender)
    {
        if (sender.GetTotalSummaryValue((ASPxSummaryItem)sender.TotalSummary["NumeroProtocolo"]) == null)
        {
            return;
        }

        int quantidadeItens = 0;
        bool retorno = false;
        retorno = int.TryParse((sender.GetTotalSummaryValue((ASPxSummaryItem)sender.TotalSummary["NumeroProtocolo"]).ToString()).ToString(), out quantidadeItens);
        if (retorno == true)
        {
            sender.SettingsPager.Summary.Text = "Página {0} de {1} (" + quantidadeItens.ToString() + " itens)";
        }
    }

    protected void gvDados_DataBound(object sender, EventArgs e)
    {
        AjustaContadorDePagina((ASPxGridView)sender);
    }

    protected void gvDados_PreRender(object sender, EventArgs e)
    {
        AjustaContadorDePagina((ASPxGridView)sender);
    }
}
