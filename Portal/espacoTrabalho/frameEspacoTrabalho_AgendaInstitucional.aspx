<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="frameEspacoTrabalho_AgendaInstitucional.aspx.cs" Inherits="espacoTrabalho_frameEspacoTrabalho_AgendaInstitucional"
    Title="Portal da Estratégia" ResponseEncoding="utf-8" %>

<%@ Register Src="~/DevExpress_Trad/ASPxSchedulerForms/VerticalAppointmentTemplate.ascx"
    TagName="VerticalAppointment" TagPrefix="demo" %>
<%@ Register Src="~/DevExpress_Trad/ASPxSchedulerForms/HorizontalAppointmentTemplate.ascx"
    TagName="HorizontalAppointment" TagPrefix="demo" %>
<%@ Register Src="~/DevExpress_Trad/ASPxSchedulerForms/HorizontalSameDayAppointmentTemplate.ascx"
    TagName="HorizontalSameDayAppointment" TagPrefix="demo" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
        width: 100%">
        <tr style="height: 26px">
            <td valign="middle" style="padding-left: 10px">
                <dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTituloTela"
                    Font-Bold="True" Text="Agenda Institucional">
                </dxe:ASPxLabel>
                
            </td>
        </tr>
    </table>
    
    <table>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; padding-left: 10px;
        padding-right: 10px">
        <tr>
            <td style="width: 150px" align="left">
                <dxe:ASPxCheckBox ID="cbHorarioTrabalho" runat="server" ClientInstanceName="cbHorarioTrabalho"
                    Text="Horário de Trabalho" Checked="True"
                    Width="172px">
                    <ClientSideEvents CheckedChanged="function(s, e) {
	pnCallback.PerformCallback();
}" />
                </dxe:ASPxCheckBox>
            </td>
            <td align="left" style="width: 240px" valign="middle">
                <dxcp:ASPxComboBox runat="server" ValueType="System.Int32" DataSourceID="sdsUsuario"
                    TextField="NomeUsuario" ValueField="CodigoUsuario" Width="100%" ClientInstanceName="ddlUsuario"
                    ID="ddlUsuario" NullText="Usuarios">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
    calendarioAgenda.PerformCallback();	
}" />
                </dxcp:ASPxComboBox>
            </td>
            <td align="right">
                <table>
                    <tr>
                        <td>
                            <dxe:ASPxCheckBox ID="cbReunioes" runat="server" ClientInstanceName="cbReunioes"
                                Text="Reuniões" Checked="True" CheckState="Checked">
                                <ClientSideEvents CheckedChanged="function(s, e) {
	calendarioAgenda.PerformCallback();
}" />
                            </dxe:ASPxCheckBox>
                        </td>
                        <td>
                            <dxe:ASPxCheckBox ID="cbTarefasCronograma" runat="server" ClientInstanceName="cbTarefasCronograma"
                                Text="Tarefas - Cronograma" Checked="True"
                                CheckState="Checked">
                                <ClientSideEvents CheckedChanged="function(s, e) {
	calendarioAgenda.PerformCallback();
}" />
                            </dxe:ASPxCheckBox>
                        </td>
                        <td>
                            <dxe:ASPxCheckBox ID="cbTarefasToDoList" runat="server" ClientInstanceName="cbTarefasToDoList"
                                Text="Tarefas - To Do List" Checked="True">
                                <ClientSideEvents CheckedChanged="function(s, e) {
	calendarioAgenda.PerformCallback();
}" />
                            </dxe:ASPxCheckBox>
                        </td>
                        <td>
                            <dxe:ASPxCheckBox ID="cbCompPeriodosNaoTrabalhados" runat="server" ClientInstanceName="cbCompPeriodosNaoTrabalhados"
                                Text="Compromissos e períodos não trabalhados"
                                Checked="True">
                                <ClientSideEvents CheckedChanged="function(s, e) {
	calendarioAgenda.PerformCallback();
}" />
                            </dxe:ASPxCheckBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:ImageButton ID="imgExportaParaPDF" runat="server" ImageUrl="~/imagens/botoes/btnPdf.png"
                                OnClick="ImageButton1_Click" AlternateText="Imprimir Painel" ToolTip="Imprimir painel" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table></td>
                    </tr>
                </table>
    
    
    
    <dxwschs:ASPxScheduler ID="calendarioAgenda" runat="server" OnActiveViewChanging="ASPxScheduler1_ActiveViewChanging"
        OnAppointmentRowInserting="ASPxScheduler1_AppointmentRowInserting" OnAppointmentRowDeleting="ASPxScheduler1_AppointmentRowDeleting"
        OnAppointmentRowUpdating="ASPxScheduler1_AppointmentRowUpdating" Start="2012-07-29"
        ActiveViewType="Month" 
        GroupType="Date" OnPopupMenuShowing="ASPxScheduler1_PreparePopupMenu"
        ClientInstanceName="calendarioAgenda" 
        OnCustomCallback="calendarioAgenda_CustomCallback">
        <Storage EnableReminders="False">
            <Appointments ResourceSharing="True">
                <Mappings AllDay="DiaInteiro" AppointmentId="CodigoEvento" Description="Anotacoes"
                    End="termino" Label="ROTULO" Location="Local" RecurrenceInfo="DescricaoRecorrencia"
                    ReminderInfo="DescricaoAlerta" ResourceId="CodigoUsuario" Start="Inicio" Status="STATUS"
                    Subject="Assunto" Type="TipoEvento" />
                <CustomFieldMappings>
                    <dxwschs:ASPxAppointmentCustomFieldMapping Member="Tipo" Name="Tipo" ValueType="String" />
                </CustomFieldMappings>
                <statuses>
                                                <dxwschs:AppointmentStatus DisplayName="Livre" MenuCaption="&amp;Livre" 
                                                    Type="Free" />
                                                <dxwschs:AppointmentStatus Color="99, 198, 76" DisplayName="Experimental" 
                                                    MenuCaption="&amp;Experimental" Type="Tentative" />
                                                <dxwschs:AppointmentStatus Color="74, 135, 226" DisplayName="Ocupado" 
                                                    MenuCaption="&amp;Ocupado" Type="Busy" />
                                                <dxwschs:AppointmentStatus Color="217, 83, 83" DisplayName="Fora de Serviço" 
                                                    MenuCaption="&amp;Fora de Serviço" Type="OutOfOffice" />
                                            </statuses>
                <labels>
                                                <dxwschs:AppointmentLabel Color="Window" DisplayName="Nenhum" MenuCaption="&amp;Nenhum">
                                                </dxwschs:AppointmentLabel>
                                                <dxwschs:AppointmentLabel Color="255, 194, 190" DisplayName="Importante" MenuCaption="&amp;Importante">
                                                </dxwschs:AppointmentLabel>
                                                <dxwschs:AppointmentLabel Color="180, 135, 213" DisplayName="Neg&#243;cios" MenuCaption="&amp;Neg&#243;cios">
                                                </dxwschs:AppointmentLabel>
                                                <dxwschs:AppointmentLabel Color="244, 244, 156" DisplayName="Pessoal" MenuCaption="&amp;Pessoal">
                                                </dxwschs:AppointmentLabel>
                                                <dxwschs:AppointmentLabel Color="243, 228, 199" DisplayName="Feriado" MenuCaption="&amp;Feriado">
                                                </dxwschs:AppointmentLabel>
                                                <dxwschs:AppointmentLabel Color="244, 206, 147" DisplayName="Deve Comparecer" MenuCaption="&amp;Deve Comparecer">
                                                </dxwschs:AppointmentLabel>
                                                <dxwschs:AppointmentLabel Color="199, 244, 255" DisplayName="Viagem Reservada" MenuCaption="V&amp;iagem Reservada">
                                                </dxwschs:AppointmentLabel>
                                                <dxwschs:AppointmentLabel Color="207, 219, 152" DisplayName="Precisa Prepara&#231;&#227;o"
                                                    MenuCaption="Precisa Pr&amp;epara&#231;&#227;o">
                                                </dxwschs:AppointmentLabel>
                                                <dxwschs:AppointmentLabel Color="224, 207, 233" DisplayName="Anivers&#225;rio" MenuCaption="A&amp;nivers&#225;rio">
                                                </dxwschs:AppointmentLabel>
                                                <dxwschs:AppointmentLabel Color="141, 233, 223" DisplayName="Data Comemorativa" MenuCaption="Data Co&amp;memorativa">
                                                </dxwschs:AppointmentLabel>
                                                <dxwschs:AppointmentLabel Color="255, 247, 165" DisplayName="Telefonar" MenuCaption="&amp;Telefonar">
                                                </dxwschs:AppointmentLabel>
                                                <dxwschs:AppointmentLabel Color="251, 207, 195" DisplayName="Corporativo" MenuCaption="&amp;Corporativo">
                                                </dxwschs:AppointmentLabel>
        <dxwschs:AppointmentLabel Color="168, 213, 255" DisplayName="Proposta de Projeto em An&#225;lise" MenuCaption="&amp;Proposta de Projeto em An&#225;lise">
        </dxwschs:AppointmentLabel>
        <dxwschs:AppointmentLabel Color="193, 244, 156" DisplayName="Proposta de Projeto Aprovado" MenuCaption="&amp;Proposta de Projeto Aprovado">
        </dxwschs:AppointmentLabel>
                                            </labels>
            </Appointments>
        </Storage>
        <Views>
            <DayView ShortDisplayName="Dia">
                <WorkTime Start="08:00:00" End="1.18:00:00" />
                <AppointmentDisplayOptions StartTimeVisibility="Always" AppointmentAutoHeight="true" />
                <DayViewStyles ScrollAreaHeight="180px">
                    <AllDayArea>
                    </AllDayArea>
                    <Appointment Wrap="False">
                    </Appointment>
                    <AppointmentHorizontalSeparator>
                    </AppointmentHorizontalSeparator>
                    <BottomMoreButton>
                    </BottomMoreButton>
                    <AlternateDateHeader>
                    </AlternateDateHeader>
                    <Appointment>
                    </Appointment>
                    <DateHeader>
                    </DateHeader>
                    <VerticalResourceHeader>
                    </VerticalResourceHeader>
                </DayViewStyles>
                <TimeSlots>
                    <dxschsc2:TimeSlot DisplayName="60 Minutos" MenuCaption="6&amp;0 Minutos" Value="01:00:00">
                    </dxschsc2:TimeSlot>
                    <dxschsc2:TimeSlot DisplayName="30 Minutos" MenuCaption="&amp;30 Minutos" Value="00:30:00">
                    </dxschsc2:TimeSlot>
                </TimeSlots>
                <Templates>
                    <VerticalAppointmentTemplate>
                        <demo:VerticalAppointment ID="VerticalAppointment1" runat="server" />
                    </VerticalAppointmentTemplate>
                    <HorizontalAppointmentTemplate>
                        <demo:HorizontalAppointment ID="HorizontalAppointment1" runat="server" />
                    </HorizontalAppointmentTemplate>
                </Templates>
            </DayView>
            <WorkWeekView ShortDisplayName="Semana de Trabalho" MenuCaption="Vis&#227;o de Trabalho Semanal"
                ShowAllAppointmentsAtTimeCells="True">
                <WorkWeekViewStyles ScrollAreaHeight="280px">
                    <Appointment>
                    </Appointment>
                    <TimeCellBody Font-Bold="False">
                    </TimeCellBody>
                </WorkWeekViewStyles>
                <TimeSlots>
                    <dxschsc2:TimeSlot DisplayName="60 Minutes" MenuCaption="6&amp;0 Minutes" Value="01:00:00">
                    </dxschsc2:TimeSlot>
                    <dxschsc2:TimeSlot DisplayName="30 Minutes" MenuCaption="&amp;30 Minutes" Value="00:30:00">
                    </dxschsc2:TimeSlot>
                </TimeSlots>
                <Templates>
                    <VerticalAppointmentTemplate>
                        <va:VerticalAppointment ID="VerticalAppointment1" runat="server" />
                    </VerticalAppointmentTemplate>
                </Templates>
            </WorkWeekView>
            <WeekView ShortDisplayName="Semana" MenuCaption="&amp;Vis&#227;o Semanal">
            </WeekView>
            <MonthView ShortDisplayName="M&#234;s" DisplayName="Calend&#225;rio do m&#234;s"
                CompressWeekend="False">
                <MonthViewStyles>
                    <VerticalResourceHeader Width="20px">
                    </VerticalResourceHeader>
                    <DateCellBody>
                    </DateCellBody>
                    <Appointment>
                    </Appointment>
                </MonthViewStyles>
                <AppointmentDisplayOptions AppointmentAutoHeight="True" />
            </MonthView>
            <TimelineView ShortDisplayName="Linha de Tempo">
                <Scales>
                    <dxschsc2:TimeScaleYear Enabled="False"></dxschsc2:TimeScaleYear>
                    <dxschsc2:TimeScaleQuarter Enabled="False"></dxschsc2:TimeScaleQuarter>
                    <dxschsc2:TimeScaleMonth Enabled="False"></dxschsc2:TimeScaleMonth>
                    <dxschsc2:TimeScaleWeek MenuCaption="&amp;Semana"></dxschsc2:TimeScaleWeek>
                    <dxschsc2:TimeScaleDay MenuCaption="&amp;Dia"></dxschsc2:TimeScaleDay>
                    <dxschsc2:TimeScaleHour Enabled="False"></dxschsc2:TimeScaleHour>
                    <dxschsc2:TimeScaleFixedInterval Enabled="False"></dxschsc2:TimeScaleFixedInterval>
                </Scales>
                <AppointmentDisplayOptions TimeDisplayType="Clock" />
            </TimelineView>
        </Views>
        <OptionsBehavior ShowRemindersForm="False" ClientTimeZoneId="Central Brazilian Standard Time" />
        <OptionsCustomization AllowInplaceEditor="None" />
        <ClientSideEvents MouseUp="" AppointmentDoubleClick="function(s, e) {
        s.ShowAppointmentFormByClientId(e.appointmentId);
    }"></ClientSideEvents>
        <OptionsLoadingPanel Text="Carregando&amp;hellip;" />
        <ResourceNavigator Visibility="Never" />
        <OptionsView NavigationButtons-NextCaption="Pr&#243;ximo" NavigationButtons-PrevCaption="Anterior" />
    </dxwschs:ASPxScheduler>
        <asp:SqlDataSource ID="sdsUsuario" runat="server"></asp:SqlDataSource>
        <dxwschsc:ASPxSchedulerControlPrintAdapter ID="ASPxSchedulerControlPrintAdapter1" 
            runat="server" SchedulerControlID="calendarioAgenda">
        </dxwschsc:ASPxSchedulerControlPrintAdapter>
    </asp:Content>
