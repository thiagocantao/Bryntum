using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Localization;
using DevExpress.Web.Internal;
 
public partial class DetailedAppointmentToolTip : ASPxSchedulerToolTipBase {
    public override bool ToolTipShowStem {
        get {
            return false;
        }
    }
    public override bool ToolTipCloseOnClick {
        get {
            return true;
        }
    }
    public override bool ToolTipResetPositionByTimer {
        get {
            return false;
        }
    }
    
    public override string ClassName { get { return "ASPxClientAppointmentDetailedToolTip"; } }
    protected override void OnLoad(EventArgs e) {
        base.OnLoad(e);

        Localize();
    }

    protected override void PrepareControls(ASPxScheduler scheduler) {
        base.PrepareControls(scheduler);

        ApplyControlsStyle(scheduler);
    }

    void Localize() {
        btnEdit.Text = ASPxSchedulerLocalizer.GetString(ASPxSchedulerStringId.ToolTip_EditAppointment);
        btnDelete.Text = ASPxSchedulerLocalizer.GetString(ASPxSchedulerStringId.ToolTip_DeleteAppointment);
    }

    protected override void PrepareLocalization(SchedulerLocalizationCache localizationCache) {
        base.PrepareLocalization(localizationCache);

        localizationCache.Add(ASPxSchedulerStringId.ToolTip_Loading);
    }

    void ApplyControlsStyle(ASPxScheduler scheduler) {
        ApplyControlsParentStyles(scheduler);
        ApplyButtonsStyle();
    }

    protected void ApplyControlsParentStyles(ASPxScheduler scheduler) {
        foreach(ASPxWebControl control in GetChildControls()) {
            if(control != null) {
                control.ParentSkinOwner = scheduler;
            }
        }
    }

    void ApplyButtonsStyle() {
        ApplyButtonStyle(btnDelete);
        ApplyButtonStyle(btnEdit);
    }

    void ApplyButtonStyle(ASPxButton button) {
        button.Width = Unit.Percentage(50);
        button.Height = Unit.Pixel(44);

        button.Border.BorderStyle = BorderStyle.None;
        button.BorderTop.BorderStyle = BorderStyle.Solid;
        button.BorderTop.BorderWidth = Unit.Pixel(1);

        var buttonWithColorClass = "dxsc-dat-colored-button";
        RenderUtils.AppendCssClass(button.HoverStyle, buttonWithColorClass);
        RenderUtils.AppendCssClass(button.PressedStyle, buttonWithColorClass);
        RenderUtils.AppendCssClass(button.DisabledStyle, "dxsc-dat-disabled-btn");
    }

    protected override Control[] GetChildControls() {
        Control[] controls = new Control[] { lblSubject, lblInterval, lblResource, btnDelete, btnEdit };
        return controls;
    }
}