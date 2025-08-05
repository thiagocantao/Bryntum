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

public partial class espacoTrabalho_frameEspacoTrabalho_AgendaInstitucional : System.Web.UI.Page
{
    DataSet dset = new DataSet();
    dados cDados;

    int registrosAfetados = 0;
    //int codigoUsuario = 0;
    public Unit alturaArea = 0;
    public string origemChamada = "";

    int codigoUsuarioResponsavel;
    int codigoEntidadeUsuarioResponsavel;

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

        calendarioAgenda.OptionsForms.AppointmentFormTemplateUrl = "../DevExpress_Trad/ASPxSchedulerForms/AgendaInstitucional_AppointmentForm.pt-BR.ascx";
        calendarioAgenda.OptionsForms.RecurrentAppointmentDeleteFormTemplateUrl = "../DevExpress_Trad/ASPxSchedulerForms/RecurrentAppointmentDeleteForm.ascx";

    }


    protected void Page_Load(object sender, EventArgs e)
    {
        carregaComboUsuarios();
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/Agenda.js""></script>"));
        origemChamada = Request.QueryString["OrigemChamada"] != null ? Request.QueryString["OrigemChamada"].ToString() : "";
        /* calendario lista */
        carregaAgenda();

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
        //ASPxScheduler1.WorkWeekView.AppointmentDisplayOptions.AppointmentHeight =400;

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
        dset = cDados.getAgendaIndividual("", -1, codigoEntidadeUsuarioResponsavel,
                    true, false, false, false, false);


        ////Se for agenda da CDIS passa 2 para a função de busca
        //int param = origemChamada == "CDIS" ? 2 : 1;
        //dset = cDados.getAgendaIndividual("", -1,codigoEntidadeUsuarioResponsavel,
        //    param, 0, 0, 0, 0);



        calendarioAgenda.AppointmentDataSource = dset;
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
        cDados.incluiAgendaInstitucional(tipo, inicio, termino, diaTodo, status, categoria, descricao, local, informacaoRecorrente, informacaoLembrete, IDRecurso, assunto, ID, codigoUsuarioResponsavel, ref registrosAfetados, codigoEntidadeUsuarioResponsavel);
        geraEmailAgendaInstitucional("inserido", tipo, inicio, termino, diaTodo, status, categoria, descricao, local, informacaoRecorrente, informacaoLembrete, IDRecurso, assunto, ID);
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
            IDRecurso = int.Parse(e.OldValues["CodigoUsuario"].ToString());
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

        if (origemChamada == "CDIS" && codigoUsuarioResponsavel != IDRecurso)
            throw new Exception("Não é possível alterar compromissos de agenda corporativa criados por outro usuário.");

        if (e.Keys.Count > 0 && e.Keys[0] != null)
            alteraDado(tipo, inicio, termino, diaTodo, status, categoria, descricao, local, informacaoRecorrente, informacaoLembrete, IDRecurso, assunto, int.Parse(e.Keys[0].ToString()), ID);
        //carregaAgenda();
        e.Cancel = true;
    }

    public void alteraDado(int tipo, string inicio, string termino, int diaTodo, int status, int categoria, string descricao, string local, string informacaoRecorrente, string informacaoLembrete, int IDRecurso, string assunto, int codigoAgenda, int ID)
    {
        assunto = origemChamada == "CDIS" ? assunto.Substring(24, assunto.Length - assunto.IndexOf('¤') - 1) : assunto;
        cDados.atualizaAgendaInstitucional(tipo, inicio, termino, diaTodo, status, categoria, descricao, local, informacaoRecorrente, informacaoLembrete, IDRecurso, assunto, ID, codigoUsuarioResponsavel, codigoAgenda, ref registrosAfetados, codigoEntidadeUsuarioResponsavel);
        geraEmailAgendaInstitucional("alterado", tipo, inicio, termino, diaTodo, status, categoria, descricao, local, informacaoRecorrente, informacaoLembrete, IDRecurso, assunto, ID);
    }

    protected void ASPxScheduler1_AppointmentRowDeleting(object sender, ASPxSchedulerDataDeletingEventArgs e)
    {
        int IDRecurso = (int)e.Values["CodigoUsuario"];
        if (origemChamada == "CDIS" && codigoUsuarioResponsavel != IDRecurso)
            throw new Exception("Não é possível excluir compromissos de agenda corporativa criados por outro usuário.");

        cDados.excluiAgendaInstitucional(int.Parse(e.Keys[0].ToString()), ref registrosAfetados);
        e.Cancel = true;
        carregaAgenda();
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
            menu.Items.RemoveAll(mi => mi.Name == "LabelSubMenu");
            DevExpress.Web.MenuItem item = new DevExpress.Web.MenuItem("Exportar", "ExportAppointment");
            e.Menu.Items.Insert(1, item);
            e.Menu.ClientSideEvents.ItemClick = "function(s, e) { OnMenuClick(s,e); }";
        }
        else if (menu.MenuId.Equals(SchedulerMenuItemId.DefaultMenu))
        {
            string[] opcoes = { "NewAllDayEvent", "NewRecurringAppointment", "NewRecurringEvent" };
            menu.Items.RemoveAll(mi => opcoes.Contains(mi.Name));
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

    private void geraEmailAgendaInstitucional(string incluiaAltera, int tipo, string inicio, string termino, int diaTodo, int status, int categoria, string descricao, string local, string informacaoRecorrente, string informacaoLembrete, int IDRecurso, string assunto, int ID)
    {
        string emailDestino = "suporte@cdis.com.br";
        string nomeUsuario = "amauri";
        string assuntoEmail = "Compromisso agenda corporativa " + nomeUsuario;
        string textoPadraoSistema = "";
        textoPadraoSistema = string.Format(
                            @"<fieldset><p></p><p>Atenção! <br />
                               Um novo compromisso corporativo foi {0}.
                        <table>
                               <tr> 
                                  <td> <b>Usuário :</b> </td> <td>{1}</td>                
                               </tr>
                               <tr> 
                                  <td><b>Incio :</b></td> <td>{2}</td> 
                               </tr>
                               <tr> 
                                  <td><b>Término :</b></td> <td>{3}</td> 
                               </tr>
                               <tr>
                                  <td><b>Assunto :</b></td><td>{4}</td></tr>
                               <tr>
                               <tr>
                                  <td><b>Descrição :</b></td><td>{5}</td></tr>
                               <tr>
                                  <td><b>Sua senha não foi alterada.</b></td><td></td>
                               </tr>
                        </table><p></p><p>Att.,<br />Administração do Sistema</p><p></p><p><b>
                        PS: Por favor, não responda esse e-mail.</b></p><p></p></fieldset>", incluiaAltera, nomeUsuario, inicio, termino, assunto, descricao);

        int retornoStatus = 0;

        string emailEnviado = cDados.enviarEmail(assuntoEmail, emailDestino, "", textoPadraoSistema, "", "", ref retornoStatus);
    }
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
