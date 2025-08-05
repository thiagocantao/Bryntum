<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="frameEspacoTrabalho_Agenda.aspx.cs"
    Inherits="espacoTrabalho_frameEspacoTrabalho_Agenda" Title="Portal da Estratégia" ResponseEncoding="utf-8" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left" style="height: 26px">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center" style="width: 1px; height: 19px;">
                            <span id="spanBotoes" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTituloTela"
                                Font-Bold="True" Text="<%# Resources.traducao.frameEspacoTrabalho_Agenda_agenda %>">
                            </dxe:ASPxLabel>
                            <dxcp:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                            </dxcp:ASPxHiddenField>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tbody>
            <tr>
                <td id="ConteudoPrincipal">
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxCheckBox ID="cbHorarioTrabalho" runat="server" ClientInstanceName="cbHorarioTrabalho"
                                    Text="Horário de Trabalho"
                                    Checked="True" Width="172px" CheckState="Checked">
                                    <ClientSideEvents CheckedChanged="function(s, e) {
calendarioAgenda.PerformCallback();
}" />
                                </dxe:ASPxCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxwschs:ASPxScheduler ID="calendarioAgenda" runat="server" OnActiveViewChanging="ASPxScheduler1_ActiveViewChanging"
                                    OnAppointmentRowInserting="ASPxScheduler1_AppointmentRowInserting" OnAppointmentRowDeleting="ASPxScheduler1_AppointmentRowDeleting"
                                    OnAppointmentRowUpdating="ASPxScheduler1_AppointmentRowUpdating"
                                    Start="2010-09-12"
                                    ActiveViewType="Timeline" GroupType="Date"
                                    OnPopupMenuShowing="ASPxScheduler1_PopupMenuShowing"
                                    ClientInstanceName="calendarioAgenda"
                                    OnCustomErrorText="ASPxScheduler1_CustomErrorText"
                                    OnCustomCallback="ASPxScheduler1_CustomCallback" OnAppointmentFormShowing="ASPxScheduler1_AppointmentFormShowing" ClientIDMode="AutoID">
                                    <ClientSideEvents EndCallback="function(s, e){if(s.cp_recarregar == 'S'){s.cp_recarregar='N';s.Refresh();}}" />
                                    <FloatingActionButton TextVisibilityMode="Hidden">
                                    </FloatingActionButton>
                                    <Storage EnableReminders="False">
                                        <Appointments ResourceSharing="True">
                                            <Mappings AllDay="DiaInteiro" AppointmentId="CodigoCompromissoUsuario" Description="Anotacoes"
                                                End="DataTermino" Label="Rotulo" Location="Local" RecurrenceInfo="DescricaoRecorrencia"
                                                ReminderInfo="DescricaoAlerta" ResourceId="CodigoUsuario" Start="DataInicio"
                                                Status="Status" Subject="Assunto" Type="TipoEvento" />
                                            <CustomFieldMappings>
                                                <dxwschs:ASPxAppointmentCustomFieldMapping Member="Tipo" Name="Tipo" ValueType="String" />
                                                <dxwschs:ASPxAppointmentCustomFieldMapping Member="HorarioInicio" Name="HorarioInicio" ValueType="String" />
                                                <dxwschs:ASPxAppointmentCustomFieldMapping Member="HorarioTermino" Name="HorarioTermino" ValueType="String" />
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
                                            <dxwschs:AppointmentLabel Color="168, 213, 255" DisplayName="Neg&#243;cios" MenuCaption="&amp;Neg&#243;cios">
                                            </dxwschs:AppointmentLabel>
                                            <dxwschs:AppointmentLabel Color="193, 244, 156" DisplayName="Pessoal" MenuCaption="&amp;Pessoal">
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
                                        </labels>
                                        </Appointments>
                                    </Storage>
                                    <Views>
                                        <DayView ShortDisplayName="Dia">
                                            <WorkTime Start="08:00:00" End="1.18:00:00" />
                                           <AppointmentDisplayOptions StartTimeVisibility="Always" />
                                            <DayViewStyles ScrollAreaHeight="180px">
                                                <AllDayArea>
                                                </AllDayArea>
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
                                                <dxschsc2:TimeSlot DisplayName="60 Minutos" MenuCaption="6&amp;0 Minutos" Value="01:00:00"></dxschsc2:TimeSlot>
                                                <dxschsc2:TimeSlot DisplayName="30 Minutos" MenuCaption="&amp;30 Minutos" Value="00:30:00"></dxschsc2:TimeSlot>
                                            </TimeSlots>
                                        </DayView>
                                        <WorkWeekView ShortDisplayName="Semana de Trabalho" MenuCaption="Vis&#227;o de Trabalho Semanal" ShowAllAppointmentsAtTimeCells="True">
                                            <WorkWeekViewStyles ScrollAreaHeight="280px">
                                                <Appointment>
                                                </Appointment>
                                                <TimeCellBody Font-Bold="False">
                                                </TimeCellBody>
                                            </WorkWeekViewStyles>
                                            <TimeSlots>
                                                <dxschsc2:TimeSlot DisplayName="60 Minutes" MenuCaption="6&amp;0 Minutes" Value="01:00:00"></dxschsc2:TimeSlot>
                                                <dxschsc2:TimeSlot DisplayName="30 Minutes" MenuCaption="&amp;30 Minutes" Value="00:30:00"></dxschsc2:TimeSlot>
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
                                        <FullWeekView>
                                        </FullWeekView>
                                    </Views>
                                    <OptionsBehavior ShowRemindersForm="True" ClientTimeZoneId="E. South America Standard Time" />
                                    <ClientSideEvents MouseUp=""></ClientSideEvents>
                                    <OptionsLoadingPanel Text="Carregando&amp;hellip;" />
                                    <ResourceNavigator Visibility="Never" />
                                    <OptionsView NavigationButtons-NextCaption="Pr&#243;ximo" NavigationButtons-PrevCaption="Anterior" />
                                </dxwschs:ASPxScheduler>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
