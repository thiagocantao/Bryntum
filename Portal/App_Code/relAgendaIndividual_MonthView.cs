using System;

public class relAgendaIndividual_MonthView : DevExpress.XtraScheduler.Reporting.XtraSchedulerReport
{
    private DevExpress.XtraScheduler.Reporting.CalendarControl calendarControl1;
    private DevExpress.XtraScheduler.Reporting.HorizontalWeek horizontalWeek1;
    private DevExpress.XtraScheduler.Reporting.ReportMonthView reportMonthView1;
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraScheduler.Reporting.VerticalResourceHeaders verticalResourceHeaders1;
    private DevExpress.XtraScheduler.Reporting.DayOfWeekHeaders dayOfWeekHeaders1;
    private DevExpress.XtraScheduler.Reporting.TimeIntervalInfo timeIntervalInfo1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public relAgendaIndividual_MonthView()
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
        //string resourceFileName = "relAgendaIndividual_MonthView.resx";
        this.calendarControl1 = new DevExpress.XtraScheduler.Reporting.CalendarControl();
        this.horizontalWeek1 = new DevExpress.XtraScheduler.Reporting.HorizontalWeek();
        this.reportMonthView1 = new DevExpress.XtraScheduler.Reporting.ReportMonthView();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.verticalResourceHeaders1 = new DevExpress.XtraScheduler.Reporting.VerticalResourceHeaders();
        this.dayOfWeekHeaders1 = new DevExpress.XtraScheduler.Reporting.DayOfWeekHeaders();
        this.timeIntervalInfo1 = new DevExpress.XtraScheduler.Reporting.TimeIntervalInfo();
        ((System.ComponentModel.ISupportInitialize)(this.reportMonthView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // calendarControl1
        // 
        this.calendarControl1.LocationFloat = new DevExpress.Utils.PointFloat(283F, 8F);
        this.calendarControl1.Name = "calendarControl1";
        this.calendarControl1.PrintInColumn = DevExpress.XtraScheduler.Reporting.PrintInColumnMode.Even;
        this.calendarControl1.SizeF = new System.Drawing.SizeF(358F, 133F);
        this.calendarControl1.TimeCells = this.horizontalWeek1;
        this.calendarControl1.View = this.reportMonthView1;
        // 
        // horizontalWeek1
        // 
        this.horizontalWeek1.LocationFloat = new DevExpress.Utils.PointFloat(25F, 175F);
        this.horizontalWeek1.Name = "horizontalWeek1";
        this.horizontalWeek1.SizeF = new System.Drawing.SizeF(625F, 693F);
        this.horizontalWeek1.View = this.reportMonthView1;
        // 
        // reportMonthView1
        // 
        this.reportMonthView1.VisibleWeekDayColumnCount = 2;
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.verticalResourceHeaders1,
            this.dayOfWeekHeaders1,
            this.timeIntervalInfo1,
            this.calendarControl1,
            this.horizontalWeek1});
        this.Detail.HeightF = 875F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // verticalResourceHeaders1
        // 
        this.verticalResourceHeaders1.Corners.Top = 25;
        this.verticalResourceHeaders1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 150F);
        this.verticalResourceHeaders1.Name = "verticalResourceHeaders1";
        this.verticalResourceHeaders1.SizeF = new System.Drawing.SizeF(25F, 725F);
        this.verticalResourceHeaders1.TimeCells = this.horizontalWeek1;
        this.verticalResourceHeaders1.View = this.reportMonthView1;
        // 
        // dayOfWeekHeaders1
        // 
        this.dayOfWeekHeaders1.LocationFloat = new DevExpress.Utils.PointFloat(25F, 150F);
        this.dayOfWeekHeaders1.Name = "dayOfWeekHeaders1";
        this.dayOfWeekHeaders1.SizeF = new System.Drawing.SizeF(625F, 25F);
        this.dayOfWeekHeaders1.TimeCells = this.horizontalWeek1;
        this.dayOfWeekHeaders1.View = this.reportMonthView1;
        // 
        // timeIntervalInfo1
        // 
        this.timeIntervalInfo1.LocationFloat = new DevExpress.Utils.PointFloat(25F, 33F);
        this.timeIntervalInfo1.Name = "timeIntervalInfo1";
        this.timeIntervalInfo1.PrintInColumn = DevExpress.XtraScheduler.Reporting.PrintInColumnMode.Odd;
        this.timeIntervalInfo1.SizeF = new System.Drawing.SizeF(233F, 92F);
        this.timeIntervalInfo1.TimeCells = this.horizontalWeek1;
        // 
        // relAgendaIndividual
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail});
        this.Version = "14.2";
        this.Views.AddRange(new DevExpress.XtraScheduler.Reporting.ReportViewBase[] {
            this.reportMonthView1});
        this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.relAgendaIndividual_DataSourceDemanded);
        ((System.ComponentModel.ISupportInitialize)(this.reportMonthView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void relAgendaIndividual_DataSourceDemanded(object sender, EventArgs e)
    {

    }
}
