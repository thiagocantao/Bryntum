/*
 * 
 * MUDANÇAS
 * 
 * 05/05/2011 :: Alejandro : Tratamento na recuperação de dados da listas de Status.
 *               método    : private void populaListaBox_StatusDisponivel(int codigoFluxo, int codigoTipoProjeto)
 * 
 * Se addiciono um switch com a siguente codição da variavel [codigoTipoProjeto]:
 *          1 e 2: "PRJ";
 *          3    : "PRC";
 *          4 e 5: "DMD";
 *          outro: "HNT";
 * 
 */
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
using System.Collections.Generic;
using System.Drawing;
using System.Collections.Specialized;
using Newtonsoft.Json;

public partial class administracao_adm_CadastroWorkflows : System.Web.UI.Page
{
    #region --- [Variáveis da classe]
    private dados cDados;
    private object objCodigo;
    ASPxGridView gvWf_;

    private int CodigoEntidade = 0;
    private string idUsuarioLogado;
    private int alturaPrincipal = 0;
    private string dbName;
    private string dbOwner;

    private char delimitadorValores = '$';
    private char delimitadorElementoLista = '¢';
    public bool podeIncluir = true;
    public string TipoOperacao = "";


    public string fluxoDestino = "";
    public string fluxoOrigem = "";
    public bool temPermissaoDeEditarTagsFluxos = false;
    #endregion

    protected void Page_Init(object sender, EventArgs e)
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
        sdsProjetosFluxo.ConnectionString = cDados.classeDados.getStringConexao();
        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado").ToString();
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, int.Parse(idUsuarioLogado), CodigoEntidade, CodigoEntidade, "null", "EN", 0, "null", "WF_Cad");
            Session.Remove("dtTiposProjetos");
        }
        temPermissaoDeEditarTagsFluxos = cDados.VerificaAcessoEmAlgumObjeto(int.Parse(idUsuarioLogado), CodigoEntidade, "EN", "EN_EdtTagFlx");
        txtIniciaisFluxo.MaxLength = int.Parse(cDados.getMetadadosTabelaBanco("Fluxos", "IniciaisFluxo").Tables[0].Rows[0]["Tamanho"].ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        if (!IsPostBack && !IsCallback)
            new traducao("pt-BR", gvDados, pcDados); // tradução dos componentes da página

        TipoOperacao = hfGeral.Contains("TipoOperacao") ? hfGeral.Get("TipoOperacao").ToString() : "";
        cDados.aplicaEstiloVisual(this);
        populaGridFluxos();
        ((ASPxGridView)gvProjetosFluxo).Settings.ShowFilterRow = false;
        gvProjetosFluxo.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        //gvProjetosFluxo.Settings.VerticalScrollableHeight = 150;

        int codigoFluxo = getChavePrimaria() == "" ? -1 : int.Parse(getChavePrimaria());

        //populaGridWorkflows(codigoFluxo);
        populaGridTiposProjetos(codigoFluxo);

       DataSet ds = cDados.getParametrosSistema(CodigoEntidade, "utilizaIntegracaoFluxosViaAPI");

        if(cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            pageControl.TabPages[2].Visible = ds.Tables[0].Rows[0]["utilizaIntegracaoFluxosViaAPI"].ToString().Trim().ToUpper() == "S";
        }

        populaDdlGrupoFluxo();
        populaGridProjetos();
        //populaComboFluxoDestino();
        tdTagControle.Style.Add(HtmlTextWriterStyle.Display, temPermissaoDeEditarTagsFluxos == true ? "block" : "none");
        tdTagControle1.Style.Add(HtmlTextWriterStyle.Display, temPermissaoDeEditarTagsFluxos == true ? "block" : "none");
        pageControl.TabPages[1].Text = Resources.traducao.adm_CadastroWorkflows_projetos_relacionados;
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            populacmbPeriodicidade();

            //hfStatusCopiaFluxo.Set("CodigoWorkflow", -1);
            //hfStatusCopiaFluxo.Set("CodigoFluxo", codigoFluxo);
        }
        this.Title = cDados.getNomeSistema();
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);


        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/FluxosTipoProjeto.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/adm_CadastroWorkflows.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" src=""../scripts/ASPxListbox.js""></script>"));
        this.TH(this.TS("barraNavegacao", "ASPxListbox", "adm_CadastroWorkflows", "WorkflowCharts", "adm_edicaoWorkflows", "FluxosTipoProjeto", "FusionCharts", "uc_crud_caminhoCondicional"));
        Header.Controls.Add(cDados.getLiteral(@"<title>" + @Resources.traducao.adm_CadastroWorkflows__title_cadastro_de_modelos_de_fluxos__title_ + "</title>"));
    }

    #endregion

    #region ---[Preenchimento Grid Fluxos e subGrid Workflows]

    public void populaGridFluxos()
    {
        string sWhere = string.Format(" AND CodigoEntidade = {0} ", CodigoEntidade);
        DataSet ds = cDados.getFluxos(sWhere);
        gvDados.DataSource = ds;
        gvDados.DataBind();
    }

    public void populaGridWorkflows(int codigoFluxo)
    {
        if (gvWf_ != null)
        {
            string sWhere = string.Format(" AND wf.[CodigoFluxo] = {0}", codigoFluxo);
            DataSet ds = cDados.getWorkFlows(sWhere);
            gvWf_.DataSource = ds;
            gvWf_.DataBind();

            gvWf_.Settings.ShowFilterRow = false;
            gvWf_.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
            gvWf_.SettingsBehavior.AllowSort = false;
        }
    }

    protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.VisibleIndex != (-1))
        {
            if (e.GetValue(Resources.traducao.adm_CadastroWorkflows_status).ToString().Equals(Resources.traducao.adm_CadastroWorkflows_desativado))
            {
                e.Row.ForeColor = Color.FromName("#914800");

            }
        }
    }

    protected void gvWorkflows_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (null == gvWf_)
            gvWf_ = (ASPxGridView)sender;
        if (null != gvWf_)
        {
            if ("btnExcluir" == e.ButtonID)
            {
                if (gvWf_.GetRowValues(e.VisibleIndex, "DataPublicacao") != null && gvWf_.GetRowValues(e.VisibleIndex, "DataPublicacao").ToString() != "")
                {
                    e.Enabled = false;
                    e.Text = Resources.traducao.adm_CadastroWorkflows_excluir_n_o_disponivel_;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                    e.Image.ToolTip = Resources.traducao.adm_CadastroWorkflows_excluir_n_o_disponivel_;
                }
                else
                {
                    e.Text = Resources.traducao.adm_CadastroWorkflows_excluir;
                    e.Image.ToolTip = Resources.traducao.adm_CadastroWorkflows_excluir;
                }
            }
            else if ("btnEditar" == e.ButtonID)
            {
                e.Text = Resources.traducao.adm_CadastroWorkflows_editar;
                e.Image.ToolTip = Resources.traducao.adm_CadastroWorkflows_editar;
            }
            if (e.CellType == GridViewTableCommandCellType.Data)
            {

                bool readOnly = gvWf_.GetRowValues(e.VisibleIndex, "DataPublicacao") == null || gvWf_.GetRowValues(e.VisibleIndex, "DataPublicacao").ToString() == "";

                if (e.ButtonID == "btnCopiaFluxo")
                {
                    e.Enabled = readOnly ? false : true;
                    e.Text = !readOnly ? Resources.traducao.adm_CadastroWorkflows_copiar_fluxo : Resources.traducao.adm_CadastroWorkflows_somente___poss_vel_efetuar_a_c_pia_de_um_modelo_de_fluxo_j__publicado_;
                    e.Image.Url = readOnly ? "~/imagens/botoes/btnDuplicarDes.png" : "~/imagens/botoes/btnDuplicar.png";
                }
            }

        }

    }

    protected void gvWorkflows_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {

        string IDBotao = e.ButtonID;

        if ((IDBotao == "btnExcluir"))
        {
            int rowIndex;

            // a variavel "objCodigo" é lida no evento "gvWorkflows_BeforePerformDataSelect"
            rowIndex = gvDados.FindVisibleIndexByKeyValue(objCodigo);

            if (rowIndex >= 0)
            {
                gvWf_ = gvDados.FindDetailRowTemplateControl(rowIndex, "gvWorkflows") as ASPxGridView;
                if (null != gvWf_)
                {
                    string codigoWorkFlow = gvWf_.GetRowValues(e.VisibleIndex, "CodigoWorkflow").ToString();
                    string codigoFluxo = gvWf_.GetRowValues(e.VisibleIndex, "CodigoFluxo").ToString();

                    int registrosAfetados = 0;
                    bool retorno = cDados.excluiWorkflow(int.Parse(codigoWorkFlow), ref registrosAfetados);
                    populaGridWorkflows(int.Parse(codigoFluxo));
                }
            }
        }
    }

    protected void gvWorkflows_BeforePerformDataSelect(object sender, EventArgs e)
    {
        // Este evento ocorre antes da grid gvWorkflows receber os dados do select que a popula
        // como é um master-detail, antes de popularmos o detail, temos que o obter o código (keyFieldName) da grid master
        ASPxGridView grid = (sender as ASPxGridView);
        objCodigo = grid.GetMasterRowKeyValue();
    }

    protected void gvDados_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        // procura pela grid "Workflows" dentro do detailRow da grid Formularios
        gvWf_ = gvDados.FindDetailRowTemplateControl(e.VisibleIndex, "gvWorkflows") as ASPxGridView;

        if (gvWf_ != null)
        {
            cDados.aplicaEstiloVisual(gvWf_);
            if (e.Expanded)
                cDados.aplicaEstiloVisual(gvWf_);

            if (e.Expanded)
            {
                // a variavel "objCodigo" é lida no evento "gvWorkflows_BeforePerformDataSelect"
                if (objCodigo != null)
                {
                    populaGridWorkflows(int.Parse(objCodigo.ToString()));
                    gvWf_.DataBind();
                }
            }
        }
    }

    #endregion

    #region ---[Preenchimento Grid TipoDeProjetos e ListBoxes]



    private void populaGridTiposProjetos(int codigoFluxo)
    {
        DataTable dt = Session["dtTiposProjetos"] == null && TipoOperacao != "Incluir" ? obtemDataTableGridTiposProjetos(codigoFluxo) : (DataTable)Session["dtTiposProjetos"];
        gvTiposProjetos.DataSource = dt;
        gvTiposProjetos.DataBind();
        gvTiposProjetos.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        ((GridViewDataTextColumn)gvTiposProjetos.Columns["TipoProjeto"]).Settings.ShowFilterRowMenu = DevExpress.Utils.DefaultBoolean.False;
        ((GridViewDataTextColumn)gvTiposProjetos.Columns["TipoProjeto"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
        ((GridViewDataTextColumn)gvTiposProjetos.Columns["TextoOpcaoFluxo"]).Settings.ShowFilterRowMenu = DevExpress.Utils.DefaultBoolean.False;
        ((GridViewDataTextColumn)gvTiposProjetos.Columns["TextoOpcaoFluxo"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
        ((GridViewDataComboBoxColumn)gvTiposProjetos.Columns["TipoOcorrencia"]).Settings.ShowFilterRowMenu = DevExpress.Utils.DefaultBoolean.False;
        ((GridViewDataComboBoxColumn)gvTiposProjetos.Columns["TipoOcorrencia"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
    }

    protected void lbDisponiveisStatus_Callback(object sender, CallbackEventArgsBase e)
    {
        if (e.Parameter.Length >= 6)
        {
            string comando = e.Parameter.Substring(0, 6).ToUpper();
            string codFluxo;
            string codTipoProjeto;
            int codigoFluxo;
            int codigoTipoProjeto;

            if (comando == "POPLBX")
            {
                /// quando se tratando de popular listBox, o parâmetro tem que conter o 
                /// código do fluxo seguido do código da tipo de projeto delimitados por [delimitadorValores]
                int posDelimit = e.Parameter.IndexOf(delimitadorValores);
                if (posDelimit > 7)
                {
                    codFluxo = e.Parameter.Substring(7, posDelimit - 7);
                    codTipoProjeto = e.Parameter.Substring(posDelimit + 1);

                    if (int.TryParse(codFluxo, out codigoFluxo) &&
                        int.TryParse(codTipoProjeto, out codigoTipoProjeto))
                    {
                        populaListaBox_StatusDisponivel(codigoFluxo, codigoTipoProjeto);
                    }
                }
            } /// if (comando == "POPLBX")
        } /// if (e.Parameter.Length >= 6)
    }

    private void populaListaBox_StatusDisponivel(int codigoFluxo, int codigoTipoProjeto)
    {
        DataTable dt = null;
        string iniciaisTipoStatus = "";

        switch (codigoTipoProjeto)
        {
            case 3:
                iniciaisTipoStatus = "PRC";
                break;
            case 4:
            case 5:
                iniciaisTipoStatus = "DMD";
                break;
            case 6:
                iniciaisTipoStatus = "HNT";
                break;
            default:
                iniciaisTipoStatus = "PRJ";
                break;
        }

        string sComando = string.Format(@"
            BEGIN
                --DECLARE @IniciaisTipoProjeto Char(3)

                --SELECT @IniciaisTipoProjeto = IndicaTipoProjeto FROM TipoProjeto WHERE CodigoTipoProjeto = {3}

                SELECT st.[CodigoStatus], st.[DescricaoStatus] 
                    FROM {0}.{1}.[Status] AS [st]
                    WHERE st.[TipoStatus] = '{4}' AND 
                        NOT EXISTS( SELECT 1 FROM {0}.{1}.[FluxosStatusTipoProjeto] AS [fstp]
                            WHERE   fstp.[CodigoStatus] = st.[CodigoStatus] 
                                AND fstp.[CodigoFluxo] = {2} 
                                AND fstp.[CodigoTipoProjeto] = {3}
                                AND fstp.[StatusRelacionamento] = 'A' )

            END
                ", dbName, dbOwner, codigoFluxo, codigoTipoProjeto
                 , iniciaisTipoStatus);
        DataSet ds = cDados.getDataSet(sComando);
        if (cDados.DataSetOk(ds))
            dt = ds.Tables[0];

        if (null != dt)
        {
            lbDisponiveisStatus.DataSource = dt;
            lbDisponiveisStatus.TextField = "DescricaoStatus";
            lbDisponiveisStatus.ValueField = "CodigoStatus";
            lbDisponiveisStatus.DataBind();
        }
    }

    protected void lbSelecionadosStatus_Callback(object sender, CallbackEventArgsBase e)
    {
        if (e.Parameter.Length >= 6)
        {
            string comando = e.Parameter.Substring(0, 6).ToUpper();
            string codFluxo;
            string codTipoProjeto;
            int codigoFluxo;
            int codigoTipoProjeto;

            if (comando == "POPLBX")
            {
                /// quando se tratando de popular listBox, o parâmetro tem que conter o 
                /// código do fluxo seguido do código da tipo de projeto delimitados por [delimitadorValores]
                int posDelimit = e.Parameter.IndexOf(delimitadorValores);
                if (posDelimit > 7)
                {
                    codFluxo = e.Parameter.Substring(7, posDelimit - 7);
                    codTipoProjeto = e.Parameter.Substring(posDelimit + 1);

                    if (int.TryParse(codFluxo, out codigoFluxo) &&
                        int.TryParse(codTipoProjeto, out codigoTipoProjeto))
                    {
                        populaListaBox_StatusSelecionados(codigoFluxo, codigoTipoProjeto);
                    }
                }
            } /// if (comando == "POPLBX")
        } /// if (e.Parameter.Length >= 6)
    }

    private void populaListaBox_StatusSelecionados(int codigoFluxo, int codigoTipoProjeto)
    {
        DataTable dt = null;

        string sComando = string.Format(@"
              
                BEGIN
                --DECLARE @IniciaisTipoProjeto Char(3)

                --SELECT @IniciaisTipoProjeto = IndicaTipoProjeto FROM TipoProjeto WHERE CodigoTipoProjeto = {3}

                SELECT st.[CodigoStatus], st.[DescricaoStatus] 
                FROM        {0}.{1}.[Status] AS [st] 
                INNER JOIN  {0}.{1}.[FluxosStatusTipoProjeto] AS [fstp] ON ( fstp.[CodigoStatus] = st.[CodigoStatus] )
                WHERE fstp.[CodigoFluxo]            = {2} 
                  AND fstp.[CodigoTipoProjeto]      = {3}
                  AND fstp.[StatusRelacionamento]   = 'A'
                END
                ", dbName, dbOwner, codigoFluxo, codigoTipoProjeto);
        DataSet ds = cDados.getDataSet(sComando);
        if (cDados.DataSetOk(ds))
            dt = ds.Tables[0];

        if (null != dt)
        {
            lbSelecionadosStatus.DataSource = dt;
            lbSelecionadosStatus.TextField = "DescricaoStatus";
            lbSelecionadosStatus.ValueField = "CodigoStatus";
            lbSelecionadosStatus.DataBind();
        }
    }

    /// <summary>
    /// Devolve uma datatable com os tipos de projetos relacionados ao fluxo em questão
    /// </summary>
    /// <param name="codigoFluxo"></param>
    /// <returns></returns>
    private DataTable obtemDataTableGridTiposProjetos(int codigoFluxo)
    {
        DataTable dt = null;
        string sCommand = string.Format(@"
SELECT
          ftp.[CodigoTipoProjeto]
        , tp.[TipoProjeto]
		, ftp.[TextoOpcaoFluxo]
        , ftp.[TipoOcorrenciaFluxo]     AS [TipoOcorrencia]
        , CAST( 'N' AS Char(1) )        AS [RegistroNovo]
	FROM
		{0}.{1}.FluxosTipoProjeto			AS [ftp]
			INNER JOIN {0}.{1}.[TipoProjeto]	AS [tp]
				ON (tp.[CodigoTipoProjeto] = ftp.[CodigoTipoProjeto] )
	WHERE
		ftp.[CodigoFluxo]			= {2} ", dbName, dbOwner, codigoFluxo);
        DataSet ds = cDados.getDataSet(sCommand);
        if (cDados.DataSetOk(ds))
        {
            dt = ds.Tables[0];

            if (gvTiposProjetos.IsEditing)
                Session["dtTiposProjetos"] = dt;
        }

        return dt;
    }

    /// <summary>
    /// Devolve a datatable que está sendo usada na grid de tipos de projetos
    /// </summary>
    /// <returns></returns>
    private DataTable obtemDataTableGridTiposProjetos()
    {
        DataTable dt = null;

        if (null != Session["dtTiposProjetos"])
            dt = (DataTable)Session["dtTiposProjetos"];
        else
        {
            int codigoFluxo = getChavePrimaria() == "" ? -1 : int.Parse(getChavePrimaria());

            //populaGridWorkflows(codigoFluxo);
            dt = obtemDataTableGridTiposProjetos(codigoFluxo);
        }

        return dt;
    }

    private void populaDdlGrupoFluxo()
    {
        DataTable dt = null;

        string sComando = string.Format(@"
                    SELECT CodigoGrupoFluxo
                          ,DescricaoGrupoFluxo
                          ,OrdemGrupoMenu
                          ,IniciaisGrupoFluxo
                          ,CodigoEntidade
                      FROM {0}.{1}.GrupoFluxo
                     WHERE CodigoEntidade = {2}
                    ", cDados.getDbName(), cDados.getDbOwner(), CodigoEntidade);

        DataSet ds = cDados.getDataSet(sComando);

        if (cDados.DataSetOk(ds))
            dt = ds.Tables[0];

        if (null != dt)
        {
            ddlGrupoFluxo.DataSource = dt;
            ddlGrupoFluxo.TextField = "DescricaoGrupoFluxo";
            ddlGrupoFluxo.ValueField = "CodigoGrupoFluxo";
            ddlGrupoFluxo.DataBind();

            ListEditItem li = new ListEditItem("", "NULL");
            ddlGrupoFluxo.Items.Add(li);
        }
    }

    #endregion

    #region ---[Tratamento para Interações com a Tela]

    /// <summary>
    /// Insere os itens no ComboBox em que o usuário irá escolher o tipo de projeto
    /// </summary>
    /// <remarks>
    ///  Esta função é acionada quando o usuário está para incluir ou editar uma linha da grid tipo de projeto
    /// </remarks>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 

    protected void gvTiposProjetos_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

        if (!gvTiposProjetos.IsEditing)
            return;

        if (e.Column.FieldName == "CodigoTipoProjeto")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            string where = "";

            DataTable dt = obtemDataTableGridTiposProjetos();

            if (null != dt)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if ((null == e.Value) ||
                        (dr["CodigoTipoProjeto"].ToString() != e.Value.ToString()))
                        where += dr["CodigoTipoProjeto"] + ",";
                }
            } /// if (null != dt)

            where += "6,";  // excluindo o tipo de projeto horas não trabalhadas.

            if (where != "")
            {
                where = " AND CodigoTipoProjeto NOT IN (" + where.Substring(0, where.Length - 1) + ")";
            }
            DataSet ds = cDados.getListaTiposProjetos(where);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                combo.DataSource = ds;
                combo.TextField = "TipoProjeto";
                combo.ValueField = "CodigoTipoProjeto";
                combo.DataBind();
            } /// if (cDados.DataSetOk(ds) && ...
        } // if (e.Column.FieldName == "CodigoTipoProjeto")
    }

    protected void gvTiposProjetos_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dt = obtemDataTableGridTiposProjetos();

        if (null != dt)
        {
            DataRow dr = dt.NewRow();

            if (e.NewValues["CodigoTipoProjeto"] != null)
            {
                dr["CodigoTipoProjeto"] = e.NewValues["CodigoTipoProjeto"];

                DataSet ds = cDados.getListaTiposProjetos("AND CodigoTipoProjeto = " + e.NewValues["CodigoTipoProjeto"]);

                if ((true == cDados.DataSetOk(ds)) && (true == cDados.DataTableOk(ds.Tables[0])))
                    dr["TipoProjeto"] = ds.Tables[0].Rows[0]["TipoProjeto"] + "";
            } /// if (e.NewValues["CodigoTipoProjeto"] != null)

            dr["TextoOpcaoFluxo"] = e.NewValues["TextoOpcaoFluxo"];
            dr["RegistroNovo"] = "S";
            dr["TipoOcorrencia"] = e.NewValues["TipoOcorrencia"];
            dt.Rows.Add(dr);

            Session["dtTiposProjetos"] = dt;

            gvTiposProjetos.DataSource = dt;
            gvTiposProjetos.DataBind();
            gvTiposProjetos.FocusedRowIndex = gvTiposProjetos.FindVisibleIndexByKeyValue(e.NewValues["CodigoTipoProjeto"]);
        }  // if (null != dt)

        e.Cancel = true;
        gvTiposProjetos.CancelEdit();
    }

    protected void gvTiposProjetos_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dt = obtemDataTableGridTiposProjetos();

        if (null != dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["CodigoTipoProjeto"].ToString() == e.Keys["CodigoTipoProjeto"].ToString())
                {
                    if (e.NewValues["CodigoTipoProjeto"] != null)
                    {
                        dr["CodigoTipoProjeto"] = e.NewValues["CodigoTipoProjeto"];

                        DataSet ds = cDados.getListaTiposProjetos("AND CodigoTipoProjeto = " + e.NewValues["CodigoTipoProjeto"]);

                        if ((true == cDados.DataSetOk(ds)) && (true == cDados.DataTableOk(ds.Tables[0])))
                            dr["TipoProjeto"] = ds.Tables[0].Rows[0]["TipoProjeto"] + "";
                    } /// if (e.NewValues["CodigoTipoProjeto"] != null)

                    dr["TextoOpcaoFluxo"] = e.NewValues["TextoOpcaoFluxo"];
                    dr["TipoOcorrencia"] = e.NewValues["TipoOcorrencia"];

                    dt.AcceptChanges();
                    break;
                } // if (dr["CodigoTipoProjeto"].ToString() == e.Keys["CodigoTipoProjeto"].ToString())
            } // foreach (DataRow dr in dt)

            Session["dtTiposProjetos"] = dt;

            gvTiposProjetos.DataSource = dt;
            gvTiposProjetos.DataBind();
        }  // if (null != dt)

        e.Cancel = true;
        gvTiposProjetos.CancelEdit();
    }

    protected void gvTiposProjetos_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        DataTable dt = obtemDataTableGridTiposProjetos();
        if ((null != dt) && (e.Keys["CodigoTipoProjeto"] != null))
        {

            foreach (DataRow dr in dt.Rows)
            {
                if ((dr["CodigoTipoProjeto"] != null) && (dr["CodigoTipoProjeto"].ToString() == e.Keys["CodigoTipoProjeto"].ToString()))
                {
                    //retiraListaStatusDoHiddenField(dr["CodigoTipoProjeto"].ToString());
                    dr.Delete();
                    dt.AcceptChanges();

                    // registra o código do tipo de projeto para tratamento on EndCallback da grid;
                    gvTiposProjetos.JSProperties["cpCodigoTipoProjetoDeletado"] = e.Keys["CodigoTipoProjeto"].ToString();

                    break;
                }
            }

            Session["dtTiposProjetos"] = dt;
            gvTiposProjetos.DataSource = dt;
            gvTiposProjetos.DataBind();
        }  // if (null != dt)

        e.Cancel = true;
        gvTiposProjetos.CancelEdit();
    }

    #endregion

    #region ---[Gravação das informações na base de dados]

    protected void pnCallback_Callback(object sender, CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        else if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        else if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
            Session.Remove("dtTiposProjetos");
        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }

    private string persisteInclusaoRegistro()
    {
        try
        {
            salvaRegistro("I", -1);

            return "";
        }
        catch (Exception ex)
        {
            if (ex is System.Data.SqlClient.SqlException)
            {
                var sqlException = ex as System.Data.SqlClient.SqlException;
                if (sqlException.Number == 2627)
                {
                    return Resources.traducao.adm_CadastroWorkflows_j__existe_um_detalhes_do_modelo_de_fluxo_com_esse_nome_;
                }
            }
            return ex.Message;
        }
    }

    private string persisteEdicaoRegistro()
    {
        try
        {
            // busca a chave primaria
            string chave = getChavePrimaria();

            salvaRegistro("E", int.Parse(chave));

            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string persisteExclusaoRegistro()
    {
        try
        {
            // busca a chave primaria
            string chave = getChavePrimaria();

            excluiRegistro(int.Parse(chave));

            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private void salvaRegistro(string modo, int codigoFluxo)
    {
        string sqlDadosFluxo = "";
        string descricao = "";
        string observacao = "";
        string nomeFluxo = "";
        string sqlInsertStatus = "";
        string codigoGrupoFluxo = "";
        string comandoSQL;
        string iniciaisFluxo = "";

        descricao = (0 == txtDescricao.Text.Length) ? "NULL" : string.Format("{0}", txtDescricao.Text.ToString().Replace("'", "''"));
        observacao = (0 == txtObservacao.Text.Length) ? "NULL" : string.Format("{0}", txtObservacao.Text.ToString().Replace("'", "''"));
        nomeFluxo = (0 == txtNomeFluxo.Text.Length) ? "NULL" : string.Format("{0}", txtNomeFluxo.Text.ToString().Replace("'", "''"));
        iniciaisFluxo = (0 == txtIniciaisFluxo.Text.Length) ? "NULL" : string.Format("'{0}'", txtIniciaisFluxo.Text.ToString().Replace("'", "''"));
        codigoGrupoFluxo = (0 == ddlGrupoFluxo.Text.Length) ? "NULL" : string.Format("{0}", ddlGrupoFluxo.Value.ToString());
        if (modo.Equals("I"))
        {
            sqlDadosFluxo = string.Format(@"
                INSERT INTO {0}.{1}.[Fluxos]
                       ([NomeFluxo]
                       ,[CodigoSistemaWf]
                       ,[Descricao]
                       ,[Observacao]
                       ,[DataInclusao]
                       ,[IdentificadorUsuarioInclusao]
                       ,[StatusFluxo]
                       ,[CodigoEntidade]
                       ,[CodigoGrupoFluxo]
                       ,[IniciaisFluxo])
                 VALUES(
                        '{2}'
                       , {3}
                       ,'{4}'
                       ,'{5}'
                       , GETDATE()
                       ,'{6}'
                       ,'{7}'
                       ,{8}
                       ,{9}
                       ,{10})

                DECLARE @CodigoFluxo Int
                SET @CodigoFluxo = SCOPE_IDENTITY()

                ", dbName, dbOwner, nomeFluxo, 1, descricao,
                   observacao, idUsuarioLogado, ddlStatusFluxo.Value.ToString(), CodigoEntidade, codigoGrupoFluxo, iniciaisFluxo);
        } // if (modo.Equals("I"))
        else if (modo.Equals("E"))
        {
            montaInsertStatus(modo, out sqlInsertStatus);
            codigoGrupoFluxo = (0 == ddlGrupoFluxo.Text.Length) ? "NULL" : string.Format("{0}", ddlGrupoFluxo.Value.ToString());
            sqlDadosFluxo = string.Format(@"
                UPDATE {0}.{1}.[Fluxos]
                   SET [NomeFluxo]          = '{3}'
                      ,[CodigoSistemaWf]    = {4}
                      ,[Descricao]          = '{5}'
                      ,[Observacao]         = '{6}'
                      ,[StatusFluxo]        = '{7}'
                      ,[CodigoGrupoFluxo]   = {8}
                      ,[IniciaisFluxo]      = {9}
                 WHERE 
                    CodigoFluxo = {2}

                DECLARE @CodigoFluxo Int
                SET @CodigoFluxo = {2}

                ", dbName, dbOwner, codigoFluxo, nomeFluxo, 1,
                   descricao, observacao, ddlStatusFluxo.Value.ToString(), codigoGrupoFluxo, iniciaisFluxo);

        } // else if (modo.Equals("E"))

        comandoSQL = sqlDadosFluxo + sqlInsertStatus;
        int registrosAfetados = 0;
        cDados.execSQL(comandoSQL, ref registrosAfetados);

        Session.Remove("dtTiposProjetos");

        populaGridFluxos();
    }

    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    private void montaInsertStatus(string modo, out string comandoSQL)
    {
        bool bLinhaClickada;
        string codigoTipoProjeto, textoOpcao, tipoOcorrencia, registroNovo;
        string deleteDadosAntigos = "", insertFluxosTipos = "", insertFluxosStatus = "";
        string notInDelete = "";

        DataTable dt = obtemDataTableGridTiposProjetos();
        List<int> listaStatus = new List<int>(); ;

        foreach (DataRow dr in dt.Rows)
        {
            codigoTipoProjeto = dr["CodigoTipoProjeto"].ToString();
            textoOpcao = dr["TextoOpcaoFluxo"].ToString().Replace("'", "''");
            tipoOcorrencia = dr["TipoOcorrencia"].ToString();
            registroNovo = dr["RegistroNovo"].ToString();


            bLinhaClickada = obtemListaStatusSelecionados(int.Parse(codigoTipoProjeto), ref listaStatus);

            if ((true == bLinhaClickada) || (registroNovo.Equals("S")))
            {
                // se não tiver selecionada status algum tipo de projeto, gera exceção
                if (0 == listaStatus.Count)
                    throw new Exception(Resources.traducao.adm_CadastroWorkflows_aten__o__para_cada_tipo_de_projeto_relacionado_ao_modelo_de_fluxo____preciso_selecionar_pelo_menos_um_status_);

                insertFluxosTipos += string.Format(@"

                    INSERT INTO {0}.{1}.[FluxosTipoProjeto]
                           ([CodigoFluxo]
                           ,[CodigoTipoProjeto]
                           ,[TextoOpcaoFluxo]
                           ,[StatusRelacionamento]
                           ,[DataAtivacao]
                           ,[IdentificadorUsuarioAtivacao]
                           ,[TipoOcorrenciaFluxo])
                     VALUES
                           (@CodigoFluxo, {2}, '{3}', 'A', GETDATE(), '{4}', '{5}')

                    ", dbName, dbOwner, codigoTipoProjeto, textoOpcao, idUsuarioLogado, tipoOcorrencia);

                foreach (int status in listaStatus)
                {
                    insertFluxosStatus += string.Format(@"
                        INSERT INTO {0}.{1}.[FluxosStatusTipoProjeto]
                               ([CodigoFluxo]
                               ,[CodigoTipoProjeto]
                               ,[CodigoStatus]
                               ,[StatusRelacionamento]
                               ,[DataAtivacao]
                               ,[IdentificadorUsuarioAtivacao])
                         VALUES
                               (@CodigoFluxo, {2}, {3}, 'A', GETDATE(), '{4}')

                        ", dbName, dbOwner, codigoTipoProjeto, status, idUsuarioLogado);
                } //  foreach (int status in listaStatus)
            } // if ((true == bLinhaClickada) || (registroNovo.Equals("S")))
            else
            {   // se a linha não foi clicada e nem é um novo registro, 
                // não deixa mexer nesta linha no banco de dados
                if (0 != notInDelete.Length)
                    notInDelete += ',';
                notInDelete += codigoTipoProjeto;
            } // else ((true == bLinhaClickada) || (registroNovo.Equals("S")))


        } // foreach (DataRow dr in dt.Rows)

        // se estiver editando um fluxo já cadastrado, apaga as linhas no bd para reinseri-las
        if (modo.Equals("E"))
        {
            if (0 != notInDelete.Length)
            {
                deleteDadosAntigos = string.Format(@"
                    DELETE {0}.{1}.[FluxosStatusTipoProjeto] WHERE 
                    [CodigoFluxo] = @CodigoFluxo AND [CodigoTipoProjeto] NOT IN ({2})

                    DELETE {0}.{1}.[FluxosTipoProjeto] WHERE 
                    [CodigoFluxo] = @CodigoFluxo AND [CodigoTipoProjeto] NOT IN ({2})

                    ", dbName, dbOwner, notInDelete);
            }// if (0 != notInDelete.Length)
            else
            {
                deleteDadosAntigos = string.Format(@"
                    DELETE {0}.{1}.[FluxosStatusTipoProjeto] WHERE 
                    [CodigoFluxo] = @CodigoFluxo 

                    DELETE {0}.{1}.[FluxosTipoProjeto] WHERE 
                    [CodigoFluxo] = @CodigoFluxo 

                    ", dbName, dbOwner);
            } // else (0 != notInDelete.Length)
        } // if (modo.Equals("E"))

        comandoSQL = deleteDadosAntigos + insertFluxosTipos + insertFluxosStatus;
    }

    private bool obtemListaStatusSelecionados(int codigoTipoProjeto, ref List<int> listaStatus)
    {
        bool bExisteReferencia;
        string idLista;
        string listaAsString = "";
        string[] strListaStatus, temp;

        idLista = "Sel_" + codigoTipoProjeto + delimitadorValores;

        listaStatus.Clear();

        if (hfStatus.Contains(idLista))
        {
            bExisteReferencia = true;
            listaAsString = hfStatus.Get(idLista).ToString();
        }
        else
            bExisteReferencia = false;

        if (null != listaAsString)
        {
            strListaStatus = listaAsString.Split(delimitadorElementoLista);
            for (int j = 0; j < strListaStatus.Length; j++)
            {
                if (strListaStatus[j].Length > 0)
                {
                    temp = strListaStatus[j].Split(delimitadorValores);
                    listaStatus.Add(int.Parse(temp[1]));
                }
            }
        } // if (null == listaAsString)

        if (bExisteReferencia == false)
            gvTiposProjetos.FocusedRowIndex = gvTiposProjetos.FindVisibleIndexByKeyValue(codigoTipoProjeto);

        return bExisteReferencia;
    }

    private void excluiRegistro(int codigoFluxo)
    {
        string comandoSQL = string.Format(@"
            SELECT TOP 1 [CodigoFluxo] FROM {0}.{1}.[Workflows] AS [wf] WHERE wf.[CodigoFluxo] = {2}
            ", dbName, dbOwner, codigoFluxo);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            throw new Exception(Resources.traducao.adm_CadastroWorkflows_exclus_o_n_o_permitida__para_a_exclus_o_de_um_modelo_de_fluxo____preciso_excluir_antes_todas_as_suas_vers_es_);
        else
        {
            excluiFluxo(codigoFluxo);
        }
    }

    private void excluiFluxo(int codigoFluxo)
    {
        string comandoSQL = string.Format(@"
            DELETE {0}.{1}.[FluxosStatusTipoProjeto] WHERE 
            [CodigoFluxo] = {2}

            DELETE {0}.{1}.[FluxosTipoProjeto] WHERE 
            [CodigoFluxo] = {2}

            DELETE {0}.{1}.[Fluxos] WHERE 
            [CodigoFluxo] = {2}
            ", dbName, dbOwner, codigoFluxo);
        int afetados = 0;
        cDados.execSQL(comandoSQL, ref afetados);
        populaGridFluxos();
    }

    #endregion
    protected void gvTiposProjetos_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        e.Enabled = (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Consultar");
        if (e.ButtonType == ColumnCommandButtonType.Edit && !e.Enabled)
        {
            e.Image.Url = "~/imagens/botoes/editarRegDes.PNG";
        }
        if (e.ButtonType == ColumnCommandButtonType.Delete && !e.Enabled)
        {
            e.Image.Url = "~/imagens/botoes/excluirRegDes.PNG";
        }
    }
    protected void gvWorkflows_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.VisibleIndex != (-1))
        {
            if ((!e.GetValue("DataPublicacao").ToString().Equals("")) &&
                 (e.GetValue("DataRevogacao").ToString().Equals("")))
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                    e.Row.Cells[i].Style.Add("color", "Green");
            }
        }

    }

    protected void gvTiposProjetos_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters.Length >= 6)
        {
            string comando = e.Parameters.Substring(0, 6).ToUpper();

            if (comando == "POPFRM")
            {
                string CodFluxo;
                int CodigoFluxo;
                CodFluxo = e.Parameters.Substring(7);
                if (int.TryParse(CodFluxo, out CodigoFluxo))
                {
                    Session.Remove("dtTiposProjetos");

                    populaGridTiposProjetos(CodigoFluxo);

                    gvTiposProjetos.FocusedRowIndex = -1;

                    // posiciona na primeira linha caso haja;
                    if (gvTiposProjetos.VisibleRowCount > 0)
                        gvTiposProjetos.FocusedRowIndex = 0;
                }
            } /// if (comando == "POPFLX")
            else if (comando.Equals("EXCROW"))
            {
                for (int count = gvTiposProjetos.VisibleRowCount; count > 0; count--)
                    gvTiposProjetos.DeleteRow(count - 1);

                // remove jsproperties para evitar processamento no javascript desnecessário 
                // em virtude da exclusão das linhas
                gvTiposProjetos.JSProperties.Remove("cpCodigoTipoProjetoDeletado");
            }
        }
    }

    protected void hfStatus_CustomCallback(object sender, CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }

        // Grava a mensagem de erro. Se não houve erro, terá conteúdo ""
        hfStatus.Set("ErroSalvar", mensagemErro_Persistencia);
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
            hfStatus.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
        else // alguma coisa deu errado...
            hfStatus.Set("StatusSalvar", "0"); // 1 indica que foi salvo com sucesso.
    }
    protected void gvDados_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadModFlx");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados); btnSalvar1.SetVisible(true); TipoOperacao = 'Incluir';", true, true, false, "CadModFlx", Resources.traducao.adm_CadastroWorkflows_cadastro_de_modelos_de_fluxos, this);
    }

    protected void menu_Init1(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "trataClickBotaoNovoWorkflow(gvDados);", true, false, false, "CadModFlx", Resources.traducao.adm_CadastroWorkflows_cadastro_de_modelos_de_fluxos, this);
    }

    #endregion

    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        if (e.CallbackName == "SORT" || e.CallbackName == "APPLYCOLUMNFILTER")
        {
            gvDados.DetailRows.CollapseAllRows();
        }
    }

    protected void gvWorkflows_Load(object sender, EventArgs e)
    {
        ASPxGridView gv = sender as ASPxGridView;
        cDados = CdadosUtil.GetCdados(null);
        cDados.aplicaEstiloVisual(gv);
        gv.Settings.ShowFilterRow = false;
        gv.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        gv.SettingsBehavior.AllowSort = false;
        gv.SettingsBehavior.AllowDragDrop = false;
    }
    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnExcluir" && gvDados.GetRowValues(e.VisibleIndex, "podeExcluir") != null)
        {
            string podeExcluir = gvDados.GetRowValues(e.VisibleIndex, "podeExcluir").ToString().TrimEnd().TrimStart();
            if (podeExcluir == "1")
            {
                e.Enabled = false;
                e.Text = "Editar";
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }
    }

    //DISPARO AUTOMATICO DE FLUXOS
    protected void menu_Disparo_Init(object sender, EventArgs e)
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

        string tipoOperacao = hfGeral.Contains("TipoOperacao") ? hfGeral.Get("TipoOperacao") + "" : "";

        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "onClickBarraNavegacaoDisparoFluxo('Incluir', gvProjetosFluxo, pcDadosDisparoFluxoProjetos)", false, false, false, "lstFluxoDisparo", Resources.traducao.adm_CadastroWorkflows_cadastro_de_modelos_de_fluxos, this);
    }

    protected void menu_Disparo_ItemClick(object source, MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "lstFluxoDisparo");
    }

    protected void pnCallbackgvProjetosFluxo_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallbackgvProjetosFluxo.JSProperties["cp_OperacaoOk"] = "";
        hfGeral1.Set("SucessoSalvar", Resources.traducao.adm_CadastroWorkflows_opera__o_efetuada_com_sucesso_);
        string operacao = e.Parameter == "Excluir" ? "Excluir" : hfGeral1.Get("ExisteRegistro").ToString() == "N" ? "Incluir" : "Editar";

        if (operacao == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistroDisparoFluxo();
        }
        if (operacao == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistroDisparoFluxo();
        }

        if (operacao == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistroDisparoFluxo();
        }



        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral1.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallbackgvProjetosFluxo.JSProperties["cp_OperacaoOk"] = e.Parameter;
            pnCallbackgvProjetosFluxo.JSProperties["cp_CodigoFluxo"] = hfGeral1.Get("CodigoFluxo").ToString();
        }
        else
        {// alguma coisa deu errado...
            hfGeral1.Set("ErroSalvar", mensagemErro_Persistencia);

        }
    }

    #region CALLBACK Disparo
    // retorna a primary key da tabela.
    private string getChavePrimariaDisparoFluxo()
    {
        if (gvProjetosFluxo.FocusedRowIndex >= 0)
            return gvProjetosFluxo.GetRowValues(gvProjetosFluxo.FocusedRowIndex, gvProjetosFluxo.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistroDisparoFluxo()
    {
        string msgRetorno = "";
        try
        {
            //string periodicidade = cmbPeriodicidade.Value != null 
            //string proximaData = calculaProximaExecucao(dteInicio.Text, hfGeral1.Get("codigoPeriodicidade").ToString());
            sdsProjetosFluxo.InsertParameters["CodigoFluxo"].DefaultValue = hfGeral1.Get("CodigoFluxo").ToString();
            sdsProjetosFluxo.InsertParameters["CodigoProjeto"].DefaultValue = hfGeral1.Get("CodigoProjeto").ToString();
            sdsProjetosFluxo.InsertParameters["IdentificadorUsuarioAtivacao"].DefaultValue = idUsuarioLogado;
            sdsProjetosFluxo.InsertParameters["DataPrimeiraExecucao"].DefaultValue = dteInicio.Text;
            sdsProjetosFluxo.InsertParameters["Periodicidade"].DefaultValue = cmbPeriodicidade.Value.ToString();
            sdsProjetosFluxo.InsertParameters["IndicaControlado"].DefaultValue = ckbIndicaControlado.Value.ToString();
            sdsProjetosFluxo.InsertParameters["DataProximaExecucao"].DefaultValue = dteInicio.Text;
            sdsProjetosFluxo.Insert();
            gvProjetosFluxo.DataBind();
        }
        catch (Exception ex)
        {
            msgRetorno = ex.Message;
        }
        return msgRetorno;

    }

    private string persisteEdicaoRegistroDisparoFluxo() // Método responsável pela Atualização do registro
    {
        string msgRetorno = "";

        //string proximaData = calculaProximaExecucao(dteInicio.Text, hfGeral1.Get("codigoPeriodicidade").ToString());
        try
        {
            string chavep = getChavePrimariaDisparoFluxo();
            string[] chavecomposta = chavep.Split('|');
            string IndicaControlado = ckbIndicaControlado.Checked ? "S" : "N";
            string CodigoPeriodicidade = hfGeral1.Get("codigoPeriodicidade").ToString();

            sdsProjetosFluxo.UpdateCommand = string.Format(@"
                                                                    DECLARE @DataDesativacao DateTime
                                                                    DECLARE @IdentificadorUsuarioDesativacao INT
                                                                    DECLARE @DataProximaExecucao DateTime 
                                                                    DECLARE @CodigoPeriodicidade INT
                                                                    DECLARE @DataAtivacao DateTime 
                                                                    DECLARE @IdentificadorUsuarioAtivacao INT
                                                                    DECLARE @DataPrimeiraExecucao DateTime 
                                                                    
                                                                    SELECT  @DataAtivacao = dataAtivacao, @IdentificadorUsuarioAtivacao = IdentificadorUsuarioAtivacao  
                                                                           ,@DataPrimeiraExecucao = DataPrimeiraExecucao
                                                                    from [dbo].[FluxosProjetoDisparoAutomatico]
                                                                    WHERE [CodigoFluxo] = {2}
	                                                                    AND [CodigoProjeto] = {3} 

                                                                    IF ('{0}' = 'N') -- se estiver desativando
                                                                    BEGIN
                                                                        SET @DataDesativacao = GETDATE()
                                                                        SET @IdentificadorUsuarioDesativacao = {1}
                                                                        SET @CodigoPeriodicidade = null
                                                                        SET @DataProximaExecucao = null
                                                                        SET @DataPrimeiraExecucao = null
                                                                    END
                                                                    ELSE -- se estiver reativando
                                                                    BEGIN
                                                                        SET @DataAtivacao = GETDATE()  
                                                                        SET @IdentificadorUsuarioAtivacao = {1} 
                                                                        SET @CodigoPeriodicidade = '{5}'
                                                                        SET @DataProximaExecucao = '{4}'
                                                                        SET @DataPrimeiraExecucao = '{4}'
                                                                        SET @DataDesativacao = null 
                                                                        SET @IdentificadorUsuarioDesativacao = null
                                                                    END
                                                                   
                                                                  UPDATE [dbo].[FluxosProjetoDisparoAutomatico]
                                                                    SET  [CodigoPeriodicidade] = @CodigoPeriodicidade 
	                                                                    ,[DataProximaExecucao] = @DataProximaExecucao
                                                                        ,[DataDesativacao] = @DataDesativacao
	                                                                    ,[IdentificadorUsuarioDesativacao] = @IdentificadorUsuarioDesativacao
                                                                        ,[IndicaControlado] = '{0}'
                                                                        ,[DataPrimeiraExecucao] = @DataPrimeiraExecucao
                                                                        ,[DataAtivacao] = @DataAtivacao
	                                                                    ,[IdentificadorUsuarioAtivacao] = @IdentificadorUsuarioAtivacao
                                                                    WHERE [CodigoFluxo] = {2}
	                                                                    AND [CodigoProjeto] = {3}", IndicaControlado, idUsuarioLogado, chavecomposta[0]
                                                                                                             , chavecomposta[1], dteInicio.Text, CodigoPeriodicidade);


            sdsProjetosFluxo.Update();
            gvProjetosFluxo.DataBind();
        }
        catch (Exception ex)
        {
            msgRetorno = ex.Message;
        }
        return msgRetorno;
    }

    private string persisteExclusaoRegistroDisparoFluxo() // Método responsável pela Exclusão do registro
    {
        string msgRetorno = "";
        try
        {
            string chavep = getChavePrimariaDisparoFluxo();
            string[] chavecomposta = chavep.Split('|');
            hfGeral1.Set("CodigoFluxo", chavecomposta[0]);
            hfGeral1.Set("CodigoProjeto", chavecomposta[1]);
            sdsProjetosFluxo.UpdateCommand = string.Format(@"
                                                                  UPDATE [dbo].[FluxosProjetoDisparoAutomatico]
                                                                    SET  [DataDesativacao] = GETDATE()
	                                                                    ,[IdentificadorUsuarioDesativacao] = {0}
                                                                        ,[IndicaControlado] = 'N'
	                                                                    ,[CodigoPeriodicidade] = null
	                                                                    ,[DataProximaExecucao] = null
                                                                        ,[DataPrimeiraExecucao] = null
                                                                    WHERE [CodigoFluxo] = {1}
	                                                                    AND [CodigoProjeto] = {2}", idUsuarioLogado, chavecomposta[0]
                                                                                                                   , chavecomposta[1]);


            sdsProjetosFluxo.Update();
            gvProjetosFluxo.DataBind();
        }
        catch (Exception ex)
        {
            msgRetorno = ex.Message;
        }
        return msgRetorno;
    }
    #endregion

    protected void gvProjetosFluxo_CustomCallback1(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {

        if (e.Parameters != null && e.Parameters.ToString() != "")
        {
            Session["ssCodigoFluxoSelecionado"] = e.Parameters;
            gvProjetosFluxo.DataBind();
        }
    }
    protected void gvProjetosFluxo_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {

        bool modoConsulta = false;
        bool temRegistro = (gvProjetosFluxo.GetRowValues(e.VisibleIndex, "existeRegistro") != null) ? gvProjetosFluxo.GetRowValues(e.VisibleIndex, "existeRegistro").ToString() == "S" : false;
        bool controlado = (gvProjetosFluxo.GetRowValues(e.VisibleIndex, "indicaControlado") != null) ? gvProjetosFluxo.GetRowValues(e.VisibleIndex, "indicaControlado").ToString() == "S" : false;

        string tipoOperacao = hfGeral.Contains("TipoOperacao") ? hfGeral.Get("TipoOperacao") + "" : "";

        modoConsulta = (tipoOperacao == "Consultar");

        if (e.ButtonID.Equals("btnExcluirDisp"))
        {
            e.Text = "Desativar disparo automatico";
            if (modoConsulta || !temRegistro || !controlado)
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
            else
            {
                e.Enabled = true;
                e.Image.Url = "~/imagens/botoes/excluirReg02.PNG";

            }
        }
        if (e.ButtonID.Equals("btnEditarDisp"))
        {
            if (modoConsulta)
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
            else
            {
                e.Enabled = true;
                e.Image.Url = "~/imagens/botoes/editarReg02.PNG";
            }
        }
    }

    public void populaGridProjetos()
    {
        gvProjetosFluxo.DataBind();
    }

    private void populacmbPeriodicidade()
    {
        string comandoSQL = string.Format(@"
                        SELECT [CodigoPeriodicidade]
                              ,[DescricaoPeriodicidade_PT]
                              ,[IntervaloDias]
                          FROM {0}.{1}.[TipoPeriodicidade]
                          ", cDados.getDbName(), cDados.getDbOwner());
        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cmbPeriodicidade.Items.Count == 0 && cDados.DataSetOk(ds))
        {
            cmbPeriodicidade.TextField = "DescricaoPeriodicidade_PT";
            cmbPeriodicidade.ValueField = "CodigoPeriodicidade";
            cmbPeriodicidade.DataSource = ds.Tables[0];
            cmbPeriodicidade.DataBind();
        }
    }

    private string calculaProximaExecucao(string inicio, string codigoPeriodicidade)
    {
        string comandoSQL = string.Format(@"
                        SELECT cast( intervaloDias as int) as [IntervaloDias]
                          FROM {0}.{1}.[TipoPeriodicidade]
                          where codigoPeriodicidade = {2}
                          ", cDados.getDbName(), cDados.getDbOwner(), codigoPeriodicidade);
        DataSet ds = cDados.getDataSet(comandoSQL);

        string intervalo = ds.Tables[0].Rows[0][0].ToString();
        int soma = int.Parse(intervalo);
        DateTime dt = Convert.ToDateTime(inicio).AddDays(soma);


        return dt.ToString();
    }
    // FIM DO DISPARO AUTOMATICO

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


    protected void hfStatusCopiaFluxo_CustomCallback(object sender, CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistroCopiaFLuxo();
        }

        // Grava a mensagem de erro. Se não houve erro, terá conteúdo ""
        hfStatusCopiaFluxo.Set("ErroSalvar", mensagemErro_Persistencia);
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfStatusCopiaFluxo.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            ddlFluxoDestino.SelectedIndex = -1;

        }
        else // alguma coisa deu errado...
            hfStatusCopiaFluxo.Set("StatusSalvar", "0"); // 1 indica que foi salvo com sucesso.
    }

    private string persisteEdicaoRegistroCopiaFLuxo()
    {
        try
        {
            fluxoDestino = hfStatusCopiaFluxo.Get("CodigoFluxoDestino").ToString();//ddlFluxoDestino.Value.ToString();
            fluxoOrigem = hfStatusCopiaFluxo.Get("CodigoWorkflow").ToString();

            string msgErro = salvaRegistroCopiaFluxo("E", int.Parse(fluxoOrigem), int.Parse(fluxoDestino));

            populaGridFluxos();

            return msgErro;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string salvaRegistroCopiaFluxo(string modo, int CodigoFluxoOrigem, int CodigoFluxoDestino)
    {


        //chamar procedure  p_adm_clonaFluxo
        string comandoSQL = string.Format(
                            @"BEGIN
                            DECLARE @retorno varchar(2048)

                            EXEC [dbo].[p_adm_clonaFluxo] {0}, {1}, {2}, @retorno output 

                            SELECT  @retorno

                          END", CodigoFluxoOrigem, CodigoFluxoDestino, idUsuarioLogado);
        DataSet ds = cDados.getDataSet(comandoSQL);
        string retorno = ds.Tables[0].Rows[0][0].ToString();
        return retorno;
    }

    protected void populaComboFluxoDestino(string parametro)
    {
        //pn_ddlFluxoDestino.PerformCallback(codigoWorkflow + '|' + codigoFluxo);

        string[] parametros = parametro.Split('|');

        fluxoDestino = "-1";
        string comandoSQL = string.Format(@"

                SELECT wf.CodigoWorkflow AS codigo
	                ,f.NomeFluxo + ' - ' + isnull(wf.DescricaoVersao, '') AS descricao
                FROM {0}.{1}.Workflows wf
                INNER JOIN {0}.{1}.Fluxos f ON (
		                f.CodigoFluxo = wf.CodigoFluxo
		                AND f.DataDesativacao IS NULL
		                AND f.StatusFluxo = 'A'
		                )
                WHERE wf.DataPublicacao IS NULL
	                AND wf.CodigoWorkflow = (
		                SELECT MAX(wf2.codigoworkflow)
		                FROM {0}.{1}.Workflows wf2
		                WHERE wf2.CodigoFluxo = f.CodigoFluxo
		                )
	                AND wf.CodigoWorkflow NOT IN ({2})
                    AND f.CodigoEntidade = {4}
                    AND NOT EXISTS( SELECT 1 FROM {0}.{1}.[SubWorkflows] AS [swf]
                    WHERE swf.[CodigoSubWorkflow] = wf.[CodigoWorkflow]) 

                order by 2    
                          ", cDados.getDbName(), cDados.getDbOwner(), parametros[0], Resources.traducao.selecione, CodigoEntidade);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlFluxoDestino.DataSource = ds.Tables[0];
                ddlFluxoDestino.ValueField = "codigo";
                ddlFluxoDestino.TextField = "descricao";
                ddlFluxoDestino.DataBind();
            }
            else
            {
                ddlFluxoDestino.Visible = false;
                //ddlFluxoDestino = codigoEntidadeLogada + "";
            }
        }
    }

    protected void pn_ddlFluxoDestino_Callback(object sender, CallbackEventArgsBase e)
    {
        populaComboFluxoDestino(e.Parameter);
    }

    protected void callbackIntegracoes_Callback(object source, CallbackEventArgs e)
    {
        try
        {
            switch (e.Parameter)
            {
                case "permitir-acionamento-exteno":
                    PermitirAcionamentoExterno();
                    break;
                case "gerar-esquema":
                    e.Result = GerarEsquema();
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            callbackIntegracoes.JSProperties["cp_Erro"] = ex.Message;
        }
    }

    private void InicializaComponentesIntegracao()
    {
        int codigoFluxo;
        if (int.TryParse(getChavePrimaria(), out codigoFluxo))
        {
            var sql = string.Format("SELECT [IndicaPossivelAcessoViaAPI] FROM [dbo].[Fluxos] WHERE [CodigoFluxo] = {0}", codigoFluxo);

            var dataSet = cDados.getDataSet(sql);

            var indicaPossivelAcessoViaAPI = dataSet.Tables[0].Rows[0]["IndicaPossivelAcessoViaAPI"] as string;
            cbPermitirAcionamentoExterno.Checked = (indicaPossivelAcessoViaAPI ?? string.Empty).ToUpper().Equals("S");
            btnGerarEsquema.ClientEnabled = cbPermitirAcionamentoExterno.Checked;
        }
    }

    private string GerarEsquema()
    {
        int codigoFluxo;
        if (int.TryParse(getChavePrimaria(), out codigoFluxo))
        {
            var sql = string.Format(@"
DECLARE @RC int
DECLARE @in_codigoFluxo int
DECLARE @in_codigoEtapa smallint

    SET @in_codigoFluxo = {0}
    SET @in_codigoEtapa = 0

EXECUTE @RC = [dbo].[p_geraFormatoJSONPreenchimentoFormularios] 
   @in_codigoFluxo
  ,@in_codigoEtapa", codigoFluxo);

            var dataSet = cDados.getDataSet(sql);
            var json = dataSet.Tables[0].Rows[0][0] as string;

            return JsonConvert.SerializeObject(JsonConvert.DeserializeObject(json), Formatting.Indented);
        }
        return string.Empty;
    }

    private void PermitirAcionamentoExterno()
    {
        int codigoFluxo;
        if (int.TryParse(getChavePrimaria(), out codigoFluxo))
        {
            var sql = string.Format(@"
UPDATE [dbo].[Fluxos]
SET [IndicaPossivelAcessoViaAPI] = '{0}'
WHERE [CodigoFluxo] = {1}", cbPermitirAcionamentoExterno.Checked ? "S" : "N", codigoFluxo);

            int registrosAfetados = 0;
            cDados.execSQL(sql, ref registrosAfetados);
        }
    }

    protected void pageControl_Callback(object sender, CallbackEventArgsBase e)
    {
        InicializaComponentesIntegracao();
    }
}
