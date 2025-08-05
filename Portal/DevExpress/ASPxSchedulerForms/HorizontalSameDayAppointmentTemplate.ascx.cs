/*
{************************************************************************************}
{                                                                                    }
{   DO NOT MODIFY THIS FILE!                                                         }
{                                                                                    }
{   It will be overwritten without prompting when a new version becomes              }
{   available. All your changes will be lost.                                        }
{                                                                                    }
{   This file contains the default template and is required for the appointment      }
{   rendering. Improper modifications may result in incorrect appearance of the      }
{   appointment.                                                                     }
{                                                                                    }
{   In order to create and use your own custom template, perform the following       }
{   steps:                                                                           }
{       1. Save a copy of this file with a different name in another location.       }
{       2. Add a Register tag in the .aspx page header for each template you use,    }
{          as follows: <%@ Register Src="PathToTemplateFile" TagName="NameOfTemplate"}
{          TagPrefix="ShortNameOfTemplate" %>                                        }
{       3. In the .aspx page find the tags for different scheduler views within      }
{          the ASPxScheduler control tag. Insert template tags into the tags         }
{          for the views which should be customized.                                 }
{          The template tag should satisfy the following pattern:                    }
{          <Templates>                                                               }
{              <HorizontalSameDayAppointmentTemplate>                                }
{                  < ShortNameOfTemplate: NameOfTemplate runat="server"/>            }
{              </HorizontalSameDayAppointmentTemplate>                               }
{          </Templates>                                                              }
{          where ShortNameOfTemplate, NameOfTemplate are the names of the            }
{          registered templates, defined in step 2.                                  }
{************************************************************************************}
 */

using System;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using DevExpress.Web.Internal;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Drawing;
using DevExpress.Web.ASPxScheduler.Internal;
using System.Web.UI;
using System.Drawing;

public partial class HorizontalSameDayAppointmentTemplate : DevExpress.Web.ASPxScheduler.SchedulerUserControl {
	HorizontalAppointmentTemplateContainer Container { get { return (HorizontalAppointmentTemplateContainer)Parent; } }
	HorizontalAppointmentTemplateItems Items { get { return Container.Items; } }

    protected override void OnLoad(EventArgs e) {
        base.OnLoad(e);

        PrepareMainDiv();
        PrepareImageContainer();
        PrepareTimeContainer();
        PrepareClockContainers();
        PrepareStatusControl();
        AssignBackgroundColor();
        ApplyCustomStyle();

        LayoutAppointmentImages();
        LayoutClockImages();
    }
    protected override void PrepareControls(ASPxScheduler scheduler) {
        lblStartTime.ControlStyle.MergeWith(Items.StartTimeText.Style);
        lblEndTime.ControlStyle.MergeWith(Items.EndTimeText.Style);
        lblTitle.ControlStyle.MergeWith(Items.Title.Style);

        lblStartTime.ParentSkinOwner = scheduler;
        lblEndTime.ParentSkinOwner = scheduler;
        lblTitle.ParentSkinOwner = scheduler;
    }
    void PrepareMainDiv() {
        appointmentDiv.Style.Value = Items.AppointmentStyle.GetStyleAttributes(Page).Value;
        appointmentDiv.Attributes["class"] = RenderUtils.CombineCssClasses(appointmentDiv.Attributes["class"], Items.AppointmentStyle.CssClass);
    }
    void PrepareImageContainer() {
        imageContainer.Visible = Items.Images.Count > 0;
    }
    void PrepareTimeContainer() {
        timeContainer.Visible = Items.StartTimeText.Visible || Items.EndTimeText.Visible || Items.StartTimeClock.Visible || Items.EndTimeClock.Visible;
    }
    void PrepareClockContainers() {
        startTimeClockContainer.Visible = Container.Items.StartTimeClock.Visible;
        endTimeClockContainer.Visible = Container.Items.EndTimeClock.Visible;
    }
    void PrepareStatusControl() {
        statusBack.Visible = Items.StatusControl.Visible;
        statusFore.Visible = Items.StatusControl.Visible;

        if(Items.StatusControl.Visible)
            AssignStatusStyle();
    }
    void AssignBackgroundColor() {
        appointmentDiv.Style[HtmlTextWriterStyle.BackgroundColor] = string.Empty;
        appointmentBackground.Style[HtmlTextWriterStyle.BackgroundColor] = HtmlConvertor.ToHtml(Items.AppointmentStyle.BackColor);
    }
    void AssignStatusStyle() {
        statusBack.Style.Add(HtmlTextWriterStyle.BackgroundColor, HtmlConvertor.ToHtml(Items.StatusControl.BackColor));
        statusBack.Style.Add(HtmlTextWriterStyle.BorderColor, HtmlConvertor.ToHtml(GetStatusBorderColor()));

        statusFore.Style.Add(HtmlTextWriterStyle.BackgroundColor, HtmlConvertor.ToHtml(Items.StatusControl.Color));
    }
    void ApplyCustomStyle() {
        var customStyle = GetCustomBackgroundStyle();
        customBackgroundLayer.Style.Value = customStyle.GetStyleAttributes(Page).Value;
        customBackgroundLayer.Attributes["class"] = RenderUtils.CombineCssClasses(customBackgroundLayer.Attributes["class"], customStyle.CssClass);
    }
    AppearanceStyleBase GetCustomBackgroundStyle() {
        AppearanceStyleBase customStyle = new AppearanceStyleBase();
        customStyle.AssignWithoutBorders(Items.AppointmentStyle);
        customStyle.BackColor = Color.Empty;
        customStyle.CssClass = Items.AppointmentStyle.CssClass;

        return customStyle;
    }
    Color GetStatusBorderColor() {
        Color borderColor = Items.StatusControl.Color;
        if(borderColor.IsEmpty || HtmlConvertor.ToHtml(borderColor) == "#FFFFFF")
            borderColor = Color.LightGray;

        return borderColor;
    }
    void LayoutClockImages() {
        startTimeClockContainer.Controls.Add(Container.Items.StartTimeClock);
        endTimeClockContainer.Controls.Add(Container.Items.EndTimeClock);
    }
    void LayoutAppointmentImages() {
        int count = Items.Images.Count;
        for(int i = 0; i < count; i++)
            AddImage(imageContainer, Items.Images[i]);
    }
    void AddImage(HtmlGenericControl container, AppointmentImageItem imageItem) {
        System.Web.UI.WebControls.Image image = new System.Web.UI.WebControls.Image();
        imageItem.ImageProperties.AssignToControl(image, false);
        SchedulerWebEventHelper.AddOnDragStartEvent(image, ASPxSchedulerScripts.GetPreventOnDragStart());
        container.Controls.Add(image);
    }
}
