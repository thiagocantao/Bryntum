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
using System.Linq;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;

public partial class espacoTrabalho_frameEspacoTrabalho_AgendaIndividual : System.Web.UI.Page
{
   
    DataSet dset = new DataSet();
    dados cDados;

    int registrosAfetados = 0;
    //int codigoUsuario = 0;
    public Unit alturaArea = 0;

    int codigoUsuarioResponsavel;
    int codigoEntidadeUsuarioResponsavel;

    const int INT_Corporativo = 11;
    const int INT_AtividadeProjetoAnalise = 12;
    const int INT_AtividadeProjetoAprovado = 13;
    int[] rotulosBloqueados = { INT_Corporativo, 
                                      INT_AtividadeProjetoAnalise, 
                                      INT_AtividadeProjetoAprovado };


    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); //usuario logado.
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());   //entidad logada.


        sdsUsuario.ConnectionString = cDados.classeDados.getStringConexao();


        this.Title = cDados.getNomeSistema();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        calendarioAgenda.OptionsForms.AppointmentFormTemplateUrl = "../DevExpress_Trad/ASPxSchedulerForms/AgendaIndividual_AppointmentForm.pt-BR.ascx";
        calendarioAgenda.OptionsForms.RecurrentAppointmentDeleteFormTemplateUrl = "../DevExpress_Trad/ASPxSchedulerForms/RecurrentAppointmentDeleteForm.ascx";

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/Agenda.js""></script>"));
    }


    protected void Page_Load(object sender, EventArgs e)
    {
       
        
        carregaComboUsuarios();
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
        }
        defineAlturaTela(cDados.getInfoSistema("ResolucaoCliente").ToString());
        calendarioAgenda.WorkWeekView.AppointmentDisplayOptions.AppointmentHeight = 400;

        cDados.aplicaEstiloVisual(Page);

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "AGENDA", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);
        }
    }

    private void carregaComboUsuarios()
    {
        sdsUsuario.SelectCommand = cDados.getSelect_Usuario(codigoEntidadeUsuarioResponsavel, "");
        sdsUsuario.DataBind();
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela

        if (resolucaoCliente != "")
        {
            int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));

            calendarioAgenda.WorkWeekView.Styles.ScrollAreaHeight = new Unit((alturaPrincipal - 290) - ((alturaPrincipal - 290) * 0.2) + "px");
 
            calendarioAgenda.WorkWeekView.Styles.ScrollAreaHeight = new Unit((alturaPrincipal - 290) - ((alturaPrincipal - 290) * 0.2) + "px");
            calendarioAgenda.WorkWeekView.Styles.AllDayAreaHeight = new Unit((0.2 * (alturaPrincipal - 290)) + "px");

            calendarioAgenda.DayView.Styles.ScrollAreaHeight = new Unit((alturaPrincipal - 290) - ((alturaPrincipal - 290) * 0.2) + "px");
            calendarioAgenda.DayView.Styles.AllDayAreaHeight = new Unit((0.2 * (alturaPrincipal - 290)) + "px");

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
        int codigoUsuario = ddlUsuario.Value != null ? int.Parse(ddlUsuario.Value.ToString()) : codigoUsuarioResponsavel;

        if (codigoUsuario != codigoUsuarioResponsavel)
        {
            calendarioAgenda.OptionsCustomization.AllowAppointmentEdit = UsedAppointmentType.None;
            calendarioAgenda.OptionsCustomization.AllowAppointmentCreate = UsedAppointmentType.None;
            calendarioAgenda.OptionsCustomization.AllowAppointmentDelete = UsedAppointmentType.None;
            calendarioAgenda.OptionsCustomization.AllowAppointmentDrag = UsedAppointmentType.None;
            calendarioAgenda.OptionsCustomization.AllowAppointmentDragBetweenResources = UsedAppointmentType.None;
            calendarioAgenda.OptionsCustomization.AllowAppointmentResize = UsedAppointmentType.None;
            calendarioAgenda.OptionsCustomization.AllowInplaceEditor = UsedAppointmentType.None;


        }
        else
        {
            calendarioAgenda.OptionsCustomization.AllowAppointmentEdit = UsedAppointmentType.All;
            calendarioAgenda.OptionsCustomization.AllowAppointmentCreate = UsedAppointmentType.All;
            calendarioAgenda.OptionsCustomization.AllowAppointmentDelete = UsedAppointmentType.All;
            calendarioAgenda.OptionsCustomization.AllowAppointmentDrag = UsedAppointmentType.All;
            calendarioAgenda.OptionsCustomization.AllowAppointmentDragBetweenResources = UsedAppointmentType.All;
            calendarioAgenda.OptionsCustomization.AllowAppointmentResize = UsedAppointmentType.All;
            calendarioAgenda.OptionsCustomization.AllowInplaceEditor = UsedAppointmentType.All;
        }

        dset = cDados.getAgendaIndividual(string.Empty, codigoUsuario,
     codigoEntidadeUsuarioResponsavel, cbEventosCorporativos.Checked,
     cbAtividadesProjAnalise.Checked, cbAtividadesProjAprovados.Checked,
     cbMeusCompromissos.Checked, cbReunioes.Checked);

        //modelo novo
        //dset = cDados.getAgendaIndividual(string.Empty, codigoUsuarioResponsavel, 
        //    codigoEntidadeUsuarioResponsavel, cbEventosCorporativos.Checked ? 1 : 0,
        //    cbAtividadesProjAnalise.Checked ? 1 : 0, cbAtividadesProjAprovados.Checked ? 1 : 0,
        //    cbMeusCompromissos.Checked ? 1 : 0, cbReunioes.Checked ? 1 : 0);


       calendarioAgenda.AppointmentDataSource = dset;
        try
        {
            calendarioAgenda.DataBind();
        }
        catch { }
    }

    protected void ASPxScheduler1_AppointmentRowInserting(object sender, DevExpress.Web.ASPxScheduler.ASPxSchedulerDataInsertingEventArgs e)
    {

        //calendarioAgenda.appo
        
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
        if (e.NewValues["Inicio"] != null)
        {
            inicio = e.NewValues["Inicio"].ToString();
        }

        //Término
        if (e.NewValues["termino"] != null)
        {
            termino = e.NewValues["termino"].ToString();
        }

        //Dia Todo
        if (e.NewValues["DiaInteiro"] != null)
        {
            diaTodo = ((bool)e.NewValues["DiaInteiro"] ? 1 : 0);
        }

        //status
        if (e.NewValues["STATUS"] != null)
        {
            status = int.Parse(e.NewValues["STATUS"].ToString()); ;
        }

        //Categoria
        if (e.NewValues["ROTULO"] != null)
        {
            categoria = int.Parse(e.NewValues["ROTULO"].ToString()); ;
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
        if (e.NewValues["CodigoEvento"] != null)
        {
            ID = int.Parse(e.NewValues["CodigoEvento"].ToString());
        }
        //Serão bloqueadas as operações de inclusão/alteração/exclusão para os eventos cujo campo RÓTULO seja CORPORATIVO, ATIVIDADE DE PROJETO EM ANÁLISE e ATIVIDADE DE PROJETO APROVADO
        if (rotulosBloqueados.Contains(categoria))
            throw new Exception("Não é possível definir um evento com os rótulos 'Corporativo', 'Proposta de Projeto em Análise' e 'Proposta de Projeto Aprovado'.");

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
        if (e.NewValues["Inicio"] != null)
        {
            inicio = e.NewValues["Inicio"].ToString();
        }

        //Término
        if (e.NewValues["termino"] != null)
        {
            termino = e.NewValues["termino"].ToString();
        }

        //Dia Todo
        if (e.NewValues["DiaInteiro"] != null)
        {
            diaTodo = ((bool)e.NewValues["DiaInteiro"] ? 1 : 0);
        }

        //status
        if (e.NewValues["STATUS"] != null)
        {
            status = int.Parse(e.NewValues["STATUS"].ToString()); ;
        }

        //Categoria
        if (e.NewValues["ROTULO"] != null)
        {
            categoria = int.Parse(e.NewValues["ROTULO"].ToString()); ;
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
        else
        {
            string where = String.Format("WHERE CodigoEvento = {0}", e.Keys[0]);
            //DataSet ds = cDados.getAgendaIndividual(where,
            //                 codigoUsuarioResponsavel,
            //                 codigoEntidadeUsuarioResponsavel,
            //                 cbEventosCorporativos.Checked ? 1 : 0,
            //                 cbAtividadesProjAnalise.Checked ? 1 : 0,
            //                 cbAtividadesProjAprovados.Checked ? 1 : 0,
            //                 cbMeusCompromissos.Checked ? 1 : 0,
            //                 cbReunioes.Checked ? 1 : 0);

            DataSet ds = cDados.getAgendaIndividual(where,
                  codigoUsuarioResponsavel,
                  codigoEntidadeUsuarioResponsavel,
                  cbEventosCorporativos.Checked ? true : false,
                  cbAtividadesProjAnalise.Checked ? true : false,
                  cbAtividadesProjAprovados.Checked ? true : false,
                  cbMeusCompromissos.Checked ? true : false,
                  cbReunioes.Checked ? true : false);
            IDRecurso = Convert.ToInt32(ds.Tables[0].Rows[0]["CodigoUsuario"]);
        }

        //Assunto
        if (e.NewValues["Assunto"] != null)
        {
            assunto = e.NewValues["Assunto"].ToString();
        }

        //ID
        if (e.NewValues["CodigoEvento"] != null)
        {
            ID = int.Parse(e.NewValues["CodigoEvento"].ToString());
        }
        //Os compromissos serão editáveis apenas pelo próprio usuário/
        if (codigoUsuarioResponsavel != IDRecurso)
            throw new Exception("Não é possível alterar compromissos de agenda criados por outro usuário.");
        //Serão bloqueadas as operações de inclusão/alteração/exclusão para os eventos cujo campo RÓTULO seja CORPORATIVO, ATIVIDADE DE PROJETO EM ANÁLISE e ATIVIDADE DE PROJETO APROVADO
        if (rotulosBloqueados.Contains(categoria))
            throw new Exception("Não é possível definir um evento com os rótulos 'Corporativo', 'Proposta de Projeto em Análise' e 'Proposta de Projeto Aprovado'.");

        if (e.Keys.Count > 0 && e.Keys[0] != null)
            alteraDado(tipo, inicio, termino, diaTodo, status, categoria, descricao, local, informacaoRecorrente, informacaoLembrete, IDRecurso, assunto, int.Parse(e.Keys[0].ToString()), ID);
        e.Cancel = true;
        //carregaAgenda();
        
    }

    public void alteraDado(int tipo, string inicio, string termino, int diaTodo, int status, int categoria, string descricao, string local, string informacaoRecorrente, string informacaoLembrete, int IDRecurso, string assunto, int codigoAgenda, int ID)
    {
        cDados.atualizaAgenda(tipo, inicio, termino, diaTodo, status, categoria, descricao, local, informacaoRecorrente, informacaoLembrete, IDRecurso, assunto, ID, codigoUsuarioResponsavel, codigoAgenda, ref registrosAfetados, codigoEntidadeUsuarioResponsavel);
    }

    protected void ASPxScheduler1_AppointmentRowDeleting(object sender, ASPxSchedulerDataDeletingEventArgs e)
    {
        int IDRecurso = (int)e.Values["CodigoUsuario"];
        if (codigoUsuarioResponsavel != IDRecurso)
            throw new Exception("Não é possível excluir compromissos de agenda criados por outro usuário.");
        cDados.excluiAgenda(int.Parse(e.Keys[0].ToString()), ref registrosAfetados);
        e.Cancel = true;
        //carregaAgenda();
    }

    protected void ASPxScheduler1_AppointmentFormShowing(object sender, DevExpress.Web.ASPxScheduler.AppointmentFormEventArgs e)
    {
        e.Container.Caption = "Cadastro de Eventos";
    }

    protected void ASPxScheduler1_PreparePopupMenu(object sender, DevExpress.Web.ASPxScheduler.PopupMenuShowingEventArgs e)
    {
        ASPxSchedulerPopupMenu menu = e.Menu;
        if (menu.MenuId.Equals(SchedulerMenuItemId.AppointmentMenu))
        {
            DevExpress.Web.MenuItem item = new DevExpress.Web.MenuItem("Exportar", "ExportAppointment");
            e.Menu.Items.Insert(1, item);
            e.Menu.ClientSideEvents.ItemClick = "function(s, e) { OnMenuClick(s,e); }";
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
            using (MemoryStream memoryStream = new MemoryStream())
            {
                exporter.Export(memoryStream);
                Stream outputStream = Control.Page.Response.OutputStream;
                memoryStream.WriteTo(outputStream);
                outputStream.Flush();
            }

            Control.Page.Response.ContentType = "text/calendar";
            Control.Page.Response.AddHeader("Content-Disposition", "attachment; filename=appointment.ics");
            Control.Page.Response.End();
        }

    }
    #endregion

    protected void calendarioAgenda_CustomCallback(object sender, DevExpress.Web.CallbackEventArgsBase e)
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
        carregaAgenda();
    }
    protected void calendarioAgenda_CustomErrorText(object handler, ASPxSchedulerCustomErrorTextEventArgs e)
    {
        e.ErrorText = "150,1|" + e.Exception.Message;  
    }
    protected void calendarioAgenda_BeforeExecuteCallbackCommand(object sender, SchedulerCallbackCommandEventArgs e)
    {
        if (e.CommandId == "EXPORTAPT")
        {
            e.Command = new ExportSelectedAppointmentsCallbackCommand((ASPxScheduler)sender, cDados.getPathSistema() + "ArquivosTemporarios/calendario.ics");
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (calendarioAgenda.ActiveView == calendarioAgenda.Views.DayView)
        {
            relAgendaIndividual_DayView relDV = new relAgendaIndividual_DayView();
            relDV.SchedulerAdapter = ASPxSchedulerControlPrintAdapter1.SchedulerAdapter;
            relDV.CreateDocument();
            ExportReport(relDV, "relatorio", "pdf", false);
        }
          if (calendarioAgenda.ActiveView == calendarioAgenda.Views.WorkWeekView)
        {
            relAgendaIndividual_WorkWeekView relWW = new relAgendaIndividual_WorkWeekView();
            relWW.SchedulerAdapter = ASPxSchedulerControlPrintAdapter1.SchedulerAdapter;
            relWW.CreateDocument();
            ExportReport(relWW, "relatorio", "pdf", false);
        }
        if (calendarioAgenda.ActiveView == calendarioAgenda.Views.WeekView)
        {
            relAgendaIndividual_WeekView relWV = new relAgendaIndividual_WeekView();
            relWV.SchedulerAdapter = ASPxSchedulerControlPrintAdapter1.SchedulerAdapter;
            relWV.CreateDocument();
            ExportReport(relWV, "relatorio", "pdf", false);
        }
        if (calendarioAgenda.ActiveView == calendarioAgenda.Views.MonthView)
        {
            relAgendaIndividual_MonthView relMV = new relAgendaIndividual_MonthView();
            relMV.SchedulerAdapter = ASPxSchedulerControlPrintAdapter1.SchedulerAdapter;
            relMV.CreateDocument();
            ExportReport(relMV, "relatorio", "pdf", false);
        }
        if (calendarioAgenda.ActiveView == calendarioAgenda.Views.TimelineView)
        {
            relAgendaIndividual_TimeLineView relTLV = new relAgendaIndividual_TimeLineView();
            relTLV.SchedulerAdapter = ASPxSchedulerControlPrintAdapter1.SchedulerAdapter;
            relTLV.CreateDocument();
            ExportReport(relTLV, "relatorio", "pdf", false);
        }
    }


    public void ExportReport(XtraReport report, string fileName, string fileType, bool inline)
    {
        MemoryStream stream = new MemoryStream();

        Response.Clear();

        if (fileType == "xls")
        {
            XlsExportOptionsEx x = new XlsExportOptionsEx();
            x.ExportType = DevExpress.Export.ExportType.WYSIWYG;
            report.ExportToXls(stream, x);
        }
            
        if (fileType == "pdf")
            report.ExportToPdf(stream);
        if (fileType == "rtf")
            report.ExportToRtf(stream);
        if (fileType == "csv")
            report.ExportToCsv(stream);

        Response.ContentType = "application/" + fileType;
        Response.AddHeader("Accept-Header", stream.Length.ToString());
        Response.AddHeader("Content-Disposition", string.Format("{0}; filename={1}.{2}",
            (inline ? "Inline" : "Attachment"), fileName, fileType));
        Response.AddHeader("Content-Length", stream.Length.ToString());
        //Response.ContentEncoding = System.Text.Encoding.Default;
        Response.BinaryWrite(stream.ToArray());
        Response.End();

    }
}
