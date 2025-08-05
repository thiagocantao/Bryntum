<%--
{************************************************************************************}
{                                                                                    }
{   DO NOT MODIFY THIS FILE!                                                         }
{                                                                                    }
{   It will be overwritten without prompting when a new version becomes              }
{   available. All your changes will be lost.                                        }
{                                                                                    }
{   This file contains the default template and is required for the form             }
{   rendering. Improper modifications may result in incorrect behavior of            }
{   the appointment form.                                                            }
{                                                                                    }
{   In order to create and use your own custom template, perform the following       }
{   steps:                                                                           }
{       1. Save a copy of this file with a different name in another location.       }
{       2. Specify the file location as the 'OptionsForms.AppointmentFormTemplateUrl'}
{          property of the ASPxScheduler control.                                    }
{       3. If you need custom fields to be displayed and processed, you should       }
{          accomplish steps 4-9; otherwise, go to step 10.                           }
{       4. Create a class, derived from the AppointmentFormTemplateContainer,        }
{          containing custom properties. This class definition can be located        }
{          within a class file in the App_Code folder.                               }
{       5. Replace AppointmentFormTemplateContainer references in the template       }
{          page with the name of the class you've created in step 4.                 }
{       6. Handle the AppointmentFormShowing event to create an instance of the      }
{          template container class, defined in step 4, and specify it as the        }
{          destination container instead of the default one.                         }
{       7. Define a class, which inherits from the                                   }
{          DevExpress.Web.ASPxScheduler.Internal.AppointmentFormController.          }
{          This class provides data exchange between the form and the appointment.   }
{          You should override ApplyCustomFieldsValues() method of the base class.   }
{       8. Define a class, which inherits from the                                   }
{          DevExpress.Web.ASPxScheduler.Internal.AppointmentFormSaveCallbackCommand. }
{          This class creates an instance of the AppointmentFormController inheritor }
{          (defined in step 7) via the CreateAppointmentFormController method and    }
{          overrides the AssignControllerValues method  of the base class to collect }
{          user data from the form's editors.                                        }
{       9. Handle the BeforeExecuteCallbackCommand event. The event handler code     }
{          should create an instance of the class defined in step 8, and specify it  }
{          as the destination command instead of the default one.                    }
{      10. Modify the overall appearance of the page and its layout.                 }
{                                                                                    }
{************************************************************************************}
--%>


<%@ Control Language="C#" AutoEventWireup="true" Inherits="cp_AppointmentForm" CodeFile="cp_AppointmentForm.pt-BR.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v19.1, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v19.1, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v19.1, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>


<table class="dxscAppointmentForm" cellpadding="0" cellspacing="0" style="width: 100%; height: 230px;">
    <tr>
        <td class="dxscDoubleCell" colspan="2">
            <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="dxscLabelCell">
                        <dxe:ASPxLabel ID="lblSubject" runat="server" AssociatedControlID="tbSubject" Text="Subject:" Font-Names="Verdana" Font-Size="8pt">
                        </dxe:ASPxLabel>
                    </td>
                    <td class="dxscControlCell">
                        <dxe:ASPxTextBox ClientInstanceName="_dx" ID="tbSubject" runat="server" Width="100%" Text='<%# ((AppointmentFormTemplateContainer)Container).Appointment.Subject %>' Font-Names="Verdana" Font-Size="8pt">
                            <DisabledStyle ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="dxscSingleCell">
            <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="dxscLabelCell">
                        <dxe:ASPxLabel ID="lblLocation" runat="server" AssociatedControlID="tbLocation" Text="Location:" Font-Names="Verdana" Font-Size="8pt">
                        </dxe:ASPxLabel>
                    </td>
                    <td class="dxscControlCell">
                        <dxe:ASPxTextBox ClientInstanceName="_dx" ID="tbLocation" runat="server" Width="100%" Text='<%# ((AppointmentFormTemplateContainer)Container).Appointment.Location %>' Font-Names="Verdana" Font-Size="8pt">
                            <DisabledStyle ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </td>
        <td class="dxscSingleCell">
            <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="dxscLabelCell" style="padding-left: 25px;">
                        <dxe:ASPxLabel ID="lblLabel" runat="server" AssociatedControlID="edtLabel" Text="Label:" Font-Names="Verdana" Font-Size="8pt">
                        </dxe:ASPxLabel>
                    </td>
                    <td class="dxscControlCell">
                        <dxe:ASPxComboBox ClientInstanceName="_dx" ID="edtLabel" runat="server" Width="100%" DataSource='<%# ((AppointmentFormTemplateContainer)Container).LabelDataSource %>' Font-Names="Verdana" Font-Size="8pt">
                            <DisabledStyle ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="dxscSingleCell">
            <table class="dxscLabelControlPair" style="padding-left:0px" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="dxscLabelCell">
                        <dxe:ASPxLabel ID="lblStartDate" runat="server" AssociatedControlID="edtStartDate" Text="Start time:" Wrap="false" Font-Names="Verdana" Font-Size="8pt">
                        </dxe:ASPxLabel>

                    </td>
                    <td class="dxscControlCell" style="padding-left: 65px">
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxDateEdit ClientInstanceName="edtStartDate" ID="edtStartDate" runat="server"
                                        Date='<%# ((AppointmentFormTemplateContainer)Container).Start %>'
                                        EditFormat="Custom" AllowNull="False" Font-Names="Verdana" Font-Size="8pt"
                                        DisplayFormatString="{0:dd/MM/yyyy}" EditFormatString="dd/MM/yyyy"
                                        UseMaskBehavior="True" Width="95px">
                                        <DisabledStyle ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtHorarioInicio" runat="server" ClientInstanceName="txtHorarioInicio" Width="60px">
                                        <MaskSettings Mask="00:00" />
                                    </dxe:ASPxTextBox>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
        <td class="dxscSingleCell">
            <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="dxscLabelCell" style="padding-left: 25px;">
                        <dxe:ASPxLabel runat="server" ID="lblEndDate" Text="End time:" Wrap="false" AssociatedControlID="edtEndDate" Font-Names="Verdana" Font-Size="8pt" />

                    </td>
                    <td class="dxscControlCell" style="padding-left:50px">
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxDateEdit ID="edtEndDate" runat="server" Date='<%# ((AppointmentFormTemplateContainer)Container).End %>'
                                        EditFormat="Custom" DateOnError="Undo" AllowNull="false"
                                        Font-Names="Verdana" Font-Size="8pt" DisplayFormatString="{0:dd/MM/yyyy}"
                                        EditFormatString="dd/MM/yyyy" UseMaskBehavior="True" Width="95px" ClientInstanceName="edtEndDate">
                                        <DisabledStyle ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtHorarioTermino" runat="server" ClientInstanceName="txtHorarioTermino" Width="60px">
                                        <MaskSettings Mask="00:00" />
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="dxscSingleCell">
            <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="dxscLabelCell">
                        <dxe:ASPxLabel ID="lblStatus" runat="server" AssociatedControlID="edtStatus" Text="Show time as:" Wrap="false" Font-Names="Verdana" Font-Size="8pt">
                        </dxe:ASPxLabel>
                    </td>
                    <td class="dxscControlCell">
                        <dxe:ASPxComboBox ClientInstanceName="_dx" ID="edtStatus" runat="server" Width="100%" DataSource='<%# ((AppointmentFormTemplateContainer)Container).StatusDataSource %>' Font-Names="Verdana" Font-Size="8pt">
                            <DisabledStyle ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </td>
        <td class="dxscSingleCell" style="padding-left: 22px;">
            <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 20px; height: 20px;">
                        <dxe:ASPxCheckBox ClientInstanceName="_dx" ID="chkAllDay" runat="server" Checked='<%# ((AppointmentFormTemplateContainer)Container).Appointment.AllDay %>' Font-Names="Verdana" Font-Size="8pt">
                        </dxe:ASPxCheckBox>
                    </td>
                    <td style="padding-left: 2px;">
                        <dxe:ASPxLabel ID="lblAllDay" runat="server" Text="All day event" AssociatedControlID="chkAllDay" Font-Names="Verdana" Font-Size="8pt" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <% if (CanShowReminders)
            { %>
        <td class="dxscSingleCell">
            <% }
                else
                { %>
        <td class="dxscDoubleCell" colspan="2" style="display: none">
            <% } %><dxe:ASPxLabel ID="ASPxLabel1" runat="server" AssociatedControlID="edtStatus" Text="Descrição:" Wrap="false" Font-Names="Verdana" Font-Size="8pt">
            </dxe:ASPxLabel>
            <table cellpadding="0" cellspacing="0" class="dxscLabelControlPair">
                <tr>
                    <td class="dxscLabelCell">
                        <dxe:ASPxLabel ID="lblResource" runat="server" AssociatedControlID="edtResource"
                            Text="Resource:">
                        </dxe:ASPxLabel>
                    </td>
                    <td class="dxscControlCell">
                        <% if (ResourceSharing)
                            { %>
                        <dxe:ASPxDropDownEdit ID="ddResource" runat="server" AllowUserInput="false" ClientInstanceName="ddResource"
                            Enabled="<%# ((AppointmentFormTemplateContainer)Container).CanEditResource %>"
                            Width="100%">
                            <DropDownWindowTemplate>
                                <dxe:ASPxListBox ID="edtMultiResource" runat="server" Border-BorderWidth="0" DataSource='<%# ResourceDataSource %>'
                                    SelectionMode="CheckColumn" Width="100%">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                        var resourceNames = new Array();
                                        var items = s.GetSelectedItems();
                                        var count = items.length;
                                        if (count &gt; 0) {
                                            for(var i=0; i&lt;count; i++) 
                                                _aspxArrayPush(resourceNames, items[i].text);
                                        }
                                        else
                                            _aspxArrayPush(resourceNames, ddResource.cp_Caption_ResourceNone);
                                        ddResource.SetValue(resourceNames.join(', '));
                                    }" />
                                </dxe:ASPxListBox>
                            </DropDownWindowTemplate>
                        </dxe:ASPxDropDownEdit>
                        <% }
                            else
                            { %>
                        <dxe:ASPxComboBox ID="edtResource" runat="server" ClientInstanceName="_dx" DataSource="<%# ResourceDataSource %> "
                            Enabled="<%# ((AppointmentFormTemplateContainer)Container).CanEditResource %>"
                            Width="100%">
                        </dxe:ASPxComboBox>
                        <% } %>
                    </td>
                </tr>
            </table>
        </td>
        <% if (CanShowReminders)
            { %>
        <td class="dxscSingleCell">
            <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="dxscLabelCell" style="padding-left: 22px;">
                        <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 20px; height: 20px;">
                                    <dxe:ASPxCheckBox ClientInstanceName="_dx" ID="chkReminder" runat="server" Font-Names="Verdana" Font-Size="8pt">
                                        <ClientSideEvents CheckedChanged="function(s, e) { OnChkReminderCheckedChanged(s, e); }" />
                                    </dxe:ASPxCheckBox>
                                </td>
                                <td style="padding-left: 2px;">
                                    <dxe:ASPxLabel ID="lblReminder" runat="server" Text="Reminder" AssociatedControlID="chkReminder" Font-Names="Verdana" Font-Size="8pt" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="dxscControlCell" style="padding-left: 3px">
                        <dxe:ASPxComboBox ID="cbReminder" ClientInstanceName="_dxAppointmentForm_cbReminder" runat="server" Width="100%" DataSource='<%# ((AppointmentFormTemplateContainer)Container).ReminderDataSource %>' Font-Names="Verdana" Font-Size="8pt">
                            <DisabledStyle ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </td>
        <% } %>
    </tr>
    <tr>
        <td class="dxscDoubleCell" colspan="2" style="height: 90px;">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Descrição:" AssociatedControlID="chkReminder" Font-Names="Verdana" Font-Size="8pt" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxhe:ASPxHtmlEditor ID="txtDescricao" runat="server" Height="220px" Width="690px" Html="<%# ((AppointmentFormTemplateContainer)Container).Appointment.Description %>">

                            <SettingsDialogs>
                                <InsertImageDialog>
                                    <SettingsImageSelector>
                                        <CommonSettings AllowedFileExtensions=".jpeg, .pjpeg, .gif, .png, .x-png"/>
                                    </SettingsImageSelector>
                                </InsertImageDialog>
                            </SettingsDialogs>
                            <ClientSideEvents HtmlChanged="function(s, e) {
	_dxDescricao.SetText(s.GetHtml());
}" />
                            <Settings AllowHtmlView="False" AllowPreview="False" />
                            <SettingsSpellChecker Culture="Portuguese (Brazil)">
                            </SettingsSpellChecker>
                        </dxhe:ASPxHtmlEditor>
                        <dxe:ASPxMemo ID="tbDescription" runat="server"
                            ClientInstanceName="_dxDescricao" ClientVisible="False"
                            Rows="6" Text="<%# ((AppointmentFormTemplateContainer)Container).Appointment.Description %>"
                            Width="100%">
                        </dxe:ASPxMemo>
                    </td>
                </tr>
            </table>
            &nbsp;
        </td>
    </tr>
</table>

<dxsc:AppointmentRecurrenceForm ID="AppointmentRecurrenceForm1" runat="server"
    IsRecurring='<%# ((AppointmentFormTemplateContainer)Container).Appointment.IsRecurring %>'
    DayNumber='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceDayNumber %>'
    End='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceEnd %>'
    Month='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceMonth %>'
    OccurrenceCount='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceOccurrenceCount %>'
    Periodicity='<%# ((AppointmentFormTemplateContainer)Container).RecurrencePeriodicity %>'
    RecurrenceRange='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceRange %>'
    Start='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceStart %>'
    WeekDays='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceWeekDays %>'
    WeekOfMonth='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceWeekOfMonth %>'
    RecurrenceType='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceType %>'
    IsFormRecreated='<%# ((AppointmentFormTemplateContainer)Container).IsFormRecreated %>' Font-Names="Verdana" Font-Size="8pt">
</dxsc:AppointmentRecurrenceForm>

<table cellpadding="0" cellspacing="0" style="width: 100%; height: 35px;">
    <tr>
        <td style="width: 100%; height: 100%;" align="right">
            <table style="height: 100%;">
                <tr>
                    <td>
                        <dxe:ASPxButton runat="server" ClientInstanceName="_dx" ID="btnDelete" Text="Delete" UseSubmitBehavior="false"
                            AutoPostBack="false" EnableViewState="false" Width="91px"
                            Enabled='<%# ((AppointmentFormTemplateContainer)Container).CanDeleteAppointment %>'
                            CausesValidation="False" Font-Names="Verdana" Font-Size="8pt" />
                    </td>
                    <td>
                        <dxe:ASPxButton runat="server" ClientInstanceName="_dx" ID="btnOk" Text="OK"
                            UseSubmitBehavior="False" AutoPostBack="False"
                            EnableViewState="False" Width="91px" Font-Names="Verdana" Font-Size="8pt" >
                            <ClientSideEvents Click="function(s, e) {
    //debugger
     e.processOnServer = false;
     var retorno = false;

     var erroFaixaHoraria = '';
     var arrayHoraInicio = txtHorarioInicio.GetText().toString().split(':');
     var horarioInicioHoras = arrayHoraInicio[0]; 
     var horarioInicioMinutos = arrayHoraInicio[1]; 

     var arrayHoraTermino = txtHorarioTermino.GetText().toString().split(':');
     var horarioTerminoHoras = arrayHoraTermino[0]; 
     var horarioTerminoMinutos = arrayHoraTermino[1]; 
      if(horarioInicioHoras &gt; 23)
      {
                erroFaixaHoraria += 'Horário de início está fora da faixa\n';
      }
      if(horarioTerminoHoras &gt; 23)
      {
               erroFaixaHoraria += 'Horário de término está fora da faixa\n';
      }
      if(horarioInicioMinutos &gt; 59)
     {
                erroFaixaHoraria += 'Os minutos do horário de início estão fora da faixa\n';
     }
     if(horarioTerminoMinutos &gt; 59)
     {
               erroFaixaHoraria += 'Os minutos do horário de término estão fora da faixa\n';
     }
     if(erroFaixaHoraria != '')
     {
               window.top.mostraMensagem(erroFaixaHoraria , 'atencao', true, false, null);
                retorno = false;
     }
     else
     {
               if(edtStartDate.GetValue() != null &amp;&amp; edtEndDate.GetValue()  !=  null)
               {
                         var dataInicio 	  = new Date(edtStartDate.GetValue());
                         var meuDataInicio = (dataInicio.getMonth() +1) + '/' + dataInicio.getDate() + '/' + dataInicio.getFullYear() + ' ' + horarioInicioHoras +':' + horarioInicioMinutos ;
                         dataInicio = Date.parse(meuDataInicio);
                         var dataTermino = new Date(edtEndDate.GetValue());
                         var meuDataTermino = (dataTermino.getMonth() +1) + '/' + dataTermino.getDate() + '/' + dataTermino.getFullYear()+ ' ' + horarioTerminoHoras +':' + horarioTerminoMinutos ;
                         dataTermino = Date.parse(meuDataTermino);
                         if(dataInicio &gt; dataTermino)
                         {
                                   window.top.mostraMensagem('A Data de Início não pode acontecer depois da Data de Término!', 'atencao', true, false, null);
                         }    
                         else
                         {
                                   retorno = true;
                         }
               }
               else
               {            
                          if(edtStartDate.GetValue() == null &amp;&amp; edtEndDate.GetValue() == null)
                         {
                                   window.top.mostraMensagem('A Data de Início e a Data de Término devem ser informadas.', 'atencao', true, false, null);
                         }
                         if(edtStartDate.GetValue() == null)
                         {
                                    window.top.mostraMensagem('A Data de Início deve ser informada.', 'atencao', true, false, null);
                         }
                         else if(edtEndDate.GetValue() == null)
                         {
                                   window.top.mostraMensagem('A Data de Término deve ser informada.', 'atencao', true, false, null);
                         }
               }
          }
          if(retorno == true)
          {
                        hfGeral.Set(&quot;HorarioInicio&quot;,  horarioInicioHoras +':' + horarioInicioMinutos );
                        hfGeral.Set(&quot;HorarioTermino&quot;,  horarioTerminoHoras +':' + horarioTerminoMinutos);
debugger
                        ASPx.AppointmentSave(&quot;ctl00_AreaTrabalho_calendarioAgenda&quot;);
           }
}" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton runat="server" ClientInstanceName="_dx" ID="btnCancel" Text="Cancel" UseSubmitBehavior="false" AutoPostBack="false" EnableViewState="false"
                            Width="91px" CausesValidation="False" Font-Names="Verdana" Font-Size="8pt" />
                    </td>

                </tr>
            </table>
        </td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0" style="width: 100%;">
    <tr>
        <td style="width: 100%;" align="left">
            <dxsc:ASPxSchedulerStatusInfo runat="server" ID="schedulerStatusInfo" Priority="1" MasterControlID='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).ControlId %>' />
        </td>
    </tr>
</table>
<script id="dxss_ASPxSchedulerAppoinmentForm" type="text/javascript">
    function OnChkReminderCheckedChanged(s, e) {
        var isReminderEnabled = s.GetValue();
        if (isReminderEnabled)
            _dxAppointmentForm_cbReminder.SetSelectedIndex(3);
        else
            _dxAppointmentForm_cbReminder.SetSelectedIndex(-1);

        _dxAppointmentForm_cbReminder.SetEnabled(isReminderEnabled);

    }
</script>
