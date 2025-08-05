using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Drawing;

/// <summary>
/// Summary description for relAnalisePerformance
/// </summary>
public class relAnalisePerformance : DevExpress.XtraReports.UI.XtraReport
{
    private string iniciaisTipoAssociacao = "";
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private dsRelAnalisePerformance dsRelAnalisePerformance1;
    private dsRelAnalisePerformanceTableAdapters.AnalisePerformanceTableAdapter analisePerformanceTableAdapter1;
    private PageHeaderBand PageHeader;

    private DevExpress.XtraReports.Parameters.Parameter pLogoUnidade = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pCodigoEntidade = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pNomeMapa = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pdataImpressao = new DevExpress.XtraReports.Parameters.Parameter();

    dados cDados = CdadosUtil.GetCdados(null);

    private XRLabel lblNomeMapa;
    private XRLabel lblDataImpressao;
    private XRLabel lblTituloPrincipal;
    private XRLabel xrLabel2;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell lstAnaliseIndicador;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private GroupHeaderBand GroupHeader4;
    private XRLabel xrLabel3;
    private XRLabel lblIntervaloDatas;
    private XRLabel lblObjetivo;
    private GroupHeaderBand GroupHeader6;
    private XRLabel lblStatusObjetivo;
    private XRPictureBox logoUnidade;
    private XRTableCell lstMesIndicador;
    private XRTableCell lstAnoIndicador;
    private XRPageInfo xrPageInfo1;
    private PageFooterBand PageFooter;
    private XRTable tbCabecalhoAnalise;
    private XRTableRow xrTableRow2;
    private XRTableCell anoIndicador;
    private XRTableCell mesIndicador;
    private XRTableCell statusIndicador;
    private XRTableCell analiseIndicador;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRLabel xrLabel1;
    private XRTableCell lstStatusIndicador;
    private XRPictureBox imgStatus;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public relAnalisePerformance(int? codUnidadeSelecionada, short? codTipoAssociacao, int? CodigoMapaEstrategico, string dataInicial, string dataFinal, string tipoAnalise, int codigoEntidadeLogada, int codigoMapaDefaultUsuario, string iniciaisTipoAssociacao)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        pLogoUnidade.Name = "pLogoUnidade";
        pCodigoEntidade.Name = "pCodigoEntidade";
        pNomeMapa.Name = "pNomeMapa";
        pdataImpressao.Name = "pdataImpressao";
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[]
        {
            this.pLogoUnidade,
            this.pCodigoEntidade,
            this.pNomeMapa,
            this.pdataImpressao});

        cDados = CdadosUtil.GetCdados(null);

        analisePerformanceTableAdapter1.Connection.ConnectionString = cDados.classeDados.getStringConexao();
        analisePerformanceTableAdapter1.Fill(dsRelAnalisePerformance1.AnalisePerformance, codigoEntidadeLogada, codUnidadeSelecionada, CodigoMapaEstrategico, dataInicial, dataFinal, iniciaisTipoAssociacao);
        int numeroLinhas = dsRelAnalisePerformance1.Tables[0].Rows.Count;
        lblTituloPrincipal.Text = string.Format(@"Relatório de Análise de Performance - {0}", tipoAnalise == "OB" ? "Objetivos" : "Indicadores", dataInicial, dataFinal);
        if (numeroLinhas > 0)
        {
            lblIntervaloDatas.Text = string.Format(@"Análises Realizadas Entre: {0} e {1}", dataInicial, dataFinal);
        }
        else
        {
            lblIntervaloDatas.Text = string.Format(@"Nenhuma informação a apresentar.");
            anoIndicador.Visible = false;
            mesIndicador.Visible = false;
            statusIndicador.Visible = false;
            analiseIndicador.Visible = false;
            xrTableCell5.Visible = false;
            xrTableCell6.Visible = false;
        }
        this.iniciaisTipoAssociacao = iniciaisTipoAssociacao;
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
        //string resourceFileName = "relAnalisePerformance.resx";
        System.Resources.ResourceManager resources = global::Resources.relAnalisePerformance.ResourceManager;
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.lstAnoIndicador = new DevExpress.XtraReports.UI.XRTableCell();
        this.lstMesIndicador = new DevExpress.XtraReports.UI.XRTableCell();
        this.lstStatusIndicador = new DevExpress.XtraReports.UI.XRTableCell();
        this.imgStatus = new DevExpress.XtraReports.UI.XRPictureBox();
        this.lstAnaliseIndicador = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.lblStatusObjetivo = new DevExpress.XtraReports.UI.XRLabel();
        this.dsRelAnalisePerformance1 = new dsRelAnalisePerformance();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.analisePerformanceTableAdapter1 = new dsRelAnalisePerformanceTableAdapters.AnalisePerformanceTableAdapter();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.lblIntervaloDatas = new DevExpress.XtraReports.UI.XRLabel();
        this.lblNomeMapa = new DevExpress.XtraReports.UI.XRLabel();
        this.lblDataImpressao = new DevExpress.XtraReports.UI.XRLabel();
        this.lblTituloPrincipal = new DevExpress.XtraReports.UI.XRLabel();
        this.logoUnidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader4 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.lblObjetivo = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader6 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.tbCabecalhoAnalise = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.anoIndicador = new DevExpress.XtraReports.UI.XRTableCell();
        this.mesIndicador = new DevExpress.XtraReports.UI.XRTableCell();
        this.statusIndicador = new DevExpress.XtraReports.UI.XRTableCell();
        this.analiseIndicador = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsRelAnalisePerformance1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.tbCabecalhoAnalise)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1,
            this.lblStatusObjetivo});
        this.Detail.HeightF = 25F;
        this.Detail.KeepTogether = true;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(790F, 25F);
        this.xrTable1.StylePriority.UseBorders = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lstAnoIndicador,
            this.lstMesIndicador,
            this.lstStatusIndicador,
            this.lstAnaliseIndicador,
            this.xrTableCell2,
            this.xrTableCell3});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1;
        // 
        // lstAnoIndicador
        // 
        this.lstAnoIndicador.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lstAnoIndicador.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AnalisePerformance.Ano")});
        this.lstAnoIndicador.Font = new System.Drawing.Font("Verdana", 7F);
        this.lstAnoIndicador.Name = "lstAnoIndicador";
        this.lstAnoIndicador.StylePriority.UseBorders = false;
        this.lstAnoIndicador.StylePriority.UseFont = false;
        this.lstAnoIndicador.StylePriority.UseTextAlignment = false;
        this.lstAnoIndicador.Text = "lstAnoIndicador";
        this.lstAnoIndicador.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.lstAnoIndicador.Weight = 0.15427216965534918;
        // 
        // lstMesIndicador
        // 
        this.lstMesIndicador.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lstMesIndicador.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AnalisePerformance.Mes")});
        this.lstMesIndicador.Font = new System.Drawing.Font("Verdana", 7F);
        this.lstMesIndicador.Name = "lstMesIndicador";
        this.lstMesIndicador.StylePriority.UseBorders = false;
        this.lstMesIndicador.StylePriority.UseFont = false;
        this.lstMesIndicador.StylePriority.UseTextAlignment = false;
        this.lstMesIndicador.Text = "lstMesIndicador";
        this.lstMesIndicador.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.lstMesIndicador.Weight = 0.15427216965534918;
        // 
        // lstStatusIndicador
        // 
        this.lstStatusIndicador.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lstStatusIndicador.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.imgStatus});
        this.lstStatusIndicador.Name = "lstStatusIndicador";
        this.lstStatusIndicador.StylePriority.UseBorders = false;
        this.lstStatusIndicador.Weight = 0.21360761762996752;
        // 
        // imgStatus
        // 
        this.imgStatus.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.imgStatus.LocationFloat = new DevExpress.Utils.PointFloat(16.00001F, 2.999987F);
        this.imgStatus.Name = "imgStatus";
        this.imgStatus.SizeF = new System.Drawing.SizeF(22.74999F, 20.00001F);
        this.imgStatus.StylePriority.UseBorders = false;
        // 
        // lstAnaliseIndicador
        // 
        this.lstAnaliseIndicador.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lstAnaliseIndicador.CanShrink = true;
        this.lstAnaliseIndicador.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AnalisePerformance.Analise")});
        this.lstAnaliseIndicador.Font = new System.Drawing.Font("Verdana", 7F);
        this.lstAnaliseIndicador.Name = "lstAnaliseIndicador";
        this.lstAnaliseIndicador.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        this.lstAnaliseIndicador.StylePriority.UseBorders = false;
        this.lstAnaliseIndicador.StylePriority.UseFont = false;
        this.lstAnaliseIndicador.StylePriority.UsePadding = false;
        this.lstAnaliseIndicador.Text = "lstAnaliseIndicador";
        this.lstAnaliseIndicador.Weight = 0.937519120180528;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.CanShrink = true;
        this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AnalisePerformance.Recomendacoes")});
        this.xrTableCell2.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UseFont = false;
        this.xrTableCell2.StylePriority.UsePadding = false;
        this.xrTableCell2.Text = "xrTableCell2";
        this.xrTableCell2.Weight = 1.0308355197749728;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell3.CanShrink = true;
        this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AnalisePerformance.DataAnalisePerformance", "{0:dd/MM/yyyy}")});
        this.xrTableCell3.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseFont = false;
        this.xrTableCell3.StylePriority.UsePadding = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "xrTableCell3";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell3.Weight = 0.50949380821492862;
        // 
        // lblStatusObjetivo
        // 
        this.lblStatusObjetivo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AnalisePerformance.StatusAnalise")});
        this.lblStatusObjetivo.LocationFloat = new DevExpress.Utils.PointFloat(801.4583F, 0F);
        this.lblStatusObjetivo.Name = "lblStatusObjetivo";
        this.lblStatusObjetivo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblStatusObjetivo.SizeF = new System.Drawing.SizeF(10.41663F, 18F);
        this.lblStatusObjetivo.Text = "lblStatusObjetivo";
        this.lblStatusObjetivo.Visible = false;
        this.lblStatusObjetivo.TextChanged += new System.EventHandler(this.lblStatusObjetivo_TextChanged);
        // 
        // dsRelAnalisePerformance1
        // 
        this.dsRelAnalisePerformance1.DataSetName = "dsRelAnalisePerformance";
        this.dsRelAnalisePerformance1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLabel2.BorderWidth = 2;
        this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AnalisePerformance.NomeUnidadeNegocio")});
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 11F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.xrLabel2.SizeF = new System.Drawing.SizeF(789.4999F, 23F);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseBorderWidth = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.Text = "xrLabel2";
        // 
        // TopMargin
        // 
        this.TopMargin.HeightF = 30F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 30F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // analisePerformanceTableAdapter1
        // 
        this.analisePerformanceTableAdapter1.ClearBeforeFill = true;
        // 
        // PageHeader
        // 
        this.PageHeader.BorderColor = System.Drawing.Color.Gainsboro;
        this.PageHeader.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblIntervaloDatas,
            this.lblNomeMapa,
            this.lblDataImpressao,
            this.lblTituloPrincipal,
            this.logoUnidade});
        this.PageHeader.HeightF = 75F;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.StylePriority.UseBorderColor = false;
        this.PageHeader.StylePriority.UseBorders = false;
        this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
        // 
        // lblIntervaloDatas
        // 
        this.lblIntervaloDatas.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.lblIntervaloDatas.Font = new System.Drawing.Font("Verdana", 9.75F);
        this.lblIntervaloDatas.LocationFloat = new DevExpress.Utils.PointFloat(0F, 56.00001F);
        this.lblIntervaloDatas.Name = "lblIntervaloDatas";
        this.lblIntervaloDatas.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblIntervaloDatas.SizeF = new System.Drawing.SizeF(789.4999F, 18.99999F);
        this.lblIntervaloDatas.StylePriority.UseBorders = false;
        this.lblIntervaloDatas.StylePriority.UseFont = false;
        // 
        // lblNomeMapa
        // 
        this.lblNomeMapa.BorderColor = System.Drawing.Color.DarkGray;
        this.lblNomeMapa.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblNomeMapa.BorderWidth = 1;
        this.lblNomeMapa.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
        this.lblNomeMapa.ForeColor = System.Drawing.Color.DarkGray;
        this.lblNomeMapa.LocationFloat = new DevExpress.Utils.PointFloat(116F, 27F);
        this.lblNomeMapa.Name = "lblNomeMapa";
        this.lblNomeMapa.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblNomeMapa.SizeF = new System.Drawing.SizeF(671F, 16F);
        this.lblNomeMapa.StylePriority.UseBorderColor = false;
        this.lblNomeMapa.StylePriority.UseBorders = false;
        this.lblNomeMapa.StylePriority.UseBorderWidth = false;
        this.lblNomeMapa.StylePriority.UseFont = false;
        this.lblNomeMapa.StylePriority.UseForeColor = false;
        this.lblNomeMapa.Text = "lblNomeMapa";
        // 
        // lblDataImpressao
        // 
        this.lblDataImpressao.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblDataImpressao.Font = new System.Drawing.Font("Verdana", 7F);
        this.lblDataImpressao.ForeColor = System.Drawing.Color.DarkGray;
        this.lblDataImpressao.LocationFloat = new DevExpress.Utils.PointFloat(590.5F, 45F);
        this.lblDataImpressao.Name = "lblDataImpressao";
        this.lblDataImpressao.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblDataImpressao.SizeF = new System.Drawing.SizeF(194F, 10F);
        this.lblDataImpressao.StylePriority.UseBorders = false;
        this.lblDataImpressao.StylePriority.UseFont = false;
        this.lblDataImpressao.StylePriority.UseForeColor = false;
        this.lblDataImpressao.StylePriority.UseTextAlignment = false;
        this.lblDataImpressao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
        // 
        // lblTituloPrincipal
        // 
        this.lblTituloPrincipal.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblTituloPrincipal.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblTituloPrincipal.LocationFloat = new DevExpress.Utils.PointFloat(114F, 0F);
        this.lblTituloPrincipal.Name = "lblTituloPrincipal";
        this.lblTituloPrincipal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblTituloPrincipal.SizeF = new System.Drawing.SizeF(670.5F, 23F);
        this.lblTituloPrincipal.StylePriority.UseBorders = false;
        this.lblTituloPrincipal.StylePriority.UseFont = false;
        this.lblTituloPrincipal.Text = "Relatório de Análises";
        // 
        // logoUnidade
        // 
        this.logoUnidade.Image = ((System.Drawing.Image)(resources.GetObject("logoUnidade.Image")));
        this.logoUnidade.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.logoUnidade.Name = "logoUnidade";
        this.logoUnidade.SizeF = new System.Drawing.SizeF(114F, 56.00001F);
        this.logoUnidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2});
        this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("NomeUnidadeNegocio", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader1.HeightF = 49F;
        this.GroupHeader1.Level = 1;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrLabel3
        // 
        this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AnalisePerformance.TituloMapaEstrategico")});
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 5F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.xrLabel3.SizeF = new System.Drawing.SizeF(790F, 23F);
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "xrLabel3";
        // 
        // GroupHeader4
        // 
        this.GroupHeader4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3});
        this.GroupHeader4.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("TituloMapaEstrategico", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader4.HeightF = 33.00001F;
        this.GroupHeader4.Level = 2;
        this.GroupHeader4.Name = "GroupHeader4";
        // 
        // lblObjetivo
        // 
        this.lblObjetivo.BorderColor = System.Drawing.Color.Goldenrod;
        this.lblObjetivo.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.lblObjetivo.BorderWidth = 5;
        this.lblObjetivo.CanShrink = true;
        this.lblObjetivo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AnalisePerformance.NomeObjeto")});
        this.lblObjetivo.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
        this.lblObjetivo.ForeColor = System.Drawing.Color.Goldenrod;
        this.lblObjetivo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 11.99999F);
        this.lblObjetivo.Name = "lblObjetivo";
        this.lblObjetivo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblObjetivo.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.lblObjetivo.SizeF = new System.Drawing.SizeF(789.4999F, 32.99999F);
        this.lblObjetivo.StylePriority.UseBorderColor = false;
        this.lblObjetivo.StylePriority.UseBorders = false;
        this.lblObjetivo.StylePriority.UseBorderWidth = false;
        this.lblObjetivo.StylePriority.UseFont = false;
        this.lblObjetivo.StylePriority.UseForeColor = false;
        this.lblObjetivo.Text = "lblObjetivo";
        // 
        // GroupHeader6
        // 
        this.GroupHeader6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.tbCabecalhoAnalise,
            this.lblObjetivo});
        this.GroupHeader6.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("NomeObjeto", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader6.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WholePage;
        this.GroupHeader6.HeightF = 88F;
        this.GroupHeader6.KeepTogether = true;
        this.GroupHeader6.Name = "GroupHeader6";
        // 
        // xrLabel1
        // 
        this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AnalisePerformance.StatusAnalise")});
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(801.4583F, 26.99998F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(10.41663F, 18F);
        this.xrLabel1.Text = "lblStatusObjetivo";
        this.xrLabel1.Visible = false;
        this.xrLabel1.TextChanged += new System.EventHandler(this.lblStatusObjetivo_TextChanged);
        // 
        // tbCabecalhoAnalise
        // 
        this.tbCabecalhoAnalise.BackColor = System.Drawing.Color.Gainsboro;
        this.tbCabecalhoAnalise.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.tbCabecalhoAnalise.LocationFloat = new DevExpress.Utils.PointFloat(0F, 63F);
        this.tbCabecalhoAnalise.Name = "tbCabecalhoAnalise";
        this.tbCabecalhoAnalise.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.tbCabecalhoAnalise.SizeF = new System.Drawing.SizeF(790F, 25F);
        this.tbCabecalhoAnalise.StylePriority.UseBackColor = false;
        this.tbCabecalhoAnalise.StylePriority.UseBorders = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.anoIndicador,
            this.mesIndicador,
            this.statusIndicador,
            this.analiseIndicador,
            this.xrTableCell5,
            this.xrTableCell6});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // anoIndicador
        // 
        this.anoIndicador.BackColor = System.Drawing.Color.Gainsboro;
        this.anoIndicador.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.anoIndicador.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.anoIndicador.Name = "anoIndicador";
        this.anoIndicador.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.anoIndicador.StylePriority.UseBackColor = false;
        this.anoIndicador.StylePriority.UseBorders = false;
        this.anoIndicador.StylePriority.UseFont = false;
        this.anoIndicador.StylePriority.UseTextAlignment = false;
        this.anoIndicador.Text = "Ano";
        this.anoIndicador.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.anoIndicador.Weight = 0.15436985916997753;
        // 
        // mesIndicador
        // 
        this.mesIndicador.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.mesIndicador.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.mesIndicador.Name = "mesIndicador";
        this.mesIndicador.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.mesIndicador.StylePriority.UseBorders = false;
        this.mesIndicador.StylePriority.UseFont = false;
        this.mesIndicador.StylePriority.UseTextAlignment = false;
        this.mesIndicador.Text = "Mês";
        this.mesIndicador.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.mesIndicador.Weight = 0.15436985916997753;
        // 
        // statusIndicador
        // 
        this.statusIndicador.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.statusIndicador.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.statusIndicador.Name = "statusIndicador";
        this.statusIndicador.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.statusIndicador.StylePriority.UseBorders = false;
        this.statusIndicador.StylePriority.UseFont = false;
        this.statusIndicador.StylePriority.UseTextAlignment = false;
        this.statusIndicador.Text = "Status";
        this.statusIndicador.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.statusIndicador.Weight = 0.21374287040570328;
        // 
        // analiseIndicador
        // 
        this.analiseIndicador.BackColor = System.Drawing.Color.Gainsboro;
        this.analiseIndicador.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.analiseIndicador.CanShrink = true;
        this.analiseIndicador.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.analiseIndicador.Name = "analiseIndicador";
        this.analiseIndicador.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        this.analiseIndicador.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.analiseIndicador.StylePriority.UseBackColor = false;
        this.analiseIndicador.StylePriority.UseBorders = false;
        this.analiseIndicador.StylePriority.UseFont = false;
        this.analiseIndicador.StylePriority.UsePadding = false;
        this.analiseIndicador.StylePriority.UseTextAlignment = false;
        this.analiseIndicador.Text = "Análise";
        this.analiseIndicador.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.analiseIndicador.Weight = 0.93809376403151457;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell5.CanShrink = true;
        this.xrTableCell5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        this.xrTableCell5.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.xrTableCell5.StylePriority.UseBackColor = false;
        this.xrTableCell5.StylePriority.UseBorders = false;
        this.xrTableCell5.StylePriority.UseFont = false;
        this.xrTableCell5.StylePriority.UsePadding = false;
        this.xrTableCell5.StylePriority.UseTextAlignment = false;
        this.xrTableCell5.Text = "Recomendações";
        this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell5.Weight = 1.031507244436253;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell6.CanShrink = true;
        this.xrTableCell6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        this.xrTableCell6.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.xrTableCell6.StylePriority.UseBackColor = false;
        this.xrTableCell6.StylePriority.UseBorders = false;
        this.xrTableCell6.StylePriority.UseFont = false;
        this.xrTableCell6.StylePriority.UsePadding = false;
        this.xrTableCell6.StylePriority.UseTextAlignment = false;
        this.xrTableCell6.Text = "Data Análise";
        this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell6.Weight = 0.50981633945535154;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrPageInfo1.ForeColor = System.Drawing.Color.Silver;
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(689.4999F, 0F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(100F, 23F);
        this.xrPageInfo1.StylePriority.UseFont = false;
        this.xrPageInfo1.StylePriority.UseForeColor = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
        this.PageFooter.HeightF = 23F;
        this.PageFooter.Name = "PageFooter";
        // 
        // relAnalisePerformance
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.GroupHeader1,
            this.GroupHeader4,
            this.GroupHeader6,
            this.PageFooter});
        this.DataAdapter = this.analisePerformanceTableAdapter1;
        this.DataMember = "AnalisePerformance";
        this.DataSource = this.dsRelAnalisePerformance1;
        this.Margins = new System.Drawing.Printing.Margins(30, 30, 30, 30);
        this.SnapGridSize = 5F;
        this.SnappingMode = SnappingMode.SnapToGrid;
        this.Version = "10.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsRelAnalisePerformance1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.tbCabecalhoAnalise)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        lblNomeMapa.Text = pNomeMapa.Value.ToString();
        logoUnidade.ImageUrl = pLogoUnidade.Value.ToString();
        lblDataImpressao.Text = pdataImpressao.Value.ToString();
    }

    private void lblStatusObjetivo_TextChanged(object sender, EventArgs e)
    {
        string cor = lblStatusObjetivo.Text;
        cor = cor.Trim();
        if (cor == "Branco")
            imgStatus.ImageUrl = "~/imagens/Branco.gif";
        if (cor == "Vermelho")
            imgStatus.ImageUrl = "~/imagens/vermelho.gif";
        if (cor == "Verde")
            imgStatus.ImageUrl = "~/imagens/verde.gif";
        if (cor == "Amarelo")
            imgStatus.ImageUrl = "~/imagens/amarelo.gif";
        if (cor == "Azul")
            imgStatus.ImageUrl = "~/imagens/azul.gif";
        if (cor == "")
        {
            imgStatus.Visible = false;
        }
        if (this.iniciaisTipoAssociacao == "IN")
        {
            lblObjetivo.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            lblObjetivo.BorderColor = System.Drawing.Color.Black;
            lblObjetivo.BorderWidth = 2;
            lblObjetivo.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblObjetivo.ForeColor = System.Drawing.Color.SaddleBrown;

            anoIndicador.WidthF = 40.62f;
            mesIndicador.WidthF = 40.62f;
            statusIndicador.WidthF = 56.25f;

            lstAnoIndicador.WidthF = 40.62f;
            lstMesIndicador.WidthF = 40.62f;
            lstStatusIndicador.WidthF = 56.25f;
        }
        else
        {
            //texto
            anoIndicador.Text = "";
            mesIndicador.Text = "";
            statusIndicador.Text = "";

            lstAnoIndicador.Text = "";
            lstMesIndicador.Text = "";
            lstStatusIndicador.Text = "";

            //largura
            anoIndicador.WidthF = 0f;
            mesIndicador.WidthF = 0f;
            statusIndicador.WidthF = 0f;

            lstAnoIndicador.WidthF = 0f;
            lstMesIndicador.WidthF = 0f;
            lstStatusIndicador.WidthF = 0f;

            //bordas
            anoIndicador.Borders = BorderSide.None;
            mesIndicador.Borders = BorderSide.None;
            statusIndicador.Borders = BorderSide.None;

            lstAnoIndicador.Borders = BorderSide.None;
            lstMesIndicador.Borders = BorderSide.None;
            lstStatusIndicador.Borders = BorderSide.None;

            //anoIndicador.Borders = BorderSide.All;
            lstAnaliseIndicador.Borders = BorderSide.Bottom | BorderSide.Right | BorderSide.Left;
            xrTableCell2.Borders = BorderSide.Bottom | BorderSide.Right;
            xrTableCell3.Borders = BorderSide.Bottom | BorderSide.Right;
            analiseIndicador.Borders = BorderSide.All;
            //Cor de fundo
            anoIndicador.BackColor = Color.White;
            mesIndicador.BackColor = Color.White;
            statusIndicador.BackColor = Color.White;
        }

    }
}
