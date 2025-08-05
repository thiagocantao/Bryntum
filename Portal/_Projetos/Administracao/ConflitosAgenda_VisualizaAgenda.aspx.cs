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


public partial class _Projetos_Administracao_ConflitosAgenda_VisualizaAgenda : System.Web.UI.Page
{
    DataSet dset = new DataSet();
    dados cDados;

    int registrosAfetados = 0;
    //int codigoUsuario = 0;
    public Unit alturaArea = 0;

    int codigoUsuarioResponsavel;
    int codigoEntidadeUsuarioResponsavel;
    int codigoAcao;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        this.Title = cDados.getNomeSistema();

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoAcao = int.Parse(Request.QueryString["CA"]);
        ASPxScheduler1.OptionsForms.AppointmentFormTemplateUrl = "../../DevExpress_Trad/ASPxSchedulerForms/ConflitosAgenda_AppointmentForm.pt-BR.ascx";
        ASPxScheduler1.OptionsForms.RecurrentAppointmentDeleteFormTemplateUrl = "../../DevExpress_Trad/ASPxSchedulerForms/RecurrentAppointmentDeleteForm.ascx";

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/Agenda.js""></script>"));
        this.TH(this.TS("Agenda"));
        if (!IsPostBack)
            AtualizaInformacoes();
        /* calendario lista */
        carregaAgenda();
        //ASPxScheduler1.ActiveViewType = (rbLista.SelectedIndex == 0) ? DevExpress.XtraScheduler.SchedulerViewType.Month : DevExpress.XtraScheduler.SchedulerViewType.WorkWeek;

        if (!IsPostBack)
        {
            ASPxScheduler1.DayView.VisibleTime.Start = new TimeSpan(08, 00, 00);
            ASPxScheduler1.DayView.VisibleTime.End = new TimeSpan(19, 00, 00);
            ASPxScheduler1.WorkWeekView.VisibleTime.Start = new TimeSpan(08, 00, 00);
            ASPxScheduler1.WorkWeekView.VisibleTime.End = new TimeSpan(19, 00, 00);
            ASPxScheduler1.DayView.TimeScale = new TimeSpan(1, 0, 0);
            ASPxScheduler1.WorkWeekView.TimeScale = new TimeSpan(1, 0, 0);
        }
        defineAlturaTela(cDados.getInfoSistema("ResolucaoCliente").ToString());
        //ASPxScheduler1.WorkWeekView.AppointmentDisplayOptions.AppointmentHeight =400;

        cDados.aplicaEstiloVisual(Page);
    }

    private void AtualizaInformacoes()
    {
        string comandoSql = string.Format(@"
 SELECT NomeAcao, Inicio, Termino FROM {0}.{1}.tai02_AcoesIniciativa WHERE CodigoAcao = {2}"
            , cDados.getDbName(), cDados.getDbOwner(), codigoAcao);
        DataSet ds = cDados.getDataSet(comandoSql);
        DataRow dr = ds.Tables[0].Rows[0];
        string nomeAcao = (string)dr["NomeAcao"];
        DateTime dataInicio = (dr["Inicio"] is DateTime) ?
            (DateTime)dr["Inicio"] : DateTime.Now;
        DateTime dataTermino = (dr["Termino"] is DateTime) ?
            (DateTime)dr["Termino"] : DateTime.Now;

        lblTituloTela.Text = string.Format("Conflito de agenda da Atividade '{0}' para o dia {1:dd/MM/yyyy}"
            , nomeAcao, dataInicio);
        deInicio.Value = dataInicio;
        deTermino.Value = dataTermino;
        ASPxScheduler1.GoToDate(dataInicio);
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela

        if (resolucaoCliente != "")
        {
            int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));


            ASPxScheduler1.WorkWeekView.Styles.ScrollAreaHeight = new Unit((alturaPrincipal - 290) - ((alturaPrincipal - 290) * 0.2) + "px");
            ASPxScheduler1.WorkWeekView.Styles.AllDayAreaHeight = new Unit((0.2 * (alturaPrincipal - 290)) + "px");

            ASPxScheduler1.DayView.Styles.ScrollAreaHeight = new Unit((alturaPrincipal - 290) - ((alturaPrincipal - 290) * 0.2) + "px");
            ASPxScheduler1.DayView.Styles.AllDayAreaHeight = new Unit((0.2 * (alturaPrincipal - 290)) + "px");

            int alturasomada = int.Parse(ASPxScheduler1.WorkWeekView.Styles.ScrollAreaHeight.ToString().Substring(0, ASPxScheduler1.WorkWeekView.Styles.ScrollAreaHeight.ToString().IndexOf("p")));
            alturasomada += int.Parse(ASPxScheduler1.WorkWeekView.Styles.AllDayAreaHeight.ToString().Substring(0, ASPxScheduler1.WorkWeekView.Styles.AllDayAreaHeight.ToString().IndexOf("p")));
            alturasomada -= 24;
            ASPxScheduler1.TimelineView.Styles.TimelineCellBody.Height = new Unit(alturasomada + "px");

            /*proporção de 5 celulas em altura = celula+ cabecalho*/
            ASPxScheduler1.MonthView.Styles.DateCellBody.Height = new Unit((alturasomada / 5) - 19 + "px");

            /*proporção de 4 celulas em altura = celula + cabeçalho*/
            ASPxScheduler1.WeekView.Styles.DateCellBody.Height = new Unit((alturasomada / 4) - 13 + "px");

        }
    }

    public void carregaAgenda()
    {
        //dset = cDados.getAgenda("", codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel);

        dset = cDados.getDataSet(string.Format(@"exec p_GetEventosAgendaInstitucional {0}", codigoEntidadeUsuarioResponsavel));

        ASPxScheduler1.AppointmentDataSource = dset;


        //dset = cDados.getFornecedores("", 0);

        //ASPxScheduler1.ResourceDataSource = dset;
        try
        {
            ASPxScheduler1.DataBind();
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
            diaTodo = ((bool)e.NewValues["DiaInteiro"] ? 1 : 0);
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
        if (e.NewValues["CodigoCompromissoUsuario"] != null)
        {
            ID = int.Parse(e.NewValues["CodigoCompromissoUsuario"].ToString());
        }

        insereNovo(tipo, inicio, termino, diaTodo, status, categoria, descricao, local, informacaoRecorrente, informacaoLembrete, IDRecurso, assunto, ID);
        //carregaAgenda();
        e.Cancel = true;
    }
    protected void ASPxScheduler1_ActiveViewChanging(object sender, DevExpress.Web.ASPxScheduler.ActiveViewChangingEventArgs e)
    {
        carregaAgenda();
    }

    public void insereNovo(int tipo, string inicio, string termino, int diaTodo, int status, int categoria, string descricao, string local, string informacaoRecorrente, string informacaoLembrete, int IDRecurso, string assunto, int ID)
    {
        cDados.incluiAgenda(tipo, inicio, termino, diaTodo, status, categoria, descricao, local, informacaoRecorrente, informacaoLembrete, IDRecurso, assunto, ID, codigoUsuarioResponsavel, ref registrosAfetados, codigoEntidadeUsuarioResponsavel);
    }

    protected void ASPxScheduler1_AppointmentRowUpdating(object sender, ASPxSchedulerDataUpdatingEventArgs e)
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
            diaTodo = ((bool)e.NewValues["DiaInteiro"] ? 1 : 0);
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
        if (e.NewValues["CodigoCompromissoUsuario"] != null)
        {
            ID = int.Parse(e.NewValues["CodigoCompromissoUsuario"].ToString());
        }

        if (e.Keys.Count > 0 && e.Keys[0] != null)
            alteraDado(tipo, inicio, termino, diaTodo, status, categoria, descricao, local, informacaoRecorrente, informacaoLembrete, IDRecurso, assunto, int.Parse(e.Keys[0].ToString()), ID);
        //carregaAgenda();
        e.Cancel = true;
    }

    public void alteraDado(int tipo, string inicio, string termino, int diaTodo, int status, int categoria, string descricao, string local, string informacaoRecorrente, string informacaoLembrete, int IDRecurso, string assunto, int codigoAgenda, int ID)
    {
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
        e.Container.Caption = "Cadastro de Eventos";
    }

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        //carregaAgenda();
        //ASPxScheduler1.DayView.VisibleTime.Start.Negate();
        if (cbHorarioTrabalho.Checked)
        {
            ASPxScheduler1.DayView.VisibleTime.Start = new TimeSpan(08, 00, 00);
            ASPxScheduler1.DayView.VisibleTime.End = new TimeSpan(19, 00, 00);
            ASPxScheduler1.WorkWeekView.VisibleTime.Start = new TimeSpan(08, 00, 00);
            ASPxScheduler1.WorkWeekView.VisibleTime.End = new TimeSpan(19, 00, 00);

        }
        else
        {
            ASPxScheduler1.DayView.VisibleTime.Start = new TimeSpan(00, 00, 00);
            ASPxScheduler1.DayView.VisibleTime.End = new TimeSpan(23, 59, 59);
            ASPxScheduler1.WorkWeekView.VisibleTime.Start = new TimeSpan(00, 00, 00);
            ASPxScheduler1.WorkWeekView.VisibleTime.End = new TimeSpan(23, 59, 59);
        }

    }
    protected void ASPxScheduler1_InitAppointmentDisplayText(object sender, DevExpress.XtraScheduler.AppointmentDisplayTextEventArgs e)
    {

    }
    protected void ASPxScheduler1_PreparePopupMenu(object sender, DevExpress.Web.ASPxScheduler.PopupMenuShowingEventArgs e)
    {
        ASPxSchedulerPopupMenu menu = e.Menu;
        if (menu.MenuId.Equals(SchedulerMenuItemId.AppointmentMenu))
        {
            menu.Items.RemoveAll(mi => mi.Name != "OpenAppointment");
            /*DevExpress.Web.MenuItem item = new DevExpress.Web.MenuItem("Exportar", "ExportAppointment");
            e.Menu.Items.Insert(1, item);
            e.Menu.ClientSideEvents.ItemClick = "function(s, e) { OnMenuClick(s,e); }";*/
        }

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

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        string comandoSql = string.Format(@"
DECLARE @CodigoAcao INT, 
        @Inicio DateTime, 
        @Termino DateTime
    SET @CodigoAcao = {0}
    SET @Inicio = '{1}'
    SET @Termino = '{2}'

 UPDATE [tai02_AcoesIniciativa]
    SET [Inicio] = @Inicio
       ,[Termino] = @Termino
  WHERE [CodigoAcao] = @CodigoAcao "
            , codigoAcao
            , deInicio.Date.ToString("dd/MM/yyyy HH:mm")
            , deTermino.Date.ToString("dd/MM/yyyy HH:mm"));
        int registrosAfetados = 0;
        cDados.execSQL(comandoSql, ref registrosAfetados);
    }
}
