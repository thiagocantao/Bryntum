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

public partial class Reunioes_reuniaoTecnicaPlanejamento1 : System.Web.UI.Page
{
		#region Fields (17) 

    public string alturaTabela = "";
    dados cDados;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoObjeto = 0;
    private int codigoUsuarioResponsavel;
    private bool editaMensagemEvento = true;
    private int idEvento = -1;
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
        int chave = int.Parse(e.Parameters.ToString());

        carregaListaProjetos(chave);
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

        if (Request.QueryString["MOD"] != null) //Ok
            moduloSistema = Request.QueryString["MOD"].ToString();
        if (Request.QueryString["IOB"] != null) //Ok
            iniciaisObjeto = Request.QueryString["IOB"].ToString();

        this.Title = cDados.getNomeSistema();

        DataSet ds = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "editaMensagemEvento");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            editaMensagemEvento = ds.Tables[0].Rows[0]["editaMensagemEvento"].ToString().Equals("S");

        btnEnviarPauta.JSProperties["cp_EditaMensagem"] = editaMensagemEvento ? "S" : "N";
        //mmEncabecadoPauta.Text = string.Format("Você foi convidado para uma reunião agendada no {0}, conforme informações apresentadas a seguir:", cDados.getNomeSistema());
        heEncabecadoAta.Html = string.Format("Você foi convidado para uma reunião agendada no {0}, conforme informações apresentadas a seguir:", cDados.getNomeSistema());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        tipoReuniao = Request.QueryString["TipoReuniao"].ToString();
        
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

            lblTituloTela.Text = "Reuniões Técnicas da Unidade";
        }

        cDados.aplicaEstiloVisual(Page);

        carregaComboResponsaveis();
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
            gvDados.Settings.VerticalScrollableHeight = altura - 115;
            lbDisponiveis.Height = ((altura - 133) / 2) + 55;
            lbSelecionados.Height = ((altura - 125));
            lbGrupos.Height = (((altura - 133) / 2) - 55 - 5);
            memoPauta.Height = new Unit((altura - 240) + "px");
            //memoAta.Height = new Unit((altura - 170) + "px");
            //alturaTabelaPlanoAcao       = altura - 250;
            pnCallback.Height = altura - 95;
        }
    }
    private void verificaTipoReuniao()
    {
        if (tipoReuniao == "E")
        {
            tabControl.TabPages[2].Text = "Objetivos Estratégicos";
            gvProjetos.Columns[2].Caption = "Objetivos Estratégicos";
            lblTituloTela.Text = "Reuniões de Análise Estratégica";
            lblUnidade.Text = "Mapa Estratégico:";
            pcDados.HeaderText = "Reunião do Mapa Estratégico";
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
        this.TH(this.TS("barraNavegacao", "reunioes_ASPxListbox", "reuniaoTecnicaPlanejamento"));
    }

    #endregion

    #region GRIDVIEW

    private void carregaGrid()
    {
        string permissaoAta = "";

        if (tipoReuniao == "E") 
            permissaoAta = "EN_EdtAtaEs";
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
        DataTable dtResponsaveis = cDados.getPossiveisResponsaveisReuniao(moduloSistema, iniciaisObjeto, codigoObjeto.ToString(), codigoEntidadeUsuarioResponsavel.ToString()).Tables[0];

        if (cDados.DataTableOk(dtResponsaveis))
        {
            ddlResponsavelEvento.DataSource = dtResponsaveis;
            ddlResponsavelEvento.TextField = "NomeUsuario";
            ddlResponsavelEvento.ValueField = "CodigoUsuario";
            ddlResponsavelEvento.Columns[0].FieldName = "NomeUsuario";
            ddlResponsavelEvento.Columns[1].FieldName = "EMail";
            ddlResponsavelEvento.TextFormatString = "{0}";
            ddlResponsavelEvento.DataBind();
        }

        if (!IsPostBack && ddlResponsavelEvento.Items.Count > 0 && idEvento == -1)
            ddlResponsavelEvento.SelectedIndex = 0;
    }

    private void carregaComboTiposEventos()
    {
        string where = string.Format("AND CodigoModuloSistema = '{0}'", moduloSistema);
        DataTable dtTiposEventos = cDados.getTiposEventos(codigoEntidadeUsuarioResponsavel, where).Tables[0];

        ddlTipoEvento.DataSource = dtTiposEventos;
        ddlTipoEvento.TextField = "DescricaoTipoEvento";
        ddlTipoEvento.ValueField = "CodigoTipoEvento";
        ddlTipoEvento.DataBind();

        if (!IsPostBack && ddlTipoEvento.Items.Count > 0 && idEvento == -1)
            ddlTipoEvento.SelectedIndex = 0;
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
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "-1";
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

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
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
            pnCallback.JSProperties["cp_OperacaoOk"] = "Erro";
        }

        hfGeral.Set("TipoOperacao", "Consultar");
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        DataSet dsTipoAssociacao = cDados.getTipoAssociacaoEventos(iniciaisObjeto, "");

        string tipoAssociacao = "0";

        if(cDados.DataSetOk(dsTipoAssociacao) && cDados.DataTableOk(dsTipoAssociacao.Tables[0]))
             tipoAssociacao = dsTipoAssociacao.Tables[0].Rows[0]["CodigoTipoAssociacao"].ToString();

        //-- compilação dos dados para Inserir.
        string descricaoResumida = txtAssunto.Text.Replace("'", "''");
        string memoPau = memoPauta.Html.Replace("'", "''");
        string memoLoc = memoLocal.Text.Replace("'", "''");

        string dataInicioPrevisto = ddlInicioPrevisto.Text + " " + txtHoraInicio.Text;
        string dataTerminoPrevisto = ddlTerminoPrevisto.Text + " " + txtHoraTermino.Text;

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
                    return "Nome do Projeto já Existe!";
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

        for(int i = 0; i < arrayProjetos.Length; i++)
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

        //FORMATAÇÃO DE DATAS

        if (ddlInicioPrevisto.Text.Equals(ddlTerminoPrevisto.Text))
        {
            string inicioPrevisto = ddlInicioPrevisto.Text;
            string[] dataInicio = inicioPrevisto.Split('/');
            DateTime dateTimeInicio = new DateTime(int.Parse(dataInicio[2]), int.Parse(dataInicio[1]), int.Parse(dataInicio[0]));

            string quando = string.Format("{0:D}", dateTimeInicio);
            quando += "  " + txtHoraInicio.Text + "-" + txtHoraTermino.Text;

            indicadonDatas = string.Format(@"<tr style='font-family:Verdana; font-size:9pt'>
                                                <td><b>Quando :</b></td>
                                                <td>{0}</td>
                                           </tr>", quando);
        }
        else
        {
            string inicioPrevisto = ddlInicioPrevisto.Text + " - " + txtHoraInicio.Text; ;
            string terminoPrevisto = ddlTerminoPrevisto.Text + " - " + txtHoraTermino.Text; ;

            indicadonDatas = string.Format(@"<tr style='font-family:Verdana; font-size:9pt'>
                                                <td><b>Início Previsto :</b></td>
                                                <td>{0}</td>
                                           </tr>
                                           <tr style='font-family:Verdana; font-size:9pt'>
                                                <td><b>Término Previsto :</b></td>
                                                <td>{1}</td>
                                           </tr>", inicioPrevisto, terminoPrevisto);
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

            if(contador < dtResponsaveis.Rows.Count)
                usuariosReuniao += Environment.NewLine;
        }

        string projetos = "";

        for (i = 0; i < gvProjetos.GetSelectedFieldValues("Descricao").Count; i++)
            projetos += "<br style='font-family:Verdana; font-size:9pt'> - " + gvProjetos.GetSelectedFieldValues("Descricao")[i].ToString();

        if (projetos == "")
            projetos = "<br style='font-family:Verdana; font-size:9pt'> - Nenhum Projeto.";
        
        string nomeDescricao = "Projetos", descricaoUnidade = "Unidade";

        if (tipoReuniao == "E")
        {
            nomeDescricao = "Objetivos Estratégicos";
            descricaoUnidade = "Mapa Estratégico";
            if (projetos == "")
                projetos = "<br style='font-family:Verdana; font-size:9pt'> - Nenhum Objetivo Estratégico.";
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
X-WR-CALNAME:Evento
X-WR-CALDESC:
BEGIN:VEVENT
DTSTART:{0:yyyyMMdd}T{1:D4}00
DTEND:{3:yyyyMMdd}T{4:D4}00
DTSTAMP:{0:yyyyMMdd}T{1:D4}00
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
END:VCALENDAR", ddlInicioPrevisto.Date
 , (int.Parse(txtHoraInicio.Text.Replace(":", "")))
 , emailUsuarioLogado
 , ddlTerminoPrevisto.Date
 , (int.Parse(txtHoraTermino.Text.Replace(":", "")))
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
            encabecadoPauta = string.Format("Você foi convidado para uma reunião agendada no {0}, conforme informações apresentadas a seguir:", cDados.getNomeSistema());
        
        assunto = string.Format("Reunião Planejada no {0} - {1}", nomeSistema.Replace("'", "''"), txtUnidade.Text);
        mensagem = string.Format(@"<span style='font-family:Verdana; font-size:9pt'>
                                    <fieldset>
                                       <b>Prezado(a),</b>
                                       <p>{10}
                                       <p>
                                       <p>
                                       <table id='idDadosReuniao' border='0' cellpadding='0' cellspacing='0' style='font-family:Verdana; font-size:9pt'>
                                           <tr>
                                                <td style='width:150px'><b>{9} :</b></td>
                                                <td>{6}.</td>
                                           </tr>
                                           <tr>
                                                <td><b>Assunto :</b></td>
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
                                                <td><b>Local :</b></td>
                                                <td>{3}</td>
                                           </tr>
                                           <tr>
                                                <td><b>Responsável :</b></td>
                                                <td><i>{4}</i></td>
                                           </tr>
                                       </table>
                                       <p>
                                       <hr>
                                       <p><b>{8}:</b> 
                                       {7}
                                       <hr>
                                       <p><b>Pauta:</b> 
                                       <p>{5}.
                                       <hr>
                                       <p>Att.,
                                       <p>{12}.
                                       <p><b>PS: Por favor, não responda esse e-mail.</b>
                                       <p>
                                    </fieldset>                                   
                                  </span>
                                  ", txtAssunto.Text
                                   , ""
                                   , "", memoLocal.Text
                                   , ddlResponsavelEvento.SelectedItem.Text
                                   , memoPauta.Html.Replace("'", "''")
                                   , txtUnidade.Text, projetos, nomeDescricao, descricaoUnidade
                                   , encabecadoPauta.Replace("'", "''")
                                   , indicadonDatas
                                   , cDados.getNomeSistema()); //Request.QueryString["NUN"].ToString());

        if (destinatarios != "")
        {
            int retornoStatus = 0;

            destinatarios = destinatarios.Remove(destinatarios.Length - 2);
            string statusEmail = cDados.enviarEmail(assunto, destinatarios, "", mensagem, "", corpoAnexo, ref retornoStatus);

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
}
