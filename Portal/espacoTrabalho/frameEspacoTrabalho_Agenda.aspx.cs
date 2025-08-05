//Revisado
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
using DevExpress.XtraScheduler.VCalendar;
using System.IO;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler.iCalendar;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Localization;
using DevExpress.Utils.Localization.Internal;
using System.Globalization;

public partial class espacoTrabalho_frameEspacoTrabalho_Agenda : System.Web.UI.Page
{
    DataSet dset = new DataSet();
    dados cDados;

    int registrosAfetados = 0;
    //int codigoUsuario = 0;
    public Unit alturaArea = 0;

    int codigoUsuarioResponsavel;
    int codigoEntidadeUsuarioResponsavel;


    protected void Page_Load(object sender, EventArgs e)
    {

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();
        string sufixo = System.Threading.Thread.CurrentThread.CurrentCulture.Name.StartsWith("pt", StringComparison.InvariantCultureIgnoreCase) == true ? "pt-BR" : "en-US";
        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        this.Title = cDados.getNomeSistema();

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        calendarioAgenda.OptionsForms.AppointmentFormTemplateUrl = string.Format(@"../DevExpress_Trad/ASPxSchedulerForms/cp_AppointmentForm.{0}.ascx", sufixo);
        calendarioAgenda.OptionsForms.RecurrentAppointmentDeleteFormTemplateUrl = string.Format(@"../DevExpress_Trad/ASPxSchedulerForms/RecurrentAppointmentDeleteForm.{0}.ascx", sufixo);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/Agenda.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        this.TH(this.TS("Agenda", "frameEspacoTrabalho_Agenda"));



        /* calendario lista */
        carregaAgenda();
        //ASPxScheduler1.ActiveViewType = (rbLista.SelectedIndex == 0) ? DevExpress.XtraScheduler.SchedulerViewType.Month : DevExpress.XtraScheduler.SchedulerViewType.WorkWeek;

        if (!IsPostBack)
        {
            calendarioAgenda.DayView.VisibleTime.Start = new TimeSpan(08, 00, 00);
            calendarioAgenda.DayView.VisibleTime.End = new TimeSpan(19, 00, 00);
            calendarioAgenda.WorkWeekView.VisibleTime.Start = new TimeSpan(08, 00, 00);
            calendarioAgenda.WorkWeekView.VisibleTime.End = new TimeSpan(19, 00, 00);
            calendarioAgenda.DayView.TimeScale = new TimeSpan(1, 0, 0);
            calendarioAgenda.WorkWeekView.TimeScale = new TimeSpan(1, 0, 0);
            calendarioAgenda.GoToDate(DateTime.Now);
        }
        defineAlturaTela(cDados.getInfoSistema("ResolucaoCliente").ToString());
        //ASPxScheduler1.WorkWeekView.AppointmentDisplayOptions.AppointmentHeight =400;

        //cDados.aplicaEstiloVisual(Page);

        CustomLocalizerCore.Activate();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "AGENDA", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela

        if (resolucaoCliente != "")
        {
            int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));


            calendarioAgenda.WorkWeekView.Styles.ScrollAreaHeight = new Unit((alturaPrincipal - 350) - ((alturaPrincipal - 350) * 0.2) + "px");
            calendarioAgenda.WorkWeekView.Styles.AllDayAreaHeight = new Unit((0.2 * (alturaPrincipal - 350)) + "px");

            calendarioAgenda.DayView.Styles.ScrollAreaHeight = new Unit((alturaPrincipal - 350) - ((alturaPrincipal - 350) * 0.2) + "px");
            calendarioAgenda.DayView.Styles.AllDayAreaHeight = new Unit((0.2 * (alturaPrincipal - 350)) + "px");

            int alturasomada = int.Parse(calendarioAgenda.WorkWeekView.Styles.ScrollAreaHeight.ToString().Substring(0, calendarioAgenda.WorkWeekView.Styles.ScrollAreaHeight.ToString().IndexOf("p")));
            alturasomada += int.Parse(calendarioAgenda.WorkWeekView.Styles.AllDayAreaHeight.ToString().Substring(0, calendarioAgenda.WorkWeekView.Styles.AllDayAreaHeight.ToString().IndexOf("p")));
            alturasomada -= 24;
            calendarioAgenda.TimelineView.Styles.TimelineCellBody.Height = new Unit(alturasomada + "px");

            /*proporção de 5 celulas em altura = celula+ cabecalho*/
            calendarioAgenda.MonthView.Styles.DateCellBody.Height = new Unit((alturasomada / 5) - 19 + "px");

            /*proporção de 4 celulas em altura = celula + cabeçalho*/
            calendarioAgenda.WeekView.Styles.DateCellBody.Height = new Unit((alturasomada / 4) - 13 + "px");

        }
    }

    public void carregaAgenda()
    {
        dset = cDados.getAgenda("", codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel);

        calendarioAgenda.AppointmentDataSource = dset;


        //dset = cDados.getFornecedores("", 0);

        //ASPxScheduler1.ResourceDataSource = dset;
        try
        {
            calendarioAgenda.DataBind();
        }
        catch { }
    }

    protected void ASPxScheduler1_AppointmentRowInserting(object sender, DevExpress.Web.ASPxScheduler.ASPxSchedulerDataInsertingEventArgs e)
    {
        int tipo = -1;
        string inicio = "";
        string termino = "";
        int diaTodo = -1;
        int status = -1;
        int categoria = -1;
        string descricao = "";
        string local = "";
        string informacaoRecorrente = "";
        string informacaoLembrete = "";
        int IDRecurso = -1;
        string assunto = "";
        int ID = -1;

        string horarioInicio = "";
        string horarioTermino = "";

        //Tipo
        if (e.NewValues["TipoEvento"] != null)
        {
            tipo = int.Parse(e.NewValues["TipoEvento"].ToString());
        }

        //Início
        if (e.NewValues["DataInicio"] != null)
        {
            inicio = e.NewValues["DataInicio"].ToString();
        }

        //Término
        if (e.NewValues["DataTermino"] != null)
        {
            termino = e.NewValues["DataTermino"].ToString();
        }

        //Dia Todo
        if (e.NewValues["DiaInteiro"] != null)
        {
            diaTodo = (e.NewValues["DiaInteiro"].ToString() == "true" ? 1 : 0);
        }

        //status
        if (e.NewValues["Status"] != null)
        {
            status = int.Parse(e.NewValues["Status"].ToString()); ;
        }

        //Categoria
        if (e.NewValues["Rotulo"] != null)
        {
            categoria = int.Parse(e.NewValues["Rotulo"].ToString()); ;
        }

        //Descrição
        if (e.NewValues["Anotacoes"] != null)
        {
            descricao = e.NewValues["Anotacoes"].ToString();
        }

        //Local
        if (e.NewValues["Local"] != null)
        {
            local = e.NewValues["Local"].ToString();
        }

        //Informação Recorrente
        if (e.NewValues["DescricaoRecorrencia"] != null)
        {
            informacaoRecorrente = e.NewValues["DescricaoRecorrencia"].ToString();
        }

        //Informação Lembrete
        if (e.NewValues["DescricaoAlerta"] != null)
        {
            informacaoLembrete = e.NewValues["DescricaoAlerta"].ToString();
        }

        //ID Recurso
        if (e.NewValues["CodigoUsuario"] != null)
        {
            IDRecurso = int.Parse(e.NewValues["CodigoUsuario"].ToString());
        }

        //Assunto
        if (e.NewValues["Assunto"] != null)
        {
            assunto = e.NewValues["Assunto"].ToString();
        }

        //ID
        if (e.NewValues["CodigoCompromissoUsuario"] != null && e.NewValues["CodigoCompromissoUsuario"].ToString() != "")
        {
            ID = int.Parse(e.NewValues["CodigoCompromissoUsuario"].ToString());
        }


        if (hfGeral.Contains("HorarioInicio") == true)
        {
            horarioInicio = hfGeral.Get("HorarioInicio").ToString();
        }

        if (hfGeral.Contains("HorarioTermino") == true)
        {
            horarioTermino = hfGeral.Get("HorarioTermino").ToString();
        }

        insereNovo(tipo, inicio, horarioInicio, termino, horarioTermino, diaTodo, status, categoria, descricao, local, informacaoRecorrente, informacaoLembrete, IDRecurso, assunto, ID);

        //Atualiza o componente após a inserção da agenda
        //Atualiza um Refresh via DevExpress para Atualizar o ID do componente para uma Deleção em seguida.
        if (calendarioAgenda.JSProperties.ContainsKey("cp_recarregar"))
            calendarioAgenda.JSProperties["cp_recarregar"] = "S";
        else
            calendarioAgenda.JSProperties.Add("cp_recarregar", "S");
        e.Cancel = true;
    }
    protected void ASPxScheduler1_ActiveViewChanging(object sender, DevExpress.Web.ASPxScheduler.ActiveViewChangingEventArgs e)
    {
        carregaAgenda();
    }

    public void insereNovo(int tipo, string inicio, string horarioInicio, string termino, string horarioTermino, int diaTodo, int status, int categoria, string descricao, string local, string informacaoRecorrente, string informacaoLembrete, int IDRecurso, string assunto, int ID)
    {
        inicio = inicio.Replace("00:00:00", horarioInicio);

        termino = termino.Replace("00:00:00", horarioTermino);
        //Converte a Data para o padrão Brasíleiro 103 Como está no comando SQL
        inicio = Convert.ToDateTime(inicio).ToString("dd/MM/yyyy hh:mm:ss");
        termino = Convert.ToDateTime(termino).ToString("dd/MM/yyyy hh:mm:ss");

        hfGeral.Clear();

        cDados.incluiAgenda(tipo, inicio, termino, diaTodo, status, categoria, descricao, local, informacaoRecorrente, informacaoLembrete, IDRecurso, assunto, ID, codigoUsuarioResponsavel, ref registrosAfetados, codigoEntidadeUsuarioResponsavel);
    }

    protected void ASPxScheduler1_AppointmentRowUpdating(object sender, ASPxSchedulerDataUpdatingEventArgs e)
    {
        int tipo = -1;
        string inicio = "";
        string horarioInicio = "";
        string termino = "";
        string horarioTermino = "";
        int diaTodo = -1;
        int status = -1;
        int categoria = -1;
        string descricao = "";
        string local = "";
        string informacaoRecorrente = "";
        string informacaoLembrete = "";
        int IDRecurso = -1;
        string assunto = "";
        int ID = -1;

        //Tipo
        if (e.NewValues["TipoEvento"] != null)
        {
            tipo = int.Parse(e.NewValues["TipoEvento"].ToString());
        }

        //Início
        if (e.NewValues["DataInicio"] != null)
        {
            inicio = e.NewValues["DataInicio"].ToString();
        }

        //Término
        if (e.NewValues["DataTermino"] != null)
        {
            termino = e.NewValues["DataTermino"].ToString();
        }

        //Dia Todo
        if (e.NewValues["DiaInteiro"] != null)
        {
            diaTodo = (e.NewValues["DiaInteiro"].ToString() == "true" ? 1 : 0);
        }

        //status
        if (e.NewValues["Status"] != null)
        {
            status = int.Parse(e.NewValues["Status"].ToString()); ;
        }

        //Categoria
        if (e.NewValues["Rotulo"] != null)
        {
            categoria = int.Parse(e.NewValues["Rotulo"].ToString()); ;
        }

        //Descrição
        if (e.NewValues["Anotacoes"] != null)
        {
            descricao = e.NewValues["Anotacoes"].ToString();
        }

        //Local
        if (e.NewValues["Local"] != null)
        {
            local = e.NewValues["Local"].ToString();
        }

        //Informação Recorrente
        if (e.NewValues["DescricaoRecorrencia"] != null)
        {
            informacaoRecorrente = e.NewValues["DescricaoRecorrencia"].ToString();
        }

        //Informação Lembrete
        if (e.NewValues["DescricaoAlerta"] != null)
        {
            informacaoLembrete = e.NewValues["DescricaoAlerta"].ToString();
        }

        //ID Recurso
        if (e.NewValues["CodigoUsuario"] != null)
        {
            IDRecurso = int.Parse(e.NewValues["CodigoUsuario"].ToString());
        }

        //Assunto
        if (e.NewValues["Assunto"] != null)
        {
            assunto = e.NewValues["Assunto"].ToString();
        }

        if (e.NewValues["CodigoCompromissoUsuario"] != null)
        {
            ID = int.Parse(e.NewValues["CodigoCompromissoUsuario"].ToString());
        }


        if (hfGeral.Contains("HorarioInicio") == true)
        {
            //verifica se o horário está dentro do padrão 103 exigido na query update.
            if (hfGeral.Get("HorarioInicio").ToString().Length > 5)
            {
                horarioInicio = "00:00";
            }
            else
            {
                horarioInicio = hfGeral.Get("HorarioInicio").ToString();
            }
        }

        if (hfGeral.Contains("HorarioTermino") == true)
        {
            //verifica se o horário está dentro do padrão 103 exigido na query update.
            if (hfGeral.Get("HorarioInicio").ToString().Length > 5)
            {
                horarioTermino = "00:00";
            }
            else
            {
                horarioTermino = hfGeral.Get("HorarioTermino").ToString();
            }
        }

        if (e.Keys.Count > 0 && e.Keys[0] != null)
            alteraDado(tipo, inicio, horarioInicio, termino, horarioTermino, diaTodo, status, categoria, descricao, local, informacaoRecorrente, informacaoLembrete, IDRecurso, assunto, int.Parse(e.Keys[0].ToString()), ID);
        //carregaAgenda();
        e.Cancel = true;
    }

    public void alteraDado(int tipo, string inicio, string horarioInicio, string termino, string horarioTermino, int diaTodo, int status, int categoria, string descricao, string local, string informacaoRecorrente, string informacaoLembrete, int IDRecurso, string assunto, int codigoAgenda, int ID)
    {
        //Se o Banco For em Englis Atualiza data Para português, Caso seje Portuguê Faz o mesmo.
        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name.StartsWith("pt", StringComparison.InvariantCultureIgnoreCase))
        {
            //Trasnforma qualquer data que venha pra edição trasnforma em Padrão português 113, como requere a Proc.
            if (!string.IsNullOrEmpty(horarioInicio) && !string.IsNullOrEmpty(horarioTermino))
            {
                //Trasnforma qualquer data que venha pra edição trasnforma em Padrão português 113, como requere a Proc.
                inicio = Convert.ToDateTime(inicio).Day.ToString() + "/" + Convert.ToDateTime(inicio).Month.ToString() + "/" + Convert.ToDateTime(inicio).Year.ToString() + " " + (string.IsNullOrEmpty(horarioInicio) ? "00:00:00" : horarioInicio);
                termino = Convert.ToDateTime(termino).Day.ToString() + "/" + Convert.ToDateTime(termino).Month.ToString() + "/" + Convert.ToDateTime(termino).Year.ToString() + " " + (string.IsNullOrEmpty(horarioTermino) ? "00:00:00" : horarioTermino);
            }
        }
        else
        {
            //Trasnforma qualquer data que venha pra edição trasnforma em Padrão português 113, como requere a Proc.
            inicio = Convert.ToDateTime(inicio).Month.ToString() + "/" + Convert.ToDateTime(inicio).Day.ToString() + "/" + Convert.ToDateTime(inicio).Year.ToString() + " " + (string.IsNullOrEmpty(horarioInicio) ? "00:00:00" : horarioInicio);
            termino = Convert.ToDateTime(termino).Month.ToString() + "/" + Convert.ToDateTime(termino).Day.ToString() + "/" + Convert.ToDateTime(termino).Year.ToString() + " " + (string.IsNullOrEmpty(horarioTermino) ? "00:00:00" : horarioTermino);
        }
        hfGeral.Clear();
        cDados.atualizaAgenda(tipo, inicio, termino, diaTodo, status, categoria, descricao, local, informacaoRecorrente, informacaoLembrete, IDRecurso, assunto, ID, codigoUsuarioResponsavel, codigoAgenda, ref registrosAfetados, codigoEntidadeUsuarioResponsavel);
    }

    protected void ASPxScheduler1_AppointmentRowDeleting(object sender, ASPxSchedulerDataDeletingEventArgs e)
    {
        cDados.excluiAgenda(int.Parse(e.Keys[0].ToString()), ref registrosAfetados);
        e.Cancel = true;
        carregaAgenda();
    }

    protected void ASPxScheduler1_AppointmentFormShowing(object sender, DevExpress.Web.ASPxScheduler.AppointmentFormEventArgs e)
    {
        e.Container.Caption = Resources.traducao.frameEspacoTrabalho_Agenda_novo_compromisso;

    }




    #region ExportSelectedAppointmentsCallbackCommand
    public class ExportSelectedAppointmentsCallbackCommand : SchedulerCallbackCommand
    {
        string nomeArquivo = "";

        public ExportSelectedAppointmentsCallbackCommand(ASPxScheduler control, string pathAplicacao)
            : base(control)
        {
            nomeArquivo = pathAplicacao;
        }

        public override string Id { get { return "EXPORTAPT"; } }



        protected override void ParseParameters(string parameters)
        {
        }
        protected override void ExecuteCore()
        {
            PostCalendarFile(Control.SelectedAppointments);
        }
        void PostCalendarFile(AppointmentBaseCollection appointments)
        {
            iCalendarExporter exporter = new iCalendarExporter(Control.Storage, appointments);

            if (appointments.Count == 0)
                return;

            exporter.Export(nomeArquivo);
        }

    }
    #endregion

    protected void ASPxScheduler1_CustomErrorText(object handler, ASPxSchedulerCustomErrorTextEventArgs e)
    {
        e.ErrorText = e.Exception.Message;
    }
    protected void ASPxScheduler1_CustomCallback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        //carregaAgenda();
        //ASPxScheduler1.DayView.VisibleTime.Start.Negate();
        if (cbHorarioTrabalho.Checked)
        {
            calendarioAgenda.DayView.VisibleTime.Start = new TimeSpan(08, 00, 00);
            calendarioAgenda.DayView.VisibleTime.End = new TimeSpan(19, 00, 00);
            calendarioAgenda.WorkWeekView.VisibleTime.Start = new TimeSpan(08, 00, 00);
            calendarioAgenda.WorkWeekView.VisibleTime.End = new TimeSpan(19, 00, 00);

        }
        else
        {
            calendarioAgenda.DayView.VisibleTime.Start = new TimeSpan(00, 00, 00);
            calendarioAgenda.DayView.VisibleTime.End = new TimeSpan(23, 59, 59);
            calendarioAgenda.WorkWeekView.VisibleTime.Start = new TimeSpan(00, 00, 00);
            calendarioAgenda.WorkWeekView.VisibleTime.End = new TimeSpan(23, 59, 59);
        }
    }

    protected void ASPxScheduler1_PopupMenuShowing(object sender, DevExpress.Web.ASPxScheduler.PopupMenuShowingEventArgs e)
    {
        ASPxSchedulerPopupMenu menu = e.Menu;
        if (menu.MenuId.Equals(SchedulerMenuItemId.AppointmentMenu))
        {
            DevExpress.Web.MenuItem item = new DevExpress.Web.MenuItem("Exportar", "ExportAppointment");
            //e.Menu.Items.Insert(1, item);
            //e.Menu.ClientSideEvents.ItemClick = "function(s, e) { OnMenuClick(s,e); }";
        }

        for (int i = 0; i < e.Menu.Items.Count; i++)
        {
            //Define Quais Menus da Agenda com o botão direito irão aparecer ou não.
            if (e.Menu.Items[i].Name == "GotoToday" || e.Menu.Items[i].Name == "GotoDate" || e.Menu.Items[i].Name == "StatusSubMenu")
            {
                e.Menu.Items[i].Visible = true;
            }
            else
            {
                e.Menu.Items[i].Visible = false;
            };
        }
    }

    public class CustomLocalizerCore : DevExpress.XtraScheduler.Localization.SchedulerResLocalizer
    {
        public static void Activate()
        {
            CustomLocalizerCore myLocalizerCore = new CustomLocalizerCore();
            DefaultActiveLocalizerProvider<SchedulerStringId> providerCore = new DefaultActiveLocalizerProvider<SchedulerStringId>(myLocalizerCore);
            SchedulerResLocalizer.SetActiveLocalizerProvider(providerCore);
            SchedulerResLocalizer.Active = myLocalizerCore;
        }

        public override string GetLocalizedString(DevExpress.XtraScheduler.Localization.SchedulerStringId id)
        {

            if (id == SchedulerStringId.Caption_10Minutes)
            {
                return Resources.traducao.frameEspacoTrabalho_Agenda__0_minutos;
            }
            else if (id == SchedulerStringId.Caption_15Minutes)
                return Resources.traducao.frameEspacoTrabalho_Agenda__5_minutos;
            else if (id == SchedulerStringId.Caption_20Minutes)
                return Resources.traducao.frameEspacoTrabalho_Agenda__0_minutos;
            else if (id == SchedulerStringId.Caption_30Minutes)
                return Resources.traducao.frameEspacoTrabalho_Agenda__0_minutos;
            else if (id == SchedulerStringId.Caption_5Minutes)
                return Resources.traducao.frameEspacoTrabalho_Agenda___minutos;
            else if (id == SchedulerStringId.Caption_60Minutes)
                return Resources.traducao.frameEspacoTrabalho_Agenda__0_minutos;
            else if (id == SchedulerStringId.Caption_6Minutes)
                return Resources.traducao.frameEspacoTrabalho_Agenda___minutos;
            else if (id == SchedulerStringId.Caption_Appointment)
                return Resources.traducao.frameEspacoTrabalho_Agenda_compromisso;
            else if (id == SchedulerStringId.Caption_RecurrenceEndTime)
                return Resources.traducao.frameEspacoTrabalho_Agenda_t_rmino_da_recorr_ncia;
            else if (id == SchedulerStringId.Caption_StartTime)
                return Resources.traducao.frameEspacoTrabalho_Agenda_in_cio;
            else if (id == SchedulerStringId.Caption_EmptyResource)
                return Resources.traducao.frameEspacoTrabalho_Agenda_compromisso;
            else if (id == SchedulerStringId.TimeScaleDisplayName_Day)
                return Resources.traducao.frameEspacoTrabalho_Agenda_dia;
            else if (id == SchedulerStringId.TimeScaleDisplayName_Hour)
                return Resources.traducao.frameEspacoTrabalho_Agenda_hora;
            else if (id == SchedulerStringId.TimeScaleDisplayName_Month)
                return Resources.traducao.frameEspacoTrabalho_Agenda_m_s;
            else if (id == SchedulerStringId.TimeScaleDisplayName_Quarter)
                return Resources.traducao.frameEspacoTrabalho_Agenda_trimestre;
            else if (id == SchedulerStringId.TimeScaleDisplayName_Week)
                return Resources.traducao.frameEspacoTrabalho_Agenda_semana;
            else if (id == SchedulerStringId.TimeScaleDisplayName_Year)
                return Resources.traducao.frameEspacoTrabalho_Agenda_ano;
            else if (id == SchedulerStringId.ViewDisplayName_Day)
                return Resources.traducao.frameEspacoTrabalho_Agenda_dia;
            else if (id == SchedulerStringId.ViewDisplayName_Month)
                return Resources.traducao.frameEspacoTrabalho_Agenda_m_s;
            else if (id == SchedulerStringId.ViewDisplayName_Week)
                return Resources.traducao.frameEspacoTrabalho_Agenda_semana;
            else if (id == SchedulerStringId.ViewDisplayName_WorkDays)
                return Resources.traducao.frameEspacoTrabalho_Agenda_dia__teis;
            else if (id == SchedulerStringId.ViewShortDisplayName_FullWeek)
                return Resources.traducao.frameEspacoTrabalho_Agenda_semana_inteira;
            else if (id == SchedulerStringId.AppointmentLabel_None)
                return Resources.traducao.frameEspacoTrabalho_Agenda_compromisso;
            else if (id == SchedulerStringId.MenuCmd_OpenAppointment)
                return Resources.traducao.frameEspacoTrabalho_Agenda_abrir___;
            else if (id == SchedulerStringId.MenuCmd_GotoDate)
                return Resources.traducao.frameEspacoTrabalho_Agenda_ir_para___;
            else if (id == SchedulerStringId.MenuCmd_GotoToday)
                return Resources.traducao.frameEspacoTrabalho_Agenda_ir_para_hoje___;
            else if (id == SchedulerStringId.MenuCmd_NavigateBackward)
                return Resources.traducao.frameEspacoTrabalho_Agenda_voltar;
            else if (id == SchedulerStringId.MenuCmd_NavigateForward)
                return Resources.traducao.frameEspacoTrabalho_Agenda_avan_ar;
            else if (id == SchedulerStringId.MenuCmd_NewAppointment)
                return Resources.traducao.frameEspacoTrabalho_Agenda_novo_compromisso;
            else if (id == SchedulerStringId.MenuCmd_NewAllDayEvent)
                return Resources.traducao.frameEspacoTrabalho_Agenda_novo_compromisso__dia_inteiro_;
            else if (id == SchedulerStringId.MenuCmd_NewRecurringAppointment)
                return Resources.traducao.frameEspacoTrabalho_Agenda_novo_compromisso_recorrente;
            else if (id == SchedulerStringId.MenuCmd_OpenAppointment)
                return Resources.traducao.frameEspacoTrabalho_Agenda_abrir_compromisso;
            else if (id == SchedulerStringId.MenuCmd_SwitchToDayView)
                return Resources.traducao.frameEspacoTrabalho_Agenda_visualiza__o_di_ria;
            else if (id == SchedulerStringId.MenuCmd_SwitchToMonthView)
                return Resources.traducao.frameEspacoTrabalho_Agenda_visualiza__o_mensal;
            else if (id == SchedulerStringId.MenuCmd_SwitchToTimelineView)
                return Resources.traducao.frameEspacoTrabalho_Agenda_linha_do_tempo;

            else if (id == SchedulerStringId.MenuCmd_TimeScaleCaptionsMenu)
                return Resources.traducao.frameEspacoTrabalho_Agenda_escalas_de_tempo;
            else if (id == SchedulerStringId.MenuCmd_SwitchViewMenu)
                return Resources.traducao.frameEspacoTrabalho_Agenda_alternar_para;
            else if (id == SchedulerStringId.MenuCmd_TimeScaleDay)
                return Resources.traducao.frameEspacoTrabalho_Agenda_dia;
            else if (id == SchedulerStringId.MenuCmd_TimeScaleMonth)
                return Resources.traducao.frameEspacoTrabalho_Agenda_m_s;
            else if (id == SchedulerStringId.MenuCmd_TimeScaleYear)
                return Resources.traducao.frameEspacoTrabalho_Agenda_ano;
            else if (id == SchedulerStringId.MenuCmd_TimeScaleQuarter)
                return Resources.traducao.frameEspacoTrabalho_Agenda_trimestre;
            else if (id == SchedulerStringId.MenuCmd_TimeScaleWeek)
                return Resources.traducao.frameEspacoTrabalho_Agenda_semana;
            else if (id == SchedulerStringId.MenuCmd_TimeScaleHour)
                return Resources.traducao.frameEspacoTrabalho_Agenda_hora;
            else if (id == SchedulerStringId.MenuCmd_DeleteAppointment)
                return Resources.traducao.frameEspacoTrabalho_Agenda_excluir;
            else if (id == SchedulerStringId.MenuCmd_EditSeries)
                return Resources.traducao.frameEspacoTrabalho_Agenda_atualizar_compromisso_rotineiro;
            else if (id == SchedulerStringId.MenuCmd_GotoThisDay)
                return Resources.traducao.frameEspacoTrabalho_Agenda_ir_para_este_dia;
            else if (id == SchedulerStringId.MenuCmd_LabelAs)
                return Resources.traducao.frameEspacoTrabalho_Agenda_tipo_de_compromisso;
            else if (id == SchedulerStringId.MenuCmd_ShowTimeAs)
                return Resources.traducao.frameEspacoTrabalho_Agenda_mostrar_como___;
            else if (id == SchedulerStringId.TextDuration_FromTo)
                return Resources.traducao.frameEspacoTrabalho_Agenda_de__0__at___1_;
            else if (id == SchedulerStringId.TextDuration_FromForDaysMinutes)
                return Resources.traducao.frameEspacoTrabalho_Agenda_de__0__por__1___3_;
            else if (id == SchedulerStringId.TextDuration_FromForDaysHoursMinutes)
                return Resources.traducao.frameEspacoTrabalho_Agenda_de__0__por__1___2___3_;
            else if (id == SchedulerStringId.TextDuration_FromForDaysHours)
                return Resources.traducao.frameEspacoTrabalho_Agenda_de__0__por__1___2_;
            else if (id == SchedulerStringId.TextDuration_FromForDays)
                return Resources.traducao.frameEspacoTrabalho_Agenda_de__0__por__1_;
            else if (id == SchedulerStringId.DescCmd_NewRecurringAppointment)
                return Resources.traducao.frameEspacoTrabalho_Agenda_cria_um_compromisso_de_rotina;
            else if (id == SchedulerStringId.Appointment_EndContinueText)
                return Resources.traducao.frameEspacoTrabalho_Agenda_at___0_;
            else if (id == SchedulerStringId.Appointment_StartContinueText)
                return Resources.traducao.frameEspacoTrabalho_Agenda_de__0_;
            else if (id == SchedulerStringId.Caption_DayViewDescription)
                return Resources.traducao.frameEspacoTrabalho_Agenda_vis_o_dia;
            else if (id == SchedulerStringId.Caption_MonthViewDescription)
                return Resources.traducao.frameEspacoTrabalho_Agenda_vis_o_mensal;
            else if (id == SchedulerStringId.Caption_TimelineViewDescription)
                return Resources.traducao.frameEspacoTrabalho_Agenda_vis_o_de_linha_do_tempo;

            return base.GetLocalizedString(id);
        }
    }
}