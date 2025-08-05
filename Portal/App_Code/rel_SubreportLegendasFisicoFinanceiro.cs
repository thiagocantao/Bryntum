using DevExpress.XtraReports.UI;
using System.Data;
using System.Drawing;

/// <summary>
/// Summary description for rel_SubreportLegendasFisicoFinanceiro
/// </summary>
public class rel_SubreportLegendasFisicoFinanceiro : XtraReport
{
    #region fields

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private DataColumn dataColumn4;
    private DataColumn dataColumn3;
    private DataColumn dataColumn2;
    private DataColumn dataColumn1;
    private DataTable dataTable1;
    private DataSet ds;
    private DetailBand Detail;
    private TopMarginBand TopMargin;
    private BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private XRLabel xrLabel1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrtcCorFisico;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRLabel xrLabel2;
    private XRLabel xrLabel3;
    private XRTableCell xrtcCorFinanceiro;
    #endregion

    public rel_SubreportLegendasFisicoFinanceiro(DataTable dt)
    {
        InitializeComponent();
        InitData(dt);
    }

    private void InitData(DataTable dt)
    {
        DataTableReader dataReader = dt.CreateDataReader();
        ds.Load(dataReader, LoadOption.OverwriteChanges, "LegendaDesempenho");
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
        //string resourceFileName = "rel_SubreportLegendasFisicoFinanceiro.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrtcCorFisico = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrtcCorFinanceiro = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.dataColumn4 = new System.Data.DataColumn();
        this.dataColumn3 = new System.Data.DataColumn();
        this.dataColumn2 = new System.Data.DataColumn();
        this.dataColumn1 = new System.Data.DataColumn();
        this.dataTable1 = new System.Data.DataTable();
        this.ds = new System.Data.DataSet();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 50F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable1
        // 
        this.xrTable1.Dpi = 254F;
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1800F, 30F);
        this.xrTable1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTable1_BeforePrint);
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrtcCorFisico,
            this.xrTableCell2,
            this.xrtcCorFinanceiro,
            this.xrTableCell3});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1D;
        // 
        // xrtcCorFisico
        // 
        this.xrtcCorFisico.Dpi = 254F;
        this.xrtcCorFisico.Name = "xrtcCorFisico";
        this.xrtcCorFisico.Weight = 0.050000002384185831D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LegendaDesempenho.DescricaoFaixaFisico")});
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.xrTableCell2.StylePriority.UsePadding = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = "xrTableCell2";
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell2.Weight = 1.4500000739097596D;
        // 
        // xrtcCorFinanceiro
        // 
        this.xrtcCorFinanceiro.Dpi = 254F;
        this.xrtcCorFinanceiro.Name = "xrtcCorFinanceiro";
        this.xrtcCorFinanceiro.Weight = 0.049999997615814175D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LegendaDesempenho.DescricaoFaixaFinanceiro")});
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.xrTableCell3.StylePriority.UsePadding = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "xrTableCell3";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell3.Weight = 1.4499999260902405D;
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1});
        this.ReportHeader.Dpi = 254F;
        this.ReportHeader.HeightF = 80F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // xrLabel3
        // 
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Calibri", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(975F, 50F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(400F, 30F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "Financeiro";
        // 
        // xrLabel2
        // 
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Calibri", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(75F, 50F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(400F, 30F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.Text = "Físico";
        // 
        // xrLabel1
        // 
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "Legenda do desvio de desempenho";
        // 
        // dataColumn4
        // 
        this.dataColumn4.ColumnName = "DescricaoFaixaFinanceiro";
        // 
        // dataColumn3
        // 
        this.dataColumn3.ColumnName = "CorFaixaFinanceiro";
        // 
        // dataColumn2
        // 
        this.dataColumn2.ColumnName = "DescricaoFaixaFisico";
        // 
        // dataColumn1
        // 
        this.dataColumn1.ColumnName = "CorFaixaFisico";
        // 
        // dataTable1
        // 
        this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4});
        this.dataTable1.TableName = "LegendaDesempenho";
        // 
        // ds
        // 
        this.ds.DataSetName = "NewDataSet";
        this.ds.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
        // 
        // rel_SubreportLegendasFisicoFinanceiro
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader});
        this.DataMember = "LegendaDesempenho";
        this.DataSource = this.ds;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Calibri", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Margins = new System.Drawing.Printing.Margins(150, 150, 100, 100);
        this.PageHeight = 2969;
        this.PageWidth = 2101;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 25F;
        this.Version = "10.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void xrTable1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        string strCorFisico = GetCurrentColumnValue("CorFaixaFisico").ToString();
        string strCorFinanceiro = GetCurrentColumnValue("CorFaixaFinanceiro").ToString();
        Color corFisico = ObtemCorPeloNome(strCorFisico);
        Color corFinanceiro = ObtemCorPeloNome(strCorFinanceiro);

        xrtcCorFisico.BackColor = corFisico;
        xrtcCorFinanceiro.BackColor = corFinanceiro;
    }

    private Color ObtemCorPeloNome(string strCor)
    {
        switch ((strCor ?? string.Empty).ToLower())
        {
            case "amarelo":
                return Color.Yellow;
            case "branco":
                return Color.White;
            case "verde":
                return Color.Green;
            case "vermelho":
                return Color.Red;
            default:
                return Color.Transparent;
        }
    }
}
