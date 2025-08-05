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
using CDIS;
using DevExpress.Web;
using System.Drawing;
using System.Globalization;
using System.Collections.Generic;

public partial class Reunioes_reuniaoTecnicaRealizacao : System.Web.UI.Page
{
    dados cDados;
    private ASPxGridView gvToDoList;
    public string nomeSistema = "";
    public string telaMapa = "";
    private string tipoReuniao = "";
    private int codigoReuniao = 0;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int mesSelecionado = DateTime.Now.Month;
    private int alturaGridTarefas = 0;
    private int codigoUnidade = 0;
    private bool editaMensagemEvento = true;
    private bool habilitaComponentes = true;

    private string dbName;
    private string dbOwner;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();

        cDados = CdadosUtil.GetCdados(null);

        if (Request.QueryString["COR"] != null) //Ok
            codigoReuniao = int.Parse(Request.QueryString["COR"].ToString());

        if (Request.QueryString["RO"] != null)
        { //Ok
            habilitaComponentes = Request.QueryString["RO"].ToString() != "S";
            hfGeral.Set("readOnly", Request.QueryString["RO"].ToString());

        }

        hfGeral.Set("codigoEvento", codigoReuniao);

        if (!IsPostBack && !IsCallback)
        {
            hfDadosSessao.Set("CodigoEntidade", cDados.getInfoSistema("CodigoEntidade").ToString());
            hfDadosSessao.Set("Resolucao", cDados.getInfoSistema("ResolucaoCliente").ToString());
            hfDadosSessao.Set("IDUsuarioLogado", cDados.getInfoSistema("IDUsuarioLogado").ToString());
            hfDadosSessao.Set("IDEstiloVisual", cDados.getInfoSistema("IDEstiloVisual").ToString());
            hfDadosSessao.Set("NomeUsuarioLogado", cDados.getInfoSistema("NomeUsuarioLogado").ToString());
            hfDadosSessao.Set("CodigoCarteira", cDados.getInfoSistema("CodigoCarteira").ToString());

            if (tipoReuniao == "E")
                hfDadosSessao.Set("CodigoMapa", cDados.getInfoSistema("CodigoMapa").ToString());
        }

        cDados.setInfoSistema("CodigoEntidade", hfDadosSessao.Get("CodigoEntidade").ToString());
        cDados.setInfoSistema("ResolucaoCliente", hfDadosSessao.Get("Resolucao").ToString());
        cDados.setInfoSistema("IDUsuarioLogado", hfDadosSessao.Get("IDUsuarioLogado").ToString());
        cDados.setInfoSistema("IDEstiloVisual", hfDadosSessao.Get("IDEstiloVisual").ToString());
        cDados.setInfoSistema("NomeUsuarioLogado", hfDadosSessao.Get("NomeUsuarioLogado").ToString());
        cDados.setInfoSistema("CodigoCarteira", hfDadosSessao.Get("CodigoCarteira").ToString());

        if (tipoReuniao == "E")
            cDados.setInfoSistema("CodigoMapa", hfDadosSessao.Get("CodigoMapa").ToString());

        codigoUsuarioResponsavel = int.Parse(hfDadosSessao.Get("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(hfDadosSessao.Get("CodigoEntidade").ToString());
        nomeSistema = cDados.getNomeSistema();

        DataSet ds = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "editaMensagemEvento");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            editaMensagemEvento = ds.Tables[0].Rows[0]["editaMensagemEvento"].ToString().Equals("S");

        btnEnviarAta.JSProperties["cp_EditaMensagem"] = editaMensagemEvento ? "S" : "N";
        //mmEncabecadoAta.Text = string.Format("Foi registrado uma reunião no {0}, sendo seu resumo apresentado a seguir:", cDados.getNomeSistema());
        heEncabecadoAta.Html = string.Format("Foi registrado uma reunião no {0}, sendo seu resumo apresentado a seguir:", cDados.getNomeSistema());

        cDados.aplicaEstiloVisual(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        tipoReuniao = Request.QueryString["TR"].ToString();
        pcApresentacaoAcao.JSProperties["cp_Path"] = cDados.getPathSistema();
        if (!IsPostBack && !IsCallback)
        {
            // tradução dos componentes da página
            new traducao("pt-BR", pnCallback, gvPendencias, gvProjetos, gvToDoList);
        }

        defineLarguraTela();

        if (!IsPostBack)
        {
            ASPxPageControl1.ActiveTabIndex = 0;
            lblTituloTela.Text = "Execução da Reunião";
        }

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        carregaAbaReuniao();
        carregaAbaPendencias();
        verificaTipoReuniao();
    }


    private void verificaTipoReuniao()
    {
        if (tipoReuniao == "E")
        {
            //telaMapa = "../_Estrategias/mapaEstrategicoReuniao.aspx?COR=" + codigoReuniao;

            ASPxPageControl1.TabPages[1].Visible = false;
            ASPxPageControl1.TabPages[3].Visible = true;
            lblUnidade.Text = "Mapa Estratégico:";
            carregaAbaObjetivos();

        }
        else
        {
            ASPxPageControl1.TabPages[1].Visible = true;
            ASPxPageControl1.TabPages[3].Visible = false;
            carregaAbaProjetos();
        }
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        memoPauta.Height = (int)((altura - 410) / 2);

        memoAta.Height = (int)((altura - 400) / 2);

        gvPendencias.Settings.VerticalScrollableHeight = (int)(altura - 525);
        gvProjetos.Settings.VerticalScrollableHeight = (int)(altura - 260);
        gvObjetivos.Settings.VerticalScrollableHeight = (int)(altura - 370);

        alturaGridTarefas = (int)(altura - 358);

        hfGeral.Set("alturaTela", altura - 153);
    }

    private void carregaAbaReuniao()
    {
        string permissaoAta = "";

        if (tipoReuniao == "E")
            permissaoAta = "EN_EdtAtaEs";
        else if (tipoReuniao == "T")
            permissaoAta = "UN_EdtAtaTec";

        DataSet dsReuniao = cDados.getEventosEntidade(codigoEntidadeUsuarioResponsavel.ToString(), "AND CodigoEvento = " + codigoReuniao, codigoUsuarioResponsavel.ToString(), permissaoAta);

        if (cDados.DataSetOk(dsReuniao) && cDados.DataTableOk(dsReuniao.Tables[0]))
        {

            DataRow dr = dsReuniao.Tables[0].Rows[0];

            if (!IsPostBack)
            {
                txtAssunto.Text = dr["DescricaoResumida"].ToString();
                memoPauta.Html = dr["Pauta"].ToString();

                ddlInicioReal.Text = dr["InicioRealData"].ToString();
                ddlTerminoReal.Text = dr["TerminoRealData"].ToString();

                txtHoraInicioAta.Text = dr["InicioRealHora"].ToString();
                txtHoraTerminoAta.Text = dr["TerminoRealHora"].ToString();

                memoAta.Html = dr["ResumoEvento"].ToString();

                memoAta.ActiveView = DevExpress.Web.ASPxHtmlEditor.HtmlEditorView.Design;

                codigoUnidade = int.Parse(dr["CodigoObjetoAssociado"].ToString());

                if (tipoReuniao == "E")
                {
                    DataSet dsMapa = cDados.getMapasEstrategicos(null, " AND Mapa.CodigoMapaEstrategico = " + cDados.getInfoSistema("CodigoMapa").ToString());

                    if (cDados.DataSetOk(dsMapa) && cDados.DataTableOk(dsMapa.Tables[0]))
                        txtUnidade.Text = dsMapa.Tables[0].Rows[0]["TituloMapaEstrategico"].ToString();
                }
                else
                {
                    DataSet dsUnidade = cDados.getUnidade("AND CodigoUnidadeNegocio = " + codigoUnidade);

                    txtUnidade.Text = dsUnidade.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();
                }
            }

            carregaAbaPlanoAcao(codigoReuniao, dr["IniciaisTipoAssociacao"].ToString());
        }
    }

    private void carregaAbaProjetos()
    {
        string where = "";

        DataSet dsProjetos = cDados.getProjetosEvento(codigoReuniao, where);

        if (cDados.DataSetOk(dsProjetos) && cDados.DataTableOk(dsProjetos.Tables[0]))
        {
            gvProjetos.DataSource = dsProjetos;

            gvProjetos.DataBind();
        }
    }

    private void carregaAbaObjetivos()
    {
        string where = " AND Selecionado = 'S'";

        DataSet dsOE = cDados.getObjetivosPlanejamentoEvento(codigoReuniao, codigoEntidadeUsuarioResponsavel, codigoReuniao, codigoUsuarioResponsavel, where);

        if (cDados.DataSetOk(dsOE))
        {
            gvObjetivos.DataSource = dsOE;

            gvObjetivos.DataBind();
        }
    }

    private void carregaAbaPendencias()
    {
        string where = "";

        if (ddlStatusPendencia.Value as string == "Cancelada")
            where = " AND CodigoStatusTarefa = 3 ";

        else if (ddlStatusPendencia.Value as string == "Concluída_no_Prazo")
            where = " AND CodigoStatusTarefa <> 3 AND f.TerminoReal IS NOT NULL AND f.TerminoReal <= f.TerminoPrevisto ";

        else if (ddlStatusPendencia.Value as string == "Concluída_com_Atraso")
            where = " AND CodigoStatusTarefa <> 3 AND f.TerminoReal IS NOT NULL AND f.TerminoReal > f.TerminoPrevisto ";

        else if (ddlStatusPendencia.Value as string == "Em_Execução")
            where = " AND CodigoStatusTarefa <> 3 AND f.TerminoReal IS NULL AND f.InicioReal IS NOT NULL ";

        else if (ddlStatusPendencia.Value as string == "Início atrasado")
            where = " AND CodigoStatusTarefa <> 3  AND ( (f.TerminoReal IS NULL AND f.TerminoPrevisto > GetDate()) AND (f.InicioPrevisto < GetDate() AND  f.InicioReal IS NULL ) ) ";

        else if (ddlStatusPendencia.Value as string == "Atrasada")
            where = " AND CodigoStatusTarefa <> 3  AND ( f.TerminoReal IS NULL AND f.TerminoPrevisto < GetDate() ) ";

        else if (ddlStatusPendencia.Value as string == "Futura")
            where = " AND CodigoStatusTarefa <> 3 AND f.InicioPrevisto > GetDate() AND f.TerminoReal IS NULL AND f.InicioReal IS NULL ";

        DataSet dsPendencias = cDados.getPendenciasEvento(codigoReuniao, where);

        if (cDados.DataSetOk(dsPendencias))
        {
            gvPendencias.DataSource = dsPendencias;

            gvPendencias.DataBind();

        }
    }

    private void carregaAbaPlanoAcao(long codigoObjetoAssociado, string tipoAssociacao)
    {
        Unit tamanho = new Unit("100%");
        
        string modulo = "PRJ";

        if (tipoReuniao == "E")
            modulo = "EST";

        int[] convidados = getParticipantesEvento(modulo, tipoAssociacao);

        int codigoAssociacaoReuniao = 0;

        DataSet ds = cDados.getTipoAssociacaoEventos("RE", "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            codigoAssociacaoReuniao = int.Parse(ds.Tables[0].Rows[0]["CodigoTipoAssociacao"].ToString());
        
        //Componente criado por Antonio - Certificar o funcionamiento.
        PlanoDeAcao myPlanoDeAcao = null;
        myPlanoDeAcao = new PlanoDeAcao(cDados.classeDados, codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, null, codigoAssociacaoReuniao, codigoObjetoAssociado, tamanho, alturaGridTarefas - 23, false, convidados.Length == 0 ? null : convidados, true, txtAssunto.Text);
        ASPxPageControl1.TabPages.FindByName("tabPlanoAcao").FindControl("Content4Div").Controls.AddAt(0, myPlanoDeAcao.constroiInterfaceFormulario());
        gvToDoList = myPlanoDeAcao.gvToDoList;
        gvToDoList.ClientSideEvents.BeginCallback = "function(s, e) { hfGeralToDoList.Set('NomeToDoList',  txtAssunto.GetText());}";
        
        if (!IsCallback)
            gvToDoList.DataBind();

        gvToDoList.Font.Name = "Verdana";
        gvToDoList.Font.Size = 10;

        cDados.aplicaEstiloVisual(gvToDoList);
    }

    private int[] getParticipantesEvento(string moduloSistema, string iniciaisObjeto)
    {
        string codigoObjeto = tipoReuniao == "E" ? cDados.getInfoSistema("CodigoMapa").ToString() : codigoUnidade.ToString();

        DataSet dsConvidados = cDados.getParticipantesEventos(moduloSistema, iniciaisObjeto, codigoObjeto, codigoEntidadeUsuarioResponsavel.ToString(), "");

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

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_MSG"] = "";
        pnCallback.JSProperties["cp_Erro"] = "";

        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        if (e.Parameter == "EnviarAta")
        {
            mensagemErro_Persistencia = persisteEdicaoAta();

            if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
                pnCallback.JSProperties["cp_MSG"] = "Reunião Salva e Ata Enviada com Sucesso!";
            else
                pnCallback.JSProperties["cp_Erro"] = mensagemErro_Persistencia;

        }
        else
            if (e.Parameter == "Editar")
            {
                mensagemErro_Persistencia = persisteEdicaoRegistro();
                if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
                    pnCallback.JSProperties["cp_MSG"] = "Reunião Salva com Sucesso!";
            }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
        }
        else
        {// alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
            pnCallback.JSProperties["cp_Erro"] = mensagemErro_Persistencia.Replace(Environment.NewLine, " ");
        }
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {

        //-- compilação dos dados para Inserir.
        string memoRes = memoAta.Html.Replace("'", "\"");
        string dataInicioReal = "";
        string dataTerminoReal = "";

        if (ddlInicioReal.Text != "")
            dataInicioReal = ddlInicioReal.Text + " " + txtHoraInicioAta.Text;
        else
            dataInicioReal = "NULL";
        if (ddlTerminoReal.Text != "")
            dataTerminoReal = ddlTerminoReal.Text + " " + txtHoraTerminoAta.Text;
        else
            dataTerminoReal = "NULL";

        string mesgError = "";

        cDados.atualizaExecucaoEvento(codigoReuniao, dataInicioReal, dataTerminoReal, memoRes, "NULL", codigoUsuarioResponsavel, ref mesgError);

        return mesgError;
    }

    private string persisteEdicaoAta()
    {
        string permissaoAta = "";

        if (tipoReuniao == "E")
            permissaoAta = "EN_EdtAtaEs";
        else if (tipoReuniao == "T")
            permissaoAta = "UN_EdtAtaTec";

        DataSet dsReuniao = cDados.getEventosEntidade(codigoEntidadeUsuarioResponsavel.ToString(), "AND CodigoEvento = " + codigoReuniao, codigoUsuarioResponsavel.ToString(),permissaoAta);

        string msgError1 = "";
        string msgError2 = "";
        string msgError3 = "";

        if (cDados.DataSetOk(dsReuniao) && cDados.DataTableOk(dsReuniao.Tables[0]))
        {
            Dictionary<string, string> usuariosEvento = new Dictionary<string, string>();
            string assunto = "";
            string mensagem = "";
            string destinatarios = "";
            string planoAcao = "";
            string indicadonDatas = "";
            string horaInicio = "00:00";
            string horaTermino = "00:00";

            //Convertendo as datas em horário universal
            IFormatProvider iFormatProvider = new CultureInfo(System.Globalization.CultureInfo.CurrentCulture.Name, false);

            DateTime dataEHoraInicioReal = DateTime.Parse(ddlInicioReal.Text + " " + txtHoraInicioAta.Text, iFormatProvider, DateTimeStyles.AssumeLocal | DateTimeStyles.AdjustToUniversal);
            DateTime dataEHoraTerminoReal = DateTime.Parse(ddlTerminoReal.Text + " " + txtHoraTerminoAta.Text, iFormatProvider, DateTimeStyles.AssumeLocal | DateTimeStyles.AdjustToUniversal);

            horaInicio = dataEHoraInicioReal.ToString("HH:mm");
            horaTermino = dataEHoraTerminoReal.ToString("HH:mm");

            //FORMATAÇÃO DE DATAS

            if (ddlInicioReal.Text.Equals(ddlTerminoReal.Text))
            {
                string inicioReal = ddlInicioReal.Text;
                string[] dataInicio = inicioReal.Split('/');
                DateTime dateTimeInicio = new DateTime(int.Parse(dataInicio[2]), int.Parse(dataInicio[1]), int.Parse(dataInicio[0]));

                string quando = string.Format("{0:D}", dateTimeInicio);
                quando += "  " + txtHoraInicioAta.Text + "-" + txtHoraTerminoAta.Text;

                indicadonDatas = string.Format(@"<tr style='font-family:Verdana; font-size:9pt'>
                                                <td><b>Quando :</b></td>
                                                <td>{0}</td>
                                           </tr>", quando);
            }
            else
            {
                string inicioReal = ddlInicioReal.Text + " - " + txtHoraInicioAta.Text;
                string terminoReal = ddlTerminoReal.Text + " - " + txtHoraTerminoAta.Text;

                indicadonDatas = string.Format(@"<tr style='font-family:Verdana; font-size:9pt'>
                                                <td><b>Início  :</b></td>
                                                <td>{0}</td>
                                           </tr>
                                           <tr style='font-family:Verdana; font-size:9pt'>
                                                <td><b>Término :</b></td>
                                                <td>{1}</td>
                                           </tr>", inicioReal, terminoReal);
            }

            string codigoResponsavelEvento = dsReuniao.Tables[0].Rows[0]["CodigoResponsavelEvento"].ToString();
            string localReuniao = dsReuniao.Tables[0].Rows[0]["LocalEvento"].ToString().Replace("'", "''"); 
            string emailResponsavelEvento = dsReuniao.Tables[0].Rows[0]["EmailResponsavel"].ToString(); 
            string nomeResponsavelEvento = dsReuniao.Tables[0].Rows[0]["NomeResponsavel"].ToString();

            destinatarios = emailResponsavelEvento + "; ";
            usuariosEvento.Add(nomeResponsavelEvento, emailResponsavelEvento);

            DataSet dsParticipantes = cDados.getParticipantesConfirmacaoEventos(codigoReuniao.ToString(), "");

            string[] arrayParticipantesSelecionados = new string[dsParticipantes.Tables[0].Rows.Count];

            if (cDados.DataSetOk(dsParticipantes) && cDados.DataTableOk(dsParticipantes.Tables[0]))
            {

                int i = 0;

                foreach (DataRow dr in dsParticipantes.Tables[0].Rows)
                {
                    if (dr["CodigoUsuario"].ToString() != "")
                    {
                        arrayParticipantesSelecionados[i++] = dr["CodigoUsuario"].ToString();
                        destinatarios += dr["EMail"].ToString() + "; ";
                        usuariosEvento.Add(dr["NomeUsuario"].ToString(), dr["EMail"].ToString());
                    }
                }
            }

            string projetos;

            string pendencias = getPendencias();

            planoAcao = getPlanoAcao();

            string nomeDescricao = "Projetos", descricaoUnidade = "Unidade";

            if (tipoReuniao == "E")
            {
                nomeDescricao = "Objetivos Estratégicos";
                descricaoUnidade = "Mapa Estratégico";

                projetos = getObjetivos();
            }
            else
            {
                projetos = getProjetos();
            }

            assunto = string.Format("Reunião Executada no {0} - {1}", nomeSistema.Replace("'", "''"), txtUnidade.Text);
            string encabecadoAta = "";
            if (editaMensagemEvento)
                encabecadoAta = heEncabecadoAta.Html; // mmEncabecadoAta.Text;
            else
                encabecadoAta = string.Format("Foi registrado uma reunião no {0}, sendo seu resumo apresentado a seguir:", cDados.getNomeSistema());


            mensagem = string.Format(@"<span style='font-family:Verdana; font-size:9pt'>
                                    <fieldset>
                                       <b>Prezado(a),</b>                                    
                                       <p>{11}
                                       <p>
                                       <p>
                                       <table id='idDadosReuniao' border='0' cellpadding='0' cellspacing='0' style='font-family:Verdana; font-size:9pt'>
                                           <tr>
                                                <td style='width:150px'><b>{10} :</b></td>
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
                                           {12}
                                           <tr>
                                                <td style='height: 10px'></td>
                                                <td></td>
                                           </tr>
                                           <tr>
                                                <td><b>Local :</b></td>
                                                <td>{3}</td>
                                           </tr>
                                       </table>
                                       <p>
                                       <hr>
                                       <p><b>{9}:</b> 
                                       {7}
                                       <hr>
                                       <p><b>Pendências:</b> 
                                       {8}
                                       <hr>
                                       <p><b>Ata:</b>
                                       <p>{4}.
                                       <hr>
                                       <p><b>Tarefas com responsabilidades e prazos finais:</b>
                                       <p>{5}
                                       <hr>
                                       <p>Att.,
                                       <p>{13}.
                                       <p><b>PS: Por favor, não responda esse e-mail.</b>
                                    </fieldset>                                   
                                  </span>
                                  ", txtAssunto.Text.Replace("'", "''")
                                   , ""
                                   , ""
                                   , localReuniao
                                   , memoAta.Html.Replace("'", "''")
                                   , planoAcao, txtUnidade.Text, projetos, pendencias, nomeDescricao
                                   , descricaoUnidade
                                   , encabecadoAta.Replace("'", "''")
                                   , indicadonDatas
                                   , cDados.getNomeSistema());

            int contador = 0;
            string participantesEvento = "";
            foreach(var usuario in usuariosEvento)
            {
                string nomeUsuario = usuario.Key;
                string emailUsuario = usuario.Value;

                if(nomeUsuario == nomeResponsavelEvento)
                {
                    participantesEvento += string.Format(@"ATTENDEE;ROLE=CHAIR;PARTSTAT=ACCEPTED ;CN=""{0}"";RSVP=FALSE :mailto:{1}", nomeUsuario
                , emailUsuario);
                }
                else
                {
                    participantesEvento += string.Format(@"ATTENDEE;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE :mailto:{0}", emailUsuario);
                }
                contador++;
                if (contador < usuariosEvento.Count)
                    participantesEvento += Environment.NewLine;
            }

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
END:VCALENDAR", dataEHoraInicioReal
        , horaInicio
        , emailResponsavelEvento
        , dataEHoraTerminoReal
        , horaTermino
        , ""
        , txtAssunto.Text
        , localReuniao
        , participantesEvento
        , nomeResponsavelEvento
        , memoPauta.Html);


            if (destinatarios != "")
            {
                int retornoStatus = 0;
                destinatarios = destinatarios.Remove(destinatarios.Length - 2);
                string statusEmail = cDados.enviarEmailCalendarAppWeb(assunto, destinatarios, "", mensagem, corpoAnexo, ref retornoStatus);

                if (retornoStatus == 1)
                {
                    cDados.atualizaEnvioAta(codigoReuniao.ToString(), codigoUsuarioResponsavel.ToString(), ref msgError1);
                    cDados.atualizaParticipantesEnvioAtaEmail(arrayParticipantesSelecionados, codigoReuniao.ToString(), ref msgError2);

                    msgError3 = persisteEdicaoRegistro();
                }
                else
                {
                    msgError3 = statusEmail;
                }
            }
        }
        return msgError1 + msgError2 + msgError3;
    }

    private string getPendencias()
    {
        string where = "", pendencias = "";

        where = " AND CodigoStatusTarefa <> 3 AND f.TerminoReal IS NULL ";

        DataSet dsPendencias = cDados.getPendenciasEvento(codigoReuniao, where);

        if (cDados.DataSetOk(dsPendencias) && cDados.DataTableOk(dsPendencias.Tables[0]))
        {
            foreach (DataRow dr in dsPendencias.Tables[0].Rows)
                pendencias += "<br style='font-family:Verdana; font-size:9pt'><p> - " + dr["DescricaoTarefa"].ToString();
        }

        if (pendencias == "")
            pendencias = "<br style='font-family:Verdana; font-size:9pt'><p> - Nenhuma Pendência.";

        return pendencias;
    }

    private string getProjetos()
    {
        string projetos = "";

        DataSet dsProjetos = cDados.getProjetosEvento(codigoReuniao, "");

        if (cDados.DataSetOk(dsProjetos) && cDados.DataTableOk(dsProjetos.Tables[0]))
        {
            foreach (DataRow dr in dsProjetos.Tables[0].Rows)
                projetos += "<br style='font-family:Verdana; font-size:9pt'> - " + dr["NomeProjeto"].ToString();
        }

        if (projetos == "")
            projetos = "<br style='font-family:Verdana; font-size:9pt'> - Nenhum Projeto.";

        return projetos;
    }

    private string getObjetivos()
    {
        string objetivos = "";

        DataSet dsObjetivos = cDados.getObjetivosEvento(codigoReuniao, "");

        if (cDados.DataSetOk(dsObjetivos) && cDados.DataTableOk(dsObjetivos.Tables[0]))
        {
            foreach (DataRow dr in dsObjetivos.Tables[0].Rows)
                objetivos += "<br style='font-family:Verdana; font-size:9pt'> - " + dr["DescricaoObjetivoEstrategico"].ToString();
        }

        if (objetivos == "")
            objetivos = "<br style='font-family:Verdana; font-size:9pt'> - Nenhum Objetivos Estratégico.";

        return objetivos;
    }

    private string getPlanoAcao()
    {
        string planoAcao = "", iniciais = "RE";

        //if(tipoReuniao == "E")
        //    iniciais = "ME";

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
    
    protected void pnGeral_Callback(object sender, CallbackEventArgsBase e)
    {

        string codTarefa = gvPendencias.GetRowValues(int.Parse(e.Parameter), "CodigoTarefa").ToString();
        string Anotacoes = gvPendencias.GetRowValues(int.Parse(e.Parameter), "Anotacoes").ToString();

        mmComentario.Text = Anotacoes;
        /*
                    string sComandoSql = string.Format(@"SELECT Anotacoes
                                                            FROM {0}.{1}.TarefaToDoList t
                                                            WHERE t.CodigoTarefa = {2}"
                                                        , dbName, dbOwner, codTarefa);
                    DataSet ds = cDados.getDataSet(sComandoSql);
                    if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                    {
                        mmComentario.Text = ds.Tables[0].Rows[0]["Anotacoes"].ToString();
                    }
         */

    }

    protected void gvPendencias_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (gvPendencias.VisibleRowCount > 0)
        {
            if (e.ButtonID == "btnComentarios")
            {

                string Anotacoes = (gvPendencias.GetRowValues(e.VisibleIndex, "Anotacoes") == null) ? "" : gvPendencias.GetRowValues(e.VisibleIndex, "Anotacoes").ToString();
                if (Anotacoes.Trim().Length == 0)
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "ReunPlan");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "ReunPlan", lblTituloTela.Text, this);
    }

    #endregion

    protected void ASPxGridViewExporter1_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
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
        if (ASPxGridViewExporter1.GridViewID == "gvPendencias")
        {
            renderBrickPendencias(sender, e);
        }
        else if (ASPxGridViewExporter1.GridViewID == "gvProjetos")
        {
            renderBrickProjetos(sender, e);
        }
        else if (ASPxGridViewExporter1.GridViewID == "gvObjetivos")
        {
            renderBrickObjetivos(sender, e);
        }
    }

    private void renderBrickPendencias(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.Column.Name == "Estagio" && e.Value != null)
        {
            Font fontex = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
            e.BrickStyle.Font = fontex;
            e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            if (e.Value.ToString().Equals("vermelhook"))
            {
                e.Text = "ü";
                e.TextValue = "ü";
                e.BrickStyle.ForeColor = Color.Red;
            }
            else if (e.Value.ToString().ToLower().Contains("verdeok"))
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
            else if (e.Value.ToString().ToLower().Contains("vermelho"))
            {
                e.Text = "l";
                e.TextValue = "l";
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

    private void renderBrickProjetos(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
    {
        if ((e.Column.Name == "DesempenhoAtual" || e.Column.Name == "DesempenhoAnterior") && e.Value != null)
        {
            Font fontex = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
            e.BrickStyle.Font = fontex;
            e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            if (e.Value.ToString().Equals("vermelhook"))
            {
                e.Text = "ü";
                e.TextValue = "ü";
                e.BrickStyle.ForeColor = Color.Red;
            }
            else if (e.Value.ToString().ToLower().Contains("verdeok"))
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
            else if (e.Value.ToString().ToLower().Contains("vermelho"))
            {
                e.Text = "l";
                e.TextValue = "l";
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
        }
    }

    private void renderBrickObjetivos(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.Column.Name == "Desempenho" && e.Value != null)
        {
            Font fontex = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
            e.BrickStyle.Font = fontex;
            e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            if (e.Value.ToString().Equals("vermelhook"))
            {
                e.Text = "ü";
                e.TextValue = "ü";
                e.BrickStyle.ForeColor = Color.Red;
            }
            else if (e.Value.ToString().ToLower().Contains("verdeok"))
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
            else if (e.Value.ToString().ToLower().Contains("vermelho"))
            {
                e.Text = "l";
                e.TextValue = "l";
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
        }
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
    protected void gvPendencias_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
    {

    }
}
