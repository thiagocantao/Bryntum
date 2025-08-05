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
using CDIS;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Globalization;

public partial class Reunioes_reuniaoTecnicaPlanejamento : System.Web.UI.Page
{
    #region Fields (17)

    public string alturaTabela = "";
    dados cDados;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoObjeto = 0;
    private int codigoUsuarioResponsavel;
    private bool editaMensagemEvento = true;
    private int mesSelecionado = DateTime.Now.Month;
    private string moduloSistema = "", iniciaisObjeto = "";
    public string nomeSistema = "";
    public bool podeIncluir = true, somenteLeitura = false;
    public string tipoReuniao = "";
    public bool usuarioAdm = false;
    public bool usuarioConsultor = false;
    public bool usuarioExecutivo = false;
    public bool usuarioExecutor = false;
    public bool visibleTD = false;
    public string timeZoneId = "";

    #endregion Fields

    #region Methods (8)

    // Protected Methods (7) 

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string listaCodigos = "";
        DataSet ds = cDados.getParticipantesGrupoSelecionados(codigoEntidadeUsuarioResponsavel.ToString(), e.Parameter, moduloSistema, iniciaisObjeto, codigoObjeto.ToString(), "");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listaCodigos += ds.Tables[0].Rows[i]["NomeUsuario"].ToString().TrimEnd() + "¥" + ds.Tables[0].Rows[i]["CodigoUsuario"].ToString() + ";";
            }
        }
        callback.JSProperties["cp_ListaCodigos"] = listaCodigos;
    }

    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGrid();
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        string terminoReal = gvDados.GetRowValues(e.VisibleIndex, "TerminoRealData") + "";
        bool PermissaoEditarAtaEntidade = (gvDados.GetRowValues(e.VisibleIndex, "PermissaoEditarAtaEntidade") + "") == "True";
        bool PermissaoEditarAtaResponsavel = (gvDados.GetRowValues(e.VisibleIndex, "PermissaoEditarAtaResponsavel") + "") == "True";



        if (e.ButtonID == "btnDetalheReal")
        {
            if (terminoReal == "")
                e.Visible = DevExpress.Utils.DefaultBoolean.False;
        }
        else if (e.ButtonID == "btnDetalheCustom")
        {
            if (terminoReal != "")
                e.Visible = DevExpress.Utils.DefaultBoolean.False;
        }
        else if (e.ButtonID == "btnExcluirCustom")
        {
            if (terminoReal != ""){
                e.Enabled = false;
                e.Text = "Excluir";
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }
        else if (e.ButtonID == "btnReal")
        {
            if (terminoReal != "" && !(PermissaoEditarAtaEntidade || PermissaoEditarAtaResponsavel))
            {
                e.Enabled = false;
                e.Text = "Editar Realização";
                e.Image.Url = "~/imagens/realizacaoReuniaoDes.PNG";
            }

        }
        else if (somenteLeitura)
            e.Visible = DevExpress.Utils.DefaultBoolean.False;

    }

    protected void gvProjetos_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        int codigoEvento = int.Parse(getChavePrimaria());

        DataSet dsProjetos;

        if (tipoReuniao == "E")
        {
            dsProjetos = cDados.getObjetivosPlanejamentoEvento(codigoEvento, codigoEntidadeUsuarioResponsavel, codigoObjeto, codigoUsuarioResponsavel, "");
        }
        else
        {
            dsProjetos = cDados.getProjetosPlanejamentoEvento(codigoEvento, codigoEntidadeUsuarioResponsavel, codigoObjeto, codigoUsuarioResponsavel, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()), "");
        }
        if (cDados.DataSetOk(dsProjetos))
        {
            gvProjetos.DataSource = dsProjetos;

            gvProjetos.DataBind();
        }
    }

    protected void gvProjetos_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters.ToString() != "")
        {
            int chave = int.Parse(e.Parameters.ToString());

            carregaListaProjetos(chave);
        }
    }

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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        
        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();
        SqlDataSource1.ConnectionString = cDados.classeDados.getStringConexao();

        if (Request.QueryString["MOD"] != null) //Ok
            moduloSistema = Request.QueryString["MOD"].ToString();
        if (Request.QueryString["IOB"] != null) //Ok
            iniciaisObjeto = Request.QueryString["IOB"].ToString();

        this.Title = cDados.getNomeSistema();

        DataSet ds = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "editaMensagemEvento", "timeZoneId");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            editaMensagemEvento = ds.Tables[0].Rows[0]["editaMensagemEvento"].ToString().Equals("S");
            timeZoneId = ds.Tables[0].Rows[0]["timeZoneId"].ToString();

        btnEnviarPauta.JSProperties["cp_EditaMensagem"] = editaMensagemEvento ? "S" : "N";
        //mmEncabecadoPauta.Text = string.Format("Você foi convidado para uma reunião agendada no {0}, conforme informações apresentadas a seguir:", cDados.getNomeSistema());
        heEncabecadoAta.Html = string.Format(Resources.traducao.reuniaoTecnicaPlanejamento_voc__foi_convidado_para_uma_reuni_o_agendada_no__0___conforme_informa__es_apresentadas_a_seguir_, cDados.getNomeSistema());

        btnEnviarPauta.ToolTip = string.Format(Resources.traducao.reunioes_o_envio_da_pauta_s___permitido_para_reuni_es_com_data_futura);
        carregaComboResponsaveis();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        tipoReuniao = Request.QueryString["TipoReuniao"].ToString();
        /* tipoReuniao = T -> Reunião Técnica de planejamento chamada lista de projetos
         *                    Utiliza a permissão UN_AdmReuTec para Editar a reunião
         * tipoReuniao = E -> Reunião Estratégica chamada Mapa Estratégico
         *                    Utiliza a permissão RE_Adm para Editar a reunião
         *                    
         * 
         */
        gvDados.JSProperties["cp_tipoReuniao"] = tipoReuniao;

        if (tipoReuniao == "E")
        {
            if (cDados.getInfoSistema("CodigoMapa") != null) //Ok
                codigoObjeto = int.Parse(cDados.getInfoSistema("CodigoMapa").ToString());

            somenteLeitura = !cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "RE_Adm");

            if (!IsPostBack)
            {
                cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "NULL", "EN", 0, "NULL", "RE_Cns");
            }
        }
        else
        {
            if (Request.QueryString["COB"] != null) //Ok
                codigoObjeto = int.Parse(Request.QueryString["COB"].ToString());

            somenteLeitura = !cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoObjeto, "null", "UN", 0, "null", "UN_AdmReuTec");

            if (!IsPostBack)
            {
                cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoObjeto, "NULL", "UN", 0, "NULL", "UN_CnsReuTec");
            }
        }

        nomeSistema = cDados.getNomeSistema();

        // inclui as funcionalidades do Eventos.
        int codigoTipoAssociacao = cDados.getCodigoTipoAssociacao(iniciaisObjeto);
        int codigoObjetoAssociado = -1;
        if (hfGeral.Contains("codigoObjetoAssociado"))
            codigoObjetoAssociado = int.Parse(hfGeral.Get("codigoObjetoAssociado").ToString());

        //if (hfGeral.Contains("TipoOperacao") && somenteLeitura == false)
        //    somenteLeitura = (hfGeral.Get("TipoOperacao").ToString() != "Incluir" && hfGeral.Get("TipoOperacao").ToString() != "Editar");

        if (!IsPostBack && !IsCallback)
            tabControl.ActiveTabIndex = 0;

        podeIncluir = !somenteLeitura;

        if (!IsPostBack)
        {
            hfGeral.Set("TipoOperacao", "");
            hfGeral.Set("CodigosSelecionados", "");

            carregaListaParticipantes("-1");

            preencheDadosUnidade();

            lblTituloTela.Text = Resources.traducao.reuni_es_tecnicas_da_unidade;
        }

        cDados.aplicaEstiloVisual(Page);
        cDados.aplicaEstiloVisual(memoPauta, "Default");
        carregaComboTiposEventos();
        carregaGrid();
        defineAlturaTela();
        verificaTipoReuniao();
        carregaGrupos("");
        if (!IsPostBack)
        {
            int nivelEstrutura = 2;

            if (tipoReuniao == "E")
                nivelEstrutura = 1;

            cDados.excluiNiveisAbaixo(nivelEstrutura);
            cDados.insereNivel(nivelEstrutura, this);
            Master.geraRastroSite();
        }
    }
    // Private Methods (1) 

    private void carregaGrupos(string where)
    {
        string comandoSQL = string.Format(@"SELECT gpe.CodigoGrupoParticipantes
                                                 ,gpe.DescricaoGrupoParticipantes
                                            FROM {0}.{1}.GrupoParticipantesEvento gpe
                                            WHERE CodigoEntidade = {2}  {3}
                                            order by DescricaoGrupoParticipantes", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, where);


        DataSet ds = cDados.getDataSet(comandoSQL);
        lbGrupos.TextField = "DescricaoGrupoParticipantes";
        lbGrupos.ValueField = "CodigoGrupoParticipantes";
        lbGrupos.DataSource = ds;
        lbGrupos.DataBind();
    }

    #endregion Methods



    #region VARIOS

    private void preencheDadosUnidade()
    {
        if (tipoReuniao == "E")
        {
            DataSet dsMapa = cDados.getMapasEstrategicos(null, " AND Mapa.CodigoMapaEstrategico = " + cDados.getInfoSistema("CodigoMapa").ToString());

            if (cDados.DataSetOk(dsMapa) && cDados.DataTableOk(dsMapa.Tables[0]))
                txtUnidade.Text = dsMapa.Tables[0].Rows[0]["TituloMapaEstrategico"].ToString();
        }
        else
        {
            DataSet dsUnidade = cDados.getUnidade("AND CodigoUnidadeNegocio = " + codigoObjeto);

            if (cDados.DataSetOk(dsUnidade) && cDados.DataTableOk(dsUnidade.Tables[0]))
                txtUnidade.Text = dsUnidade.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();
        }
    }

    private void defineAlturaTela() //Ok
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));

        //alturaPrincipal = 

        int altura = (alturaPrincipal - 170);

        if (altura > 0)
        {
            gvDados.Settings.VerticalScrollableHeight = altura - 270;
            lbDisponiveis.Height = ((altura - 159) / 2) + 54;
            lbSelecionados.Height = ((altura - 154));
            lbGrupos.Height = (((altura - 159) / 2) - 60);
            memoPauta.Height = new Unit((altura - 246) + "px");
            //gvProjetos.Settings.VerticalScrollableHeight = altura - 202;
            //memoAta.Height = new Unit((altura - 170) + "px");
            //alturaTabelaPlanoAcao       = altura - 250;
            //pnCallback.Height = altura - 95;
            hfGeral.Set("alturaTela", (altura - 133));
        }
    }

    private void verificaTipoReuniao()
    {
        if (tipoReuniao == "E")
        {
            tabControl.TabPages[2].Text = Resources.traducao.objetivos_estrat_gicos;
            gvProjetos.Columns[2].Caption = Resources.traducao.objetivos_estrat_gicos;
            lblTituloTela.Text = Resources.traducao.reuniaoTecnicaPlanejamento_reuni_es_de_an_lise_estrat_gica;
            lblUnidade.Text = Resources.traducao.reuniaoTecnicaPlanejamento_mapa_estrat_gico_;
            pcDados.HeaderText = Resources.traducao.reuniaoTecnicaPlanejamento_reuni_o_do_mapa_estrat_gico;
        }
    }

    private void HeaderOnTela()
    {
        //Tela não cacheable.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/reuniaoTecnicaPlanejamento.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/reunioes_ASPxListbox.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "reuniaoTecnicaPlanejamento", "reunioes", "reunioes_ASPxListbox"));
    }

    #endregion

    #region GRIDVIEW

    private void carregaGrid()
    {
        string permissaoAta = "";

        if (tipoReuniao == "E")
             permissaoAta = "EN_EdtAtaEs" ;
        else if (tipoReuniao == "T")
            permissaoAta = "UN_EdtAtaTec";

        string where = string.Format(@"
                AND tev.CodigoModuloSistema = '{0}'
	            AND ev.CodigoObjetoAssociado = {1}
                AND ta.IniciaisTipoAssociacao = '{2}'", moduloSistema, codigoObjeto, iniciaisObjeto);

        DataTable dt = cDados.getEventosEntidade(codigoEntidadeUsuarioResponsavel.ToString(), where, codigoUsuarioResponsavel.ToString(), permissaoAta).Tables[0];
        gvDados.DataSource = dt; // tlEventos.DataSource = dt;
        gvDados.DataBind();      // tlEventos.DataBind();
    }



    #endregion

    #region COMBOBOX

    private void carregaComboResponsaveis() //Ok
    {
       
            ddlResponsavelEvento.TextField = "NomeUsuario";
            ddlResponsavelEvento.ValueField = "CodigoUsuario";
            ddlResponsavelEvento.Columns[0].FieldName = "NomeUsuario";
            ddlResponsavelEvento.Columns[1].FieldName = "EMail";
            ddlResponsavelEvento.TextFormatString = "{0}";
       
        //if (!IsPostBack && ddlResponsavelEvento.Items.Count > 0 && idEvento == -1)
        //    ddlResponsavelEvento.SelectedIndex = 0;
    }

    private void carregaComboTiposEventos()
    {
        string where = string.Format("AND CodigoModuloSistema = '{0}'", moduloSistema);
        DataTable dtTiposEventos = cDados.getTiposEventos(codigoEntidadeUsuarioResponsavel, where).Tables[0];

        ddlTipoEvento.DataSource = dtTiposEventos;
        ddlTipoEvento.TextField = "DescricaoTipoEvento";
        ddlTipoEvento.ValueField = "CodigoTipoEvento";
        ddlTipoEvento.DataBind();

        //if (!IsPostBack && ddlTipoEvento.Items.Count > 0 && idEvento == -1)
        //    ddlTipoEvento.SelectedIndex = 0;
    }

    #endregion

    #region LISTBOX

    private void carregaListaParticipantes(string codigoEvento)
    {
        string where = string.Format(@"
                        AND u.CodigoUsuario NOT IN(SELECT CodigoParticipante 
                                                   FROM {0}.{1}.ParticipanteEvento
                                                  WHERE CodigoEvento = {2})",
               cDados.getDbName(), cDados.getDbOwner(), codigoEvento);
        DataTable dtParticipantes = cDados.getParticipantesEventos(moduloSistema, iniciaisObjeto, codigoObjeto.ToString(), codigoEntidadeUsuarioResponsavel.ToString(), where).Tables[0];

        lbDisponiveis.DataSource = dtParticipantes;
        lbDisponiveis.TextField = "NomeUsuario";
        lbDisponiveis.ValueField = "CodigoUsuario";
        lbDisponiveis.DataBind();

        if (lbDisponiveis.Items.Count > 0)
            lbDisponiveis.SelectedIndex = -1;
    }

    protected void lbDisponiveis_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string codigoEvento = e.Parameter.ToString();
        if (codigoEvento != "")
            carregaListaParticipantes(codigoEvento);
    }

    private void carregaListaParticipantesAssociados(string codigoEvento, string where)
    {
        DataSet ds = cDados.getParticipantesConfirmacaoEventos(codigoEvento, where);

        lbSelecionados.DataSource = ds.Tables[0];
        lbSelecionados.TextField = "NomeUsuario";
        lbSelecionados.ValueField = "CodigoUsuario";
        lbSelecionados.DataBind();

        if (lbSelecionados.Items.Count > 0)
            lbSelecionados.SelectedIndex = -1;
    }

    protected void lbSelecionados_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string codigoEvento = e.Parameter.ToString();
        string where = "";
        if (codigoEvento != "")
            carregaListaParticipantesAssociados(codigoEvento, where);
    }

    #endregion

    #region Lista de Projetos ou Objetivos

    private void carregaListaProjetos(int codigoEvento)
    {
        DataSet dsProjetos;

        if (tipoReuniao == "E")
        {
            dsProjetos = cDados.getObjetivosPlanejamentoEvento(codigoEvento, codigoEntidadeUsuarioResponsavel, codigoObjeto, codigoUsuarioResponsavel, "");
        }
        else
        {
            dsProjetos = cDados.getProjetosPlanejamentoEvento(codigoEvento, codigoEntidadeUsuarioResponsavel, codigoObjeto, codigoUsuarioResponsavel, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()), "");
        }

        if (cDados.DataSetOk(dsProjetos))
        {
            gvProjetos.DataSource = dsProjetos;

            gvProjetos.DataBind();

            gvProjetos.Selection.UnselectAll();

            int i = 0;
            foreach (DataRow dr in dsProjetos.Tables[0].Rows)
            {
                if (dr["Selecionado"].ToString() == "S")
                    gvProjetos.Selection.SelectRow(i);

                i++;
            }
        }
    }

    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.GetSelectedFieldValues(gvDados.KeyFieldName).Count > 0)
            return gvDados.GetSelectedFieldValues(gvDados.KeyFieldName)[0].ToString();
        else
            return "-1";
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_OperacaoOk"] = "";
        pnCallback.JSProperties["cp_Erro"] = "";
        pnCallback.JSProperties["cp_PodeEnviarAta"] = "N";

        bool podeEnviarAta = verificaSePodeEnviarAta(ddlTerminoPrevisto);
        pnCallback.JSProperties["cp_PodeEnviarAta"] = (podeEnviarAta == true) ? "S" : "N";
        if (e.Parameter == "EnviarPauta")
        {
            mensagemErro_Persistencia = persisteEdicaoPauta();
        }
        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else
        {
            pnCallback.JSProperties["cp_Erro"] = mensagemErro_Persistencia;
            pnCallback.JSProperties["cp_OperacaoOk"] = "Erro";            
        }

        hfGeral.Set("TipoOperacao", "Consultar");
    }

    private bool verificaSePodeEnviarAta(ASPxDateEdit ddlTerminoPrevisto)
    {
        bool retorno = true;
        DateTime agora = DateTime.Now;
        retorno = (agora < ddlTerminoPrevisto.Date);
        return retorno;
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        DataSet dsTipoAssociacao = cDados.getTipoAssociacaoEventos(iniciaisObjeto, "");

        string tipoAssociacao = "0";

        if (cDados.DataSetOk(dsTipoAssociacao) && cDados.DataTableOk(dsTipoAssociacao.Tables[0]))
            tipoAssociacao = dsTipoAssociacao.Tables[0].Rows[0]["CodigoTipoAssociacao"].ToString();

        //-- compilação dos dados para Inserir.
        string descricaoResumida = txtAssunto.Text.Replace("'", "''");
        string memoPau = memoPauta.Html.Replace("'", "''");
        string memoLoc = memoLocal.Text.Replace("'", "''");
        
        string dataInicioPrevisto = ddlInicioPrevisto.Text + " " + txtHoraInicio.Text;
        string dataTerminoPrevisto = ddlTerminoPrevisto.Text + " " + txtHoraTermino.Text;

        bool podeEnviarAta = (DateTime.Now < ddlTerminoPrevisto.Date);

        string responsavel = ddlResponsavelEvento.Value.ToString();
        string tipoEvento = ddlTipoEvento.Value.ToString();


        string[] arrayParticipantesSelecionados = hfGeral.Get("CodigosSelecionados").ToString().Split(';');

        int[] arrayProjetos = new int[gvProjetos.GetSelectedFieldValues("Codigo").Count];

        for (int i = 0; i < arrayProjetos.Length; i++)
            arrayProjetos[i] = int.Parse(gvProjetos.GetSelectedFieldValues("Codigo")[i].ToString());

        string idEventoNovo = "";
        string mesgError = "";

        try
        {
            bool result = cDados.incluiEvento(descricaoResumida, responsavel, dataInicioPrevisto, dataTerminoPrevisto,
                                              "NULL", "NULL", tipoAssociacao, codigoObjeto.ToString(),
                                              memoLoc, memoPau, "", codigoEntidadeUsuarioResponsavel.ToString(),
                                              "NULL", "NULL", tipoEvento, codigoUsuarioResponsavel.ToString(),
                                              ref idEventoNovo, ref mesgError);


            if (result == false)
            {
                if (mesgError.Contains("UQ_Projeto_NomeProjeto"))
                    return Resources.traducao.reuniaoTecnicaPlanejamento_nome_do_projeto_j__existe_;
                else
                    return mesgError;
            }
            else
            {
                if (arrayParticipantesSelecionados.Length > 0)
                    cDados.incluiParticipantesSelecionados(arrayParticipantesSelecionados, idEventoNovo);

                if (arrayProjetos.Length > 0)
                {
                    if (tipoReuniao == "E")
                    {
                        cDados.incluiObjetivosSelecionados(arrayProjetos, int.Parse(idEventoNovo), codigoEntidadeUsuarioResponsavel);
                    }
                    else
                    {
                        cDados.incluiProjetosSelecionados(arrayProjetos, idEventoNovo);
                    }
                }

                hfGeral.Set("codigoObjetoAssociado", idEventoNovo);
                carregaGrid();
                gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(idEventoNovo);
                gvDados.ClientVisible = false;
                return "";
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        DataSet dsTipoAssociacao = cDados.getTipoAssociacaoEventos(iniciaisObjeto, "");

        string tipoAssociacao = "0";

        if (cDados.DataSetOk(dsTipoAssociacao) && cDados.DataTableOk(dsTipoAssociacao.Tables[0]))
            tipoAssociacao = dsTipoAssociacao.Tables[0].Rows[0]["CodigoTipoAssociacao"].ToString();

        // busca a chave primaria
        string chave = getChavePrimaria();

        //-- compilação dos dados para Inserir.
        string descricaoResumida = txtAssunto.Text.Replace("'", "''");
        string memoPau = memoPauta.Html.Replace("'", "''");
        string memoLoc = memoLocal.Text.Replace("'", "''");

        string dataInicioPrevisto = ddlInicioPrevisto.Text + " " + txtHoraInicio.Text;
        string dataTerminoPrevisto = ddlTerminoPrevisto.Text + " " + txtHoraTermino.Text;

        string responsavel = ddlResponsavelEvento.Value + "";
        string tipoEvento = ddlTipoEvento.Value.ToString();

        string[] arrayParticipantesSelecionados = hfGeral.Get("CodigosSelecionados").ToString().Split(';');
        int[] arrayProjetos = new int[gvProjetos.GetSelectedFieldValues("Codigo").Count];
        string mesgError = "";

        for (int i = 0; i < arrayProjetos.Length; i++)
            arrayProjetos[i] = int.Parse(gvProjetos.GetSelectedFieldValues("Codigo")[i].ToString());

        cDados.atualizaEvento(descricaoResumida, responsavel, dataInicioPrevisto, dataTerminoPrevisto, "NULL",
                              "NULL", tipoAssociacao, codigoObjeto.ToString(), memoLoc, memoPau, "",
                              codigoEntidadeUsuarioResponsavel.ToString(), "NULL", "NULL", tipoEvento,
                              codigoUsuarioResponsavel.ToString(), chave, false, ref mesgError);

        if (arrayParticipantesSelecionados.Length > 0)
            cDados.incluiParticipantesSelecionados(arrayParticipantesSelecionados, chave);

        if (arrayProjetos.Length > 0)
        {
            if (tipoReuniao == "E")
            {
                cDados.incluiObjetivosSelecionados(arrayProjetos, int.Parse(chave), codigoEntidadeUsuarioResponsavel);
            }
            else
            {
                cDados.incluiProjetosSelecionados(arrayProjetos, chave);
            }
        }

        hfGeral.Set("codigoObjetoAssociado", chave);
        carregaGrid();
        gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(chave);
        gvDados.ClientVisible = false;
        return mesgError;
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();
        string msgRetorno = "";

        cDados.excluiEvento(chave, ref msgRetorno);
        carregaGrid();
        //gvDados.ClientVisible = false;

        return msgRetorno;
    }

    private string persisteEdicaoPauta()
    {

        string msgError1 = "";
        string msgError2 = "";
        string msgError3 = "";
        string assunto = "";
        string mensagem = "";
        string destinatarios = "";
        string whereDestinatarios = "";
        string chave = getChavePrimaria();
        string indicadonDatas = "";
        string horaInicio = "00:00";
        string horaTermino = "00:00";

        //Convertendo as datas em horário universal
        IFormatProvider iFormatProvider = new CultureInfo(System.Globalization.CultureInfo.CurrentCulture.Name, false);

        DateTime dataEHoraInicioPrevisto = DateTime.Parse(ddlInicioPrevisto.Text + " " + txtHoraInicio.Text, iFormatProvider, DateTimeStyles.AssumeLocal |DateTimeStyles.AdjustToUniversal);
        DateTime dataEHoraTerminoPrevisto = DateTime.Parse(ddlTerminoPrevisto.Text + " " + txtHoraTermino.Text, iFormatProvider, DateTimeStyles.AssumeLocal | DateTimeStyles.AdjustToUniversal);

        horaInicio = dataEHoraInicioPrevisto.ToString("HH:mm");
        horaTermino = dataEHoraTerminoPrevisto.ToString("HH:mm");

        //FORMATAÇÃO DE DATAS

        if (ddlInicioPrevisto.Text.Equals(ddlTerminoPrevisto.Text))
        {
            string inicioPrevisto = ddlInicioPrevisto.Text;
            DateTime dateTimeInicio = DateTime.Parse(cDados.converteDataHoraBd(inicioPrevisto));

            string quando = string.Format("{0:D}", dateTimeInicio);
            quando += "  " + txtHoraInicio.Text + "-" + txtHoraTermino.Text;

            indicadonDatas = string.Format(@"<tr style='font-family:Verdana; font-size:9pt'>
                                                <td><b>{1} :</b></td>
                                                <td>{0}</td>
                                           </tr>", quando, Resources.traducao.reuniaoTecnicaPlanejamento_quando);
        }
        else
        {
            string inicioPrevisto = ddlInicioPrevisto.Text + " - " + txtHoraInicio.Text; ;
            string terminoPrevisto = ddlTerminoPrevisto.Text + " - " + txtHoraTermino.Text; ;

            indicadonDatas = string.Format(@"<tr style='font-family:Verdana; font-size:9pt'>
                                                <td><b>{2} :</b></td>
                                                <td>{0}</td>
                                           </tr>
                                           <tr style='font-family:Verdana; font-size:9pt'>
                                                <td><b>{3} :</b></td>
                                                <td>{1}</td>
                                           </tr>", inicioPrevisto, terminoPrevisto, Resources.traducao.reuniaoTecnicaPlanejamento_in_cio_previsto, Resources.traducao.reuniaoTecnicaPlanejamento_t_rmino_previsto);
        }

        string[] arrayParticipantesSelecionados = hfGeral.Get("CodigosSelecionados").ToString().Split(';');

        int i = 0;
        foreach (string codigoUsuario in arrayParticipantesSelecionados)
        {
            if ("" != codigoUsuario)
            {
                arrayParticipantesSelecionados[i++] = codigoUsuario;
                whereDestinatarios += codigoUsuario + ",";
            }
        }

        //foreach (ListEditItem lei in lbSelecionados.Items)
        //    whereDestinatarios += lei.Value + ",";

        string usuariosReuniao = "";

        whereDestinatarios = " AND u.CodigoUsuario IN(" + whereDestinatarios + ddlResponsavelEvento.Value + ")";
        DataTable dtResponsaveis = cDados.getUsuarios(whereDestinatarios).Tables[0];//cDados.getUsuariosCadastrados(idEntidade, whereDestinatarios).Tables[0];

        int contador = 0;

        foreach (DataRow dr in dtResponsaveis.Rows)
        {
            destinatarios += dr["EMail"].ToString() + "; ";

            usuariosReuniao += dr["CodigoUsuario"].ToString() == ddlResponsavelEvento.Value.ToString() ? string.Format(@"ATTENDEE;ROLE=CHAIR;PARTSTAT=ACCEPTED
 ;CN=""{0}"";RSVP=FALSE
 :mailto:{1}", dr["NomeUsuario"].ToString()
             , dr["EMail"].ToString()) :
                string.Format(@"ATTENDEE;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE
 :mailto:{0}", dr["EMail"].ToString());

            contador++;

            if (contador < dtResponsaveis.Rows.Count)
                usuariosReuniao += Environment.NewLine;
        }

        string projetos = "";

        for (i = 0; i < gvProjetos.GetSelectedFieldValues("Descricao").Count; i++)
            projetos += "<br style='font-family:Verdana; font-size:9pt'> - " + gvProjetos.GetSelectedFieldValues("Descricao")[i].ToString();

        if (projetos == "")
            projetos = "<br style='font-family:Verdana; font-size:9pt'> - " + Resources.traducao.reuniaoTecnicaPlanejamento_nenhum_projeto_;

        string nomeDescricao = Resources.traducao.reuniaoTecnicaPlanejamento_projetos, descricaoUnidade = Resources.traducao.reuniaoTecnicaPlanejamento_unidade;

        if (tipoReuniao == "E")
        {
            nomeDescricao = Resources.traducao.reuniaoTecnicaPlanejamento_objetivos_estrat_gicos;
            descricaoUnidade = Resources.traducao.reuniaoTecnicaPlanejamento_mapa_estrat_gico;
            if (projetos == "")
                projetos = "<br style='font-family:Verdana; font-size:9pt'> - " + Resources.traducao.reuniaoTecnicaPlanejamento_nenhum_objetivo_estrat_gico_;
        }

        DataSet dsUsuarioLogado = cDados.getUsuarios(" AND u.CodigoUsuario = " + ddlResponsavelEvento.Value);

        string nomeUsuarioLogado = dsUsuarioLogado.Tables[0].Rows[0]["NomeUsuario"].ToString();
        string emailUsuarioLogado = dsUsuarioLogado.Tables[0].Rows[0]["EMail"].ToString();

        TimeSpan tsMeridiano = new TimeSpan(DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, DateTime.UtcNow.Second) - new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

        int horas = tsMeridiano.Hours * 100;

        string corpoAnexo = string.Format(@"BEGIN:VCALENDAR
PRODID:-//Google Inc//Google Calendar 70.9054//EN
VERSION:2.0
CALSCALE:GREGORIAN
METHOD:PUBLISH
X-MS-OLK-FORCEINSPECTOROPEN:TRUE
X-WR-CALNAME:Evento
X-WR-CALDESC:
BEGIN:VEVENT
CLASS: PUBLIC
DTSTART:{0:yyyyMMdd}T{1:D4}00Z
DTEND:{3:yyyyMMdd}T{4:D4}00Z
DTSTAMP:{0:yyyyMMdd}T{1:D4}00Z
ORGANIZER;CN={9}:mailto:{2}
UID:{0:yyyyMMdd}T{1}00{3:yyyyMMdd}T{4:D4}00-CDIS_Generated
{8}
CREATED:{0:yyyyMMdd}T{1:D4}00Z
DESCRIPTION: 
LAST-MODIFIED:{0:yyyyMMdd}T{1:D4}00Z
LOCATION:{7}
SEQUENCE:0
STATUS:TENTATIVE
SUMMARY;LANGUAGE=pt-br:{6}
X-ALT-DESC;FMTTYPE=text/html:{10}
TRANSP:OPAQUE
END:VEVENT
END:VCALENDAR", dataEHoraInicioPrevisto
 , (int.Parse(horaInicio.Replace(":", "")))
 , emailUsuarioLogado
 , dataEHoraTerminoPrevisto
 , (int.Parse(horaTermino.Replace(":", "")))
 , ddlTipoEvento.Text
 , txtAssunto.Text
 , memoLocal.Text
 , usuariosReuniao
 , nomeUsuarioLogado
 , memoPauta.Html);


        string encabecadoPauta = "";
        if (editaMensagemEvento)
            encabecadoPauta = heEncabecadoAta.Html; // mmEncabecadoPauta.Text;
        else
            encabecadoPauta = string.Format(Resources.traducao.reuniaoTecnicaPlanejamento_voc__foi_convidado_para_uma_reuni_o_agendada_no__0___conforme_informa__es_apresentadas_a_seguir_, cDados.getNomeSistema());

        assunto = string.Format("{2} {0} - {1}", nomeSistema.Replace("'", "''"), txtUnidade.Text, Resources.traducao.reuniaoTecnicaPlanejamento_reuni_o_planejada_no);
        mensagem = string.Format(@"<span style='font-family:Verdana; font-size:9pt'>
                                    <fieldset>
                                       <b>" + Resources.traducao.reuniaoTecnicaPlanejamento_prezado_a_ + @",</b>
                                       <p>{10}
                                       <p>
                                       <p>
                                       <table id='idDadosReuniao' border='0' cellpadding='0' cellspacing='0' style='font-family:Verdana; font-size:9pt'>
                                           <tr>
                                                <td style='width:150px'><b>{9} :</b></td>
                                                <td>{6}.</td>
                                           </tr>
                                           <tr>
                                                <td><b>" + Resources.traducao.reuniaoTecnicaPlanejamento_assunto + @" :</b></td>
                                                <td><i>{0}.</i></td>
                                           </tr>
                                           <tr>
                                                <td style='height: 10px'></td>
                                                <td></td>
                                           </tr>
                                          {11}
                                           <tr>
                                                <td style='height: 10px'></td>
                                                <td></td>
                                           </tr>
                                           <tr>
                                                <td><b>" + Resources.traducao.reuniaoTecnicaPlanejamento_local + @" :</b></td>
                                                <td>{3}</td>
                                           </tr>
                                           <tr>
                                                <td><b>" + Resources.traducao.reuniaoTecnicaPlanejamento_respons_vel + @" :</b></td>
                                                <td><i>{4}</i></td>
                                           </tr>
                                       </table>
                                       <p>
                                       <hr>
                                       <p><b>{8}:</b> 
                                       {7}
                                       <hr>
                                       <p><b>" + Resources.traducao.reuniaoTecnicaPlanejamento_pauta + @":</b> 
                                       <p>{5}.
                                       <hr>
                                       <p>Att.,
                                       <p>{12}.
                                       <p><b>" + Resources.traducao.reuniaoTecnicaPlanejamento_ps__por_favor__n_o_responda_esse_e_mail_ + @"</b>
                                       <p>
                                    </fieldset>                                   
                                  </span>
                                  ", txtAssunto.Text
                                   , ""
                                   , ""
                                   , memoLocal.Text
                                   , ddlResponsavelEvento.SelectedItem.Text
                                   , memoPauta.Html.Replace("'", "''")
                                   , txtUnidade.Text
                                   , projetos
                                   , nomeDescricao
                                   , descricaoUnidade
                                   , encabecadoPauta.Replace("'", "''")
                                   , indicadonDatas
                                   , cDados.getNomeSistema()
                               ); //Request.QueryString["NUN"].ToString());
        if (destinatarios != "")
        {
            int retornoStatus = 0;

            destinatarios = destinatarios.Remove(destinatarios.Length - 2);
            string statusEmail = cDados.enviarEmailCalendarAppWeb(assunto, destinatarios, "", mensagem, corpoAnexo, ref retornoStatus);
            if (retornoStatus == 1)
            {
                cDados.atualizaEnvioPauta(chave, codigoUsuarioResponsavel.ToString(), ref msgError1);
                cDados.atualizaParticipantesEnvioPautaEmail(arrayParticipantesSelecionados, chave, ref msgError2);

                msgError3 = persisteEdicaoRegistro();
            }
            else
            {
                msgError3 = statusEmail;
            }
        }
        return msgError1 + msgError2 + msgError3;
    }

    #endregion
    
    protected void lbGrupos_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {

    }
    
    protected void gvDados_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
    {
        string fieldName = e.Column.FieldName;
        if (fieldName == "InicioPrevisto")
        {
            var periodos = e.Values.Where(v => v.IsFilterByValue).Select(
                v => DateTime.Parse(v.Value)).Distinct(new MonthComparer()).ToList();
            e.Values.RemoveAll(v => !v.IsShowAllFilter);
            foreach (var p in periodos)
            {
                int ano = p.Year;
                int mes = p.Month;
                DateTime inicio = new DateTime(ano, mes, 1);
                DateTime termino = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));
                string query = string.Format("[{0}] >= #{1} 00:00:00# And [{0}] <= #{2} 23:59:59#",
                    fieldName, inicio.ToString("yyyy-MM-dd"), termino.ToString("yyyy-MM-dd"));
                e.AddValue(UppercaseFirst(p.ToString("y")), p.ToString(), query);
            }
        }
    }

    static string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
            return string.Empty;
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    class MonthComparer : IEqualityComparer<DateTime>
    {
        public bool Equals(DateTime x, DateTime y)
        {
            return x.Month == y.Month && x.Year == y.Year;
        }

        public int GetHashCode(DateTime obj)
        {
            if (Object.ReferenceEquals(obj, null))
                return 0;
            return (obj.Year * 12 + obj.Month).GetHashCode();
        }
    }
    
    protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (gvDados.GetRowValues(e.VisibleIndex, "IndicaAtrasada") + "" == "S")
            e.Row.ForeColor = Color.Red;
    }

    protected void ddlResponsavel_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        if (e.Value != null)
        {
            long value = 0;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            
            dsResponsavel.SelectCommand = cDados.getSQLComboUsuariosPorID(codigoEntidadeUsuarioResponsavel);

            dsResponsavel.SelectParameters.Clear();
            dsResponsavel.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsResponsavel;
            comboBox.DataBind();
        }
        else
        {
            comboBox.DataSource = SqlDataSource1;
            comboBox.DataBind();
        }
    }

    protected void ddlResponsavel_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string where = string.Format(@"AND us.CodigoUsuario IN (SELECT * 
                                                     FROM dbo.f_GetPossiveisResponsaveisReuniao('{0}', '{1}', {2}, {3}))"
            , moduloSistema, iniciaisObjeto, codigoObjeto.ToString(), codigoEntidadeUsuarioResponsavel.ToString());

        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidadeUsuarioResponsavel, e.Filter, where);

        cDados.populaComboVirtual(SqlDataSource1, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "LstReun");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "novaReuniao();", true, true, false, "LstReun", lblTituloTela.Text, this);
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

    protected void callbackEnviaPauta_Callback(object sender, CallbackEventArgsBase e)
    {
        btnEnviarPauta.ClientEnabled = (e.Parameter == "S");
    }
}
