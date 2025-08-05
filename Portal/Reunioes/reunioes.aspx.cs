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
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.IO;

public partial class Reunioes_reunioes : System.Web.UI.Page
{
    dados cDados;
    private ASPxGridView gvToDoList;
    public string alturaTabela = "";
    //public int alturaTabelaPlanoAcao = 220;
    public string nomeProjeto = "";
    public string moduloSistema = "";
    public string iniciaisObjeto = "";
    public string nomeSistema = "";
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int idEvento = -1;
    public string codigoEvento = "";
    private int idProjeto;
    private int mesSelecionado = DateTime.Now.Month;
    public bool podeAdministrar = true;
    public bool PermissaoEditarAtaProjeto = false;
    public bool visibleTD = false;
    private bool editaMensagemEvento = true;

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
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();
        SqlDataSource1.ConnectionString = cDados.classeDados.getStringConexao();
        nomeSistema = cDados.getNomeSistema();
        if (Request.QueryString["MOD"] != null) //Ok
        {
            moduloSistema = Request.QueryString["MOD"].ToString();
            hfGeral.Set("moduloSistema", moduloSistema);
        }
        if (Request.QueryString["IOB"] != null) //Ok
            iniciaisObjeto = Request.QueryString["IOB"].ToString();
        if (Request.QueryString["NMP"] != null) //Ok
            nomeProjeto = Request.QueryString["NMP"].ToString();
        if (Request.QueryString["idProjeto"] != null)
        {
            idProjeto = int.Parse(Request.QueryString["idProjeto"].ToString());
            hfGeral.Set("CodigoProjetoAtual", idProjeto);
            if (!IsPostBack)
            {
                cDados.VerificaAcessoTelaSemMaster(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_CnsReu");
            }
            DataSet dsNomeProjeto = cDados.getProjetos(string.Format(@" and P.CodigoProjeto = {0}", idProjeto));
            if (cDados.DataSetOk(dsNomeProjeto) && cDados.DataTableOk(dsNomeProjeto.Tables[0]))
            {
                nomeProjeto = dsNomeProjeto.Tables[0].Rows[0]["NomeProjeto"].ToString();
            }

            //nomeProjeto = 
            podeAdministrar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_AdmReu");
            PermissaoEditarAtaProjeto = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_EdtReuAta");
            cDados.verificaPermissaoProjetoInativo(idProjeto, ref podeAdministrar, ref podeAdministrar, ref podeAdministrar);
        }

        //Verifica si pode editar o Mensagem.
        DataSet ds = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "editaMensagemEvento");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            editaMensagemEvento = ds.Tables[0].Rows[0]["editaMensagemEvento"].ToString().Equals("S");
        btnEnviarPauta.JSProperties["cp_EditaMensagem"] = editaMensagemEvento ? "S" : "N";
        btnEnviarPauta.JSProperties["cp_MensagemPauta"] = string.Format(Resources.traducao.reunioes_voc__foi_convidado_para_uma_reuni_o_agendada_no__0___conforme_informa__es_apresentadas_a_seguir_, cDados.getNomeSistema());
        btnEnviarPauta.JSProperties["cp_MensagemAta"] = string.Format(Resources.traducao.reunioes_foi_registrado_uma_reuni_o_no__0___sendo_seu_resumo_apresentado_a_seguir_, cDados.getNomeSistema());
        //carregaPlanoAcao();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        //Tela não cacheable.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        carregaComboResponsaveis();
        carregaComboTiposEventos();
        cDados.aplicaEstiloVisual(Page, true);
        cDados.aplicaEstiloVisual(memoPauta, "Default", true);
        DefineFormatoDatasDosComponentes();
        //gvPendencias.SettingsPager.AlwaysShowPager = false;
        gvPendencias.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        if (!IsPostBack)
        {
            carregaListaParticipantes("-1");
        }
        carregaGrid();
        carregaGrupos("");
        carregaAbaPendencias();
        defineAlturaTela();
        if (!IsCallback)
        {
            hfGeral.Set("TipoOperacao", "");
            hfGeral.Set("CodigosSelecionados", "");
        }

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/reunioes.js""></script>"));
        this.TH(this.TS("reunioes"));

    }

    private void DefineFormatoDatasDosComponentes()
    {
        string formatoData = Resources.traducao.geral_formato_data_csharp_PT_EN;
        ddlInicioReal.EditFormatString = formatoData;
        ddlInicioReal.DisplayFormatString = formatoData;

        ddlTerminoReal.EditFormatString = formatoData;
        ddlTerminoReal.DisplayFormatString = formatoData;

        ddlInicioPrevisto.EditFormatString = formatoData;
        ddlInicioPrevisto.DisplayFormatString = formatoData;

        ddlTerminoPrevisto.EditFormatString = formatoData;
        ddlTerminoPrevisto.DisplayFormatString = formatoData;
    }

    private void carregaPlanoAcao()
    {
        // inclui as funcionalidades do Eventos.
        int codigoTipoAssociacao = cDados.getCodigoTipoAssociacao("RE");
        int codigoObjetoAssociado = -1;
        if (hfGeral.Contains("codigoObjetoAssociado"))
            codigoObjetoAssociado = int.Parse(hfGeral.Get("codigoObjetoAssociado").ToString());
        bool somenteLeitura = true;
        if (hfGeral.Contains("TipoOperacao"))
            somenteLeitura = (hfGeral.Get("TipoOperacao").ToString() != "Incluir" && hfGeral.Get("TipoOperacao").ToString() != "Editar");

        int[] convidados = getParticipantesEvento();

        Unit tamanho = new Unit("100%");

        //Componente criado por Antonio - Certificar o funcionamiento.
        PlanoDeAcao myPlanoDeAcao = null;
        myPlanoDeAcao = new PlanoDeAcao(cDados.classeDados, codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, idProjeto, codigoTipoAssociacao, codigoObjetoAssociado, tamanho, 50, somenteLeitura, convidados, true, txtAssunto.Text);
        tabControl.TabPages.FindByName("TabD").FindControl("Content4Div").Controls.AddAt(0, myPlanoDeAcao.constroiInterfaceFormulario());
        gvToDoList = myPlanoDeAcao.gvToDoList;
        //gvToDoList.Font.Name = "Verdana";
        //gvToDoList.Font.Size = new FontUnit("8pt");
        gvToDoList.ClientSideEvents.BeginCallback = "function(s, e) { hfGeralToDoList.Set('NomeToDoList',  txtAssunto.GetText());}";

        if (!IsCallback)
            gvToDoList.DataBind();
    }

    private void carregaAbaPendencias()
    {
        string where = "";

        /*
          Metodologia do desenvolvedor:
          Com a combo selecionada em "Todos" 
          certificar-se de que sejam trazidas tarefas,  
          pelo menos uma  de várias com os seguintes status:  
          "Em execução", "Início atrasado", "Atrasada", "Futura".
         */

        int codigoReuniao = int.Parse(getChavePrimaria());

        where = " AND CodigoStatusTarefa NOT IN (3, 2) ";

        if (ddlStatusPendencia.Value as string == "Em_Execução")
            where += " AND f.TerminoReal IS NULL AND f.InicioReal IS NOT NULL ";

        else if (ddlStatusPendencia.Value as string == "Início atrasado")
            where += " AND ( (f.TerminoReal IS NULL AND f.TerminoPrevisto > GetDate()) AND (f.InicioPrevisto < GetDate() AND  f.InicioReal IS NULL ) ) ";

        else if (ddlStatusPendencia.Value as string == "Atrasada")
            where += " AND ( f.TerminoReal IS NULL AND f.TerminoPrevisto < GetDate() ) ";

        else if (ddlStatusPendencia.Value as string == "Futura")
            where += " AND f.InicioPrevisto > GetDate() AND f.TerminoReal IS NULL AND f.InicioReal IS NULL ";

        DataSet dsPendencias = cDados.getPendenciasEvento(codigoReuniao, where);

        if (cDados.DataSetOk(dsPendencias))
        {
            gvPendencias.DataSource = dsPendencias;
            gvPendencias.DataBind();

        }
    }

    private int[] getParticipantesEvento()
    {
        DataSet dsConvidados = cDados.getUsuarioDaEntidadeAtiva(codigoEntidadeUsuarioResponsavel.ToString(), "");

        int[] convidados = new int[dsConvidados.Tables[0].Rows.Count];

        if (cDados.DataSetOk(dsConvidados))
        {
            int i = 0;
            foreach (DataRow dr in dsConvidados.Tables[0].Rows)
            {
                convidados[i] = int.Parse(dr["CodigoUsuario"].ToString());
                i++;
            }
        }
        return convidados;
    }

    //Pega todos o Usuário Responsável, os Usuários Selecionados e os Usuários que estão na Aba de Plano de Ação para Envio de Email.
    private string[] getParticipantesEventoParaEnvioEmail()
    {
        DataSet dsConvidados = cDados.getUsuarioDaEntidadeAtivaParaEnvioDeEmails(codigoEntidadeUsuarioResponsavel.ToString(), hfGeral.Get("CodigoEventoAtual").ToString(), idProjeto);
        string[] convidados = new string[dsConvidados.Tables[0].Rows.Count];

        if (cDados.DataSetOk(dsConvidados))
        {
            int i = 0;
            foreach (DataRow dr in dsConvidados.Tables[0].Rows)
            {
                convidados[i] = dr["CodigoUsuario"].ToString();
                i++;
            }
        }
        return convidados;
    }
    #region VARIOS

    private void defineAlturaTela() //Ok
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));

        //alturaPrincipal = 

        int altura = (alturaPrincipal - 170);

        if (altura > 0)
        {
            int adicional = 0;
            if (alturaPrincipal <= 800)
            {
                adicional = 0;
            }
            else
            {
                adicional = 30;
            }

            lbDisponiveis.Height = ((altura - adicional - 310) / 2) + 55;
            lbSelecionados.Height = ((altura - adicional - 220));
            lbGrupos.Height = (((altura - adicional - 140) / 2) - 60);
            //memoPauta.Height = new Unit((altura - adicional - 320) + "px");
            memoAta.Height = new Unit((altura - adicional - 280) + "px");
            //alturaTabelaPlanoAcao       = altura - 250;
            pnCallback.Height = alturaPrincipal - 170;
            //gvToDoList.Settings.VerticalScrollableHeight = altura - adicional - 310;
            gvPendencias.Settings.VerticalScrollableHeight = altura - adicional - 300;

        }
    }

    #endregion

    #region GRIDVIEW

    private void carregaGrid()
    {

        string where = string.Format(@"
                AND tev.CodigoModuloSistema = '{0}'
	            AND ev.CodigoObjetoAssociado = {1}", moduloSistema, idProjeto.ToString());

        DataTable dt = cDados.getEventosEntidade(codigoEntidadeUsuarioResponsavel.ToString(), where, codigoUsuarioResponsavel.ToString(), "").Tables[0];
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
        DataTable dtParticipantes = cDados.getParticipantesEventos(moduloSistema, iniciaisObjeto, idProjeto.ToString(), codigoEntidadeUsuarioResponsavel.ToString(), where).Tables[0];

        lbDisponiveis.DataSource = dtParticipantes;
        lbDisponiveis.TextField = "NomeUsuario";
        lbDisponiveis.ValueField = "CodigoUsuario";
        lbDisponiveis.DataBind();

        if (lbDisponiveis.Items.Count > 0)
            lbDisponiveis.SelectedIndex = -1;
    }

    protected void lbDisponiveis_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        codigoEvento = e.Parameter.ToString();
        hfGeral.Set("CodigoEventoAtual", codigoEvento);
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
        /*
         SELECT u.CodigoUsuario, u.NomeUsuario,u.EMail,'Unidade'
                    FROM {0}.{1}.Usuario u INNER JOIN
		                 {0}.{1}.ParticipanteEvento pe ON pe.CodigoParticipante = u.CodigoUsuario
                    WHERE CodigoEvento = {2}
                     {3}
                    ORDER BY NomeUsuario
         */



        string codigoEvento = e.Parameter.ToString();
        string where = "";
        //        if (lbGrupos.SelectedIndex != -1 && lbGrupos.SelectedItem != null)
        //        {
        //            where += string.Format(@" AND u.CodigoUsuario 
        //                                      IN (SELECT CodigoUsuarioParticipante 
        //                                            FROM UsuarioGrupoParticipantesEvento 
        //                                           WHERE CodigoGrupoParticipantes = {0})", lbGrupos.SelectedItem.Value);
        //        }
        carregaListaParticipantesAssociados(codigoEvento, where);
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

    // retorna a primary key da tabela.
    private string getChavePrimariaGrid()
    {
        if (gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString() != "")
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
            btnEnviarPauta.Text = "Enviar Pauta";
            btnEnviarPauta.ClientVisible = true;
        }
        if (e.Parameter == "EnviarAta")
        {
            mensagemErro_Persistencia = persisteEdicaoAta();
            btnEnviarPauta.Text = "Enviar Ata";
            btnEnviarPauta.ClientVisible = true;
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
            mensagemErro_Persistencia = persisteExclusaoRegistroGrid();
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
            pnCallback.JSProperties["cp_ErroSalvar"] = mensagemErro_Persistencia;
        }

        //memoPauta.ActiveView = DevExpress.Web.ASPxHtmlEditor.HtmlEditorView.Design;
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        //-- compilação dos dados para Inserir.
        string descricaoResumida = txtAssunto.Text.Replace("'", "''");
        string memoPau = memoPauta.Html.Replace("'", "''");
        string memoLoc = memoLocal.Text.Replace("'", "''");
        string memoRes = memoAta.Html.Replace("'", "''");

        string dataInicioReal = "";
        string dataTerminoReal = "";

        //Caso seje em banco Portugês salva no formato Universal com Data e hora
        //Caso seja em banco Inglês salva no formato Universal Transational
        string dataInicioPrevisto = ddlInicioPrevisto.Text + " " + txtHoraInicio.Text;
        string dataTerminoPrevisto = ddlTerminoPrevisto.Text + " " + txtHoraTermino.Text;

        string responsavel = ddlResponsavelEvento.Value.ToString();
        string tipoEvento = ddlTipoEvento.Value.ToString();
        string tipoAssociacao = (iniciaisObjeto == "PR" ? "4" : "4"); // <--- Consultar com ERICSSON !!!

        if (ddlInicioReal.Text != "")
            dataInicioReal = ddlInicioReal.Text + " " + txtHoraInicioAta.Text;
        else
            dataInicioReal = "NULL";
        if (ddlTerminoReal.Text != "")
            dataTerminoReal = ddlTerminoReal.Text + " " + txtHoraTerminoAta.Text;
        else
            dataTerminoReal = "NULL";

        string[] arrayParticipantesSelecionados = hfGeral.Get("CodigosSelecionados").ToString().Split(';');

        string idEventoNovo = "";
        string mesgError = "";

        try
        {
            bool result = cDados.incluiEvento(descricaoResumida, responsavel, dataInicioPrevisto, dataTerminoPrevisto,
                                              dataInicioReal, dataTerminoReal, tipoAssociacao, idProjeto.ToString(),
                                              memoLoc, memoPau, memoRes, codigoEntidadeUsuarioResponsavel.ToString(),
                                              "NULL", "NULL", tipoEvento, codigoUsuarioResponsavel.ToString(),
                                              ref idEventoNovo, ref mesgError);

            if (result == false)
            {
                if (mesgError.Contains("UQ_Projeto_NomeProjeto"))
                    return Resources.traducao.reunioes_nome_do_projeto_j__existe_;
                else
                    return mesgError;
            }
            else
            {
                if (arrayParticipantesSelecionados.Length > 0)
                    cDados.incluiParticipantesSelecionados(arrayParticipantesSelecionados, idEventoNovo);
                hfGeral.Set("codigoEvento", idEventoNovo);
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
    {   // busca a chave primaria
        string chave = getChavePrimaria();

        //-- compilação dos dados para Inserir.
        string descricaoResumida = txtAssunto.Text.Replace("'", "''");
        string memoPau = memoPauta.Html.Replace("'", "''");
        string memoLoc = memoLocal.Text.Replace("'", "''");
        string memoRes = memoAta.Html.Replace("'", "''");

        //Caso seje em banco Portugês salva no formato Universal com Data e hora
        //Caso seja em banco Inglês salva no formato Universal Transational
        string dataInicioPrevisto = ddlInicioPrevisto.Date.Year + "-" + ddlInicioPrevisto.Date.Month + "-" + ddlInicioPrevisto.Date.Day + "T" + txtHoraInicio.Text;
        string dataTerminoPrevisto = ddlTerminoPrevisto.Date.Year + "-" + ddlTerminoPrevisto.Date.Month + "-" + ddlTerminoPrevisto.Date.Day + "T" + txtHoraTermino.Text;

        string responsavel = ddlResponsavelEvento.Value.ToString();
        string tipoEvento = ddlTipoEvento.Value.ToString();
        string tipoAssociacao = (iniciaisObjeto == "PR" ? "4" : "4"); // <--- Consultar com ERICSSON !!!

        string dataInicioReal = "";
        string dataTerminoReal = "";

        //Aba de Início real 
        if (ddlInicioReal.Text != "")
            dataInicioReal = ddlInicioReal.Date.Year + "-" + ddlInicioReal.Date.Month + "-" + ddlInicioReal.Date.Day + "T" + txtHoraInicioAta.Text;
        else
            dataInicioReal = "NULL";
        if (ddlTerminoReal.Text != "")
            dataTerminoReal = ddlTerminoReal.Date.Year + "-" + ddlTerminoReal.Date.Month + "-" + ddlTerminoReal.Date.Day + "T" + txtHoraTerminoAta.Text;
        else
            dataTerminoReal = "NULL";


        //inclui Particpante Responsável
        string[] arrayParticipantesSelecionados = hfGeral.Get("CodigosSelecionados").ToString().Split(';');

        arrayParticipantesSelecionados = hfGeral.Get("CodigosSelecionados").ToString().Split(';');

        //Inclui Participantes Selecionados, Inclui Participantes da Aba

        string mesgError = "";

        cDados.atualizaEvento(descricaoResumida, responsavel, dataInicioPrevisto, dataTerminoPrevisto, dataInicioReal,
                              dataTerminoReal, tipoAssociacao, idProjeto.ToString(), memoLoc, memoPau, memoRes,
                              codigoEntidadeUsuarioResponsavel.ToString(), "NULL", "NULL", tipoEvento,
                              codigoUsuarioResponsavel.ToString(), chave, true, ref mesgError);
        if (arrayParticipantesSelecionados.Length > 0)
            cDados.incluiParticipantesSelecionados(arrayParticipantesSelecionados, chave);
        hfGeral.Set("codigoEvento", chave);
        carregaGrid();
        gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(chave);
        gvDados.ClientVisible = false;
        return mesgError;
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();
        string msgRetorno = "";

        if (cDados.excluiEvento(chave, ref msgRetorno))
            carregaGrid();
        else
            msgRetorno = Resources.traducao.reunioes_error_ao_excluir_o_evento_;

        return msgRetorno;
    }

    private string persisteExclusaoRegistroGrid() // Método responsável pela Exclusão do registro
    {   // busca a chave primaria
        string chave = getChavePrimariaGrid();
        string msgRetorno = "";

        if (cDados.excluiEvento(chave, ref msgRetorno))
            carregaGrid();
        else
            msgRetorno = Resources.traducao.reunioes_error_ao_excluir_o_evento_;

        return msgRetorno;
    }

    private string persisteEdicaoAta()
    {
        string msgError1 = "";
        string msgError2 = "";
        string msgError3 = "";
        string chave = getChavePrimaria();
        string assunto = "";
        string mensagem = "";
        string destinatarios = "";
        string planoAcao = "";
        string whereDestinatarios = "";
        string indicadonDatas = "";

        //se as data são de início e final são iguais ou 
        //se a Data ddlInicioReal for vazio ou nulo não entra aqui.
        if ((!ddlInicioReal.Text.Equals(ddlTerminoReal.Text)) || (!String.IsNullOrEmpty(ddlTerminoReal.Text)) || (!String.IsNullOrEmpty(ddlTerminoReal.Text)) && (!String.IsNullOrEmpty(ddlTerminoReal.Text)))
        {
            if (System.Globalization.CultureInfo.CurrentCulture.Name == "en-US")
            {
                string inicioReal = ddlInicioReal.Text;
                string[] dataInicio = inicioReal.Split('/');
                DateTime dateTimeInicio = new DateTime(int.Parse(dataInicio[2]), int.Parse(dataInicio[0]), int.Parse(dataInicio[1]));
                string quando = string.Format("{0:D}", dateTimeInicio);
                quando += "  " + txtHoraInicioAta.Text + "-" + txtHoraTerminoAta.Text;
                indicadonDatas = string.Format(@"<tr>
                                                <td><b>" + Resources.traducao.reunioes_quando + " :</b></td>" +
                                                "<td>{0}</td>" +
                                                "</tr>", quando);

            }
            else
            {
                string inicioReal = ddlInicioReal.Text;
                string[] dataInicio = inicioReal.Split('/');
                DateTime dateTimeInicio = new DateTime(int.Parse(dataInicio[2]), int.Parse(dataInicio[1]), int.Parse(dataInicio[0]));
                string quando = string.Format("{0:D}", dateTimeInicio);
                quando += "  " + txtHoraInicioAta.Text + "-" + txtHoraTerminoAta.Text;
                indicadonDatas = string.Format(@"<tr>
                                                <td><b>" + Resources.traducao.reunioes_quando + " :</b></td>" +
                                                "<td>{0}</td>" +
                                                "</tr>", quando);
            }
        }
        else
        {
            string inicioReal = ddlInicioReal.Text + " - " + txtHoraInicioAta.Text;
            string terminoReal = ddlTerminoReal.Text + " - " + txtHoraTerminoAta.Text;

            indicadonDatas = string.Format(@"<tr>
                                                <td><b>" + Resources.traducao.reunioes_in_cio + "  :</b></td>" +
                                                "<td>{0}</td>" +
                                           "</tr>" +
                                           "<tr>" +
                                                "<td><b>" + Resources.traducao.reunioes_t_rmino + " :</b></td>" +
                                                "<td>{1}</td>" +
                                           "</tr>", inicioReal, terminoReal);
        }


        string[] arrayParticipantesSelecionados = getParticipantesEventoParaEnvioEmail();

        int i = 0;
        foreach (string codigoUsuario in arrayParticipantesSelecionados)
        {
            if ("" != codigoUsuario)
            {
                arrayParticipantesSelecionados[i++] = codigoUsuario;
                whereDestinatarios += codigoUsuario + ",";
            }
        }

        whereDestinatarios = " AND u.CodigoUsuario IN(" + whereDestinatarios + ddlResponsavelEvento.Value + ")";
        DataTable dtResponsaveis = cDados.getUsuarios(whereDestinatarios).Tables[0];

        foreach (DataRow dr in dtResponsaveis.Rows)
        {
            destinatarios += dr["EMail"].ToString() + "; ";
        }

        planoAcao = getPlanoAcao();
        assunto = string.Format(Resources.traducao.reunioes_reuni_o_executada_no__0_____1_, nomeSistema.Replace("'", "''"), nomeProjeto.Replace("'", "''"));

        string encabecadoAta = "";
        if (editaMensagemEvento)
            encabecadoAta = heEncabecadoAta.Html; // mmEncabecadoPauta.Text;
        else
            encabecadoAta = string.Format(Resources.traducao.reunioes_foi_registrada_uma_reuni_o_no__0___sendo_seu_resumo_apresentado_a_seguir_, cDados.getNomeSistema());

        mensagem = string.Format(@"<span>" +
                                    "<fieldset>" +
                                    "<p> <b>" + Resources.traducao.reunioes_prezado_a_ + "</b>," +
                                    "<p> {9}" +
                                    "<p>" +
                                    "<p>" +
                                       "<table id='idDadosReuniao' border='0' cellpadding='0' cellspacing='0'>" +
                                           "<tr>" +
                                                "<td style='width:150px'><b>" + Resources.traducao.reunioes_projeto + " :</b></td>" +
                                                "<td>{6}.</td>" +
                                           "</tr>" +
                                           "<tr>" +
                                                "<td><b>" + Resources.traducao.reunioes_assunto + " :</b></td>" +
                                                "<td><i>{0}.</i></td>" +
                                           "</tr>" +
                                           "<tr>" +
                                                "<td style='height: 10px'></td>" +
                                                "<td></td>" +
                                           "</tr>" +
                                           "{8}" +
                                           "<tr>" +
                                                "<td style='height: 10px'></td>" +
                                                "<td></td>" +
                                           "</tr>" +
                                           "<tr>" +
                                                "<td><b>" + Resources.traducao.reunioes_local + "   :</b></td>" +
                                                "<td>{3}</td>" +
                                           "</tr>" +
                                       "</table>" +
                                    "<p>" +
                                    "<hr>" +
                                    "<p><b>" + Resources.traducao.reunioes_ata + ":</b>" +
                                    "<p>{4}." +
                                    "<hr>" +
                                    "<p><b>" + Resources.traducao.reunioes_tarefas_com_responsabilidades_e_prazos_finais + ":</b>" +
                                    "<p>{5}" +
                                    "<hr>" +
                                    "<p>" + Resources.traducao.reunioes_att_ + "," +
                                    "<p>{7}" +
                                    "<p><b>" + Resources.traducao.reunioes_ps__por_favor__n_o_responda_esse_e___mail + ".</b>" +
                                    "<p>" +
                                    "</fieldset>" +
                                    "</span>"
                                  , txtAssunto.Text.Replace("'", "''")
                                   , "", ""
                                   , memoLocal.Text.Replace("'", "''")
                                   , memoAta.Html.Replace("'", "''")
                                   , planoAcao
                                   , nomeProjeto.Replace("'", "''")
                                   , nomeSistema.Replace("'", "''")
                                   , indicadonDatas
                                   , encabecadoAta.Replace("'", "''"));

        //inclusão de Alerta Calendário para Inserir em Anexo
        int contador = 0;
        string usuariosReuniao = "";
        foreach (DataRow dr in dtResponsaveis.Rows)
        {
            usuariosReuniao += dr["CodigoUsuario"].ToString() == ddlResponsavelEvento.Value.ToString() ? string.Format(@"ATTENDEE;ROLE=CHAIR;PARTSTAT=ACCEPTED ;CN=""{0}"";RSVP=FALSE :mailto:{1}", dr["NomeUsuario"].ToString()
            , dr["EMail"].ToString()) : string.Format(@"ATTENDEE;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE :mailto:{0}", dr["EMail"].ToString());
            contador++;
            if (contador < dtResponsaveis.Rows.Count)
                usuariosReuniao += Environment.NewLine;
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
X-WR-TIMEZONE:America/Sao_Paulo
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
STATUS:CONFIRMED
SUMMARY;LANGUAGE=pt-br:{6}
X-ALT-DESC;FMTTYPE=text/html:{10}
TRANSP:OPAQUE
END:VEVENT
END:VCALENDAR", String.IsNullOrEmpty(ddlInicioReal.ToString()) ? ddlInicioPrevisto.Date : ddlInicioReal.Date
  , (String.IsNullOrEmpty(txtHoraInicioAta.Text) ? int.Parse(txtHoraInicio.Text.Replace(":", "")) : int.Parse(txtHoraInicioAta.Text.Replace(":", "")))
  , emailUsuarioLogado
  , String.IsNullOrEmpty(ddlTerminoReal.ToString()) ? ddlTerminoPrevisto.Date : ddlTerminoReal.Date
  , (String.IsNullOrEmpty(txtHoraTerminoAta.Text) ? int.Parse(txtHoraTermino.Text.Replace(":", "")) : int.Parse(txtHoraTerminoAta.Text.Replace(":", "")))
  , ddlTipoEvento.Text
  , txtAssunto.Text
  , memoLocal.Text
  , usuariosReuniao
  , nomeUsuarioLogado
  , memoPauta.Html);

        string LocalCalendariCSW = Path.Combine(Server.MapPath("~/ArquivosTemporarios"), ("CalendarioEventoReuniao" + DateTime.Now.ToString("ddMMyyyyhhmmssfffffff") + ".ics"));
        File.WriteAllText(LocalCalendariCSW, corpoAnexo);
        //Fim inclusão de Alerta Calendário para Inserir em Anexo
        if (destinatarios != "")
        {
            int retornoStatus = 0;
            destinatarios = destinatarios.Remove(destinatarios.Length - 2);
            //string statusEmail = cDados.enviarEmail(assunto, destinatarios, "", mensagem, "", "", ref retornoStatus);
            string statusEmail = cDados.enviarEmailCalendar(assunto, destinatarios, "", mensagem, "", LocalCalendariCSW, LocalCalendariCSW, ref retornoStatus);

            msgError3 = persisteEdicaoRegistro();

            if (retornoStatus == 1)
            {
                cDados.atualizaEnvioAta(chave, codigoUsuarioResponsavel.ToString(), ref msgError1);
                cDados.atualizaParticipantesEnvioAtaEmail(arrayParticipantesSelecionados, chave, ref msgError2);
            }
            else
            {
                msgError3 += " " + statusEmail;
            }
        }
        return msgError1 + msgError2 + msgError3;
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
            string quando = string.Format("{0:D}", ddlInicioPrevisto.Date);
            quando += string.Format("  Início: {0} - Previsão de término: {1}", txtHoraInicio.Text, txtHoraTermino.Text);

            indicadonDatas = string.Format(@"<tr>
                                                <td><b>Quando :</b></td>
                                                <td>{0}</td>
                                           </tr>", quando);
        }
        else
        {
            string inicioPrevisto = ddlInicioPrevisto.Text + " - " + txtHoraInicio.Text; ;
            string terminoPrevisto = ddlTerminoPrevisto.Text + " - " + txtHoraTermino.Text; ;

            indicadonDatas = string.Format(@"<tr>
                                                <td><b>Início Previsto  :</b></td>
                                                <td>{0}</td>
                                           </tr>
                                           <tr>
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

        whereDestinatarios = " AND u.CodigoUsuario IN(" + whereDestinatarios + ddlResponsavelEvento.Value + ")";
        DataTable dtResponsaveis = cDados.getUsuarios(whereDestinatarios).Tables[0];//cDados.getUsuariosCadastrados(idEntidade, whereDestinatarios).Tables[0];

        int contador = 0;

        string usuariosReuniao = "";

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
X-WR-TIMEZONE:America/Sao_Paulo
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
STATUS:CONFIRMED
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

        assunto = string.Format("Reunião Planejada no {0} - {1}", nomeSistema.Replace("'", "''"), nomeProjeto.Replace("'", "''"));
        mensagem = string.Format(@"<span>
                                    <fieldset>
                                    <p><b>Prezado(a)</b>,
                                    <p>{7}
                                    <p>
                                    <p>
                                       <table id='idDadosReuniao' border='0' cellpadding='0' cellspacing='0'>
                                           <tr>
                                                <td style='width:150px'><b>Projeto     :</b></td>
                                                <td>{6}.</td>
                                           </tr>
                                           <tr>
                                                <td><b>Assunto     :</b></td>
                                                <td><i>{0}.</i></td>
                                           </tr>
                                           <tr>
                                                <td style='height: 10px'></td>
                                                <td></td>
                                           </tr>
                                           {8}
                                           <tr>
                                                <td style='height: 10px'></td>
                                                <td></td>
                                           </tr>
                                           <tr>
                                                <td><b>Local       :</b></td>
                                                <td>{3}</td>
                                           </tr>
                                           <tr>
                                                <td><b>Responsável :</b></td>
                                                <td><i>{4}</i></td>
                                           </tr>
                                       </table>
                                    <p>
                                    <hr>
                                    <p><b>Pauta:</b> 
                                    <p>{5}.
                                    <hr>
                                    <p>Att.,
                                    <p>{9}.
                                    <p><b>PS: Por favor, não responda esse e-mail.</b>
                                    <p>
                                    </fieldset>
                                    </span>
                                  ", txtAssunto.Text
                                   , "", ""
                                   , memoLocal.Text.Replace("'", "''")
                                   , ddlResponsavelEvento.Text
                                   , memoPauta.Html.Replace("'", "''")
                                   , nomeProjeto.Replace("'", "''")
                                   , encabecadoPauta.Replace("'", "''") //nomeSistema.Replace("'", "''")
                                   , indicadonDatas
                                   , cDados.getNomeSistema()); //Request.QueryString["NUN"].ToString());

        if (destinatarios != "")
        {
            int retornoStatus = 0;
            destinatarios = destinatarios.Remove(destinatarios.Length - 2);
            //string statusEmail = cDados.enviarEmail(assunto, destinatarios, "", mensagem, "", corpoAnexo, ref retornoStatus);
            string statusEmail = cDados.enviarEmailCalendarAppWeb(assunto, destinatarios, "", mensagem, corpoAnexo, ref retornoStatus);

            msgError3 = persisteEdicaoRegistro();

            if (retornoStatus == 1)
            {
                cDados.atualizaEnvioPauta(chave, codigoUsuarioResponsavel.ToString(), ref msgError1);
                cDados.atualizaParticipantesEnvioPautaEmail(arrayParticipantesSelecionados, chave, ref msgError2);
            }
            else
            {
                msgError3 += " " + statusEmail;
            }
        }
        return msgError1 + msgError2 + msgError3;
    }

    #endregion

    private string getPlanoAcao()
    {
        int codigoReuniao = int.Parse(getChavePrimaria());

        string planoAcao = "", iniciais = "RE";

        string[] InicioPrevisto;
        string[] TerminoPrevisto;

        DataSet ds = cDados.getTarefasEvento(codigoReuniao, iniciais, "");


        //Se for em Português imprime a data no email de uma forma caso contrário de outra.
        if (System.Globalization.CultureInfo.CurrentCulture.Name == "en-US")
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                InicioPrevisto = dr["InicioPrevisto"].ToString().Split('/');
                TerminoPrevisto = dr["TerminoPrevisto"].ToString().Split('/');

                planoAcao += string.Format(@"<br><p><b>" + Resources.traducao.reunioes_tarefa + ":</b> {0}" +
                                             "<br><b> " + Resources.traducao.reunioes_in_cio_previsto + ":</b>  {1}" +
                                             "<br><b> " + Resources.traducao.reunioes_t_rmino_previsto + ":</b> {2}" +
                                             "<br><b> " + Resources.traducao.reunioes_respons_vel + ":</b>      {3}"
                                             , dr["DescricaoTarefa"]
                                             , InicioPrevisto[1] + "/" + InicioPrevisto[0] + "/" + InicioPrevisto[2]
                                             , TerminoPrevisto[1] + "/" + TerminoPrevisto[0] + "/" + TerminoPrevisto[2]
                                             , dr["NomeUsuario"]);
            }
        }
        else
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
                planoAcao += string.Format(@"<br><p><b>" + Resources.traducao.reunioes_tarefa + ":</b> {0}" +
                                             "<br><b> " + Resources.traducao.reunioes_in_cio_previsto + ":</b>  {1}" +
                                             "<br><b> " + Resources.traducao.reunioes_t_rmino_previsto + ":</b> {2}" +
                                             "<br><b> " + Resources.traducao.reunioes_respons_vel + ":</b>      {3}"
                                             , dr["DescricaoTarefa"]
                                             , dr["InicioPrevisto"]
                                             , dr["TerminoPrevisto"]
                                             , dr["NomeUsuario"]);
        }




        if (planoAcao == "")
            planoAcao = Resources.traducao.reunioes__br____nenhum_plano_de_a__o_;
        return planoAcao;
    }





    protected void gvDados_CustomButtonInitialize1(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        string terminoReal = gvDados.GetRowValues(e.VisibleIndex, "TerminoRealData") + "";
        bool PermissaoEditarAtaResponsavel = (gvDados.GetRowValues(e.VisibleIndex, "PermissaoEditarAtaResponsavel") + "") == "True";

        if (e.ButtonID == "btnEditarCustom")
        {
            if (podeAdministrar)
            {
                e.Enabled = true;
                if (terminoReal != "")
                {
                    if (!(PermissaoEditarAtaProjeto || PermissaoEditarAtaResponsavel))
                    {
                        e.Enabled = false;
                        e.Text = "Excluir";
                        e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                    }

                }
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }

        else if (e.ButtonID == "btnExcluirCustom")
        {

            if (podeAdministrar)
            {
                e.Enabled = true;
                if (terminoReal != "")
                {
                    e.Enabled = false;
                    e.Text = "Excluir";
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
            }
            else
            {
                e.Enabled = false;
                e.Text = "Excluir";
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }

    }

    protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (gvDados.GetRowValues(e.VisibleIndex, "IndicaAtrasada") + "" == "S")
            e.Row.ForeColor = Color.FromName("#fc6e51"); ;
    }

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

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string listaCodigos = "";
        DataSet ds = cDados.getParticipantesGrupoSelecionados(codigoEntidadeUsuarioResponsavel.ToString(), e.Parameter, moduloSistema, iniciaisObjeto, idProjeto.ToString(), "");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listaCodigos += ds.Tables[0].Rows[i]["NomeUsuario"].ToString().TrimEnd() + "¥" + ds.Tables[0].Rows[i]["CodigoUsuario"].ToString() + ";";
            }
        }
        callback.JSProperties["cp_ListaCodigos"] = listaCodigos;
    }

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string mensagemErro_Persistencia = "";
        callbackSalvar.JSProperties["cp_OperacaoOk"] = "";

        if (e.Parameter == "EnviarPauta")
        {
            mensagemErro_Persistencia = persisteEdicaoPauta();
        }
        if (e.Parameter == "EnviarAta")
        {
            mensagemErro_Persistencia = persisteEdicaoAta();
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
            callbackSalvar.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else
        {
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
            callbackSalvar.JSProperties["cp_OperacaoOk"] = "Erro";
        }
    }

    protected void gvDados_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
    {
        string fieldName = e.Column.FieldName;
        if (fieldName == "InicioPrevisto" || fieldName == "InicioReal")
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
            , moduloSistema, iniciaisObjeto, idProjeto.ToString(), codigoEntidadeUsuarioResponsavel.ToString());

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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "ReunPrj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeAdministrar, "novaReuniao(" + idProjeto + ");", true, true, false, "ReunPrj", "Reuniões", this);
    }

    protected void menu2_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "ReunPen", "Reuniões", this);
    }
    protected void menu2_ItemClick(object source, MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter_pendencia, "ReunPen");
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
    protected void gvPendencias_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {

    }
    protected void gvPendencias_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
    {

    }
    protected void gvPendencias_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //carregaAbaPendencias();
    }
    protected void pnGeral_Callback(object sender, CallbackEventArgsBase e)
    {
        string codTarefa = gvPendencias.GetRowValues(int.Parse(e.Parameter), "CodigoTarefa").ToString();
        string Anotacoes = gvPendencias.GetRowValues(int.Parse(e.Parameter), "Anotacoes").ToString();

        mmComentario.Text = Anotacoes;
    }
    protected void gvPendencias_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Estagio")
        {
            int indice = e.Cell.TabIndex;
            string dado = (e.CellValue != null) ? e.CellValue.ToString() : "";
            e.Cell.Text = "<img " + dado + " >";
        }
    }
    protected void ASPxGridViewExporter_pendencia_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
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
        if (e.Column.Name == "Estagio" && e.Value != null)
        {
            Font fontex = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
            e.BrickStyle.Font = fontex;
            e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            //if (e.Value.ToString().Equals("vermelhook"))
            //{
            //    e.Text = "ü";
            //    e.TextValue = "ü";
            //    e.BrickStyle.ForeColor = Color.Red;
            //}
            if (e.Value.ToString().ToLower().Contains("verdeok"))
            {
                e.Text = "ü";
                e.TextValue = "ü";
                e.BrickStyle.ForeColor = Color.Green;
            }
            else if (e.Value.ToString().ToLower().Contains("verde"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Green;
            }
            else if (e.Value.ToString().ToLower().Contains("laranja"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Orange;
            }
            else if (e.Value.ToString().ToLower().Contains("vermelho") && !e.Value.ToString().ToLower().Contains("vermelhook"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Red;
            }
            else if (e.Value.ToString().ToLower().Contains("vermelhook"))
            {
                e.Text = "ü";
                e.TextValue = "ü";
                e.BrickStyle.ForeColor = Color.Red;
            }
            else if (e.Value.ToString().ToLower().Contains("amarelo"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Yellow;
            }
            else if (e.Value.ToString().ToLower().Contains("branco"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.WhiteSmoke;
            }
            else if (e.Value.ToString().ToLower().Contains("futura"))
            {
                Font fontey = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
                e.BrickStyle.Font = fontey;
                e.Text = "¹";
                e.TextValue = "¹";
                e.BrickStyle.ForeColor = Color.DarkBlue;
            }
            else if (e.Value.ToString().ToLower().Contains("cancelada"))
            {
                Font fontey = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
                e.BrickStyle.Font = fontey;
                e.Text = "û";
                e.TextValue = "û";
                e.BrickStyle.ForeColor = Color.Black;
            }
        }
    }
    protected void gvPendencias_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaAbaPendencias();
    }
    protected void gvDados_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        if (e.ErrorTextKind == GridErrorTextKind.General && e.Exception != null)
            e.ErrorText = e.Exception.Message;
    }


    //inicio classe de Envio de Email com Calendário Agendamento de Reunião





    //public static string MontariCals(string assunto, string DTSTART, string DTEND, string DTSTAMP, string UID, string Local)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    //Calendar
    //    sb.AppendLine("BEGIN:VCALENDAR");
    //    sb.AppendLine("PRODID:-//Microsoft Corporation//Outlook 16.0 MIMEDIR//EN");
    //    sb.AppendLine("VERSION:2.0");
    //    sb.AppendLine("METHOD:REQUEST");
    //    sb.AppendLine("X -MS-OLK-FORCEINSPECTOROPEN:TRUE");
    //    //Event
    //    sb.AppendLine("BEGIN:VEVENT");
    //    sb.AppendLine("CLASS: PUBLIC");
    //    sb.AppendLine("DESCRIPTION: " + assunto);
    //    sb.AppendLine("DTSTART:" + DTSTART);
    //    sb.AppendLine("DTEND:" + DTEND);
    //    sb.AppendLine("DTSTAMP:" + DTSTAMP);
    //    sb.AppendLine("SEQUENCE:0");
    //    sb.AppendLine("UID:" + UID);
    //    sb.AppendLine("PRIORITY:1");
    //    //Organizer
    //    foreach (var item in iCal.Organizer)
    //    {
    //        sb.AppendLine("ORGANIZER;CN=" + item.Name + ":mailto:" + item.Address);
    //    }
    //    //Attendee
    //    foreach (var item in iCal.ToAddresses)
    //    {
    //        sb.AppendLine("ATTENDEE;CUTYPE=INDIVIDUAL;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE;CN=" + item.Name + ";X-NUM-GUESTS=0:" + item.Address);
    //    }
    //    sb.AppendLine("CREATED:" + DateTime.Now);
    //    sb.AppendLine("X-ALT-DESC;FMTTYPE=text/html:" + assunto);
    //    sb.AppendLine("LAST-MODIFIED:" + toUniversalTime(iCal.EventLastModifiedTimeStamp));
    //    sb.AppendLine("LOCATION:" + Local != "" ? "CDIS" : Local); //Se passar o Local coloca caso contrário vai o local CDIS
    //    sb.AppendLine("STATUS:CONFIRMED");
    //    sb.AppendLine("SUMMARY:" + assunto);
    //    sb.AppendLine("TRANSP:OPAQUE");
    //    sb.AppendLine("CATEGORIES: BRISK");
    //    //sb.AppendLine("COLOR: black");
    //    //Alarm
    //    sb.AppendLine("BEGIN:VALARM");
    //    sb.AppendLine("TRIGGER:" + String.Format("-PT{0}M", iCal.AlarmTrigger));
    //    sb.AppendLine("REPEAT:" + iCal.AlarmRepeat);
    //    sb.AppendLine("DURATION:" + String.Format("PT{0}M", iCal.AlarmDuration));
    //    sb.AppendLine("ACTION:DISPLAY");
    //    sb.AppendLine("DESCRIPTION:" + iCal.AlarmDescription);
    //    sb.AppendLine("END:VALARM");
    //    sb.AppendLine("END:VEVENT");
    //    sb.AppendLine("END:VCALENDAR");





    //    return sb.ToString(); ;
    //}


    //fim Agendamento Reunião


}
