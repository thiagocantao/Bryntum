<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConflitosAgenda_VisualizaAgenda.aspx.cs"
    Inherits="_Projetos_Administracao_ConflitosAgenda_VisualizaAgenda" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<script language="javascript" type="text/javascript">
    function ValidaCamposPreenchidos() {
        var dataInicioValida = deInicio.GetIsValid();
        var dataTerminoValida = deTermino.GetIsValid();
        var periodoValido = deInicio.GetDate() <= deTermino.GetDate();
        return dataInicioValida && dataTerminoValida && periodoValido;
    }
</script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                        width: 100%">
                        <tr>
                            <td valign="middle">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTituloSelecao"
                                                Font-Bold="True" 
                                                ForeColor="Red">
                                            </dxe:ASPxLabel>
                                                                            <dxe:ASPxCheckBox ID="cbHorarioTrabalho" runat="server" ClientInstanceName="cbHorarioTrabalho"
                                    Text="Horário de Trabalho"  Checked="True" Width="172px">
                                    <ClientSideEvents CheckedChanged="function(s, e) {
	pnCallback.PerformCallback();
}" />
                                </dxe:ASPxCheckBox>
                                        </td>
                                        <td style="width: 125px; padding-right: 10px;">
                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Início">
                                            </dxe:ASPxLabel>
                                            <dxe:ASPxDateEdit ID="deInicio" runat="server" 
                                                Width="100%" ClientInstanceName="deInicio" 
                                                DisplayFormatString="{0:dd/MM/yyyy HH:mm}" EditFormat="DateTime">
                                                <ValidationSettings Display="None">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dxe:ASPxDateEdit>
                                        </td>
                                        <td style="width: 125px; padding-right: 10px;">
                                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Término">
                                            </dxe:ASPxLabel>
                                            <dxe:ASPxDateEdit ID="deTermino" runat="server" 
                                                Width="100%" ClientInstanceName="deTermino" 
                                                DisplayFormatString="{0:dd/MM/yyyy HH:mm}" EditFormat="DateTime">
                                                <ValidationSettings Display="None">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </dxe:ASPxDateEdit>
                                        </td>
                                        <td style="width: 70px;">
                                            <dxe:ASPxButton ID="btnSalvar" runat="server" Text="Salvar"
                                                AutoPostBack="False" Width="70px" 
                                                onclick="btnSalvar_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if(ValidaCamposPreenchidos())
		window.parent.recarregar = true;
	else{
		e.processOnServer = false;
		window.top.mostraMensagem(&quot;Os campos 'Início' e 'Término' devem ser informados e a data de término deve ser maior ou igual a data de início.&quot;, 'atencao', true, false, null);
	}
}" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="width: 20px">
                            </td>
                            <td>
                                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                                    OnCallback="pnCallback_Callback">
                                    <PanelCollection>
                                        <dxp:PanelContent ID="PanelContent1" runat="server">
                                            <dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" OnActiveViewChanging="ASPxScheduler1_ActiveViewChanging"
                                                OnAppointmentRowInserting="ASPxScheduler1_AppointmentRowInserting" OnAppointmentRowDeleting="ASPxScheduler1_AppointmentRowDeleting"
                                                OnAppointmentRowUpdating="ASPxScheduler1_AppointmentRowUpdating" Start="2012-07-29"
                                                 ActiveViewType="Month" GroupType="Date"
                                                OnInitAppointmentDisplayText="ASPxScheduler1_InitAppointmentDisplayText"
                                                OnPopupMenuShowing="ASPxScheduler1_PreparePopupMenu" 
                                                ClientInstanceName="scheduler" >
                                                <Storage EnableReminders="False">
                                                    <Appointments ResourceSharing="True">
                                                        <Mappings AllDay="DiaInteiro" AppointmentId="CodigoAcao" Description="Anotacoes"
                                                            End="termino" Label="ROTULO" Location="Local" RecurrenceInfo="DescricaoRecorrencia"
                                                            ReminderInfo="DescricaoAlerta" ResourceId="CodigoUsuario" Start="Inicio"
                                                            Status="STATUS" Subject="Assunto" Type="TipoEvento" />
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
                                           <AppointmentDisplayOptions StartTimeVisibility="Always" />
                                            <DayViewStyles ScrollAreaHeight="180px">
                                                <AllDayArea >
                                                </AllDayArea>
                                                <AppointmentHorizontalSeparator >
                                                </AppointmentHorizontalSeparator>
                                                <BottomMoreButton >
                                                </BottomMoreButton>
                                                <AlternateDateHeader >
                                                </AlternateDateHeader>
                                                <Appointment >
                                                </Appointment>
                                                <DateHeader >
                                                </DateHeader>
                                                <VerticalResourceHeader >
                                                </VerticalResourceHeader>
                                            </DayViewStyles>
                                            
                                            <TimeSlots>
                                                <dxschsc2:TimeSlot DisplayName="60 Minutos" MenuCaption="6&amp;0 Minutos" Value="01:00:00">
                                                </dxschsc2:TimeSlot>
                                                <dxschsc2:TimeSlot DisplayName="30 Minutos" MenuCaption="&amp;30 Minutos" Value="00:30:00">
                                                </dxschsc2:TimeSlot>
                                            </TimeSlots>
                                        </DayView>
                                        <WorkWeekView ShortDisplayName="Semana de Trabalho" MenuCaption="Vis&#227;o de Trabalho Semanal" ShowAllAppointmentsAtTimeCells="True">
                                            <WorkWeekViewStyles ScrollAreaHeight="280px">
                                                <Appointment >
                                                </Appointment>
                                                <TimeCellBody Font-Bold="False" >
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
                                                        <va:VerticalAppointment ID="VerticalAppointment1" runat="server"/>
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
                                                <DateCellBody >
                                                </DateCellBody>
                                                <Appointment >
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
                                                <ClientSideEvents MouseUp="" AppointmentDoubleClick="function(s, e) {
        s.ShowAppointmentFormByClientId(e.appointmentId);
    }" />
                                                <OptionsLoadingPanel Text="Carregando&amp;hellip;" />
                                                <ResourceNavigator Visibility="Never" />
                                                <OptionsCustomization AllowAppointmentCopy="None" AllowAppointmentCreate="None" 
                                                    AllowAppointmentDelete="None" AllowAppointmentDrag="None" 
                                                    AllowAppointmentDragBetweenResources="None" AllowAppointmentEdit="Custom"
                                                    AllowAppointmentResize="None" AllowInplaceEditor="None" />
                                                <OptionsView NavigationButtons-NextCaption="Pr&#243;ximo" NavigationButtons-PrevCaption="Anterior" />
                                            </dxwschs:ASPxScheduler>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dxcp:ASPxCallbackPanel>
                            </td>
                            <td style="width: 20px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20px; height: 14px;">
                                &nbsp;
                            </td>
                            <td style="padding-top: 5px;" align="center">
                                <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False"
                                    Text="Fechar">
                                    <ClientSideEvents Click="function(s, e) {
	window.parent.pcModal.Hide();
}" />
                                </dxe:ASPxButton>
                            </td>
                            <td style="width: 20px; height: 14px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
