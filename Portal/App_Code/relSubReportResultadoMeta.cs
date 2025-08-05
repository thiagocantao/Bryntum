using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

/// <summary>
/// Summary description for relSubreportProdutos
/// </summary>
public class relSubreportResultadoMeta : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    public DevExpress.XtraReports.Parameters.Parameter CodProjeto;
    private DsAcompanhamento dsAcompanhamento1;
    private dados cDados = CdadosUtil.GetCdados(null);
    private ReportHeaderBand ReportHeader;
    private XRLabel xrLabel1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell celulaProjeto;
    private XRTableCell celulaMeta;
    private XRPageBreak xrPageBreak1;
    private XRTable tabelaResultados;
    private XRTableRow xrTableRow2;
    private XRTableCell celulaAno;
    private XRTableCell celulaMetaRefAno;
    private XRTableCell celulaResultado;
    private XRTableCell celulaStatus;
    private XRPictureBox imgCorStatus;
    private DsAcompanhamento dsAcompanhamento2;
    private GroupHeaderBand GroupHeader2;
    private XRTable xrTable2;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private PageHeaderBand PageHeader;
    private int codigoUnidadeGlobal = 0;
    private PageFooterBand PageFooter;
    private XRPictureBox xrPictureBox3;
    private XRLabel xrLabel2;
    private XRLabel xrLabel3;
    private XRPictureBox xrPictureBox1;
    private XRPictureBox xrPictureBox2;
    private XRPageInfo xrPageInfo1;
    private XRLabel lblMesReferencia;
    private XRLabel lblMesReferencia1;
    private XRPictureBox xrPictureBox4;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public relSubreportResultadoMeta(int codigoUsuario, int codigoEntidade, int codigoUnidade, int ano)
    {
        InitializeComponent();
        codigoUnidadeGlobal = codigoUnidade;
        InitData(codigoUsuario, codigoEntidade, codigoUnidade, ano);
    }

    private void InitData(int codigoUsuario, int codigoEntidade, int codigoUnidade, int ano)
    {

        cDados = CdadosUtil.GetCdados(null);
        string comandoSql = string.Empty;

        comandoSql = string.Format(@"DECLARE @RC int
        DECLARE @codigoUsuario int
        DECLARE @codigoEntidade int
        DECLARE @codigoUnidade int
        DECLARE @ano as smallint

        SET @codigoUsuario = {2}
        SET @codigoEntidade = {3}
        SET @codigoUnidade = {4}
        SET @ano = {5}
        EXECUTE @RC = {0}.{1}.p_DadosRelatorioReprojetos 
        @codigoUsuario
        ,@codigoEntidade
        ,@codigoUnidade
        ,@ano", cDados.getDbName(), cDados.getDbOwner(), codigoUsuario, codigoEntidade, codigoUnidade, ano);

        DataSet ds = cDados.getDataSet(comandoSql);

        //dsAcompanhamento1.Load(ds.CreateDataReader(), LoadOption.OverwriteChanges, "ResultadoMeta");
        dsAcompanhamento1.EnforceConstraints = false;
        dsAcompanhamento1.Load(ds.CreateDataReader(), LoadOption.OverwriteChanges, "Projetos1", "Projetos", "Indicadores", "Desdobramento", "Resultado", "ResultadoMeta");


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
        //string resourceFileName = "relSubReportResultadoMeta.resx";
        System.Resources.ResourceManager resources = global::Resources.relSubReportResultadoMeta.ResourceManager;
        DevExpress.XtraReports.UI.DetailBand Detail;
        this.tabelaResultados = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celulaAno = new DevExpress.XtraReports.UI.XRTableCell();
        this.celulaMetaRefAno = new DevExpress.XtraReports.UI.XRTableCell();
        this.celulaResultado = new DevExpress.XtraReports.UI.XRTableCell();
        this.celulaStatus = new DevExpress.XtraReports.UI.XRTableCell();
        this.imgCorStatus = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celulaProjeto = new DevExpress.XtraReports.UI.XRTableCell();
        this.celulaMeta = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.CodProjeto = new DevExpress.XtraReports.Parameters.Parameter();
        this.dsAcompanhamento1 = new DsAcompanhamento();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.lblMesReferencia = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox3 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrPageBreak1 = new DevExpress.XtraReports.UI.XRPageBreak();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.dsAcompanhamento2 = new DsAcompanhamento();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.lblMesReferencia1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrPictureBox4 = new DevExpress.XtraReports.UI.XRPictureBox();
        Detail = new DevExpress.XtraReports.UI.DetailBand();
        ((System.ComponentModel.ISupportInitialize)(this.tabelaResultados)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsAcompanhamento1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsAcompanhamento2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tabelaResultados});
        Detail.Dpi = 254F;
        Detail.HeightF = 30F;
        Detail.KeepTogetherWithDetailReports = true;
        Detail.Name = "Detail";
        Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // tabelaResultados
        // 
        this.tabelaResultados.BorderColor = System.Drawing.Color.WhiteSmoke;
        this.tabelaResultados.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.tabelaResultados.Dpi = 254F;
        this.tabelaResultados.LocationFloat = new DevExpress.Utils.PointFloat(262.0795F, 0F);
        this.tabelaResultados.Name = "tabelaResultados";
        this.tabelaResultados.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.tabelaResultados.SizeF = new System.Drawing.SizeF(1653.188F, 30F);
        this.tabelaResultados.StylePriority.UseBorderColor = false;
        this.tabelaResultados.StylePriority.UseBorders = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celulaAno,
            this.celulaMetaRefAno,
            this.celulaResultado,
            this.celulaStatus});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 0.36008236525638865D;
        // 
        // celulaAno
        // 
        this.celulaAno.CanShrink = true;
        this.celulaAno.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ResultadoMeta.Ano")});
        this.celulaAno.Dpi = 254F;
        this.celulaAno.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celulaAno.Name = "celulaAno";
        this.celulaAno.StylePriority.UseFont = false;
        this.celulaAno.Text = "celulaAno";
        this.celulaAno.Weight = 0.21645806674298274D;
        // 
        // celulaMetaRefAno
        // 
        this.celulaMetaRefAno.CanShrink = true;
        this.celulaMetaRefAno.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ResultadoMeta.MetaRefAno")});
        this.celulaMetaRefAno.Dpi = 254F;
        this.celulaMetaRefAno.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celulaMetaRefAno.Name = "celulaMetaRefAno";
        this.celulaMetaRefAno.StylePriority.UseFont = false;
        this.celulaMetaRefAno.Text = "celulaMetaRefAno";
        this.celulaMetaRefAno.Weight = 0.21645806674298274D;
        // 
        // celulaResultado
        // 
        this.celulaResultado.CanShrink = true;
        this.celulaResultado.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ResultadoMeta.ResultadoRefAno")});
        this.celulaResultado.Dpi = 254F;
        this.celulaResultado.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celulaResultado.Name = "celulaResultado";
        this.celulaResultado.StylePriority.UseFont = false;
        this.celulaResultado.Text = "celulaResultado";
        this.celulaResultado.Weight = 0.75017512980708045D;
        // 
        // celulaStatus
        // 
        this.celulaStatus.CanShrink = true;
        this.celulaStatus.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.imgCorStatus});
        this.celulaStatus.Dpi = 254F;
        this.celulaStatus.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celulaStatus.Name = "celulaStatus";
        this.celulaStatus.StylePriority.UseFont = false;
        this.celulaStatus.StylePriority.UseTextAlignment = false;
        this.celulaStatus.Text = "celulaStatus";
        this.celulaStatus.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celulaStatus.Weight = 0.086203572909064052D;
        // 
        // imgCorStatus
        // 
        this.imgCorStatus.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.imgCorStatus.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.imgCorStatus.BorderWidth = 0;
        this.imgCorStatus.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "ResultadoMeta.DesempenhoRefAno", "~/imagens/{0}.gif")});
        this.imgCorStatus.Dpi = 254F;
        this.imgCorStatus.LocationFloat = new DevExpress.Utils.PointFloat(37.3451F, 0F);
        this.imgCorStatus.Name = "imgCorStatus";
        this.imgCorStatus.SizeF = new System.Drawing.SizeF(49.9303F, 30F);
        this.imgCorStatus.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.imgCorStatus.StylePriority.UseBorders = false;
        this.imgCorStatus.StylePriority.UseBorderWidth = false;
        // 
        // xrTable1
        // 
        this.xrTable1.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTable1.Dpi = 254F;
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(111.2674F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1804F, 30F);
        this.xrTable1.StylePriority.UseBackColor = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celulaProjeto,
            this.celulaMeta});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 0.094650205761316886D;
        // 
        // celulaProjeto
        // 
        this.celulaProjeto.CanShrink = true;
        this.celulaProjeto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ResultadoMeta.Projeto")});
        this.celulaProjeto.Dpi = 254F;
        this.celulaProjeto.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celulaProjeto.Multiline = true;
        this.celulaProjeto.Name = "celulaProjeto";
        this.celulaProjeto.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.celulaProjeto.StylePriority.UseFont = false;
        this.celulaProjeto.Text = "celulaProjeto";
        this.celulaProjeto.Weight = 0.47910637400997436D;
        this.celulaProjeto.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celulaProjeto_BeforePrint);
        // 
        // celulaMeta
        // 
        this.celulaMeta.CanShrink = true;
        this.celulaMeta.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ResultadoMeta.MetaDescritiva")});
        this.celulaMeta.Dpi = 254F;
        this.celulaMeta.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celulaMeta.Multiline = true;
        this.celulaMeta.Name = "celulaMeta";
        this.celulaMeta.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.celulaMeta.StylePriority.UseFont = false;
        this.celulaMeta.Text = "celulaMeta";
        this.celulaMeta.Weight = 0.48184006330916213D;
        this.celulaMeta.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celulaMeta_BeforePrint);
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 13F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.TopMargin.Visible = false;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 18F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.BottomMargin.Visible = false;
        // 
        // CodProjeto
        // 
        this.CodProjeto.Name = "CodProjeto";
        this.CodProjeto.Type = typeof(int);
        this.CodProjeto.Visible = false;
        // 
        // dsAcompanhamento1
        // 
        this.dsAcompanhamento1.DataSetName = "DsAcompanhamento";
        this.dsAcompanhamento1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblMesReferencia,
            this.xrLabel2,
            this.xrPictureBox3,
            this.xrPageBreak1,
            this.xrLabel1});
        this.ReportHeader.Dpi = 254F;
        this.ReportHeader.HeightF = 465.7725F;
        this.ReportHeader.KeepTogether = true;
        this.ReportHeader.Name = "ReportHeader";
        this.ReportHeader.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
        this.ReportHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ReportHeader_BeforePrint);
        // 
        // lblMesReferencia
        // 
        this.lblMesReferencia.Dpi = 254F;
        this.lblMesReferencia.Font = new System.Drawing.Font("Century Gothic", 12F);
        this.lblMesReferencia.LocationFloat = new DevExpress.Utils.PointFloat(764.6458F, 187.7483F);
        this.lblMesReferencia.Name = "lblMesReferencia";
        this.lblMesReferencia.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblMesReferencia.SizeF = new System.Drawing.SizeF(616.4791F, 58.42F);
        this.lblMesReferencia.StylePriority.UseFont = false;
        this.lblMesReferencia.StylePriority.UseTextAlignment = false;
        this.lblMesReferencia.Text = "lblMesReferencia";
        this.lblMesReferencia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(277.1633F, 47.51923F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(1648.354F, 58.41998F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.Text = "Relatório de Processos Redesenhados";
        // 
        // xrPictureBox3
        // 
        this.xrPictureBox3.Dpi = 254F;
        this.xrPictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox3.Image")));
        this.xrPictureBox3.LocationFloat = new DevExpress.Utils.PointFloat(2.999978F, 0F);
        this.xrPictureBox3.Name = "xrPictureBox3";
        this.xrPictureBox3.SizeF = new System.Drawing.SizeF(2098F, 402.0608F);
        // 
        // xrPageBreak1
        // 
        this.xrPageBreak1.Dpi = 254F;
        this.xrPageBreak1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPageBreak1.Name = "xrPageBreak1";
        // 
        // xrLabel1
        // 
        this.xrLabel1.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(114.2675F, 425.8733F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1801F, 39.89917F);
        this.xrLabel1.StylePriority.UseBorderDashStyle = false;
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "2.  VISÃO CONSOLIDADA DOS RESULTADOS";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // dsAcompanhamento2
        // 
        this.dsAcompanhamento2.DataSetName = "DsAcompanhamento";
        this.dsAcompanhamento2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2,
            this.xrTable1});
        this.GroupHeader2.Dpi = 254F;
        this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Projeto", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("MetaDescritiva", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader2.HeightF = 60F;
        this.GroupHeader2.Name = "GroupHeader2";
        // 
        // xrTable2
        // 
        this.xrTable2.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTable2.BorderColor = System.Drawing.Color.WhiteSmoke;
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.Dpi = 254F;
        this.xrTable2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(262.0795F, 30F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable2.SizeF = new System.Drawing.SizeF(1653.188F, 30F);
        this.xrTable2.StylePriority.UseBackColor = false;
        this.xrTable2.StylePriority.UseBorderColor = false;
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseFont = false;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 0.36008236525638865D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.CanShrink = true;
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.Text = "Ano";
        this.xrTableCell1.Weight = 0.21645806674298274D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.CanShrink = true;
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseFont = false;
        this.xrTableCell2.Text = "Meta";
        this.xrTableCell2.Weight = 0.21645806674298274D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.CanShrink = true;
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseFont = false;
        this.xrTableCell3.Text = "Resultado";
        this.xrTableCell3.Weight = 0.75017512980708045D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.CanShrink = true;
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseFont = false;
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.Text = "Status";
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell4.Weight = 0.086203572909064052D;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblMesReferencia1,
            this.xrLabel3,
            this.xrPictureBox1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 397.6249F;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.PrintOn = ((DevExpress.XtraReports.UI.PrintOnPages)((DevExpress.XtraReports.UI.PrintOnPages.NotWithReportHeader | DevExpress.XtraReports.UI.PrintOnPages.NotWithReportFooter)));
        this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
        // 
        // lblMesReferencia1
        // 
        this.lblMesReferencia1.Dpi = 254F;
        this.lblMesReferencia1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblMesReferencia1.LocationFloat = new DevExpress.Utils.PointFloat(764.6458F, 187.96F);
        this.lblMesReferencia1.Name = "lblMesReferencia1";
        this.lblMesReferencia1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblMesReferencia1.SizeF = new System.Drawing.SizeF(616.4791F, 58.42F);
        this.lblMesReferencia1.StylePriority.UseFont = false;
        this.lblMesReferencia1.StylePriority.UseTextAlignment = false;
        this.lblMesReferencia1.Text = "lblMesReferencia";
        this.lblMesReferencia1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(277.16F, 47.52F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(1648.354F, 58.41998F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "Relatório de Processos Redesenhados";
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Dpi = 254F;
        this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(1.499989F, 0F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(2099.5F, 397.6249F);
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox4,
            this.xrPageInfo1,
            this.xrPictureBox2});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 305.6955F;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageFooter_BeforePrint);
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Font = new System.Drawing.Font("Calibri", 10F);
        this.xrPageInfo1.ForeColor = System.Drawing.Color.DarkGray;
        this.xrPageInfo1.Format = "|{0}|";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1010.702F, 247.2755F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.Number;
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(60.85406F, 58.42F);
        this.xrPageInfo1.StylePriority.UseFont = false;
        this.xrPageInfo1.StylePriority.UseForeColor = false;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Dpi = 254F;
        this.xrPictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox2.Image")));
        this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(226.4959F, 7F);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.SizeF = new System.Drawing.SizeF(1688.771F, 193.3831F);
        // 
        // xrPictureBox4
        // 
        this.xrPictureBox4.Dpi = 254F;
        this.xrPictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox4.Image")));
        this.xrPictureBox4.LocationFloat = new DevExpress.Utils.PointFloat(111.2674F, 0F);
        this.xrPictureBox4.Name = "xrPictureBox4";
        this.xrPictureBox4.SizeF = new System.Drawing.SizeF(127F, 193.3831F);
        // 
        // relSubreportResultadoMeta
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.GroupHeader2,
            this.PageHeader,
            this.PageFooter});
        this.DataMember = "ResultadoMeta";
        this.DataSource = this.dsAcompanhamento1;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Margins = new System.Drawing.Printing.Margins(0, 0, 13, 18);
        this.PageHeight = 2969;
        this.PageWidth = 2101;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.CodProjeto});
        this.ReportPrintOptions.DetailCountOnEmptyDataSource = 0;
        this.ReportPrintOptions.PrintOnEmptyDataSource = false;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 10F;
        this.Version = "12.1";
        this.VerticalContentSplitting = DevExpress.XtraPrinting.VerticalContentSplitting.Smart;
        ((System.ComponentModel.ISupportInitialize)(this.tabelaResultados)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsAcompanhamento1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsAcompanhamento2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion



    private void celulaProjeto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (celulaProjeto.Text == "")
        {
            celulaProjeto.CanShrink = true;
            //xrTable2.CanShrink = true;
        }
        else
        {
            celulaProjeto.Text = "Projeto: " + celulaProjeto.Text;
        }

    }

    private void celulaMeta_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (celulaProjeto.Text == "")
        {
            celulaMeta.CanShrink = true;
            //xrTable2.CanShrink = true;
        }
        else
        {
            celulaMeta.Text = "Meta: " + celulaMeta.Text;
        }

    }


    private string getMesReferencia()
    {
        string mesAno = cDados.classeDados.getDateDB();
        string mesAnoFormatado = mesAno.Substring(3, 7);
        string mesporextenso = "";
        int mes = int.Parse(mesAnoFormatado.Substring(0, 2));
        int ano = int.Parse(mesAnoFormatado.Substring(3, 4));
        if (mes == 1 || mes == 2)
        {
            mesporextenso = "Jan/Fev";
        }
        else if (mes == 3 || mes == 4)
        {
            mesporextenso = "Mar/Abr";
        }
        else if (mes == 5 || mes == 6)
        {
            mesporextenso = "Mai/Jun";
        }
        else if (mes == 7 || mes == 8)
        {
            mesporextenso = "Jul/Ago";
        }
        else if (mes == 9 || mes == 10)
        {
            mesporextenso = "Set/Out";
        }
        else if (mes == 11 || mes == 12)
        {
            mesporextenso = "Nov/Dez";
        }
        mesporextenso += " - " + ano;
        return mesporextenso;

    }


    private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DataSet ds = cDados.getUnidadeNegocio(" and un.CodigoUnidadeNegocio = " + codigoUnidadeGlobal);
        string nomeUnidade = "";
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            nomeUnidade = ds.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();
        }



        object x = "";
        int codEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        DataSet dsLogoUnidade = cDados.getLogoEntidade(codigoUnidadeGlobal, x);
        if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]))
        {
            byte[] bytesLogo = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];
            System.IO.MemoryStream stream = new System.IO.MemoryStream(bytesLogo);
            Image logo = Image.FromStream(stream);
            xrPictureBox2.Image = logo;
        }
        //lblCabecalho.Text = "RELATÓRIO DE ACOMPANHAMENTO DOS PROCESSOS REDESENHADOS - " + nomeUnidade;
        lblMesReferencia1.Text = getMesReferencia();
    }

    private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DataSet ds = cDados.getUnidadeNegocio(" and un.CodigoUnidadeNegocio = " + codigoUnidadeGlobal);
        string nomeUnidade = "";
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            nomeUnidade = ds.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();
        }



        object x = "";
        int codEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        DataSet dsLogoUnidade = cDados.getLogoEntidade(codigoUnidadeGlobal, x);
        if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]))
        {
            byte[] bytesLogo = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];
            System.IO.MemoryStream stream = new System.IO.MemoryStream(bytesLogo);
            Image logo = Image.FromStream(stream);
            xrPictureBox1.Image = logo;
        }
        lblMesReferencia.Text = getMesReferencia();
        //lblPeriodoReferenciaRH.Text = "MÊS DE REFERÊNCIA: " + getMesReferencia();
        //lblCabecalho2.Text = "RELATÓRIO DE ACOMPANHAMENTO DOS PROCESSOS REDESENHADOS - " + nomeUnidade;
    }

    private void PageFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        /*DataSet ds = cDados.getUnidadeNegocio(" and un.CodigoUnidadeNegocio = " + codigoUnidadeGlobal);
        string nomeUnidade = "";
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            nomeUnidade = ds.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();
        }

        string mesAno = cDados.classeDados.getDateDB();
        string mesAnoFormatado = mesAno.Substring(3, 7);
        string mesporextenso = "";
        int mes = int.Parse(mesAnoFormatado.Substring(0, 2));
        int ano = int.Parse(mesAnoFormatado.Substring(3, 4));
        if (mes == 1)
        {
            mesporextenso = "Janeiro";
        }
        else if (mes == 2)
        {
            mesporextenso = "Fevereiro";
        }
        else if (mes == 3)
        {
            mesporextenso = "Março";
        }
        else if (mes == 4)
        {
            mesporextenso = "Abril";
        }
        else if (mes == 5)
        {
            mesporextenso = "Maio";
        }
        else if (mes == 6)
        {
            mesporextenso = "Junho";
        }
        else if (mes == 7)
        {
            mesporextenso = "Julho";
        }
        else if (mes == 8)
        {
            mesporextenso = "Agosto";
        }
        else if (mes == 9)
        {
            mesporextenso = "Setembro";
        }
        else if (mes == 10)
        {
            mesporextenso = "Outubro";
        }
        else if (mes == 11)
        {
            mesporextenso = "Novembro";
        }
        else if (mes == 12)
        {
            mesporextenso = "Dezembro";
        }*/

        //lblPeriodoReferencia.Text = "Período de Referência: " + mesporextenso + "/" + ano.ToString();
    }
}
