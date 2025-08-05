using System;

public class relAgendaIndividual_TimeLineView : DevExpress.XtraScheduler.Reporting.XtraSchedulerReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraScheduler.Reporting.TimelineScaleHeaders timelineScaleHeaders1;
    private DevExpress.XtraScheduler.Reporting.ReportTimelineView reportTimelineView1;
    private DevExpress.XtraScheduler.Reporting.TimelineCells timelineCells1;
    private DevExpress.XtraScheduler.Reporting.VerticalResourceHeaders verticalResourceHeaders1;
    private DevExpress.XtraScheduler.Reporting.TimeIntervalInfo timeIntervalInfo1;
    private DevExpress.XtraScheduler.Reporting.CalendarControl calendarControl1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public relAgendaIndividual_TimeLineView()
    {
        InitializeComponent();
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }
    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        //string resourceFileName = "relAgendaIndividual_TimeLineView.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.reportTimelineView1 = new DevExpress.XtraScheduler.Reporting.ReportTimelineView();
        this.timelineCells1 = new DevExpress.XtraScheduler.Reporting.TimelineCells();
        this.verticalResourceHeaders1 = new DevExpress.XtraScheduler.Reporting.VerticalResourceHeaders();
        this.timelineScaleHeaders1 = new DevExpress.XtraScheduler.Reporting.TimelineScaleHeaders();
        this.timeIntervalInfo1 = new DevExpress.XtraScheduler.Reporting.TimeIntervalInfo();
        this.calendarControl1 = new DevExpress.XtraScheduler.Reporting.CalendarControl();
        ((System.ComponentModel.ISupportInitialize)(this.reportTimelineView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.timelineScaleHeaders1,
            this.timelineCells1,
            this.verticalResourceHeaders1,
            this.timeIntervalInfo1,
            this.calendarControl1});
        this.Detail.HeightF = 888F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // reportTimelineView1
        // 
        this.reportTimelineView1.VisibleIntervalColumnCount = 2;
        this.reportTimelineView1.VisibleIntervalCount = 7;
        this.reportTimelineView1.VisibleResourceCount = 2;
        // 
        // timelineCells1
        // 
        this.timelineCells1.AppointmentDisplayOptions.AppointmentHeight = 30;
        this.timelineCells1.AppointmentDisplayOptions.EndTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Never;
        this.timelineCells1.AppointmentDisplayOptions.StartTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Never;
        this.timelineCells1.HorizontalHeaders = this.timelineScaleHeaders1;
        this.timelineCells1.LocationFloat = new DevExpress.Utils.PointFloat(25F, 199F);
        this.timelineCells1.Name = "timelineCells1";
        this.timelineCells1.SizeF = new System.Drawing.SizeF(624F, 558F);
        this.timelineCells1.View = this.reportTimelineView1;
        // 
        // verticalResourceHeaders1
        // 
        this.verticalResourceHeaders1.Corners.Top = 50;
        this.verticalResourceHeaders1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 149F);
        this.verticalResourceHeaders1.Name = "verticalResourceHeaders1";
        this.verticalResourceHeaders1.SizeF = new System.Drawing.SizeF(25F, 733F);
        this.verticalResourceHeaders1.TimeCells = this.timelineCells1;
        this.verticalResourceHeaders1.View = this.reportTimelineView1;
        // 
        // timelineScaleHeaders1
        // 
        this.timelineScaleHeaders1.LocationFloat = new DevExpress.Utils.PointFloat(25F, 149F);
        this.timelineScaleHeaders1.Name = "timelineScaleHeaders1";
        this.timelineScaleHeaders1.SizeF = new System.Drawing.SizeF(624F, 50F);
        this.timelineScaleHeaders1.View = this.reportTimelineView1;
        // 
        // timeIntervalInfo1
        // 
        this.timeIntervalInfo1.FormatType = DevExpress.XtraScheduler.Reporting.TimeIntervalFormatType.Weekly;
        this.timeIntervalInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.timeIntervalInfo1.Name = "timeIntervalInfo1";
        this.timeIntervalInfo1.PrintContentMode = DevExpress.XtraScheduler.Reporting.PrintContentMode.AllColumns;
        this.timeIntervalInfo1.PrintInColumn = DevExpress.XtraScheduler.Reporting.PrintInColumnMode.Odd;
        this.timeIntervalInfo1.SizeF = new System.Drawing.SizeF(233F, 117F);
        this.timeIntervalInfo1.TimeCells = this.timelineCells1;
        // 
        // calendarControl1
        // 
        this.calendarControl1.LocationFloat = new DevExpress.Utils.PointFloat(283F, 8F);
        this.calendarControl1.Name = "calendarControl1";
        this.calendarControl1.PrintInColumn = DevExpress.XtraScheduler.Reporting.PrintInColumnMode.Even;
        this.calendarControl1.SizeF = new System.Drawing.SizeF(350F, 133F);
        this.calendarControl1.TimeCells = this.timelineCells1;
        this.calendarControl1.View = this.reportTimelineView1;
        // 
        // relAgendaIndividual_TimeLineView
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail});
        this.Version = "14.2";
        this.Views.AddRange(new DevExpress.XtraScheduler.Reporting.ReportViewBase[] {
            this.reportTimelineView1});
        this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.relAgendaIndividual_DataSourceDemanded);
        ((System.ComponentModel.ISupportInitialize)(this.reportTimelineView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void relAgendaIndividual_DataSourceDemanded(object sender, EventArgs e)
    {

    }
}
