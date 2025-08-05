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

<%@ Control Language="C#" AutoEventWireup="true" Inherits="cp_AppointmentForm" CodeFile="cp_AppointmentForm.en-US.ascx.cs" %>
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
                        <dxe:ASPxTextBox ClientInstanceName="_dx" ID="tbSubject" runat="server" Width="100%" Text='<%# ((AppointmentFormTemplateContainer)Container).Appointment.Subject %>' Font-Names="Verdana" Font-Size="8pt" ReadOnly="True">
                            <ReadOnlyStyle BackColor="#EBEBEB">
                            </ReadOnlyStyle>
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
                        <dxe:ASPxTextBox ClientInstanceName="_dx" ID="tbLocation" runat="server" Width="100%" Text='<%# ((AppointmentFormTemplateContainer)Container).Appointment.Location %>' Font-Names="Verdana" Font-Size="8pt" ReadOnly="True">
                            <ReadOnlyStyle BackColor="#EBEBEB">
                            </ReadOnlyStyle>
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
                        <dxe:ASPxComboBox ClientInstanceName="_dx" ID="edtLabel" runat="server" Width="100%" DataSource='<%# ((AppointmentFormTemplateContainer)Container).LabelDataSource %>' Font-Names="Verdana" Font-Size="8pt" ReadOnly="True">
                            <ReadOnlyStyle BackColor="#EBEBEB">
                            </ReadOnlyStyle>
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
                                        UseMaskBehavior="True" Width="120px" ReadOnly="True">
                                        <ReadOnlyStyle BackColor="#EBEBEB">
                                        </ReadOnlyStyle>
                                        <DisabledStyle ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtHorarioInicio" runat="server" ClientInstanceName="txtHorarioInicio" Width="60px" ReadOnly="True">
                                        <MaskSettings Mask="00:00" />
                                        <ReadOnlyStyle BackColor="#EBEBEB">
                                        </ReadOnlyStyle>
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
                                        EditFormatString="dd/MM/yyyy" UseMaskBehavior="True" Width="120px" ClientInstanceName="edtEndDate" ReadOnly="True">
                                        <ReadOnlyStyle BackColor="#EBEBEB">
                                        </ReadOnlyStyle>
                                        <DisabledStyle ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtHorarioTermino" runat="server" ClientInstanceName="txtHorarioTermino" Width="60px" ReadOnly="True">
                                        <MaskSettings Mask="00:00" />
                                        <ReadOnlyStyle BackColor="#EBEBEB">
                                        </ReadOnlyStyle>
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
                        <dxe:ASPxComboBox ClientInstanceName="_dx" ID="edtStatus" runat="server" Width="100%" DataSource='<%# ((AppointmentFormTemplateContainer)Container).StatusDataSource %>' Font-Names="Verdana" Font-Size="8pt" ReadOnly="True">
                            <ReadOnlyStyle BackColor="#EBEBEB">
                            </ReadOnlyStyle>
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
                        <dxe:ASPxCheckBox ClientInstanceName="_dx" ID="chkAllDay" runat="server" Checked='<%# ((AppointmentFormTemplateContainer)Container).Appointment.AllDay %>' Font-Names="Verdana" Font-Size="8pt" ReadOnly="True">
                            <ReadOnlyStyle BackColor="#EBEBEB">
                            </ReadOnlyStyle>
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
                            Width="100%" ReadOnly="True">
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
                            <ReadOnlyStyle BackColor="#EBEBEB">
                            </ReadOnlyStyle>
                        </dxe:ASPxDropDownEdit>
                        <% }
                            else
                            { %>
                        <dxe:ASPxComboBox ID="edtResource" runat="server" ClientInstanceName="_dx" DataSource="<%# ResourceDataSource %> "
                            Enabled="<%# ((AppointmentFormTemplateContainer)Container).CanEditResource %>"
                            Width="100%" ReadOnly="True">
                            <ReadOnlyStyle BackColor="#EBEBEB">
                            </ReadOnlyStyle>
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
                                    <dxe:ASPxCheckBox ClientInstanceName="_dx" ID="chkReminder" runat="server" Font-Names="Verdana" Font-Size="8pt" ReadOnly="True">
                                        <ClientSideEvents CheckedChanged="function(s, e) { OnChkReminderCheckedChanged(s, e); }" />
                                        <ReadOnlyStyle BackColor="#EBEBEB">
                                        </ReadOnlyStyle>
                                    </dxe:ASPxCheckBox>
                                </td>
                                <td style="padding-left: 2px;">
                                    <dxe:ASPxLabel ID="lblReminder" runat="server" Text="Reminder" AssociatedControlID="chkReminder" Font-Names="Verdana" Font-Size="8pt" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="dxscControlCell" style="padding-left: 3px">
                        <dxe:ASPxComboBox ID="cbReminder" ClientInstanceName="_dxAppointmentForm_cbReminder" runat="server" Width="100%" DataSource='<%# ((AppointmentFormTemplateContainer)Container).ReminderDataSource %>' Font-Names="Verdana" Font-Size="8pt" ReadOnly="True">
                            <ReadOnlyStyle BackColor="#EBEBEB">
                            </ReadOnlyStyle>
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
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Description" AssociatedControlID="chkReminder" Font-Names="Verdana" Font-Size="8pt" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxhe:ASPxHtmlEditor ID="txtDescricao" runat="server" Height="220px" Width="100%" Html="<%# ((AppointmentFormTemplateContainer)Container).Appointment.Description %>">

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
                            <Settings AllowHtmlView="False" AllowDesignView="False" />
                            <SettingsSpellChecker Culture="Portuguese (Brazil)">
                            </SettingsSpellChecker>

<SettingsDialogs>
<InsertImageDialog>
<SettingsImageUpload>
<ValidationSettings AllowedFileExtensions=".jpeg, .pjpeg, .gif, .png, .x-png"></ValidationSettings>
</SettingsImageUpload>

<SettingsImageSelector>
<CommonSettings AllowedFileExtensions=".jpeg, .pjpeg, .gif, .png, .x-png"></CommonSettings>
</SettingsImageSelector>
</InsertImageDialog>
</SettingsDialogs>
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
<table cellpadding="0" cellspacing="0" style="width: 100%; height: 35px;">
    <tr>
        <td style="width: 100%; height: 100%;" align="right">
            <table style="height: 100%;">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <dxe:ASPxButton runat="server" ClientInstanceName="_dx" ID="btnCancel" Text="Close" UseSubmitBehavior="false" AutoPostBack="false" EnableViewState="false"
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
