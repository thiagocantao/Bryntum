using DevExpress.XtraReports.UI;
using System;
using System.Data.SqlClient;

/// <summary>
/// Summary description for rel_sub_Valores
/// </summary>
public class rel_sub_Valores : DevExpress.XtraReports.UI.XtraReport
{
    private dados cDados;
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private DetailReportBand DetailReport;
    private DetailBand Detail1;
    private DetailReportBand DetailReport1;
    private DetailBand Detail2;
    private DsBoletimStatusRAPU dsBoletimStatusRAPU1;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell6;
    private XRTableRow xrTableRow4;
    private XRTableCell cellValorPrevisto;
    private XRTableRow xrTableRow6;
    private XRTableCell cellValorRealizado;
    private XRTableRow xrTableRow10;
    private XRTableCell cellVariacao;
    private DevExpress.XtraReports.Parameters.Parameter pCodigoStatusReport;
    private DetailReportBand DetailReport2;
    private DetailBand Detail3;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableRow xrTableRow2;
    private XRTableCell cellValorPrevistoA;
    private XRTableRow xrTableRow5;
    private XRTableCell cellValorRealizadoA;
    private XRTableRow xrTableRow7;
    private XRTableCell cellVariacaoA;
    private DevExpress.XtraReports.Parameters.Parameter pTipoGrafico;
    private CalculatedField cfNomeMes3;
    private CalculatedField cfNomeMes2;

    public int quantidadeColunasValores;
    public int quantidadeColunasValoresAcumulados;
    public int contador;
    public int contadorAcumulados;
    private CalculatedField cfNomeMes4;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rel_sub_Valores()
    {
        contador = 0;
        contadorAcumulados = 0;
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
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
        string resourceFileName = "rel_sub_Valores.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_sub_Valores.ResourceManager;
        this.pCodigoStatusReport = new DevExpress.XtraReports.Parameters.Parameter();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.cellValorPrevisto = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.cellValorRealizado = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        this.cellVariacao = new DevExpress.XtraReports.UI.XRTableCell();
        this.dsBoletimStatusRAPU1 = new DsBoletimStatusRAPU();
        this.DetailReport2 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.cellValorPrevistoA = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.cellValorRealizadoA = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.cellVariacaoA = new DevExpress.XtraReports.UI.XRTableCell();
        this.pTipoGrafico = new DevExpress.XtraReports.Parameters.Parameter();
        this.cfNomeMes3 = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfNomeMes2 = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfNomeMes4 = new DevExpress.XtraReports.UI.CalculatedField();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsBoletimStatusRAPU1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // pCodigoStatusReport
        // 
        this.pCodigoStatusReport.Name = "pCodigoStatusReport";
        this.pCodigoStatusReport.Visible = false;
        // 
        // Detail
        // 
        this.Detail.Dpi = 100F;
        this.Detail.HeightF = 0F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 100F;
        this.TopMargin.HeightF = 0F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 100F;
        this.BottomMargin.HeightF = 1F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // DetailReport
        // 
        this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.DetailReport1,
            this.DetailReport2});
        this.DetailReport.Dpi = 100F;
        this.DetailReport.Level = 0;
        this.DetailReport.Name = "DetailReport";
        // 
        // Detail1
        // 
        this.Detail1.Dpi = 100F;
        this.Detail1.HeightF = 0F;
        this.Detail1.Name = "Detail1";
        // 
        // DetailReport1
        // 
        this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2});
        this.DetailReport1.DataMember = "Valores";
        this.DetailReport1.DataSource = this.dsBoletimStatusRAPU1;
        this.DetailReport1.Dpi = 100F;
        this.DetailReport1.Level = 0;
        this.DetailReport1.Name = "DetailReport1";
        this.DetailReport1.DataSourceRowChanged += new DevExpress.XtraReports.UI.DataSourceRowEventHandler(this.DetailReport1_DataSourceRowChanged);
        // 
        // Detail2
        // 
        this.Detail2.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.Detail2.Dpi = 100F;
        this.Detail2.HeightF = 101.1719F;
        this.Detail2.MultiColumn.ColumnCount = 15;
        this.Detail2.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
        this.Detail2.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnCount;
        this.Detail2.Name = "Detail2";
        this.Detail2.StylePriority.UseBorders = false;
        // 
        // xrTable3
        // 
        this.xrTable3.BorderWidth = 1F;
        this.xrTable3.Dpi = 100F;
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3,
            this.xrTableRow4,
            this.xrTableRow6,
            this.xrTableRow10});
        this.xrTable3.SizeF = new System.Drawing.SizeF(49.81846F, 101.1719F);
        this.xrTable3.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTableRow3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell6});
        this.xrTableRow3.Dpi = 100F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.StylePriority.UseBackColor = false;
        this.xrTableRow3.StylePriority.UseBorders = false;
        this.xrTableRow3.StylePriority.UseTextAlignment = false;
        this.xrTableRow3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableRow3.Weight = 1D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell6.CanGrow = false;
        this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Valores.cfNomeMes2")});
        this.xrTableCell6.Dpi = 100F;
        this.xrTableCell6.Font = new System.Drawing.Font("Calibri", 8F);
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseBorders = false;
        this.xrTableCell6.StylePriority.UseFont = false;
        this.xrTableCell6.Weight = 0.75254640369681369D;
        this.xrTableCell6.WordWrap = false;
        this.xrTableCell6.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.cellValorRealizado_BeforePrint);
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.cellValorPrevisto});
        this.xrTableRow4.Dpi = 100F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1D;
        // 
        // cellValorPrevisto
        // 
        this.cellValorPrevisto.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.cellValorPrevisto.CanGrow = false;
        this.cellValorPrevisto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Valores.ValorPrevisto", "{0:#,#}")});
        this.cellValorPrevisto.Dpi = 100F;
        this.cellValorPrevisto.Font = new System.Drawing.Font("Calibri", 7F);
        this.cellValorPrevisto.Name = "cellValorPrevisto";
        this.cellValorPrevisto.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
        this.cellValorPrevisto.StylePriority.UseBorders = false;
        this.cellValorPrevisto.StylePriority.UseFont = false;
        this.cellValorPrevisto.StylePriority.UsePadding = false;
        this.cellValorPrevisto.StylePriority.UseTextAlignment = false;
        this.cellValorPrevisto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.cellValorPrevisto.Weight = 0.75254640369681369D;
        this.cellValorPrevisto.WordWrap = false;
        this.cellValorPrevisto.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.cellValorRealizado_BeforePrint);
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.cellValorRealizado});
        this.xrTableRow6.Dpi = 100F;
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 1D;
        // 
        // cellValorRealizado
        // 
        this.cellValorRealizado.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.cellValorRealizado.CanGrow = false;
        this.cellValorRealizado.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Valores.ValorRealizado", "{0:#,#}")});
        this.cellValorRealizado.Dpi = 100F;
        this.cellValorRealizado.Font = new System.Drawing.Font("Calibri", 7F);
        this.cellValorRealizado.Name = "cellValorRealizado";
        this.cellValorRealizado.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
        this.cellValorRealizado.StylePriority.UseBorders = false;
        this.cellValorRealizado.StylePriority.UseFont = false;
        this.cellValorRealizado.StylePriority.UsePadding = false;
        this.cellValorRealizado.StylePriority.UseTextAlignment = false;
        this.cellValorRealizado.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.cellValorRealizado.Weight = 0.75254640369681369D;
        this.cellValorRealizado.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.cellValorRealizado_BeforePrint);
        // 
        // xrTableRow10
        // 
        this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.cellVariacao});
        this.xrTableRow10.Dpi = 100F;
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 1D;
        // 
        // cellVariacao
        // 
        this.cellVariacao.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.cellVariacao.CanGrow = false;
        this.cellVariacao.Dpi = 100F;
        this.cellVariacao.Font = new System.Drawing.Font("Calibri", 7F);
        this.cellVariacao.Name = "cellVariacao";
        this.cellVariacao.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
        this.cellVariacao.StylePriority.UseBorders = false;
        this.cellVariacao.StylePriority.UseFont = false;
        this.cellVariacao.StylePriority.UsePadding = false;
        this.cellVariacao.StylePriority.UseTextAlignment = false;
        this.cellVariacao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.cellVariacao.Weight = 0.75254640369681369D;
        this.cellVariacao.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.cellVariacao_BeforePrint);
        // 
        // dsBoletimStatusRAPU1
        // 
        this.dsBoletimStatusRAPU1.DataSetName = "DsBoletimStatusRAPU";
        this.dsBoletimStatusRAPU1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // DetailReport2
        // 
        this.DetailReport2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3});
        this.DetailReport2.DataMember = "ValoresAcumuladosTabela";
        this.DetailReport2.DataSource = this.dsBoletimStatusRAPU1;
        this.DetailReport2.Dpi = 100F;
        this.DetailReport2.Level = 1;
        this.DetailReport2.Name = "DetailReport2";
        this.DetailReport2.DataSourceRowChanged += new DevExpress.XtraReports.UI.DataSourceRowEventHandler(this.DetailReport2_DataSourceRowChanged);
        // 
        // Detail3
        // 
        this.Detail3.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.Detail3.Dpi = 100F;
        this.Detail3.HeightF = 101.1719F;
        this.Detail3.MultiColumn.ColumnCount = 15;
        this.Detail3.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
        this.Detail3.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnCount;
        this.Detail3.Name = "Detail3";
        this.Detail3.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBandExceptLastEntry;
        this.Detail3.StylePriority.UseBorders = false;
        // 
        // xrTable1
        // 
        this.xrTable1.BorderWidth = 1F;
        this.xrTable1.Dpi = 100F;
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow5,
            this.xrTableRow7});
        this.xrTable1.SizeF = new System.Drawing.SizeF(49.81846F, 101.1719F);
        this.xrTable1.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTableRow1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
        this.xrTableRow1.Dpi = 100F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.StylePriority.UseBackColor = false;
        this.xrTableRow1.StylePriority.UseBorders = false;
        this.xrTableRow1.StylePriority.UseTextAlignment = false;
        this.xrTableRow1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableRow1.Weight = 1D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell1.BorderWidth = 1F;
        this.xrTableCell1.CanGrow = false;
        this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ValoresAcumuladosTabela.cfNomeMes4")});
        this.xrTableCell1.Dpi = 100F;
        this.xrTableCell1.Font = new System.Drawing.Font("Calibri", 8F);
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.StylePriority.UseBorderWidth = false;
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.Weight = 0.75254640369681369D;
        this.xrTableCell1.WordWrap = false;
        this.xrTableCell1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell1_BeforePrint);
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.cellValorPrevistoA});
        this.xrTableRow2.Dpi = 100F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1D;
        // 
        // cellValorPrevistoA
        // 
        this.cellValorPrevistoA.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.cellValorPrevistoA.CanGrow = false;
        this.cellValorPrevistoA.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ValoresAcumuladosTabela.ValorPrevisto", "{0:#,#}")});
        this.cellValorPrevistoA.Dpi = 100F;
        this.cellValorPrevistoA.Font = new System.Drawing.Font("Calibri", 7F);
        this.cellValorPrevistoA.Name = "cellValorPrevistoA";
        this.cellValorPrevistoA.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
        this.cellValorPrevistoA.StylePriority.UseBorders = false;
        this.cellValorPrevistoA.StylePriority.UseFont = false;
        this.cellValorPrevistoA.StylePriority.UsePadding = false;
        this.cellValorPrevistoA.StylePriority.UseTextAlignment = false;
        this.cellValorPrevistoA.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.cellValorPrevistoA.Weight = 0.75254640369681369D;
        this.cellValorPrevistoA.WordWrap = false;
        this.cellValorPrevistoA.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell1_BeforePrint);
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.cellValorRealizadoA});
        this.xrTableRow5.Dpi = 100F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1D;
        // 
        // cellValorRealizadoA
        // 
        this.cellValorRealizadoA.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.cellValorRealizadoA.CanGrow = false;
        this.cellValorRealizadoA.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ValoresAcumuladosTabela.ValorRealizado", "{0:#,#}")});
        this.cellValorRealizadoA.Dpi = 100F;
        this.cellValorRealizadoA.Font = new System.Drawing.Font("Calibri", 7F);
        this.cellValorRealizadoA.Name = "cellValorRealizadoA";
        this.cellValorRealizadoA.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
        this.cellValorRealizadoA.StylePriority.UseBorders = false;
        this.cellValorRealizadoA.StylePriority.UseFont = false;
        this.cellValorRealizadoA.StylePriority.UsePadding = false;
        this.cellValorRealizadoA.StylePriority.UseTextAlignment = false;
        this.cellValorRealizadoA.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.cellValorRealizadoA.Weight = 0.75254640369681369D;
        this.cellValorRealizadoA.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell1_BeforePrint);
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.cellVariacaoA});
        this.xrTableRow7.Dpi = 100F;
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 1D;
        // 
        // cellVariacaoA
        // 
        this.cellVariacaoA.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.cellVariacaoA.CanGrow = false;
        this.cellVariacaoA.Dpi = 100F;
        this.cellVariacaoA.Font = new System.Drawing.Font("Calibri", 7F);
        this.cellVariacaoA.Name = "cellVariacaoA";
        this.cellVariacaoA.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
        this.cellVariacaoA.StylePriority.UseBorders = false;
        this.cellVariacaoA.StylePriority.UseFont = false;
        this.cellVariacaoA.StylePriority.UsePadding = false;
        this.cellVariacaoA.StylePriority.UseTextAlignment = false;
        this.cellVariacaoA.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.cellVariacaoA.Weight = 0.75254640369681369D;
        this.cellVariacaoA.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.cellVariacaoA_BeforePrint);
        // 
        // pTipoGrafico
        // 
        this.pTipoGrafico.Name = "pTipoGrafico";
        this.pTipoGrafico.Visible = false;
        // 
        // cfNomeMes3
        // 
        this.cfNomeMes3.DataMember = "ValoresAcumulados";
        this.cfNomeMes3.DataSource = this.dsBoletimStatusRAPU1;
        this.cfNomeMes3.Expression = resources.GetString("cfNomeMes3.Expression");
        this.cfNomeMes3.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfNomeMes3.Name = "cfNomeMes3";
        // 
        // cfNomeMes2
        // 
        this.cfNomeMes2.DataMember = "Valores";
        this.cfNomeMes2.DataSource = this.dsBoletimStatusRAPU1;
        this.cfNomeMes2.Expression = resources.GetString("cfNomeMes2.Expression");
        this.cfNomeMes2.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfNomeMes2.Name = "cfNomeMes2";
        // 
        // cfNomeMes4
        // 
        this.cfNomeMes4.DataMember = "ValoresAcumuladosTabela";
        this.cfNomeMes4.DataSource = this.dsBoletimStatusRAPU1;
        this.cfNomeMes4.Expression = resources.GetString("cfNomeMes4.Expression");
        this.cfNomeMes4.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfNomeMes4.Name = "cfNomeMes4";
        // 
        // rel_sub_Valores
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.DetailReport});
        this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.cfNomeMes3,
            this.cfNomeMes2,
            this.cfNomeMes4});
        this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 1);
        this.PageWidth = 750;
        this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pCodigoStatusReport,
            this.pTipoGrafico});
        this.Version = "17.2";
        this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.rel_sub_Valores_DataSourceDemanded);
        this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.rel_sub_Valores_BeforePrint);
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsBoletimStatusRAPU1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void rel_sub_Valores_DataSourceDemanded(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);
        string comandoSql = string.Format(
            "exec p_rel_BoletimQuinzenalRAPU {0}", pCodigoStatusReport.Value);
        string connectionString = cDados.classeDados.getStringConexao();
        SqlDataAdapter adapter = new SqlDataAdapter(comandoSql, connectionString);
        adapter.TableMappings.Add("Table", "StatusReport");
        adapter.TableMappings.Add("Table1", "MarcosCriticos");
        adapter.TableMappings.Add("Table2", "Valores");
        adapter.TableMappings.Add("Table3", "ValoresAcumulados");
        adapter.TableMappings.Add("Table4", "LegendaDesempenho");
        adapter.TableMappings.Add("Table5", "Periodo");
        adapter.TableMappings.Add("Table6", "ValoresAcumuladosTabela");


        adapter.Fill(dsBoletimStatusRAPU1);
        //quantidadeColunasValores = dsBoletimStatusRAPU1.Valores.AsEnumerable().Max(r => r.Field<byte>("Mes"));
        //quantidadeColunasValoresAcumulados = dsBoletimStatusRAPU1.ValoresAcumulados.AsEnumerable().Max(r => r.Field<byte>("Mes"));

    }

    private void rel_sub_Valores_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DetailReport1.Visible = (pTipoGrafico.Value.ToString() == "Geral") ? true : false;
        DetailReport2.Visible = (pTipoGrafico.Value.ToString() == "Acumulado") ? true : false;
    }

    private void cellVariacao_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

        decimal valorPrevisto = 0;
        decimal valorRealizado = 0;

        decimal.TryParse(cellValorPrevisto.Text, out valorPrevisto);
        decimal.TryParse(cellValorRealizado.Text, out valorRealizado);

        ((XRTableCell)sender).Text = string.Format("{0:#,#}", (valorPrevisto - valorRealizado));
        //contador++;
        if (contador == quantidadeColunasValores)
        {
            //((XRTableCell)sender).Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
            contador = 0;
        }

    }

    private void cellVariacaoA_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        decimal valorPrevisto = 0;
        decimal valorRealizado = 0;

        decimal.TryParse(cellValorPrevistoA.Text, out valorPrevisto);
        decimal.TryParse(cellValorRealizadoA.Text, out valorRealizado);

        var cell = (XRTableCell)sender;


        ((XRTableCell)sender).Text = string.Format("{0:#,#}", (valorPrevisto - valorRealizado));
        //contador++;
        if (contadorAcumulados == quantidadeColunasValoresAcumulados)
        {
            //((XRTableCell)sender).Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
            contadorAcumulados = 0;
        }
    }

    private void cellValorRealizado_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        //contador++;
        if (contador == quantidadeColunasValores)
        {
            //((XRTableCell)sender).Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
            contador = 0;
        }
    }

    private void xrTableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        //contador++;
        if (contadorAcumulados == quantidadeColunasValoresAcumulados)
        {
            //((XRTableCell)sender).Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
            contadorAcumulados = 0;
        }
    }

    private void DetailReport1_DataSourceRowChanged(object sender, DataSourceRowEventArgs e)
    {
        contador++;
    }

    private void DetailReport2_DataSourceRowChanged(object sender, DataSourceRowEventArgs e)
    {
        contadorAcumulados++;
    }
}
