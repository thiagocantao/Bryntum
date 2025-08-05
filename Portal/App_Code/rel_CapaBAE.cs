using DevExpress.XtraReports.UI;
using System;
using System.Drawing;

/// <summary>
/// Summary description for rel_CapaBAE
/// </summary>
public class rel_CapaBAE : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRPictureBox picImagemCapaBAE;
    public DevExpress.XtraReports.Parameters.Parameter ParamDataInicioPeriodoRelatorio;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    public DevExpress.XtraReports.Parameters.Parameter ParamUrlImagemCapaBAE;

    Bitmap img;

    public rel_CapaBAE()
    {
        InitializeComponent();
    }

    private void DefineImagemCapa()
    {
        //string resourceFileName = "rel_CapaBAE.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_CapaBAE.ResourceManager;
        string imgPath = ParamUrlImagemCapaBAE.Value as string;
        img = (Bitmap)Image.FromFile(imgPath);
    }

    private void InserePeriodoImagemCapa()
    {
        Graphics g = Graphics.FromImage(img);
        DateTime dataInicioPeriodoRelatorio = (DateTime)ParamDataInicioPeriodoRelatorio.Value;
        String drawString = string.Format("{0:MMMM/yyyy}", dataInicioPeriodoRelatorio).ToUpper();
        Font drawFont = new Font("Cambria", 14);
        SolidBrush drawBrush = new SolidBrush(Color.FromArgb(0x1F, 0x49, 0x91));
        SizeF stringSize = g.MeasureString(drawString, drawFont);
        float x = (img.Width - stringSize.Width) / 2;
        float y = img.Height * 0.95f;
        g.DrawString(drawString, drawFont, drawBrush, x, y);
        g.Flush();
        g.Save();

        picImagemCapaBAE.Image = img;
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
        //string resourceFileName = "rel_CapaBAE.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.picImagemCapaBAE = new DevExpress.XtraReports.UI.XRPictureBox();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.ParamDataInicioPeriodoRelatorio = new DevExpress.XtraReports.Parameters.Parameter();
        this.ParamUrlImagemCapaBAE = new DevExpress.XtraReports.Parameters.Parameter();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.picImagemCapaBAE});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 2969F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // picImagemCapaBAE
        // 
        this.picImagemCapaBAE.Dpi = 254F;
        this.picImagemCapaBAE.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.picImagemCapaBAE.Name = "picImagemCapaBAE";
        this.picImagemCapaBAE.SizeF = new System.Drawing.SizeF(2101F, 2969F);
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 0F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 0F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // ParamDataInicioPeriodoRelatorio
        // 
        this.ParamDataInicioPeriodoRelatorio.Name = "ParamDataInicioPeriodoRelatorio";
        this.ParamDataInicioPeriodoRelatorio.Type = typeof(System.DateTime);
        this.ParamDataInicioPeriodoRelatorio.Value = new System.DateTime(2013, 4, 1, 16, 37, 6, 182);
        this.ParamDataInicioPeriodoRelatorio.Visible = false;
        // 
        // ParamUrlImagemCapaBAE
        // 
        this.ParamUrlImagemCapaBAE.Name = "ParamUrlImagemCapaBAE";
        this.ParamUrlImagemCapaBAE.Visible = false;
        // 
        // rel_CapaBAE
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
        this.PageHeight = 2969;
        this.PageWidth = 2101;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.ParamDataInicioPeriodoRelatorio,
            this.ParamUrlImagemCapaBAE});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 25F;
        this.Version = "12.1";
        this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.rel_CapaBAE_BeforePrint);
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void rel_CapaBAE_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DefineImagemCapa();
        InserePeriodoImagemCapa();
    }
}
