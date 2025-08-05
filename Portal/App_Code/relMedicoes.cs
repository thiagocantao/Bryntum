using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using naObraV2.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;

namespace naObra.Relatorios
{
    public partial class relMedicoes : DevExpress.XtraReports.UI.XtraReport
    {
        private Dictionary<string, decimal> valoresTotais = new Dictionary<string, decimal>(3);
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            //string resourceFileName = "relMedicoes.resx";
            System.Resources.ResourceManager resources = global::Resources.relMedicoes.ResourceManager;
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportFooterAssinantes = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLine4 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.nomeEmpresa = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellTotalPrevisto = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellTotalMes = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellTotalAteMes = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellSaldo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellTotalExtenso = new DevExpress.XtraReports.UI.XRTableCell();
            this.dadosMedicao = new DadosMedicao();
            this.codigoMedicao = new DevExpress.XtraReports.Parameters.Parameter();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.numeroMedicao = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlContratante = new DevExpress.XtraReports.UI.XRLabel();
            this.codigoContrato = new DevExpress.XtraReports.Parameters.Parameter();
            this.DetailValoresAdicionais = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.valorPagar = new DevExpress.XtraReports.Parameters.Parameter();
            this.valorAcumulado = new DevExpress.XtraReports.Parameters.Parameter();
            this.DetailReportAssinantes = new DevExpress.XtraReports.UI.DetailReportBand();
            this.DetailAssinantes = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTableAssinantes = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLine6 = new DevExpress.XtraReports.UI.XRLine();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter3 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrPageInfo3 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.nomeContratada = new DevExpress.XtraReports.Parameters.Parameter();
            this.logoRelatorio = new DevExpress.Utils.ImageCollection(this.components);
            this.Saldo = new DevExpress.XtraReports.UI.CalculatedField();
            this.DetailValoresAdicionais2 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader4 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter4 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTable9 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.valorPagar2 = new DevExpress.XtraReports.Parameters.Parameter();
            this.valorAcumulado2 = new DevExpress.XtraReports.Parameters.Parameter();
            this.DetailValorLiquido = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail4 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable10 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
            this.valorLiquidoReceber = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTable11 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
            this.DetailComentario = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail5 = new DevExpress.XtraReports.UI.DetailBand();
            this.ValorTotalAcumulado = new DevExpress.XtraReports.UI.CalculatedField();
            this.ValorMesSemNulos = new DevExpress.XtraReports.UI.CalculatedField();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dadosMedicao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableAssinantes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoRelatorio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2});
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 150F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.Dpi = 254F;
            this.xrPageInfo2.Format = "{0} de {1}";
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(2415F, 91.58F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(244.2397F, 58.42F);
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 150F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportFooterAssinantes
            // 
            this.ReportFooterAssinantes.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine4,
            this.xrLine1,
            this.xrLabel13,
            this.xrPageInfo1,
            this.xrLabel10});
            this.ReportFooterAssinantes.Dpi = 254F;
            this.ReportFooterAssinantes.Expanded = false;
            this.ReportFooterAssinantes.HeightF = 265.1592F;
            this.ReportFooterAssinantes.Name = "ReportFooterAssinantes";
            this.ReportFooterAssinantes.Visible = false;
            // 
            // xrLine4
            // 
            this.xrLine4.Dpi = 254F;
            this.xrLine4.LineWidth = 3;
            this.xrLine4.LocationFloat = new DevExpress.Utils.PointFloat(2059.999F, 166.895F);
            this.xrLine4.Name = "xrLine4";
            this.xrLine4.SizeF = new System.Drawing.SizeF(550F, 34.4F);
            // 
            // xrLine1
            // 
            this.xrLine1.Dpi = 254F;
            this.xrLine1.LineWidth = 3;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 166.895F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(670.0001F, 34.40001F);
            // 
            // xrLabel13
            // 
            this.xrLabel13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.nomeEmpresa, "Text", "Aprovação - {0}")});
            this.xrLabel13.Dpi = 254F;
            this.xrLabel13.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(2059.999F, 201.2949F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(550F, 58.42F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "xrLabel13";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // nomeEmpresa
            // 
            this.nomeEmpresa.Name = "nomeEmpresa";
            this.nomeEmpresa.Visible = false;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Dpi = 254F;
            this.xrPageInfo1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo1.Format = "Data: {0:dd.MM.yyyy}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1174.75F, 63.5F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(334.6666F, 58.42F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel10
            // 
            this.xrLabel10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.Contratada", "Medição Financeira - {0}")});
            this.xrLabel10.Dpi = 254F;
            this.xrLabel10.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 201.295F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(670F, 58.42F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "xrLabel10";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.GroupHeader1,
            this.GroupFooter1});
            this.DetailReport.DataMember = "Medicao.Medicao_ItensMedicao";
            this.DetailReport.DataSource = this.dadosMedicao;
            this.DetailReport.Dpi = 254F;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail1.Dpi = 254F;
            this.Detail1.HeightF = 50F;
            this.Detail1.Name = "Detail1";
            this.Detail1.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("EstruturaHierarquica", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Dpi = 254F;
            this.xrTable2.Font = new System.Drawing.Font("Calibri", 7.5F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable2.SizeF = new System.Drawing.SizeF(2759.24F, 50F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell17,
            this.xrTableCell21,
            this.xrTableCell18,
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrTableCell22,
            this.xrTableCell25,
            this.xrTableCell26,
            this.xrTableCell19,
            this.xrTableCell41});
            this.xrTableRow3.Dpi = 254F;
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 0.78740157480314954D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.Medicao_ItensMedicao.DescricaoItem")});
            this.xrTableCell17.Dpi = 254F;
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.Text = "xrTableCell17";
            this.xrTableCell17.Weight = 1.1530301945039667D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.Medicao_ItensMedicao.UnidadeMedidaItem")});
            this.xrTableCell21.Dpi = 254F;
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseTextAlignment = false;
            this.xrTableCell21.Text = "xrTableCell21";
            this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell21.Weight = 0.1784994824772313D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.Medicao_ItensMedicao.ValorUnitarioItem", "{0:n2}")});
            this.xrTableCell18.Dpi = 254F;
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseTextAlignment = false;
            this.xrTableCell18.Text = "xrTableCell18";
            this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell18.Weight = 0.33577552818885881D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.Medicao_ItensMedicao.QuantidadePrevistaTotal", "{0:n2}")});
            this.xrTableCell23.Dpi = 254F;
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StylePriority.UseTextAlignment = false;
            this.xrTableCell23.Text = "xrTableCell23";
            this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell23.Weight = 0.32003423360987576D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.Medicao_ItensMedicao.QuantidadeMedidaMes", "{0:n2}")});
            this.xrTableCell24.Dpi = 254F;
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.StylePriority.UseTextAlignment = false;
            this.xrTableCell24.Text = "xrTableCell24";
            this.xrTableCell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell24.Weight = 0.32867755797430637D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.Medicao_ItensMedicao.QuantidadeAcumuladaFinal", "{0:n2}")});
            this.xrTableCell22.Dpi = 254F;
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseTextAlignment = false;
            this.xrTableCell22.Text = "xrTableCell22";
            this.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell22.Weight = 0.32907911399543377D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.Medicao_ItensMedicao.ValorPrevistoTotal", "{0:n2}")});
            this.xrTableCell25.Dpi = 254F;
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.StylePriority.UseTextAlignment = false;
            this.xrTableCell25.Text = "xrTableCell25";
            this.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell25.Weight = 0.32514476983918317D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.Medicao_ItensMedicao.ValorMes", "{0:n2}")});
            this.xrTableCell26.Dpi = 254F;
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.StylePriority.UseTextAlignment = false;
            this.xrTableCell26.Text = "[Medicao_ItensMedicao.ValorMes]";
            this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell26.Weight = 0.32514514177851717D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.Medicao_ItensMedicao.ValorTotalAteMes", "{0:n2}")});
            this.xrTableCell19.Dpi = 254F;
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.StylePriority.UseTextAlignment = false;
            this.xrTableCell19.Text = "xrTableCell19";
            this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell19.Weight = 0.32487445182916919D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.Medicao_ItensMedicao.Saldo", "{0:n2}")});
            this.xrTableCell41.Dpi = 254F;
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.StylePriority.UseTextAlignment = false;
            this.xrTableCell41.Text = "xrTableCell41";
            this.xrTableCell41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell41.Weight = 0.33450876689352094D;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.GroupHeader1.Dpi = 254F;
            this.GroupHeader1.HeightF = 150F;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.RepeatEveryPage = true;
            // 
            // xrTable1
            // 
            this.xrTable1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Dpi = 254F;
            this.xrTable1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50.00002F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2,
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(2759.24F, 100F);
            this.xrTable1.StylePriority.UseBackColor = false;
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12});
            this.xrTableRow2.Dpi = 254F;
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 0.78740157480314954D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell8.Dpi = 254F;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.Text = "Descrição";
            this.xrTableCell8.Weight = 0.7729499635847058D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell9.Dpi = 254F;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.Text = "UM";
            this.xrTableCell9.Weight = 0.11965955696896068D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell10.Dpi = 254F;
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseBorders = false;
            this.xrTableCell10.Text = "Valor";
            this.xrTableCell10.Weight = 0.22509169541146129D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Dpi = 254F;
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Text = "Quantidades";
            this.xrTableCell11.Weight = 0.655475746465318D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Dpi = 254F;
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.Text = "Valores R$";
            this.xrTableCell12.Weight = 0.87795780833674D;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell5,
            this.xrTableCell2,
            this.xrTableCell14,
            this.xrTableCell13,
            this.xrTableCell6,
            this.xrTableCell16,
            this.xrTableCell15,
            this.xrTableCell3,
            this.xrTableCell35});
            this.xrTableRow1.Dpi = 254F;
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 0.78740157480314965D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.Dpi = 254F;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.Weight = 0.89393342990511326D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell5.Dpi = 254F;
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.Weight = 0.13838895268403556D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.Dpi = 254F;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.Text = "Unitário";
            this.xrTableCell2.Weight = 0.2603234966095484D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Dpi = 254F;
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.Text = "Prevista";
            this.xrTableCell14.Weight = 0.24811949173834344D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Dpi = 254F;
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Text = "No Mês";
            this.xrTableCell13.Weight = 0.25482044014413613D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Dpi = 254F;
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Text = "Acumulada";
            this.xrTableCell6.Weight = 0.25513205419008D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Dpi = 254F;
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.Text = "Previsto";
            this.xrTableCell16.Weight = 0.25208165119869758D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Dpi = 254F;
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.Text = "No Mês";
            this.xrTableCell15.Weight = 0.2520819326919756D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Dpi = 254F;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "Acumulado";
            this.xrTableCell3.Weight = 0.25187179779113417D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.Dpi = 254F;
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.Text = "Saldo";
            this.xrTableCell35.Weight = 0.25934176964115568D;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.GroupFooter1.Dpi = 254F;
            this.GroupFooter1.HeightF = 147.625F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.Dpi = 254F;
            this.xrTable3.Font = new System.Drawing.Font("Calibri", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4,
            this.xrTableRow5});
            this.xrTable3.SizeF = new System.Drawing.SizeF(2759.24F, 100F);
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseFont = false;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell33,
            this.cellTotalPrevisto,
            this.cellTotalMes,
            this.cellTotalAteMes,
            this.cellSaldo});
            this.xrTableRow4.Dpi = 254F;
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 0.78740157480314954D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.Dpi = 254F;
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.Text = "TOTAL GERAL";
            this.xrTableCell33.Weight = 2.1696420093945914D;
            // 
            // cellTotalPrevisto
            // 
            this.cellTotalPrevisto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.Medicao_ItensMedicao.ValorPrevistoTotal")});
            this.cellTotalPrevisto.Dpi = 254F;
            this.cellTotalPrevisto.Name = "cellTotalPrevisto";
            this.cellTotalPrevisto.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:n2}";
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.cellTotalPrevisto.Summary = xrSummary1;
            this.cellTotalPrevisto.Text = "cellTotalPrevisto";
            this.cellTotalPrevisto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.cellTotalPrevisto.Weight = 0.26670023725862524D;
            this.cellTotalPrevisto.SummaryGetResult += new SummaryGetResultHandler(onSummaryGetResult);
            this.cellTotalPrevisto.SummaryRowChanged += new EventHandler(onSummaryRowChanged);
            this.cellTotalPrevisto.SummaryReset += new EventHandler(onSummaryReset);
            // 
            // cellTotalMes
            // 
            this.cellTotalMes.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.Medicao_ItensMedicao.ValorMes")});
            this.cellTotalMes.Dpi = 254F;
            this.cellTotalMes.Name = "cellTotalMes";
            this.cellTotalMes.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:n2}";
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.cellTotalMes.Summary = xrSummary2;
            this.cellTotalMes.Text = "cellTotalMes";
            this.cellTotalMes.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.cellTotalMes.Weight = 0.2667002435514374D;
            // 
            // cellTotalAteMes
            // 
            this.cellTotalAteMes.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.Medicao_ItensMedicao.ValorTotalAteMes")});
            this.cellTotalAteMes.Dpi = 254F;
            this.cellTotalAteMes.Name = "cellTotalAteMes";
            this.cellTotalAteMes.StylePriority.UseTextAlignment = false;
            xrSummary3.FormatString = "{0:n2}";
            xrSummary3.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary3.IgnoreNullValues = true;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.cellTotalAteMes.Summary = xrSummary3;
            this.cellTotalAteMes.Text = "cellTotalAteMes";
            this.cellTotalAteMes.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.cellTotalAteMes.Weight = 0.26647850954395524D;
            this.cellTotalAteMes.SummaryGetResult += new SummaryGetResultHandler(onSummaryGetResult);
            this.cellTotalAteMes.SummaryRowChanged += new EventHandler(onSummaryRowChanged);
            this.cellTotalAteMes.SummaryReset += new EventHandler(onSummaryReset);
            // 
            // cellSaldo
            // 
            this.cellSaldo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.Medicao_ItensMedicao.Saldo")});
            this.cellSaldo.Dpi = 254F;
            this.cellSaldo.Name = "cellSaldo";
            this.cellSaldo.StylePriority.UseTextAlignment = false;
            xrSummary4.FormatString = "{0:n2}";
            xrSummary4.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary4.IgnoreNullValues = true;
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.cellSaldo.Summary = xrSummary4;
            this.cellSaldo.Text = "cellSaldo";
            this.cellSaldo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.cellSaldo.Weight = 0.27438141506989128D;
            this.cellSaldo.SummaryGetResult += new SummaryGetResultHandler(onSummaryGetResult);
            this.cellSaldo.SummaryRowChanged += new EventHandler(onSummaryRowChanged);
            this.cellSaldo.SummaryReset += new EventHandler(onSummaryReset);
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell27,
            this.cellTotalExtenso});
            this.xrTableRow5.Dpi = 254F;
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 0.78740157480314954D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.Dpi = 254F;
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.Text = "Obs.: ";
            this.xrTableCell27.Weight = 0.30487804333416274D;
            // 
            // cellTotalExtenso
            // 
            this.cellTotalExtenso.Dpi = 254F;
            this.cellTotalExtenso.Name = "cellTotalExtenso";
            this.cellTotalExtenso.Weight = 1.4695121857421687D;
            // 
            // dadosMedicao
            // 
            this.dadosMedicao.DataSetName = "DadosMedicao";
            this.dadosMedicao.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // codigoMedicao
            // 
            this.codigoMedicao.Name = "codigoMedicao";
            this.codigoMedicao.Type = typeof(int);
            this.codigoMedicao.ValueInfo = "0";
            this.codigoMedicao.Visible = false;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Condition = "IsNull([CodigoTarefaPai]) And (Not IsNull([CodigoItemMedicao]))";
            this.formattingRule1.DataMember = "Medicao.Medicao_ItensMedicao";
            // 
            // 
            // 
            this.formattingRule1.Formatting.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formattingRule1.Name = "formattingRule1";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1,
            this.xrLabel7,
            this.xrLabel12,
            this.xrLabel11,
            this.xrLabel3,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel9,
            this.xrLabel1,
            this.xrLabel2,
            this.xrLabel4,
            this.xrlContratante});
            this.PageHeader.Dpi = 254F;
            this.PageHeader.HeightF = 461.284F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrLabel7
            // 
            this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.NumeroContratoSAP", "SAP Nº {0}")});
            this.xrLabel7.Dpi = 254F;
            this.xrLabel7.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(1243.062F, 251.8397F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(1200F, 58.42F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.Text = "xrLabel7";
            // 
            // xrLabel12
            // 
            this.xrLabel12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.AnoMedicao")});
            this.xrLabel12.Dpi = 254F;
            this.xrLabel12.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(2055.812F, 310.2598F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(400F, 58.42F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.Text = "xrLabel8";
            this.xrLabel12.Visible = false;
            // 
            // xrLabel11
            // 
            this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.AnoMesMedicao", "PERÍODO  {0}")});
            this.xrLabel11.Dpi = 254F;
            this.xrLabel11.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(1243.062F, 310.2597F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(400.0002F, 58.42F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.Text = "PERÍODO:";
            // 
            // xrLabel3
            // 
            this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.MesMedicao", "{0: MMM}")});
            this.xrLabel3.Dpi = 254F;
            this.xrLabel3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(1655.812F, 310.2597F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(400F, 58.42F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.Text = "xrLabel7";
            this.xrLabel3.Visible = false;
            // 
            // xrLabel6
            // 
            this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.NumeroContrato", "CONTRATO Nº {0}")});
            this.xrLabel6.Dpi = 254F;
            this.xrLabel6.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(1243.062F, 193.42F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(1200F, 58.42F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.Text = "xrLabel6";
            // 
            // xrLabel5
            // 
            this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.InicioContrato", "Data: {0:dd/MM/yyyy} (Início)")});
            this.xrLabel5.Dpi = 254F;
            this.xrLabel5.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 310.2599F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(1200F, 58.41995F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.Text = "xrLabel5";
            // 
            // xrLabel9
            // 
            this.xrLabel9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.numeroMedicao, "Text", "MEDIÇÃO Nº {0}")});
            this.xrLabel9.Dpi = 254F;
            this.xrLabel9.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(1243.062F, 134.9999F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(400.0002F, 58.42F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.Text = "xrLabel9";
            // 
            // numeroMedicao
            // 
            this.numeroMedicao.Description = "numeroMedicao";
            this.numeroMedicao.Name = "numeroMedicao";
            this.numeroMedicao.Visible = false;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("Calibri", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(1016F, 25F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(627.0626F, 84.87833F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Boletim de Medição";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.Contratada", "CONTRATADA  -  {0}")});
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 134.9999F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(1200F, 58.42F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "[Contratada]";
            // 
            // xrLabel4
            // 
            this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.DescricaoObjetoContrato", "OBJETO: {0}")});
            this.xrLabel4.Dpi = 254F;
            this.xrLabel4.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 251.8399F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(1200F, 58.42F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.Text = "[NomeObra]";
            // 
            // xrlContratante
            // 
            this.xrlContratante.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.nomeEmpresa, "Text", "CONTRATANTE - {0}")});
            this.xrlContratante.Dpi = 254F;
            this.xrlContratante.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlContratante.LocationFloat = new DevExpress.Utils.PointFloat(0F, 193.4199F);
            this.xrlContratante.Name = "xrlContratante";
            this.xrlContratante.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrlContratante.SizeF = new System.Drawing.SizeF(1200F, 58.42001F);
            this.xrlContratante.StylePriority.UseFont = false;
            // 
            // codigoContrato
            // 
            this.codigoContrato.Name = "codigoContrato";
            this.codigoContrato.Type = typeof(int);
            this.codigoContrato.ValueInfo = "0";
            this.codigoContrato.Visible = false;
            // 
            // DetailValoresAdicionais
            // 
            this.DetailValoresAdicionais.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.GroupHeader2,
            this.GroupFooter2});
            this.DetailValoresAdicionais.DataMember = "ValoresAdicionais";
            this.DetailValoresAdicionais.DataSource = this.dadosMedicao;
            this.DetailValoresAdicionais.Dpi = 254F;
            this.DetailValoresAdicionais.KeepTogether = true;
            this.DetailValoresAdicionais.Level = 1;
            this.DetailValoresAdicionais.Name = "DetailValoresAdicionais";
            // 
            // Detail2
            // 
            this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
            this.Detail2.Dpi = 254F;
            this.Detail2.HeightF = 60.58335F;
            this.Detail2.Name = "Detail2";
            // 
            // xrTable4
            // 
            this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable4.Dpi = 254F;
            this.xrTable4.Font = new System.Drawing.Font("Calibri", 7.5F);
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(1041.1136F, 0F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.xrTable4.SizeF = new System.Drawing.SizeF(1718.886F, 60.58334F);
            this.xrTable4.StylePriority.UseBorders = false;
            this.xrTable4.StylePriority.UseFont = false;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell30,
            this.xrTableCell20,
            this.xrTableCell36,
            this.xrTableCell52});
            this.xrTableRow7.Dpi = 254F;
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 0.78740157480314954D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ValoresAdicionais.DescricaoAcessorio")});
            this.xrTableCell7.Dpi = 254F;
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Text = "xrTableCell7";
            this.xrTableCell7.Weight = 1.589748313017842D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ValoresAdicionais.Tipo")});
            this.xrTableCell30.Dpi = 254F;
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.StylePriority.UseTextAlignment = false;
            this.xrTableCell30.Text = "xrTableCell30";
            this.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell30.Weight = 0.41501587603964518D;
            this.xrTableCell30.EvaluateBinding += new BindingEventHandler(xrTableCell30_EvaluateBinding);
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ValoresAdicionais.Aliquota", "{0:n2}")});
            this.xrTableCell20.Dpi = 254F;
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseTextAlignment = false;
            this.xrTableCell20.Text = "xrTableCell20";
            this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell20.Weight = 0.41501634369930257D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ValoresAdicionais.Valor", "{0:n2}")});
            this.xrTableCell36.Dpi = 254F;
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.StylePriority.UseTextAlignment = false;
            this.xrTableCell36.Text = "xrTableCell36";
            this.xrTableCell36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell36.Weight = 0.41467085241104645D;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ValoresAdicionais.ValorAcumulado", "{0:n2}")});
            this.xrTableCell52.Dpi = 254F;
            this.xrTableCell52.Name = "xrTableCell52";
            this.xrTableCell52.StylePriority.UseTextAlignment = false;
            this.xrTableCell52.Text = "xrTableCell52";
            this.xrTableCell52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell52.Weight = 0.42840993394485327D;
            this.xrTableCell52.EvaluateBinding += new BindingEventHandler(xrTableCell52_EvaluateBinding);
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
            this.GroupHeader2.Dpi = 254F;
            this.GroupHeader2.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WholePage;
            this.GroupHeader2.HeightF = 95.25F;
            this.GroupHeader2.Name = "GroupHeader2";
            // 
            // xrTable5
            // 
            this.xrTable5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable5.Dpi = 254F;
            this.xrTable5.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(1041.1133F, 45.24998F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
            this.xrTable5.SizeF = new System.Drawing.SizeF(1718.886F, 49.99999F);
            this.xrTable5.StylePriority.UseBackColor = false;
            this.xrTable5.StylePriority.UseBorders = false;
            this.xrTable5.StylePriority.UseFont = false;
            this.xrTable5.StylePriority.UseTextAlignment = false;
            this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell38,
            this.xrTableCell29,
            this.xrTableCell39,
            this.xrTableCell40,
            this.xrTableCell51});
            this.xrTableRow8.Dpi = 254F;
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 0.78740157480314954D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.Dpi = 254F;
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.StylePriority.UseBorders = false;
            this.xrTableCell38.Text = "Descrição Itens Adicionais";
            this.xrTableCell38.Weight = 1.1019972987705338D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.Dpi = 254F;
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.Text = "Tipo";
            this.xrTableCell29.Weight = 0.2876848802113729D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.Dpi = 254F;
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.StylePriority.UseBorders = false;
            this.xrTableCell39.Text = "Alíquota";
            this.xrTableCell39.Weight = 0.28768503335691947D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.Dpi = 254F;
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.StylePriority.UseBorders = false;
            this.xrTableCell40.Text = "Valor R$";
            this.xrTableCell40.Weight = 0.28744521445650423D;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.Dpi = 254F;
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.StylePriority.UseTextAlignment = false;
            this.xrTableCell51.Text = "Acumulado";
            this.xrTableCell51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell51.Weight = 0.29696934522431928D;
            // 
            // GroupFooter2
            // 
            this.GroupFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
            this.GroupFooter2.Dpi = 254F;
            this.GroupFooter2.HeightF = 127F;
            this.GroupFooter2.Name = "GroupFooter2";
            // 
            // xrTable6
            // 
            this.xrTable6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable6.Dpi = 254F;
            this.xrTable6.Font = new System.Drawing.Font("Calibri", 7.5F);
            this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(2097.23F, 0F);
            this.xrTable6.Name = "xrTable6";
            this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
            this.xrTable6.SizeF = new System.Drawing.SizeF(437.0813F, 60.58334F);
            this.xrTable6.StylePriority.UseBorders = false;
            this.xrTable6.StylePriority.UseFont = false;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell31,
            this.xrTableCell43});
            this.xrTableRow11.Dpi = 254F;
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 0.78740157480314954D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.Dpi = 254F;
            this.xrTableCell31.Font = new System.Drawing.Font("Calibri", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell31.Multiline = true;
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.StylePriority.UseFont = false;
            this.xrTableCell31.StylePriority.UseTextAlignment = false;
            this.xrTableCell31.Text = "Total R$:\r\n";
            this.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell31.Weight = 0.46322452421898774D;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.valorPagar, "Text", "{0:n2}")});
            this.xrTableCell43.Dpi = 254F;
            this.xrTableCell43.Font = new System.Drawing.Font("Calibri", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.StylePriority.UseFont = false;
            this.xrTableCell43.StylePriority.UseTextAlignment = false;
            this.xrTableCell43.Text = "[Parameters.valorPagar]";
            this.xrTableCell43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell43.Weight = 0.46283785595479804D;
            // 
            // valorPagar
            // 
            this.valorPagar.Description = "Valor a pagar";
            this.valorPagar.Name = "valorPagar";
            this.valorPagar.Type = typeof(decimal);
            this.valorPagar.ValueInfo = "0";
            this.valorPagar.Visible = false;
            // 
            // valorAcumulado
            // 
            this.valorAcumulado.Description = "Valor acumulado ";
            this.valorAcumulado.Name = "valorAcumulado";
            this.valorAcumulado.Type = typeof(decimal);
            this.valorAcumulado.ValueInfo = "0";
            this.valorAcumulado.Visible = false;
            // 
            // DetailReportAssinantes
            // 
            this.DetailReportAssinantes.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.DetailAssinantes,
            this.GroupFooter3,
            this.GroupHeader3});
            this.DetailReportAssinantes.DataMember = "Assinantes";
            this.DetailReportAssinantes.DataSource = this.dadosMedicao;
            this.DetailReportAssinantes.Dpi = 254F;
            this.DetailReportAssinantes.Level = 5;
            this.DetailReportAssinantes.Name = "DetailReportAssinantes";
            // 
            // DetailAssinantes
            // 
            this.DetailAssinantes.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableAssinantes});
            this.DetailAssinantes.Dpi = 254F;
            this.DetailAssinantes.HeightF = 277.8125F;
            this.DetailAssinantes.MultiColumn.ColumnCount = 4;
            this.DetailAssinantes.MultiColumn.ColumnSpacing = 10F;
            this.DetailAssinantes.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
            this.DetailAssinantes.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnCount;
            this.DetailAssinantes.Name = "DetailAssinantes";
            // 
            // xrTableAssinantes
            // 
            this.xrTableAssinantes.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
            this.xrTableAssinantes.BorderColor = System.Drawing.Color.Transparent;
            this.xrTableAssinantes.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableAssinantes.Dpi = 254F;
            this.xrTableAssinantes.LocationFloat = new DevExpress.Utils.PointFloat(10.58333F, 52.54169F);
            this.xrTableAssinantes.Name = "xrTableAssinantes";
            this.xrTableAssinantes.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9,
            this.xrTableRow6,
            this.xrTableRow10});
            this.xrTableAssinantes.SizeF = new System.Drawing.SizeF(646.1875F, 173.8125F);
            this.xrTableAssinantes.StylePriority.UseBorderColor = false;
            this.xrTableAssinantes.StylePriority.UseBorders = false;
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell28});
            this.xrTableRow9.Dpi = 254F;
            this.xrTableRow9.Name = "xrTableRow9";
            this.xrTableRow9.Weight = 0.78740157480314954D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.CanGrow = false;
            this.xrTableCell28.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine6});
            this.xrTableCell28.Dpi = 254F;
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.Text = "xrTableCell28";
            this.xrTableCell28.Weight = 1.232418564629689D;
            // 
            // xrLine6
            // 
            this.xrLine6.Dpi = 254F;
            this.xrLine6.LineWidth = 3;
            this.xrLine6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 15.59999F);
            this.xrLine6.Name = "xrLine6";
            this.xrLine6.SizeF = new System.Drawing.SizeF(634.4167F, 34.40001F);
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4});
            this.xrTableRow6.Dpi = 254F;
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 0.78740157480314954D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.CanGrow = false;
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Assinantes.NomeUsuario")});
            this.xrTableCell4.Dpi = 254F;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "xrTableCell4";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell4.Weight = 1.232418564629689D;
            // 
            // xrTableRow10
            // 
            this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell32});
            this.xrTableRow10.Dpi = 254F;
            this.xrTableRow10.Name = "xrTableRow10";
            this.xrTableRow10.Weight = 0.78740157480314954D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.CanGrow = false;
            this.xrTableCell32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Assinantes.DescricaoCargo")});
            this.xrTableCell32.Dpi = 254F;
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.StylePriority.UseTextAlignment = false;
            this.xrTableCell32.Text = "xrTableCell32";
            this.xrTableCell32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell32.Weight = 1.232418564629689D;
            // 
            // GroupFooter3
            // 
            this.GroupFooter3.Dpi = 254F;
            this.GroupFooter3.HeightF = 254F;
            this.GroupFooter3.Name = "GroupFooter3";
            // 
            // GroupHeader3
            // 
            this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo3});
            this.GroupHeader3.Dpi = 254F;
            this.GroupHeader3.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WholePage;
            this.GroupHeader3.HeightF = 207.5033F;
            this.GroupHeader3.Name = "GroupHeader3";
            // 
            // xrPageInfo3
            // 
            this.xrPageInfo3.Dpi = 254F;
            this.xrPageInfo3.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.xrPageInfo3.Format = "Data: {0:dd.MM.yyyy}";
            this.xrPageInfo3.LocationFloat = new DevExpress.Utils.PointFloat(1142.285F, 25F);
            this.xrPageInfo3.Name = "xrPageInfo3";
            this.xrPageInfo3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrPageInfo3.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo3.SizeF = new System.Drawing.SizeF(379.6458F, 58.42F);
            this.xrPageInfo3.StylePriority.UseFont = false;
            this.xrPageInfo3.StylePriority.UseTextAlignment = false;
            this.xrPageInfo3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // nomeContratada
            // 
            this.nomeContratada.Name = "nomeContratada";
            this.nomeContratada.Visible = false;
            // 
            // logoRelatorio
            // 
            this.logoRelatorio.ImageSize = new System.Drawing.Size(513, 135);
            this.logoRelatorio.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("logoRelatorio.ImageStream")));
            // 
            // Saldo
            // 
            this.Saldo.DataMember = "Medicao.Medicao_ItensMedicao";
            this.Saldo.Expression = "[ValorPrevistoTotal] - [ValorTotalAteMes]";
            this.Saldo.Name = "Saldo";
            // 
            // DetailValoresAdicionais2
            // 
            this.DetailValoresAdicionais2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3,
            this.GroupHeader4,
            this.GroupFooter4});
            this.DetailValoresAdicionais2.DataMember = "ValoresAdicionais";
            this.DetailValoresAdicionais2.DataSource = this.dadosMedicao;
            this.DetailValoresAdicionais2.Dpi = 254F;
            this.DetailValoresAdicionais2.FilterString = "Contains([Tipo], \'Desconto\')";
            this.DetailValoresAdicionais2.Level = 2;
            this.DetailValoresAdicionais2.Name = "DetailValoresAdicionais2";
            this.DetailValoresAdicionais2.Visible = false;
            // 
            // Detail3
            // 
            this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable8});
            this.Detail3.Dpi = 254F;
            this.Detail3.HeightF = 60.58334F;
            this.Detail3.Name = "Detail3";
            // 
            // xrTable8
            // 
            this.xrTable8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable8.Dpi = 254F;
            this.xrTable8.Font = new System.Drawing.Font("Calibri", 7.5F);
            this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(1040.3541F, 0F);
            this.xrTable8.Name = "xrTable8";
            this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow13});
            this.xrTable8.SizeF = new System.Drawing.SizeF(1718.886F, 60.58334F);
            this.xrTable8.StylePriority.UseBorders = false;
            this.xrTable8.StylePriority.UseFont = false;
            // 
            // xrTableRow13
            // 
            this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell45,
            this.xrTableCell46,
            this.xrTableCell47,
            this.xrTableCell48,
            this.xrTableCell55});
            this.xrTableRow13.Dpi = 254F;
            this.xrTableRow13.Name = "xrTableRow13";
            this.xrTableRow13.Weight = 0.78740157480314954D;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ValoresAdicionais.DescricaoAcessorio")});
            this.xrTableCell45.Dpi = 254F;
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.Text = "xrTableCell7";
            this.xrTableCell45.Weight = 1.5911896050494867D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ValoresAdicionais.Tipo")});
            this.xrTableCell46.Dpi = 254F;
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.StylePriority.UseTextAlignment = false;
            this.xrTableCell46.Text = "xrTableCell30";
            this.xrTableCell46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell46.Weight = 0.4150161077586213D;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ValoresAdicionais.Aliquota", "{0:n2}")});
            this.xrTableCell47.Dpi = 254F;
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.StylePriority.UseTextAlignment = false;
            this.xrTableCell47.Text = "xrTableCell20";
            this.xrTableCell47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell47.Weight = 0.41501611198032645D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ValoresAdicionais.Valor", "{0:n2}")});
            this.xrTableCell48.Dpi = 254F;
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.StylePriority.UseTextAlignment = false;
            this.xrTableCell48.Text = "xrTableCell36";
            this.xrTableCell48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell48.Weight = 0.41467038897309416D;
            // 
            // xrTableCell55
            // 
            this.xrTableCell55.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ValoresAdicionais.ValorAcumulado", "{0:n2}")});
            this.xrTableCell55.Dpi = 254F;
            this.xrTableCell55.Name = "xrTableCell55";
            this.xrTableCell55.StylePriority.UseTextAlignment = false;
            this.xrTableCell55.Text = "xrTableCell55";
            this.xrTableCell55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell55.Weight = 0.42696910535116089D;
            // 
            // GroupHeader4
            // 
            this.GroupHeader4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable7});
            this.GroupHeader4.Dpi = 254F;
            this.GroupHeader4.HeightF = 106F;
            this.GroupHeader4.Name = "GroupHeader4";
            // 
            // xrTable7
            // 
            this.xrTable7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.xrTable7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable7.Dpi = 254F;
            this.xrTable7.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(1041.1133F, 55.83332F);
            this.xrTable7.Name = "xrTable7";
            this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow12});
            this.xrTable7.SizeF = new System.Drawing.SizeF(1718.886F, 49.99999F);
            this.xrTable7.StylePriority.UseBackColor = false;
            this.xrTable7.StylePriority.UseBorders = false;
            this.xrTable7.StylePriority.UseFont = false;
            this.xrTable7.StylePriority.UseTextAlignment = false;
            this.xrTable7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrTableRow12
            // 
            this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell34,
            this.xrTableCell37,
            this.xrTableCell42,
            this.xrTableCell44,
            this.xrTableCell54});
            this.xrTableRow12.Dpi = 254F;
            this.xrTableRow12.Name = "xrTableRow12";
            this.xrTableRow12.Weight = 0.78740157480314954D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.Dpi = 254F;
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.StylePriority.UseBorders = false;
            this.xrTableCell34.Text = "Descrição Itens Adicionais de Desconto";
            this.xrTableCell34.Weight = 1.1019973927210878D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.Dpi = 254F;
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.Text = "Tipo";
            this.xrTableCell37.Weight = 0.28768503084747382D;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.Dpi = 254F;
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.StylePriority.UseBorders = false;
            this.xrTableCell42.Text = "Alíquota";
            this.xrTableCell42.Weight = 0.28768455135212212D;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.Dpi = 254F;
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.StylePriority.UseBorders = false;
            this.xrTableCell44.Text = "Valor R$";
            this.xrTableCell44.Weight = 0.28744521432552106D;
            // 
            // xrTableCell54
            // 
            this.xrTableCell54.Dpi = 254F;
            this.xrTableCell54.Name = "xrTableCell54";
            this.xrTableCell54.StylePriority.UseTextAlignment = false;
            this.xrTableCell54.Text = "Acumulado";
            this.xrTableCell54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell54.Weight = 0.29696958604636525D;
            // 
            // GroupFooter4
            // 
            this.GroupFooter4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable9});
            this.GroupFooter4.Dpi = 254F;
            this.GroupFooter4.HeightF = 60.58334F;
            this.GroupFooter4.Name = "GroupFooter4";
            // 
            // xrTable9
            // 
            this.xrTable9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable9.Dpi = 254F;
            this.xrTable9.Font = new System.Drawing.Font("Calibri", 7.5F);
            this.xrTable9.LocationFloat = new DevExpress.Utils.PointFloat(2097.23F, 0F);
            this.xrTable9.Name = "xrTable9";
            this.xrTable9.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow14});
            this.xrTable9.SizeF = new System.Drawing.SizeF(437.0813F, 60.58334F);
            this.xrTable9.StylePriority.UseBorders = false;
            this.xrTable9.StylePriority.UseFont = false;
            // 
            // xrTableRow14
            // 
            this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell49,
            this.xrTableCell50});
            this.xrTableRow14.Dpi = 254F;
            this.xrTableRow14.Name = "xrTableRow14";
            this.xrTableRow14.Weight = 0.78740157480314954D;
            // 
            // xrTableCell49
            // 
            this.xrTableCell49.Dpi = 254F;
            this.xrTableCell49.Font = new System.Drawing.Font("Calibri", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell49.Multiline = true;
            this.xrTableCell49.Name = "xrTableCell49";
            this.xrTableCell49.StylePriority.UseFont = false;
            this.xrTableCell49.StylePriority.UseTextAlignment = false;
            this.xrTableCell49.Text = "Total  R$:\r\n";
            this.xrTableCell49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell49.Weight = 0.46322455708464533D;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.valorPagar2, "Text", "{0:n2}")});
            this.xrTableCell50.Dpi = 254F;
            this.xrTableCell50.Font = new System.Drawing.Font("Calibri", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.StylePriority.UseFont = false;
            this.xrTableCell50.StylePriority.UseTextAlignment = false;
            this.xrTableCell50.Text = "[Parameters.valorPagar2]";
            this.xrTableCell50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell50.Weight = 0.46283840609106269D;
            // 
            // valorPagar2
            // 
            this.valorPagar2.Description = "Valor a ser pago de descontos";
            this.valorPagar2.Name = "valorPagar2";
            this.valorPagar2.Type = typeof(decimal);
            this.valorPagar2.ValueInfo = "0";
            this.valorPagar2.Visible = false;
            // 
            // valorAcumulado2
            // 
            this.valorAcumulado2.Description = "Valor acumulado";
            this.valorAcumulado2.Name = "valorAcumulado2";
            this.valorAcumulado2.Type = typeof(decimal);
            this.valorAcumulado2.ValueInfo = "0";
            this.valorAcumulado2.Visible = false;
            // 
            // DetailValorLiquido
            // 
            this.DetailValorLiquido.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail4});
            this.DetailValorLiquido.Dpi = 254F;
            this.DetailValorLiquido.Level = 3;
            this.DetailValorLiquido.Name = "DetailValorLiquido";
            // 
            // Detail4
            // 
            this.Detail4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable10});
            this.Detail4.Dpi = 254F;
            this.Detail4.HeightF = 173.7708F;
            this.Detail4.Name = "Detail4";
            // 
            // xrTable10
            // 
            this.xrTable10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable10.Dpi = 254F;
            this.xrTable10.Font = new System.Drawing.Font("Calibri", 7.5F);
            this.xrTable10.LocationFloat = new DevExpress.Utils.PointFloat(2097.23F, 60F);
            this.xrTable10.Name = "xrTable10";
            this.xrTable10.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow15});
            this.xrTable10.SizeF = new System.Drawing.SizeF(662.0104F, 60.58334F);
            this.xrTable10.StylePriority.UseBorders = false;
            this.xrTable10.StylePriority.UseFont = false;
            // 
            // xrTableRow15
            // 
            this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell59,
            this.xrTableCell57});
            this.xrTableRow15.Dpi = 254F;
            this.xrTableRow15.Name = "xrTableRow15";
            this.xrTableRow15.Weight = 0.78740157480314954D;
            // 
            // xrTableCell59
            // 
            this.xrTableCell59.Dpi = 254F;
            this.xrTableCell59.Font = new System.Drawing.Font("Calibri", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell59.Name = "xrTableCell59";
            this.xrTableCell59.StylePriority.UseFont = false;
            this.xrTableCell59.StylePriority.UseTextAlignment = false;
            this.xrTableCell59.Text = "Valor liquido a receber R$:";
            this.xrTableCell59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell59.Weight = 0.29951089701231465D;
            // 
            // xrTableCell57
            // 
            this.xrTableCell57.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.valorLiquidoReceber, "Text", "{0:n2}")});
            this.xrTableCell57.Dpi = 254F;
            this.xrTableCell57.Font = new System.Drawing.Font("Calibri", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell57.Name = "xrTableCell57";
            this.xrTableCell57.StylePriority.UseFont = false;
            this.xrTableCell57.StylePriority.UseTextAlignment = false;
            this.xrTableCell57.Text = "[Parameters.valorLiquidoReceber]";
            this.xrTableCell57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell57.Weight = 0.15413249882698984D;
            // 
            // valorLiquidoReceber
            // 
            this.valorLiquidoReceber.Name = "valorLiquidoReceber";
            this.valorLiquidoReceber.Type = typeof(decimal);
            this.valorLiquidoReceber.ValueInfo = "0";
            this.valorLiquidoReceber.Visible = false;
            // 
            // xrTable11
            // 
            this.xrTable11.Dpi = 254F;
            this.xrTable11.LocationFloat = new DevExpress.Utils.PointFloat(10.58333F, 47.625F);
            this.xrTable11.Name = "xrTable11";
            this.xrTable11.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow16});
            this.xrTable11.SizeF = new System.Drawing.SizeF(2648.657F, 63.5F);
            // 
            // xrTableRow16
            // 
            this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell61});
            this.xrTableRow16.Dpi = 254F;
            this.xrTableRow16.Name = "xrTableRow16";
            this.xrTableRow16.Weight = 1D;
            // 
            // xrTableCell61
            // 
            this.xrTableCell61.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Medicao.ComentarioMedicao", "Comentário: {0}")});
            this.xrTableCell61.Dpi = 254F;
            this.xrTableCell61.Font = new System.Drawing.Font("Calibri", 9F);
            this.xrTableCell61.Name = "xrTableCell61";
            this.xrTableCell61.StylePriority.UseFont = false;
            this.xrTableCell61.Text = "xrTableCell61";
            this.xrTableCell61.Weight = 1D;
            // 
            // DetailComentario
            // 
            this.DetailComentario.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail5});
            this.DetailComentario.Dpi = 254F;
            this.DetailComentario.Level = 4;
            this.DetailComentario.Name = "DetailComentario";
            // 
            // Detail5
            // 
            this.Detail5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable11});
            this.Detail5.Dpi = 254F;
            this.Detail5.HeightF = 111.125F;
            this.Detail5.Name = "Detail5";
            // 
            // ValorTotalAcumulado
            // 
            this.ValorTotalAcumulado.DataMember = "ValoresAdicionais";
            this.ValorTotalAcumulado.Expression = "Iif(IsNull([ValorAcumulado]),  0, [ValorAcumulado]) + Iif(IsNull([Valor]), 0 , [Valor])";
            this.ValorTotalAcumulado.FieldType = DevExpress.XtraReports.UI.FieldType.Decimal;
            this.ValorTotalAcumulado.Name = "ValorTotalAcumulado";
            // 
            // ValorMesSemNulos
            // 
            this.ValorMesSemNulos.DataMember = "Medicao.Medicao_ItensMedicao";
            this.ValorMesSemNulos.Expression = "Iif(IsNull([ValorMes]), 0 , [ValorMes])";
            this.ValorMesSemNulos.Name = "ValorMesSemNulos";
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Dpi = 254F;
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("logoTP")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(513F, 135F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // relMedicoes
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportFooterAssinantes,
            this.DetailReport,
            this.PageHeader,
            this.DetailValoresAdicionais,
            this.DetailReportAssinantes,
            this.DetailValoresAdicionais2,
            this.DetailValorLiquido,
            this.DetailComentario});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.Saldo,
            this.ValorTotalAcumulado,
            this.ValorMesSemNulos});
            this.DataMember = "Medicao";
            this.DataSource = this.dadosMedicao;
            this.Dpi = 254F;
            this.FilterString = "[CodigoMedicao] = ?codigoMedicao";
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormattingRules.Add(this.formattingRule1);
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(151, 0, 150, 150);
            this.PageHeight = 2100;
            this.PageWidth = 2970;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.codigoMedicao,
            this.codigoContrato,
            this.nomeEmpresa,
            this.nomeContratada,
            this.valorPagar,
            this.numeroMedicao,
            this.valorPagar2,
            this.valorAcumulado,
            this.valorAcumulado2,
            this.valorLiquidoReceber});
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 31.75F;
            this.Version = "12.2";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.relMedicoes_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dadosMedicao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableAssinantes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoRelatorio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        void xrTableCell52_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //XtraReportBase report  = xrTableCell52.Report;
            //string tipo = report.GetCurrentColumnValue("Tipo").ToString().Trim();
            //string descricao = report.GetCurrentColumnValue("DescricaoAcessorio").ToString().Trim().ToLower();
            //if (tipo == "=")
            //    e.Value = string.Empty;
            //else if(descricao == "reajuste")
            //    e.Value = string.Empty;
        }

        void xrTableCell30_EvaluateBinding(object sender, BindingEventArgs e)
        {
            string value = e.Value.ToString().Trim();
            if (string.IsNullOrEmpty(value) || value == "=")
                e.Value = string.Empty;
        }

        void onSummaryReset(object sender, EventArgs e)
        {
            valoresTotais.Clear();
        }

        void onSummaryRowChanged(object sender, EventArgs e)
        {
            XRTableCell cell = (XRTableCell)sender;
            XtraReportBase report = cell.Report;
            string fieldName = cell.DataBindings["Text"].DataMember.Split('.').Last();
            object valor = report.GetCurrentColumnValue(fieldName);
            string registroEditavel = report.GetCurrentColumnValue<string>("RegistroEditavel");
            if (registroEditavel == "S")
            {
                decimal valorDecimal = valor == null || Convert.IsDBNull(valor) ? 0 : Convert.ToDecimal(valor);
                if (valoresTotais.ContainsKey(fieldName))
                {
                    decimal valorSomatorio = valoresTotais[fieldName];
                    valorSomatorio += valorDecimal;
                    valoresTotais[fieldName] = valorSomatorio;
                }
                else
                    valoresTotais.Add(fieldName, valorDecimal);
            }
        }

        void onSummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            XRTableCell cell = (XRTableCell)sender;
            string fieldName = cell.DataBindings["Text"].DataMember.Split('.').Last();
            if (valoresTotais.ContainsKey(fieldName))
            {
                decimal valorSomatorio = valoresTotais[fieldName];
                e.Result = valorSomatorio;
                e.Handled = true;
            }
        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooterAssinantes;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        private DevExpress.XtraReports.Parameters.Parameter codigoMedicao;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell17;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell21;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell18;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell23;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell24;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell22;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell25;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell26;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell19;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell10;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell11;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell12;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell14;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell13;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell16;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell15;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRTable xrTable3;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell33;
        private DevExpress.XtraReports.UI.XRTableCell cellTotalPrevisto;
        private DevExpress.XtraReports.UI.XRTableCell cellTotalMes;
        private DevExpress.XtraReports.UI.XRTableCell cellTotalAteMes;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell27;
        private DevExpress.XtraReports.UI.XRTableCell cellTotalExtenso;
        private DevExpress.XtraReports.UI.XRLine xrLine4;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel13;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel10;
        private DevExpress.XtraReports.UI.FormattingRule formattingRule1;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRLabel xrLabel9;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel xrlContratante;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
        private DevExpress.XtraReports.Parameters.Parameter codigoContrato;
        private DevExpress.XtraReports.Parameters.Parameter nomeEmpresa;
        private DevExpress.XtraReports.UI.DetailReportBand DetailValoresAdicionais;
        private DevExpress.XtraReports.UI.DetailBand Detail2;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReportAssinantes;
        private DevExpress.XtraReports.UI.DetailBand DetailAssinantes;
        private DadosMedicao dadosMedicao;
        private DevExpress.XtraReports.UI.XRTable xrTableAssinantes;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow6;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTable xrTable4;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow7;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell20;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell36;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader2;
        private DevExpress.XtraReports.UI.XRTable xrTable5;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow8;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell38;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell39;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell40;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow9;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell28;
        private DevExpress.XtraReports.UI.XRLine xrLine6;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow10;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell32;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter3;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo3;
        private DevExpress.XtraReports.Parameters.Parameter nomeContratada;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell30;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell29;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader3;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter2;
        private DevExpress.XtraReports.UI.XRTable xrTable6;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow11;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell31;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell43;
        private DevExpress.XtraReports.Parameters.Parameter valorPagar;
        private DevExpress.Utils.ImageCollection logoRelatorio;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell41;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell35;
        private DevExpress.XtraReports.UI.XRTableCell cellSaldo;
        private DevExpress.XtraReports.UI.CalculatedField Saldo;
        private DevExpress.XtraReports.Parameters.Parameter numeroMedicao;
        private DevExpress.XtraReports.UI.DetailReportBand DetailValoresAdicionais2;
        private DevExpress.XtraReports.UI.DetailBand Detail3;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader4;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter4;
        private DevExpress.XtraReports.Parameters.Parameter valorPagar2;
        private DevExpress.XtraReports.UI.XRTable xrTable8;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow13;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell45;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell46;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell47;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell48;
        private DevExpress.XtraReports.UI.XRTable xrTable7;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow12;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell34;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell37;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell42;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell44;
        private DevExpress.XtraReports.UI.XRTable xrTable9;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow14;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell49;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell50;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell52;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell51;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell55;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell54;
        private DevExpress.XtraReports.Parameters.Parameter valorAcumulado;
        private DevExpress.XtraReports.Parameters.Parameter valorAcumulado2;
        private DevExpress.XtraReports.UI.DetailReportBand DetailValorLiquido;
        private DevExpress.XtraReports.UI.DetailBand Detail4;
        private DevExpress.XtraReports.UI.XRTable xrTable10;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow15;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell59;
        private DevExpress.XtraReports.Parameters.Parameter valorLiquidoReceber;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell57;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRTable xrTable11;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow16;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell61;
        private DevExpress.XtraReports.UI.DetailReportBand DetailComentario;
        private DevExpress.XtraReports.UI.DetailBand Detail5;
        private DevExpress.XtraReports.UI.CalculatedField ValorTotalAcumulado;
        private DevExpress.XtraReports.UI.CalculatedField ValorMesSemNulos;

        internal dados cDados;
        //Totais
        private readonly decimal tPrevisto;
        private readonly decimal tAteMes;
        private decimal tMes;
        private decimal tValorFaturar;
        private readonly bool lerDadosLocal;
        private readonly string diretorioXmlDados;
        private DataTable dtValoresAdicionaisR;
        private DataTable dtItensMedicao;

        private DataTable dtAssinantesR;

        private const string NOME_ARQUIVO_MEDICAO = "Medicao.xml";
        private const string NOME_ARQUIVO_ITENS = "ItensMedicao.xml";
        private const string NOME_ARQUIVO_ADICIONAIS = "ValoresAdicionais.xml";
        private XRPictureBox xrPictureBox1;
        private const string NOME_ARQUIVO_ASSINANTES = "Assinantes.xml";

        public relMedicoes(int _codigoMedicao, int _codigoContrato, DataRow _medicaoAtual, string _nomeEmpresa, bool lerDadosLocal, decimal tPrevisto, decimal tAteMes, decimal tMes, DataTable dtValoresAdicionais, string enderecoXml, string numeroMedicao)
        {
            cDados = CdadosUtil.GetCdados(null);
            this.tPrevisto = tPrevisto;
            this.tAteMes = tAteMes;
            this.tMes = tMes;
            this.lerDadosLocal = lerDadosLocal;
            this.dtValoresAdicionaisR = dtValoresAdicionais.Copy();
            InitializeComponent();
            System.Resources.ResourceManager resources = global::Resources.relMedicoes.ResourceManager;
            if (_nomeEmpresa.ToLower().IndexOf("teles pires") >= 0)
                xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("logoTP")));
            else
                xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("logoNE")));

            //Define os labels de totais
            //cellTotalMes.Text = string.Format("{0:n2}", tMes);
            //cellTotalAteMes.Text = string.Format("{0:n2}", tAteMes);
            //cellTotalPrevisto.Text = string.Format("{0:n2}", tPrevisto);
            //cellSaldo.Text = String.Format("{0:n2}", (tPrevisto - tAteMes));


            diretorioXmlDados = string.Format(@"{0}\DATA", enderecoXml);
            codigoMedicao.Value = _codigoMedicao;
            codigoContrato.Value = _codigoContrato;
            nomeEmpresa.Value = _nomeEmpresa;
            //nomeContratada.Value = _medicaoAtual["Contratada"];
            //string anoMedicaoAtual = _medicaoAtual["AnoMesMedicao"].ToString().Split('/')[1];
            //try
            //{
            //    numeroMedicao.Value = _medicaoAtual["numeroMedicao"].ToString();
            //}
            //catch(Exception e)
            //{
            //    numeroMedicao.Value = anoMedicaoAtual;
            //}
            this.numeroMedicao.Value = numeroMedicao;
        }

        private void InitData()
        {
            DataSet ds;
            //if (!lerDadosLocal)
            //    ds = GetDataFromXml();
            //else
            ds = GetDataFromDataBase();

            dadosMedicao.Load(ds.CreateDataReader(),
                System.Data.LoadOption.OverwriteChanges,
                dadosMedicao.Medicao,
                dadosMedicao.ItensMedicao,
                dadosMedicao.ValoresAdicionais,
                dadosMedicao.Assinantes);
            tMes = dadosMedicao.ItensMedicao.Sum(im => im.IsValorMesNull() ? 0 : im.ValorMes);
            tValorFaturar = dadosMedicao.ValoresAdicionais.Single(va => !string.IsNullOrEmpty(va.Tipo) && (va.Tipo.Trim() == "=")).Valor;
            decimal tFaturamentoDireto = dadosMedicao.ValoresAdicionais.Single(va => !string.IsNullOrEmpty(va.DescricaoAcessorio) && (va.DescricaoAcessorio.Trim() == "Faturamento Direto")).Valor;
            decimal somaAcrescimos = dadosMedicao.ValoresAdicionais
                .Where(va => (!string.IsNullOrEmpty(va.Tipo) && !(va.Tipo.Trim() == "=") && va.Tipo != null && va.Tipo.Trim().ToLower() != "desconto"))
                .Sum(va => va.IsValorNull() ? 0 : va.Valor);
            decimal somaDescontos = dadosMedicao.ValoresAdicionais
                .Where(va => va.Tipo != null && va.Tipo.Trim().ToLower() == "desconto")
                .Sum(va => va.IsValorNull() ? 0 : va.Valor);
            decimal resultadoValoresAdicionais = somaAcrescimos - somaDescontos;

            valorPagar.Value = resultadoValoresAdicionais;
            valorLiquidoReceber.Value = (tValorFaturar + tFaturamentoDireto) + resultadoValoresAdicionais;
            int qtdeAssinantes = dadosMedicao.Assinantes.Count;
            if (qtdeAssinantes == 0)
                DetailAssinantes.Visible = false;
            else
            {
                DetailAssinantes.Visible = true;
                DetailAssinantes.MultiColumn.Mode = MultiColumnMode.UseColumnCount;
                DetailAssinantes.MultiColumn.ColumnCount = Math.Min(4, qtdeAssinantes);
                DetailAssinantes.MultiColumn.Layout = ColumnLayout.AcrossThenDown;
                float x = ((PageWidth / qtdeAssinantes) - (xrTableAssinantes.SizeF.Width)) / 2;
                xrTableAssinantes.LocationF = new PointF(x, 50);
            }
            NumeroPorExtenso vE = new NumeroPorExtenso((Decimal)tMes, true);
            String valorExtenso = "Reais";
            if (vE.numeroValido)
                valorExtenso = vE.ToString1();
            valorExtenso = (Decimal)tMes != 0 ? valorExtenso : "Zero reais.";
            cellTotalExtenso.Text = string.Format("Importa a presente medição em R$ {0:n2} ({1})"
                , tMes, valorExtenso);
        }

        private DataSet GetDataFromDataBase()
        {
            DataSet ds;

            #region Comando SQL
            string comandoSql = string.Format(@"
DECLARE @CodigoMedicao INT
    SET @CodigoMedicao = {0}
DECLARE @CodigoContrato INT
    SET @CodigoContrato = {1}

 EXEC p_ItensMedicaoRelatorioPDF @CodigoMedicao, @CodigoContrato"
                , codigoMedicao.Value
                , codigoContrato.Value);
            #endregion

            ds = cDados.getDataSet(comandoSql);
            ds.Tables[0].TableName = "Medicao";
            ds.Tables[1].TableName = "ItensMedicao";
            ds.Tables[2].TableName = "ValoresAdicionais";
            ds.Tables[3].TableName = "Assinantes";

            //Realiza os ajustes necessários nas colunas da 'ItensMedicao':
            //Remove a coluna 'QuantidadeAcumuladaFinal' e calculas as colunas 'calculo'
            DataTable dtItens = ds.Tables["ItensMedicao"];
            Type decimalType = System.Type.GetType("System.Decimal");
            //dtItens.Columns.Remove("QuantidadeAcumuladaFinal");
            dtItens.Columns.Add("ValorMesCalculo", decimalType, "ValorMes");
            dtItens.Columns.Add("ValorTotalAteMesCalculo", decimalType, "ValorTotalAteMes");
            dtItens.Columns.Add("ValorPrevistoTotalCalculo", decimalType,
                "(ISNULL(ValorUnitarioItem, 0) * ISNULL(QuantidadePrevistaTotal, 0))");
            dtItensMedicao = dtItens.Copy();
            return ds;
        }

        private System.Data.DataSet GetDataFromXml()
        {
            System.Data.DataSet ds = new System.Data.DataSet();

            ds.Tables.Add("rs");
            ds.Tables[0].ReadXmlSchema(string.Format(@"{0}\{1}", diretorioXmlDados, NOME_ARQUIVO_MEDICAO));
            ds.Tables[0].ReadXml(string.Format(@"{0}\{1}", diretorioXmlDados, NOME_ARQUIVO_MEDICAO));
            ds.Tables[0].TableName = "Medicao";

            ds.Tables.Add("rs");
            ds.Tables[1].ReadXmlSchema(string.Format(@"{0}\{1}", diretorioXmlDados, NOME_ARQUIVO_ITENS));
            ds.Tables[1].ReadXml(string.Format(@"{0}\{1}", diretorioXmlDados, NOME_ARQUIVO_ITENS));
            ds.Tables[1].TableName = "ItensMedicao";

            //Valores adicionais com desconto
            var filtro = (from DataRow dRow in dtValoresAdicionaisR.Rows
                          where dRow["CodigoContrato"].Equals(codigoContrato.Value)
                          && dRow["CodigoMedicao"].Equals(codigoMedicao.Value)
                          && (
                          (dRow["Valor"].ToString() != ""
                          && decimal.Parse(dRow["Valor"].ToString()) != 0)
                          ||
                          (dRow["ValorAcumulado"].ToString() != ""
                          && decimal.Parse(dRow["ValorAcumulado"].ToString()) != 0)
                          ||
                          (dRow["Aliquota"].ToString() != ""
                          && decimal.Parse(dRow["Aliquota"].ToString()) != 0))
                          select dRow);
            if (filtro.Count() > 0)
                dtValoresAdicionaisR = filtro.CopyToDataTable();
            else
                dtValoresAdicionaisR.Clear();

            //dtValoresAdicionaisR = Util.atualizarValoresDatatable<decimal>(dtValoresAdicionaisR, "ValorAcumulado", decimal.Zero).Copy();
            //dtValoresAdicionaisR = Util.atualizarValoresDatatable<decimal>(dtValoresAdicionaisR, "Valor", decimal.Zero).Copy();
            //dtValoresAdicionaisR = Util.atualizarValoresDatatable<decimal>(dtValoresAdicionaisR, "ValorAcumulado", decimal.Zero, "", "Valor").Copy();

            Object somaValorO1 = dtValoresAdicionaisR.Compute("Sum(Valor)", "(Tipo <> 'Desconto') or (Tipo = '') or (Tipo is null)");
            Object somaValorO1Des = dtValoresAdicionaisR.Compute("Sum(Valor)", "Tipo = 'Desconto'");
            Object somaAcO1 = dtValoresAdicionaisR.Compute("Sum(ValorAcumulado)", "(Tipo <> 'Desconto') or (Tipo = '') or (Tipo is null)");
            Object somaAcO1Des = dtValoresAdicionaisR.Compute("Sum(ValorAcumulado)", "Tipo = 'Desconto'");
            Decimal somaValor1 = decimal.Zero;
            if (somaValorO1.ToString() == "")
                somaValorO1 = decimal.Zero;
            if (somaValorO1Des.ToString() == "")
                somaValorO1Des = decimal.Zero;
            if (Convert.IsDBNull(somaAcO1))
                somaAcO1 = decimal.Zero;
            if (Convert.IsDBNull(somaAcO1Des))
                somaAcO1Des = decimal.Zero;
            if (somaValorO1.ToString() != "" || somaValorO1Des.ToString() != "")
            {
                /*                    
                                if (somaValor1.ToString() != "" && somaValor1.ToString() != "0")
                                    somaValor1 = (decimal)somaValorO1;
                                if (somaValorO1Des.ToString() != "" && somaValorO1Des.ToString() != "0")
                                    somaValor1 -= (decimal)somaValorO1Des;
                */
                somaValor1 = (decimal)somaValorO1 - (decimal)somaValorO1Des;
                valorPagar.Value = somaValor1;

                /*
                                if (somaAcO1.ToString() != "" && somaAcO1.ToString() != "0")    
                                    somaAc1 = (decimal)somaAcO1;
                                if (somaAcO1Des.ToString() != "" && somaAcO1Des.ToString() != "0")
                                    somaAc1 -= (decimal)somaAcO1Des;
                                valorAcumulado.Value = somaAc1;
                */

                valorAcumulado.Value = (decimal)somaAcO1 - (decimal)somaAcO1Des;
                //                    DetailValoresAdicionais.Visible = somaValor1 != 0;
            }
            else
            {
                DetailValoresAdicionais.Visible = false;
            }
            Decimal totalVA = (decimal)tMes + (somaValor1);
            valorLiquidoReceber.Value = totalVA;
            if (dtValoresAdicionaisR.Rows.Count == 0)
            {
                //DetailValoresAdicionais.Visible = false;
                //DetailValoresAdicionais2.Visible = false;
                //DetailValorLiquido.Visible = false;
                Detail2.Visible = false;
                GroupFooter2.Visible = false;
            }
            ds.Tables.Add(Util.padronizarReajuste(dtValoresAdicionaisR, false, true));
            ds.Tables[2].TableName = "ValoresAdicionais";


            if (File.Exists(string.Format(@"{0}\{1}", diretorioXmlDados, NOME_ARQUIVO_ASSINANTES)))
            {
                dtAssinantesR = new DataTable();
                dtAssinantesR.ReadXml(string.Format(@"{0}\{1}", diretorioXmlDados, NOME_ARQUIVO_ASSINANTES));
                DataRow novaLinha = dtAssinantesR.NewRow();
                novaLinha["NomeUsuario"] = nomeContratada.Value;
                novaLinha["DescricaoCargo"] = "Contratada";
                novaLinha["NumeroOrdem"] = 0;
                novaLinha["CodigoContrato"] = codigoContrato.Value;
                novaLinha["codigoMedicao"] = codigoMedicao.Value;
                dtAssinantesR.Rows.Add(novaLinha);
                filtro = (from DataRow dRow in dtAssinantesR.Rows
                          where dRow["CodigoContrato"].Equals(codigoContrato.Value)
                          && dRow["CodigoMedicao"].Equals(codigoMedicao.Value)
                          orderby dRow["NumeroOrdem"].ToString()
                          select dRow);
                if (filtro.Count() > 0)
                    dtAssinantesR = filtro.CopyToDataTable();
                else
                    dtAssinantesR.Clear();
                ds.Tables.Add(dtAssinantesR);
            }
            else
                ds.Tables.Add();
            ds.Tables[3].TableName = "Assinantes";

            DetailReportAssinantes.Visible = ds.Tables[3].Rows.Count > 1;
            ReportFooterAssinantes.Visible = ds.Tables[3].Rows.Count <= 1;

            return ds;
        }

        private void xrLabel7_EvaluateBinding(object sender, BindingEventArgs e)
        {
            string value = e.Value.ToString();
            string text = string.Empty;

            switch (value)
            {
                case "1":
                    text = "Janeiro";
                    break;
                case "2":
                    text = "Fevereiro";
                    break;
                case "3":
                    text = "Março";
                    break;
                case "4":
                    text = "Abril";
                    break;
                case "5":
                    text = "Maio";
                    break;
                case "6":
                    text = "Junho";
                    break;
                case "7":
                    text = "Julho";
                    break;
                case "8":
                    text = "Agosto";
                    break;
                case "9":
                    text = "Setembro";
                    break;
                case "10":
                    text = "Outubro";
                    break;
                case "11":
                    text = "Novembro";
                    break;
                case "12":
                    text = "Dezembro";
                    break;
                default:
                    text = "Indefinido";
                    break;
            }
            e.Binding.FormatString = text;
        }

        private void relMedicoes_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            InitData();
        }
    }
}
