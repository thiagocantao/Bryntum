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
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.Web;
using DevExpress.XtraScheduler.Native;
using DevExpress.XtraScheduler.Localization;
using DevExpress.Web.ASPxScheduler.Localization;

public partial class ReminderForm : SchedulerFormControl {
    public override string ClassName { get { return "ASPxReminderForm"; } }

    protected override void OnLoad(EventArgs e) {
        base.OnLoad(e);
        Localize();
		//PrepareChildControls();
	}
    void Localize() {
        btnDismissAll.Text = ASPxSchedulerLocalizer.GetString(ASPxSchedulerStringId.Form_ButtonDismissAll);
        btnDismiss.Text = ASPxSchedulerLocalizer.GetString(ASPxSchedulerStringId.Form_ButtonDismiss);
        lblClickSnooze.Text = ASPxSchedulerLocalizer.GetString(ASPxSchedulerStringId.Form_SnoozeInfo);
        btnSnooze.Text = ASPxSchedulerLocalizer.GetString(ASPxSchedulerStringId.Form_ButtonSnooze);
        btnOpenItem.Text = ASPxSchedulerLocalizer.GetString(ASPxSchedulerStringId.Form_ButtonOpenItem);
        lblStartTime.Text = ASPxSchedulerLocalizer.GetString(ASPxSchedulerStringId.Form_StartTime);
    }
	public override void DataBind() {
		base.DataBind();

		RemindersFormTemplateContainer container = (RemindersFormTemplateContainer)Parent;

		btnDismiss.ClientSideEvents.Click = container.DismissReminderHandler;
		btnDismissAll.ClientSideEvents.Click = container.DismissAllRemindersHandler;
		btnSnooze.ClientSideEvents.Click = container.SnoozeRemindersHandler;

		InitItemListBox(container);
		InitSnoozeCombo(container);
	}
	void InitItemListBox(RemindersFormTemplateContainer container) {
		ReminderCollection reminders = container.Reminders;
		int count = reminders.Count;
		for(int i = 0; i < count; i++) {
			Reminder reminder = reminders[i];
            ListEditItem item = new ListEditItem(reminder.Subject, AppointmentSearchHelper.GetAppointmentClientId(reminder.Appointment));
			lbItems.Items.Add(item);
		}
		lbItems.SelectedIndex = 0;
	}
	void InitSnoozeCombo(RemindersFormTemplateContainer container) {
		cbSnooze.Items.Clear();
		TimeSpan[] timeSpans = container.SnoozeTimeSpans;
		int count = timeSpans.Length;
		for(int i = 0; i < count; i++) {
			TimeSpan timeSpan = timeSpans[i];
			cbSnooze.Items.Add(new ListEditItem(container.ConvertSnoozeTimeSpanToString(timeSpan), timeSpan));
		}
		cbSnooze.SelectedIndex = 4;
	}
	protected override ASPxEditBase[] GetChildEditors() {
		ASPxEditBase[] edits = new ASPxEditBase[] {
			lbItems, lblClickSnooze, cbSnooze, lblStartTime, lblAppointmentStartTime, lblAppointmentSubject
        };
		return edits;
	}
	protected override ASPxButton[] GetChildButtons() {
		ASPxButton[] buttons = new ASPxButton[] {
			btnDismissAll, btnDismiss, btnSnooze, btnOpenItem
        };
		return buttons;
	}
}
