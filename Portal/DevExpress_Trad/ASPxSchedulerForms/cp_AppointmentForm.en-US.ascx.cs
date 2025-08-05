/*
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
*/
using System;
using System.Web.UI;
using DevExpress.XtraScheduler;
using DevExpress.Web;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using System.Collections;
using System.Collections.Generic;
using DevExpress.XtraScheduler.Localization;
using DevExpress.Web.Internal;

public partial class cp_AppointmentForm : SchedulerFormControl {
	public bool CanShowReminders {
		get {
			return ((AppointmentFormTemplateContainer)Parent).Control.Storage.EnableReminders;
		}
	}
    public bool ResourceSharing { 
        get {
            return ((AppointmentFormTemplateContainer)Parent).Control.Storage.ResourceSharing;
        } 
    }
    public IEnumerable ResourceDataSource {
        get {
            return ((AppointmentFormTemplateContainer)Parent).ResourceDataSource;
        }
    }

    public bool EventoDiaInteiro
    {
        get
        {
            return ((AppointmentFormTemplateContainer)Parent).Control.Storage.Appointments.Mappings.AllDay.ToLower() == "1";
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
		//PrepareChildControls();
		tbSubject.Focus();
        //lblLabel.Text = "Rótulo:";
        //lblSubject.Text = "Assunto:";
        //lblLocation.Text = "Local:";
        //lblStartDate.Text = "Início:";
        //lblEndDate.Text = "Término:";
        //lblResource.Text = "Recurso:";
        //lblAllDay.Text = "Dia Todo";
        //lblReminder.Text = "Lembrete";
        //lblStatus.Text = "Exibir Como:"; 
        //btnCancel.Text = "Fechar";
        tbDescription.Text = txtDescricao.Html;

        AppointmentFormTemplateContainer container = (AppointmentFormTemplateContainer)Parent;
        Appointment apt = container.Appointment;

        if (apt.CustomFields["HorarioInicio"] != null
           && !string.IsNullOrWhiteSpace(apt.CustomFields["HorarioInicio"].ToString())
           && !string.IsNullOrEmpty(apt.CustomFields["HorarioInicio"].ToString()))
        {
            txtHorarioInicio.Text = apt.CustomFields["HorarioInicio"].ToString();
        }
        if (apt.CustomFields["HorarioTermino"] != null
            && !string.IsNullOrWhiteSpace(apt.CustomFields["HorarioTermino"].ToString())
            && !string.IsNullOrEmpty(apt.CustomFields["HorarioTermino"].ToString()))
        {
            txtHorarioTermino.Text = apt.CustomFields["HorarioTermino"].ToString();
        }
    }
	public override void DataBind() {
		base.DataBind();

		AppointmentFormTemplateContainer container = (AppointmentFormTemplateContainer)Parent;
		Appointment apt = container.Appointment;
		
        edtLabel.SelectedIndex = int.Parse(apt.LabelKey.ToString());
		edtStatus.SelectedIndex = int.Parse(apt.StatusKey.ToString());

        tbDescription.Text = txtDescricao.Html;

        PopulateResourceEditors(apt, container);

		if (container.Appointment.HasReminder) {
			cbReminder.Value = container.Appointment.Reminder.TimeBeforeStart.ToString();
			chkReminder.Checked = true;
		}
		else {
			cbReminder.ClientEnabled = false;
		}

        if (apt.CustomFields["Tipo"] != null && apt.CustomFields["Tipo"].ToString() == "R")
        {
            btnCancel.ClientVisible = true;
            tbDescription.ClientEnabled = false;
            tbLocation.ClientEnabled = false;
            tbSubject.ClientEnabled = false;
            edtEndDate.ClientEnabled = false;
            edtLabel.ClientEnabled = false;
            edtResource.ClientEnabled = false;
            edtStartDate.ClientEnabled = false;
            edtStatus.ClientEnabled = false;
            txtDescricao.Settings.AllowDesignView = false;
            txtDescricao.Settings.AllowPreview = true;
            cbReminder.ClientVisible = false;
            chkAllDay.ClientEnabled = false;
        }
        else
        {}
        btnCancel.ClientSideEvents.Click = container.CancelHandler;
        if (apt.CustomFields["HorarioInicio"] != null
            && !string.IsNullOrWhiteSpace(apt.CustomFields["HorarioInicio"].ToString())
            && !string.IsNullOrEmpty(apt.CustomFields["HorarioInicio"].ToString()))
        {
            txtHorarioInicio.Text = apt.CustomFields["HorarioInicio"].ToString();
        }
        if (apt.CustomFields["HorarioTermino"] != null
            && !string.IsNullOrWhiteSpace(apt.CustomFields["HorarioTermino"].ToString())
            && !string.IsNullOrEmpty(apt.CustomFields["HorarioTermino"].ToString()))
        {
            txtHorarioTermino.Text = apt.CustomFields["HorarioTermino"].ToString();
        }
    }
    private void PopulateResourceEditors(Appointment apt, AppointmentFormTemplateContainer container) {
        chkAllDay.Checked = false;
        if (ResourceSharing) {
            ASPxListBox edtMultiResource = ddResource.FindControl("edtMultiResource") as ASPxListBox;
            if(edtMultiResource == null)
                return;
            SetListBoxSelectedValues(edtMultiResource, apt.ResourceIds);
            List<String> multiResourceString = GetListBoxSeletedItemsText(edtMultiResource);
            string stringResourceNone = SchedulerLocalizer.GetString(SchedulerStringId.Caption_ResourceNone);
            ddResource.Value = stringResourceNone;
            if (multiResourceString.Count > 0) 
                ddResource.Value = String.Join(", ", multiResourceString.ToArray());
            ddResource.JSProperties.Add("cp_Caption_ResourceNone", stringResourceNone);
        }
        else {
            if(!Object.Equals(apt.ResourceId, ResourceEmpty.Id))
                edtResource.Value = apt.ResourceId.ToString();
            else
                edtResource.Value = SchedulerIdHelper.EmptyResourceId;
        }
        if(EventoDiaInteiro == true)
        {
            chkAllDay.Checked = true;
        }
    }
    List<String> GetListBoxSeletedItemsText(ASPxListBox listBox) {
        List<String> result = new List<string>();
        foreach(ListEditItem editItem in listBox.Items) {
            if(editItem.Selected)
                result.Add(editItem.Text);
        }
        return result;
    }
    void SetListBoxSelectedValues(ASPxListBox listBox, IEnumerable values) {
        listBox.Value = null;
        foreach(object value in values) {
            ListEditItem item = listBox.Items.FindByValue(value.ToString());
            if(item != null)
                item.Selected = true;
        }
    } 
	protected override void PrepareChildControls() {
		AppointmentFormTemplateContainer container = (AppointmentFormTemplateContainer)Parent;
		ASPxScheduler control = container.Control;

		base.PrepareChildControls();
	}
	protected override ASPxEditBase[] GetChildEditors() {
		ASPxEditBase[] edits = new ASPxEditBase[] {
			lblSubject, tbSubject,
			lblLocation, tbLocation,
			lblLabel, edtLabel,
			lblStartDate, edtStartDate,
			lblEndDate, edtEndDate,
			lblStatus, edtStatus,
			lblAllDay, chkAllDay,
			lblResource, edtResource,
			tbDescription, cbReminder,
            ddResource, txtHorarioInicio, txtHorarioTermino
		};
		return edits;
	}
	protected override ASPxButton[] GetChildButtons() {
		ASPxButton[] buttons = new ASPxButton[] {
			btnCancel
		};
		return buttons;
	}
}
