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

public partial class Reunioes_reunioes1 : System.Web.UI.Page
{
    dados cDados;
    public string alturaTabela = "";
    public int alturaTabelaPlanoAcao = 380;
    public string nomeProjeto = "";
    public string moduloSistema = "";
    public string iniciaisObjeto = "";
    public string nomeSistema = "";
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int idProjeto;
    private int mesSelecionado = DateTime.Now.Month;
    public bool podeAdministrar = true;
    public bool visibleTD = false;
    private bool editaMensagemEvento = true;
    private string anoGlobalFiltro = "";
    private string mesGlobalFiltro = "";


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
                cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_CnsReu");
            }

            podeAdministrar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_AdmReu");

            cDados.verificaPermissaoProjetoInativo(idProjeto, ref podeAdministrar, ref podeAdministrar, ref podeAdministrar);
        }

        defineAlturaTela();


        //Verifica si pode editar o Mensagem.
        DataSet ds = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "editaMensagemEvento");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            editaMensagemEvento = ds.Tables[0].Rows[0]["editaMensagemEvento"].ToString().Equals("S");

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Tela não cacheable.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);


        
        if (!IsPostBack)
        {
            populaFiltroReunioes(idProjeto, moduloSistema);
            cDados.aplicaEstiloVisual(Page);


        }

        carregaGrid(anoGlobalFiltro, mesGlobalFiltro);

        if (!IsPostBack && !IsCallback)
        {
            hfGeral.Set("TipoOperacao", "");
            hfGeral.Set("CodigosSelecionados", "");
            // tradução dos componentes da página
        }

    }

    private void populaFiltroReunioes(int codigoProjeto, string moduloSistema)
    {
        string comandoSQL = string.Format(@"                    
                    SELECT distinct --ev.CodigoEvento,
                           YEAR(CONVERT(datetime,InicioPrevisto,103)) as anoReuniao,
			               CASE WHEN MONTH(CONVERT(datetime,InicioPrevisto,103)) = 1 THEN 'Janeiro - ' + convert(varchar,YEAR(CONVERT(datetime,InicioPrevisto,103)),4)
                          WHEN MONTH(CONVERT(datetime,InicioPrevisto,103)) = 2 THEN 'Fevereiro - ' + convert(varchar,YEAR(CONVERT(datetime,InicioPrevisto,103)),4)                                  
                          WHEN MONTH(CONVERT(datetime,InicioPrevisto,103)) = 3 THEN 'Março - ' + convert(varchar,YEAR(CONVERT(datetime,InicioPrevisto,103)),4)
							  WHEN MONTH(CONVERT(datetime,InicioPrevisto,103)) = 4 THEN 'Abril - ' + convert(varchar,YEAR(CONVERT(datetime,InicioPrevisto,103)),4)
								WHEN MONTH(CONVERT(datetime,InicioPrevisto,103)) = 5 THEN 'Maio - ' + convert(varchar,YEAR(CONVERT(datetime,InicioPrevisto,103)),4)
								WHEN MONTH(CONVERT(datetime,InicioPrevisto,103)) = 6 THEN 'Junho - ' + convert(varchar,YEAR(CONVERT(datetime,InicioPrevisto,103)),4)
								WHEN MONTH(CONVERT(datetime,InicioPrevisto,103)) = 7 THEN 'Julho - ' + convert(varchar,YEAR(CONVERT(datetime,InicioPrevisto,103)),4)
								WHEN MONTH(CONVERT(datetime,InicioPrevisto,103)) = 8 THEN 'Agosto - ' + convert(varchar,YEAR(CONVERT(datetime,InicioPrevisto,103)),4)
								WHEN MONTH(CONVERT(datetime,InicioPrevisto,103)) = 9 THEN 'Setembro - ' + convert(varchar,YEAR(CONVERT(datetime,InicioPrevisto,103)),4)
								WHEN MONTH(CONVERT(datetime,InicioPrevisto,103)) = 10 THEN 'Outubro - ' + convert(varchar,YEAR(CONVERT(datetime,InicioPrevisto,103)),4)
								WHEN MONTH(CONVERT(datetime,InicioPrevisto,103)) = 11 THEN 'Novembro - ' + convert(varchar,YEAR(CONVERT(datetime,InicioPrevisto,103)),4)
								WHEN MONTH(CONVERT(datetime,InicioPrevisto,103)) = 12 THEN 'Dezembro - ' + convert(varchar,YEAR(CONVERT(datetime,InicioPrevisto,103)),4) END as mesReuniao,
                     MONTH(CONVERT(datetime,InicioPrevisto,103)) as numeroMesReuniao
                          FROM {0}.{1}.Evento AS ev INNER JOIN 
                               {0}.{1}.TipoEvento AS tev ON ev.CodigoTipoEvento = tev.CodigoTipoEvento INNER JOIN
                               {0}.{1}.TipoAssociacao AS ta ON ta.CodigoTipoAssociacao = ev.CodigoTipoAssociacao
                         WHERE ev.CodigoEntidade = {2}                       
                           AND tev.CodigoModuloSistema = '{3}'
	                       AND ev.CodigoObjetoAssociado = {4}
                      ORDER BY 1,2", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, moduloSistema, codigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            ddlFiltroInicioPrevisto.DataSource = ds.Tables[0];
            ddlFiltroInicioPrevisto.TextField = "mesReuniao";
            ddlFiltroInicioPrevisto.ValueField = "anoReuniao";
            ddlFiltroInicioPrevisto.DataBind();
        }
        ddlFiltroInicioPrevisto.Items.Add(new ListEditItem(Resources.traducao.todos, ""));
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

    #region VARIOS

    private void defineAlturaTela() //Ok
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));

        int altura = (alturaPrincipal - 170);

        if (altura > 0)
        {
            alturaTabelaPlanoAcao = (int)(altura * 0.75);
            gvDados.Settings.VerticalScrollableHeight = altura - 145;
           
        }
    }



    #endregion
     
    #region GRIDVIEW

    private void carregaGrid(string ano, string mes)
    {
        string where = string.Format(@"
                AND tev.CodigoModuloSistema = '{0}'
	            AND ev.CodigoObjetoAssociado = {1}", moduloSistema, idProjeto.ToString());
        if (ano != "" && mes != "")
        {
            where += string.Format(@" AND YEAR(CONVERT(datetime,InicioPrevisto,103)) = {0} ", ano);
            where += string.Format(@" AND MONTH(CONVERT(datetime,InicioPrevisto,103)) = {0} ", mes);
        }
        DataTable dt = cDados.getEventosEntidade(codigoEntidadeUsuarioResponsavel.ToString(), where, codigoUsuarioResponsavel.ToString(), "").Tables[0];
        gvDados.DataSource = dt; // tlEventos.DataSource = dt;
        gvDados.DataBind();      // tlEventos.DataBind();
    }

    #endregion

 
    #region PAUTAS & ATAS

    //    protected void btnEnviarPauta_Click(object sender, EventArgs e)
    //    {
    //        bool enviarPauta = (tabControl.ActiveTabIndex == 0 || tabControl.ActiveTabIndex == 1);
    //        bool enviarAta = (tabControl.ActiveTabIndex == 2 || tabControl.ActiveTabIndex == 3);

    //        if (enviarPauta)
    //            enviaPauta();
    //        if (enviarAta)
    //            enviaAta();
    //    }

    //    private void salvaEvento(bool enviarPauta, bool enviarAta)
    //    {
    //    //    int regAfetados = 0;
    //    //    int novoCodigo = 0;
    //    //    bool retorno = false;

    //    //    int[] convidados = new int[lbSelecionados.Items.Count];
    //    //    int i = 0;

    //    //    foreach (ListEditItem lei in lbSelecionados.Items)
    //    //    {
    //    //        convidados[i] = int.Parse(lei.Value.ToString());
    //    //        i++;
    //    //    }

    //    //    if (idEvento == -1)
    //    //    {
    //    //        //retorno = cDados.incluiEvento(idEntidade, txtAssunto.Text, memoPauta.Text, memoLocal.Text, ddlInicioPrevisto.Text, ddlTerminoPrevisto.Text,
    //    //        //    int.Parse(ddlResponsavelEvento.Value.ToString()), int.Parse(ddlTipoEvento.Value.ToString()), convidados, idPlanoAcao, ref regAfetados, ref novoCodigo);

    //    //        if (retorno)
    //    //        {
    //    //            if (enviarPauta)
    //    //            {
    //    //                enviaPauta();
    //    //            }

    //    //            Response.Redirect("reuniao_planejamento.aspx?CodigoEvento=" + novoCodigo + "&" + Request.QueryString.ToString());
    //    //        }


    //    //    }
    //    //    else
    //    //    {
    //    //        retorno = cDados.atualizaEvento(idEvento, txtAssunto.Text, memoPauta.Text, memoLocal.Text, ddlInicioPrevisto.Text, ddlTerminoPrevisto.Text,
    //    //            int.Parse(ddlResponsavelEvento.Value.ToString()), int.Parse(ddlTipoEvento.Value.ToString()), convidados, ref regAfetados);

    //    //        if (retorno)
    //    //        {
    //    //            salvaExecucao();

    //    //            if (enviarPauta)
    //    //            {
    //    //                enviaPauta();
    //    //            }
    //    //            if (enviarAta)
    //    //            {
    //    //                enviaAta();
    //    //            }

    //    //            Response.Redirect("reuniao_planejamento.aspx?" + Request.QueryString.ToString());
    //    //        }
    //    //    }

    //    }

    //    private void salvaExecucao()
    //    {
    //        int regAfetados = 0;

    //        DataTable dtTarefas = (DataTable)Session["gridTarefas"];

    //        bool retorno = cDados.incluiExecucaoEvento(idEvento, memoAta.Html, ddlInicioReal.Text, ddlTerminoReal.Text, dtTarefas, int.Parse(Session["idUsuarioLogado"].ToString()), ref regAfetados);

    //    }

    //    private void enviaPauta()
    //    {
    //        string assunto = "", mensagem = "", destinatarios = "";
    //        string whereDestinatarios = "";

    //        foreach (ListEditItem lei in lbSelecionados.Items)
    //            whereDestinatarios += lei.Value + ",";

    //        whereDestinatarios = " AND u.CodigoUsuario IN(" + whereDestinatarios + ddlResponsavelEvento.Value + ")";
    //        DataTable dtResponsaveis = cDados.getUsuarios(whereDestinatarios).Tables[0];//cDados.getUsuariosCadastrados(idEntidade, whereDestinatarios).Tables[0];

    //        foreach (DataRow dr in dtResponsaveis.Rows)
    //            destinatarios += dr["EMail"].ToString() + "; ";

    //        assunto = "Reunião Planejada no Sistema de Gestão da Estratégia";
    //        mensagem = string.Format(@"<span style='font-family:Verdana; font-size:9pt'>Prezado(a),                                        
    //                                       <br style='font-family:Verdana; font-size:9pt'><p>Você foi convidado para uma reunião agendada no Sistema de Gestão da Estratégia, conforme informações apresentadas a seguir:
    //                                       <br style='font-family:Verdana; font-size:9pt'><p>Entidade: {6}.
    //                                       <br style='font-family:Verdana; font-size:9pt'><p>Assunto: {0}.
    //                                       <br style='font-family:Verdana; font-size:9pt'><p>Início Previsto: {1}
    //                                       <br style='font-family:Verdana; font-size:9pt'><p>Término Previsto: {2}
    //                                       <br style='font-family:Verdana; font-size:9pt'><p>Local: {3}
    //                                       <br style='font-family:Verdana; font-size:9pt'><p>Responsável: {4}
    //                                       <br style='font-family:Verdana; font-size:9pt'><p>-------------------------------------------------------------------
    //                                       <br style='font-family:Verdana; font-size:9pt'><p><b>Pauta:</b> 
    //                                       <br style='font-family:Verdana; font-size:9pt'><p>{5}.
    //                                       <br style='font-family:Verdana; font-size:9pt'><p>-------------------------------------------------------------------
    //                                       <br style='font-family:Verdana; font-size:9pt'><p>Att.,
    //                                       <br style='font-family:Verdana; font-size:9pt'><p>Sistema de Gestão da Estratégia.
    //                                       <br style='font-family:Verdana; font-size:9pt'><p><b>PS: Por favor, não responda esse e-mail.</b></span>
    //                                  ",txtAssunto.Text.Replace("'", "''"), 
    //                                   ddlInicioPrevisto.Text, ddlTerminoPrevisto.Text, memoLocal.Text,
    //                                   ddlResponsavelEvento.SelectedItem.Text, memoPauta.Html, nomeProjeto);

    //        if (destinatarios != "")
    //        {
    //            destinatarios = destinatarios.Remove(destinatarios.Length - 2);
    //            string statusEmail = cDados.enviarEmail(assunto, destinatarios, "", mensagem);
    //        }
    //    }

    //    private void enviaAta()
    //    {
    ////        DataTable dtTarefas = (DataTable)Session["gridTarefas"];

    ////        string assunto = "", mensagem = "", destinatarios = "", planoAcao = "";

    ////        string whereDestinatarios = "";

    ////        foreach (ListEditItem lei in lbSelecionados.Items)
    ////        {
    ////            whereDestinatarios += lei.Value + ",";
    ////        }

    ////        whereDestinatarios = " AND u.CodigoUsuario IN(" + whereDestinatarios + ddlResponsavelEvento.Value + ")";

    ////        DataTable dtResponsaveis = cDados.getUsuariosCadastrados(idEntidade, whereDestinatarios).Tables[0];

    ////        foreach (DataRow dr in dtResponsaveis.Rows)
    ////        {
    ////            destinatarios += dr["EMail"].ToString() + "; ";
    ////        }

    ////        for (int i = 0; i < dtTarefas.Rows.Count; i++)
    ////        {
    ////            planoAcao += string.Format("<UL><LI> {0} - {1} - Início Previsto: {2} - Término Previsto: {3}</LI></UL>",
    ////                dtTarefas.Rows[i]["DescricaoAcao"].ToString(), dtTarefas.Rows[i]["Responsavel"].ToString()
    ////                , dtTarefas.Rows[i]["InicioPrevisto"].ToString(), dtTarefas.Rows[i]["TerminoPrevisto"].ToString());
    ////        }

    ////        assunto = "Reunião Executada no Sistema de Gestão da Estratégia";

    ////        mensagem = string.Format(@"<span style='font-family:Verdana; font-size:9pt'>Prezado(a),                                        
    ////                                       <br style='font-family:Verdana; font-size:9pt'><p>Foi registrado um evento no Sistema de Gestão da Estratégia, sendo seu resumo apresentado a seguir:                                       
    ////                                       <br style='font-family:Verdana; font-size:9pt'><p>Entidade: {6}.
    ////                                       <br style='font-family:Verdana; font-size:9pt'><p>Assunto: {0}.
    ////                                       <br style='font-family:Verdana; font-size:9pt'><p>Início: {1}
    ////                                       <br style='font-family:Verdana; font-size:9pt'><p>Término: {2}
    ////                                       <br style='font-family:Verdana; font-size:9pt'><p>Local: {3}
    ////                                       <br style='font-family:Verdana; font-size:9pt'><p>---------------------------------------------------------------
    ////                                       <br style='font-family:Verdana; font-size:9pt'><p><b>Ata:</b>
    ////                                       <br style='font-family:Verdana; font-size:9pt'><p>{4}.
    ////                                       <br style='font-family:Verdana; font-size:9pt'><p>---------------------------------------------------------------
    ////                                       <br style='font-family:Verdana; font-size:9pt'><p><b>Tarefas com responsabilidades e prazos finais:</b>
    ////                                       <br style='font-family:Verdana; font-size:9pt'><p>{5}
    ////                                       <br style='font-family:Verdana; font-size:9pt'><p>---------------------------------------------------------------
    ////                                       <br style='font-family:Verdana; font-size:9pt'><p>Att.,
    ////                                       <br style='font-family:Verdana; font-size:9pt'><p>Sistema de Gestão da Estratégia
    ////                                       <br style='font-family:Verdana; font-size:9pt'><p><b>PS: Por favor, não responda esse e-mail.</b></span>",
    ////                                                      txtAssunto.Text, ddlInicioReal.Text, ddlTerminoReal.Text, memoLocal.Text, memoAta.Text, planoAcao, Request.QueryString["NUN"].ToString());

    ////        if (destinatarios != "")
    ////        {
    ////            destinatarios = destinatarios.Remove(destinatarios.Length - 2);

    ////            string statusEmail = cDados.enviarEmail(assunto, destinatarios, "", "", mensagem);
    ////        }
    //    }

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

    #endregion

    private string getPlanoAcao()
    {
        int codigoReuniao = int.Parse(getChavePrimaria());

        string planoAcao = "", iniciais = "RE";

        DataSet ds = cDados.getTarefasEvento(codigoReuniao, iniciais, "");

        foreach (DataRow dr in ds.Tables[0].Rows)
            planoAcao += string.Format(@"
                         <br style='font-family:Verdana; font-size:9pt'><p><b>Tarefa:</b> {0}
                         <br style='font-family:Verdana; font-size:9pt'><b> Início Previsto:</b>  {1}
                         <br style='font-family:Verdana; font-size:9pt'><b> Término Previsto:</b> {2}
                         <br style='font-family:Verdana; font-size:9pt'><b> Responsável:</b>      {3}"
                         , dr["DescricaoTarefa"]
                         , dr["InicioPrevisto"]
                         , dr["TerminoPrevisto"]
                         , dr["NomeUsuario"]);


        if (planoAcao == "")
            planoAcao = "<br style='font-family:Verdana; font-size:9pt'> - Nenhum Plano de Ação.";

        return planoAcao;
    }

    protected void gvDados_CustomButtonInitialize1(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnEditarCustom")
        {
            if (podeAdministrar)
                e.Enabled = true;
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        else if (e.ButtonID == "btnExcluirCustom")
        {
            if (podeAdministrar)
                e.Enabled = true;
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }

    }

    protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (gvDados.GetRowValues(e.VisibleIndex, "IndicaAtrasada") + "" == "S")
            e.Row.ForeColor = Color.Red;
    }


    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string[] parametroRecebido = e.Parameters.Split(';');
        if (parametroRecebido.Length >= 2)
        {
            anoGlobalFiltro = parametroRecebido[0];
            mesGlobalFiltro = parametroRecebido[1];
            if (mesGlobalFiltro == "Janeiro")
            {
                mesGlobalFiltro = "1";
            }
            else if (mesGlobalFiltro == "Fevereiro")
            {
                mesGlobalFiltro = "2";
            }
            else if (mesGlobalFiltro == "Março")
            {
                mesGlobalFiltro = "3";
            }
            else if (mesGlobalFiltro == "Abril")
            {
                mesGlobalFiltro = "4";
            }
            else if (mesGlobalFiltro == "Maio")
            {
                mesGlobalFiltro = "5";
            }
            else if (mesGlobalFiltro == "Junho")
            {
                mesGlobalFiltro = "6";
            }
            else if (mesGlobalFiltro == "Julho")
            {
                mesGlobalFiltro = "7";
            }
            else if (mesGlobalFiltro == "Agosto")
            {
                mesGlobalFiltro = "8";
            }
            else if (mesGlobalFiltro == "Setembro")
            {
                mesGlobalFiltro = "9";
            }
            else if (mesGlobalFiltro == "Outubro")
            {
                mesGlobalFiltro = "10";
            }
            else if (mesGlobalFiltro == "Novembro")
            {
                mesGlobalFiltro = "11";
            }
            else if (mesGlobalFiltro == "Dezembro")
            {
                mesGlobalFiltro = "12";
            }
        }
        carregaGrid(anoGlobalFiltro, mesGlobalFiltro);

    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

    }
    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "InicioPrevisto")
        {
            if (e.CellValue.ToString().Contains("1900") == true)
            {
                e.Cell.Text = "";
            }
        }
        if (e.DataColumn.FieldName == "TerminoPrevisto")
        {
            if (e.CellValue.ToString().Contains("1900") == true)
            {
                e.Cell.Text = "";
            }
        }
    }
    protected void callbackExcluir_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        // busca a chave primaria
        string msgRetorno = "";

        if (cDados.excluiEvento(e.Parameter,ref msgRetorno))
            carregaGrid(anoGlobalFiltro, mesGlobalFiltro);
        else
            msgRetorno = "Error ao excluir.";

        callbackExcluir.JSProperties["cp_StatusExclusao"] = msgRetorno;
    }
    protected void ddlFiltroInicioPrevisto_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        populaFiltroReunioes(idProjeto, moduloSistema);
        ListEditItem li = ddlFiltroInicioPrevisto.Items.FindByText("Todos");
        ddlFiltroInicioPrevisto.JSProperties["cp_IndiceSelecionado"] = li.Index;
    }
}
