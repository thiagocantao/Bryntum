using DevExpress.XtraReports.UI;
using DsPlanilhaCustosProjetoTableAdapters;
using System;
using System.Data;
using System.Drawing;
using System.IO;

/// <summary>
/// Summary description for relPlanilhaCustosProjeto
/// </summary>
public class relPlanilhaCustosProjeto : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private XRPictureBox picLogoEntidade;
    private XRLabel xrLabel1;
    private XRControlStyle styleTitulo;
    private XRLine xrLine1;
    private XRLabel xrLabel2;
    private XRControlStyle styleSubTitulo;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell6;
    private GroupHeaderBand GroupHeader2;
    private GroupHeaderBand GroupHeader1;
    private XRControlStyle styleTabela;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell18;
    private XRControlStyle styleCabecalhoTabela;
    private GroupFooterBand GroupFooter1;
    private XRTable xrTable4;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell19;
    private GroupFooterBand GroupFooter2;
    private XRTable xrTable5;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell25;
    public DevExpress.XtraReports.Parameters.Parameter pCodigoProjeto;
    public DevExpress.XtraReports.Parameters.Parameter pNomeProjeto;
    private DsPlanilhaCustosProjeto ds;
    public DevExpress.XtraReports.Parameters.Parameter pLinhaBase;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private ItensOrcamentoProjetoTableAdapter ta;

    dados cDados = CdadosUtil.GetCdados(null);

    public relPlanilhaCustosProjeto()
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
        //string resourceFileName = "relPlanilhaCustosProjeto.resx";
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.pNomeProjeto = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.picLogoEntidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.styleTitulo = new DevExpress.XtraReports.UI.XRControlStyle();
        this.styleSubTitulo = new DevExpress.XtraReports.UI.XRControlStyle();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.styleTabela = new DevExpress.XtraReports.UI.XRControlStyle();
        this.styleCabecalhoTabela = new DevExpress.XtraReports.UI.XRControlStyle();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.pCodigoProjeto = new DevExpress.XtraReports.Parameters.Parameter();
        this.ds = new DsPlanilhaCustosProjeto();
        this.pLinhaBase = new DevExpress.XtraReports.Parameters.Parameter();
        this.ta = new DsPlanilhaCustosProjetoTableAdapters.ItensOrcamentoProjetoTableAdapter();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 50F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.Dpi = 254F;
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(25F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(1875F, 50F);
        this.xrTable2.StyleName = "styleTabela";
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ItensOrcamentoProjeto.Nome")});
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Weight = 0.61978951146175976D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ItensOrcamentoProjeto.Especificacao")});
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Weight = 0.65852613975123375D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ItensOrcamentoProjeto.Quantidade", "{0:n2}")});
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseTextAlignment = false;
        this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell9.Weight = 0.34863186745091485D;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ItensOrcamentoProjeto.ValorUnitario", "{0:n2}")});
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseTextAlignment = false;
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell10.Weight = 0.3873682436330696D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ItensOrcamentoProjeto.ValorTotal", "{0:n2}")});
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseTextAlignment = false;
        this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell11.Weight = 0.387368602680407D;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ItensOrcamentoProjeto.FonteRecursos")});
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Weight = 0.503578792917352D;
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
            this.xrLine1,
            this.xrLabel2,
            this.xrLabel1,
            this.picLogoEntidade});
        this.ReportHeader.Dpi = 254F;
        this.ReportHeader.HeightF = 210F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // xrLine1
        // 
        this.xrLine1.Dpi = 254F;
        this.xrLine1.LineWidth = 3;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 205F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(1900F, 5F);
        // 
        // xrLabel2
        // 
        this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.pNomeProjeto, "Text", "Projeto: {0}")});
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(400F, 125F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(25, 0, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(1500F, 50F);
        this.xrLabel2.StyleName = "styleSubTitulo";
        this.xrLabel2.StylePriority.UsePadding = false;
        // 
        // pNomeProjeto
        // 
        this.pNomeProjeto.Description = "Nome do Projeto";
        this.pNomeProjeto.Name = "pNomeProjeto";
        this.pNomeProjeto.Value = "";
        this.pNomeProjeto.Visible = false;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(475F, 25F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(950F, 50F);
        this.xrLabel1.StyleName = "styleTitulo";
        this.xrLabel1.Text = "Anexo - Planilha de Custos do Projeto";
        // 
        // picLogoEntidade
        // 
        this.picLogoEntidade.Dpi = 254F;
        this.picLogoEntidade.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.picLogoEntidade.Name = "picLogoEntidade";
        this.picLogoEntidade.SizeF = new System.Drawing.SizeF(400F, 200F);
        this.picLogoEntidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // styleTitulo
        // 
        this.styleTitulo.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
        this.styleTitulo.Name = "styleTitulo";
        this.styleTitulo.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.styleTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // styleSubTitulo
        // 
        this.styleSubTitulo.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.styleSubTitulo.Name = "styleSubTitulo";
        this.styleSubTitulo.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        // 
        // xrTable1
        // 
        this.xrTable1.Dpi = 254F;
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1900F, 50F);
        this.xrTable1.StyleName = "styleCabecalhoTabela";
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell4,
            this.xrTableCell2,
            this.xrTableCell5,
            this.xrTableCell3,
            this.xrTableCell6});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "Item";
        this.xrTableCell1.Weight = 0.67105264764083061D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "Especificação";
        this.xrTableCell4.Weight = 0.6710526476408305D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = "Quantidade";
        this.xrTableCell2.Weight = 0.35526314986379526D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Text = "Unitário (R$)";
        this.xrTableCell5.Weight = 0.39473683407432153D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Text = "Total (R$)";
        this.xrTableCell3.Weight = 0.39473683407432153D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Text = "Fonte de Recursos";
        this.xrTableCell6.Weight = 0.51315788670590035D;
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.GroupHeader2.Dpi = 254F;
        this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Tipo", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader2.HeightF = 50F;
        this.GroupHeader2.Name = "GroupHeader2";
        this.GroupHeader2.RepeatEveryPage = true;
        // 
        // xrTable3
        // 
        this.xrTable3.Dpi = 254F;
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable3.SizeF = new System.Drawing.SizeF(1900F, 50F);
        this.xrTable3.StyleName = "styleTabela";
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell18});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1D;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ItensOrcamentoProjeto.Tipo", "Tipo: {0}")});
        this.xrTableCell18.Dpi = 254F;
        this.xrTableCell18.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.StylePriority.UseFont = false;
        this.xrTableCell18.Weight = 3D;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.GroupHeader1.Dpi = 254F;
        this.GroupHeader1.HeightF = 75F;
        this.GroupHeader1.Level = 1;
        this.GroupHeader1.Name = "GroupHeader1";
        this.GroupHeader1.RepeatEveryPage = true;
        // 
        // styleTabela
        // 
        this.styleTabela.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.styleTabela.Name = "styleTabela";
        this.styleTabela.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        // 
        // styleCabecalhoTabela
        // 
        this.styleCabecalhoTabela.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.styleCabecalhoTabela.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.styleCabecalhoTabela.ForeColor = System.Drawing.Color.Gray;
        this.styleCabecalhoTabela.Name = "styleCabecalhoTabela";
        this.styleCabecalhoTabela.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.styleCabecalhoTabela.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
        this.GroupFooter1.Dpi = 254F;
        this.GroupFooter1.HeightF = 50F;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // xrTable4
        // 
        this.xrTable4.Dpi = 254F;
        this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 0F);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
        this.xrTable4.SizeF = new System.Drawing.SizeF(1875F, 50F);
        this.xrTable4.StyleName = "styleTabela";
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell19});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseFont = false;
        this.xrTableCell13.Text = "Sub Total";
        this.xrTableCell13.Weight = 0.61978951146175976D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Weight = 0.65852613975123375D;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseTextAlignment = false;
        this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell15.Weight = 0.34863186745091485D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Dpi = 254F;
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.StylePriority.UseTextAlignment = false;
        this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell16.Weight = 0.3873682436330696D;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ItensOrcamentoProjeto.ValorTotal")});
        this.xrTableCell17.Dpi = 254F;
        this.xrTableCell17.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.StylePriority.UseFont = false;
        this.xrTableCell17.StylePriority.UseTextAlignment = false;
        xrSummary1.FormatString = "{0:n2}";
        xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell17.Summary = xrSummary1;
        this.xrTableCell17.Text = "xrTableCell17";
        this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell17.Weight = 0.387368602680407D;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.Weight = 0.503578792917352D;
        // 
        // GroupFooter2
        // 
        this.GroupFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
        this.GroupFooter2.Dpi = 254F;
        this.GroupFooter2.HeightF = 50F;
        this.GroupFooter2.Level = 1;
        this.GroupFooter2.Name = "GroupFooter2";
        // 
        // xrTable5
        // 
        this.xrTable5.Dpi = 254F;
        this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
        this.xrTable5.SizeF = new System.Drawing.SizeF(1900F, 50F);
        this.xrTable5.StyleName = "styleTabela";
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20,
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrTableCell25});
        this.xrTableRow5.Dpi = 254F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1D;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.Dpi = 254F;
        this.xrTableCell20.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell20.Multiline = true;
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseFont = false;
        this.xrTableCell20.Text = "Total";
        this.xrTableCell20.Weight = 0.64986156473175627D;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Dpi = 254F;
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.Weight = 0.64986128869730375D;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.Dpi = 254F;
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.StylePriority.UseTextAlignment = false;
        this.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell22.Weight = 0.344044423177196D;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.Dpi = 254F;
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.StylePriority.UseTextAlignment = false;
        this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell23.Weight = 0.38227146182580679D;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ItensOrcamentoProjeto.ValorTotal")});
        this.xrTableCell24.Dpi = 254F;
        this.xrTableCell24.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.StylePriority.UseFont = false;
        this.xrTableCell24.StylePriority.UseTextAlignment = false;
        xrSummary2.FormatString = "{0:n2}";
        xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell24.Summary = xrSummary2;
        this.xrTableCell24.Text = "xrTableCell24";
        this.xrTableCell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell24.Weight = 0.38227147089316238D;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.Dpi = 254F;
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.Weight = 0.49695294856951178D;
        // 
        // pCodigoProjeto
        // 
        this.pCodigoProjeto.Description = "Código do Projeto";
        this.pCodigoProjeto.Name = "pCodigoProjeto";
        this.pCodigoProjeto.Type = typeof(short);
        this.pCodigoProjeto.Value = ((short)(-1));
        this.pCodigoProjeto.Visible = false;
        // 
        // ds
        // 
        this.ds.DataSetName = "DsPlanilhaCustosProjeto";
        this.ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // pLinhaBase
        // 
        this.pLinhaBase.Description = "Linha de Base";
        this.pLinhaBase.Name = "pLinhaBase";
        this.pLinhaBase.Type = typeof(short);
        this.pLinhaBase.Value = ((short)(-1));
        this.pLinhaBase.Visible = false;
        // 
        // ta
        // 
        this.ta.ClearBeforeFill = true;
        // 
        // relPlanilhaCustosProjeto
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.GroupHeader2,
            this.GroupHeader1,
            this.GroupFooter1,
            this.GroupFooter2});
        this.DataAdapter = this.ta;
        this.DataMember = "ItensOrcamentoProjeto";
        this.DataSource = this.ds;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 8F);
        this.PageHeight = 2969;
        this.PageWidth = 2101;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pCodigoProjeto,
            this.pNomeProjeto,
            this.pLinhaBase});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 25F;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.styleTitulo,
            this.styleSubTitulo,
            this.styleTabela,
            this.styleCabecalhoTabela});
        this.Version = "12.1";
        this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.relPlanilhaCustosProjeto_DataSourceDemanded);
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void relPlanilhaCustosProjeto_DataSourceDemanded(object sender, EventArgs e)
    {
        LoadData();
    }

    private void LoadData()
    {
        string comandoSql = string.Format("SELECT NomeProjeto FROM Projeto WHERE CodigoProjeto = {0}", pCodigoProjeto.Value);
        DataSet dsTemp = cDados.getDataSet(comandoSql);
        pNomeProjeto.Value = dsTemp.Tables[0].Rows[0]["NomeProjeto"];
        DefineLogoEntidade();
        ta.Connection.ConnectionString = cDados.classeDados.getStringConexao();
        ta.FillBy(ds.ItensOrcamentoProjeto, (int)pCodigoProjeto.Value, (int)pLinhaBase.Value);
    }

    private void DefineLogoEntidade()
    {
        Int32 codigoEntidade = (Int32)cDados.getInfoSistema("CodigoEntidade");
        DataSet dsTemp = cDados.getLogoEntidade(codigoEntidade, "");
        Byte[] binaryImage = (Byte[])dsTemp.Tables[0].Rows[0]["LogoUnidadeNegocio"];
        MemoryStream ms = new MemoryStream(binaryImage);
        picLogoEntidade.Image = Bitmap.FromStream(ms);
    }
}
