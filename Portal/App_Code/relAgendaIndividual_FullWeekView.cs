using System;

public class relAgendaIndividual_FullWeekView : DevExpress.XtraScheduler.Reporting.XtraSchedulerReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraScheduler.Reporting.HorizontalResourceHeaders horizontalResourceHeaders1;
    private DevExpress.XtraScheduler.Reporting.ReportWeekView reportWeekView1;
    private DevExpress.XtraScheduler.Reporting.FullWeek fullWeek1;
    private DevExpress.XtraScheduler.Reporting.TimeIntervalInfo timeIntervalInfo1;
    private DevExpress.XtraScheduler.Reporting.CalendarControl calendarControl1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public relAgendaIndividual_FullWeekView()
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
        //string resourceFileName = "relAgendaIndividual_FullWeekView.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.calendarControl1 = new DevExpress.XtraScheduler.Reporting.CalendarControl();
        this.timeIntervalInfo1 = new DevExpress.XtraScheduler.Reporting.TimeIntervalInfo();
        this.fullWeek1 = new DevExpress.XtraScheduler.Reporting.FullWeek();
        this.horizontalResourceHeaders1 = new DevExpress.XtraScheduler.Reporting.HorizontalResourceHeaders();
        this.reportWeekView1 = new DevExpress.XtraScheduler.Reporting.ReportWeekView();
        ((System.ComponentModel.ISupportInitialize)(this.reportWeekView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.horizontalResourceHeaders1,
            this.fullWeek1,
            this.timeIntervalInfo1,
            this.calendarControl1});
        this.Detail.HeightF = 875F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // calendarControl1
        // 
        this.calendarControl1.LocationFloat = new DevExpress.Utils.PointFloat(283F, 8F);
        this.calendarControl1.Name = "calendarControl1";
        this.calendarControl1.SizeF = new System.Drawing.SizeF(350F, 142F);
        this.calendarControl1.TimeCells = this.fullWeek1;
        this.calendarControl1.View = this.reportWeekView1;
        // 
        // timeIntervalInfo1
        // 
        this.timeIntervalInfo1.LocationFloat = new DevExpress.Utils.PointFloat(25F, 33F);
        this.timeIntervalInfo1.Name = "timeIntervalInfo1";
        this.timeIntervalInfo1.SizeF = new System.Drawing.SizeF(233F, 92F);
        this.timeIntervalInfo1.TimeCells = this.fullWeek1;
        // 
        // fullWeek1
        // 
        this.fullWeek1.HorizontalHeaders = this.horizontalResourceHeaders1;
        this.fullWeek1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 183F);
        this.fullWeek1.Name = "fullWeek1";
        this.fullWeek1.SizeF = new System.Drawing.SizeF(650F, 683F);
        this.fullWeek1.View = this.reportWeekView1;
        // 
        // horizontalResourceHeaders1
        // 
        this.horizontalResourceHeaders1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 158F);
        this.horizontalResourceHeaders1.Name = "horizontalResourceHeaders1";
        this.horizontalResourceHeaders1.SizeF = new System.Drawing.SizeF(650F, 25F);
        this.horizontalResourceHeaders1.View = this.reportWeekView1;
        // 
        // reportWeekView1
        // 
        this.reportWeekView1.VisibleResourceCount = 2;
        // 
        // relAgendaIndividual_FullWeekView
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail});
        this.Version = "14.2";
        this.Views.AddRange(new DevExpress.XtraScheduler.Reporting.ReportViewBase[] {
            this.reportWeekView1});
        this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.relAgendaIndividual_DataSourceDemanded);
        ((System.ComponentModel.ISupportInitialize)(this.reportWeekView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void relAgendaIndividual_DataSourceDemanded(object sender, EventArgs e)
    {

    }
}
