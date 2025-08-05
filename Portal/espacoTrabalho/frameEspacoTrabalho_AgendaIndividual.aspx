<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="frameEspacoTrabalho_AgendaIndividual.aspx.cs" Inherits="espacoTrabalho_frameEspacoTrabalho_AgendaIndividual"
    Title="Portal da Estratégia" ResponseEncoding="utf-8" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
   <div>
        <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
            width: 100%">
            <tr style="height: 26px">
                <td valign="middle">
                    &nbsp; &nbsp;<dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTituloTela"
                        Font-Bold="True" Text="Agenda">
                    </dxe:ASPxLabel>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; padding-left: 10px;
            padding-right: 10px">
            <tr>
                <td style="width: 150px" align="left">
                    <dxe:ASPxCheckBox ID="cbHorarioTrabalho" runat="server" ClientInstanceName="cbHorarioTrabalho"
                        Text="Horário de Trabalho" Checked="True"
                        Width="100%">
                        <ClientSideEvents CheckedChanged="function(s, e) {
	calendarioAgenda.PerformCallback();
}" />
                    </dxe:ASPxCheckBox>
                </td>
                <td align="left" style="width: 240px" valign="middle">
                    <dxcp:ASPxComboBox runat="server" ValueType="System.Int32" DataSourceID="sdsUsuario" 
                        TextField="NomeUsuario" ValueField="CodigoUsuario" Width="100%" 
                        ClientInstanceName="ddlUsuario" 
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
                                <dxe:ASPxCheckBox ID="cbEventosCorporativos" runat="server" ClientInstanceName="cbEventosCorporativos"
                                    Text="Eventos Corporativos" Checked="True"
                                    CheckState="Checked">
                                    <ClientSideEvents CheckedChanged="function(s, e) {
	calendarioAgenda.PerformCallback();
}" />
                                </dxe:ASPxCheckBox>
                            </td>
                            <td>
                                <dxe:ASPxCheckBox ID="cbAtividadesProjAnalise" runat="server" ClientInstanceName="cbAtividadesProjAnalise"
                                    Text="Atividades de Proj em Análise" Checked="True">
                                    <ClientSideEvents CheckedChanged="function(s, e) {
	calendarioAgenda.PerformCallback();
}" />
                                </dxe:ASPxCheckBox>
                            </td>
                            <td>
                                <dxe:ASPxCheckBox ID="cbAtividadesProjAprovados" runat="server" ClientInstanceName="cbAtividadesProjAprovados"
                                    Text="Atividades de Proj Aprovados" Checked="True">
                                    <ClientSideEvents CheckedChanged="function(s, e) {
	calendarioAgenda.PerformCallback();
}" />
                                </dxe:ASPxCheckBox>
                            </td>
                            <td>
                                <dxe:ASPxCheckBox ID="cbMeusCompromissos" runat="server" ClientInstanceName="cbMeusCompromissos"
                                    Text="Meus Compromissos" Checked="True">
                                    <ClientSideEvents CheckedChanged="function(s, e) {
	calendarioAgenda.PerformCallback();
}" />
                                </dxe:ASPxCheckBox>
                            </td>
                           <td>
                              <asp:ImageButton ID="imgExportaParaPDF" runat="server" 
                                    ImageUrl="~/imagens/botoes/btnPdf.png" OnClick="ImageButton1_Click" 
                                    AlternateText="Imprimir Painel" ToolTip="Imprimir painel" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="position: relative; padding-left: 10px; padding-right: 10px">
            
            
            
            <dxwschs:ASPxScheduler ID="calendarioAgenda" runat="server" OnActiveViewChanging="ASPxScheduler1_ActiveViewChanging"
                OnAppointmentRowInserting="ASPxScheduler1_AppointmentRowInserting" OnAppointmentRowDeleting="ASPxScheduler1_AppointmentRowDeleting"
                OnAppointmentFormShowing="ASPxScheduler1_AppointmentFormShowing" OnAppointmentRowUpdating="ASPxScheduler1_AppointmentRowUpdating"
                Start="2015-04-05" ActiveViewType="Month"
                GroupType="Date" OnPopupMenuShowing="ASPxScheduler1_PreparePopupMenu" ClientInstanceName="calendarioAgenda"
                OnCustomCallback="calendarioAgenda_CustomCallback" OnCustomErrorText="calendarioAgenda_CustomErrorText"
                ClientIDMode="AutoID" OnBeforeExecuteCallbackCommand="calendarioAgenda_BeforeExecuteCallbackCommand">
                <Storage EnableReminders="False">
                    <Appointments ResourceSharing="True">
                        <Mappings AllDay="DiaInteiro" AppointmentId="CodigoEvento" Description="Anotacoes"
                            End="termino" Label="ROTULO" Location="Local" RecurrenceInfo="DescricaoRecorrencia"
                            ReminderInfo="DescricaoAlerta" ResourceId="CodigoUsuario" Start="Inicio" Status="STATUS"
                            Subject="Assunto" Type="TipoEvento" />
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
                    <DayView ShortDisplayName="Dia" DisplayName="Calendário Dia" MenuCaption="Visualização &amp;Dia"
                        TimeScale="00:00:05">
                        <WorkTime Start="08:00:00" End="1.18:00:00" />
                        <TimeRulers>
                            <dxschsc2:TimeRuler ShowMinutes="True"></dxschsc2:TimeRuler>
                        </TimeRulers>
                        <AppointmentDisplayOptions StartTimeVisibility="Always" AppointmentAutoHeight="true"
                            SnapToCellsMode="Disabled" />
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
                            <dxschsc2:TimeSlot Value="00:00:15" DisplayName="15 Minutos" MenuCaption="1&amp;5 Minutos">
                            </dxschsc2:TimeSlot>
                            <dxschsc2:TimeSlot Value="00:00:05" DisplayName="5 Minutos" MenuCaption="5 &amp;Minutos">
                            </dxschsc2:TimeSlot>
                        </TimeSlots>
                    </DayView>
                    <WorkWeekView ShortDisplayName="Semana de Trabalho" MenuCaption="Vis&#227;o de Trabalho Semanal"
                        ShowAllAppointmentsAtTimeCells="True" DisplayName="Semana de Trabalho">
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
                            <dxschsc2:TimeSlot Value="00:00:15" DisplayName="15 Minutes" MenuCaption="6&amp;0 Minutes">
                            </dxschsc2:TimeSlot>
                        </TimeSlots>
                        <AppointmentDisplayOptions SnapToCellsMode="Never" />
                        <Templates>
                            <VerticalAppointmentTemplate>
                                <va:VerticalAppointment ID="VerticalAppointment1" runat="server" />
                            </VerticalAppointmentTemplate>
                        </Templates>
                    </WorkWeekView>
                    <WeekView ShortDisplayName="Semana" MenuCaption="&amp;Vis&#227;o Semanal">
                    </WeekView>
                    <MonthView ShortDisplayName="M&#234;s" DisplayName="Calend&#225;rio do m&#234;s"
                        CompressWeekend="False" MenuCaption="Visualização &amp;Mês">
                        <MonthViewStyles>
                            <VerticalResourceHeader Width="20px">
                            </VerticalResourceHeader>
                            <DateCellBody>
                            </DateCellBody>
                            <Appointment>
                            </Appointment>
                        </MonthViewStyles>
                        <AppointmentDisplayOptions AppointmentAutoHeight="True" TimeDisplayType="Clock" />
                    </MonthView>
                    <TimelineView ShortDisplayName="Linha de Tempo" MenuCaption="Visão &amp;Linha do Tempo">
                        <Scales>
                            <dxschsc2:TimeScaleYear Enabled="False"></dxschsc2:TimeScaleYear>
                            <dxschsc2:TimeScaleQuarter Enabled="False"></dxschsc2:TimeScaleQuarter>
                            <dxschsc2:TimeScaleMonth Enabled="False"></dxschsc2:TimeScaleMonth>
                            <dxschsc2:TimeScaleWeek MenuCaption="&amp;Semana"></dxschsc2:TimeScaleWeek>
                            <dxschsc2:TimeScaleDay MenuCaption="&amp;Dia"></dxschsc2:TimeScaleDay>
                            <dxschsc2:TimeScaleHour Enabled="False"></dxschsc2:TimeScaleHour>
                            <dxschsc2:TimeScaleFixedInterval Enabled="False"></dxschsc2:TimeScaleFixedInterval>
                        </Scales>
                        <AppointmentDisplayOptions TimeDisplayType="Clock" SnapToCellsMode="Never" />
                    </TimelineView>
                    <FullWeekView TimeScale="00:15:00" DisplayName="Visualização &amp;Semana Completa">
                        <AppointmentDisplayOptions AppointmentAutoHeight="True" SnapToCellsMode="Never" />
                    </FullWeekView>
                </Views>
                <OptionsBehavior ShowRemindersForm="False" ClientTimeZoneId="E. South America Standard Time" />
                <Styles>
                    <InplaceEditor>
                        <Paddings Padding="10px" />
                    </InplaceEditor>
                    <HorizontalResourceHeader>
                        <Paddings PaddingLeft="10px" />
                    </HorizontalResourceHeader>
                </Styles>
                <OptionsCustomization AllowInplaceEditor="None" />
                <ClientSideEvents MouseUp="" AppointmentDoubleClick="function(s, e) {
        s.ShowAppointmentFormByClientId(e.appointmentId);
    }" AppointmentClick="function(s, e) {
            	var scheduler = s;
            	var apt = scheduler.GetAppointmentById(e.appointmentId);
}"></ClientSideEvents>
                <OptionsLoadingPanel Text="Carregando&amp;hellip;" />
                <ResourceNavigator Visibility="Never" />
                <OptionsView NavigationButtons-NextCaption="Pr&#243;ximo" NavigationButtons-PrevCaption="Anterior" />
                <BorderBottom BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" />
            </dxwschs:ASPxScheduler>
        </div>
        <asp:SqlDataSource ID="sdsUsuario" runat="server"></asp:SqlDataSource>
        <dxwschsc:ASPxSchedulerControlPrintAdapter ID="ASPxSchedulerControlPrintAdapter1" 
            runat="server" SchedulerControlID="calendarioAgenda">
        </dxwschsc:ASPxSchedulerControlPrintAdapter>
    </div>
</asp:Content>
